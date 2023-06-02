using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace TimeChip_App
{
    public partial class DlgArbeitszeitprofile : Form
    {
        static BindingList<ClsArbeitsprofil> m_arbeitsprofilliste = new BindingList<ClsArbeitsprofil>();
        public DlgArbeitszeitprofile()
        {
            InitializeComponent();

            UpdateCMBX();
            UpdateAbzpList();

            m_lbxArbeitszeitprofile.DataSource = m_arbeitsprofilliste;

            if (m_arbeitsprofilliste.Count > 0)
            {
                m_lbxArbeitszeitprofile.SelectedIndex = 0;
            }
        }

        public static BindingList<ClsArbeitsprofil> ArbeitsprofilListe { get { return m_arbeitsprofilliste; } set { m_arbeitsprofilliste = value; } }

        private void BtnTage_Click(object sender, EventArgs e)
        {
            DlgTag Tag = new DlgTag();
            if(Tag.ShowDialog() == DialogResult.OK)
            {
                UpdateCMBX();
            }
        }

        private void BtnAktualisieren_Click(object sender, EventArgs e)
        {
            ClsArbeitsprofil Aktualisieren = m_lbxArbeitszeitprofile.SelectedItem as ClsArbeitsprofil;

            Aktualisieren.Name = m_tbxName.Text;
            Aktualisieren.Montag = m_cmbxMontag.SelectedItem as ClsTag;
            Aktualisieren.Dienstag = m_cmbxDienstag.SelectedItem as ClsTag;
            Aktualisieren.Mittwoch = m_cmbxMittwoch.SelectedItem as ClsTag;
            Aktualisieren.Donnerstag = m_cmbxDonnerstag.SelectedItem as ClsTag;
            Aktualisieren.Freitag = m_cmbxFreitag.SelectedItem as ClsTag;
            Aktualisieren.Samstag = m_cmbxSamstag.SelectedItem as ClsTag;
            Aktualisieren.Sonntag = m_cmbxSonntag.SelectedItem as ClsTag;
            Aktualisieren.Gleitzeit = m_cbGleitzeit.Checked;

            DataProvider.UpdateArbeitszeitprofil(Aktualisieren);

            UpdateAbzpList();

            m_lbxArbeitszeitprofile.SelectedItem = m_arbeitsprofilliste.ToList().Find(x=>x.ID == Aktualisieren.ID);
        }

        private void BtnNeu_Click(object sender, EventArgs e)
        {
            m_tbxName.Text = "";
            m_cmbxMontag.Text = "";
            m_cmbxDienstag.Text = "";
            m_cmbxMittwoch.Text = "";
            m_cmbxDonnerstag.Text = "";
            m_cmbxFreitag.Text = "";
            m_cmbxSamstag.Text = "";
            m_cmbxSonntag.Text = "";
            m_cbGleitzeit.Checked = false;
        }

        private void BtnErstellen_Click(object sender, EventArgs e)
        {
            ClsArbeitsprofil abzp = DataProvider.InsertArbeitszeitprofil(m_tbxName.Text, m_cmbxMontag.SelectedItem as ClsTag,
                m_cmbxDienstag.SelectedItem as ClsTag, m_cmbxMittwoch.SelectedItem as ClsTag,
                m_cmbxDonnerstag.SelectedItem as ClsTag, m_cmbxFreitag.SelectedItem as ClsTag,
                m_cmbxSamstag.SelectedItem as ClsTag, m_cmbxSonntag.SelectedItem as ClsTag, m_cbGleitzeit.Checked);
            
            UpdateAbzpList();

            m_tbxName.Text = "";
            m_cmbxMontag.Text = "";
            m_cmbxDienstag.Text = "";
            m_cmbxMittwoch.Text = "";
            m_cmbxDonnerstag.Text = "";
            m_cmbxFreitag.Text = "";
            m_cmbxSamstag.Text = "";
            m_cmbxSonntag.Text = "";
            m_cbGleitzeit.Checked = false;

            m_lbxArbeitszeitprofile.SelectedItem = m_arbeitsprofilliste.ToList().Find(x => x.ID == abzp.ID);
        }

        private void BtnLöschen_Click(object sender, EventArgs e)
        {
            ClsArbeitsprofil abzp = m_lbxArbeitszeitprofile.SelectedItem as ClsArbeitsprofil;
            List<ClsMitarbeiter> mtbtrs = DataProvider.SelectAllMitarbeiter();

            bool fehler = false;

            foreach (ClsMitarbeiter mtbtr in mtbtrs)
            {
                if (mtbtr.Arbeitszeitprofil.ID == abzp.ID) { fehler = true; break; }
            }

            if (fehler)
            {
                MessageBox.Show("Das Arbeitszeitprofil wird noch verwendet und kann deshalb nicht gelöscht werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Wollen Sie wirklich das Arbeitszeitprofil löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataProvider.DeleteArbeitszeitprofil(abzp);
                UpdateAbzpList();
            }
        }

        private void UpdateAbzpList()
        {
            m_arbeitsprofilliste.Clear();

            List<ClsArbeitsprofil> alleabzps = DataProvider.SelectAllArbeitszeitprofil();

            foreach (ClsArbeitsprofil abzp in alleabzps)
            {
                m_arbeitsprofilliste.Add(abzp);
            }

            m_arbeitsprofilliste.ResetBindings();
        }
        private void UpdateCMBX()
        {
            m_cmbxMontag.Items.Clear();
            m_cmbxDienstag.Items.Clear();
            m_cmbxMittwoch.Items.Clear();
            m_cmbxDonnerstag.Items.Clear();
            m_cmbxFreitag.Items.Clear();
            m_cmbxSamstag.Items.Clear();
            m_cmbxSonntag.Items.Clear();

            m_cmbxMontag.Items.AddRange(DlgTag.Tagesliste.ToArray());
            m_cmbxDienstag.Items.AddRange(DlgTag.Tagesliste.ToArray());
            m_cmbxMittwoch.Items.AddRange(DlgTag.Tagesliste.ToArray());
            m_cmbxDonnerstag.Items.AddRange(DlgTag.Tagesliste.ToArray());
            m_cmbxFreitag.Items.AddRange(DlgTag.Tagesliste.ToArray());
            m_cmbxSamstag.Items.AddRange(DlgTag.Tagesliste.ToArray());
            m_cmbxSonntag.Items.AddRange(DlgTag.Tagesliste.ToArray());

            /*
            for (int i = 0; i < DlgTag.Tagesliste.Count; i++)
            {
                m_cmbxMontag.Items.Add(DlgTag.Tagesliste[i]);
                m_cmbxDienstag.Items.Add(DlgTag.Tagesliste[i]);
                m_cmbxMittwoch.Items.Add(DlgTag.Tagesliste[i]);
                m_cmbxDonnerstag.Items.Add(DlgTag.Tagesliste[i]);
                m_cmbxFreitag.Items.Add(DlgTag.Tagesliste[i]);
                m_cmbxSamstag.Items.Add(DlgTag.Tagesliste[i]);
                m_cmbxSonntag.Items.Add(DlgTag.Tagesliste[i]);
            }
            */
        }
        int i = 0;
        private void LbxArbeitszeitprofile_SelectedIndexChanged(object sender, EventArgs e)
        {   
            if(m_arbeitsprofilliste.Count >0)
            {
                Debug.WriteLine("Selected Index Changed ausgelöst!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! " + i);
                i++;

                ClsArbeitsprofil Auswählen = m_lbxArbeitszeitprofile.SelectedItem as ClsArbeitsprofil;

                m_tbxName.Text = Auswählen.Name;

                if (m_cmbxMontag.Items.Count > 0)
                {
                    UpdateCMBX();
                    
                    m_cmbxMontag.SelectedItem = DlgTag.Tagesliste.ToList().Find(x => x.ID.Equals(Auswählen.Montag.ID));
                    m_cmbxDienstag.SelectedItem = DlgTag.Tagesliste.ToList().Find(x => x.ID.Equals(Auswählen.Dienstag.ID));
                    m_cmbxMittwoch.SelectedItem = DlgTag.Tagesliste.ToList().Find(x => x.ID.Equals(Auswählen.Mittwoch.ID));
                    m_cmbxDonnerstag.SelectedItem = DlgTag.Tagesliste.ToList().Find(x => x.ID.Equals(Auswählen.Donnerstag.ID));
                    m_cmbxFreitag.SelectedItem = DlgTag.Tagesliste.ToList().Find(x => x.ID.Equals(Auswählen.Freitag.ID));
                    m_cmbxSamstag.SelectedItem = DlgTag.Tagesliste.ToList().Find(x => x.ID.Equals(Auswählen.Samstag.ID));
                    m_cmbxSonntag.SelectedItem = DlgTag.Tagesliste.ToList().Find(x => x.ID.Equals(Auswählen.Sonntag.ID));
                    m_cbGleitzeit.Checked = Auswählen.Gleitzeit;
                }
            }
        }
    }
}
