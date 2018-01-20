using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YbotFieldControl
{
    public partial class Score : Form
    {
        const int teamPenalty = 200;

        enum RocketPosition : int
        {
            Loaded = 1,
            DoorClosed = 3,
            LaunchPosition = 5
        }

        public bool done = false;
        public int manualOverride = 0;
        public bool acceptScores = false;

        GameControl game;

        public Score () : this (new GameControl ()) { }

        public Score (GameControl game) {
            InitializeComponent ();
            this.game = game;

            tbGreenPenalty.Validated += PenaltyAutoDq;
            tbRedPenalty.Validated += PenaltyAutoDq;
        }

        private void Score_Shown (object sender, EventArgs e) {
            InitScore ();
        }

        private void Score_FormClosed (object sender, FormClosedEventArgs e) {
            //UpdateScore (true);
            done = true;
        }

        private void InitScore () {
            var red = game.red;
            var green = game.green;

            cbRedAutoSwitchOff.Checked = red.autoSwitchTurnedOff;
            cbRedAutoRightTower.Checked = red.autoRightTowerPressed;
            cbRedAutoLeftTower.Checked = red.autoLeftTowerPressed;
            lbRedSpeedTowers.Text = red.speedTowers.ToString ();
            cbRedManualSwitchOn.Checked = red.manualSwitchTurnedOn;
            tbRedPenalty.Text = red.penalty.ToString ();
            if (red.dq || (red.penalty >= 3)) {
                cbRedDq.Checked = true;
            }

            cbGreenAutoSwitchOff.Checked = green.autoSwitchTurnedOff;
            cbGreenAutoRightTower.Checked = green.autoRightTowerPressed;
            cbGreenAutoLeftTower.Checked = green.autoLeftTowerPressed;
            lbGreenSpeedTowers.Text = green.speedTowers.ToString ();
            cbGreenManualSwitchOn.Checked = green.manualSwitchTurnedOn;
            tbGreenPenalty.Text = green.penalty.ToString ();
            if (green.dq || (green.penalty >= 3)) {
                cbGreenDq.Checked = true;
            }

            UpdateScore ();
        }

        protected void UpdateScore () {
            UpdateScore (false);
        }

        protected void UpdateScore (bool updateTeams) {
            var redAutoScore = 0;
            var redRodsRemoved = 0;
            var redRodsRemovedScore = 0;
            var redRodsReplaced = 0;
            var redRodsReplacedPointValue = 5;
            var redRodsReplacedScore = 0;
            var redReactorMultiplier = 1;
            var redReactorScore = 0;
            var redSpeedTowers = 0;
            var redSpeedTowerScore = 0;
            var redSpeedScore = 0;
            var redManualScore = 0;
            var redPushScore = 0;
            var redPenaltyScore = 0;
            var redFinalScore = 0;

            var greenAutoScore = 0;
            var greenRodsRemoved = 0;
            var greenRodsRemovedScore = 0;
            var greenRodsReplaced = 0;
            var greenRodsReplacedPointValue = 5;
            var greenRodsReplacedScore = 0;
            var greenReactorMultiplier = 1;
            var greenReactorScore = 0;
            var greenSpeedTowers = 0;
            var greenSpeedTowerScore = 0;
            var greenSpeedScore = 0;
            var greenManualScore = 0;
            var greenPushScore = 0;
            var greenPenaltyScore = 0;
            var greenFinalScore = 0;

            // Auto
            if (cbRedAutoSwitchOff.Checked) {
                redAutoScore = GameControl.AUTO_SWITCH_TURNED_OFF_SCORE;
                lbRedAutoSwitchOffScore.Text = GameControl.AUTO_SWITCH_TURNED_OFF_SCORE.ToString ();
            }
            if (cbRedAutoRightTower.Checked) {
                redAutoScore += GameControl.AUTO_RIGHT_TOWER_PRESSED_SCORE;
                lbRedAutoRightTowerScore.Text = GameControl.AUTO_RIGHT_TOWER_PRESSED_SCORE.ToString ();
            }
            if (cbRedAutoLeftTower.Checked) {
                redAutoScore += GameControl.AUTO_LEFT_TOWER_PRESSED_SCORE;
                lbRedAutoLeftTowerScore.Text = GameControl.AUTO_LEFT_TOWER_PRESSED_SCORE.ToString ();
            }
            lbRedAutoScore.Text = redAutoScore.ToString ();

            if (cbGreenAutoSwitchOff.Checked) {
                greenAutoScore = GameControl.AUTO_SWITCH_TURNED_OFF_SCORE;
                lbGreenAutoSwitchOffScore.Text = GameControl.AUTO_SWITCH_TURNED_OFF_SCORE.ToString ();
            }
            if (cbGreenAutoRightTower.Checked) {
                greenAutoScore += GameControl.AUTO_RIGHT_TOWER_PRESSED_SCORE;
                lbGreenAutoRightTowerScore.Text = GameControl.AUTO_RIGHT_TOWER_PRESSED_SCORE.ToString ();
            }
            if (cbGreenAutoLeftTower.Checked) {
                greenAutoScore += GameControl.AUTO_LEFT_TOWER_PRESSED_SCORE;
                lbGreenAutoLeftTowerScore.Text = GameControl.AUTO_LEFT_TOWER_PRESSED_SCORE.ToString ();
            }
            lbGreenAutoScore.Text = greenAutoScore.ToString ();

            // Reactor
            redRodsRemoved = Convert.ToInt32 (tbRedRodsRemoved.Text);
            redRodsRemovedScore = redRodsRemoved * 5;
            lbRedRodsRemovedScore.Text = redRodsRemovedScore.ToString ();
            redRodsReplaced = Convert.ToInt32 (tbRedRodsReplaced.Text);
            if (redRodsReplaced == 2) {
                redRodsReplacedPointValue = 15;
            } else if (redRodsReplaced == 3) {
                redRodsReplacedPointValue = 25;
            } else if (redRodsReplaced == 4) {
                redRodsReplacedPointValue = 50;
            }
            lbRedRodsReplacedPointValue.Text = redRodsReplacedPointValue.ToString ();
            redRodsReplacedScore = redRodsReplaced * redRodsReplacedPointValue;
            lbRedRodsReplacedScore.Text = redRodsReplacedScore.ToString ();
            if (cbRedRodZone.Text == "Yellow") {
                redReactorMultiplier = 2;
            } else if (cbRedRodZone.Text == "Green") {
                redReactorMultiplier = 4;
            }
            redReactorScore = (redRodsRemovedScore + redRodsReplacedScore) * redReactorMultiplier;
            lbRedReactorScore.Text = redReactorScore.ToString ();

            greenRodsRemoved = Convert.ToInt32 (tbGreenRodsRemoved.Text);
            greenRodsRemovedScore = greenRodsRemoved * 5;
            lbGreenRodsRemovedScore.Text = greenRodsRemovedScore.ToString ();
            greenRodsReplaced = Convert.ToInt32 (tbGreenRodsReplaced.Text);
            if (greenRodsReplaced == 2) {
                greenRodsReplacedPointValue = 15;
            } else if (greenRodsReplaced == 3) {
                greenRodsReplacedPointValue = 25;
            } else if (greenRodsReplaced == 4) {
                greenRodsReplacedPointValue = 50;
            }
            lbGreenRodsReplacedPointValue.Text = greenRodsReplacedPointValue.ToString ();
            greenRodsReplacedScore = greenRodsReplaced * greenRodsReplacedPointValue;
            lbGreenRodsReplacedScore.Text = greenRodsReplacedScore.ToString ();
            if (cbGreenRodZone.Text == "Yellow") {
                greenReactorMultiplier = 2;
            } else if (cbGreenRodZone.Text == "Green") {
                greenReactorMultiplier = 4;
            }
            greenReactorScore = (greenRodsRemovedScore + greenRodsReplacedScore) * greenReactorMultiplier;
            lbGreenReactorScore.Text = greenReactorScore.ToString ();

            // Speed
            if (manualOverride == 0) {
                redSpeedTowers = Convert.ToInt32 (lbRedSpeedTowers.Text);
            } else {
                redSpeedTowers = Convert.ToInt32 (tbRedSpeedTowers.Text);
            }
            redSpeedTowerScore = redSpeedTowers * GameControl.SPEED_BUTTON_PRESSED_SCORE;
            lbRedSpeedTowersScore.Text = redSpeedTowerScore.ToString ();
            redSpeedScore = redSpeedTowerScore;
            if (cbRedManualSwitchOn.Checked) {
                redSpeedScore += GameControl.SPEED_SWITCH_TURNED_ON_SCORE;
                lbRedManualSwitchOnScore.Text = GameControl.SPEED_SWITCH_TURNED_ON_SCORE.ToString ();
            }
            lbRedSpeedScore.Text = redSpeedScore.ToString ();

            if (manualOverride == 0) {
                greenSpeedTowers = Convert.ToInt32 (lbGreenSpeedTowers.Text);
            } else {
                greenSpeedTowers = Convert.ToInt32 (tbGreenSpeedTowers.Text);
            }
            greenSpeedTowerScore = greenSpeedTowers * GameControl.SPEED_BUTTON_PRESSED_SCORE;
            lbGreenSpeedTowersScore.Text = greenSpeedTowerScore.ToString ();
            greenSpeedScore = greenSpeedTowerScore;
            if (cbGreenManualSwitchOn.Checked) {
                greenSpeedScore += GameControl.SPEED_SWITCH_TURNED_ON_SCORE;
                lbGreenManualSwitchOnScore.Text = GameControl.SPEED_SWITCH_TURNED_ON_SCORE.ToString ();
            }
            lbGreenSpeedScore.Text = greenSpeedScore.ToString ();

            // Manual
            redManualScore = redReactorScore + redSpeedScore;
            lbRedManualScore.Text = redManualScore.ToString ();

            greenManualScore = greenReactorScore + greenSpeedScore;
            lbGreenManualScore.Text = greenManualScore.ToString ();

            // Penalties and Final
            redPushScore = Convert.ToInt32 (tbRedPushes.Text) * 100;
            lbRedPushScore.Text = redPushScore.ToString ();
            redPenaltyScore = Convert.ToInt32 (tbRedPenalty.Text) * 100;
            lbRedPenaltyScore.Text = redPenaltyScore.ToString ();
            if (manualOverride != 2) {
                redFinalScore = redAutoScore + redManualScore - redPushScore - redPenaltyScore;
            } else {
                redFinalScore = Convert.ToInt32 (tbRedScore.Text);
            }
            if (cbRedDq.Checked) {
                redFinalScore = 0;
            }
            lbRedScore.Text = redFinalScore.ToString ();

            greenPushScore = Convert.ToInt32 (tbGreenPushes.Text) * 100;
            lbGreenPushScore.Text = greenPushScore.ToString ();
            greenPenaltyScore = Convert.ToInt32 (tbGreenPenalty.Text) * 100;
            lbGreenPenaltyScore.Text = greenPenaltyScore.ToString ();
            if (manualOverride != 2) {
                greenFinalScore = greenAutoScore + greenManualScore - greenPushScore - greenPenaltyScore;
            } else {
                greenFinalScore = Convert.ToInt32 (tbGreenScore.Text);
            }
            if (cbGreenDq.Checked) {
                greenFinalScore = 0;
            }
            lbGreenScore.Text = greenFinalScore.ToString ();

            if (updateTeams) {
                var red = game.red;
                var green = game.green;

                red.autoSwitchTurnedOff = cbRedAutoSwitchOff.Checked;
                red.autoRightTowerPressed = cbRedAutoRightTower.Checked;
                red.autoLeftTowerPressed = cbRedAutoLeftTower.Checked;
                red.autoScore = redAutoScore;
                red.rodsRemoved = redRodsRemoved;
                red.rodsReplaced = redRodsReplaced;
                red.reactorScore = redReactorScore;
                red.reactorMultiplier = redReactorMultiplier;
                red.speedTowers = redSpeedTowers;
                red.manualSwitchTurnedOn = cbRedManualSwitchOn.Checked;
                red.speedScore = redSpeedScore;
                red.penalty = redPenaltyScore + redPushScore;
                red.dq = cbRedDq.Checked;
                red.score = redFinalScore;

                green.autoSwitchTurnedOff = cbGreenAutoSwitchOff.Checked;
                green.autoRightTowerPressed = cbGreenAutoRightTower.Checked;
                green.autoLeftTowerPressed = cbGreenAutoLeftTower.Checked;
                green.autoScore = greenAutoScore;
                green.rodsRemoved = greenRodsRemoved;
                green.rodsReplaced = greenRodsReplaced;
                green.reactorScore = greenReactorScore;
                green.reactorMultiplier = greenReactorMultiplier;
                green.speedTowers = greenSpeedTowers;
                green.manualSwitchTurnedOn = cbGreenManualSwitchOn.Checked;
                green.speedScore = greenSpeedScore;
                green.penalty = greenPenaltyScore + greenPushScore;
                green.dq = cbGreenDq.Checked;
                green.score = greenFinalScore;
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
            manualOverride = ++manualOverride % 3;
            if (manualOverride == 1) {
                cbRedAutoSwitchOff.Enabled = true;
                cbRedAutoRightTower.Enabled = true;
                cbRedAutoLeftTower.Enabled = true;
                cbRedManualSwitchOn.Enabled = true;
                tbRedSpeedTowers.Visible = true;
                lbRedSpeedTowers.Visible = false;
                tbRedSpeedTowers.Text = lbRedSpeedTowers.Text;

                cbGreenAutoSwitchOff.Enabled = true;
                cbGreenAutoRightTower.Enabled = true;
                cbGreenAutoLeftTower.Enabled = true;
                cbGreenManualSwitchOn.Enabled = true;
                tbGreenSpeedTowers.Visible = true;
                lbGreenSpeedTowers.Visible = false;
                tbGreenSpeedTowers.Text = lbGreenSpeedTowers.Text;

                btnOverride.BackColor = Color.SteelBlue;
            } else if (manualOverride == 2) {
                tbRedScore.Visible = true;
                lbRedScore.Visible = false;
                tbRedScore.Text = lbRedScore.Text;

                tbGreenScore.Visible = true;
                lbGreenScore.Visible = false;
                tbGreenScore.Text = lbGreenScore.Text;

                btnOverride.BackColor = Color.Crimson;
            } else {
                tbGreenScore.Visible = false;
                lbGreenScore.Visible = true;

                cbGreenAutoSwitchOff.Enabled = false;
                cbGreenAutoRightTower.Enabled = false;
                cbGreenAutoLeftTower.Enabled = false;

                cbGreenManualSwitchOn.Enabled = false;

                tbGreenSpeedTowers.Visible = false;
                lbGreenSpeedTowers.Visible = true;

                tbRedScore.Visible = false;
                lbRedScore.Visible = true;

                cbRedAutoSwitchOff.Enabled = false;
                cbRedAutoRightTower.Enabled = false;
                cbRedAutoLeftTower.Enabled = false;

                cbRedManualSwitchOn.Enabled = false;

                tbRedSpeedTowers.Visible = false;
                lbRedSpeedTowers.Visible = true;

                btnOverride.BackColor = DefaultBackColor;
                InitScore ();
            }
        }

        protected void TextBoxValidation (object sender, CancelEventArgs e) {
            var tb = sender as TextBox;
            if (tb != null) {
                try {
                    var number = Convert.ToInt32 (tb.Text);

                    if (manualOverride == 0) {
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
                    Console.WriteLine ("error with {0}", tb.Name);
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

        private void OnValidation (object sender, EventArgs e) {
            UpdateScore ();
        }

        private void CheckedChanged (object sender, EventArgs e) {
            var cb = sender as CheckBox;
            if (cb != null) {
                UpdateScore ();
            }
        }

        private void ComboBoxSelectedIndexChanged (object sender, EventArgs e) {
            var cb = sender as ComboBox;
            if (cb != null) {
                var reactorMultiplier = 1;
                if (cb.Text == "Yellow") {
                    reactorMultiplier = 2;
                } else if (cb.Text == "Green") {
                    reactorMultiplier = 4;
                }

                var reactorMultiplierString = string.Format ("x{0}", reactorMultiplier);
                if (cb.Name.Contains ("Red")) {
                    lbRedRodZoneMultiplier.Text = reactorMultiplierString;
                } else {
                    lbGreenRodZoneMultiplier.Text = reactorMultiplierString;
                }

                UpdateScore ();
            }
        }
    }
}
