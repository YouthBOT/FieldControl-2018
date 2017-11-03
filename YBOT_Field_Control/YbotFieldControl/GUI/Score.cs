using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YbotFieldControl
{
    public partial class Score : Form
    {
        const int autoCornersTestedPointValue = 50;
        const int autoEmergencyCycledPointValue = 100;
        const int manualEmergencyCycledPointValue = 100;
        const int emergencyCycledPenaltyPointValue = 250;
        const int minimumEmergencyCycled = 4;
        const int rocketLaunchedPointValue = 200;
        const int teamPenalty = 200;

        enum RocketPosition : int {
            Loaded = 1,
            DoorClosed = 3,
            LaunchPosition = 5
        }

        public bool done = false;
        public bool manualOverride = false;
        public bool acceptScores = false;

        GameControl game;

        public Score () : this (new GameControl ()) { }

        public Score (GameControl game) {
            InitializeComponent();
            this.game = game;

            tbGreenPenalty.Validated += PenaltyAutoDq;
            tbRedPenalty.Validated += PenaltyAutoDq;
            tbManualEmergencyCycled.Validated += ManualEmergencyCycledAutoPenalty;
        }

        private void Score_Shown (object sender, EventArgs e) {
            InitScore ();
        }

        private void Score_FormClosed (object sender, FormClosedEventArgs e) {
            //UpdateScore (true);
            done = true;
        }

        private void InitScore () {
            var green = game.green;
            var red = game.red;
			var joint = game.joint;

			var jointScore = joint.score.ToString();
			tbJointScore.Text = jointScore;
			tbRedScore.Text = jointScore;
			tbGreenScore.Text = jointScore;

			tbAutoCornersTested.Text = joint.autoTowerTested.ToString ();
			tbAutoEmergencyCycled.Text = joint.autoEmergencyTowerCycled.ToString ();
			tbAutoSolarScore.Text = joint.autoSolarPanelScore.ToString ();
			tbManualSolar1Score.Text = joint.manSolarPanelScore1.ToString ();
			tbManualSolar2Score.Text = joint.manSolarPanelScore2.ToString ();
			tbManualEmergencyCycled.Text = joint.emergencyCleared.ToString ();
			if (joint.emergencyCleared < minimumEmergencyCycled) {
				cbEmergencyCycledPenalty.Text = emergencyCycledPenaltyPointValue.ToString();
			} else {
				cbEmergencyCycledPenalty.Text = "0";
			}

            tbGreenPenalty.Text = green.penalty.ToString ();
            tbRedPenalty.Text = red.penalty.ToString ();

            if (green.dq || (green.penalty >= 3)) {
                cbGreenDq.Checked = true;
            }

            if (red.dq || (red.penalty >= 3)) {
                cbRedDq.Checked = true;
            }

			if (game.IsChampionshipMatch()) {
				grRedFinalScore.Visible = false;
				gbGreenFinalScore.Text = "Individual Final Score";
				gbGreenFinalScore.ForeColor = System.Drawing.Color.SteelBlue;
			}

            UpdateScore ();
        }

        protected void UpdateScore () {
            UpdateScore (false);
        }

        protected void UpdateScore (bool updateTeams) {
            var autoCornersTested = Convert.ToInt32 (tbAutoCornersTested.Text);
            var autoCornersTestedScore = autoCornersTested * autoCornersTestedPointValue;
            var autoEmergencyCycled = Convert.ToInt32 (tbAutoEmergencyCycled.Text);
            var autoEmergencyCycledScore = autoEmergencyCycled * autoEmergencyCycledPointValue;
            var autoSolar = Convert.ToInt32 (tbAutoSolarScore.Text);
            var manualSolar1 = Convert.ToInt32 (tbManualSolar1Score.Text);
            var manualSolar2 = Convert.ToInt32 (tbManualSolar2Score.Text);
            var manualEmergencyCycled = Convert.ToInt32 (tbManualEmergencyCycled.Text);
            var manualEmergencyCycledScore = manualEmergencyCycled * manualEmergencyCycledPointValue;
            var emergencyCycledPenalty = Convert.ToInt32 (cbEmergencyCycledPenalty.Text);
            var rocketPositionMultiplier = Convert.ToInt32 (lbRocketPositionMulitplier.Text);
            var rockWeight = Convert.ToInt32 (tbRockWeight.Text);
            var rockScore = rockWeight * rocketPositionMultiplier;
            var rocketLaunched = Convert.ToInt32 (lbRocketLaunchedScore.Text);
            var autoScore = autoCornersTestedScore + autoEmergencyCycledScore + autoSolar;
            var manualScore = manualSolar1 + manualSolar2 + manualEmergencyCycledScore - emergencyCycledPenalty + rockScore + rocketLaunched;

            int jointScore;
            if (!manualOverride) {
                jointScore = autoScore + manualScore;
            } else {
                jointScore = Convert.ToInt32 (tbJointScore.Text);
            }

            var greenPush = Convert.ToInt32 (tbGreenPushes.Text);
            var greenPenalty = Convert.ToInt32 (tbGreenPenalty.Text);

            int greenScore;
            if (!manualOverride) {
                greenScore = jointScore - (greenPenalty * teamPenalty + greenPush * teamPenalty);
            } else {
                greenScore = Convert.ToInt32 (tbGreenScore.Text);
            }

            if (cbGreenDq.Checked || cbGreenDidntPlay.Checked) {
                greenScore = 0;
            }

            var redPush = Convert.ToInt32 (tbRedPushes.Text);
            var redPenalty = Convert.ToInt32 (tbRedPenalty.Text);

            int redScore;
            if (!manualOverride) {
                redScore = jointScore - (redPenalty * teamPenalty + redPush * teamPenalty);
            } else {
                redScore = Convert.ToInt32 (tbRedScore.Text);
            }

            if (cbRedDq.Checked || cbRedDidntPlay.Checked) {
                redScore = 0;
            }

            lbAutoCornerTestedScore.Text = autoCornersTestedScore.ToString ();
            lbAutoEmergencyCycledScore.Text = autoEmergencyCycledScore.ToString ();
            lbManualEmergencyCycledScore.Text = manualEmergencyCycledScore.ToString ();
            lbRockScore.Text = rockScore.ToString ();
            lbJointScore.Text = jointScore.ToString ();

            lbGreenPenaltyScore.Text = (greenPenalty * teamPenalty).ToString ();
            lbGreenPushScore.Text = (greenPush * teamPenalty).ToString ();

            lbRedPenaltyScore.Text = (redPenalty * teamPenalty).ToString ();
            lbRedPushScore.Text = (redPush * teamPenalty).ToString ();

            lbGreenScore.Text = greenScore.ToString ();
            lbRedScore.Text = redScore.ToString ();

            if (updateTeams) {
                var green = game.green;
                var red = game.red;
				var joint = game.joint;

				joint.autoTowerTested = autoCornersTested;
				joint.autoEmergencyTowerCycled = autoEmergencyCycled;
				joint.autoSolarPanelScore = autoSolar;

				joint.manSolarPanelScore1 = manualSolar1;
				joint.manSolarPanelScore2 = manualSolar2;
				joint.emergencyCleared = manualEmergencyCycled;

				joint.rockWeight = rockWeight;
				joint.rockScore = rockScore;
				joint.rocketPosition = rocketPositionMultiplier;
				joint.rocketBonus = cbRocketLaunched.Checked;

				joint.autoScore = autoScore;
				joint.manScore = manualScore;
				joint.score = jointScore;

                green.autoTowerTested = autoCornersTested;
                green.autoEmergencyTowerCycled = autoEmergencyCycled;
                green.autoSolarPanelScore = autoSolar;

                green.manSolarPanelScore1 = manualSolar1;
                green.manSolarPanelScore2 = manualSolar2;
                green.emergencyCleared = manualEmergencyCycled;

                green.rockWeight = rockWeight;
                green.rockScore = rockScore;
                green.rocketPosition = rocketPositionMultiplier;
                green.rocketBonus = cbRocketLaunched.Checked;

                green.autoScore = autoScore;
                green.manScore = manualScore;
                green.score = jointScore;

                green.penalty = greenPenalty;
                if (cbGreenDq.Checked) {
                    green.dq = true;
                } else {
                    green.dq = false;
                }

                if (!green.dq) {
                    green.finalScore = greenScore;
                } else {
                    green.finalScore = 0;
                }

                if (!cbGreenDidntPlay.Checked) {
                    green.matchResult = "P";
                } else {
                    green.matchResult = "NP";
                }

                red.autoTowerTested = autoCornersTested;
                red.autoEmergencyTowerCycled = autoEmergencyCycled;
                red.autoSolarPanelScore = autoSolar;

                red.manSolarPanelScore1 = manualSolar1;
                red.manSolarPanelScore2 = manualSolar2;
                red.emergencyCleared = manualEmergencyCycled;

                red.rockWeight = rockWeight;
                red.rockScore = rockScore;
                red.rocketPosition = rocketPositionMultiplier;
                red.rocketBonus = cbRocketLaunched.Checked;

                red.autoScore = autoScore;
                red.manScore = manualScore;
                red.score = jointScore;

                red.penalty = redPenalty;
                if (cbRedDq.Checked) {
                    red.dq = true;
                } else {
                    red.dq = false;
                }

                if (!red.dq) {
                    red.finalScore = redScore;
                } else {
                    red.finalScore = 0;
                }

                if (!cbRedDidntPlay.Checked) {
                    red.matchResult = "P";
                } else {
                    red.matchResult = "NP";
                }
            }
        }

        private void btnFinalScore_Click (object sender, EventArgs e) {
            UpdateScore (true);
            acceptScores = true;
            done = true;
        }

        private void btnUpdateScore_Click (object sender, EventArgs e) {
            UpdateScore ();
        }

        private void btnOverride_Click (object sender, EventArgs e) {
            if (!manualOverride) {
                UpdateScore ();

                tbJointScore.Visible = true;
                tbJointScore.Text = lbJointScore.Text;

                tbGreenScore.Visible = true;
                tbGreenScore.Text = lbGreenScore.Text;

                tbRedScore.Visible = true;
                tbRedScore.Text = lbRedScore.Text;

                btnOverride.BackColor = Color.SteelBlue;
                manualOverride = true;
            } else {
                manualOverride = false;

                tbJointScore.Visible = false;
                tbGreenScore.Visible = false;
                tbRedScore.Visible = false;

                UpdateScore ();

                btnOverride.BackColor = DefaultBackColor;
            }
        }

        private void cbRocketLaunched_CheckedChanged (object sender, EventArgs e) {
            var cb = sender as CheckBox;
            if (cb != null) {
                if (cb.Checked) {
                    lbRocketLaunchedScore.Text = rocketLaunchedPointValue.ToString ();
                } else {
                    lbRocketLaunchedScore.Text = "0";
                }

                UpdateScore ();
            }
        }

        protected void TextBoxValidation (object sender, CancelEventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var number = Convert.ToInt32 (tb.Text);

                    if (!manualOverride) {
                        var tag = tb.Tag as string;
                        if (tag != null) {
                            if (!string.IsNullOrWhiteSpace (tag)) {
                                try {
                                    var indexComma = tag.IndexOf (',');
                                    var min = Convert.ToInt32 (tag.Substring (1, indexComma - 1));
                                    var max = Convert.ToInt32 (tag.Substring (indexComma + 1, tag.Length - indexComma - 2));
                                    if ((number < min) || (number > max)) {
                                        MessageBox.Show (string.Format ("Invalid number of towers\nMust be between {0} and {1}", min, max));
                                        e.Cancel = true;
                                    }
                                } catch {
                                    //
                                }
                            }
                        }
                    }
                } catch (FormatException) {
					Console.WriteLine("error with {0}", tb.Name);
					MessageBox.Show ("Invalid integer format");
                    e.Cancel = true;
                } catch (OverflowException) {
                    MessageBox.Show ("Number too large");
                    e.Cancel = true;
                }
                
            } else {
                e.Cancel = true;
            }
        }

        private void PenaltyAutoDq (object sender, EventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var penalties = Convert.ToInt32 (tb.Text);
                    if (penalties >= 3) {
                        if (tb.Name.Contains ("Green")) {
                            cbGreenDq.Checked = true;
                        } else if (tb.Name.Contains ("Red")) {
                            cbRedDq.Checked = true;
                        }
                    } else {
                        if (tb.Name.Contains ("Green")) {
                            cbGreenDq.Checked = false;
                        } else if (tb.Name.Contains ("Red")) {
                            cbRedDq.Checked = false;
                        }
                    }
                } catch {
                    //
                }
            } else {
            }
        }

        private void ManualEmergencyCycledAutoPenalty (object sender, EventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var towers = Convert.ToInt32 (tb.Text);
                    if (towers < minimumEmergencyCycled) {
                        cbEmergencyCycledPenalty.Text = emergencyCycledPenaltyPointValue.ToString ();
                    } else {
                        cbEmergencyCycledPenalty.Text = "0";
                    }
                } catch {
                    //
                }
            }
        }

        private void OnValidation (object sender, EventArgs e) {
            UpdateScore ();
        }

        private void CheckedChanged (object sender, EventArgs e) {
            var cb = sender as CheckBox;
            if (cb != null) {
                if ((cb.Name.Contains ("Green")) && cb.Checked) {
                    if (cb.Name.Contains ("Dq")) {
                        cbGreenDidntPlay.Checked = false;
                    } else {
                        cbGreenDq.Checked = false;
                    }
                } else if ((cb.Name.Contains ("Red")) && cb.Checked) {
                    if (cb.Name.Contains ("Dq")) {
                        cbRedDidntPlay.Checked = false;
                    } else {
                        cbRedDq.Checked = false;
                    }
                }

                UpdateScore ();
            }
        }

        private void cbRocketPosition_TextChanged (object sender, EventArgs e) {
            var cb = sender as ComboBox;
            if (cb != null) {
                var rocketPositionMultiplier = RocketPosition.Loaded;

                cbRocketLaunched.Enabled = false;
                cbRocketLaunched.Checked = false;
                lbRocketLaunchedScore.Text = "0";

                switch (cb.Text) {
                case "Door Closed":
                    rocketPositionMultiplier = RocketPosition.DoorClosed;
                    break;
                case "Launch Position":
                    rocketPositionMultiplier = RocketPosition.LaunchPosition;
                    cbRocketLaunched.Enabled = true;
                    break;
                default:
                    rocketPositionMultiplier = RocketPosition.Loaded;
                    break;
                }

                lbRocketPositionMulitplier.Text = ((int)rocketPositionMultiplier).ToString ();

                UpdateScore ();
            }
        }
    }
}
