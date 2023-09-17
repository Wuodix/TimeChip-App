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
        }

        public Buchungstyp Buchungstyp { get { return IndexToBuchungstyp(m_cmbxBTyp.SelectedIndex); } set { m_cmbxBTyp.SelectedItem = value; } }
        public string Titel { get { return m_lblTitel.Text; } set { m_lblTitel.Text = value; } }
        public string OkKnopf { get { return m_btnOK.Text; } set { m_btnOK.Text = value; } }
        public ClsMitarbeiter Mitarbeiter { get { return m_cmbxMitarbeiter.SelectedItem as ClsMitarbeiter; } set { m_cmbxMitarbeiter.SelectedItem = value; } }
        public DateTime Datum { get { return GetDateTime(); } set { SetDateTime(value); } }

        public DateTime GetDateTime()
        {
            return new DateTime(m_dtpDate.Value.Year, m_dtpDate.Value.Month, m_dtpDate.Value.Day, m_dtpTime.Value.Hour, m_dtpTime.Value.Minute, m_dtpTime.Value.Second);
        }

        public void SetDateTime(DateTime datetime)
        {
            m_dtpDate.Value = datetime;
            m_dtpTime.Value = datetime;
        }

        public void SetBuchungstyp(Buchungstyp buchungstyp)
        {
            m_cmbxBTyp.SelectedText = buchungstyp.ToString();
        }

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
