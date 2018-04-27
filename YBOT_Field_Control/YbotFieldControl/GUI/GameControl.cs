using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Threading;
using YBotSqlWrapper;
using HelpfulUtilites;

namespace YbotFieldControl
{
    public partial class GameControl : Form
    {
		public GameControl() : this (new FieldControl ()) { }

        public GameControl(FieldControl fc)
        {
            InitializeComponent();
            this.fc = fc;

			if (YbotSql.Instance.IsConnected) {
                InitializeSqlData ();
            } else {
                YbotSql.Instance.SqlConnectedEvent += OnSqlConnect;
            }
        }

        private void OnSqlConnect (object sender) {
            InitializeSqlData ();
            YbotSql.Instance.SqlConnectedEvent -= OnSqlConnect;
        }

        private void InitializeSqlData () {
            if (btnTournamentNext.InvokeRequired) {
                btnTournamentNext.Invoke ((MethodInvoker)delegate () {
                    btnTournamentNext.Visible = true;
                });
            } else {
                btnTournamentNext.Visible = true;
            }

            if (btnTournamentPrev.InvokeRequired) {
                btnTournamentPrev.Invoke ((MethodInvoker)delegate () {
                    btnTournamentPrev.Visible = true;
                });
            } else {
                btnTournamentPrev.Visible = true;
            }

            foreach (var t in YBotSqlData.Global.tournaments) {
                if (t.date.Date.CompareTo (DateTime.Now.Date) == 0) {
                    YBotSqlData.Global.currentTournament = t.name;
                }
            }

            YBotSqlData.Global.currentMatchNumber = 0;
            if (YBotSqlData.Global.currentTournament.IsEmpty ()) {
                YBotSqlData.Global.currentTournament = "Field Testing";
            }

            if (lblTournamentName.InvokeRequired) {
                lblTournamentName.Invoke ((MethodInvoker)delegate () {
                    lblTournamentName.Visible = true;
                    lblTournamentName.Text = YBotSqlData.Global.currentTournament;
                });
            } else {
                lblTournamentName.Visible = true;
                lblTournamentName.Text = YBotSqlData.Global.currentTournament;
            }
        }

        #region Game Controls

        #region Penalties

        #region Green Penalties
        private void lblGreenPenalty1_Click(object sender, EventArgs e)
        {
            if (lblGreenPenalty1.BackColor == GameControl.DefaultBackColor)
            {
                lblGreenPenalty1.BackColor = Color.Lime;
                lblGreenPenalty1.ForeColor = Color.Black;
                GD.lblGreenPenalty1.BackColor = Color.Lime;
                GD.lblGreenPenalty1.ForeColor = Color.Black;
                this.green.penalty++;
                this.GameLog("Green Penalty: ADD");
            }
            else
            {
                lblGreenPenalty1.BackColor = GameControl.DefaultBackColor;
                lblGreenPenalty1.ForeColor = Color.Lime;
                GD.lblGreenPenalty1.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenPenalty1.ForeColor = GameControl.DefaultBackColor;
                this.green.penalty--;
                this.GameLog("Green Penalty: Subtract");
            }
        }

        private void lblGreenPenalty2_Click(object sender, EventArgs e)
        {
            if (lblGreenPenalty2.BackColor == GameControl.DefaultBackColor)
            {
                lblGreenPenalty2.BackColor = Color.Lime;
                lblGreenPenalty2.ForeColor = Color.Black;
                GD.lblGreenPenalty2.BackColor = Color.Lime;
                GD.lblGreenPenalty2.ForeColor = Color.Black;
                this.green.penalty++;
                this.GameLog("Green Penalty: ADD");
            }
            else
            {
                lblGreenPenalty2.BackColor = GameControl.DefaultBackColor;
                lblGreenPenalty2.ForeColor = Color.Lime;
                GD.lblGreenPenalty2.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenPenalty2.ForeColor = GameControl.DefaultBackColor;
                this.green.penalty--;
                this.GameLog("Green Penalty: Subtract");
            }
        }

        private void lblGreenPenalty3_Click(object sender, EventArgs e)
        {
            if (lblGreenPenalty3.BackColor == GameControl.DefaultBackColor)
            {
                lblGreenPenalty3.BackColor = Color.Lime;
                lblGreenPenalty3.ForeColor = Color.Black;
                GD.lblGreenPenalty3.BackColor = Color.Lime;
                GD.lblGreenPenalty3.ForeColor = Color.Black;
                this.green.penalty++;
                this.GameLog("Green Penalty: ADD");
            }
            else
            {
                lblGreenPenalty3.BackColor = GameControl.DefaultBackColor;
                lblGreenPenalty3.ForeColor = Color.Lime;
                GD.lblGreenPenalty3.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenPenalty3.ForeColor = GameControl.DefaultBackColor;
                this.green.penalty--;
                this.GameLog("Green Penalty: Subtract");
            }
        }

        private void lblGreenDQ_Click(object sender, EventArgs e)
        {
            if (lblGreenDQ.BackColor == GameControl.DefaultBackColor)
            {
                lblGreenDQ.BackColor = Color.Lime;
                lblGreenDQ.ForeColor = Color.Black;
                GD.lblGreenDQ.BackColor = Color.Lime;
                GD.lblGreenDQ.ForeColor = Color.Black;
                this.green.dq = true;
                this.GameLog("Green DQ: True");
            }
            else
            {
                lblGreenDQ.BackColor = GameControl.DefaultBackColor;
                lblGreenDQ.ForeColor = Color.Lime;
                GD.lblGreenDQ.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenDQ.ForeColor = GameControl.DefaultBackColor;
                this.green.dq = false;
                this.GameLog("Green DQ: False");
            }
        }

        private void btnDisableGreen_Click(object sender, EventArgs e)
        {
            if (btnDisableGreen.BackColor == GameControl.DefaultBackColor)
            {

                btnDisableGreen.BackColor = Color.Lime;
                btnDisableGreen.ForeColor = Color.Black;
                GD.lblGreenScore.BackColor = Color.Lime;
                GD.lblGreenScore.ForeColor = Color.Black;
                this.fc.RobotTransmitters("green", State.off, State.off);
                this.GameLog("Green Disabled = True");
            }
            else
            {
                btnDisableGreen.BackColor = GameControl.DefaultBackColor;
                btnDisableGreen.ForeColor = Color.Lime;
                GD.lblGreenScore.BackColor = GameControl.DefaultBackColor;
                GD.lblGreenScore.ForeColor = Color.Lime;
                if (gameMode == GameModes.manual)
                {
                    fc.RobotTransmitters("green", State.off, State.on);
                }
                else
                {
                    fc.RobotTransmitters("green", State.on, State.on);
                }
                this.GameLog("Green Disabled = False");
            }
        }
        #endregion

        #region Red Penalties
        private void lblRedPenalty1_Click(object sender, EventArgs e)
        {
            if (lblRedPenalty1.BackColor == GameControl.DefaultBackColor)
            {
                lblRedPenalty1.BackColor = Color.Red;
                lblRedPenalty1.ForeColor = Color.Black;
                GD.lblRedPenalty1.BackColor = Color.Red;
                GD.lblRedPenalty1.ForeColor = Color.Black;
                this.red.penalty++;
                this.GameLog("Red Penalty: ADD");
            }
            else
            {
                lblRedPenalty1.BackColor = GameControl.DefaultBackColor;
                lblRedPenalty1.ForeColor = Color.Red;
                GD.lblRedPenalty1.BackColor = GameControl.DefaultBackColor;
                GD.lblRedPenalty1.ForeColor = GameControl.DefaultBackColor;
                this.red.penalty--;
                this.GameLog("Red Penalty: Subtract");
            }
        }

        private void lblRedPenalty2_Click(object sender, EventArgs e)
        {
            if (lblRedPenalty2.BackColor == GameControl.DefaultBackColor)
            {
                lblRedPenalty2.BackColor = Color.Red;
                lblRedPenalty2.ForeColor = Color.Black;
                GD.lblRedPenalty2.BackColor = Color.Red;
                GD.lblRedPenalty2.ForeColor = Color.Black;
                this.red.penalty++;
                this.GameLog("Red Penalty: ADD");
            }
            else
            {
                lblRedPenalty2.BackColor = GameControl.DefaultBackColor;
                lblRedPenalty2.ForeColor = Color.Red;
                GD.lblRedPenalty2.BackColor = GameControl.DefaultBackColor;
                GD.lblRedPenalty2.ForeColor = GameControl.DefaultBackColor;
                this.red.penalty--;
                this.GameLog("Red Penalty: Subtract");
            }
        }

        private void lblRedPenalty3_Click(object sender, EventArgs e)
        {
            if (lblRedPenalty3.BackColor == GameControl.DefaultBackColor)
            {
                lblRedPenalty3.BackColor = Color.Red;
                lblRedPenalty3.ForeColor = Color.Black;
                GD.lblRedPenalty3.BackColor = Color.Red;
                GD.lblRedPenalty3.ForeColor = Color.Black;
                this.red.penalty++;
                this.GameLog("Red Penalty: ADD");
            }
            else
            {
                lblRedPenalty3.BackColor = GameControl.DefaultBackColor;
                lblRedPenalty3.ForeColor = Color.Red;
                GD.lblRedPenalty3.BackColor = GameControl.DefaultBackColor;
                GD.lblRedPenalty3.ForeColor = GameControl.DefaultBackColor;
                this.red.penalty--;
                this.GameLog("Red Penalty: Subtract");
            }
        }

        private void lblRedDQ_Click(object sender, EventArgs e)
        {
            if (lblRedDQ.BackColor == GameControl.DefaultBackColor)
            {
                lblRedDQ.BackColor = Color.Red;
                lblRedDQ.ForeColor = Color.Black;
                GD.lblRedDQ.BackColor = Color.Red;
                GD.lblRedDQ.ForeColor = Color.Black;
                this.red.dq = true;
                this.GameLog("Red DQ: True");
            }
            else
            {
                lblRedDQ.BackColor = GameControl.DefaultBackColor;
                lblRedDQ.ForeColor = Color.Red;
                GD.lblRedDQ.BackColor = GameControl.DefaultBackColor;
                GD.lblRedDQ.ForeColor = GameControl.DefaultBackColor;
                this.red.dq = false;
                this.GameLog("Red DQ: False");
            }
        }

        private void btnDisableRed_Click(object sender, EventArgs e)
        {
            if (btnDisableRed.BackColor == GameControl.DefaultBackColor)
            {

                btnDisableRed.BackColor = Color.Red;
                btnDisableRed.ForeColor = Color.Black;
                GD.lblRedScore.BackColor = Color.Red;
                GD.lblRedScore.ForeColor = Color.Black;
                this.fc.RobotTransmitters("red", State.off, State.off);
                this.GameLog("Red Disabled = True");
            }
            else
            {
                btnDisableRed.BackColor = GameControl.DefaultBackColor;
                btnDisableRed.ForeColor = Color.Red;
                GD.lblRedScore.BackColor = GameControl.DefaultBackColor;
                GD.lblRedScore.ForeColor = Color.Red;
                if (gameMode == GameModes.manual)
                {
                    fc.RobotTransmitters("red", State.off, State.on);
                } 
                else
                {
                    fc.RobotTransmitters("red", State.on, State.on);
                }
                GameLog("Red Disabled = False");
            }
        }

        #endregion

        #endregion

        #region Game Controls

        private void btnStartGame_Click(object sender, EventArgs e) {
            ClearDisplay();
            DisableGameButtons();

            lblGameClock.ForeColor = Color.Blue;
            GD.lblGameClock.ForeColor = Color.Blue;

            btnSetupGame.Visible = true;
            btnStartGame.Visible = false;

            GameStartUp();
            Thread.Sleep (200);

            manAutoTime = 0;
            autoModeTime = 30;
            midModeTime = 90;
            time.CountDownStart(2, 30);
            //autoModeTime = 30;
            //midModeTime = 35;
            //time.CountDownStart(1, 15);
            time.timesUp = false;
            gameTimer.Start();

            MainGame ();
        }

        private void btnStop_Click (object sender, EventArgs e) {
            GameShutDown ();
            EnableGameButtons ();

            gameTimer.Stop ();
            practiceTimer.Stop ();
            testTimer.Stop ();

            btnStartGame.Visible = false;
            btnSetupGame.Visible = true;

            GameLog ("Field Off");
            LogGame ();
        }

        private void btnAutoMode_Click(object sender, EventArgs e) {
            DisableGameButtons();
            ClearDisplay();

            GameStartUp(GameModes.autonomous);
            Thread.Sleep(200);
            GameLog ("Auto Mode Started");

            time.elapsedTime.Restart();
            time.timesUp = false;
            practiceTimer.Start();

            MainGame();
        }

        private void btnManualMode_Click(object sender, EventArgs e) {
            DisableGameButtons();
            ClearDisplay();

            GameStartUp (GameModes.manual);
            speedMode = false;
            Thread.Sleep(200);
            GameLog("Manual Mode Started");

            time.elapsedTime.Restart();
            time.timesUp = false;
            practiceTimer.Start();

            MainGame();
        }

        private void btnSpeedMode_Click (object sender, EventArgs e) {
            DisableGameButtons ();
            ClearDisplay ();

            GameStartUp (GameModes.manual);
            speedMode = true;
            Thread.Sleep (200);
            GameLog ("Speed Mode Started");

            time.elapsedTime.Restart ();
            time.timesUp = false;
            practiceTimer.Start ();

            MainGame ();
        }

        private void btnPracticeMode_Click (object sender, EventArgs e) {
            DisableGameButtons ();
            ClearDisplay ();

            GameStartUp (GameModes.debug);
            Thread.Sleep (200);
            GameLog ("Practice Mode Started");

            time.elapsedTime.Restart ();
            time.timesUp = false;
            practiceTimer.Start ();

            MainGame ();
        }

        private void btnTestMode_Click(object sender, EventArgs e) {
            if (btnTestMode.BackColor == DefaultBackColor)
            {
                DisableGameButtons();
                btnStop.BackColor = DefaultBackColor;
                btnTestMode.BackColor = Color.LimeGreen;
                testTimer.Start();
                TestMode();
            } else {
                btnStop.PerformClick();
            }
        }

        private void TestMode() {
            autoModeTime = 5;
            manAutoTime =  0;
            midModeTime = 10;

            btnMatchNext.PerformClick();

            ClearDisplay();
            GameStartUp();
            gameTimer.Start();
            time.CountDownStart(0, 20);
            time.timesUp = false;
            MainGame();
        }

        private void btnMatchNext_Click(object sender, EventArgs e) {
            if (gameMode == GameModes.off) {
                matchNumber++;
                var displayedMatchNumber = matchNumber;
                if (IsChampionshipBracketMatch ()) {
                    displayedMatchNumber -= 100;
                }
                lblMatchNumber.Text = "Match " + displayedMatchNumber.ToString();
                GD.lblMatchNumber.Text = "Match " + displayedMatchNumber.ToString();
                ClearDisplay();
                GetTeamNames();
                YBotSqlData.Global.currentMatchNumber = matchNumber;
            }
        }

        private void btnMatchPrev_Click(object sender, EventArgs e) {
            if (gameMode == GameModes.off) {
                if (matchNumber > 0) matchNumber--;
                var displayedMatchNumber = matchNumber;
                if (IsChampionshipBracketMatch ()) {
                    displayedMatchNumber -= 100;
                }
                lblMatchNumber.Text = "Match " + displayedMatchNumber.ToString();
                GD.lblMatchNumber.Text = "Match " + displayedMatchNumber.ToString();
                ClearDisplay();
                GetTeamNames();
                YBotSqlData.Global.currentMatchNumber = matchNumber;
            }
        }

        private void lblMatchNumber_Click(object sender, EventArgs e) {
            GetTeamNames();
        }

        private void btnGameDisplay_Click(object sender, EventArgs e)
        {
            if (Screen.AllScreens.Length > 1)//Check for Multiple Displays
            {

                // Important !
                GD.StartPosition = FormStartPosition.Manual;

                // Get the second monitor screen
                Screen screen = GetSecondaryScreen();

                // set the location to the top left of the second screen
                GD.Location = screen.WorkingArea.Location;

                // set it fullscreen
                GD.Size = new Size(screen.WorkingArea.Width, screen.WorkingArea.Height);

                // Show the form
                GD.Show();//Shows Display on Second Display

            }
            else GD.Show(); //Shows Display on the only display
            btnGameDisplay.Visible = false; //Disables the button
        }

        private void btnScoreGame_Click(object sender, EventArgs e)
        {
            Score score = new Score(this);
            score.Show();

            while (!score.done)
            {
                Application.DoEvents();
            }

            var accept = score.acceptScores;
            score.Close();

            if (accept) {
                ScoreGame();
                RecordGame();

                btnStop.BackColor = Color.Red;
                btnStartGame.BackColor = DefaultBackColor;
                btnPracticeMode.BackColor = DefaultBackColor;
                gameMode = fc.ChangeGameMode(GameModes.off);

                lblGreenScore.Text = green.finalScore.ToString();
                lblRedScore.Text = red.finalScore.ToString();
                GD.lblGreenScore.Text = green.finalScore.ToString();
                GD.lblRedScore.Text = red.finalScore.ToString();

                btnStop.PerformClick();
            }
        }

        private void lblGreenScore_Click(object sender, EventArgs e) {
            ClearDisplay();
        }

        private void lblRedScore_Click(object sender, EventArgs e) {
            ClearDisplay();
        }

        private void lblGameClock_Click(object sender, EventArgs e) {
            ClearDisplay();
        }

        private void UpdateGame () {
            MainGame ();
            UpdateDisplays ();
        }

        private void ScoreGame()
        {
            //Add convert additional points to intergers here

            if (red.dq || red.penalty == 3)
            {
                red.finalScore = 0;
                red.matchResult = "L";
            }
            if (green.dq || green.penalty == 3)
            {
                green.finalScore = 0;
                green.matchResult = "L";
            }

            if (green.finalScore > red.finalScore)
            {
                green.matchResult = "W";
                red.matchResult = "L";
            }
            else if (red.finalScore > green.finalScore)
            {
                red.matchResult = "W";
                green.matchResult = "L";
            }
            else if (green.finalScore == red.finalScore && !green.dq && !red.dq)
            {
                red.matchResult = "T";
                green.matchResult = "T";
            }
        }

        private void RecordGame() {
            string file = @"\Match " + matchNumber.ToString() + " - Score";
            string file2 = @"\Match Scores";
            string folder = @"Matches\" + "Match " + matchNumber.ToString();
            string folder2 = @"Matches\";

            string field = ("Match Number" + "\t" + "Team Name" + "\t" + "Final Score" + "\t" + "Penalties" + "\t" + "DQ" + "\t" + "Result");
            string field2 = ("Auto Switch Off" + "\t" + "Auto Right" + "\t" + "Auto Left" + "\t" + "Auto Score" 
                + "\t" + "Depleted Stored" + "\t" + "Loaded Fresh" + "\t" + "Rod Zone" + "\t" + "Reactor Score"
                + "\t" + "Speed Towers" + "\t" + "Manual Switch On" + "\t" + "Speed Score" + "\t" + "Manual Score");

            string greenTeam = (matchNumber.ToString() + "\t" + lblGreenTeam.Text + "\t" + green.finalScore.ToString()
                              + "\t" + green.penalty.ToString() + "\t" + green.dq.ToString() + "\t" + green.matchResult);
            string greenTeam2 = string.Format ("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}",
                green.autoSwitchTurnedOff,
                green.autoRightTowerPressed,
                green.autoLeftTowerPressed,
                green.autoScore,
                green.rodsRemoved,
                green.rodsReplaced,
                green.reactorMultiplier,
                green.reactorScore,
                green.speedTowers,
                green.manualSwitchTurnedOn,
                green.speedScore,
                green.manScore);

			string redTeam = (matchNumber.ToString() + "\t" + lblGreenTeam.Text + "\t" + green.finalScore.ToString()
                              + "\t" + green.penalty.ToString() + "\t" + green.dq.ToString() + "\t" + green.matchResult);
            string redTeam2 = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}",
                red.autoSwitchTurnedOff,
                red.autoRightTowerPressed,
                red.autoLeftTowerPressed,
                red.autoScore,
                red.rodsRemoved,
                red.rodsReplaced,
                red.reactorMultiplier,
                red.reactorScore,
                red.speedTowers,
                red.manualSwitchTurnedOn,
                red.speedScore,
                red.manScore);

            string text = "\r\n" + field + "\t" + field2 + "\r\n" + greenTeam + "\t" + greenTeam2 + "\r\n" + redTeam + "\t" + redTeam2;

            try {
				lw.WriteLog(text, file, folder);
				lw.WriteLog(text, file2, folder2);
			} catch {
				MessageBox.Show("Game score was not recorded to file");
			} finally {
				if (YbotSql.Instance.IsConnected) {
					var schools = YBotSqlData.Global.schools;

					var match = new Match();
					match.tournamentId = YBotSqlData.Global.tournaments[lblTournamentName.Text].id;
					match.matchNumber = matchNumber;
					green.StoreJointVariablesToSqlMatch(ref match);

					green.StoreTeamVariablesToSqlMatch(ref match, greenTeam2, field2);
					match.greenTeam = FindSchoolId(schools, lblGreenTeam.Text, "Green Team");

					red.StoreTeamVariablesToSqlMatch(ref match, redTeam2, field2);
					match.redTeam = FindSchoolId(schools, lblRedTeam.Text, "Red Team");

					YbotSql.Instance.AddMatch(match);
				}
			}
        }

        protected int FindSchoolId(Schools schools, string schoolName, string team) {
            int id = schools["TOBy"].id;

            if (schoolName != team) {
                var school = schools[schoolName];
                if (school != null) {
                    id = school.id;
                } else {
                    var schoolNames = new List<string> ();
                    schools.ForEach (s => schoolNames.Add (s.name));
                    var teamNameFound = false;

                    foreach (var s in schoolNames) {
                        if (s.StartsWith(schoolName)) {
                            var result = MessageBox.Show (
                                string.Format ("Is {0} the same as {1}", schoolName, s), 
                                "Matching " + team, 
                                MessageBoxButtons.YesNoCancel);

                            if (result == DialogResult.Yes) {
                                teamNameFound = true;
                                id = schools[s].id;
                            } else if (result == DialogResult.Cancel) {
                                teamNameFound = true;
                            }
                        }
                    }
                    
                    while (!teamNameFound && (schoolNames.Count > 0)) {
                        var closestString = lblRedTeam.Text.ClosestMatchingString (schoolNames);
                        var result = MessageBox.Show (
                            string.Format ("Is {0} the same as {1}", schoolName, closestString),
                            "Matching " + team, 
                            MessageBoxButtons.YesNoCancel);

                        if (result == DialogResult.Yes) {
                            teamNameFound = true;
                            id = schools[closestString].id;
                        } else if (result == DialogResult.Cancel) {
                            teamNameFound = true;
                        } else {
                            schoolNames.Remove (closestString);
                        }
                    }
                }
            }

            return id;
        }

		private void GetTeamNames()
        {
			if (YbotSql.Instance.IsConnected) {
				int matchId = matchNumber;
				var match = YbotSql.Instance.GetMatch (YBotSqlData.Global.tournaments[YBotSqlData.Global.currentTournament].id, matchId);

                var greenTeam = YBotSqlData.Global.schools[match.greenTeam];
                var redTeam = YBotSqlData.Global.schools[match.redTeam];

				if (greenTeam != null) {
					lblGreenTeam.Text = greenTeam.name;
					GD.lblGreenTeam.Text = greenTeam.name;
				} else {
					lblGreenTeam.Text = "Green Team";
					GD.lblGreenTeam.Text = "Green Team";
				}

				if (redTeam != null) {
					lblRedTeam.Text = redTeam.name;
					GD.lblRedTeam.Text = redTeam.name;
				} else {
					lblRedTeam.Text = "Red Team";
					GD.lblRedTeam.Text = "Red Team";
				}

                var greenScore = match.greenScore;
                var redScore = match.redScore;

                if (greenScore != 0) {
                    lblGreenScore.Text = greenScore.ToString();
                } else {
                    lblGreenScore.Text = "000";
                }

                if (redScore != 0) {
                    lblRedScore.Text = redScore.ToString();
                } else {
                    lblRedScore.Text = "000";
                }
			} else {
				string greenTeam = null;
				string redTeam = null;
				string content = null;
				string path = filePath;
				string file = this.fs.setupFilePath + @"\Teams.txt";

				try {
					if (!Directory.Exists(path)) {
						Directory.CreateDirectory(path);
					}
				} catch {
					path = null;
				}

				try {
					if (File.Exists(file)) {
						//StreamReader sr = new StreamReader(file, System.Text.Encoding.Default);
						Stream stream = File.Open(file, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
						StreamReader sr = new StreamReader(stream);

						for (int i = 0; i < matchNumber; i++) {
							content = sr.ReadLine();
							string[] teams = content.Split(new string[] { "\t" }, StringSplitOptions.None);//Delimited the Tab keycode
							greenTeam = teams[0];
							redTeam = teams[1];
						}
						sr.Close();
						sr.Dispose();
					} else {
						greenTeam = "Green Team";
						redTeam = "Red Team";
					}
				} catch {
					greenTeam = "Green Team";
					redTeam = "Red Team";
				}
				if (greenTeam == null) greenTeam = "Green Team";
				if (redTeam == null) redTeam = "Red Team";


				lblGreenTeam.Text = greenTeam;
				lblRedTeam.Text = redTeam;
				GD.lblGreenTeam.Text = greenTeam;
				GD.lblRedTeam.Text = redTeam;
			}
        }

        // Only works for 2018 game
        private void btnSeedBracket_Click (object sender, EventArgs e) {
            if (generatedSeedBracketMatches) {
                var dr = MessageBox.Show (
                    "Are you sure you want to regenerate the seeded bracket matches", 
                    "Seeded bracket matches are already generated", 
                    MessageBoxButtons.YesNo);
                if (dr == DialogResult.No) {
                    return;
                }
            }

            generatedSeedBracketMatches = true;
            var seeding = new Dictionary<int, SchoolStandings> ();
            for (int i = 1; i <= 18; ++i) {
                var match = YbotSql.Instance.GetMatch (14, i);
                if (match != null) {
                    // Red Team
                    if (!seeding.ContainsKey (match.redTeam)) {
                        seeding.Add (match.redTeam, new SchoolStandings ());

                        seeding[match.redTeam].name = YBotSqlData.Global.schools[match.redTeam].name;
                        seeding[match.redTeam].id = match.redTeam;
                    }

                    switch (match.redResult) {
                    case "W":
                        seeding[match.redTeam].wins++;
                        break;
                    case "L":
                        seeding[match.redTeam].loses++;
                        break;
                    case "T":
                        seeding[match.redTeam].ties++;
                        break;
                    default:
                        break;
                    }

                    seeding[match.redTeam].average += match.redScore;
                    seeding[match.redTeam].matchesPlayed++;

                    if (seeding[match.redTeam].highest < match.redScore) {
                        seeding[match.redTeam].highest = match.redScore;
                    }

                    // Green Team
                    if (!seeding.ContainsKey (match.greenTeam)) {
                        seeding.Add (match.greenTeam, new SchoolStandings ());
                        seeding[match.greenTeam].name = YBotSqlData.Global.schools[match.greenTeam].name;
                        seeding[match.greenTeam].id = match.greenTeam;
                    }

                    switch (match.greenResult) {
                    case "W":
                        seeding[match.greenTeam].wins++;
                        break;
                    case "L":
                        seeding[match.greenTeam].loses++;
                        break;
                    case "T":
                        seeding[match.greenTeam].ties++;
                        break;
                    default:
                        break;
                    }

                    seeding[match.greenTeam].average += match.greenScore;
                    seeding[match.greenTeam].matchesPlayed++;

                    if (seeding[match.greenTeam].highest < match.greenScore) {
                        seeding[match.greenTeam].highest = match.greenScore;
                    }
                }
            }

            foreach (var seed in seeding.Values) {
                seed.average = seed.average / seed.matchesPlayed;
            }

            var seeds = new List<SchoolStandings> ();
            // Top 4 teams
            seeds.AddRange (from row in seeding.Values
                            orderby row.wins descending, row.ties descending, row.loses, row.average descending, row.highest descending
                            where row.id == 1 || row.id == 4 || row.id == 10 || row.id == 13
                            select row);

            // Remaining 3 teams
            seeds.AddRange (from row in seeding.Values
                            orderby row.wins descending, row.ties descending, row.loses, row.average descending, row.highest descending
                            where row.id == 3 || row.id == 7 || row.id == 8
                            select row);

            var bracketMatch = new Match ();
            bracketMatch.tournamentId = 14;
            bracketMatch.matchNumber = 101;
            // Seed 4
            bracketMatch.greenTeam = seeds[3].id;
            // Seed 5
            bracketMatch.redTeam = seeds[4].id;
            YbotSql.Instance.AddNewBracketMatch (bracketMatch);

            bracketMatch.matchNumber = 102;
            // Seed 2
            bracketMatch.greenTeam = seeds[1].id;
            // Seed 7
            bracketMatch.redTeam = seeds[6].id;
            YbotSql.Instance.AddNewBracketMatch (bracketMatch);

            bracketMatch.matchNumber = 103;
            // Seed 3
            bracketMatch.greenTeam = seeds[2].id;
            // Seed 6
            bracketMatch.redTeam = seeds[5].id;
            YbotSql.Instance.AddNewBracketMatch (bracketMatch);

            bracketMatch.matchNumber = 104;
            // Seed 1
            bracketMatch.greenTeam = seeds[0].id;
            // Waiting
            bracketMatch.redTeam = 16;
            YbotSql.Instance.AddNewBracketMatch (bracketMatch);
        }

        private void DisableGameButtons () {
            btnStartGame.Enabled = false;
            btnSetupGame.Enabled = false;
            btnAutoMode.Enabled = false;
            btnManualMode.Enabled = false;
            btnSpeedMode.Enabled = false;
            btnPracticeMode.Enabled = false;
            btnTestMode.Enabled = false;

            btnStop.BackColor = DefaultBackColor;
            btnStartGame.BackColor = Color.LimeGreen;
        }

        private void EnableGameButtons () {
            btnStartGame.Enabled = true;
            btnSetupGame.Enabled = true;
            btnAutoMode.Enabled = true;
            btnManualMode.Enabled = true;
            btnSpeedMode.Enabled = true;
            btnPracticeMode.Enabled = true;
            btnTestMode.Enabled = true;

            btnStop.BackColor = Color.Red;
            btnStartGame.BackColor = DefaultBackColor;
            btnAutoMode.BackColor = DefaultBackColor;
            btnManualMode.BackColor = DefaultBackColor;
            btnPracticeMode.BackColor = DefaultBackColor;
            btnTestMode.BackColor = DefaultBackColor;
        }

		private void btnTournamentNext_Click(object sender, EventArgs e) {
			var tournaments = YBotSqlData.Global.tournaments;
			var index = tournaments.IndexOf(tournaments[lblTournamentName.Text]);

            if (IsChampionshipMatch ()) {
                DisableChampionshipButtons ();
            }

            index = ++index % tournaments.Count;

            lblTournamentName.Text = tournaments[index].name;
			YBotSqlData.Global.currentTournament = lblTournamentName.Text;

            if (IsChampionshipMatch ()) {
                EnableChampionshipButtons ();
            }
        }

        private void btnTournamentPrev_Click (object sender, EventArgs e) {
            var tournaments = YBotSqlData.Global.tournaments;
            var index = tournaments.IndexOf (tournaments[lblTournamentName.Text]);

            if (IsChampionshipMatch ()) {
                DisableChampionshipButtons ();
            }

            index = --index;
            if (index < 0) {
                index = tournaments.Count - 1;
            }

            lblTournamentName.Text = tournaments[index].name;
            YBotSqlData.Global.currentTournament = lblTournamentName.Text;

            if (IsChampionshipMatch ()) {
                EnableChampionshipButtons ();
            }
        }

		private void EnableChampionshipButtons() {
            lblChampionshipRounds.Visible = true;
            btnChampionshipRoundNext.Visible = true;
            btnChampionshipRoundPrevious.Visible = true;
        }

		private void DisableChampionshipButtons() {
            lblChampionshipRounds.Visible = false;
            btnChampionshipRoundNext.Visible = false;
            btnChampionshipRoundPrevious.Visible = false;
        }

        private void btnChampionshipRound_Click (object sender, EventArgs e) {
            if (lblChampionshipRounds.Text == "Round Robin") {
                lblChampionshipRounds.Text = "Bracket";
                matchNumber += 100;
                btnSeedBracket.Visible = true;
            } else {
                lblChampionshipRounds.Text = "Round Robin";
                matchNumber -= 100;
                btnSeedBracket.Visible = true;
            }
			GetTeamNames();
        }

		public bool IsChampionshipBracketMatch() {
			return (lblTournamentName.Text == "Championship") && (lblChampionshipRounds.Text == "Bracket");
		}

		public bool IsChampionshipMatch() {
			return lblTournamentName.Text == "Championship";
		}

        #endregion

        #region Display

        private void UpdateDisplays () {
            lblGameClock.Text = time.CountDownStatus ();
            GD.lblGameClock.Text = lblGameClock.Text.ToString ();

            lblGreenScore.Text = green.score.ToString();
            lblRedScore.Text = red.score.ToString();
            GD.lblGreenScore.Text = green.score.ToString();
            GD.lblRedScore.Text = red.score.ToString();
        }

        public Screen GetSecondaryScreen()
        {
            if (Screen.AllScreens.Length == 1)
            {
                return null;
            }

            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Primary == false)
                {
                    return screen;
                }
            }

            return null;
        }

        private void ClearDisplay()
        {
            if (gameMode == GameModes.off) {
                lblGameClock.Text = "2:30";
                GD.lblGameClock.Text = "2:30";

                lblGameClock.ForeColor = Color.Black;
                GD.lblGameClock.ForeColor = Color.Black;

                lblRedScore.Text = "000";
                GD.lblRedScore.Text = "000";
                lblGreenScore.Text = "000";
                GD.lblGreenScore.Text = "000";

                lblGreenPenalty1.BackColor = DefaultBackColor;
                lblGreenPenalty1.ForeColor = Color.Lime;
                GD.lblGreenPenalty1.BackColor = DefaultBackColor;
                GD.lblGreenPenalty1.ForeColor = DefaultBackColor;

                lblGreenPenalty2.BackColor = DefaultBackColor;
                lblGreenPenalty2.ForeColor = Color.Lime;
                GD.lblGreenPenalty2.BackColor = DefaultBackColor;
                GD.lblGreenPenalty2.ForeColor = DefaultBackColor;

                lblGreenPenalty3.BackColor = DefaultBackColor;
                lblGreenPenalty3.ForeColor = Color.Lime;
                GD.lblGreenPenalty3.BackColor = DefaultBackColor;
                GD.lblGreenPenalty3.ForeColor = DefaultBackColor;

                lblGreenDQ.BackColor = DefaultBackColor;
                lblGreenDQ.ForeColor = Color.Lime;
                GD.lblGreenDQ.BackColor = DefaultBackColor;
                GD.lblGreenDQ.ForeColor = DefaultBackColor;

                lblRedPenalty1.BackColor = DefaultBackColor;
                lblRedPenalty1.ForeColor = Color.Red;
                GD.lblRedPenalty1.BackColor = DefaultBackColor;
                GD.lblRedPenalty1.ForeColor = DefaultBackColor;

                lblRedPenalty2.BackColor = DefaultBackColor;
                lblRedPenalty2.ForeColor = Color.Red;
                GD.lblRedPenalty2.BackColor = DefaultBackColor;
                GD.lblRedPenalty2.ForeColor = DefaultBackColor;

                lblRedPenalty3.BackColor = DefaultBackColor;
                lblRedPenalty3.ForeColor = Color.Red;
                GD.lblRedPenalty3.BackColor = DefaultBackColor;
                GD.lblRedPenalty3.ForeColor = DefaultBackColor;

                lblRedDQ.BackColor = DefaultBackColor;
                lblRedDQ.ForeColor = Color.Red;
                GD.lblRedDQ.BackColor = DefaultBackColor;
                GD.lblRedDQ.ForeColor = DefaultBackColor;

            }
        }

        private void GameControl_FormClosed (object sender, FormClosedEventArgs e) 
        {
            btnStop.PerformClick ();
            fc.ChangeGameMode (GameModes.off);
            gameMode = GameModes.off;
            YBotSqlData.Global.currentMatchNumber = -1;
            YBotSqlData.Global.currentTournament = string.Empty;
        }

        #endregion

        #region Timers
        private void gameTimer_Tick(object sender, EventArgs e) {
            TimeKeeper();
            UpdateGame();
        }

        private void practiceTimer_Tick(object sender, EventArgs e) {
            UpdateGame();
        }

        private void TimeKeeper () {
            // Game has finished
            if (time.timesUp) {
                GameShutDown ();
                gameMode = fc.ChangeGameMode (GameModes.end);

                EnableGameButtons ();
                lblGameClock.Text = "0:00";
                GD.lblGameClock.Text = "0:00";
                lblGameClock.ForeColor = Color.Black;
                GD.lblGameClock.ForeColor = Color.Black;

                GameLog ("Game Stopped");
                gameTimer.Stop ();

                LogGame ();
            }
            // In auto mode and not passed auto time
            else if (gameMode == GameModes.autonomous && !time.CheckTimeElapsed (autoModeTime)) {

            }
            // In auto mode and passed auto time
            else if (gameMode == GameModes.autonomous && time.CheckTimeElapsed (autoModeTime)) {
                gameMode = fc.ChangeGameMode (GameModes.manual);
                lblGameClock.ForeColor = Color.Black;
                GD.lblGameClock.ForeColor = Color.Black;
            }
            // In manual mode and not passed manual time
            else if (gameMode == GameModes.manual && !time.CheckTimeElapsed (midModeTime) && !speedMode) {

            }
            // In manual mode and passed mid point time
            else if (gameMode == GameModes.manual && time.CheckTimeElapsed (midModeTime) && !speedMode) {
                speedMode = true;
                fc.switchMode = true;
            }
            // In speed mode
            else if (gameMode == GameModes.manual && speedMode) {

            } else {
                Console.WriteLine ("Something went wrong with time keeping, please looking into this");
            }
        }

        private void testTimer_Tick(object sender, EventArgs e)
        {
            if(gameMode == GameModes.end)
            {
                //this.red.finalScore = this.red.score;
                //this.green.finalScore = this.green.score;
                //this.ScoreGame();
                //this.RecordGame();
                Thread.Sleep(200);
                fc.FieldAllOff();
                Thread.Sleep(1000);
                gameMode = GameModes.off;
                Thread.Sleep(100);
                //GameShutDown();

                //if (this.red.score != this.green.score)
                //{
                //    string file = "\\Match - BAD Scores";
                //    string folder = "Matches\\";
                //    string text = string.Format("Match# {0} - Red = {1} | Green = {2}", matchNumber, this.red.finalScore, this.green.finalScore);
                //    this.lw.WriteLog(text, file, folder);
                //}

                //Thread.Sleep(100);
                gameTimer.Stop();
                TestMode();
            }
        }
        #endregion

        #endregion

        //------------------------------------------------------------------------------------------------\\
        //Current year's game methods
        //------------------------------------------------------------------------------------------------\\

        private void btnGreenMantonomous_Click(object sender, EventArgs e)
        {
            if (btnGreenMantonomous.BackColor == DefaultBackColor && this.gameMode == GameModes.autonomous)
            {
                btnGreenMantonomous.BackColor = Color.Lime;
                btnGreenMantonomous.ForeColor = Color.Black;
    
                green.autoMan = true;
                GameLog("Green ManTonomous");

                fc.RobotTransmitters("green", State.off, State.on);
            }
        }

        private void btnRedMantonomous_Click(object sender, EventArgs e)
        {
            if (btnRedMantonomous.BackColor == DefaultBackColor && this.gameMode == GameModes.autonomous)
            {
                btnRedMantonomous.BackColor = Color.Red;
                btnRedMantonomous.ForeColor = Color.Black;

                red.autoMan = true;
                GameLog("Red ManTonomous");

                fc.RobotTransmitters("red", State.off, State.on);
            }
        }

        private void btnSetupGame_Click(object sender, EventArgs e)
        {
            GameStartUp(GameModes.ready);
            btnSetupGame.Visible = false;
            btnStartGame.Visible = true;
        }
    }

    //Vertical Progress Bar 
    public class VerticalProgressBar : ProgressBar
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= 0x04;
                return cp;
            }
        }

        protected override System.Drawing.Size DefaultSize { get { return new System.Drawing.Size(23, 100); } }  
    }
}
