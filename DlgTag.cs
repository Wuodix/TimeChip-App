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
    public partial class DlgTag : Form
    {
        static BindingList<ClsTag> m_tagesliste = new BindingList<ClsTag>();
        public DlgTag()
        {
            InitializeComponent();

            m_lbxTage.DataSource = m_tagesliste;

            UpdateTagesListe();
        }

        public static BindingList<ClsTag> Tagesliste { get { return m_tagesliste; } set { m_tagesliste = value; } }

        private void m_btnNeu_Click(object sender, EventArgs e)
        {
            m_tbxName.Text = "";
            m_dtpArbeitsbeginn.Value = DateTime.Today;
            m_dtpArbeitsende.Value = DateTime.Today;
            m_dtpPausenbeginn.Value = DateTime.Today;
            m_dtpPausenende.Value = DateTime.Today;
            m_dtpArbeitszeit.Value = DateTime.Today;
            m_dtpPausendauer.Value = DateTime.Today;
            m_cbPause.Checked = false;
        }

        private void m_btnLöschen_Click(object sender, EventArgs e)
        {
            DataProvider.DeleteTag(m_lbxTage.SelectedItem as ClsTag);
            UpdateTagesListe();
        }

        private void m_btnErstellen_Click(object sender, EventArgs e)
        {
            TimeSpan arbeitsbeginn = m_dtpArbeitsbeginn.Value.TimeOfDay;
            TimeSpan arbeitsende = m_dtpArbeitsende.Value.TimeOfDay;
            TimeSpan pausenbeginn = m_dtpPausenbeginn.Value.TimeOfDay;
            TimeSpan pausenende = m_dtpPausenende.Value.TimeOfDay;
            TimeSpan arbeitszeit = m_dtpArbeitszeit.Value.TimeOfDay;
            TimeSpan pausendauer = m_dtpPausendauer.Value.TimeOfDay;

            DataProvider.InsertTag(m_tbxName.Text, arbeitsbeginn, arbeitsende, arbeitszeit, pausenbeginn, pausenende, pausendauer, m_cbPause.Checked);
            UpdateTagesListe();
        }

        private void m_btnAuswählen_Click(object sender, EventArgs e)
        {
            ClsTag Auswählen = m_lbxTage.SelectedItem as ClsTag;

            m_tbxName.Text = Auswählen.Name;
            m_dtpArbeitsbeginn.Value = DateTime.Today.Add(Auswählen.Arbeitsbeginn);
            m_dtpArbeitsende.Value = DateTime.Today.Add(Auswählen.Arbeitsende);
            m_dtpPausenbeginn.Value = DateTime.Today.Add(Auswählen.Pausenbeginn);
            m_dtpPausenende.Value = DateTime.Today.Add(Auswählen.Pausenende);
            m_dtpArbeitszeit.Value = DateTime.Today.Add(Auswählen.Arbeitszeit);
            m_dtpPausendauer.Value = DateTime.Today.Add(Auswählen.Pausendauer);
            m_cbPause.Checked = Auswählen.Pause;

        }

        private void m_btnAktualisieren_Click(object sender, EventArgs e)
        {
            ClsTag Aktualisieren = m_lbxTage.SelectedItem as ClsTag;

            Aktualisieren.Name = m_tbxName.Text;
            Aktualisieren.Arbeitsbeginn = m_dtpArbeitsbeginn.Value.TimeOfDay;
            Aktualisieren.Arbeitsende = m_dtpArbeitsende.Value.TimeOfDay;
            Aktualisieren.Pausenbeginn = m_dtpPausenbeginn.Value.TimeOfDay;
            Aktualisieren.Pausenende = m_dtpPausenende.Value.TimeOfDay;
            Aktualisieren.Arbeitszeit = m_dtpArbeitszeit.Value.TimeOfDay;
            Aktualisieren.Pausendauer = m_dtpPausendauer.Value.TimeOfDay;
            Aktualisieren.Pause = m_cbPause.Checked;

            DataProvider.UpdateTag(Aktualisieren);

            m_tagesliste.ResetBindings();
        }

        private void UpdateTagesListe()
        {
            m_tagesliste.Clear();

            foreach (ClsTag tag in DataProvider.SelectAllTage())
            {
                m_tagesliste.Add(tag);
            }

            m_tagesliste.ResetBindings();
        }
    }
}
