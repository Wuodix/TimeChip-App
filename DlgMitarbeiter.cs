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
        string URL = "http://192.168.1.205/";
        string responseContent = "";
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
        public bool Finger { get { return m_finger; } set { m_finger = value; } }

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

                SendRecieveHTTP("Finger" + m_mitarbeiternummer);

                if (responseContent.Contains("Finger-hinzugefuegt"))
                {
                    if (!m_bearbeiten)
                    {
                        MessageBox.Show("Der Finger wurde erfolgreich hinzugefügt!");
                        m_finger = true;
                    }
                    else
                    {
                        MessageBox.Show("Der Finger wurde erfolgreich geändert!");
                    }
                }
                else if (responseContent.Contains("Finger-nicht-hinzugefuegt"))
                {
                    if (!m_bearbeiten)
                    {
                        MessageBox.Show("Der Finger konnte leider nicht hinzugefügt werden!");
                    }
                    else
                    {
                        MessageBox.Show("Der Finger konnte leider nicht geändert werden!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Es wurde bereits ein Finger hinzugefügt.");
            }
        }

        private void m_btnAddCard_Click(object sender, EventArgs e)
        {
            if (m_bearbeiten || m_finger)
            {
                SendRecieveHTTP("Card");

                if (responseContent.Contains("CardUID:"))
                {
                    string[] parts = responseContent.Split('$');

                    if (!m_bearbeiten)
                    {
                        DataProvider.InsertFingerRFIDUID(m_mitarbeiternummer, parts[1]);

                        MessageBox.Show("Karte wurde erfolgreich hinzugefügt.");
                    }
                    else
                    {
                        ClsFingerprintRFID finger = DataProvider.SelectAllFingerprintRFID().Last(x => x.Fingerprint.Equals(m_mitarbeiternummer));
                        DataProvider.UpdateFingerprintRFID(new ClsFingerprintRFID(finger.ID, parts[1], m_mitarbeiternummer));

                        MessageBox.Show("Karte wurde erfolgreich geändert.");
                    }

                    
                }
            }
            else
            {
                MessageBox.Show("Speichern Sie bitte zuerst einen Finger ein");
            }
        }

        void SendRecieveHTTP(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text + "\n\r\n");
            
            WebRequest webRequest = WebRequest.Create(URL);
            webRequest.Method = "POST";
            webRequest.ContentLength = data.Length;

            using (Stream strm = webRequest.GetRequestStream())
            {
                strm.Write(data, 0, data.Length);
            }

            using (WebResponse response = webRequest.GetResponse())
            {
                using (Stream strm = response.GetResponseStream())
                {
                    using (StreamReader sr99 = new StreamReader(strm))
                    {
                        responseContent = sr99.ReadToEnd();
                    }
                }
            }
        }
    }
}
