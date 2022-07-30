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
    public partial class FrmHaupt : Form
    {
        BindingList<ClsMitarbeiter> m_mitarbeiterliste = new BindingList<ClsMitarbeiter>();
        BindingList<ClsTag> m_tagesliste = new BindingList<ClsTag>();

        public FrmHaupt()
        {
            InitializeComponent();

            m_lbxMitarbeiter.DataSource = m_mitarbeiterliste;
            TimeSpan time = new TimeSpan(7, 30, 0);
            ClsTag tag = new ClsTag("test2",time, time, time, time, time, time);
            ClsArbeitsprofil test = new ClsArbeitsprofil("test", tag, tag, tag, tag, tag, tag, tag, 5, true);
            m_mitarbeiterliste.Add(new ClsMitarbeiter("Julian", "Wildt", test, new DateTime(2021,12,31)));
        }

        private void m_btnNeu_Click(object sender, EventArgs e)
        {
            DlgMitarbeiter neu = new DlgMitarbeiter();
            neu.Titel = "Neuer:e Mitarbeiter:in";
            neu.OkKnopf = "Erstellen";
            neu.Name = "Neu";
            if (neu.ShowDialog() == DialogResult.OK)
            {
                m_mitarbeiterliste.Add(new ClsMitarbeiter(neu.Vorname, neu.Nachname, neu.Arbeitzeitprofil, neu.Arbeitsbeginn));
            }
        }

        private void m_btnBearbeiten_Click(object sender, EventArgs e)
        {
            DlgMitarbeiter Bearbeiten = new DlgMitarbeiter();

            /*
            for(int i = 0; i<m_arbeitsprofilliste.Count; i++)
            {
                Bearbeiten.m_cmbxAProfil.Items.Add(m_arbeitsprofilliste[i]);
            }
            */

            ClsMitarbeiter zubearbeitender = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;
            Bearbeiten.Titel = "Mitarbeiter:in bearbeiten";
            Bearbeiten.OkKnopf = "Bearbeiten";
            Bearbeiten.Name = "Bearbeiten";
            Bearbeiten.Vorname = zubearbeitender.Vorname;
            Bearbeiten.Nachname = zubearbeitender.Nachname;
            Bearbeiten.Arbeitsbeginn = zubearbeitender.Arbeitsbeginn;
            Bearbeiten.m_cmbxAProfil.SelectedItem = zubearbeitender.Arbeitszeitprofil;

            if (Bearbeiten.ShowDialog() == DialogResult.OK)
            {
                zubearbeitender.Vorname = Bearbeiten.Vorname;
                zubearbeitender.Nachname = Bearbeiten.Nachname;
                zubearbeitender.Arbeitsbeginn = Bearbeiten.Arbeitsbeginn;
                zubearbeitender.Arbeitszeitprofil = Bearbeiten.Arbeitzeitprofil;

                m_mitarbeiterliste.ResetBindings();
            }
        }

        private void m_btnLöschen_Click(object sender, EventArgs e)
        {
            m_mitarbeiterliste.Remove(m_mitarbeiterliste.ElementAt<ClsMitarbeiter>(m_lbxMitarbeiter.SelectedIndex));
        }

        private void m_btnarbeitsprofil_Click(object sender, EventArgs e)
        {
            DlgArbeitszeitprofile arbeitszeitprofile = new DlgArbeitszeitprofile();
            arbeitszeitprofile.ShowDialog();
        }
    }
}
