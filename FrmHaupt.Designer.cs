
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
            this.m_btnNeuerMitarbeiter = new System.Windows.Forms.Button();
            this.m_btnBearbeiten = new System.Windows.Forms.Button();
            this.m_btnLöschen = new System.Windows.Forms.Button();
            this.m_btnarbeitsprofil = new System.Windows.Forms.Button();
            this.m_lbxBuchungen = new System.Windows.Forms.ListBox();
            this.m_btnNeueBuchung = new System.Windows.Forms.Button();
            this.m_btnBuchungBearbeiten = new System.Windows.Forms.Button();
            this.m_btnBuchungLöschen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // m_cldKalender
            // 
            this.m_cldKalender.Location = new System.Drawing.Point(675, 16);
            this.m_cldKalender.Margin = new System.Windows.Forms.Padding(7);
            this.m_cldKalender.MaxSelectionCount = 1;
            this.m_cldKalender.Name = "m_cldKalender";
            this.m_cldKalender.ShowToday = false;
            this.m_cldKalender.ShowWeekNumbers = true;
            this.m_cldKalender.TabIndex = 0;
            this.m_cldKalender.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.m_cldKalenderChanged);
            this.m_cldKalender.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.m_cldKalenderChanged);
            // 
            // m_lbxMitarbeiter
            // 
            this.m_lbxMitarbeiter.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbxMitarbeiter.FormattingEnabled = true;
            this.m_lbxMitarbeiter.ItemHeight = 18;
            this.m_lbxMitarbeiter.Location = new System.Drawing.Point(9, 115);
            this.m_lbxMitarbeiter.Margin = new System.Windows.Forms.Padding(2);
            this.m_lbxMitarbeiter.Name = "m_lbxMitarbeiter";
            this.m_lbxMitarbeiter.Size = new System.Drawing.Size(296, 328);
            this.m_lbxMitarbeiter.TabIndex = 1;
            this.m_lbxMitarbeiter.SelectedIndexChanged += new System.EventHandler(this.m_lbxMitarbeiterChanged);
            this.m_lbxMitarbeiter.SelectedValueChanged += new System.EventHandler(this.m_lbxMitarbeiterChanged);
            // 
            // m_btnNeuerMitarbeiter
            // 
            this.m_btnNeuerMitarbeiter.Location = new System.Drawing.Point(9, 63);
            this.m_btnNeuerMitarbeiter.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnNeuerMitarbeiter.Name = "m_btnNeuerMitarbeiter";
            this.m_btnNeuerMitarbeiter.Size = new System.Drawing.Size(95, 49);
            this.m_btnNeuerMitarbeiter.TabIndex = 3;
            this.m_btnNeuerMitarbeiter.Text = "Neu";
            this.m_btnNeuerMitarbeiter.UseVisualStyleBackColor = true;
            this.m_btnNeuerMitarbeiter.Click += new System.EventHandler(this.m_btnNeu_Click);
            // 
            // m_btnBearbeiten
            // 
            this.m_btnBearbeiten.Location = new System.Drawing.Point(109, 63);
            this.m_btnBearbeiten.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnBearbeiten.Name = "m_btnBearbeiten";
            this.m_btnBearbeiten.Size = new System.Drawing.Size(95, 49);
            this.m_btnBearbeiten.TabIndex = 4;
            this.m_btnBearbeiten.Text = "Bearbeiten";
            this.m_btnBearbeiten.UseVisualStyleBackColor = true;
            this.m_btnBearbeiten.Click += new System.EventHandler(this.m_btnBearbeiten_Click);
            // 
            // m_btnLöschen
            // 
            this.m_btnLöschen.Location = new System.Drawing.Point(208, 63);
            this.m_btnLöschen.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnLöschen.Name = "m_btnLöschen";
            this.m_btnLöschen.Size = new System.Drawing.Size(95, 49);
            this.m_btnLöschen.TabIndex = 5;
            this.m_btnLöschen.Text = "Löschen";
            this.m_btnLöschen.UseVisualStyleBackColor = true;
            this.m_btnLöschen.Click += new System.EventHandler(this.m_btnLöschen_Click);
            // 
            // m_btnarbeitsprofil
            // 
            this.m_btnarbeitsprofil.Location = new System.Drawing.Point(9, 10);
            this.m_btnarbeitsprofil.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnarbeitsprofil.Name = "m_btnarbeitsprofil";
            this.m_btnarbeitsprofil.Size = new System.Drawing.Size(295, 49);
            this.m_btnarbeitsprofil.TabIndex = 6;
            this.m_btnarbeitsprofil.Text = "Arbeitsprofile";
            this.m_btnarbeitsprofil.UseVisualStyleBackColor = true;
            this.m_btnarbeitsprofil.Click += new System.EventHandler(this.m_btnarbeitsprofil_Click);
            // 
            // m_lbxBuchungen
            // 
            this.m_lbxBuchungen.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lbxBuchungen.FormattingEnabled = true;
            this.m_lbxBuchungen.ItemHeight = 18;
            this.m_lbxBuchungen.Location = new System.Drawing.Point(349, 115);
            this.m_lbxBuchungen.Margin = new System.Windows.Forms.Padding(2);
            this.m_lbxBuchungen.Name = "m_lbxBuchungen";
            this.m_lbxBuchungen.Size = new System.Drawing.Size(296, 328);
            this.m_lbxBuchungen.TabIndex = 7;
            // 
            // m_btnNeueBuchung
            // 
            this.m_btnNeueBuchung.Location = new System.Drawing.Point(349, 62);
            this.m_btnNeueBuchung.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnNeueBuchung.Name = "m_btnNeueBuchung";
            this.m_btnNeueBuchung.Size = new System.Drawing.Size(95, 49);
            this.m_btnNeueBuchung.TabIndex = 8;
            this.m_btnNeueBuchung.Text = "Neue Buchung";
            this.m_btnNeueBuchung.UseVisualStyleBackColor = true;
            this.m_btnNeueBuchung.Click += new System.EventHandler(this.m_btnNeueBuchung_Click);
            // 
            // m_btnBuchungBearbeiten
            // 
            this.m_btnBuchungBearbeiten.Location = new System.Drawing.Point(448, 63);
            this.m_btnBuchungBearbeiten.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnBuchungBearbeiten.Name = "m_btnBuchungBearbeiten";
            this.m_btnBuchungBearbeiten.Size = new System.Drawing.Size(95, 49);
            this.m_btnBuchungBearbeiten.TabIndex = 9;
            this.m_btnBuchungBearbeiten.Text = "Buchung Bearbeiten";
            this.m_btnBuchungBearbeiten.UseVisualStyleBackColor = true;
            this.m_btnBuchungBearbeiten.Click += new System.EventHandler(this.m_btnBuchungBearbeiten_Click);
            // 
            // m_btnBuchungLöschen
            // 
            this.m_btnBuchungLöschen.Location = new System.Drawing.Point(547, 62);
            this.m_btnBuchungLöschen.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnBuchungLöschen.Name = "m_btnBuchungLöschen";
            this.m_btnBuchungLöschen.Size = new System.Drawing.Size(95, 49);
            this.m_btnBuchungLöschen.TabIndex = 10;
            this.m_btnBuchungLöschen.Text = "Buchung Löschen";
            this.m_btnBuchungLöschen.UseVisualStyleBackColor = true;
            this.m_btnBuchungLöschen.Click += new System.EventHandler(this.m_btnBuchungLöschen_Click);
            // 
            // FrmHaupt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 457);
            this.Controls.Add(this.m_btnBuchungLöschen);
            this.Controls.Add(this.m_btnBuchungBearbeiten);
            this.Controls.Add(this.m_btnNeueBuchung);
            this.Controls.Add(this.m_lbxBuchungen);
            this.Controls.Add(this.m_btnarbeitsprofil);
            this.Controls.Add(this.m_btnLöschen);
            this.Controls.Add(this.m_btnBearbeiten);
            this.Controls.Add(this.m_btnNeuerMitarbeiter);
            this.Controls.Add(this.m_lbxMitarbeiter);
            this.Controls.Add(this.m_cldKalender);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmHaupt";
            this.Text = " Time Chip";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.MonthCalendar m_cldKalender;
        private System.Windows.Forms.ListBox m_lbxMitarbeiter;
        private System.Windows.Forms.Button m_btnNeuerMitarbeiter;
        private System.Windows.Forms.Button m_btnBearbeiten;
        private System.Windows.Forms.Button m_btnLöschen;
        private System.Windows.Forms.Button m_btnarbeitsprofil;
        private System.Windows.Forms.ListBox m_lbxBuchungen;
        private System.Windows.Forms.Button m_btnNeueBuchung;
        private System.Windows.Forms.Button m_btnBuchungBearbeiten;
        private System.Windows.Forms.Button m_btnBuchungLöschen;
    }
}

