using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeChip_App_1._0
{
    public partial class DlgMitarbeiter : Form
    {
        public DlgMitarbeiter()
        {
            InitializeComponent();
        }

        public string Vorname { get { return m_tbxVorname.Text; } set { m_tbxVorname.Text = value; } }
        public string Nachname { get { return m_tbxNachname.Text; } set { m_tbxNachname.Text = value; } }
        public ClsArbeitsprofil Arbeitzeitprofil { get { return m_cmbxAProfil.SelectedItem as ClsArbeitsprofil; } }
        public DateTime Arbeitsbeginn { get { return m_dtpArbeitsb.Value; } set { m_dtpArbeitsb.Value = value; } }
        public string Titel { get { return m_lblTitel.Text; } set { m_lblTitel.Text = value; } }
        public string OkKnopf { get { return m_btnOK.Text; } set { m_btnOK.Text = value; } }
    }
}
