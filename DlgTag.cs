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
        private bool m_fehler;
        public DlgTag()
        {
            InitializeComponent();

            m_lbxTage.DataSource = m_tagesliste;

            m_fehler = false;

            UpdateTagesListe();
        }

        public static BindingList<ClsTag> Tagesliste { get { return m_tagesliste; } set { m_tagesliste = value; } }

        private void m_btnNeu_Click(object sender, EventArgs e)
        {
            m_tbxName.Text = "";
            m_tbxArbeitsbeginn.Text = "";
            m_tbxArbeitsende.Text = "";
            m_tbxPausenbeginn.Text = "";
            m_tbxPausenende.Text = "";
            m_tbxArbeitszeit.Text = "";
            m_tbxPausendauer.Text = "";
        }

        private void m_btnLöschen_Click(object sender, EventArgs e)
        {
            DataProvider.DeleteTag(m_lbxTage.SelectedItem as ClsTag);
            UpdateTagesListe();
        }

        private void m_btnErstellen_Click(object sender, EventArgs e)
        {
            TimeSpan arbeitsbeginn = StringToTimeSpan(m_tbxArbeitsbeginn.Text);
            TimeSpan arbeitsende = StringToTimeSpan(m_tbxArbeitsende.Text);
            TimeSpan pausenbeginn = StringToTimeSpan(m_tbxPausenbeginn.Text);
            TimeSpan pausenende = StringToTimeSpan(m_tbxPausenende.Text);
            TimeSpan arbeitszeit = StringToTimeSpan(m_tbxArbeitszeit.Text);
            TimeSpan pausendauer = StringToTimeSpan(m_tbxPausendauer.Text);

            if (m_fehler == false)
            {
                DataProvider.InsertTag(m_tbxName.Text, arbeitsbeginn, arbeitsende, arbeitszeit, pausenbeginn, pausenende, pausendauer);
                UpdateTagesListe();
            }
            else { m_fehler = false; }
            
        }

        private void m_btnAuswählen_Click(object sender, EventArgs e)
        {
            ClsTag Auswählen = m_lbxTage.SelectedItem as ClsTag;

            m_tbxName.Text = Auswählen.Name;
            m_tbxArbeitsbeginn.Text = Auswählen.Arbeitsbeginn.ToString(@"hh\:mm");
            m_tbxArbeitsende.Text = Auswählen.Arbeitsende.ToString(@"hh\:mm");
            m_tbxPausenbeginn.Text = Auswählen.Pausenbeginn.ToString(@"hh\:mm");
            m_tbxPausenende.Text = Auswählen.Pausenende.ToString(@"hh\:mm");
            m_tbxArbeitszeit.Text = Auswählen.Arbeitszeit.ToString(@"hh\:mm");
            m_tbxPausendauer.Text = Auswählen.Pausendauer.ToString(@"hh\:mm");

        }

        private void m_btnAktualisieren_Click(object sender, EventArgs e)
        {
            ClsTag Aktualisieren = m_lbxTage.SelectedItem as ClsTag;

            Aktualisieren.Name = m_tbxName.Text;
            Aktualisieren.Arbeitsbeginn = StringToTimeSpan(m_tbxArbeitsbeginn.Text);
            Aktualisieren.Arbeitsende = StringToTimeSpan(m_tbxArbeitsende.Text);
            Aktualisieren.Pausenbeginn = StringToTimeSpan(m_tbxPausenbeginn.Text);
            Aktualisieren.Pausenende = StringToTimeSpan(m_tbxPausenende.Text);
            Aktualisieren.Arbeitszeit = StringToTimeSpan(m_tbxArbeitszeit.Text);
            Aktualisieren.Pausendauer = StringToTimeSpan(m_tbxPausendauer.Text);

            Debug.WriteLine(Aktualisieren.Arbeitsbeginn.Ticks);

            if(m_fehler == false)
            {
                DataProvider.UpdateTag(Aktualisieren);

                m_tagesliste.ResetBindings();
            }
            else { m_fehler = false; }
        }

        private TimeSpan StringToTimeSpan(string str)
        {
            TimeSpan temp = new TimeSpan();
            try
            {
                string[] strarr = str.Split(':');
                temp = new TimeSpan(Convert.ToInt32(strarr[0]), Convert.ToInt32(strarr[1]), 0);
            }
            catch
            {
                if(m_fehler == false)
                {
                    MessageBox.Show("Bitte die Uhrzeiten im Format HH:MM eingeben!");
                    m_fehler = true;
                }
            }
            return temp;
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
