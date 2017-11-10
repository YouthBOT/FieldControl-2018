namespace YbotFieldControl
{
    partial class GameControl
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
            this.components = new System.ComponentModel.Container();
            this.btnGreenPlus = new System.Windows.Forms.Button();
            this.lblGreenPenalty1 = new System.Windows.Forms.Label();
            this.lblGreenPenalty2 = new System.Windows.Forms.Label();
            this.lblGreenPenalty3 = new System.Windows.Forms.Label();
            this.lblGreenDQ = new System.Windows.Forms.Label();
            this.lblRedDQ = new System.Windows.Forms.Label();
            this.lblRedPenalty1 = new System.Windows.Forms.Label();
            this.lblRedPenalty3 = new System.Windows.Forms.Label();
            this.lblRedPenalty2 = new System.Windows.Forms.Label();
            this.btnRedPlus = new System.Windows.Forms.Button();
            this.btnRedMinus = new System.Windows.Forms.Button();
            this.btnGameDisplay = new System.Windows.Forms.Button();
            this.lblGameClock = new System.Windows.Forms.Label();
            this.btnGreenMinus = new System.Windows.Forms.Button();
            this.lblGreenTeam = new System.Windows.Forms.Label();
            this.btnDisableGreen = new System.Windows.Forms.Button();
            this.btnDisableRed = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnMatchNext = new System.Windows.Forms.Button();
            this.btnMatchPrev = new System.Windows.Forms.Button();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnAutoMode = new System.Windows.Forms.Button();
            this.btnPracticeMode = new System.Windows.Forms.Button();
            this.grbGreenPenalty = new System.Windows.Forms.GroupBox();
            this.btnManualMode = new System.Windows.Forms.Button();
            this.btnScoreGame = new System.Windows.Forms.Button();
            this.lblMatchNumber = new System.Windows.Forms.Label();
            this.lblGreenScore = new System.Windows.Forms.Label();
            this.grbFieldDisplay = new System.Windows.Forms.GroupBox();
            this.grbRedPenalty = new System.Windows.Forms.GroupBox();
            this.lblRedTeam = new System.Windows.Forms.Label();
            this.lblRedScore = new System.Windows.Forms.Label();
            this.grbRedScore = new System.Windows.Forms.GroupBox();
            this.btnRedMantonomous = new System.Windows.Forms.Button();
            this.grbGameMode = new System.Windows.Forms.GroupBox();
            this.btnTestMode = new System.Windows.Forms.Button();
            this.grbGreenScore = new System.Windows.Forms.GroupBox();
            this.btnGreenMantonomous = new System.Windows.Forms.Button();
            this.dsScores = new System.Data.DataSet();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.practiceTimer = new System.Windows.Forms.Timer(this.components);
            this.testTimer = new System.Windows.Forms.Timer(this.components);
            this.btnTournamentNext = new System.Windows.Forms.Button();
            this.btnTournamentPrev = new System.Windows.Forms.Button();
            this.lblTournamentName = new System.Windows.Forms.Label();
            this.lblChampionshipRounds = new System.Windows.Forms.Label();
            this.btnChampionshipRoundNext = new System.Windows.Forms.Button();
            this.btnChampionshipRoundPrevious = new System.Windows.Forms.Button();
            this.btnSpeedMode = new System.Windows.Forms.Button();
            this.grbGreenPenalty.SuspendLayout();
            this.grbFieldDisplay.SuspendLayout();
            this.grbRedPenalty.SuspendLayout();
            this.grbRedScore.SuspendLayout();
            this.grbGameMode.SuspendLayout();
            this.grbGreenScore.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsScores)).BeginInit();
            this.SuspendLayout();
            // 
            // btnGreenPlus
            // 
            this.btnGreenPlus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGreenPlus.BackColor = System.Drawing.Color.Lime;
            this.btnGreenPlus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGreenPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreenPlus.ForeColor = System.Drawing.Color.Black;
            this.btnGreenPlus.Location = new System.Drawing.Point(93, 215);
            this.btnGreenPlus.Name = "btnGreenPlus";
            this.btnGreenPlus.Size = new System.Drawing.Size(103, 60);
            this.btnGreenPlus.TabIndex = 162;
            this.btnGreenPlus.Text = "+";
            this.btnGreenPlus.UseVisualStyleBackColor = false;
            this.btnGreenPlus.Visible = false;
            // 
            // lblGreenPenalty1
            // 
            this.lblGreenPenalty1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenPenalty1.AutoSize = true;
            this.lblGreenPenalty1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenPenalty1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenPenalty1.ForeColor = System.Drawing.Color.Lime;
            this.lblGreenPenalty1.Location = new System.Drawing.Point(6, 22);
            this.lblGreenPenalty1.MinimumSize = new System.Drawing.Size(50, 40);
            this.lblGreenPenalty1.Name = "lblGreenPenalty1";
            this.lblGreenPenalty1.Size = new System.Drawing.Size(87, 40);
            this.lblGreenPenalty1.TabIndex = 136;
            this.lblGreenPenalty1.Text = "- 200 pts ";
            this.lblGreenPenalty1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGreenPenalty1.Click += new System.EventHandler(this.lblGreenPenalty1_Click);
            // 
            // lblGreenPenalty2
            // 
            this.lblGreenPenalty2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenPenalty2.AutoSize = true;
            this.lblGreenPenalty2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenPenalty2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenPenalty2.ForeColor = System.Drawing.Color.Lime;
            this.lblGreenPenalty2.Location = new System.Drawing.Point(6, 83);
            this.lblGreenPenalty2.MinimumSize = new System.Drawing.Size(50, 40);
            this.lblGreenPenalty2.Name = "lblGreenPenalty2";
            this.lblGreenPenalty2.Size = new System.Drawing.Size(87, 40);
            this.lblGreenPenalty2.TabIndex = 137;
            this.lblGreenPenalty2.Text = "- 200 pts ";
            this.lblGreenPenalty2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGreenPenalty2.Click += new System.EventHandler(this.lblGreenPenalty2_Click);
            // 
            // lblGreenPenalty3
            // 
            this.lblGreenPenalty3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenPenalty3.AutoSize = true;
            this.lblGreenPenalty3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenPenalty3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenPenalty3.ForeColor = System.Drawing.Color.Lime;
            this.lblGreenPenalty3.Location = new System.Drawing.Point(6, 147);
            this.lblGreenPenalty3.MinimumSize = new System.Drawing.Size(50, 40);
            this.lblGreenPenalty3.Name = "lblGreenPenalty3";
            this.lblGreenPenalty3.Size = new System.Drawing.Size(87, 40);
            this.lblGreenPenalty3.TabIndex = 138;
            this.lblGreenPenalty3.Text = "- 200 pts ";
            this.lblGreenPenalty3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGreenPenalty3.Click += new System.EventHandler(this.lblGreenPenalty3_Click);
            // 
            // lblGreenDQ
            // 
            this.lblGreenDQ.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenDQ.AutoSize = true;
            this.lblGreenDQ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenDQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenDQ.ForeColor = System.Drawing.Color.Lime;
            this.lblGreenDQ.Location = new System.Drawing.Point(30, 200);
            this.lblGreenDQ.MinimumSize = new System.Drawing.Size(50, 50);
            this.lblGreenDQ.Name = "lblGreenDQ";
            this.lblGreenDQ.Size = new System.Drawing.Size(50, 50);
            this.lblGreenDQ.TabIndex = 139;
            this.lblGreenDQ.Text = "DQ";
            this.lblGreenDQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGreenDQ.Click += new System.EventHandler(this.lblGreenDQ_Click);
            // 
            // lblRedDQ
            // 
            this.lblRedDQ.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRedDQ.AutoSize = true;
            this.lblRedDQ.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedDQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedDQ.ForeColor = System.Drawing.Color.Red;
            this.lblRedDQ.Location = new System.Drawing.Point(26, 200);
            this.lblRedDQ.MinimumSize = new System.Drawing.Size(50, 50);
            this.lblRedDQ.Name = "lblRedDQ";
            this.lblRedDQ.Size = new System.Drawing.Size(50, 50);
            this.lblRedDQ.TabIndex = 134;
            this.lblRedDQ.Text = "DQ";
            this.lblRedDQ.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRedDQ.Click += new System.EventHandler(this.lblRedDQ_Click);
            // 
            // lblRedPenalty1
            // 
            this.lblRedPenalty1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRedPenalty1.AutoSize = true;
            this.lblRedPenalty1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedPenalty1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPenalty1.ForeColor = System.Drawing.Color.Red;
            this.lblRedPenalty1.Location = new System.Drawing.Point(10, 22);
            this.lblRedPenalty1.MinimumSize = new System.Drawing.Size(50, 40);
            this.lblRedPenalty1.Name = "lblRedPenalty1";
            this.lblRedPenalty1.Size = new System.Drawing.Size(87, 40);
            this.lblRedPenalty1.TabIndex = 131;
            this.lblRedPenalty1.Text = "- 200 pts ";
            this.lblRedPenalty1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRedPenalty1.Click += new System.EventHandler(this.lblRedPenalty1_Click);
            // 
            // lblRedPenalty3
            // 
            this.lblRedPenalty3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRedPenalty3.AutoSize = true;
            this.lblRedPenalty3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedPenalty3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPenalty3.ForeColor = System.Drawing.Color.Red;
            this.lblRedPenalty3.Location = new System.Drawing.Point(10, 147);
            this.lblRedPenalty3.MinimumSize = new System.Drawing.Size(50, 40);
            this.lblRedPenalty3.Name = "lblRedPenalty3";
            this.lblRedPenalty3.Size = new System.Drawing.Size(87, 40);
            this.lblRedPenalty3.TabIndex = 133;
            this.lblRedPenalty3.Text = "- 200 pts ";
            this.lblRedPenalty3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRedPenalty3.Click += new System.EventHandler(this.lblRedPenalty3_Click);
            // 
            // lblRedPenalty2
            // 
            this.lblRedPenalty2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblRedPenalty2.AutoSize = true;
            this.lblRedPenalty2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedPenalty2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPenalty2.ForeColor = System.Drawing.Color.Red;
            this.lblRedPenalty2.Location = new System.Drawing.Point(10, 83);
            this.lblRedPenalty2.MinimumSize = new System.Drawing.Size(50, 40);
            this.lblRedPenalty2.Name = "lblRedPenalty2";
            this.lblRedPenalty2.Size = new System.Drawing.Size(87, 40);
            this.lblRedPenalty2.TabIndex = 132;
            this.lblRedPenalty2.Text = "- 200 pts ";
            this.lblRedPenalty2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRedPenalty2.Click += new System.EventHandler(this.lblRedPenalty2_Click);
            // 
            // btnRedPlus
            // 
            this.btnRedPlus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRedPlus.BackColor = System.Drawing.Color.Red;
            this.btnRedPlus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRedPlus.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedPlus.ForeColor = System.Drawing.Color.Black;
            this.btnRedPlus.Location = new System.Drawing.Point(1006, 215);
            this.btnRedPlus.Name = "btnRedPlus";
            this.btnRedPlus.Size = new System.Drawing.Size(103, 60);
            this.btnRedPlus.TabIndex = 160;
            this.btnRedPlus.Text = "+";
            this.btnRedPlus.UseVisualStyleBackColor = false;
            this.btnRedPlus.Visible = false;
            // 
            // btnRedMinus
            // 
            this.btnRedMinus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRedMinus.BackColor = System.Drawing.Color.Red;
            this.btnRedMinus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRedMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedMinus.ForeColor = System.Drawing.Color.Black;
            this.btnRedMinus.Location = new System.Drawing.Point(1006, 295);
            this.btnRedMinus.Name = "btnRedMinus";
            this.btnRedMinus.Size = new System.Drawing.Size(103, 60);
            this.btnRedMinus.TabIndex = 159;
            this.btnRedMinus.Text = "-";
            this.btnRedMinus.UseVisualStyleBackColor = false;
            this.btnRedMinus.Visible = false;
            // 
            // btnGameDisplay
            // 
            this.btnGameDisplay.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGameDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGameDisplay.Location = new System.Drawing.Point(279, 17);
            this.btnGameDisplay.Margin = new System.Windows.Forms.Padding(10);
            this.btnGameDisplay.Name = "btnGameDisplay";
            this.btnGameDisplay.Size = new System.Drawing.Size(100, 75);
            this.btnGameDisplay.TabIndex = 128;
            this.btnGameDisplay.Text = "Game Display";
            this.btnGameDisplay.UseVisualStyleBackColor = true;
            this.btnGameDisplay.Click += new System.EventHandler(this.btnGameDisplay_Click);
            // 
            // lblGameClock
            // 
            this.lblGameClock.AutoSize = true;
            this.lblGameClock.Font = new System.Drawing.Font("Microsoft Sans Serif", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGameClock.Location = new System.Drawing.Point(260, 64);
            this.lblGameClock.Name = "lblGameClock";
            this.lblGameClock.Size = new System.Drawing.Size(235, 108);
            this.lblGameClock.TabIndex = 9;
            this.lblGameClock.Text = "0:00";
            this.lblGameClock.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGameClock.Click += new System.EventHandler(this.lblGameClock_Click);
            // 
            // btnGreenMinus
            // 
            this.btnGreenMinus.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGreenMinus.BackColor = System.Drawing.Color.Lime;
            this.btnGreenMinus.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGreenMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreenMinus.ForeColor = System.Drawing.Color.Black;
            this.btnGreenMinus.Location = new System.Drawing.Point(93, 295);
            this.btnGreenMinus.Name = "btnGreenMinus";
            this.btnGreenMinus.Size = new System.Drawing.Size(103, 60);
            this.btnGreenMinus.TabIndex = 161;
            this.btnGreenMinus.Text = "-";
            this.btnGreenMinus.UseVisualStyleBackColor = false;
            this.btnGreenMinus.Visible = false;
            // 
            // lblGreenTeam
            // 
            this.lblGreenTeam.AutoSize = true;
            this.lblGreenTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenTeam.ForeColor = System.Drawing.Color.Lime;
            this.lblGreenTeam.Location = new System.Drawing.Point(17, 21);
            this.lblGreenTeam.MinimumSize = new System.Drawing.Size(185, 0);
            this.lblGreenTeam.Name = "lblGreenTeam";
            this.lblGreenTeam.Size = new System.Drawing.Size(185, 20);
            this.lblGreenTeam.TabIndex = 67;
            this.lblGreenTeam.Text = "Green Team";
            this.lblGreenTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnDisableGreen
            // 
            this.btnDisableGreen.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDisableGreen.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDisableGreen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisableGreen.ForeColor = System.Drawing.Color.Lime;
            this.btnDisableGreen.Location = new System.Drawing.Point(93, 130);
            this.btnDisableGreen.Name = "btnDisableGreen";
            this.btnDisableGreen.Size = new System.Drawing.Size(103, 60);
            this.btnDisableGreen.TabIndex = 153;
            this.btnDisableGreen.Text = "Disable Green";
            this.btnDisableGreen.UseVisualStyleBackColor = true;
            this.btnDisableGreen.Click += new System.EventHandler(this.btnDisableGreen_Click);
            // 
            // btnDisableRed
            // 
            this.btnDisableRed.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDisableRed.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnDisableRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDisableRed.ForeColor = System.Drawing.Color.Red;
            this.btnDisableRed.Location = new System.Drawing.Point(1006, 130);
            this.btnDisableRed.Name = "btnDisableRed";
            this.btnDisableRed.Size = new System.Drawing.Size(103, 60);
            this.btnDisableRed.TabIndex = 152;
            this.btnDisableRed.Text = "Disable Red";
            this.btnDisableRed.UseVisualStyleBackColor = true;
            this.btnDisableRed.Click += new System.EventHandler(this.btnDisableRed_Click);
            // 
            // btnStop
            // 
            this.btnStop.BackColor = System.Drawing.Color.Red;
            this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStop.Location = new System.Drawing.Point(13, 18);
            this.btnStop.Margin = new System.Windows.Forms.Padding(10);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 75);
            this.btnStop.TabIndex = 65;
            this.btnStop.Text = "STOP";
            this.btnStop.UseVisualStyleBackColor = false;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnMatchNext
            // 
            this.btnMatchNext.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnMatchNext.Location = new System.Drawing.Point(713, 132);
            this.btnMatchNext.Name = "btnMatchNext";
            this.btnMatchNext.Size = new System.Drawing.Size(41, 33);
            this.btnMatchNext.TabIndex = 156;
            this.btnMatchNext.Text = ">>";
            this.btnMatchNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnMatchNext.UseVisualStyleBackColor = true;
            this.btnMatchNext.Click += new System.EventHandler(this.btnMatchNext_Click);
            // 
            // btnMatchPrev
            // 
            this.btnMatchPrev.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnMatchPrev.Location = new System.Drawing.Point(440, 132);
            this.btnMatchPrev.Name = "btnMatchPrev";
            this.btnMatchPrev.Size = new System.Drawing.Size(41, 33);
            this.btnMatchPrev.TabIndex = 155;
            this.btnMatchPrev.Text = "<<";
            this.btnMatchPrev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMatchPrev.UseVisualStyleBackColor = true;
            this.btnMatchPrev.Click += new System.EventHandler(this.btnMatchPrev_Click);
            // 
            // btnStartGame
            // 
            this.btnStartGame.BackColor = System.Drawing.SystemColors.Control;
            this.btnStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStartGame.Location = new System.Drawing.Point(128, 17);
            this.btnStartGame.Margin = new System.Windows.Forms.Padding(5);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(100, 75);
            this.btnStartGame.TabIndex = 60;
            this.btnStartGame.Text = "START GAME";
            this.btnStartGame.UseVisualStyleBackColor = false;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnAutoMode
            // 
            this.btnAutoMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAutoMode.Location = new System.Drawing.Point(546, 17);
            this.btnAutoMode.Margin = new System.Windows.Forms.Padding(10);
            this.btnAutoMode.Name = "btnAutoMode";
            this.btnAutoMode.Size = new System.Drawing.Size(100, 75);
            this.btnAutoMode.TabIndex = 144;
            this.btnAutoMode.Text = "Auto Mode";
            this.btnAutoMode.UseVisualStyleBackColor = true;
            this.btnAutoMode.Click += new System.EventHandler(this.btnAutoMode_Click);
            // 
            // btnPracticeMode
            // 
            this.btnPracticeMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPracticeMode.Location = new System.Drawing.Point(906, 17);
            this.btnPracticeMode.Margin = new System.Windows.Forms.Padding(10);
            this.btnPracticeMode.Name = "btnPracticeMode";
            this.btnPracticeMode.Size = new System.Drawing.Size(100, 75);
            this.btnPracticeMode.TabIndex = 64;
            this.btnPracticeMode.Text = "Debug Mode";
            this.btnPracticeMode.UseVisualStyleBackColor = true;
            this.btnPracticeMode.Click += new System.EventHandler(this.btnPracticeMode_Click);
            // 
            // grbGreenPenalty
            // 
            this.grbGreenPenalty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbGreenPenalty.Controls.Add(this.lblGreenPenalty1);
            this.grbGreenPenalty.Controls.Add(this.lblGreenPenalty2);
            this.grbGreenPenalty.Controls.Add(this.lblGreenPenalty3);
            this.grbGreenPenalty.Controls.Add(this.lblGreenDQ);
            this.grbGreenPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbGreenPenalty.Location = new System.Drawing.Point(93, 368);
            this.grbGreenPenalty.Name = "grbGreenPenalty";
            this.grbGreenPenalty.Size = new System.Drawing.Size(103, 261);
            this.grbGreenPenalty.TabIndex = 158;
            this.grbGreenPenalty.TabStop = false;
            this.grbGreenPenalty.Text = "Green Penalties";
            // 
            // btnManualMode
            // 
            this.btnManualMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnManualMode.Location = new System.Drawing.Point(666, 17);
            this.btnManualMode.Margin = new System.Windows.Forms.Padding(10);
            this.btnManualMode.Name = "btnManualMode";
            this.btnManualMode.Size = new System.Drawing.Size(100, 75);
            this.btnManualMode.TabIndex = 145;
            this.btnManualMode.Text = "Manual Mode";
            this.btnManualMode.UseVisualStyleBackColor = true;
            this.btnManualMode.Click += new System.EventHandler(this.btnManualMode_Click);
            // 
            // btnScoreGame
            // 
            this.btnScoreGame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnScoreGame.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnScoreGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnScoreGame.Location = new System.Drawing.Point(399, 17);
            this.btnScoreGame.Margin = new System.Windows.Forms.Padding(10);
            this.btnScoreGame.Name = "btnScoreGame";
            this.btnScoreGame.Size = new System.Drawing.Size(100, 75);
            this.btnScoreGame.TabIndex = 122;
            this.btnScoreGame.Text = "Score Game";
            this.btnScoreGame.UseVisualStyleBackColor = true;
            this.btnScoreGame.Click += new System.EventHandler(this.btnScoreGame_Click);
            // 
            // lblMatchNumber
            // 
            this.lblMatchNumber.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMatchNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblMatchNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatchNumber.Location = new System.Drawing.Point(483, 132);
            this.lblMatchNumber.MinimumSize = new System.Drawing.Size(200, 0);
            this.lblMatchNumber.Name = "lblMatchNumber";
            this.lblMatchNumber.Size = new System.Drawing.Size(228, 33);
            this.lblMatchNumber.TabIndex = 154;
            this.lblMatchNumber.Text = "Match 00";
            this.lblMatchNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGreenScore
            // 
            this.lblGreenScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblGreenScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblGreenScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenScore.ForeColor = System.Drawing.Color.Lime;
            this.lblGreenScore.Location = new System.Drawing.Point(17, 50);
            this.lblGreenScore.MaximumSize = new System.Drawing.Size(0, 75);
            this.lblGreenScore.MinimumSize = new System.Drawing.Size(185, 75);
            this.lblGreenScore.Name = "lblGreenScore";
            this.lblGreenScore.Size = new System.Drawing.Size(185, 75);
            this.lblGreenScore.TabIndex = 65;
            this.lblGreenScore.Text = "000";
            this.lblGreenScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblGreenScore.Click += new System.EventHandler(this.lblGreenScore_Click);
            // 
            // grbFieldDisplay
            // 
            this.grbFieldDisplay.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbFieldDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.grbFieldDisplay.Controls.Add(this.lblGameClock);
            this.grbFieldDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbFieldDisplay.Location = new System.Drawing.Point(216, 432);
            this.grbFieldDisplay.Name = "grbFieldDisplay";
            this.grbFieldDisplay.Size = new System.Drawing.Size(761, 200);
            this.grbFieldDisplay.TabIndex = 151;
            this.grbFieldDisplay.TabStop = false;
            // 
            // grbRedPenalty
            // 
            this.grbRedPenalty.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbRedPenalty.Controls.Add(this.lblRedDQ);
            this.grbRedPenalty.Controls.Add(this.lblRedPenalty1);
            this.grbRedPenalty.Controls.Add(this.lblRedPenalty3);
            this.grbRedPenalty.Controls.Add(this.lblRedPenalty2);
            this.grbRedPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbRedPenalty.Location = new System.Drawing.Point(1006, 368);
            this.grbRedPenalty.Name = "grbRedPenalty";
            this.grbRedPenalty.Size = new System.Drawing.Size(103, 261);
            this.grbRedPenalty.TabIndex = 157;
            this.grbRedPenalty.TabStop = false;
            this.grbRedPenalty.Text = "Red Penalties";
            // 
            // lblRedTeam
            // 
            this.lblRedTeam.AutoSize = true;
            this.lblRedTeam.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedTeam.ForeColor = System.Drawing.Color.Red;
            this.lblRedTeam.Location = new System.Drawing.Point(17, 21);
            this.lblRedTeam.MinimumSize = new System.Drawing.Size(185, 0);
            this.lblRedTeam.Name = "lblRedTeam";
            this.lblRedTeam.Size = new System.Drawing.Size(185, 20);
            this.lblRedTeam.TabIndex = 66;
            this.lblRedTeam.Text = "Red Team";
            this.lblRedTeam.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRedScore
            // 
            this.lblRedScore.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblRedScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedScore.ForeColor = System.Drawing.Color.Red;
            this.lblRedScore.Location = new System.Drawing.Point(13, 50);
            this.lblRedScore.Name = "lblRedScore";
            this.lblRedScore.Size = new System.Drawing.Size(185, 75);
            this.lblRedScore.TabIndex = 0;
            this.lblRedScore.Text = "000";
            this.lblRedScore.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblRedScore.Click += new System.EventHandler(this.lblRedScore_Click);
            // 
            // grbRedScore
            // 
            this.grbRedScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbRedScore.Controls.Add(this.btnRedMantonomous);
            this.grbRedScore.Controls.Add(this.lblRedTeam);
            this.grbRedScore.Controls.Add(this.lblRedScore);
            this.grbRedScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbRedScore.Location = new System.Drawing.Point(760, 122);
            this.grbRedScore.Name = "grbRedScore";
            this.grbRedScore.Padding = new System.Windows.Forms.Padding(10);
            this.grbRedScore.Size = new System.Drawing.Size(217, 233);
            this.grbRedScore.TabIndex = 149;
            this.grbRedScore.TabStop = false;
            // 
            // btnRedMantonomous
            // 
            this.btnRedMantonomous.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnRedMantonomous.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnRedMantonomous.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRedMantonomous.ForeColor = System.Drawing.Color.Red;
            this.btnRedMantonomous.Location = new System.Drawing.Point(58, 160);
            this.btnRedMantonomous.Name = "btnRedMantonomous";
            this.btnRedMantonomous.Size = new System.Drawing.Size(100, 60);
            this.btnRedMantonomous.TabIndex = 153;
            this.btnRedMantonomous.Text = "Mantonomous";
            this.btnRedMantonomous.UseVisualStyleBackColor = true;
            this.btnRedMantonomous.Click += new System.EventHandler(this.btnRedMantonomous_Click);
            // 
            // grbGameMode
            // 
            this.grbGameMode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbGameMode.Controls.Add(this.btnSpeedMode);
            this.grbGameMode.Controls.Add(this.btnTestMode);
            this.grbGameMode.Controls.Add(this.btnGameDisplay);
            this.grbGameMode.Controls.Add(this.btnStartGame);
            this.grbGameMode.Controls.Add(this.btnManualMode);
            this.grbGameMode.Controls.Add(this.btnStop);
            this.grbGameMode.Controls.Add(this.btnScoreGame);
            this.grbGameMode.Controls.Add(this.btnAutoMode);
            this.grbGameMode.Controls.Add(this.btnPracticeMode);
            this.grbGameMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbGameMode.Location = new System.Drawing.Point(19, 4);
            this.grbGameMode.Margin = new System.Windows.Forms.Padding(10);
            this.grbGameMode.Name = "grbGameMode";
            this.grbGameMode.Size = new System.Drawing.Size(1139, 105);
            this.grbGameMode.TabIndex = 148;
            this.grbGameMode.TabStop = false;
            this.grbGameMode.Text = "Game Modes";
            // 
            // btnTestMode
            // 
            this.btnTestMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTestMode.Location = new System.Drawing.Point(1026, 17);
            this.btnTestMode.Margin = new System.Windows.Forms.Padding(10);
            this.btnTestMode.Name = "btnTestMode";
            this.btnTestMode.Size = new System.Drawing.Size(100, 75);
            this.btnTestMode.TabIndex = 146;
            this.btnTestMode.Text = "Test Mode";
            this.btnTestMode.UseVisualStyleBackColor = true;
            this.btnTestMode.Click += new System.EventHandler(this.btnTestMode_Click);
            // 
            // grbGreenScore
            // 
            this.grbGreenScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.grbGreenScore.Controls.Add(this.btnGreenMantonomous);
            this.grbGreenScore.Controls.Add(this.lblGreenTeam);
            this.grbGreenScore.Controls.Add(this.lblGreenScore);
            this.grbGreenScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grbGreenScore.Location = new System.Drawing.Point(216, 122);
            this.grbGreenScore.Name = "grbGreenScore";
            this.grbGreenScore.Padding = new System.Windows.Forms.Padding(10);
            this.grbGreenScore.Size = new System.Drawing.Size(218, 233);
            this.grbGreenScore.TabIndex = 150;
            this.grbGreenScore.TabStop = false;
            // 
            // btnGreenMantonomous
            // 
            this.btnGreenMantonomous.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnGreenMantonomous.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnGreenMantonomous.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGreenMantonomous.ForeColor = System.Drawing.Color.Lime;
            this.btnGreenMantonomous.Location = new System.Drawing.Point(57, 160);
            this.btnGreenMantonomous.Name = "btnGreenMantonomous";
            this.btnGreenMantonomous.Size = new System.Drawing.Size(100, 60);
            this.btnGreenMantonomous.TabIndex = 154;
            this.btnGreenMantonomous.Text = "Mantonomous";
            this.btnGreenMantonomous.UseVisualStyleBackColor = true;
            this.btnGreenMantonomous.Click += new System.EventHandler(this.btnGreenMantonomous_Click);
            // 
            // dsScores
            // 
            this.dsScores.DataSetName = "dsScores";
            // 
            // gameTimer
            // 
            this.gameTimer.Interval = 200;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimer_Tick);
            // 
            // practiceTimer
            // 
            this.practiceTimer.Interval = 200;
            this.practiceTimer.Tick += new System.EventHandler(this.practiceTimer_Tick);
            // 
            // testTimer
            // 
            this.testTimer.Tick += new System.EventHandler(this.testTimer_Tick);
            // 
            // btnTournamentNext
            // 
            this.btnTournamentNext.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTournamentNext.Location = new System.Drawing.Point(713, 172);
            this.btnTournamentNext.Name = "btnTournamentNext";
            this.btnTournamentNext.Size = new System.Drawing.Size(41, 33);
            this.btnTournamentNext.TabIndex = 165;
            this.btnTournamentNext.Text = ">>";
            this.btnTournamentNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTournamentNext.UseVisualStyleBackColor = true;
            this.btnTournamentNext.Visible = false;
            this.btnTournamentNext.Click += new System.EventHandler(this.btnTournamentNext_Click);
            // 
            // btnTournamentPrev
            // 
            this.btnTournamentPrev.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnTournamentPrev.Location = new System.Drawing.Point(440, 172);
            this.btnTournamentPrev.Name = "btnTournamentPrev";
            this.btnTournamentPrev.Size = new System.Drawing.Size(41, 33);
            this.btnTournamentPrev.TabIndex = 164;
            this.btnTournamentPrev.Text = "<<";
            this.btnTournamentPrev.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTournamentPrev.UseVisualStyleBackColor = true;
            this.btnTournamentPrev.Visible = false;
            this.btnTournamentPrev.Click += new System.EventHandler(this.btnTournamentPrev_Click);
            // 
            // lblTournamentName
            // 
            this.lblTournamentName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTournamentName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTournamentName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTournamentName.Location = new System.Drawing.Point(483, 172);
            this.lblTournamentName.MinimumSize = new System.Drawing.Size(200, 0);
            this.lblTournamentName.Name = "lblTournamentName";
            this.lblTournamentName.Size = new System.Drawing.Size(228, 33);
            this.lblTournamentName.TabIndex = 163;
            this.lblTournamentName.Text = "Field Testing";
            this.lblTournamentName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblTournamentName.Visible = false;
            // 
            // lblChampionshipRounds
            // 
            this.lblChampionshipRounds.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblChampionshipRounds.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblChampionshipRounds.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChampionshipRounds.Location = new System.Drawing.Point(483, 212);
            this.lblChampionshipRounds.MinimumSize = new System.Drawing.Size(200, 0);
            this.lblChampionshipRounds.Name = "lblChampionshipRounds";
            this.lblChampionshipRounds.Size = new System.Drawing.Size(228, 33);
            this.lblChampionshipRounds.TabIndex = 167;
            this.lblChampionshipRounds.Text = "Practice";
            this.lblChampionshipRounds.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblChampionshipRounds.Visible = false;
            // 
            // btnChampionshipRoundNext
            // 
            this.btnChampionshipRoundNext.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnChampionshipRoundNext.Location = new System.Drawing.Point(713, 212);
            this.btnChampionshipRoundNext.Name = "btnChampionshipRoundNext";
            this.btnChampionshipRoundNext.Size = new System.Drawing.Size(41, 33);
            this.btnChampionshipRoundNext.TabIndex = 169;
            this.btnChampionshipRoundNext.Text = ">>";
            this.btnChampionshipRoundNext.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnChampionshipRoundNext.UseVisualStyleBackColor = true;
            this.btnChampionshipRoundNext.Visible = false;
            this.btnChampionshipRoundNext.Click += new System.EventHandler(this.btnChampionshipRound_Click);
            // 
            // btnChampionshipRoundPrevious
            // 
            this.btnChampionshipRoundPrevious.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnChampionshipRoundPrevious.Location = new System.Drawing.Point(440, 212);
            this.btnChampionshipRoundPrevious.Name = "btnChampionshipRoundPrevious";
            this.btnChampionshipRoundPrevious.Size = new System.Drawing.Size(41, 33);
            this.btnChampionshipRoundPrevious.TabIndex = 168;
            this.btnChampionshipRoundPrevious.Text = "<<";
            this.btnChampionshipRoundPrevious.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChampionshipRoundPrevious.UseVisualStyleBackColor = true;
            this.btnChampionshipRoundPrevious.Visible = false;
            this.btnChampionshipRoundPrevious.Click += new System.EventHandler(this.btnChampionshipRound_Click);
            // 
            // btnSpeedMode
            // 
            this.btnSpeedMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpeedMode.Location = new System.Drawing.Point(786, 17);
            this.btnSpeedMode.Margin = new System.Windows.Forms.Padding(10);
            this.btnSpeedMode.Name = "btnSpeedMode";
            this.btnSpeedMode.Size = new System.Drawing.Size(100, 75);
            this.btnSpeedMode.TabIndex = 147;
            this.btnSpeedMode.Text = "Speed Mode";
            this.btnSpeedMode.UseVisualStyleBackColor = true;
            this.btnSpeedMode.Click += new System.EventHandler(this.btnSpeedMode_Click);
            // 
            // GameControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1177, 637);
            this.Controls.Add(this.btnChampionshipRoundNext);
            this.Controls.Add(this.btnChampionshipRoundPrevious);
            this.Controls.Add(this.lblChampionshipRounds);
            this.Controls.Add(this.btnTournamentNext);
            this.Controls.Add(this.btnTournamentPrev);
            this.Controls.Add(this.lblTournamentName);
            this.Controls.Add(this.btnGreenPlus);
            this.Controls.Add(this.btnRedPlus);
            this.Controls.Add(this.btnRedMinus);
            this.Controls.Add(this.btnGreenMinus);
            this.Controls.Add(this.btnDisableGreen);
            this.Controls.Add(this.btnDisableRed);
            this.Controls.Add(this.btnMatchNext);
            this.Controls.Add(this.btnMatchPrev);
            this.Controls.Add(this.grbGreenPenalty);
            this.Controls.Add(this.lblMatchNumber);
            this.Controls.Add(this.grbFieldDisplay);
            this.Controls.Add(this.grbRedPenalty);
            this.Controls.Add(this.grbRedScore);
            this.Controls.Add(this.grbGameMode);
            this.Controls.Add(this.grbGreenScore);
            this.Name = "GameControl";
            this.Text = "Game Control";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GameControl_FormClosed);
            this.grbGreenPenalty.ResumeLayout(false);
            this.grbGreenPenalty.PerformLayout();
            this.grbFieldDisplay.ResumeLayout(false);
            this.grbFieldDisplay.PerformLayout();
            this.grbRedPenalty.ResumeLayout(false);
            this.grbRedPenalty.PerformLayout();
            this.grbRedScore.ResumeLayout(false);
            this.grbRedScore.PerformLayout();
            this.grbGameMode.ResumeLayout(false);
            this.grbGreenScore.ResumeLayout(false);
            this.grbGreenScore.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dsScores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnGreenPlus;
        public System.Windows.Forms.Label lblGreenPenalty1;
        public System.Windows.Forms.Label lblGreenPenalty2;
        public System.Windows.Forms.Label lblGreenPenalty3;
        public System.Windows.Forms.Label lblGreenDQ;
        public System.Windows.Forms.Label lblRedDQ;
        public System.Windows.Forms.Label lblRedPenalty1;
        public System.Windows.Forms.Label lblRedPenalty3;
        public System.Windows.Forms.Label lblRedPenalty2;
        public System.Windows.Forms.Button btnRedPlus;
        public System.Windows.Forms.Button btnRedMinus;
        public System.Windows.Forms.Button btnGameDisplay;
        public System.Windows.Forms.Label lblGameClock;
        public System.Windows.Forms.Button btnGreenMinus;
        private System.Windows.Forms.Label lblGreenTeam;
        public System.Windows.Forms.Button btnDisableGreen;
        public System.Windows.Forms.Button btnDisableRed;
        public System.Windows.Forms.Button btnStop;
        public System.Windows.Forms.Button btnMatchNext;
        public System.Windows.Forms.Button btnMatchPrev;
        public System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnAutoMode;
        public System.Windows.Forms.Button btnPracticeMode;
        public System.Windows.Forms.GroupBox grbGreenPenalty;
        private System.Windows.Forms.Button btnManualMode;
        public System.Windows.Forms.Button btnScoreGame;
        public System.Windows.Forms.Label lblMatchNumber;
        public System.Windows.Forms.Label lblGreenScore;
        public System.Windows.Forms.GroupBox grbFieldDisplay;
        public System.Windows.Forms.GroupBox grbRedPenalty;
        private System.Windows.Forms.Label lblRedTeam;
        public System.Windows.Forms.Label lblRedScore;
        public System.Windows.Forms.GroupBox grbRedScore;
        public System.Windows.Forms.GroupBox grbGameMode;
        public System.Windows.Forms.GroupBox grbGreenScore;
        public System.Windows.Forms.Button btnRedMantonomous;
        public System.Windows.Forms.Button btnGreenMantonomous;
        private System.Data.DataSet dsScores;
        private System.Windows.Forms.Timer gameTimer;
        private System.Windows.Forms.Timer practiceTimer;
        public System.Windows.Forms.Button btnTestMode;
        private System.Windows.Forms.Timer testTimer;
        public System.Windows.Forms.Button btnTournamentNext;
        public System.Windows.Forms.Button btnTournamentPrev;
        public System.Windows.Forms.Label lblTournamentName;
        public System.Windows.Forms.Label lblChampionshipRounds;
        public System.Windows.Forms.Button btnChampionshipRoundNext;
        public System.Windows.Forms.Button btnChampionshipRoundPrevious;
        private System.Windows.Forms.Button btnSpeedMode;
    }
}