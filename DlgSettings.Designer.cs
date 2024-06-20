namespace TimeChip_App
{
    partial class DlgSettings
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
            this.m_tbxIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_tbxDatabase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_tbxUID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.m_tbxPass = new System.Windows.Forms.TextBox();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.m_dtpBerechnungsdate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.m_tbxArduinoIP = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.m_tbxLogLevel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_tbxIP
            // 
            this.m_tbxIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tbxIP.Location = new System.Drawing.Point(174, 12);
            this.m_tbxIP.Name = "m_tbxIP";
            this.m_tbxIP.Size = new System.Drawing.Size(217, 26);
            this.m_tbxIP.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Server IP-Adresse";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Datenbankname";
            // 
            // m_tbxDatabase
            // 
            this.m_tbxDatabase.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tbxDatabase.Location = new System.Drawing.Point(174, 44);
            this.m_tbxDatabase.Name = "m_tbxDatabase";
            this.m_tbxDatabase.Size = new System.Drawing.Size(217, 26);
            this.m_tbxDatabase.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Benutzername";
            // 
            // m_tbxUID
            // 
            this.m_tbxUID.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tbxUID.Location = new System.Drawing.Point(174, 76);
            this.m_tbxUID.Name = "m_tbxUID";
            this.m_tbxUID.Size = new System.Drawing.Size(217, 26);
            this.m_tbxUID.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "Passwort";
            // 
            // m_tbxPass
            // 
            this.m_tbxPass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tbxPass.Location = new System.Drawing.Point(174, 108);
            this.m_tbxPass.Name = "m_tbxPass";
            this.m_tbxPass.PasswordChar = '*';
            this.m_tbxPass.Size = new System.Drawing.Size(217, 26);
            this.m_tbxPass.TabIndex = 3;
            // 
            // m_btnOK
            // 
            this.m_btnOK.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOK.Location = new System.Drawing.Point(120, 250);
            this.m_btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(150, 38);
            this.m_btnOK.TabIndex = 5;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnCancel.Location = new System.Drawing.Point(274, 250);
            this.m_btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(150, 38);
            this.m_btnCancel.TabIndex = 6;
            this.m_btnCancel.Text = "Abbrechen";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 228);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(149, 20);
            this.label5.TabIndex = 29;
            this.label5.Text = "Berechnungsdatum";
            // 
            // m_dtpBerechnungsdate
            // 
            this.m_dtpBerechnungsdate.Location = new System.Drawing.Point(174, 227);
            this.m_dtpBerechnungsdate.Name = "m_dtpBerechnungsdate";
            this.m_dtpBerechnungsdate.Size = new System.Drawing.Size(217, 20);
            this.m_dtpBerechnungsdate.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 146);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 20);
            this.label6.TabIndex = 31;
            this.label6.Text = "Arduino IP";
            // 
            // m_tbxArduinoIP
            // 
            this.m_tbxArduinoIP.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tbxArduinoIP.Location = new System.Drawing.Point(174, 140);
            this.m_tbxArduinoIP.Name = "m_tbxArduinoIP";
            this.m_tbxArduinoIP.Size = new System.Drawing.Size(217, 26);
            this.m_tbxArduinoIP.TabIndex = 30;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(12, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 20);
            this.label7.TabIndex = 33;
            this.label7.Text = "Log Level";
            // 
            // m_tbxLogLevel
            // 
            this.m_tbxLogLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tbxLogLevel.Location = new System.Drawing.Point(174, 172);
            this.m_tbxLogLevel.Name = "m_tbxLogLevel";
            this.m_tbxLogLevel.Size = new System.Drawing.Size(217, 26);
            this.m_tbxLogLevel.TabIndex = 32;
            // 
            // DlgSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 299);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.m_tbxLogLevel);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_tbxArduinoIP);
            this.Controls.Add(this.m_dtpBerechnungsdate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_tbxPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_tbxUID);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_tbxDatabase);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_tbxIP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgSettings";
            this.Text = "Einstellungen";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox m_tbxIP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_tbxDatabase;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox m_tbxUID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox m_tbxPass;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker m_dtpBerechnungsdate;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox m_tbxArduinoIP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox m_tbxLogLevel;
    }
}