using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeChip_App_1._0
{
    public partial class DlgMitarbeiter : Form
    {
        int m_mitarbeiternummer;
        bool m_finger = false, m_bearbeiten = false;

        public DlgMitarbeiter()
        {
            InitializeComponent();

            m_cmbxAProfil.Items.Clear();
            m_cmbxAProfil.Items.AddRange(DlgArbeitszeitprofile.ArbeitsprofilListe.ToArray());
        }

        public string Vorname { get { return m_tbxVorname.Text; } set { m_tbxVorname.Text = value; } }
        public string Nachname { get { return m_tbxNachname.Text; } set { m_tbxNachname.Text = value; } }
        public ClsArbeitsprofil Arbeitzeitprofil { get { return m_cmbxAProfil.SelectedItem as ClsArbeitsprofil; } set { m_cmbxAProfil.SelectedItem = value; } }
        public DateTime Arbeitsbeginn { get { return m_dtpArbeitsb.Value; } set { m_dtpArbeitsb.Value = value; } }
        public string Titel { get { return m_lblTitel.Text; } set { m_lblTitel.Text = value; } }
        public string OkKnopf { get { return m_btnOK.Text; } set { m_btnOK.Text = value; } }
        public string BtnAddFinger { get { return m_btnAddFinger.Text; } set { m_btnAddFinger.Text = value; } }
        public string BtnAddCard { get { return m_btnAddCard.Text; } set { m_btnAddCard.Text = value; } }
        public int Mitarbeiternummer { get { return m_mitarbeiternummer; } set { m_mitarbeiternummer = value; } }
        public bool Bearbeiten { get { return m_bearbeiten; } set { m_bearbeiten = value; } }

        public void SetUrlaub(TimeSpan Urlaub)
        {
            string minutes = Urlaub.Minutes.ToString();
            if(Urlaub.Minutes < 10)
                minutes = "0" + minutes;
            m_tbxUrlaub.Text = Urlaub.TotalHours.ToString() + ":" + minutes;
        }

        public TimeSpan GetUrlaub()
        {
            string[] teile = m_tbxUrlaub.Text.Split(':');

            int hours = Convert.ToInt32(teile[0]);
            int minutes = Convert.ToInt32(teile[1]);

            return new TimeSpan(hours, minutes, 0);
        }

        private void m_btnAddFinger_Click(object sender, EventArgs e)
        {
            if (!m_finger || m_bearbeiten)
            {
                if (!m_bearbeiten)
                {
                    List<ClsFingerprintRFID> list = DataProvider.SelectAllFingerprintRFID();
                    List<ClsFingerprintRFID> list1 = list.OrderBy(f => f.Fingerprint).ToList();

                    m_mitarbeiternummer = list1.Count + 1;
                    for (int i = 0; i < list1.Count; i++)
                    {
                        if (list1[i].Fingerprint != i + 1)
                        {
                            m_mitarbeiternummer = i + 1;
                            break;
                        }
                    }
                }

                string responseContent = DataProvider.SendRecieveHTTP("Finger" + m_mitarbeiternummer);

                if (responseContent.Contains("Finger-hinzugefuegt"))
                {
                    if (!m_bearbeiten)
                    {
                        MessageBox.Show("Der Finger wurde erfolgreich hinzugefügt!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        m_finger = true;
                    }
                    else
                    {
                        MessageBox.Show("Der Finger wurde erfolgreich geändert!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (responseContent.Contains("Finger-nicht-hinzugefuegt"))
                {
                    if (!m_bearbeiten)
                    {
                        MessageBox.Show("Der Finger konnte leider nicht hinzugefügt werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show("Der Finger konnte leider nicht geändert werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show("Es wurde bereits ein Finger hinzugefügt.", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void m_btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string[] teile = m_tbxUrlaub.Text.Split(':');
                int hours = Convert.ToInt32(teile[0]);
                int minutes = Convert.ToInt32(teile[1]);

                DialogResult = DialogResult.OK;
                Debug.WriteLine("hi");
                Close();
            }
            catch
            {
                MessageBox.Show("Bitte Urlaub im Format Stunden:Minuten eingeben!");
            }
        }

        private void m_btnAddCard_Click(object sender, EventArgs e)
        {
            if (m_bearbeiten || m_finger)
            {
                string responseContent = DataProvider.SendRecieveHTTP("Card");

                if (responseContent.Contains("CardUID:"))
                {
                    string[] parts = responseContent.Split('$');

                    if (!m_bearbeiten)
                    {
                        DataProvider.InsertFingerRFIDUID(m_mitarbeiternummer, parts[1]);

                        MessageBox.Show("Karte wurde erfolgreich hinzugefügt.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        ClsFingerprintRFID finger = DataProvider.SelectAllFingerprintRFID().Last(x => x.Fingerprint.Equals(m_mitarbeiternummer));
                        DataProvider.UpdateFingerprintRFID(new ClsFingerprintRFID(finger.ID, parts[1], m_mitarbeiternummer));

                        MessageBox.Show("Karte wurde erfolgreich geändert.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    
                }
            }
            else
            {
                MessageBox.Show("Speichern Sie bitte zuerst einen Finger ein", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
