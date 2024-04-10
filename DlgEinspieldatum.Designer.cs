namespace TimeChip_App
{
    partial class DlgEinspieldatum
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
            this.m_dtpEinspieldatum = new System.Windows.Forms.DateTimePicker();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 13.8F);
            this.label1.Location = new System.Drawing.Point(12, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(450, 22);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ab wann sollen die Änderungen angewandt werden?";
            // 
            // m_dtpEinspieldatum
            // 
            this.m_dtpEinspieldatum.Location = new System.Drawing.Point(144, 79);
            this.m_dtpEinspieldatum.Name = "m_dtpEinspieldatum";
            this.m_dtpEinspieldatum.Size = new System.Drawing.Size(200, 20);
            this.m_dtpEinspieldatum.TabIndex = 1;
            // 
            // m_btnOK
            // 
            this.m_btnOK.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOK.Location = new System.Drawing.Point(179, 138);
            this.m_btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(126, 38);
            this.m_btnOK.TabIndex = 10;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // DlgEinspieldatum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 187);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_dtpEinspieldatum);
            this.Controls.Add(this.label1);
            this.Name = "DlgEinspieldatum";
            this.Text = "DlgEinspieldatum";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker m_dtpEinspieldatum;
        private System.Windows.Forms.Button m_btnOK;
    }
}