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
    public partial class DlgTag : Form
    {
        static BindingList<ClsTag> m_tagesliste = new BindingList<ClsTag>();
        private bool m_fehler;
        public DlgTag()
        {
            InitializeComponent();

            m_lbxTage.DataSource = m_tagesliste;

            m_fehler = false;
            /*
            TimeSpan time = new TimeSpan(7, 30, 0);
            ClsTag tag = new ClsTag("hi", time, time, time, time, time, time);
            m_tagesliste.Add(tag);
            */
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

        private void m_btnErstellen_Click(object sender, EventArgs e)
        {
            ClsTag neu = new ClsTag(m_tbxName.Text, StringToTimeSpan(m_tbxArbeitsbeginn.Text), StringToTimeSpan(m_tbxArbeitsende.Text), StringToTimeSpan(m_tbxPausenbeginn.Text), StringToTimeSpan(m_tbxPausenende.Text), StringToTimeSpan(m_tbxArbeitszeit.Text), StringToTimeSpan(m_tbxPausendauer.Text));
            if(m_fehler == false)
            {
                m_tagesliste.Add(neu);
                m_tagesliste.ResetBindings();
            }
            m_fehler = false;
        }

        private void m_btnAuswählen_Click(object sender, EventArgs e)
        {
            ClsTag Auswählen = m_lbxTage.SelectedItem as ClsTag;

            m_tbxName.Text = Auswählen.Name;
            m_tbxArbeitsbeginn.Text = TimeSpanToString(Auswählen.Arbeitsbeginn);
            m_tbxArbeitsende.Text = TimeSpanToString(Auswählen.Arbeitsende);
            m_tbxPausenbeginn.Text = TimeSpanToString(Auswählen.Pausenbeginn);
            m_tbxPausenende.Text = TimeSpanToString(Auswählen.Pausenende);
            m_tbxArbeitszeit.Text = TimeSpanToString(Auswählen.Arbeitszeit);
            m_tbxPausendauer.Text = TimeSpanToString(Auswählen.Pausendauer);

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

            m_tagesliste.ResetBindings();
        }

        private string TimeSpanToString(TimeSpan time)
        {
            string[] temp = new string[2];
            temp[0] = time.Hours.ToString();
            temp[1] = time.Minutes.ToString();
            return String.Join(":", temp);
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
                    MessageBox.Show("Bitte die Uhrzeiten im Format Stunden:Minuten eingeben!");
                    m_fehler = true;
                }
            }
            return temp;
        }
    }
}
