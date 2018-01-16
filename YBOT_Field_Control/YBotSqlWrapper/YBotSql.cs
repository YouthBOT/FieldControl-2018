﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using HelpfulUtilites;

namespace YBotSqlWrapper
{
    public delegate void SqlMessageHandler(object sender, SqlMessageArgs args);
    public delegate void SqlStatusHandler(object sender);

    public class YbotSql
    {
        protected static YbotSql _instance = new YbotSql();
        public static YbotSql Instance {
            get {
                return _instance;
            }
        }

        private MySqlConnection sql;
        private SshClient ssh;

        public bool IsConnected {
            get {
                if (sql != null) {
                    return !((sql.State == ConnectionState.Broken) ||
                        (sql.State == ConnectionState.Closed) ||
                        (sql.State == ConnectionState.Connecting));
                } else {
                    return false;
                }
            }
        }

        protected YbotSql() {

        }

        public event SqlMessageHandler SqlMessageEvent;
        public event SqlStatusHandler SqlConnectedEvent;

        public void Connect(string serverIp, string password, bool useSsh = true) {
            Thread connectThread = new Thread(() => {
                if (useSsh) {
                    ConnectSsh(serverIp, password);
                    serverIp = "127.0.0.1";
                }
                ConnectMySql(serverIp, password);
            });

            connectThread.Start();
        }

        protected void ConnectSsh(string server, string password) {
            if (ssh == null) { // if the ssh object is not null, then a tunnel has already been created
                ssh = new SshClient(server, 22, "youthbot", password);
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs("Connecting to SSH server..."));

                try {
                    ssh.Connect();
                } catch (Exception ex) {
                    string text = "failure\n" + ex.ToString();
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(text));
                    return;
                }

                if (ssh.IsConnected) {
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs("connected\n"));

                    var tunnel = new ForwardedPortLocal("127.0.0.1", 3306, "127.0.0.1", 3306);
                    ssh.AddForwardedPort(tunnel);
                    tunnel.Start();
                }
            }
        }

        protected void ConnectMySql(string server, string password) {
            var sb = new MySqlConnectionStringBuilder();
            sb.Server = server;
            sb.Port = 3306;
            sb.Database = "ybot";
            sb.UserID = "youthbot";
            sb.Password = password;

            sql = new MySqlConnection(sb.ConnectionString);

            try {
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs("Connecting to MySQL server..."));
                sql.Open();
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs("connected\n"));
                SqlConnectedEvent?.Invoke(this);

                return;
            } catch (MySqlException ex) {
                if (ssh != null) {
                    ssh.Disconnect();
                    ssh.Dispose();
                }

                string text = "failure\n" + ex.ToString();
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, text));
                return;
            }
        }

        public void Disconnect() {
            sql.Close();
            sql.Dispose();

            if (ssh != null) {
                ssh.Disconnect();
                ssh.Dispose();
            }
        }

        public async void AddLog(string text, string type) {
            if ((sql != null) && (IsConnected)) {
                if (text.IsNotEmpty()) {
                    var query = "INSERT INTO event_log (event_id, event_type, event_message";
                    if (YBotSqlData.Global.currentTournament.IsNotEmpty()) {
                        query += ", tournament_id";
                    }
                    if (YBotSqlData.Global.currentMatchNumber != -1) {
                        query += ", match_number";
                    }
                    query += ") ";
                    query += string.Format("VALUES (NOW(), '{0}', '{1}'", type, text);
                    if (YBotSqlData.Global.currentTournament.IsNotEmpty()) {
                        query += string.Format(", '{0}'", YBotSqlData.Global.tournaments[YBotSqlData.Global.currentTournament].id);
                    }
                    if (YBotSqlData.Global.currentMatchNumber != -1) {
                        query += string.Format(", '{0}'", YBotSqlData.Global.currentMatchNumber);
                    }
                    query += ");";

                    var command = new MySqlCommand(query, sql);

                    try {
                        await command.ExecuteNonQueryAsync();
                    } catch (MySqlException ex) {
                        SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                    }
                }
            }
        }

        public async void AddMatch(Match match) {
            if ((sql != null) && (IsConnected)) {
                var query = "SELECT match_id FROM matches " +
                    string.Format("WHERE tournament_id={0} and match_number={1};", match.tournamentId, match.matchNumber);

                var command = new MySqlCommand(query, sql);

                System.Data.Common.DbDataReader reader;
                try {
                    reader = await command.ExecuteReaderAsync();
                } catch (MySqlException ex) {
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                    return;
                }

                if (await reader.ReadAsync()) {
                    var obj = reader[0];
                    if (obj.GetType() != typeof(DBNull)) {
                        query = string.Empty;
                        try {
                            var id = Convert.ToInt32(obj);

                            query = "UPDATE matches " +
                                "SET played = 1, " +
                                string.Format("red_team = {0}, red_score = {1}, red_penalty = {2}, red_dq = {3}, red_result = '{4}', ",
                                    match.redTeam, match.redScore, match.redPenalty, match.redDq, match.redResult) +
                                string.Format("green_team = {0}, green_score = {1}, green_penalty = {2}, green_dq = {3}, green_result = '{4}', ",
                                    match.greenTeam, match.greenScore, match.greenPenalty, match.greenDq, match.greenResult) + 
                                string.Format("WHERE match_id = {0};", id);
                        } catch (Exception ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                        } finally {
                            reader.Close();
                        }

                        if (query.IsNotEmpty()) {
                            command = new MySqlCommand(query, sql);

                            try {
                                await command.ExecuteNonQueryAsync();
                            } catch (MySqlException ex) {
                                SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                            }
                        }
                    } else {
                        await AddNewMatch(match);
                    }
                } else {
                    await AddNewMatch(match);
                }

                if (!reader.IsClosed) {
                    reader.Close();
                }
            }
        }

        private async Task AddNewMatch(Match match) {
            if (match.tournamentId == 6) {
                var query = "INSERT INTO matches " +
                    "(tournament_id, match_number, played, " +
                    "red_team, red_score, red_penalty, red_dq, red_result, " +
                    "green_team, green_score, green_penalty, green_dq, green_result) " +
                    string.Format("VALUES ({0}, {1}, 1, ", match.tournamentId, match.matchNumber) +
                    string.Format("{0}, {1}, {2}, {3}, '{4}', ", match.redTeam, match.redScore, match.redPenalty, match.redDq, match.redResult) +
                    string.Format("{0}, {1}, {2}, {3}, '{4}', ", match.greenTeam, match.greenScore, match.greenPenalty, match.greenDq, match.greenResult);

                var command = new MySqlCommand(query, sql);

                try {
                    await command.ExecuteNonQueryAsync();
                } catch (MySqlException ex) {
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                }
            } else {
                SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.General, "Can't add matches to this tournament"));
            }
        }

        public async void GetGlobalData() {
            if ((sql != null) && (IsConnected)) {
                try {
                    var query = "SELECT * FROM tournaments " +
                        string.Format("WHERE YEAR(tournament_date)={0};", DateTime.Now.Year);
                    var command = new MySqlCommand(query, sql);
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        try {
                            var id = Convert.ToInt32(reader[0]);
                            var date = (DateTime)reader[1];
                            var name = (string)reader[2];

                            var t = new Tournament(id, date, name);
                            YBotSqlData.Global.tournaments.Add(t);
                        } catch (Exception ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                        }
                    }
                    reader.Close();

                    query = "SELECT * FROM schools;";
                    command = new MySqlCommand(query, sql);
                    reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        try {
                            var id = Convert.ToInt32(reader[0]);
                            var name = (string)reader[1];

                            var s = new School(id, name);
                            YBotSqlData.Global.schools.Add(s);
                        } catch (Exception ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                        }
                    }
                    reader.Close();
                } catch (MySqlException ex) {
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                }
            }
        }

        public async Task<Match> GetMatch(int tournamentId, int matchNumber) {
            var match = new Match();

            if ((sql != null) && (IsConnected)) {
                try {
                    var query = "SELECT * FROM matches " +
                        string.Format("WHERE tournament_id={0} ", tournamentId) +
                        string.Format("AND match_number={0};", matchNumber);
                    var command = new MySqlCommand(query, sql);
                    var reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        try {
                            match.matchNumber = Convert.ToInt32(reader[2]);
                            match.greenTeam = Convert.ToInt32(reader[9]);
                            try {
                                match.greenScore = Convert.ToInt32 (reader[10]);
                            } catch {
                                match.greenScore = 0;
                            }
                            match.redTeam = Convert.ToInt32(reader[4]);
                            try {
                                match.redScore = Convert.ToInt32(reader[5]);
                            } catch {
                                match.redScore = 0;
                            }
                        } catch (Exception ex) {
                            SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                        }
                    }
                    reader.Close();
                } catch (MySqlException ex) {
                    SqlMessageEvent?.Invoke(this, new SqlMessageArgs(SqlMessageType.Exception, ex.ToString()));
                }
            }

            return match;
        }
    }

    public enum SqlMessageType
    {
        Exception,
        General
    }

    public class SqlMessageArgs : EventArgs
    {
        public string message;
        public SqlMessageType type;

        public SqlMessageArgs(string message) {
            type = SqlMessageType.General;
            this.message = message;
        }

        public SqlMessageArgs(SqlMessageType type, string message) {
            this.type = type;
            this.message = message;
        }
    }
}
