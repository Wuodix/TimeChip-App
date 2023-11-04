
namespace TimeChip_App
{
    partial class DlgTag
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
            this.m_btnNeu = new System.Windows.Forms.Button();
            this.m_btnLöschen = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_dtpPausendauer = new System.Windows.Forms.DateTimePicker();
            this.m_dtpArbeitszeit = new System.Windows.Forms.DateTimePicker();
            this.m_dtpPausenende = new System.Windows.Forms.DateTimePicker();
            this.m_dtpPausenbeginn = new System.Windows.Forms.DateTimePicker();
            this.m_dtpArbeitsende = new System.Windows.Forms.DateTimePicker();
            this.m_dtpArbeitsbeginn = new System.Windows.Forms.DateTimePicker();
            this.label10 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.m_tbxName = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.m_cbPause = new System.Windows.Forms.CheckBox();
            this.m_btnAbbrechen = new System.Windows.Forms.Button();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_lbxTage = new System.Windows.Forms.ListBox();
            this.m_btnAktualisieren = new System.Windows.Forms.Button();
            this.m_btnErstellen = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // m_btnNeu
            // 
            this.m_btnNeu.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnNeu.Location = new System.Drawing.Point(159, 398);
            this.m_btnNeu.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnNeu.Name = "m_btnNeu";
            this.m_btnNeu.Size = new System.Drawing.Size(104, 38);
            this.m_btnNeu.TabIndex = 30;
            this.m_btnNeu.Text = "Neu";
            this.m_btnNeu.UseVisualStyleBackColor = true;
            this.m_btnNeu.Click += new System.EventHandler(this.BtnNeu_Click);
            // 
            // m_btnLöschen
            // 
            this.m_btnLöschen.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnLöschen.Location = new System.Drawing.Point(368, 398);
            this.m_btnLöschen.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnLöschen.Name = "m_btnLöschen";
            this.m_btnLöschen.Size = new System.Drawing.Size(97, 38);
            this.m_btnLöschen.TabIndex = 29;
            this.m_btnLöschen.Text = "Löschen";
            this.m_btnLöschen.UseVisualStyleBackColor = true;
            this.m_btnLöschen.Click += new System.EventHandler(this.BtnLöschen_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.7537F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.2463F));
            this.tableLayoutPanel1.Controls.Add(this.m_dtpPausendauer, 1, 7);
            this.tableLayoutPanel1.Controls.Add(this.m_dtpArbeitszeit, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.m_dtpPausenende, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.m_dtpPausenbeginn, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.m_dtpArbeitsende, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.m_dtpArbeitsbeginn, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_tbxName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 7);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label7, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.m_cbPause, 1, 3);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(159, 10);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 8;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54854F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54854F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54854F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 11.66623F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 13.03873F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54854F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.54854F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.55231F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(304, 368);
            this.tableLayoutPanel1.TabIndex = 27;
            // 
            // m_dtpPausendauer
            // 
            this.m_dtpPausendauer.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_dtpPausendauer.CustomFormat = "HH:mm";
            this.m_dtpPausendauer.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpPausendauer.Location = new System.Drawing.Point(201, 333);
            this.m_dtpPausendauer.Margin = new System.Windows.Forms.Padding(2);
            this.m_dtpPausendauer.Name = "m_dtpPausendauer";
            this.m_dtpPausendauer.ShowUpDown = true;
            this.m_dtpPausendauer.Size = new System.Drawing.Size(53, 20);
            this.m_dtpPausendauer.TabIndex = 36;
            this.m_dtpPausendauer.Value = new System.DateTime(2022, 12, 31, 0, 0, 0, 0);
            // 
            // m_dtpArbeitszeit
            // 
            this.m_dtpArbeitszeit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_dtpArbeitszeit.CustomFormat = "HH:mm";
            this.m_dtpArbeitszeit.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpArbeitszeit.Location = new System.Drawing.Point(201, 286);
            this.m_dtpArbeitszeit.Margin = new System.Windows.Forms.Padding(2);
            this.m_dtpArbeitszeit.Name = "m_dtpArbeitszeit";
            this.m_dtpArbeitszeit.ShowUpDown = true;
            this.m_dtpArbeitszeit.Size = new System.Drawing.Size(53, 20);
            this.m_dtpArbeitszeit.TabIndex = 35;
            this.m_dtpArbeitszeit.Value = new System.DateTime(2022, 12, 31, 0, 0, 0, 0);
            // 
            // m_dtpPausenende
            // 
            this.m_dtpPausenende.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_dtpPausenende.CustomFormat = "HH:mm";
            this.m_dtpPausenende.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpPausenende.Location = new System.Drawing.Point(201, 240);
            this.m_dtpPausenende.Margin = new System.Windows.Forms.Padding(2);
            this.m_dtpPausenende.Name = "m_dtpPausenende";
            this.m_dtpPausenende.ShowUpDown = true;
            this.m_dtpPausenende.Size = new System.Drawing.Size(53, 20);
            this.m_dtpPausenende.TabIndex = 34;
            this.m_dtpPausenende.Value = new System.DateTime(2022, 12, 31, 0, 0, 0, 0);
            // 
            // m_dtpPausenbeginn
            // 
            this.m_dtpPausenbeginn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_dtpPausenbeginn.CustomFormat = "HH:mm";
            this.m_dtpPausenbeginn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpPausenbeginn.Location = new System.Drawing.Point(201, 193);
            this.m_dtpPausenbeginn.Margin = new System.Windows.Forms.Padding(2);
            this.m_dtpPausenbeginn.Name = "m_dtpPausenbeginn";
            this.m_dtpPausenbeginn.ShowUpDown = true;
            this.m_dtpPausenbeginn.Size = new System.Drawing.Size(53, 20);
            this.m_dtpPausenbeginn.TabIndex = 33;
            this.m_dtpPausenbeginn.Value = new System.DateTime(2022, 12, 31, 0, 0, 0, 0);
            // 
            // m_dtpArbeitsende
            // 
            this.m_dtpArbeitsende.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_dtpArbeitsende.CustomFormat = "HH:mm";
            this.m_dtpArbeitsende.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpArbeitsende.Location = new System.Drawing.Point(201, 105);
            this.m_dtpArbeitsende.Margin = new System.Windows.Forms.Padding(2);
            this.m_dtpArbeitsende.Name = "m_dtpArbeitsende";
            this.m_dtpArbeitsende.ShowUpDown = true;
            this.m_dtpArbeitsende.Size = new System.Drawing.Size(53, 20);
            this.m_dtpArbeitsende.TabIndex = 32;
            this.m_dtpArbeitsende.Value = new System.DateTime(2022, 12, 31, 0, 0, 0, 0);
            // 
            // m_dtpArbeitsbeginn
            // 
            this.m_dtpArbeitsbeginn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_dtpArbeitsbeginn.CustomFormat = "HH:mm";
            this.m_dtpArbeitsbeginn.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpArbeitsbeginn.Location = new System.Drawing.Point(201, 59);
            this.m_dtpArbeitsbeginn.Margin = new System.Windows.Forms.Padding(2);
            this.m_dtpArbeitsbeginn.Name = "m_dtpArbeitsbeginn";
            this.m_dtpArbeitsbeginn.ShowUpDown = true;
            this.m_dtpArbeitsbeginn.Size = new System.Drawing.Size(53, 20);
            this.m_dtpArbeitsbeginn.TabIndex = 31;
            this.m_dtpArbeitsbeginn.Value = new System.DateTime(2022, 12, 31, 0, 0, 0, 0);
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(45, 13);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(60, 20);
            this.label10.TabIndex = 21;
            this.label10.Text = "Name:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 105);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Arbeitsende:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 59);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 12;
            this.label1.Text = "Arbeitsbeginn:";
            // 
            // m_tbxName
            // 
            this.m_tbxName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_tbxName.Location = new System.Drawing.Point(153, 13);
            this.m_tbxName.Margin = new System.Windows.Forms.Padding(2);
            this.m_tbxName.Name = "m_tbxName";
            this.m_tbxName.Size = new System.Drawing.Size(149, 20);
            this.m_tbxName.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 333);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 20);
            this.label6.TabIndex = 17;
            this.label6.Text = "Pausendauer:";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 286);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "Arbeitszeit:";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(18, 240);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(114, 20);
            this.label4.TabIndex = 15;
            this.label4.Text = "Pausenende:";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(43, 149);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 20);
            this.label7.TabIndex = 29;
            this.label7.Text = "Pause:";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(11, 193);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 20);
            this.label3.TabIndex = 14;
            this.label3.Text = "Pausenbeginn:";
            // 
            // m_cbPause
            // 
            this.m_cbPause.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.m_cbPause.AutoSize = true;
            this.m_cbPause.Location = new System.Drawing.Point(220, 152);
            this.m_cbPause.Name = "m_cbPause";
            this.m_cbPause.Size = new System.Drawing.Size(15, 14);
            this.m_cbPause.TabIndex = 30;
            this.m_cbPause.UseVisualStyleBackColor = true;
            // 
            // m_btnAbbrechen
            // 
            this.m_btnAbbrechen.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnAbbrechen.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAbbrechen.Location = new System.Drawing.Point(314, 441);
            this.m_btnAbbrechen.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnAbbrechen.Name = "m_btnAbbrechen";
            this.m_btnAbbrechen.Size = new System.Drawing.Size(150, 38);
            this.m_btnAbbrechen.TabIndex = 26;
            this.m_btnAbbrechen.Text = "Abbrechen";
            this.m_btnAbbrechen.UseVisualStyleBackColor = true;
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOK.Location = new System.Drawing.Point(159, 441);
            this.m_btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(150, 38);
            this.m_btnOK.TabIndex = 25;
            this.m_btnOK.Text = "OK";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // m_lbxTage
            // 
            this.m_lbxTage.FormattingEnabled = true;
            this.m_lbxTage.Location = new System.Drawing.Point(9, 10);
            this.m_lbxTage.Margin = new System.Windows.Forms.Padding(2);
            this.m_lbxTage.Name = "m_lbxTage";
            this.m_lbxTage.Size = new System.Drawing.Size(134, 368);
            this.m_lbxTage.TabIndex = 1;
            this.m_lbxTage.SelectedIndexChanged += new System.EventHandler(this.LbxTage_SelectedIndexChanged);
            // 
            // m_btnAktualisieren
            // 
            this.m_btnAktualisieren.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAktualisieren.Location = new System.Drawing.Point(9, 398);
            this.m_btnAktualisieren.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnAktualisieren.Name = "m_btnAktualisieren";
            this.m_btnAktualisieren.Size = new System.Drawing.Size(134, 81);
            this.m_btnAktualisieren.TabIndex = 32;
            this.m_btnAktualisieren.Text = "Aktualisieren";
            this.m_btnAktualisieren.UseVisualStyleBackColor = true;
            this.m_btnAktualisieren.Click += new System.EventHandler(this.BtnAktualisieren_Click);
            // 
            // m_btnErstellen
            // 
            this.m_btnErstellen.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnErstellen.Location = new System.Drawing.Point(267, 398);
            this.m_btnErstellen.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnErstellen.Name = "m_btnErstellen";
            this.m_btnErstellen.Size = new System.Drawing.Size(97, 38);
            this.m_btnErstellen.TabIndex = 33;
            this.m_btnErstellen.Text = "Erstellen";
            this.m_btnErstellen.UseVisualStyleBackColor = true;
            this.m_btnErstellen.Click += new System.EventHandler(this.BtnErstellen_Click);
            // 
            // DlgTag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(502, 490);
            this.Controls.Add(this.m_btnErstellen);
            this.Controls.Add(this.m_btnAktualisieren);
            this.Controls.Add(this.m_btnNeu);
            this.Controls.Add(this.m_btnLöschen);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.m_btnAbbrechen);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.m_lbxTage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DlgTag";
            this.Text = "Tage";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button m_btnNeu;
        private System.Windows.Forms.Button m_btnLöschen;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbxName;
        private System.Windows.Forms.Button m_btnAbbrechen;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.ListBox m_lbxTage;
        private System.Windows.Forms.Button m_btnAktualisieren;
        private System.Windows.Forms.Button m_btnErstellen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox m_cbPause;
        private System.Windows.Forms.DateTimePicker m_dtpPausendauer;
        private System.Windows.Forms.DateTimePicker m_dtpArbeitszeit;
        private System.Windows.Forms.DateTimePicker m_dtpPausenende;
        private System.Windows.Forms.DateTimePicker m_dtpPausenbeginn;
        private System.Windows.Forms.DateTimePicker m_dtpArbeitsende;
        private System.Windows.Forms.DateTimePicker m_dtpArbeitsbeginn;
    }
}