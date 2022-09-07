
namespace TimeChip_App_1._0
{
    partial class DlgBuchung
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
            this.m_cmbxMitarbeiter = new System.Windows.Forms.ComboBox();
            this.m_dtpTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.m_btnOK = new System.Windows.Forms.Button();
            this.m_btnCancel = new System.Windows.Forms.Button();
            this.m_dtpDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.m_cmbxBTyp = new System.Windows.Forms.ComboBox();
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
            this.m_lblTitel.Size = new System.Drawing.Size(231, 38);
            this.m_lblTitel.TabIndex = 0;
            this.m_lblTitel.Text = "Neue Buchung";
            // 
            // label2
            // 
            this.label2.AllowDrop = true;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(9, 83);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 22);
            this.label2.TabIndex = 1;
            this.label2.Text = "Datum:";
            // 
            // m_cmbxMitarbeiter
            // 
            this.m_cmbxMitarbeiter.FormattingEnabled = true;
            this.m_cmbxMitarbeiter.Location = new System.Drawing.Point(241, 154);
            this.m_cmbxMitarbeiter.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxMitarbeiter.Name = "m_cmbxMitarbeiter";
            this.m_cmbxMitarbeiter.Size = new System.Drawing.Size(194, 21);
            this.m_cmbxMitarbeiter.TabIndex = 5;
            // 
            // m_dtpTime
            // 
            this.m_dtpTime.CustomFormat = "HH:MM";
            this.m_dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.m_dtpTime.Location = new System.Drawing.Point(377, 71);
            this.m_dtpTime.Margin = new System.Windows.Forms.Padding(2);
            this.m_dtpTime.Name = "m_dtpTime";
            this.m_dtpTime.ShowUpDown = true;
            this.m_dtpTime.Size = new System.Drawing.Size(58, 20);
            this.m_dtpTime.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AllowDrop = true;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(9, 151);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 22);
            this.label5.TabIndex = 8;
            this.label5.Text = "Mitarbeiter:";
            // 
            // m_btnOK
            // 
            this.m_btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.m_btnOK.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnOK.Location = new System.Drawing.Point(63, 224);
            this.m_btnOK.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnOK.Name = "m_btnOK";
            this.m_btnOK.Size = new System.Drawing.Size(126, 38);
            this.m_btnOK.TabIndex = 9;
            this.m_btnOK.Text = "Erstellen";
            this.m_btnOK.UseVisualStyleBackColor = true;
            // 
            // m_btnCancel
            // 
            this.m_btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.m_btnCancel.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_btnCancel.Location = new System.Drawing.Point(241, 224);
            this.m_btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.m_btnCancel.Name = "m_btnCancel";
            this.m_btnCancel.Size = new System.Drawing.Size(128, 38);
            this.m_btnCancel.TabIndex = 10;
            this.m_btnCancel.Text = "Abbrechen";
            this.m_btnCancel.UseVisualStyleBackColor = true;
            // 
            // m_dtpDate
            // 
            this.m_dtpDate.Location = new System.Drawing.Point(243, 95);
            this.m_dtpDate.Margin = new System.Windows.Forms.Padding(2);
            this.m_dtpDate.Name = "m_dtpDate";
            this.m_dtpDate.Size = new System.Drawing.Size(192, 20);
            this.m_dtpDate.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AllowDrop = true;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 120);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 22);
            this.label1.TabIndex = 15;
            this.label1.Text = "Buchungstyp:";
            // 
            // m_cmbxBTyp
            // 
            this.m_cmbxBTyp.FormattingEnabled = true;
            this.m_cmbxBTyp.Location = new System.Drawing.Point(242, 123);
            this.m_cmbxBTyp.Margin = new System.Windows.Forms.Padding(2);
            this.m_cmbxBTyp.Name = "m_cmbxBTyp";
            this.m_cmbxBTyp.Size = new System.Drawing.Size(194, 21);
            this.m_cmbxBTyp.TabIndex = 14;
            // 
            // clsArbeitsprofilBindingSource
            // 
            this.clsArbeitsprofilBindingSource.DataSource = typeof(TimeChip_App_1._0.ClsArbeitsprofil);
            // 
            // DlgBuchung
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(446, 285);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.m_cmbxBTyp);
            this.Controls.Add(this.m_dtpDate);
            this.Controls.Add(this.m_btnCancel);
            this.Controls.Add(this.m_btnOK);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.m_dtpTime);
            this.Controls.Add(this.m_cmbxMitarbeiter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.m_lblTitel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DlgBuchung";
            this.Text = "Neu";
            ((System.ComponentModel.ISupportInitialize)(this.clsArbeitsprofilBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_lblTitel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button m_btnOK;
        private System.Windows.Forms.Button m_btnCancel;
        private System.Windows.Forms.BindingSource clsArbeitsprofilBindingSource;
        private System.Windows.Forms.ComboBox m_cmbxMitarbeiter;
        private System.Windows.Forms.DateTimePicker m_dtpDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox m_cmbxBTyp;
        private System.Windows.Forms.DateTimePicker m_dtpTime;
    }
}