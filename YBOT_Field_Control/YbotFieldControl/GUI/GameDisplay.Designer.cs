namespace YbotFieldControl
{
    partial class GameDisplay
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameDisplay));
            this.YBOTLogo = new System.Windows.Forms.PictureBox();
            this.lblMatchNumber = new System.Windows.Forms.Label();
            this.grbGreenScore = new System.Windows.Forms.GroupBox();
            this.lblGreenTeam = new System.Windows.Forms.Label();
            this.lblGreenScore = new System.Windows.Forms.Label();
            this.lblRedPenalty2 = new System.Windows.Forms.Label();
            this.lblRedPenalty3 = new System.Windows.Forms.Label();
            this.grbFieldDisplay = new System.Windows.Forms.GroupBox();
            this.lblGameClock = new System.Windows.Forms.Label();
            this.lblRedDQ = new System.Windows.Forms.Label();
            this.grbGreenPenalty = new System.Windows.Forms.GroupBox();
            this.lblGreenPenalty1 = new System.Windows.Forms.Label();
            this.lblGreenPenalty2 = new System.Windows.Forms.Label();
            this.lblGreenPenalty3 = new System.Windows.Forms.Label();
            this.lblGreenDQ = new System.Windows.Forms.Label();
            this.grbRedScore = new System.Windows.Forms.GroupBox();
            this.lblRedTeam = new System.Windows.Forms.Label();
            this.lblRedScore = new System.Windows.Forms.Label();
            this.lblRedPenalty1 = new System.Windows.Forms.Label();
            this.grbRedPenalty = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.YBOTLogo)).BeginInit();
            this.grbGreenScore.SuspendLayout();
            this.grbFieldDisplay.SuspendLayout();
            this.grbGreenPenalty.SuspendLayout();
            this.grbRedScore.SuspendLayout();
            this.grbRedPenalty.SuspendLayout();
            this.SuspendLayout();
            // 
            // YBOTLogo
            // 
            this.YBOTLogo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.YBOTLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.YBOTLogo.Image = ((System.Drawing.Image)(resources.GetObject("YBOTLogo.Image")));
            this.YBOTLogo.Location = new System.Drawing.Point(329, 25);
            this.YBOTLogo.Name = "YBOTLogo";
            this.YBOTLogo.Size = new System.Drawing.Size(367, 74);
            this.YBOTLogo.TabIndex = 161;
            this.YBOTLogo.TabStop = false;
            // 
            // lblMatchNumber
            // 
            this.lblMatchNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMatchNumber.AutoSize = true;
            this.lblMatchNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatchNumber.Location = new System.Drawing.Point(362, 124);
            this.lblMatchNumber.MinimumSize = new System.Drawing.Size(300, 0);
            this.lblMatchNumber.Name = "lblMatchNumber";
            this.lblMatchNumber.Size = new System.Drawing.Size(300, 39);
            this.lblMatchNumber.TabIndex = 158;
            this.lblMatchNumber.Text = "Match 00";
            this.lblMatchNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grbGreenScore
            // 
            this.grbGreenScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbGreenScore.Controls.Add(this.lblGreenTeam);
            this.grbGreenScore.Controls.Add(this.lblGreenScore);
            this.grbGreenScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbGreenScore.Location = new System.Drawing.Point(667, 158);
            this.grbGreenScore.Name = "grbGreenScore";
            this.grbGreenScore.Padding = new System.Windows.Forms.Padding(10);
            this.grbGreenScore.Size = new System.Drawing.Size(315, 233);
            this.grbGreenScore.TabIndex = 156;
            this.grbGreenScore.TabStop = false;
            // 
            // lblGreenTeam
            // 
            this.lblGreenTeam.AutoSize = true;
            this.lblGreenTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenTeam.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblGreenTeam.Location = new System.Drawing.Point(30, 21);
            this.lblGreenTeam.MinimumSize = new System.Drawing.Size(250, 0);
            this.lblGreenTeam.Name = "lblGreenTeam";
            this.lblGreenTeam.Size = new System.Drawing.Size(250, 33);
            this.lblGreenTeam.TabIndex = 67;
            this.lblGreenTeam.Text = "GreenTeam";
            this.lblGreenTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGreenScore
            // 
            this.lblGreenScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenScore.ForeColor = System.Drawing.Color.LimeGreen;
            this.lblGreenScore.Location = new System.Drawing.Point(30, 71);
            this.lblGreenScore.Name = "lblGreenScore";
            this.lblGreenScore.Size = new System.Drawing.Size(250, 140);
            this.lblGreenScore.TabIndex = 65;
            this.lblGreenScore.Text = "000";
            this.lblGreenScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRedPenalty2
            // 
            this.lblRedPenalty2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRedPenalty2.AutoSize = true;
            this.lblRedPenalty2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedPenalty2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPenalty2.ForeColor = System.Drawing.SystemColors.Control;
            this.lblRedPenalty2.Location = new System.Drawing.Point(20, 97);
            this.lblRedPenalty2.MinimumSize = new System.Drawing.Size(50, 25);
            this.lblRedPenalty2.Name = "lblRedPenalty2";
            this.lblRedPenalty2.Padding = new System.Windows.Forms.Padding(5);
            this.lblRedPenalty2.Size = new System.Drawing.Size(111, 37);
            this.lblRedPenalty2.TabIndex = 142;
            this.lblRedPenalty2.Text = "- 10 pts ";
            this.lblRedPenalty2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRedPenalty3
            // 
            this.lblRedPenalty3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRedPenalty3.AutoSize = true;
            this.lblRedPenalty3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedPenalty3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPenalty3.ForeColor = System.Drawing.SystemColors.Control;
            this.lblRedPenalty3.Location = new System.Drawing.Point(20, 157);
            this.lblRedPenalty3.MinimumSize = new System.Drawing.Size(50, 25);
            this.lblRedPenalty3.Name = "lblRedPenalty3";
            this.lblRedPenalty3.Padding = new System.Windows.Forms.Padding(5);
            this.lblRedPenalty3.Size = new System.Drawing.Size(111, 37);
            this.lblRedPenalty3.TabIndex = 143;
            this.lblRedPenalty3.Text = "- 10 pts ";
            this.lblRedPenalty3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grbFieldDisplay
            // 
            this.grbFieldDisplay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.grbFieldDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.grbFieldDisplay.Controls.Add(this.lblGameClock);
            this.grbFieldDisplay.Location = new System.Drawing.Point(218, 428);
            this.grbFieldDisplay.Name = "grbFieldDisplay";
            this.grbFieldDisplay.Size = new System.Drawing.Size(576, 316);
            this.grbFieldDisplay.TabIndex = 157;
            this.grbFieldDisplay.TabStop = false;
            // 
            // lblGameClock
            // 
            this.lblGameClock.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblGameClock.AutoSize = true;
            this.lblGameClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 100F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameClock.Location = new System.Drawing.Point(113, 100);
            this.lblGameClock.Name = "lblGameClock";
            this.lblGameClock.Size = new System.Drawing.Size(331, 153);
            this.lblGameClock.TabIndex = 9;
            this.lblGameClock.Text = "0:00";
            // 
            // lblRedDQ
            // 
            this.lblRedDQ.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRedDQ.AutoSize = true;
            this.lblRedDQ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedDQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedDQ.ForeColor = System.Drawing.SystemColors.Control;
            this.lblRedDQ.Location = new System.Drawing.Point(36, 220);
            this.lblRedDQ.MinimumSize = new System.Drawing.Size(75, 75);
            this.lblRedDQ.Name = "lblRedDQ";
            this.lblRedDQ.Padding = new System.Windows.Forms.Padding(5);
            this.lblRedDQ.Size = new System.Drawing.Size(75, 75);
            this.lblRedDQ.TabIndex = 144;
            this.lblRedDQ.Text = "DQ";
            this.lblRedDQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grbGreenPenalty
            // 
            this.grbGreenPenalty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbGreenPenalty.Controls.Add(this.lblGreenPenalty1);
            this.grbGreenPenalty.Controls.Add(this.lblGreenPenalty2);
            this.grbGreenPenalty.Controls.Add(this.lblGreenPenalty3);
            this.grbGreenPenalty.Controls.Add(this.lblGreenDQ);
            this.grbGreenPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbGreenPenalty.Location = new System.Drawing.Point(818, 428);
            this.grbGreenPenalty.Name = "grbGreenPenalty";
            this.grbGreenPenalty.Padding = new System.Windows.Forms.Padding(5);
            this.grbGreenPenalty.Size = new System.Drawing.Size(164, 316);
            this.grbGreenPenalty.TabIndex = 160;
            this.grbGreenPenalty.TabStop = false;
            this.grbGreenPenalty.Text = "Green Penalties";
            // 
            // lblGreenPenalty1
            // 
            this.lblGreenPenalty1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenPenalty1.AutoSize = true;
            this.lblGreenPenalty1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenPenalty1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenPenalty1.ForeColor = System.Drawing.SystemColors.Control;
            this.lblGreenPenalty1.Location = new System.Drawing.Point(28, 37);
            this.lblGreenPenalty1.MinimumSize = new System.Drawing.Size(50, 25);
            this.lblGreenPenalty1.Name = "lblGreenPenalty1";
            this.lblGreenPenalty1.Padding = new System.Windows.Forms.Padding(5);
            this.lblGreenPenalty1.Size = new System.Drawing.Size(111, 37);
            this.lblGreenPenalty1.TabIndex = 146;
            this.lblGreenPenalty1.Text = "- 10 pts ";
            this.lblGreenPenalty1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGreenPenalty2
            // 
            this.lblGreenPenalty2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenPenalty2.AutoSize = true;
            this.lblGreenPenalty2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenPenalty2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenPenalty2.ForeColor = System.Drawing.SystemColors.Control;
            this.lblGreenPenalty2.Location = new System.Drawing.Point(28, 97);
            this.lblGreenPenalty2.MinimumSize = new System.Drawing.Size(50, 25);
            this.lblGreenPenalty2.Name = "lblGreenPenalty2";
            this.lblGreenPenalty2.Padding = new System.Windows.Forms.Padding(5);
            this.lblGreenPenalty2.Size = new System.Drawing.Size(111, 37);
            this.lblGreenPenalty2.TabIndex = 147;
            this.lblGreenPenalty2.Text = "- 10 pts ";
            this.lblGreenPenalty2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGreenPenalty3
            // 
            this.lblGreenPenalty3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenPenalty3.AutoSize = true;
            this.lblGreenPenalty3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenPenalty3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenPenalty3.ForeColor = System.Drawing.SystemColors.Control;
            this.lblGreenPenalty3.Location = new System.Drawing.Point(28, 157);
            this.lblGreenPenalty3.MinimumSize = new System.Drawing.Size(50, 25);
            this.lblGreenPenalty3.Name = "lblGreenPenalty3";
            this.lblGreenPenalty3.Padding = new System.Windows.Forms.Padding(5);
            this.lblGreenPenalty3.Size = new System.Drawing.Size(111, 37);
            this.lblGreenPenalty3.TabIndex = 148;
            this.lblGreenPenalty3.Text = "- 10 pts ";
            this.lblGreenPenalty3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGreenDQ
            // 
            this.lblGreenDQ.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenDQ.AutoSize = true;
            this.lblGreenDQ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenDQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenDQ.ForeColor = System.Drawing.SystemColors.Control;
            this.lblGreenDQ.Location = new System.Drawing.Point(45, 220);
            this.lblGreenDQ.MinimumSize = new System.Drawing.Size(75, 75);
            this.lblGreenDQ.Name = "lblGreenDQ";
            this.lblGreenDQ.Padding = new System.Windows.Forms.Padding(5);
            this.lblGreenDQ.Size = new System.Drawing.Size(75, 75);
            this.lblGreenDQ.TabIndex = 149;
            this.lblGreenDQ.Text = "DQ";
            this.lblGreenDQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grbRedScore
            // 
            this.grbRedScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbRedScore.Controls.Add(this.lblRedTeam);
            this.grbRedScore.Controls.Add(this.lblRedScore);
            this.grbRedScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbRedScore.Location = new System.Drawing.Point(43, 158);
            this.grbRedScore.Name = "grbRedScore";
            this.grbRedScore.Padding = new System.Windows.Forms.Padding(10);
            this.grbRedScore.Size = new System.Drawing.Size(315, 233);
            this.grbRedScore.TabIndex = 155;
            this.grbRedScore.TabStop = false;
            // 
            // lblRedTeam
            // 
            this.lblRedTeam.AutoSize = true;
            this.lblRedTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedTeam.ForeColor = System.Drawing.Color.Red;
            this.lblRedTeam.Location = new System.Drawing.Point(43, 21);
            this.lblRedTeam.MinimumSize = new System.Drawing.Size(250, 0);
            this.lblRedTeam.Name = "lblRedTeam";
            this.lblRedTeam.Size = new System.Drawing.Size(250, 33);
            this.lblRedTeam.TabIndex = 66;
            this.lblRedTeam.Text = "Red Team";
            this.lblRedTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRedScore
            // 
            this.lblRedScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRedScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 80F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedScore.ForeColor = System.Drawing.Color.Red;
            this.lblRedScore.Location = new System.Drawing.Point(31, 71);
            this.lblRedScore.Name = "lblRedScore";
            this.lblRedScore.Size = new System.Drawing.Size(250, 140);
            this.lblRedScore.TabIndex = 0;
            this.lblRedScore.Text = "000";
            this.lblRedScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRedPenalty1
            // 
            this.lblRedPenalty1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRedPenalty1.AutoSize = true;
            this.lblRedPenalty1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedPenalty1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPenalty1.ForeColor = System.Drawing.SystemColors.Control;
            this.lblRedPenalty1.Location = new System.Drawing.Point(20, 37);
            this.lblRedPenalty1.MinimumSize = new System.Drawing.Size(50, 25);
            this.lblRedPenalty1.Name = "lblRedPenalty1";
            this.lblRedPenalty1.Padding = new System.Windows.Forms.Padding(5);
            this.lblRedPenalty1.Size = new System.Drawing.Size(111, 37);
            this.lblRedPenalty1.TabIndex = 141;
            this.lblRedPenalty1.Text = "- 10 pts ";
            this.lblRedPenalty1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grbRedPenalty
            // 
            this.grbRedPenalty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbRedPenalty.Controls.Add(this.lblRedDQ);
            this.grbRedPenalty.Controls.Add(this.lblRedPenalty1);
            this.grbRedPenalty.Controls.Add(this.lblRedPenalty2);
            this.grbRedPenalty.Controls.Add(this.lblRedPenalty3);
            this.grbRedPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbRedPenalty.Location = new System.Drawing.Point(43, 428);
            this.grbRedPenalty.Name = "grbRedPenalty";
            this.grbRedPenalty.Padding = new System.Windows.Forms.Padding(5);
            this.grbRedPenalty.Size = new System.Drawing.Size(151, 316);
            this.grbRedPenalty.TabIndex = 159;
            this.grbRedPenalty.TabStop = false;
            this.grbRedPenalty.Text = "Red Penalties";
            // 
            // GameDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.YBOTLogo);
            this.Controls.Add(this.lblMatchNumber);
            this.Controls.Add(this.grbGreenScore);
            this.Controls.Add(this.grbFieldDisplay);
            this.Controls.Add(this.grbGreenPenalty);
            this.Controls.Add(this.grbRedScore);
            this.Controls.Add(this.grbRedPenalty);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "GameDisplay";
            this.Text = "GameDisplay";
            ((System.ComponentModel.ISupportInitialize)(this.YBOTLogo)).EndInit();
            this.grbGreenScore.ResumeLayout(false);
            this.grbGreenScore.PerformLayout();
            this.grbFieldDisplay.ResumeLayout(false);
            this.grbFieldDisplay.PerformLayout();
            this.grbGreenPenalty.ResumeLayout(false);
            this.grbGreenPenalty.PerformLayout();
            this.grbRedScore.ResumeLayout(false);
            this.grbRedScore.PerformLayout();
            this.grbRedPenalty.ResumeLayout(false);
            this.grbRedPenalty.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.PictureBox YBOTLogo;
        public System.Windows.Forms.Label lblMatchNumber;
        public System.Windows.Forms.GroupBox grbGreenScore;
        public System.Windows.Forms.Label lblGreenTeam;
        public System.Windows.Forms.Label lblGreenScore;
        public System.Windows.Forms.Label lblRedPenalty2;
        public System.Windows.Forms.Label lblRedPenalty3;
        public System.Windows.Forms.GroupBox grbFieldDisplay;
        public System.Windows.Forms.Label lblGameClock;
        public System.Windows.Forms.Label lblRedDQ;
        public System.Windows.Forms.GroupBox grbGreenPenalty;
        public System.Windows.Forms.Label lblGreenPenalty1;
        public System.Windows.Forms.Label lblGreenPenalty2;
        public System.Windows.Forms.Label lblGreenPenalty3;
        public System.Windows.Forms.Label lblGreenDQ;
        public System.Windows.Forms.GroupBox grbRedScore;
        public System.Windows.Forms.Label lblRedTeam;
        public System.Windows.Forms.Label lblRedScore;
        public System.Windows.Forms.Label lblRedPenalty1;
        public System.Windows.Forms.GroupBox grbRedPenalty;
    }
}