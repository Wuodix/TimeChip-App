using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeChip_App_1._0
{
    public partial class DlgArbeitszeitprofile : Form
    {
        static BindingList<ClsArbeitsprofil> m_arbeitsprofilliste = new BindingList<ClsArbeitsprofil>();
        public DlgArbeitszeitprofile()
        {
            InitializeComponent();

            m_lbxArbeitszeitprofile.DataSource = m_arbeitsprofilliste;

            UpdateCMBX();
            UpdateAbzpList();
        }

        public static BindingList<ClsArbeitsprofil> ArbeitsprofilListe { get { return m_arbeitsprofilliste; } set { m_arbeitsprofilliste = value; } }

        private void m_btnTage_Click(object sender, EventArgs e)
        {
            DlgTag Tag = new DlgTag();
            if(Tag.ShowDialog() == DialogResult.OK)
            {
                UpdateCMBX();
            }
        }

        private void m_btnAuswählen_Click(object sender, EventArgs e)
        {
            ClsArbeitsprofil Auswählen = m_lbxArbeitszeitprofile.SelectedItem as ClsArbeitsprofil;

            m_tbxName.Text = Auswählen.Name;

            m_cmbxMontag.SelectedItem = DlgTag.Tagesliste[DlgTag.Tagesliste.IndexOf(Auswählen.Montag)];
            m_cmbxDienstag.SelectedItem = DlgTag.Tagesliste[DlgTag.Tagesliste.IndexOf(Auswählen.Dienstag)];
            m_cmbxMittwoch.SelectedItem = DlgTag.Tagesliste[DlgTag.Tagesliste.IndexOf(Auswählen.Mittwoch)];
            m_cmbxDonnerstag.SelectedItem = DlgTag.Tagesliste[DlgTag.Tagesliste.IndexOf(Auswählen.Donnerstag)];
            m_cmbxFreitag.SelectedItem = DlgTag.Tagesliste[DlgTag.Tagesliste.IndexOf(Auswählen.Freitag)];
            m_cmbxSamstag.SelectedItem = DlgTag.Tagesliste[DlgTag.Tagesliste.IndexOf(Auswählen.Samstag)];
            m_cmbxSonntag.SelectedItem = DlgTag.Tagesliste[DlgTag.Tagesliste.IndexOf(Auswählen.Sonntag)];
            m_cbGleitzeit.Checked = Auswählen.Gleitzeit;
        }

        private void m_btnAktualisieren_Click(object sender, EventArgs e)
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
        }

        private void m_btnNeu_Click(object sender, EventArgs e)
        {
            m_tbxName.Text = "";
            m_cmbxMontag.Text = "";
            m_cmbxDienstag.Text = "";
            m_cmbxMittwoch.Text = "";
            m_cmbxDonnerstag.Text = "";
            m_cmbxFreitag.Text = "";
            m_cmbxSamstag.Text = "";
            m_cmbxSonntag.Text = "";
            m_tbxUrlaub.Text = "";
            m_cbGleitzeit.Checked = false;
        }

        private void m_btnErstellen_Click(object sender, EventArgs e)
        {
            try
            {
                int Urlaub = Convert.ToInt32(m_tbxUrlaub.Text);
                DataProvider.InsertArbeitszeitprofil(m_tbxName.Text, m_cmbxMontag.SelectedItem as ClsTag,
                    m_cmbxDienstag.SelectedItem as ClsTag, m_cmbxMittwoch.SelectedItem as ClsTag,
                    m_cmbxDonnerstag.SelectedItem as ClsTag, m_cmbxFreitag.SelectedItem as ClsTag,
                    m_cmbxSamstag.SelectedItem as ClsTag, m_cmbxSonntag.SelectedItem as ClsTag, m_cbGleitzeit.Checked);
                UpdateAbzpList();
            }
            catch
            {
                MessageBox.Show("Bitte den Urlaub als Zahl eingeben!");
            }
        }

        private void m_btnLöschen_Click(object sender, EventArgs e)
        {
            DataProvider.DeleteArbeitszeitprofil(m_lbxArbeitszeitprofile.SelectedItem as ClsArbeitsprofil);
            UpdateAbzpList();
        }

        private void UpdateAbzpList()
        {
            m_arbeitsprofilliste.Clear();

            foreach (ClsArbeitsprofil abzp in DataProvider.SelectAllArbeitszeitprofil())
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
        }
    }
}
