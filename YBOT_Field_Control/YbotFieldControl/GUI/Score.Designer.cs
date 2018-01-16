namespace YbotFieldControl
{
    partial class Score
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
            this.btnOverride = new System.Windows.Forms.Button();
            this.btnUpdateScore = new System.Windows.Forms.Button();
            this.grRedFinalScore = new System.Windows.Forms.GroupBox();
            this.tbRedScore = new System.Windows.Forms.TextBox();
            this.lbRedPushScore = new System.Windows.Forms.Label();
            this.lbRedPushPointValue = new System.Windows.Forms.Label();
            this.tbRedPushes = new System.Windows.Forms.TextBox();
            this.lbRedPushes = new System.Windows.Forms.Label();
            this.cbRedDq = new System.Windows.Forms.CheckBox();
            this.lbRedScore = new System.Windows.Forms.Label();
            this.tbRedPenalty = new System.Windows.Forms.TextBox();
            this.lbRedPenaltyPointValue = new System.Windows.Forms.Label();
            this.lbRedPenaltyScore = new System.Windows.Forms.Label();
            this.lblRedDQ = new System.Windows.Forms.Label();
            this.lblRedPenalty = new System.Windows.Forms.Label();
            this.lblRedFinalScore = new System.Windows.Forms.Label();
            this.gbGreenFinalScore = new System.Windows.Forms.GroupBox();
            this.tbGreenScore = new System.Windows.Forms.TextBox();
            this.lbGreenPushScore = new System.Windows.Forms.Label();
            this.lbGreenPushPointValue = new System.Windows.Forms.Label();
            this.tbGreenPushes = new System.Windows.Forms.TextBox();
            this.lbGreenPushes = new System.Windows.Forms.Label();
            this.cbGreenDq = new System.Windows.Forms.CheckBox();
            this.lbGreenScore = new System.Windows.Forms.Label();
            this.tbGreenPenalty = new System.Windows.Forms.TextBox();
            this.lbGreenPenaltyPointValue = new System.Windows.Forms.Label();
            this.lbGreenPenaltyScore = new System.Windows.Forms.Label();
            this.lblGreenPenalty = new System.Windows.Forms.Label();
            this.lblGreenFinalScore = new System.Windows.Forms.Label();
            this.btnFinalScore = new System.Windows.Forms.Button();
            this.lblGreenDq = new System.Windows.Forms.Label();
            this.grRedFinalScore.SuspendLayout();
            this.gbGreenFinalScore.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOverride
            // 
            this.btnOverride.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOverride.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnOverride.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOverride.Location = new System.Drawing.Point(259, 701);
            this.btnOverride.Margin = new System.Windows.Forms.Padding(10);
            this.btnOverride.Name = "btnOverride";
            this.btnOverride.Size = new System.Drawing.Size(200, 40);
            this.btnOverride.TabIndex = 132;
            this.btnOverride.Text = "Manual Override";
            this.btnOverride.UseVisualStyleBackColor = true;
            this.btnOverride.Click += new System.EventHandler(this.btnOverride_Click);
            // 
            // btnUpdateScore
            // 
            this.btnUpdateScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnUpdateScore.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnUpdateScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdateScore.Location = new System.Drawing.Point(45, 701);
            this.btnUpdateScore.Margin = new System.Windows.Forms.Padding(10);
            this.btnUpdateScore.Name = "btnUpdateScore";
            this.btnUpdateScore.Size = new System.Drawing.Size(200, 40);
            this.btnUpdateScore.TabIndex = 131;
            this.btnUpdateScore.Text = "Update Score";
            this.btnUpdateScore.UseVisualStyleBackColor = true;
            this.btnUpdateScore.Click += new System.EventHandler(this.btnUpdateScore_Click);
            // 
            // grRedFinalScore
            // 
            this.grRedFinalScore.Controls.Add(this.tbRedScore);
            this.grRedFinalScore.Controls.Add(this.lbRedPushScore);
            this.grRedFinalScore.Controls.Add(this.lbRedPushPointValue);
            this.grRedFinalScore.Controls.Add(this.tbRedPushes);
            this.grRedFinalScore.Controls.Add(this.lbRedPushes);
            this.grRedFinalScore.Controls.Add(this.cbRedDq);
            this.grRedFinalScore.Controls.Add(this.lbRedScore);
            this.grRedFinalScore.Controls.Add(this.tbRedPenalty);
            this.grRedFinalScore.Controls.Add(this.lbRedPenaltyPointValue);
            this.grRedFinalScore.Controls.Add(this.lbRedPenaltyScore);
            this.grRedFinalScore.Controls.Add(this.lblRedDQ);
            this.grRedFinalScore.Controls.Add(this.lblRedPenalty);
            this.grRedFinalScore.Controls.Add(this.lblRedFinalScore);
            this.grRedFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grRedFinalScore.ForeColor = System.Drawing.Color.Maroon;
            this.grRedFinalScore.Location = new System.Drawing.Point(370, 12);
            this.grRedFinalScore.Name = "grRedFinalScore";
            this.grRedFinalScore.Size = new System.Drawing.Size(316, 676);
            this.grRedFinalScore.TabIndex = 130;
            this.grRedFinalScore.TabStop = false;
            this.grRedFinalScore.Text = "Red Final Score";
            // 
            // tbRedScore
            // 
            this.tbRedScore.Location = new System.Drawing.Point(210, 639);
            this.tbRedScore.Name = "tbRedScore";
            this.tbRedScore.Size = new System.Drawing.Size(100, 31);
            this.tbRedScore.TabIndex = 195;
            this.tbRedScore.Tag = "";
            this.tbRedScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRedScore.Visible = false;
            this.tbRedScore.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            // 
            // lbRedPushScore
            // 
            this.lbRedPushScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRedPushScore.ForeColor = System.Drawing.Color.Red;
            this.lbRedPushScore.Location = new System.Drawing.Point(260, 532);
            this.lbRedPushScore.Name = "lbRedPushScore";
            this.lbRedPushScore.Size = new System.Drawing.Size(50, 24);
            this.lbRedPushScore.TabIndex = 207;
            this.lbRedPushScore.Text = "0";
            this.lbRedPushScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbRedPushPointValue
            // 
            this.lbRedPushPointValue.AutoSize = true;
            this.lbRedPushPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRedPushPointValue.ForeColor = System.Drawing.Color.Red;
            this.lbRedPushPointValue.Location = new System.Drawing.Point(199, 532);
            this.lbRedPushPointValue.Name = "lbRedPushPointValue";
            this.lbRedPushPointValue.Size = new System.Drawing.Size(43, 24);
            this.lbRedPushPointValue.TabIndex = 206;
            this.lbRedPushPointValue.Text = "200";
            this.lbRedPushPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbRedPushes
            // 
            this.tbRedPushes.Location = new System.Drawing.Point(126, 528);
            this.tbRedPushes.Name = "tbRedPushes";
            this.tbRedPushes.Size = new System.Drawing.Size(67, 31);
            this.tbRedPushes.TabIndex = 205;
            this.tbRedPushes.Tag = "";
            this.tbRedPushes.Text = "0";
            this.tbRedPushes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRedPushes.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            this.tbRedPushes.Validated += new System.EventHandler(this.OnValidation);
            // 
            // lbRedPushes
            // 
            this.lbRedPushes.AutoSize = true;
            this.lbRedPushes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRedPushes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRedPushes.Location = new System.Drawing.Point(6, 532);
            this.lbRedPushes.Name = "lbRedPushes";
            this.lbRedPushes.Size = new System.Drawing.Size(79, 24);
            this.lbRedPushes.TabIndex = 204;
            this.lbRedPushes.Text = "Pushes";
            this.lbRedPushes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbRedDq
            // 
            this.cbRedDq.AutoSize = true;
            this.cbRedDq.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbRedDq.Location = new System.Drawing.Point(293, 611);
            this.cbRedDq.Name = "cbRedDq";
            this.cbRedDq.Size = new System.Drawing.Size(15, 14);
            this.cbRedDq.TabIndex = 199;
            this.cbRedDq.UseVisualStyleBackColor = true;
            this.cbRedDq.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // lbRedScore
            // 
            this.lbRedScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRedScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbRedScore.Location = new System.Drawing.Point(158, 643);
            this.lbRedScore.Name = "lbRedScore";
            this.lbRedScore.Size = new System.Drawing.Size(150, 24);
            this.lbRedScore.TabIndex = 195;
            this.lbRedScore.Text = "0";
            this.lbRedScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbRedPenalty
            // 
            this.tbRedPenalty.Location = new System.Drawing.Point(126, 565);
            this.tbRedPenalty.Name = "tbRedPenalty";
            this.tbRedPenalty.Size = new System.Drawing.Size(67, 31);
            this.tbRedPenalty.TabIndex = 185;
            this.tbRedPenalty.Tag = "[0,3]";
            this.tbRedPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbRedPenalty.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            this.tbRedPenalty.Validated += new System.EventHandler(this.OnValidation);
            // 
            // lbRedPenaltyPointValue
            // 
            this.lbRedPenaltyPointValue.AutoSize = true;
            this.lbRedPenaltyPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRedPenaltyPointValue.ForeColor = System.Drawing.Color.Red;
            this.lbRedPenaltyPointValue.Location = new System.Drawing.Point(199, 569);
            this.lbRedPenaltyPointValue.Name = "lbRedPenaltyPointValue";
            this.lbRedPenaltyPointValue.Size = new System.Drawing.Size(43, 24);
            this.lbRedPenaltyPointValue.TabIndex = 184;
            this.lbRedPenaltyPointValue.Text = "200";
            this.lbRedPenaltyPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbRedPenaltyScore
            // 
            this.lbRedPenaltyScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbRedPenaltyScore.ForeColor = System.Drawing.Color.Red;
            this.lbRedPenaltyScore.Location = new System.Drawing.Point(260, 569);
            this.lbRedPenaltyScore.Name = "lbRedPenaltyScore";
            this.lbRedPenaltyScore.Size = new System.Drawing.Size(50, 24);
            this.lbRedPenaltyScore.TabIndex = 177;
            this.lbRedPenaltyScore.Text = "0";
            this.lbRedPenaltyScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblRedDQ
            // 
            this.lblRedDQ.AutoSize = true;
            this.lblRedDQ.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedDQ.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRedDQ.Location = new System.Drawing.Point(5, 606);
            this.lblRedDQ.Name = "lblRedDQ";
            this.lblRedDQ.Size = new System.Drawing.Size(118, 24);
            this.lblRedDQ.TabIndex = 135;
            this.lblRedDQ.Text = "Disqualified";
            this.lblRedDQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRedPenalty
            // 
            this.lblRedPenalty.AutoSize = true;
            this.lblRedPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedPenalty.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRedPenalty.Location = new System.Drawing.Point(6, 569);
            this.lblRedPenalty.Name = "lblRedPenalty";
            this.lblRedPenalty.Size = new System.Drawing.Size(95, 24);
            this.lblRedPenalty.TabIndex = 133;
            this.lblRedPenalty.Text = "Penalties";
            this.lblRedPenalty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRedFinalScore
            // 
            this.lblRedFinalScore.AutoSize = true;
            this.lblRedFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRedFinalScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblRedFinalScore.Location = new System.Drawing.Point(6, 643);
            this.lblRedFinalScore.Name = "lblRedFinalScore";
            this.lblRedFinalScore.Size = new System.Drawing.Size(117, 24);
            this.lblRedFinalScore.TabIndex = 5;
            this.lblRedFinalScore.Text = "Final Score";
            this.lblRedFinalScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // gbGreenFinalScore
            // 
            this.gbGreenFinalScore.Controls.Add(this.tbGreenScore);
            this.gbGreenFinalScore.Controls.Add(this.lbGreenPushScore);
            this.gbGreenFinalScore.Controls.Add(this.lbGreenPushPointValue);
            this.gbGreenFinalScore.Controls.Add(this.tbGreenPushes);
            this.gbGreenFinalScore.Controls.Add(this.lbGreenPushes);
            this.gbGreenFinalScore.Controls.Add(this.cbGreenDq);
            this.gbGreenFinalScore.Controls.Add(this.lbGreenScore);
            this.gbGreenFinalScore.Controls.Add(this.tbGreenPenalty);
            this.gbGreenFinalScore.Controls.Add(this.lbGreenPenaltyPointValue);
            this.gbGreenFinalScore.Controls.Add(this.lbGreenPenaltyScore);
            this.gbGreenFinalScore.Controls.Add(this.lblGreenDq);
            this.gbGreenFinalScore.Controls.Add(this.lblGreenPenalty);
            this.gbGreenFinalScore.Controls.Add(this.lblGreenFinalScore);
            this.gbGreenFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGreenFinalScore.ForeColor = System.Drawing.Color.Lime;
            this.gbGreenFinalScore.Location = new System.Drawing.Point(45, 12);
            this.gbGreenFinalScore.Name = "gbGreenFinalScore";
            this.gbGreenFinalScore.Size = new System.Drawing.Size(316, 676);
            this.gbGreenFinalScore.TabIndex = 129;
            this.gbGreenFinalScore.TabStop = false;
            this.gbGreenFinalScore.Text = "Green Final Score";
            // 
            // tbGreenScore
            // 
            this.tbGreenScore.Location = new System.Drawing.Point(210, 639);
            this.tbGreenScore.Name = "tbGreenScore";
            this.tbGreenScore.Size = new System.Drawing.Size(100, 31);
            this.tbGreenScore.TabIndex = 208;
            this.tbGreenScore.Tag = "";
            this.tbGreenScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbGreenScore.Visible = false;
            // 
            // lbGreenPushScore
            // 
            this.lbGreenPushScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreenPushScore.ForeColor = System.Drawing.Color.Red;
            this.lbGreenPushScore.Location = new System.Drawing.Point(260, 528);
            this.lbGreenPushScore.Name = "lbGreenPushScore";
            this.lbGreenPushScore.Size = new System.Drawing.Size(50, 24);
            this.lbGreenPushScore.TabIndex = 203;
            this.lbGreenPushScore.Text = "0";
            this.lbGreenPushScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbGreenPushPointValue
            // 
            this.lbGreenPushPointValue.AutoSize = true;
            this.lbGreenPushPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreenPushPointValue.ForeColor = System.Drawing.Color.Red;
            this.lbGreenPushPointValue.Location = new System.Drawing.Point(189, 528);
            this.lbGreenPushPointValue.Name = "lbGreenPushPointValue";
            this.lbGreenPushPointValue.Size = new System.Drawing.Size(43, 24);
            this.lbGreenPushPointValue.TabIndex = 202;
            this.lbGreenPushPointValue.Text = "200";
            this.lbGreenPushPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbGreenPushes
            // 
            this.tbGreenPushes.Location = new System.Drawing.Point(116, 525);
            this.tbGreenPushes.Name = "tbGreenPushes";
            this.tbGreenPushes.Size = new System.Drawing.Size(67, 31);
            this.tbGreenPushes.TabIndex = 201;
            this.tbGreenPushes.Tag = "";
            this.tbGreenPushes.Text = "0";
            this.tbGreenPushes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbGreenPushes.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            this.tbGreenPushes.Validated += new System.EventHandler(this.OnValidation);
            // 
            // lbGreenPushes
            // 
            this.lbGreenPushes.AutoSize = true;
            this.lbGreenPushes.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreenPushes.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbGreenPushes.Location = new System.Drawing.Point(6, 528);
            this.lbGreenPushes.Name = "lbGreenPushes";
            this.lbGreenPushes.Size = new System.Drawing.Size(79, 24);
            this.lbGreenPushes.TabIndex = 200;
            this.lbGreenPushes.Text = "Pushes";
            this.lbGreenPushes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cbGreenDq
            // 
            this.cbGreenDq.AutoSize = true;
            this.cbGreenDq.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cbGreenDq.Location = new System.Drawing.Point(295, 611);
            this.cbGreenDq.Name = "cbGreenDq";
            this.cbGreenDq.Size = new System.Drawing.Size(15, 14);
            this.cbGreenDq.TabIndex = 198;
            this.cbGreenDq.UseVisualStyleBackColor = true;
            this.cbGreenDq.CheckedChanged += new System.EventHandler(this.CheckedChanged);
            // 
            // lbGreenScore
            // 
            this.lbGreenScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreenScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lbGreenScore.Location = new System.Drawing.Point(160, 643);
            this.lbGreenScore.Name = "lbGreenScore";
            this.lbGreenScore.Size = new System.Drawing.Size(150, 24);
            this.lbGreenScore.TabIndex = 194;
            this.lbGreenScore.Text = "0";
            this.lbGreenScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tbGreenPenalty
            // 
            this.tbGreenPenalty.Location = new System.Drawing.Point(116, 565);
            this.tbGreenPenalty.Name = "tbGreenPenalty";
            this.tbGreenPenalty.Size = new System.Drawing.Size(67, 31);
            this.tbGreenPenalty.TabIndex = 184;
            this.tbGreenPenalty.Tag = "[0,3]";
            this.tbGreenPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.tbGreenPenalty.Validating += new System.ComponentModel.CancelEventHandler(this.TextBoxValidation);
            this.tbGreenPenalty.Validated += new System.EventHandler(this.OnValidation);
            // 
            // lbGreenPenaltyPointValue
            // 
            this.lbGreenPenaltyPointValue.AutoSize = true;
            this.lbGreenPenaltyPointValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreenPenaltyPointValue.ForeColor = System.Drawing.Color.Red;
            this.lbGreenPenaltyPointValue.Location = new System.Drawing.Point(189, 569);
            this.lbGreenPenaltyPointValue.Name = "lbGreenPenaltyPointValue";
            this.lbGreenPenaltyPointValue.Size = new System.Drawing.Size(43, 24);
            this.lbGreenPenaltyPointValue.TabIndex = 183;
            this.lbGreenPenaltyPointValue.Text = "200";
            this.lbGreenPenaltyPointValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbGreenPenaltyScore
            // 
            this.lbGreenPenaltyScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbGreenPenaltyScore.ForeColor = System.Drawing.Color.Red;
            this.lbGreenPenaltyScore.Location = new System.Drawing.Point(260, 569);
            this.lbGreenPenaltyScore.Name = "lbGreenPenaltyScore";
            this.lbGreenPenaltyScore.Size = new System.Drawing.Size(50, 24);
            this.lbGreenPenaltyScore.TabIndex = 176;
            this.lbGreenPenaltyScore.Text = "0";
            this.lbGreenPenaltyScore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblGreenPenalty
            // 
            this.lblGreenPenalty.AutoSize = true;
            this.lblGreenPenalty.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenPenalty.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblGreenPenalty.Location = new System.Drawing.Point(6, 569);
            this.lblGreenPenalty.Name = "lblGreenPenalty";
            this.lblGreenPenalty.Size = new System.Drawing.Size(95, 24);
            this.lblGreenPenalty.TabIndex = 132;
            this.lblGreenPenalty.Text = "Penalties";
            this.lblGreenPenalty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblGreenFinalScore
            // 
            this.lblGreenFinalScore.AutoSize = true;
            this.lblGreenFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenFinalScore.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblGreenFinalScore.Location = new System.Drawing.Point(6, 643);
            this.lblGreenFinalScore.Name = "lblGreenFinalScore";
            this.lblGreenFinalScore.Size = new System.Drawing.Size(117, 24);
            this.lblGreenFinalScore.TabIndex = 5;
            this.lblGreenFinalScore.Text = "Final Score";
            this.lblGreenFinalScore.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnFinalScore
            // 
            this.btnFinalScore.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnFinalScore.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnFinalScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFinalScore.Location = new System.Drawing.Point(486, 701);
            this.btnFinalScore.Margin = new System.Windows.Forms.Padding(10);
            this.btnFinalScore.Name = "btnFinalScore";
            this.btnFinalScore.Size = new System.Drawing.Size(200, 40);
            this.btnFinalScore.TabIndex = 128;
            this.btnFinalScore.Text = "Final Score";
            this.btnFinalScore.UseVisualStyleBackColor = true;
            this.btnFinalScore.Click += new System.EventHandler(this.btnFinalScore_Click);
            // 
            // lblGreenDq
            // 
            this.lblGreenDq.AutoSize = true;
            this.lblGreenDq.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGreenDq.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblGreenDq.Location = new System.Drawing.Point(6, 606);
            this.lblGreenDq.Name = "lblGreenDq";
            this.lblGreenDq.Size = new System.Drawing.Size(118, 24);
            this.lblGreenDq.TabIndex = 134;
            this.lblGreenDq.Text = "Disqualified";
            this.lblGreenDq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Score
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 760);
            this.Controls.Add(this.btnOverride);
            this.Controls.Add(this.btnUpdateScore);
            this.Controls.Add(this.grRedFinalScore);
            this.Controls.Add(this.gbGreenFinalScore);
            this.Controls.Add(this.btnFinalScore);
            this.Name = "Score";
            this.Text = "Score";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Score_FormClosed);
            this.Load += new System.EventHandler(this.Score_Shown);
            this.grRedFinalScore.ResumeLayout(false);
            this.grRedFinalScore.PerformLayout();
            this.gbGreenFinalScore.ResumeLayout(false);
            this.gbGreenFinalScore.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button btnOverride;
        public System.Windows.Forms.Button btnUpdateScore;
        private System.Windows.Forms.GroupBox grRedFinalScore;
        private System.Windows.Forms.Label lblRedDQ;
        private System.Windows.Forms.Label lblRedPenalty;
        private System.Windows.Forms.Label lblRedFinalScore;
        private System.Windows.Forms.GroupBox gbGreenFinalScore;
        private System.Windows.Forms.Label lblGreenPenalty;
        private System.Windows.Forms.Label lblGreenFinalScore;
        public System.Windows.Forms.Button btnFinalScore;
        private System.Windows.Forms.Label lbRedPenaltyScore;
        private System.Windows.Forms.Label lbGreenPenaltyScore;
        private System.Windows.Forms.TextBox tbRedPenalty;
        private System.Windows.Forms.TextBox tbGreenPenalty;
        private System.Windows.Forms.Label lbRedPenaltyPointValue;
        private System.Windows.Forms.Label lbGreenPenaltyPointValue;
        private System.Windows.Forms.Label lbRedScore;
        private System.Windows.Forms.Label lbGreenScore;
        private System.Windows.Forms.CheckBox cbRedDq;
        private System.Windows.Forms.CheckBox cbGreenDq;
        private System.Windows.Forms.Label lbRedPushScore;
        private System.Windows.Forms.Label lbRedPushPointValue;
        private System.Windows.Forms.TextBox tbRedPushes;
        private System.Windows.Forms.Label lbRedPushes;
        private System.Windows.Forms.Label lbGreenPushScore;
        private System.Windows.Forms.Label lbGreenPushPointValue;
        private System.Windows.Forms.TextBox tbGreenPushes;
        private System.Windows.Forms.Label lbGreenPushes;
        private System.Windows.Forms.TextBox tbRedScore;
        private System.Windows.Forms.TextBox tbGreenScore;
        private System.Windows.Forms.Label lblGreenDq;
    }
}