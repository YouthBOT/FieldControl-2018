namespace YbotFieldControl
{
    partial class SqlConnectWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose ();
            }
            base.Dispose (disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.serverIpTextBox = new System.Windows.Forms.TextBox();
            this.serverIpLabel = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.connectionButton = new System.Windows.Forms.Button();
            this.messageTextbox = new System.Windows.Forms.TextBox();
            this.sshCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // serverIpTextBox
            // 
            this.serverIpTextBox.Location = new System.Drawing.Point(110, 14);
            this.serverIpTextBox.Name = "serverIpTextBox";
            this.serverIpTextBox.Size = new System.Drawing.Size(160, 20);
            this.serverIpTextBox.TabIndex = 0;
            this.serverIpTextBox.Text = "149.56.109.90";
            // 
            // serverIpLabel
            // 
            this.serverIpLabel.AutoSize = true;
            this.serverIpLabel.Location = new System.Drawing.Point(25, 17);
            this.serverIpLabel.Name = "serverIpLabel";
            this.serverIpLabel.Size = new System.Drawing.Size(79, 13);
            this.serverIpLabel.TabIndex = 1;
            this.serverIpLabel.Text = "Server Address";
            this.serverIpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(110, 41);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.PasswordChar = '*';
            this.passwordTextBox.Size = new System.Drawing.Size(160, 20);
            this.passwordTextBox.TabIndex = 2;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(51, 44);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 3;
            this.passwordLabel.Text = "Password";
            this.passwordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // connectionButton
            // 
            this.connectionButton.Location = new System.Drawing.Point(170, 234);
            this.connectionButton.Name = "connectionButton";
            this.connectionButton.Size = new System.Drawing.Size(100, 40);
            this.connectionButton.TabIndex = 4;
            this.connectionButton.Text = "Connect";
            this.connectionButton.UseVisualStyleBackColor = true;
            this.connectionButton.Click += new System.EventHandler(this.connectionButton_Click);
            // 
            // messageTextbox
            // 
            this.messageTextbox.Location = new System.Drawing.Point(12, 90);
            this.messageTextbox.Multiline = true;
            this.messageTextbox.Name = "messageTextbox";
            this.messageTextbox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.messageTextbox.Size = new System.Drawing.Size(258, 138);
            this.messageTextbox.TabIndex = 6;
            // 
            // sshCheckBox
            // 
            this.sshCheckBox.AutoSize = true;
            this.sshCheckBox.Location = new System.Drawing.Point(110, 67);
            this.sshCheckBox.Name = "sshCheckBox";
            this.sshCheckBox.Size = new System.Drawing.Size(149, 17);
            this.sshCheckBox.TabIndex = 7;
            this.sshCheckBox.Text = "Connect via a SSH server";
            this.sshCheckBox.UseVisualStyleBackColor = true;
            // 
            // SqlConnectWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 286);
            this.Controls.Add(this.sshCheckBox);
            this.Controls.Add(this.messageTextbox);
            this.Controls.Add(this.connectionButton);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.serverIpLabel);
            this.Controls.Add(this.serverIpTextBox);
            this.Name = "SqlConnectWindow";
            this.Text = "Sql Server Connection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox serverIpTextBox;
        private System.Windows.Forms.Label serverIpLabel;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.Button connectionButton;
        private System.Windows.Forms.TextBox messageTextbox;
        private System.Windows.Forms.CheckBox sshCheckBox;
    }
}