
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
            this.m_btnEntschuldigt = new System.Windows.Forms.Button();
            this.m_btnUrlaub = new System.Windows.Forms.Button();
            this.m_btnUnentschuldigt = new System.Windows.Forms.Button();
            this.m_btnRefresh = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.m_lblIst = new System.Windows.Forms.Label();
            this.m_lblSoll = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.m_lblUrlaub = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
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
            this.m_lbxMitarbeiter.Size = new System.Drawing.Size(294, 328);
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
            this.m_lbxBuchungen.Size = new System.Drawing.Size(293, 166);
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
            // m_btnEntschuldigt
            // 
            this.m_btnEntschuldigt.Location = new System.Drawing.Point(675, 187);
            this.m_btnEntschuldigt.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnEntschuldigt.Name = "m_btnEntschuldigt";
            this.m_btnEntschuldigt.Size = new System.Drawing.Size(95, 49);
            this.m_btnEntschuldigt.TabIndex = 11;
            this.m_btnEntschuldigt.Text = "Entschuldigte Abwesenheit";
            this.m_btnEntschuldigt.UseVisualStyleBackColor = true;
            this.m_btnEntschuldigt.Click += new System.EventHandler(this.m_btnEntschuldigt_Click);
            // 
            // m_btnUrlaub
            // 
            this.m_btnUrlaub.Location = new System.Drawing.Point(780, 187);
            this.m_btnUrlaub.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnUrlaub.Name = "m_btnUrlaub";
            this.m_btnUrlaub.Size = new System.Drawing.Size(95, 49);
            this.m_btnUrlaub.TabIndex = 12;
            this.m_btnUrlaub.Text = "Urlaub";
            this.m_btnUrlaub.UseVisualStyleBackColor = true;
            this.m_btnUrlaub.Click += new System.EventHandler(this.m_btnUrlaub_Click);
            // 
            // m_btnUnentschuldigt
            // 
            this.m_btnUnentschuldigt.Location = new System.Drawing.Point(675, 240);
            this.m_btnUnentschuldigt.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnUnentschuldigt.Name = "m_btnUnentschuldigt";
            this.m_btnUnentschuldigt.Size = new System.Drawing.Size(95, 49);
            this.m_btnUnentschuldigt.TabIndex = 13;
            this.m_btnUnentschuldigt.Text = "Zeitausgleichs-\r\nstunden";
            this.m_btnUnentschuldigt.UseVisualStyleBackColor = true;
            this.m_btnUnentschuldigt.Click += new System.EventHandler(this.m_btnUnentschuldigt_Click);
            // 
            // m_btnRefresh
            // 
            this.m_btnRefresh.Location = new System.Drawing.Point(349, 11);
            this.m_btnRefresh.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnRefresh.Name = "m_btnRefresh";
            this.m_btnRefresh.Size = new System.Drawing.Size(293, 49);
            this.m_btnRefresh.TabIndex = 14;
            this.m_btnRefresh.Text = "Aktualisieren";
            this.m_btnRefresh.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 43.10345F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 56.89655F));
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_lblIst, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_lblSoll, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_lblUrlaub, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(647, 294);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(232, 149);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(22, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 20);
            this.label6.TabIndex = 13;
            this.label6.Text = "Urlaub";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(36, 62);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(27, 20);
            this.label7.TabIndex = 7;
            this.label7.Text = "Ist";
            // 
            // m_lblIst
            // 
            this.m_lblIst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_lblIst.AutoSize = true;
            this.m_lblIst.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblIst.Location = new System.Drawing.Point(143, 63);
            this.m_lblIst.Name = "m_lblIst";
            this.m_lblIst.Size = new System.Drawing.Size(46, 18);
            this.m_lblIst.TabIndex = 5;
            this.m_lblIst.Text = "label6";
            // 
            // m_lblSoll
            // 
            this.m_lblSoll.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_lblSoll.AutoSize = true;
            this.m_lblSoll.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblSoll.Location = new System.Drawing.Point(143, 34);
            this.m_lblSoll.Name = "m_lblSoll";
            this.m_lblSoll.Size = new System.Drawing.Size(46, 18);
            this.m_lblSoll.TabIndex = 3;
            this.m_lblSoll.Text = "label4";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Soll";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(139, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Zeiten";
            // 
            // m_lblUrlaub
            // 
            this.m_lblUrlaub.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_lblUrlaub.AutoSize = true;
            this.m_lblUrlaub.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblUrlaub.Location = new System.Drawing.Point(143, 123);
            this.m_lblUrlaub.Name = "m_lblUrlaub";
            this.m_lblUrlaub.Size = new System.Drawing.Size(46, 18);
            this.m_lblUrlaub.TabIndex = 4;
            this.m_lblUrlaub.Text = "label5";
            // 
            // FrmHaupt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 457);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.m_btnRefresh);
            this.Controls.Add(this.m_btnUnentschuldigt);
            this.Controls.Add(this.m_btnUrlaub);
            this.Controls.Add(this.m_btnEntschuldigt);
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
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
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
        private System.Windows.Forms.Button m_btnEntschuldigt;
        private System.Windows.Forms.Button m_btnUrlaub;
        private System.Windows.Forms.Button m_btnUnentschuldigt;
        private System.Windows.Forms.Button m_btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label m_lblIst;
        private System.Windows.Forms.Label m_lblSoll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label m_lblUrlaub;
    }
}

