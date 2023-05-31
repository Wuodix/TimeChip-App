using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace TimeChip_App
{
    public partial class DlgMitarbeiter : Form
    {
        int m_mtbtrID, m_mtbtrNummer; //mtbtrNummmer beinhaltet die immer gerade aktuelle Mitarbeiternummer mit der gerade gearbeitet wird
        //m_finger/m_card gibt an ob schon ein Finger/Karte hinzugefügt wurde; m_bearbeiten gibt an ob das Fenster gerade zum bearbeiten oder erstellen genutzt wird
        bool m_finger = false, m_bearbeiten = false, m_card = false;
        //Beinhaltet (wenn vorhanden) ein FingerprintRFID Objekt, von dem man wichtige Infos ablesen kann
        ClsFingerprintRFID m_fingerprintRFID;

        //Beinhaltet alle FingerprintRFID Objekte des aktuellen Mtbtrs
        BindingList<ClsFingerprintRFID> m_fingers = new BindingList<ClsFingerprintRFID>();

        public DlgMitarbeiter()
        {
            InitializeComponent();

            m_cmbxAProfil.Items.Clear();
            m_cmbxAProfil.Items.AddRange(DlgArbeitszeitprofile.ArbeitsprofilListe.ToArray());
            m_lbxFinger.DataSource = m_fingers;
            m_lbxFinger.DisplayMember = "FingerName";
        }

        public string Vorname { get { return m_tbxVorname.Text; } set { m_tbxVorname.Text = value; } }
        public string Nachname { get { return m_tbxNachname.Text; } set { m_tbxNachname.Text = value; } }
        public ClsArbeitsprofil Arbeitzeitprofil { get { return m_cmbxAProfil.SelectedItem as ClsArbeitsprofil; } set { m_cmbxAProfil.SelectedItem = value; } }
        public DateTime Arbeitsbeginn { get { return m_dtpArbeitsb.Value; } set { m_dtpArbeitsb.Value = value; } }
        public TimeSpan Überstunden { get { return GetTimeSpans(false); }set { m_tbxÜberstunden.Text = Stundenrunder(value); } }
        public TimeSpan Urlaub { get { return GetTimeSpans(true); } set { m_tbxUrlaub.Text = Stundenrunder(value); } }

        public void Bearbeiten(ClsMitarbeiter Bearbeitender)
        {
            //Daten in graphisches Fenster einfüllen
            m_bearbeiten = true;

            m_lblTitel.Text = "Mitarbeiter:in bearbeiten";
            m_btnOK.Text = "Bearbeiten";
            Name = "Bearbeiten";

            m_tbxVorname.Text = Bearbeitender.Vorname;
            m_tbxNachname.Text = Bearbeitender.Nachname;
            m_dtpArbeitsb.Value = Bearbeitender.Arbeitsbeginn;
            m_mtbtrID = Bearbeitender.ID;
            m_tbxÜberstunden.Text = Stundenrunder(Bearbeitender.Überstunden);
            m_tbxUrlaub.Text = Stundenrunder(Bearbeitender.Urlaub);

            List<ClsArbeitsprofil> clsArbeitsprofils = DlgArbeitszeitprofile.ArbeitsprofilListe.ToList();
            ClsArbeitsprofil arbeitsprofil = clsArbeitsprofils.FindLast(x => x.ID.Equals(Bearbeitender.Arbeitszeitprofil.ID));
            m_cmbxAProfil.SelectedItem = arbeitsprofil;

            //Fügt alle FingerprintRFID Objekte des Mtbtrs, die einen Finger beinhalten zu m_fingers hinzu
            foreach (ClsFingerprintRFID fingerRfid in DataProvider.SelectFingerprintRFIDOfMtbtr(Bearbeitender.ID))
            {
                if (fingerRfid.FingerName != "NaN")
                {
                    m_fingers.Add(fingerRfid);
                }
            }
            m_fingers.ResetBindings();

            //Wenn m_fingers nicht leer ist muss es bereits einen eingespeicherten Finger geben
            if(m_fingers.Count > 0)
            {
                m_finger = true;
            }

            //Speichert in m_fingerprintRFID (falls vorhanden) das Objekt ohne Finger oder das erste
            List<ClsFingerprintRFID> Fingerprints = DataProvider.SelectFingerprintRFIDOfMtbtr(Bearbeitender.ID);
            m_fingerprintRFID = Fingerprints.Find(x=>x.FingerName == "NaN");
            if(m_fingerprintRFID == null)
            {
                m_fingerprintRFID = Fingerprints.First();
            }
            m_mtbtrNummer = m_fingerprintRFID.Fingerprint;

            if(m_fingerprintRFID.RFIDUID != "NaN")
            {
                m_card = true;
                m_btnAddCard.Text = "Karte ändern";
            }
        }

        private string Stundenrunder(TimeSpan Zeit)
        {
            string result = Convert.ToInt32(Math.Round(Math.Abs(Zeit.TotalHours) - 0.49, 0, MidpointRounding.AwayFromZero)).ToString("D2") + ":" + Math.Abs(Zeit.Minutes).ToString("D2");

            if (Zeit.TotalHours < 0)
            {
                result = "-" + result;
            }

            return result;
        }

        public void Neu()
        {
            m_lblTitel.Text = "Neuer:e Mitarbeiter:in";
            m_btnOK.Text = "Erstellen";
            Name = "Neu";
            m_btnAddCard.Text = "Karte hinzufügen";
            m_bearbeiten = false;
        }

        /// <summary>
        /// Konvertiert die eingegebenen Zeitspannen zu Text
        /// true = Urlaub
        /// false = Überstunden
        /// </summary>
        /// <param name="which">Gibt an welche Zeitspanne abgefragt wird</param>
        /// <returns>Angefragte Zeitspanne</returns>
        private TimeSpan GetTimeSpans(bool which)
        {
            string[] teile;
            if (which)
            {
                teile = m_tbxUrlaub.Text.Split(':');
            }
            else
            {
                teile = m_tbxÜberstunden.Text.Split(':');
            }

            int hours = Convert.ToInt32(teile[0]);
            int minutes = Convert.ToInt32(teile[1]);

            TimeSpan result;
            if(hours < 0)
            {
                result = new TimeSpan(Math.Abs(hours), minutes, 0);
                result = result.Negate();
            }
            else
            {
                result = new TimeSpan(hours,minutes, 0);
            }

            return result;
        }

        private void SetMitarbeiternummer()
        {
            List<ClsFingerprintRFID> list = DataProvider.SelectAllFingerprintRFID();
            List<ClsFingerprintRFID> list1 = list.OrderBy(f => f.Fingerprint).ToList();

            m_mtbtrNummer = list1.Count + 1;
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].Fingerprint != i + 1)
                {
                    m_mtbtrNummer = i + 1;
                    break;
                }
            }
        }

        private void BtnDeleteFinger_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Wollen Sie den ausgewählten Finger wirklich löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DataProvider.DeleteFingerprintRFID(m_lbxFinger.SelectedItem as ClsFingerprintRFID);
            m_fingers.Remove(m_lbxFinger.SelectedItem as ClsFingerprintRFID);
            m_fingers.ResetBindings();
        }

        private void BtnAddFinger_Click(object sender, EventArgs e)
        {
            DlgFingerName fingerName = new DlgFingerName();
            fingerName.ShowDialog();
            string finger = "";
            if (fingerName.DialogResult == DialogResult.OK)
            {
                finger = fingerName.FingerName;
            }
            else if (fingerName.DialogResult == DialogResult.Cancel)
                return;

            if(!m_card || m_fingerprintRFID.FingerName != "NaN")
            {
                SetMitarbeiternummer();
            }

            string responseContent = DataProvider.SendRecieveHTTP("Finger" + m_mtbtrNummer);

            if (responseContent.Contains("Finger-hinzugefuegt"))
            {
                MessageBox.Show("Der Finger wurde erfolgreich hinzugefügt!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_finger = true;

                if (!m_card && !m_bearbeiten)
                {
                    m_fingerprintRFID = DataProvider.InsertFingerRFIDUID(m_mtbtrNummer, "NaN", 0, finger);
                }
                else
                {
                    m_fingerprintRFID = DataProvider.InsertFingerRFIDUID(m_mtbtrNummer, m_fingerprintRFID.RFIDUID, m_fingerprintRFID.MtbtrID, finger);
                }

                m_fingers.Add(m_fingerprintRFID);
                m_fingers.ResetBindings();
            }
            else
            {
                MessageBox.Show("Der Finger konnte nicht hinzugefügt werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddCard_Click(object sender, EventArgs e)
        {
            if(m_bearbeiten || m_card)
            {
                if (MessageBox.Show("Wollen Sie die Karte wirklich ändern?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            if (!m_bearbeiten && !m_finger && !m_card)
            {
                SetMitarbeiternummer();
            }

            string responseContent = DataProvider.SendRecieveHTTP("Card");

            if (responseContent.Contains("CardUID:"))
            {
                string[] parts = responseContent.Split('$');

                if (!m_bearbeiten && !m_finger && !m_card)
                {
                    m_fingerprintRFID = DataProvider.InsertFingerRFIDUID(m_mtbtrNummer, parts[1], 0, "NaN");
                }
                else
                {
                    m_fingerprintRFID.RFIDUID = parts[1];
                    DataProvider.UpdateAllFingerprintRFIDCards(m_fingerprintRFID);
                }

                if (!m_card)
                {
                    MessageBox.Show("Karte wurde erfolgreich hinzugefügt.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Karte wurde erfolgreich geändert.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                m_card = true;
            }
            else
            {
                if (!m_card)
                {
                    MessageBox.Show("Die Karte konnte nicht hinzugefügt werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Die Karte konnte nicht geändert werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {            
            if (!m_bearbeiten)
            {
                if (!m_card)
                {
                    if(MessageBox.Show("Wollen Sie wirklich einen Mitarbeiter ohne zugewiesene Karte erstellen?", "Achtung",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (!m_finger)
                {
                    if (MessageBox.Show("Wollen Sie wirklich einen Mitarbeiter ohne eingespeicherten Finger erstellen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            try
            {
                GetTimeSpans(true);
                GetTimeSpans(false);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch
            {
                MessageBox.Show("Bitte Urlaub und Überstunden im Format Stunden:Minuten eingeben!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!m_card && !m_finger && !m_bearbeiten)
            {
                SetMitarbeiternummer();

                DataProvider.InsertFingerRFIDUID(m_mtbtrNummer, "NaN",0,"NaN");
            }
        }
    }
}
