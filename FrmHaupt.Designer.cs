
namespace TimeChip_App_1._0
{
    partial class FrmHaupt
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.m_cldKalender = new System.Windows.Forms.MonthCalendar();
            this.m_lbxMitarbeiter = new System.Windows.Forms.ListBox();
            this.m_btnNeu = new System.Windows.Forms.Button();
            this.m_btnBearbeiten = new System.Windows.Forms.Button();
            this.m_btnLöschen = new System.Windows.Forms.Button();
            this.m_btnarbeitsprofil = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_cldKalender
            // 
            this.m_cldKalender.Location = new System.Drawing.Point(954, 18);
            this.m_cldKalender.Name = "m_cldKalender";
            this.m_cldKalender.ShowToday = false;
            this.m_cldKalender.TabIndex = 0;
            // 
            // m_lbxMitarbeiter
            // 
            this.m_lbxMitarbeiter.FormattingEnabled = true;
            this.m_lbxMitarbeiter.ItemHeight = 16;
            this.m_lbxMitarbeiter.Location = new System.Drawing.Point(12, 142);
            this.m_lbxMitarbeiter.Name = "m_lbxMitarbeiter";
            this.m_lbxMitarbeiter.Size = new System.Drawing.Size(393, 420);
            this.m_lbxMitarbeiter.TabIndex = 2;
            // 
            // m_btnNeu
            // 
            this.m_btnNeu.Location = new System.Drawing.Point(12, 78);
            this.m_btnNeu.Name = "m_btnNeu";
            this.m_btnNeu.Size = new System.Drawing.Size(127, 60);
            this.m_btnNeu.TabIndex = 3;
            this.m_btnNeu.Text = "Neu";
            this.m_btnNeu.UseVisualStyleBackColor = true;
            this.m_btnNeu.Click += new System.EventHandler(this.m_btnNeu_Click);
            // 
            // m_btnBearbeiten
            // 
            this.m_btnBearbeiten.Location = new System.Drawing.Point(145, 78);
            this.m_btnBearbeiten.Name = "m_btnBearbeiten";
            this.m_btnBearbeiten.Size = new System.Drawing.Size(127, 60);
            this.m_btnBearbeiten.TabIndex = 4;
            this.m_btnBearbeiten.Text = "Bearbeiten";
            this.m_btnBearbeiten.UseVisualStyleBackColor = true;
            this.m_btnBearbeiten.Click += new System.EventHandler(this.m_btnBearbeiten_Click);
            // 
            // m_btnLöschen
            // 
            this.m_btnLöschen.Location = new System.Drawing.Point(278, 78);
            this.m_btnLöschen.Name = "m_btnLöschen";
            this.m_btnLöschen.Size = new System.Drawing.Size(127, 60);
            this.m_btnLöschen.TabIndex = 5;
            this.m_btnLöschen.Text = "Löschen";
            this.m_btnLöschen.UseVisualStyleBackColor = true;
            this.m_btnLöschen.Click += new System.EventHandler(this.m_btnLöschen_Click);
            // 
            // m_btnarbeitsprofil
            // 
            this.m_btnarbeitsprofil.Location = new System.Drawing.Point(12, 12);
            this.m_btnarbeitsprofil.Name = "m_btnarbeitsprofil";
            this.m_btnarbeitsprofil.Size = new System.Drawing.Size(393, 60);
            this.m_btnarbeitsprofil.TabIndex = 6;
            this.m_btnarbeitsprofil.Text = "Arbeitsprofile";
            this.m_btnarbeitsprofil.UseVisualStyleBackColor = true;
            this.m_btnarbeitsprofil.Click += new System.EventHandler(this.m_btnarbeitsprofil_Click);
            // 
            // FrmHaupt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 563);
            this.Controls.Add(this.m_btnarbeitsprofil);
            this.Controls.Add(this.m_btnLöschen);
            this.Controls.Add(this.m_btnBearbeiten);
            this.Controls.Add(this.m_btnNeu);
            this.Controls.Add(this.m_lbxMitarbeiter);
            this.Controls.Add(this.m_cldKalender);
            this.Name = "FrmHaupt";
            this.Text = " Time Chip";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar m_cldKalender;
        private System.Windows.Forms.ListBox m_lbxMitarbeiter;
        private System.Windows.Forms.Button m_btnNeu;
        private System.Windows.Forms.Button m_btnBearbeiten;
        private System.Windows.Forms.Button m_btnLöschen;
        private System.Windows.Forms.Button m_btnarbeitsprofil;
    }
}

