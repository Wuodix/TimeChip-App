using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TimeChip_App
{
    public partial class DlgMitarbeiter : Form
    {
        int m_mitarbeiternummer;
        //m_finger gibt an ob schon ein Finger hinzugefügt wurde; m_bearbeiten gibt an ob das Fenster gerade zum bearbeiten oder erstellen genutzt wird
        bool m_finger = false, m_bearbeiten = false, m_card = false;
        ClsFingerprintRFID m_fingerprintRFID;

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
        public int Mitarbeiternummer { get { return m_mitarbeiternummer; } set { m_mitarbeiternummer = value; } }
        public TimeSpan Überstunden { get { return GetTimeSpans(false); }set { m_tbxÜberstunden.Text = Stundenrunder(value); } }
        public TimeSpan Urlaub { get { return GetTimeSpans(true); } set { m_tbxUrlaub.Text = Stundenrunder(value); } }

        public void Bearbeiten(ClsMitarbeiter Bearbeitender)
        {
            m_bearbeiten = true;

            m_lblTitel.Text = "Mitarbeiter:in bearbeiten";
            m_btnOK.Text = "Bearbeiten";
            Name = "Bearbeiten";
            m_btnAddFinger.Text = "Finger ändern";
            m_btnAddCard.Text = "Karte ändern";

            m_tbxVorname.Text = Bearbeitender.Vorname;
            m_tbxNachname.Text = Bearbeitender.Nachname;
            m_dtpArbeitsb.Value = Bearbeitender.Arbeitsbeginn;
            m_mitarbeiternummer = Bearbeitender.Mitarbeiternummer;
            m_tbxÜberstunden.Text = Stundenrunder(Bearbeitender.Überstunden);
            m_tbxUrlaub.Text = Stundenrunder(Bearbeitender.Urlaub);

            List<ClsArbeitsprofil> clsArbeitsprofils = DlgArbeitszeitprofile.ArbeitsprofilListe.ToList();
            ClsArbeitsprofil arbeitsprofil = clsArbeitsprofils.FindLast(x => x.ID.Equals(Bearbeitender.Arbeitszeitprofil.ID));
            m_cmbxAProfil.SelectedItem = arbeitsprofil;

            m_fingerprintRFID = DataProvider.SelectAllFingerprintRFID().Find(x => x.Fingerprint.Equals(Bearbeitender.Mitarbeiternummer));

            if(m_fingerprintRFID.RFIDUID != "NaN") { m_card = true; }
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
            m_btnAddFinger.Text = "Finger hinzufügen";
            m_btnAddCard.Text = "Karte hinzufügen";
            m_bearbeiten = false;
        }

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

        private void BtnAddFinger_Click(object sender, EventArgs e)
        {
            if(m_bearbeiten || m_finger)
            {
                if(MessageBox.Show("Wollen Sie den Finger wirklich ändern?","Achtung",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            if (!m_bearbeiten && !m_finger && !m_card)
            {
                SetMitarbeiternummer();
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

                if (!m_card && !m_bearbeiten)
                {
                    m_fingerprintRFID = DataProvider.InsertFingerRFIDUID(Mitarbeiternummer, "NaN");
                }
            }
            else
            {
                if (!m_bearbeiten)
                {
                    MessageBox.Show("Der Finger konnte nicht hinzugefügt werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Der Finger konnte nicht geändert werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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

                if (!m_bearbeiten && !m_finger)
                {
                    m_fingerprintRFID = DataProvider.InsertFingerRFIDUID(m_mitarbeiternummer, parts[1]);
                }
                else
                {
                    m_fingerprintRFID.RFIDUID = parts[1];
                    DataProvider.UpdateFingerprintRFID(m_fingerprintRFID);
                }
                m_card = true;

                if (!m_bearbeiten)
                {
                    MessageBox.Show("Karte wurde erfolgreich hinzugefügt.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Karte wurde erfolgreich geändert.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                if (!m_bearbeiten)
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

                DataProvider.InsertFingerRFIDUID(m_mitarbeiternummer, "NaN");
            }
        }
    }
}
