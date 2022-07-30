
namespace TimeChip_App_1._0
{
    partial class DlgMitarbeiter
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
            this.m_lblTitel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_tbxVorname = new System.Windows.Forms.TextBox();
            this.m_tbxNachname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cmbxAProfil = new System.Windows.Forms.ComboBox();
            this.clsArbeitsprofilBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.m_dtpArbeitsb = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.clsArbeitsprofilBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblTitel
            // 
            this.m_lblTitel.AutoSize = true;
            this.m_lblTitel.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTitel.Location = new System.Drawing.Point(9, 9);
            this.m_lblTitel.Name = "m_lblTitel";
            this.m_lblTitel.Size = new System.Drawing.Size(396, 46);
            this.m_lblTitel.TabIndex = 0;
            this.m_lblTitel.Text = "Neue:r Mitarbeiter:in";
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 27);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vorname:";
            // 
            // m_tbxVorname
            // 
            this.m_tbxVorname.Location = new System.Drawing.Point(324, 96);
            this.m_tbxVorname.Multiline = true;
            this.m_tbxVorname.Name = "m_tbxVorname";
            this.m_tbxVorname.Size = new System.Drawing.Size(258, 29);
            this.m_tbxVorname.TabIndex = 2;
            // 
            // m_tbxNachname
            // 
            this.m_tbxNachname.Location = new System.Drawing.Point(324, 156);
            this.m_tbxNachname.Multiline = true;
            this.m_tbxNachname.Name = "m_tbxNachname";
            this.m_tbxNachname.Size = new System.Drawing.Size(258, 29);
            this.m_tbxNachname.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AllowDrop = true;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 160);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(135, 27);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nachname:";
            // 
            // m_cmbxAProfil
            // 
            this.m_cmbxAProfil.FormattingEnabled = true;
            this.m_cmbxAProfil.Location = new System.Drawing.Point(324, 263);
            this.m_cmbxAProfil.Name = "m_cmbxAProfil";
            this.m_cmbxAProfil.Size = new System.Drawing.Size(258, 24);
            this.m_cmbxAProfil.TabIndex = 5;
            // 
            // clsArbeitsprofilBindingSource
            // 
            this.clsArbeitsprofilBindingSource.DataSource = typeof(TimeChip_App_1._0.ClsArbeitsprofil);
            // 
            // label4
            // 
            this.label4.AllowDrop = true;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 262);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(183, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "Arbeitszeitprofil:";
            // 
            // m_dtpArbeitsb
            // 
            this.m_dtpArbeitsb.Location = new System.Drawing.Point(324, 326);
            this.m_dtpArbeitsb.Name = "m_dtpArbeitsb";
            this.m_dtpArbeitsb.Size = new System.Drawing.Size(258, 22);
            this.m_dtpArbeitsb.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AllowDrop = true;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 323);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(167, 27);
            this.label5.TabIndex = 8;
            this.label5.Text = "Arbeitsbeginn:";
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOK.Location = new System.Drawing.Point(97, 434);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(168, 47);
            this.m_btnOK.TabIndex = 9;
            this.m_btnOK.Text = "Erstellen";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnCancel.Location = new System.Drawing.Point(369, 434);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(170, 47);
            this.m_btnCancel.TabIndex = 10;
            this.m_btnCancel.Text = "Abbrechen";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // DlgMitarbeiter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 522);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_dtpArbeitsb);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.m_cmbxAProfil);
            this.Controls.Add(this.m_tbxNachname);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.m_tbxVorname);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_lblTitel);
            this.Name = "DlgMitarbeiter";
            this.Text = "Neu";
            ((System.ComponentModel.ISupportInitialize)(this.clsArbeitsprofilBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblTitel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox m_tbxVorname;
        private System.Windows.Forms.TextBox m_tbxNachname;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker m_dtpArbeitsb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.BindingSource clsArbeitsprofilBindingSource;
        public System.Windows.Forms.ComboBox m_cmbxAProfil;
    }
}