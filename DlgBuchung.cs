using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace TimeChip_App
{
    public partial class DlgBuchung : Form
    {
        public DlgBuchung()
        {
            InitializeComponent();

            m_cmbxMitarbeiter.Items.Clear();
            m_cmbxMitarbeiter.Items.AddRange(FrmHaupt.Mitarbeiterliste.ToArray());

            m_cmbxBTyp.Items.Clear();
            m_cmbxBTyp.Items.Add(Buchungstyp.Kommen);
            m_cmbxBTyp.Items.Add(Buchungstyp.Gehen);

            m_dtpTime.TabIndex = 1;
            m_dtpDate.TabIndex = 2;
            m_cmbxBTyp.TabIndex = 3;
            m_cmbxMitarbeiter.TabIndex = 4;
            m_btnOK.TabIndex = 5;
            m_btnCancel.TabIndex = 6;
        }

        public Buchungstyp Buchungstyp { get { return IndexToBuchungstyp(m_cmbxBTyp.SelectedIndex); } set { m_cmbxBTyp.SelectedItem = value; } }
        public string Titel { get { return m_lblTitel.Text; } set { m_lblTitel.Text = value; } }
        public string OkKnopf { get { return m_btnOK.Text; } set { m_btnOK.Text = value; } }
        public ClsMitarbeiter Mitarbeiter { get { return m_cmbxMitarbeiter.SelectedItem as ClsMitarbeiter; } set { m_cmbxMitarbeiter.SelectedItem = value; } }
        public DateTime Datum { get { return GetDateTime(); } set { SetDateTime(value); } }

        /// <summary>
        /// Liest die Daten aus den DateTimePickern aus und setzt daraus ein DateTime-Objekt zusammen
        /// </summary>
        /// <returns>Das neu erstellte DateTime Objekt</returns>
        public DateTime GetDateTime()
        {
            return new DateTime(m_dtpDate.Value.Year, m_dtpDate.Value.Month, m_dtpDate.Value.Day, m_dtpTime.Value.Hour, m_dtpTime.Value.Minute, m_dtpTime.Value.Second);
        }

        /// <summary>
        /// Setzt die Werte der DateTimePicker auf den Wert von datetime
        /// </summary>
        /// <param name="datetime"></param>
        public void SetDateTime(DateTime datetime)
        {
            m_dtpDate.Value = datetime;
            m_dtpTime.Value = datetime;
        }

        /// <summary>
        /// Setzt den Wert der Combobox zur Auswahl des Buchungstyps auf den Wert von buchungstyp
        /// </summary>
        /// <param name="buchungstyp"></param>
        public void SetBuchungstyp(Buchungstyp buchungstyp)
        {
            m_cmbxBTyp.SelectedText = buchungstyp.ToString();
        }

        /// <summary>
        /// Wandelt den ausgewählten Index der Buchungstyp Combobox in ein Buchungstyp-Objekt um
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Buchungstyp IndexToBuchungstyp(int index)
        {
            switch (index)
            {
                case 0:
                    return Buchungstyp.Kommen;
                default:
                    return Buchungstyp.Gehen;
            }
        }
    }
}
