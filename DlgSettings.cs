using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TimeChip_App.Properties;

namespace TimeChip_App
{
    public partial class DlgSettings : Form
    {
        string[] m_connectionString;
        public DlgSettings()
        {
            InitializeComponent();

            m_connectionString = DataProvider.ConnectionString.Split(';','=');

            /*
            SERVER			    0
            localhost			1
            DATABASE			2
            apotheke_time_chip1	3
            UID				    4
            Hauptapp	   		5
            Password			6
            oo/1X)ZV1jlmTyEm	7
            */

            m_tbxIP.Text = m_connectionString[1];
            m_tbxDatabase.Text = m_connectionString[3];
            m_tbxUID.Text = m_connectionString[5];
            m_tbxPass.Text = m_connectionString[7];

            m_dtpBerechnungsdate.Value = DataProvider.ReadBerechnungsdate().Date;
        }

        public string IP { get { return m_tbxIP.Text; } }
        public string Database { get { return m_tbxDatabase.Text; } }
        public string UID { get { return m_tbxUID.Text;} }
        public string Password { get { return m_tbxPass.Text; } }
        public DateTime Berechnungsdate { get {  return m_dtpBerechnungsdate.Value.Date; } }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            bool verändert = false;

            if(m_tbxIP.Text != m_connectionString[1])
            {
                verändert = true;
            }

            if(verändert == false && m_tbxDatabase.Text != m_connectionString[3])
            {
                verändert = true;
            }

            if(verändert == false && m_tbxUID.Text != m_connectionString[5])
            {
                verändert = true;
            }

            if (m_tbxPass.Text != m_connectionString[7])
            {
                if(MessageBox.Show("Wollen Sie wirklich das Passwort ändern?","Achtung!",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                verändert = true;
            }

            if(verändert == false && m_dtpBerechnungsdate.Value.Date != Settings.Default.Berechnungsdate)
            {
                verändert = true;
            }

            if (verändert)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
            Close();
        }
    }
}
