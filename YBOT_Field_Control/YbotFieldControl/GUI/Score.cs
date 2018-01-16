using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YbotFieldControl
{
    public partial class Score : Form
    {
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
				gbGreenFinalScore.ForeColor = Color.SteelBlue;
			}

            UpdateScore ();
        }

        protected void UpdateScore () {
            UpdateScore (false);
        }

        protected void UpdateScore (bool updateTeams) {
            var autoScore = 0;
            var manualScore = 0;

            var greenPush = Convert.ToInt32 (tbGreenPushes.Text);
            var greenPenalty = Convert.ToInt32 (tbGreenPenalty.Text);

            int greenScore;
            if (!manualOverride) {
                greenScore = 0 - (greenPenalty * teamPenalty + greenPush * teamPenalty);
            } else {
                greenScore = Convert.ToInt32 (tbGreenScore.Text);
            }

            if (cbGreenDq.Checked) {
                greenScore = 0;
            }

            var redPush = Convert.ToInt32 (tbRedPushes.Text);
            var redPenalty = Convert.ToInt32 (tbRedPenalty.Text);

            int redScore;
            if (!manualOverride) {
                redScore = 0 - (redPenalty * teamPenalty + redPush * teamPenalty);
            } else {
                redScore = Convert.ToInt32 (tbRedScore.Text);
            }

            if (cbRedDq.Checked) {
                redScore = 0;
            }

            lbGreenPenaltyScore.Text = (greenPenalty * teamPenalty).ToString ();
            lbGreenPushScore.Text = (greenPush * teamPenalty).ToString ();

            lbRedPenaltyScore.Text = (redPenalty * teamPenalty).ToString ();
            lbRedPushScore.Text = (redPush * teamPenalty).ToString ();

            lbGreenScore.Text = greenScore.ToString ();
            lbRedScore.Text = redScore.ToString ();

            if (updateTeams) {
                var green = game.green;
                var red = game.red;

                green.autoScore = autoScore;
                green.manScore = manualScore;
                green.score = 0;

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

                red.autoScore = autoScore;
                red.manScore = manualScore;
                red.score = 0;

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

                tbGreenScore.Visible = true;
                tbGreenScore.Text = lbGreenScore.Text;

                tbRedScore.Visible = true;
                tbRedScore.Text = lbRedScore.Text;

                btnOverride.BackColor = Color.SteelBlue;
                manualOverride = true;
            } else {
                manualOverride = false;

                tbGreenScore.Visible = false;
                tbRedScore.Visible = false;

                UpdateScore ();

                btnOverride.BackColor = DefaultBackColor;
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
    }
}
