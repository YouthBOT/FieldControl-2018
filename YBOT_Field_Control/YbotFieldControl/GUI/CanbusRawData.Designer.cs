namespace YbotFieldControl
{
    partial class CANRawData
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
            this.btnClearText = new System.Windows.Forms.Button();
            this.btnSaveText = new System.Windows.Forms.Button();
            this.tbCANRawData = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnClearText
            // 
            this.btnClearText.Location = new System.Drawing.Point(13, 372);
            this.btnClearText.Name = "btnClearText";
            this.btnClearText.Size = new System.Drawing.Size(75, 50);
            this.btnClearText.TabIndex = 2;
            this.btnClearText.Text = "Clear Text";
            this.btnClearText.UseVisualStyleBackColor = true;
            this.btnClearText.Click += new System.EventHandler(this.btnClearText_Click);
            // 
            // btnSaveText
            // 
            this.btnSaveText.Location = new System.Drawing.Point(94, 372);
            this.btnSaveText.Name = "btnSaveText";
            this.btnSaveText.Size = new System.Drawing.Size(75, 50);
            this.btnSaveText.TabIndex = 3;
            this.btnSaveText.Text = "Save Text";
            this.btnSaveText.UseVisualStyleBackColor = true;
            this.btnSaveText.Click += new System.EventHandler(this.btnSaveText_Click);
            // 
            // tbCANRawData
            // 
            this.tbCANRawData.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.tbCANRawData.Font = new System.Drawing.Font("Monospac821 BT", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbCANRawData.HideSelection = false;
            this.tbCANRawData.Location = new System.Drawing.Point(13, 13);
            this.tbCANRawData.Multiline = true;
            this.tbCANRawData.Name = "tbCANRawData";
            this.tbCANRawData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbCANRawData.Size = new System.Drawing.Size(393, 341);
            this.tbCANRawData.TabIndex = 4;
            // 
            // CANRawData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(418, 449);
            this.Controls.Add(this.tbCANRawData);
            this.Controls.Add(this.btnSaveText);
            this.Controls.Add(this.btnClearText);
            this.Name = "CANRawData";
            this.Text = "CANBUS RAW DATA";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClearText;
        private System.Windows.Forms.Button btnSaveText;
        private System.Windows.Forms.TextBox tbCANRawData;
    }
}