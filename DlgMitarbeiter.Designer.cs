﻿
namespace TimeChip_App
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
            this.label4 = new System.Windows.Forms.Label();
            this.m_dtpArbeitsb = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_btnAddFinger = new System.Windows.Forms.Button();
            this.m_btnAddCard = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.m_tbxUrlaub = new System.Windows.Forms.TextBox();
            this.m_tbxÜberstunden = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.m_lbxFinger = new System.Windows.Forms.ListBox();
            this.m_btnDeleteFinger = new System.Windows.Forms.Button();
            this.clsArbeitsprofilBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.clsArbeitsprofilBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // m_lblTitel
            // 
            this.m_lblTitel.AutoSize = true;
            this.m_lblTitel.Font = new System.Drawing.Font("Arial Black", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_lblTitel.Location = new System.Drawing.Point(7, 7);
            this.m_lblTitel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.m_lblTitel.Name = "m_lblTitel";
            this.m_lblTitel.Size = new System.Drawing.Size(323, 38);
            this.m_lblTitel.TabIndex = 0;
            this.m_lblTitel.Text = "Neue:r Mitarbeiter:in";
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 69);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Vorname:";
            // 
            // m_tbxVorname
            // 
            this.m_tbxVorname.Location = new System.Drawing.Point(243, 66);
            this.m_tbxVorname.Margin = new System.Windows.Forms.Padding(2);
            this.m_tbxVorname.Multiline = true;
            this.m_tbxVorname.Name = "m_tbxVorname";
            this.m_tbxVorname.Size = new System.Drawing.Size(194, 24);
            this.m_tbxVorname.TabIndex = 1;
            // 
            // m_tbxNachname
            // 
            this.m_tbxNachname.Location = new System.Drawing.Point(243, 98);
            this.m_tbxNachname.Margin = new System.Windows.Forms.Padding(2);
            this.m_tbxNachname.Multiline = true;
            this.m_tbxNachname.Name = "m_tbxNachname";
            this.m_tbxNachname.Size = new System.Drawing.Size(194, 24);
            this.m_tbxNachname.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AllowDrop = true;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 101);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 22);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nachname:";
            // 
            // m_cmbxAProfil
            // 
            this.m_cmbxAProfil.FormattingEnabled = true;
            this.m_cmbxAProfil.Location = new System.Drawing.Point(243, 225);
            this.m_cmbxAProfil.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxAProfil.Name = "m_cmbxAProfil";
            this.m_cmbxAProfil.Size = new System.Drawing.Size(194, 21);
            this.m_cmbxAProfil.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AllowDrop = true;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 222);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 22);
            this.label4.TabIndex = 6;
            this.label4.Text = "Arbeitszeitprofil:";
            // 
            // m_dtpArbeitsb
            // 
            this.m_dtpArbeitsb.Location = new System.Drawing.Point(243, 257);
            this.m_dtpArbeitsb.Margin = new System.Windows.Forms.Padding(2);
            this.m_dtpArbeitsb.Name = "m_dtpArbeitsb";
            this.m_dtpArbeitsb.Size = new System.Drawing.Size(194, 20);
            this.m_dtpArbeitsb.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AllowDrop = true;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 257);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(132, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Arbeitsbeginn:";
            // 
            // m_btnOK
            // 
            this.m_btnOK.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOK.Location = new System.Drawing.Point(65, 438);
            this.m_btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(126, 38);
            this.m_btnOK.TabIndex = 9;
            this.m_btnOK.Text = "Erstellen";
            this.m_btnOK.UseVisualStyleBackColor = true;
            this.m_btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnCancel.Location = new System.Drawing.Point(243, 438);
            this.m_btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(128, 38);
            this.m_btnCancel.TabIndex = 10;
            this.m_btnCancel.Text = "Abbrechen";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_btnAddFinger
            // 
            this.m_btnAddFinger.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAddFinger.Location = new System.Drawing.Point(13, 312);
            this.m_btnAddFinger.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnAddFinger.Name = "m_btnAddFinger";
            this.m_btnAddFinger.Size = new System.Drawing.Size(220, 31);
            this.m_btnAddFinger.TabIndex = 7;
            this.m_btnAddFinger.Text = "Finger hinzufügen";
            this.m_btnAddFinger.UseVisualStyleBackColor = true;
            this.m_btnAddFinger.Click += new System.EventHandler(this.BtnAddFinger_Click);
            // 
            // m_btnAddCard
            // 
            this.m_btnAddCard.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnAddCard.Location = new System.Drawing.Point(14, 361);
            this.m_btnAddCard.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnAddCard.Name = "m_btnAddCard";
            this.m_btnAddCard.Size = new System.Drawing.Size(220, 31);
            this.m_btnAddCard.TabIndex = 8;
            this.m_btnAddCard.Text = "NFC Karte hinzufügen";
            this.m_btnAddCard.UseVisualStyleBackColor = true;
            this.m_btnAddCard.Click += new System.EventHandler(this.BtnAddCard_Click);
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 187);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 22);
            this.label1.TabIndex = 13;
            this.label1.Text = "Urlaub:";
            // 
            // m_tbxUrlaub
            // 
            this.m_tbxUrlaub.Location = new System.Drawing.Point(379, 187);
            this.m_tbxUrlaub.Margin = new System.Windows.Forms.Padding(2);
            this.m_tbxUrlaub.Multiline = true;
            this.m_tbxUrlaub.Name = "m_tbxUrlaub";
            this.m_tbxUrlaub.Size = new System.Drawing.Size(58, 24);
            this.m_tbxUrlaub.TabIndex = 4;
            // 
            // m_tbxÜberstunden
            // 
            this.m_tbxÜberstunden.Location = new System.Drawing.Point(379, 155);
            this.m_tbxÜberstunden.Margin = new System.Windows.Forms.Padding(2);
            this.m_tbxÜberstunden.Multiline = true;
            this.m_tbxÜberstunden.Name = "m_tbxÜberstunden";
            this.m_tbxÜberstunden.Size = new System.Drawing.Size(58, 24);
            this.m_tbxÜberstunden.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AllowDrop = true;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(9, 155);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(123, 22);
            this.label6.TabIndex = 15;
            this.label6.Text = "Überstunden:";
            // 
            // m_lbxFinger
            // 
            this.m_lbxFinger.FormattingEnabled = true;
            this.m_lbxFinger.Location = new System.Drawing.Point(243, 295);
            this.m_lbxFinger.Name = "m_lbxFinger";
            this.m_lbxFinger.Size = new System.Drawing.Size(194, 95);
            this.m_lbxFinger.TabIndex = 16;
            // 
            // m_btnDeleteFinger
            // 
            this.m_btnDeleteFinger.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnDeleteFinger.Location = new System.Drawing.Point(243, 395);
            this.m_btnDeleteFinger.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnDeleteFinger.Name = "m_btnDeleteFinger";
            this.m_btnDeleteFinger.Size = new System.Drawing.Size(194, 31);
            this.m_btnDeleteFinger.TabIndex = 17;
            this.m_btnDeleteFinger.Text = "Finger löschen";
            this.m_btnDeleteFinger.UseVisualStyleBackColor = true;
            this.m_btnDeleteFinger.Click += new System.EventHandler(this.BtnDeleteFinger_Click);
            // 
            // clsArbeitsprofilBindingSource
            // 
            this.clsArbeitsprofilBindingSource.DataSource = typeof(TimeChip_App.ClsArbeitsprofil);
            // 
            // DlgMitarbeiter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 487);
            this.Controls.Add(this.m_btnDeleteFinger);
            this.Controls.Add(this.m_lbxFinger);
            this.Controls.Add(this.m_tbxÜberstunden);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.m_tbxUrlaub);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_btnAddCard);
            this.Controls.Add(this.m_btnAddFinger);
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
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DlgMitarbeiter";
            this.Text = "Neu";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.DlgMitarbeiter_MouseMove);
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
        private System.Windows.Forms.Button m_btnAddFinger;
        private System.Windows.Forms.Button m_btnAddCard;
        private System.Windows.Forms.ComboBox m_cmbxAProfil;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_tbxUrlaub;
        private System.Windows.Forms.TextBox m_tbxÜberstunden;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox m_lbxFinger;
        private System.Windows.Forms.Button m_btnDeleteFinger;
    }
}