namespace TimeChip_App
{
    partial class DlgFingerName
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
            this.label1 = new System.Windows.Forms.Label();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_tbxFingerName = new System.Windows.Forms.TextBox();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 13.8F);
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(422, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Geben Sie dem neuen Finger bitte einen Namen:";
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOK.Location = new System.Drawing.Point(16, 127);
            this.m_btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(126, 38);
            this.m_btnOK.TabIndex = 10;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // m_tbxFingerName
            // 
            this.m_tbxFingerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_tbxFingerName.Location = new System.Drawing.Point(16, 75);
            this.m_tbxFingerName.Name = "m_tbxFingerName";
            this.m_tbxFingerName.Size = new System.Drawing.Size(287, 26);
            this.m_tbxFingerName.TabIndex = 11;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnCancel.Location = new System.Drawing.Point(177, 127);
            this.m_btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(126, 38);
            this.m_btnCancel.TabIndex = 12;
            this.m_btnCancel.Text = "Abbrechen";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // DlgFingerName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 187);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_tbxFingerName);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "DlgFingerName";
            this.Text = "Finger Name";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.TextBox m_tbxFingerName;
        private System.Windows.Forms.Button m_btnCancel;
    }
}