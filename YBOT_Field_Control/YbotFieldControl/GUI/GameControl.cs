using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
        Button btnAdvanceTeam;

		public GameControl() : this (new FieldControl ()) { }

        public GameControl(FieldControl fc)
        {
            InitializeComponent();
            this.fc = fc;

            btnAdvanceTeam = new Button();
            btnAdvanceTeam.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            btnAdvanceTeam.Location = new Point(895, 125);
            btnAdvanceTeam.Name = "btnAdvanceTeam";
            btnAdvanceTeam.Size = new Size(100, 75);
            btnAdvanceTeam.Text = "Advance Team";
            btnAdvanceTeam.UseVisualStyleBackColor = true;
            btnAdvanceTeam.Visible = false;
            Controls.Add(btnAdvanceTeam);

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
                this.fc.RobotTransmitters("green", State.on, State.on);
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
                this.fc.RobotTransmitters("red", State.on, State.on);
                this.GameLog("Red Disabled = False");
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

            GameStartUp();
            Thread.Sleep (200);

            autoModeTime = 10;
            manAutoTime = 0;
            //midModeTime = 90;
            //time.CountDownStart(2, 30);
            midModeTime = 15;
            time.CountDownStart(1, 15);
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
                lblMatchNumber.Text = "Match " + matchNumber.ToString();
                GD.lblMatchNumber.Text = "Match " + matchNumber.ToString();
                ClearDisplay();
                GetTeamNames();
                YBotSqlData.Global.currentMatchNumber = matchNumber;
            }
        }

        private void btnMatchPrev_Click(object sender, EventArgs e) {
            if (gameMode == GameModes.off) {
                if (matchNumber > 0) matchNumber--;
                lblMatchNumber.Text = "Match " + matchNumber.ToString();
                GD.lblMatchNumber.Text = "Match " + matchNumber.ToString();
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
                //ScoreGame(); // not used this year
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

        // Not used this year
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

            string greenTeam = (matchNumber.ToString() + "\t" + lblGreenTeam.Text + "\t" + green.finalScore.ToString()
                               + "\t" + green.penalty.ToString() + "\t" + green.dq.ToString() + "\t" + green.matchResult);
            string greenTeam2 = string.Format ("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}", 
                green.autoTowerTested,
                green.autoEmergencyTowerCycled,
                green.autoSolarPanelScore,
                green.manSolarPanelScore1,
                green.manSolarPanelScore2,
                green.emergencyCleared,
                green.rocketPosition,
                green.rockWeight,
                green.rockScore,
                green.rocketBonus);


            string field = ("Match Number" + "\t" + "Team Name" + "\t" + "Final Score" + "\t" + "Penalties" + "\t" + "DQ" + "\t" + "Result");
            string field2 = ("Auto Tested" + "\t" + "Auto Cycled" + "\t" + "Auto Solar" + "\t" + "Manual Solar 1" + "\t" + "Manual Solar 2" + "\t" + 
                "Emergencies Clear" + "\t" + "Rocket Position" + "\t" + "Rock Weight" + "\t" + "Rock Score" + "\t" + "Rocket Bonus");

            string text = "\r\n" + field + "\t" + field2 + "\r\n" + greenTeam + "\t" + greenTeam2;

			if (!IsChampionshipMatch()) {
				string redTeam = (matchNumber.ToString() + "\t" + lblRedTeam.Text + "\t" + red.finalScore.ToString()
								 + "\t" + red.penalty.ToString() + "\t" + red.dq.ToString() + "\t" + red.matchResult);

				string redTeam2 = string.Format("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}",
					red.autoTowerTested,
					red.autoEmergencyTowerCycled,
					red.autoSolarPanelScore,
					red.manSolarPanelScore1,
					red.manSolarPanelScore2,
					red.emergencyCleared,
					red.rocketPosition,
					red.rockWeight,
					red.rockScore,
					red.rocketBonus);

				text += "\r\n" + redTeam + "\t" + redTeam2;
			}

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

					green.StoreTeamVariablesToSqlMatch(ref match);
					match.greenTeam = FindSchoolId(schools, lblGreenTeam.Text, "Green Team");

					if (!IsChampionshipMatch()) {
						red.StoreTeamVariablesToSqlMatch(ref match);
						match.redTeam = FindSchoolId(schools, lblRedTeam.Text, "Red Team");
					}

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
				var match = YbotSql.Instance.GetMatch(YBotSqlData.Global.tournaments[YBotSqlData.Global.currentTournament].id, matchId);
				while (match.Status == TaskStatus.Running
				       || match.Status == TaskStatus.Created 
				       || match.Status == TaskStatus.WaitingForActivation
				       || match.Status == TaskStatus.WaitingForChildrenToComplete
				       || match.Status == TaskStatus.WaitingToRun) 
				{
					continue;
				}

				if (match.Status == TaskStatus.RanToCompletion) {
					var greenTeam = YBotSqlData.Global.schools[match.Result.greenTeam];
					var redTeam = YBotSqlData.Global.schools[match.Result.redTeam];

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

                    var greenScore = match.Result.greenScore;
                    var redScore = match.Result.redScore;

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

        private void DisableGameButtons () {
            btnStartGame.Enabled = false;
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

			if (tournaments[index].name == "Championship") {
				EnableRedTeam();
			}

			index = ++index % tournaments.Count;

			if (tournaments[index].name == "Championship") {
				DisableRedTeam();
			}

			lblTournamentName.Text = tournaments[index].name;
			YBotSqlData.Global.currentTournament = lblTournamentName.Text;
		}

        private void btnTournamentPrev_Click (object sender, EventArgs e) {
            var tournaments = YBotSqlData.Global.tournaments;
            var index = tournaments.IndexOf (tournaments[lblTournamentName.Text]);

			if (tournaments[index].name == "Championship") {
				EnableRedTeam();
			}

            index = --index;
            if (index < 0) {
                index = tournaments.Count - 1;
            }

			if (tournaments[index].name == "Championship") {
				DisableRedTeam();
			}

            lblTournamentName.Text = tournaments[index].name;
            YBotSqlData.Global.currentTournament = lblTournamentName.Text;
        }

		private void EnableRedTeam() {
			btnDisableRed.Click += btnDisableRed_Click;
			btnDisableRed.PerformClick();

            lblChampionshipRounds.Visible = false;
            btnChampionshipRoundNext.Visible = false;
            btnChampionshipRoundPrevious.Visible = false;
			lblRedDQ.Visible = true;
			lblRedPenalty1.Visible = true;
			lblRedPenalty3.Visible = true;
			lblRedPenalty2.Visible = true;
			grbRedScore.Visible = true;
			grbRedPenalty.Visible = true;
			lblRedTeam.Visible = true;
			lblRedScore.Visible = true;
			grbRedScore.Visible = true;
			btnRedMantonomous.Visible = true;
            btnAdvanceTeam.Visible = false;
		}

		private void DisableRedTeam() {
			btnDisableRed.PerformClick();
			btnDisableRed.Click -= btnDisableRed_Click;

            lblChampionshipRounds.Visible = true;
            btnChampionshipRoundNext.Visible = true;
            btnChampionshipRoundPrevious.Visible = true;
            lblRedDQ.Visible = false;
			lblRedPenalty1.Visible = false;
			lblRedPenalty3.Visible = false;
			lblRedPenalty2.Visible = false;
			grbRedScore.Visible = false;
			grbRedPenalty.Visible = false;
			lblRedTeam.Visible = false;
			lblRedScore.Visible = false;
			grbRedScore.Visible = false;
			btnRedMantonomous.Visible = false;
            if (IsChampionshipBracketMatch()) {
                btnAdvanceTeam.Visible = true;
            } else {
                btnAdvanceTeam.Visible = false;
            }
		}

        private void btnChampionshipRound_Click (object sender, EventArgs e) {
            if (lblChampionshipRounds.Text == "Practice") {
                lblChampionshipRounds.Text = "Bracket";
                btnAdvanceTeam.Visible = true;
            } else {
                lblChampionshipRounds.Text = "Practice";
                btnAdvanceTeam.Visible = false;
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
            else if (gameMode == GameModes.manual && !time.CheckTimeElapsed (midModeTime) && !_speedMode) {

            }
            // In manual mode and passed mid point time
            else if (gameMode == GameModes.manual && time.CheckTimeElapsed (midModeTime) && !_speedMode) {
                _speedMode = true;
                fc.switchMode = true;
            }
            // In speed mode
            else if (gameMode == GameModes.manual && _speedMode) {

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
