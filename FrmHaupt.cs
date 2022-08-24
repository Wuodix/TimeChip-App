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

            UpdateMtbtrList();
        }

        private void m_btnNeu_Click(object sender, EventArgs e)
        {
            DlgMitarbeiter neu = new DlgMitarbeiter();
            neu.Titel = "Neuer:e Mitarbeiter:in";
            neu.OkKnopf = "Erstellen";
            neu.Name = "Neu";
            if (neu.ShowDialog() == DialogResult.OK)
            {
                DataProvider.InsertMitarbeiter(1,neu.Vorname, neu.Nachname, neu.Arbeitsbeginn, new TimeSpan(0), neu.Arbeitzeitprofil, new TimeSpan(0));
                UpdateMtbtrList();
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
                ClsMitarbeiter oldmtbtr = zubearbeitender;
                zubearbeitender.Vorname = Bearbeiten.Vorname;
                zubearbeitender.Nachname = Bearbeiten.Nachname;
                zubearbeitender.Arbeitsbeginn = Bearbeiten.Arbeitsbeginn;
                zubearbeitender.Arbeitszeitprofil = Bearbeiten.Arbeitzeitprofil;

                DataProvider.UpdateMitarbeiter(zubearbeitender);
                m_mitarbeiterliste.ResetBindings();
            }
        }

        private void m_btnLöschen_Click(object sender, EventArgs e)
        {
            DataProvider.DeleteMitarbeiter(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter);
            UpdateMtbtrList();
        }

        private void m_btnarbeitsprofil_Click(object sender, EventArgs e)
        {
            DlgArbeitszeitprofile arbeitszeitprofile = new DlgArbeitszeitprofile();
            arbeitszeitprofile.ShowDialog();
        }

        private void UpdateMtbtrList()
        {
            m_mitarbeiterliste.Clear();

            foreach(ClsMitarbeiter mtbtr in DataProvider.SelectAllMitarbeiter())
            {
                m_mitarbeiterliste.Add(mtbtr);
            }

            m_mitarbeiterliste.ResetBindings();
        }
    }
}
