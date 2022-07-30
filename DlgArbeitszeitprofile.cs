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
    public partial class DlgArbeitszeitprofile : Form
    {
        private BindingList<ClsArbeitsprofil> m_arbeitsprofilliste = new BindingList<ClsArbeitsprofil>();
        public DlgArbeitszeitprofile()
        {
            InitializeComponent();

            m_lbxArbeitszeitprofile.DataSource = m_arbeitsprofilliste;

            UpdateCMBX();

            ClsTag Tag = new ClsTag("test1",new TimeSpan(7, 30, 0), new TimeSpan(18, 00, 0), new TimeSpan(12, 30, 0), new TimeSpan(15, 00, 0), new TimeSpan(8,0,0), new TimeSpan(2, 30, 0));

            ClsArbeitsprofil Test = new ClsArbeitsprofil("hi",Tag, Tag, Tag, Tag, Tag, Tag, Tag, 5, false);
            m_arbeitsprofilliste.Add(Test);
        }
        private void m_btnTage_Click(object sender, EventArgs e)
        {
            DlgTag Tag = new DlgTag();
            if(Tag.ShowDialog() == DialogResult.OK)
            {
                UpdateCMBX();
            }
        }

        private void UpdateCMBX()
        {
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

        private void m_btnAuswählen_Click(object sender, EventArgs e)
        {
            ClsArbeitsprofil Auswählen = m_lbxArbeitszeitprofile.SelectedItem as ClsArbeitsprofil;

            m_tbxName.Text = Auswählen.Name;
            m_cmbxMontag.SelectedItem = Auswählen.Montag;
            m_cmbxDienstag.SelectedItem = Auswählen.Dienstag;
            m_cmbxMittwoch.SelectedItem = Auswählen.Mittwoch;
            m_cmbxDonnerstag.SelectedItem = Auswählen.Donnerstag;
            m_cmbxFreitag.SelectedItem = Auswählen.Freitag;
            m_cmbxSamstag.SelectedItem = Auswählen.Samstag;
            m_cmbxSonntag.SelectedItem = Auswählen.Sonntag;
            m_tbxUrlaub.Text = Auswählen.Urlaub.ToString();
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
            try
            {
                Aktualisieren.Urlaub = Convert.ToInt32(m_tbxUrlaub.Text);
            }
            catch
            {
                MessageBox.Show("Bitte Urlaub als Zahl eingeben");
            }
            Aktualisieren.Gleitzeit = m_cbGleitzeit.Checked;
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
                ClsArbeitsprofil neu = new ClsArbeitsprofil(m_tbxName.Text, m_cmbxMontag.SelectedItem as ClsTag, m_cmbxDienstag.SelectedItem as ClsTag, m_cmbxMittwoch.SelectedItem as ClsTag, m_cmbxDonnerstag.SelectedItem as ClsTag, m_cmbxFreitag.SelectedItem as ClsTag, m_cmbxSamstag.SelectedItem as ClsTag, m_cmbxSonntag.SelectedItem as ClsTag, Urlaub, m_cbGleitzeit.Checked);
                m_arbeitsprofilliste.Add(neu);
                m_arbeitsprofilliste.ResetBindings();
            }
            catch
            {
                MessageBox.Show("Bitte den Urlaub als Zahl eingeben!");
            }
        }
    }
}
