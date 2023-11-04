
namespace TimeChip_App
{
    partial class DlgArbeitszeitprofile
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
            this.m_lbxArbeitszeitprofile = new System.Windows.Forms.ListBox();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnAbbrechen = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_cmbxSonntag = new System.Windows.Forms.ComboBox();
            this.m_cmbxSamstag = new System.Windows.Forms.ComboBox();
            this.m_cmbxFreitag = new System.Windows.Forms.ComboBox();
            this.m_cmbxDonnerstag = new System.Windows.Forms.ComboBox();
            this.m_cmbxMittwoch = new System.Windows.Forms.ComboBox();
            this.m_cmbxDienstag = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.m_tbxName = new System.Windows.Forms.TextBox();
            this.m_cmbxMontag = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.m_cbGleitzeit = new System.Windows.Forms.CheckBox();
            this.m_btnTage = new System.Windows.Forms.Button();
            this.m_btnErstellen = new System.Windows.Forms.Button();
            this.m_btnNeu = new System.Windows.Forms.Button();
            this.m_btnAktualisieren = new System.Windows.Forms.Button();
            this.m_btnLöschen = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_lbxArbeitszeitprofile
            // 
            this.m_lbxArbeitszeitprofile.FormattingEnabled = true;
            this.m_lbxArbeitszeitprofile.Location = new System.Drawing.Point(9, 10);
            this.m_lbxArbeitszeitprofile.Margin = new System.Windows.Forms.Padding(2);
            this.m_lbxArbeitszeitprofile.Name = "m_lbxArbeitszeitprofile";
            this.m_lbxArbeitszeitprofile.Size = new System.Drawing.Size(134, 316);
            this.m_lbxArbeitszeitprofile.TabIndex = 18;
            this.m_lbxArbeitszeitprofile.SelectedIndexChanged += new System.EventHandler(this.LbxArbeitszeitprofile_SelectedIndexChanged);
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOK.Location = new System.Drawing.Point(290, 373);
            this.m_btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(134, 38);
            this.m_btnOK.TabIndex = 16;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // m_btnAbbrechen
            // 
            this.m_btnAbbrechen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAbbrechen.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAbbrechen.Location = new System.Drawing.Point(428, 373);
            this.m_btnAbbrechen.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnAbbrechen.Name = "m_btnAbbrechen";
            this.m_btnAbbrechen.Size = new System.Drawing.Size(134, 38);
            this.m_btnAbbrechen.TabIndex = 17;
            this.m_btnAbbrechen.Text = "Abbrechen";
            this.m_btnAbbrechen.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(65, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Montag:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(59, 77);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Dienstag:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(60, 112);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Mittwoch:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(48, 147);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Donnerstag:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(67, 182);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Freitag:";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(60, 217);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Samstag:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(61, 252);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 20);
            this.label7.TabIndex = 18;
            this.label7.Text = "Sonntag:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.m_cmbxSonntag, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.m_cmbxSamstag, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.m_cmbxFreitag, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.m_cmbxDonnerstag, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.m_cmbxMittwoch, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.m_cmbxDienstag, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_tbxName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.m_cmbxMontag, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label9, 0, 8);
            this.tableLayoutPanel1.Controls.Add(this.m_cbGleitzeit, 1, 8);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(152, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 9;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(410, 315);
            this.tableLayoutPanel1.TabIndex = 19;
            // 
            // m_cmbxSonntag
            // 
            this.m_cmbxSonntag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cmbxSonntag.FormattingEnabled = true;
            this.m_cmbxSonntag.Location = new System.Drawing.Point(241, 252);
            this.m_cmbxSonntag.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxSonntag.Name = "m_cmbxSonntag";
            this.m_cmbxSonntag.Size = new System.Drawing.Size(133, 21);
            this.m_cmbxSonntag.TabIndex = 9;
            // 
            // m_cmbxSamstag
            // 
            this.m_cmbxSamstag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cmbxSamstag.FormattingEnabled = true;
            this.m_cmbxSamstag.Location = new System.Drawing.Point(241, 217);
            this.m_cmbxSamstag.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxSamstag.Name = "m_cmbxSamstag";
            this.m_cmbxSamstag.Size = new System.Drawing.Size(133, 21);
            this.m_cmbxSamstag.TabIndex = 8;
            // 
            // m_cmbxFreitag
            // 
            this.m_cmbxFreitag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cmbxFreitag.FormattingEnabled = true;
            this.m_cmbxFreitag.Location = new System.Drawing.Point(241, 182);
            this.m_cmbxFreitag.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxFreitag.Name = "m_cmbxFreitag";
            this.m_cmbxFreitag.Size = new System.Drawing.Size(133, 21);
            this.m_cmbxFreitag.TabIndex = 7;
            // 
            // m_cmbxDonnerstag
            // 
            this.m_cmbxDonnerstag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cmbxDonnerstag.FormattingEnabled = true;
            this.m_cmbxDonnerstag.Location = new System.Drawing.Point(241, 147);
            this.m_cmbxDonnerstag.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxDonnerstag.Name = "m_cmbxDonnerstag";
            this.m_cmbxDonnerstag.Size = new System.Drawing.Size(133, 21);
            this.m_cmbxDonnerstag.TabIndex = 6;
            // 
            // m_cmbxMittwoch
            // 
            this.m_cmbxMittwoch.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cmbxMittwoch.FormattingEnabled = true;
            this.m_cmbxMittwoch.Location = new System.Drawing.Point(241, 112);
            this.m_cmbxMittwoch.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxMittwoch.Name = "m_cmbxMittwoch";
            this.m_cmbxMittwoch.Size = new System.Drawing.Size(133, 21);
            this.m_cmbxMittwoch.TabIndex = 5;
            // 
            // m_cmbxDienstag
            // 
            this.m_cmbxDienstag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cmbxDienstag.FormattingEnabled = true;
            this.m_cmbxDienstag.Location = new System.Drawing.Point(239, 77);
            this.m_cmbxDienstag.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxDienstag.Name = "m_cmbxDienstag";
            this.m_cmbxDienstag.Size = new System.Drawing.Size(137, 21);
            this.m_cmbxDienstag.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(72, 7);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 20);
            this.label10.TabIndex = 21;
            this.label10.Text = "Name:";
            // 
            // m_tbxName
            // 
            this.m_tbxName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_tbxName.Location = new System.Drawing.Point(237, 7);
            this.m_tbxName.Margin = new System.Windows.Forms.Padding(2);
            this.m_tbxName.Name = "m_tbxName";
            this.m_tbxName.Size = new System.Drawing.Size(140, 20);
            this.m_tbxName.TabIndex = 2;
            // 
            // m_cmbxMontag
            // 
            this.m_cmbxMontag.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cmbxMontag.FormattingEnabled = true;
            this.m_cmbxMontag.Location = new System.Drawing.Point(239, 42);
            this.m_cmbxMontag.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxMontag.Name = "m_cmbxMontag";
            this.m_cmbxMontag.Size = new System.Drawing.Size(137, 21);
            this.m_cmbxMontag.TabIndex = 3;
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(62, 287);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 20);
            this.label9.TabIndex = 20;
            this.label9.Text = "Gleitzeit:";
            // 
            // m_cbGleitzeit
            // 
            this.m_cbGleitzeit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cbGleitzeit.AutoSize = true;
            this.m_cbGleitzeit.Location = new System.Drawing.Point(300, 290);
            this.m_cbGleitzeit.Margin = new System.Windows.Forms.Padding(2);
            this.m_cbGleitzeit.Name = "m_cbGleitzeit";
            this.m_cbGleitzeit.Size = new System.Drawing.Size(15, 14);
            this.m_cbGleitzeit.TabIndex = 10;
            this.m_cbGleitzeit.UseVisualStyleBackColor = true;
            // 
            // m_btnTage
            // 
            this.m_btnTage.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnTage.Location = new System.Drawing.Point(152, 373);
            this.m_btnTage.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnTage.Name = "m_btnTage";
            this.m_btnTage.Size = new System.Drawing.Size(134, 38);
            this.m_btnTage.TabIndex = 15;
            this.m_btnTage.Text = "Tage";
            this.m_btnTage.UseVisualStyleBackColor = true;
            this.m_btnTage.Click += new System.EventHandler(this.BtnTage_Click);
            // 
            // m_btnErstellen
            // 
            this.m_btnErstellen.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnErstellen.Location = new System.Drawing.Point(290, 330);
            this.m_btnErstellen.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnErstellen.Name = "m_btnErstellen";
            this.m_btnErstellen.Size = new System.Drawing.Size(134, 38);
            this.m_btnErstellen.TabIndex = 13;
            this.m_btnErstellen.Text = "Erstellen";
            this.m_btnErstellen.UseVisualStyleBackColor = true;
            this.m_btnErstellen.Click += new System.EventHandler(this.BtnErstellen_Click);
            // 
            // m_btnNeu
            // 
            this.m_btnNeu.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnNeu.Location = new System.Drawing.Point(152, 330);
            this.m_btnNeu.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnNeu.Name = "m_btnNeu";
            this.m_btnNeu.Size = new System.Drawing.Size(134, 38);
            this.m_btnNeu.TabIndex = 12;
            this.m_btnNeu.Text = "Neu";
            this.m_btnNeu.UseVisualStyleBackColor = true;
            this.m_btnNeu.Click += new System.EventHandler(this.BtnNeu_Click);
            // 
            // m_btnAktualisieren
            // 
            this.m_btnAktualisieren.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAktualisieren.Location = new System.Drawing.Point(9, 330);
            this.m_btnAktualisieren.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnAktualisieren.Name = "m_btnAktualisieren";
            this.m_btnAktualisieren.Size = new System.Drawing.Size(134, 81);
            this.m_btnAktualisieren.TabIndex = 11;
            this.m_btnAktualisieren.Text = "Aktualisieren";
            this.m_btnAktualisieren.UseVisualStyleBackColor = true;
            this.m_btnAktualisieren.Click += new System.EventHandler(this.BtnAktualisieren_Click);
            // 
            // m_btnLöschen
            // 
            this.m_btnLöschen.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnLöschen.Location = new System.Drawing.Point(428, 330);
            this.m_btnLöschen.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnLöschen.Name = "m_btnLöschen";
            this.m_btnLöschen.Size = new System.Drawing.Size(134, 38);
            this.m_btnLöschen.TabIndex = 14;
            this.m_btnLöschen.Text = "Löschen";
            this.m_btnLöschen.UseVisualStyleBackColor = true;
            this.m_btnLöschen.Click += new System.EventHandler(this.BtnLöschen_Click);
            // 
            // DlgArbeitszeitprofile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 415);
            this.Controls.Add(this.m_btnLöschen);
            this.Controls.Add(this.m_btnAktualisieren);
            this.Controls.Add(this.m_btnNeu);
            this.Controls.Add(this.m_btnErstellen);
            this.Controls.Add(this.m_btnTage);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.m_btnAbbrechen);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_lbxArbeitszeitprofile);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DlgArbeitszeitprofile";
            this.Text = "Arbeitszeitprofile";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox m_lbxArbeitszeitprofile;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnAbbrechen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox m_cmbxSonntag;
        private System.Windows.Forms.ComboBox m_cmbxSamstag;
        private System.Windows.Forms.ComboBox m_cmbxFreitag;
        private System.Windows.Forms.ComboBox m_cmbxDonnerstag;
        private System.Windows.Forms.ComboBox m_cmbxMittwoch;
        private System.Windows.Forms.ComboBox m_cmbxDienstag;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox m_tbxName;
        private System.Windows.Forms.ComboBox m_cmbxMontag;
        private System.Windows.Forms.CheckBox m_cbGleitzeit;
        private System.Windows.Forms.Button m_btnTage;
        private System.Windows.Forms.Button m_btnErstellen;
        private System.Windows.Forms.Button m_btnNeu;
        private System.Windows.Forms.Button m_btnAktualisieren;
        private System.Windows.Forms.Button m_btnLöschen;
    }
}