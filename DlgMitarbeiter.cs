using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TimeChip_App
{
    public partial class DlgMitarbeiter : Form
    {
        //m_finger gibt an ob schon ein Finger hinzugefügt wurde; m_bearbeiten gibt an ob das Fenster gerade zum bearbeiten oder erstellen genutzt wird
        bool m_finger = false, m_bearbeiten = false, m_card = false;
        //Beinhaltet (wenn vorhanden) ein FingerprintRFID Objekt, von dem man wichtige Infos ablesen kann
        ClsArbeitsprofil m_arbeitsprofil;
        int m_fingernumber, m_mtbtrID;

        //Wird für den Tooltip auf den deaktivierten Knöpfen gebraucht
        private readonly System.Windows.Forms.ToolTip m_toolTip = new System.Windows.Forms.ToolTip
        {
            InitialDelay = 0,
            AutomaticDelay = 0,
            ToolTipIcon = ToolTipIcon.Warning,
            ToolTipTitle = "Achtung",
            IsBalloon = false,
            ShowAlways = false,
            UseFading = true,
        };
        private Control m_currentToolTipControl = null;

#pragma warning disable IDE0044 // Modifizierer "readonly" hinzufügen
        BindingList<ClsFingerprintRFID> m_fingers = new BindingList<ClsFingerprintRFID>();
#pragma warning restore IDE0044 // Modifizierer "readonly" hinzufügen

        public DlgMitarbeiter()
        {
            InitializeComponent();

            m_cmbxAProfil.Items.Clear();
            m_cmbxAProfil.Items.AddRange(DlgArbeitszeitprofile.ArbeitsprofilListe.ToArray());
            m_lbxFinger.DataSource = m_fingers;
            m_lbxFinger.DisplayMember = "FingerName";
            CardUID = "NaN";

            m_tbxVorname.TabIndex = 1;
            m_tbxNachname.TabIndex = 2;
            m_tbxÜberstunden.TabIndex = 3;
            m_tbxUrlaub.TabIndex = 4;
            m_cmbxAProfil.TabIndex = 5;
            m_dtpArbeitsb.TabIndex = 6;
            m_btnAddFinger.TabIndex = 7;
            m_btnAddCard.TabIndex = 8;
            m_btnOK.TabIndex = 9;
            m_btnCancel.TabIndex = 10;
        }

        public string Vorname { get { return m_tbxVorname.Text; } set { m_tbxVorname.Text = value; } }
        public string Nachname { get { return m_tbxNachname.Text; } set { m_tbxNachname.Text = value; } }
        public string CardUID { get;set; }
        public ClsArbeitsprofil Arbeitzeitprofil { get { return m_cmbxAProfil.SelectedItem as ClsArbeitsprofil; } set { m_cmbxAProfil.SelectedItem = value; } }
        public DateTime Arbeitsbeginn { get { return m_dtpArbeitsb.Value; } set { m_dtpArbeitsb.Value = value; } }
        public TimeSpan Überstunden { get { return GetTimeSpans(false); }set { m_tbxÜberstunden.Text = Stundenrunder(value); } }
        public TimeSpan Urlaub { get { return GetTimeSpans(true); } set { m_tbxUrlaub.Text = Stundenrunder(value); } }

        /// <summary>
        /// Verändert Werte, wie den Titel des Forms um dieses auf das Bearbeiten eines Mitarbeiters vorzubereiten
        /// </summary>
        /// <param name="Bearbeitender"></param>
        public void Bearbeiten(ClsMitarbeiter Bearbeitender)
        {
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
            m_arbeitsprofil = arbeitsprofil;

            List<ClsFingerprintRFID> fingerprintRFIDs = DataProvider.SelectFingerprintRFIDofMtbtr(Bearbeitender.ID);
            
            //Fügt alle FingerprintRFID Objekte des Mtbtrs zu m_fingers hinzu
            foreach (ClsFingerprintRFID f in fingerprintRFIDs)
            {
                m_fingers.Add(f);
            }
            m_fingers.ResetBindings();

            //Wenn m_fingers nicht leer ist muss es bereits einen eingespeicherten Finger geben
            if (m_fingers.Count > 0)
            {
                m_finger = true;
            }

            if(Bearbeitender.RFIDUID != "NaN")
            {
                m_card = true;
                m_btnAddCard.Text = "Karte ändern";
            }
            CardUID = Bearbeitender.RFIDUID;
        }

        /// <summary>
        /// Stellt eine Zeitspanne rein mit Stunden anstelle von Tagen dar. Zb.: 1 Tag 5h 25 min --> 29:25
        /// </summary>
        /// <param name="Zeit">Darzustellende Zeitspanne</param>
        /// <returns>Umgewandelte Zeitspanne als String</returns>
        private string Stundenrunder(TimeSpan Zeit)
        {
            string result = Convert.ToInt32(Math.Round(Math.Abs(Zeit.TotalHours) - 0.49, 0, MidpointRounding.AwayFromZero)).ToString("D2") + ":" + Math.Abs(Zeit.Minutes).ToString("D2");

            if (Zeit.TotalHours < 0)
            {
                result = "-" + result;
            }

            return result;
        }

        /// <summary>
        /// Verändert Werte, wie den Titel des Forms um dieses auf das Erstellen eines neuen Mitarbeiters vorzubereiten
        /// </summary>
        public void Neu()
        {
            m_lblTitel.Text = "Neuer:e Mitarbeiter:in";
            m_btnOK.Text = "Erstellen";
            Name = "Neu";
            m_btnAddCard.Text = "Karte hinzufügen";
            m_bearbeiten = false;
            m_finger = false; 
            m_card = false;

            m_btnAddCard.Enabled = false;
            m_btnAddFinger.Enabled = false;
            m_btnDeleteFinger.Enabled = false;
            m_lbxFinger.Enabled = false;

            m_toolTip.SetToolTip(m_btnAddCard, "Es kann erst eine Karte hinzugefügt werden, wenn der Mitarbeiter erstellt wurde!");
            m_toolTip.SetToolTip(m_btnAddFinger, "Es kann erst ein Finger hinzugefügt werden, wenn der Mitarbeiter erstellt wurde!");
            m_toolTip.SetToolTip(m_btnDeleteFinger, "Es kann erst mit den Fingern gearbeitet werden, wenn der Mitarbeiter erstellt wurde!");
            m_toolTip.SetToolTip(m_lbxFinger, "Es kann erst mit den Fingern gearbeitet werden, wenn der Mitarbeiter erstellt wurde!");
        }

        private void DlgMitarbeiter_MouseMove(object sender,  MouseEventArgs e)
        {
            Control control = GetChildAtPoint(e.Location);
            if (control != null)
            {
                if (!control.Enabled && m_currentToolTipControl == null)
                {
                    string toolTipString = m_toolTip.GetToolTip(control);
                    Thread.Sleep(250);
                    if (GetChildAtPoint(PointToClient(MousePosition)) != control) { return; }
                    Point coordinates = control.PointToClient(MousePosition);
                    m_toolTip.Show(toolTipString, control, coordinates.X, coordinates.Y);
                    m_currentToolTipControl = control;
                }
            }
            else
            {
                if (m_currentToolTipControl != null) m_toolTip.Hide(m_currentToolTipControl);
                m_currentToolTipControl = null;
            }
        }

        /// <summary>
        /// Liest den Urlaub und die Überstunden des Mitarbeiters aus
        /// </summary>
        /// <param name="which">true = Urlaub; false = Überstunden</param>
        /// <returns></returns>
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

        /// <summary>
        /// Wählt eine neue Mitarbeiternummer, für einen neuen Mitarbeiter, die möglichst niedrig und noch nicht belegt ist
        /// </summary>
        private void SetMitarbeiternummer()
        {
            List<ClsFingerprintRFID> list = DataProvider.SelectAllFingerprintRFID();
            List<ClsFingerprintRFID> list1 = list.OrderBy(f => f.Fingerprint).ToList();

            m_fingernumber = list1.Count + 1;
            for (int i = 0; i < list1.Count; i++)
            {
                if (list1[i].Fingerprint != i + 1)
                {
                    m_fingernumber = i + 1;
                    break;
                }
            }
        }

        private void BtnAddFinger_Click(object sender, EventArgs e)
        {
            SetMitarbeiternummer();

            MessageBox.Show("Sobald Sie auf OK drücken haben Sie 30 Sekunden Zeit den Finger am Terminal einzuspeichern. Wenn in 30 Sekunden kein Finger erkannt wird, wird der Vorgang abgebrochen!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string responseContent = DataProvider.SendRecieveHTTP("Finger" + m_fingernumber);

            if (responseContent.Contains("Finger-hinzugefuegt"))
            {
                DlgFingerName fingerName = new DlgFingerName();
                if (fingerName.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                m_fingers.Add(DataProvider.InsertFingerRFIDUID(m_fingernumber, fingerName.FingerName, m_mtbtrID));

                MessageBox.Show("Der Finger wurde erfolgreich hinzugefügt!", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                m_finger = true;
            }
            else
            {
                MessageBox.Show("Der Finger konnte nicht hinzugefügt werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDeleteFinger_Click(object sender, EventArgs e)
        {
            if((m_lbxFinger.SelectedItem as ClsFingerprintRFID) == null)
            {
                MessageBox.Show("Es wurde kein Finger ausgewählt!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Wollen Sie den ausgewählten Finger wirklich löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            DataProvider.DeleteFingerprintRFID(m_lbxFinger.SelectedItem as ClsFingerprintRFID);

            if(m_fingers.Count == 1)
            {
                m_finger = false;
                m_fingers.Clear();
            }
            else
            {
                m_fingers.Remove(m_lbxFinger.SelectedItem as ClsFingerprintRFID);
            }

            m_fingers.ResetBindings();
        }

        private void BtnAddCard_Click(object sender, EventArgs e)
        {
            if(m_card)
            {
                if (MessageBox.Show("Wollen Sie die Karte wirklich ändern?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
            }

            MessageBox.Show("Sobald Sie auf OK drücken haben Sie 30 Sekunden Zeit die Karte am Terminal einzuspeichern. Wenn in 30 Sekunden keine Karte erkannt wird, wird der Vorgang abgebrochen!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Information);

            string responseContent = DataProvider.SendRecieveHTTP("Card");

            if (responseContent.Contains("CardUID:"))
            {
                string[] parts = responseContent.Split('$');

                CardUID = parts[1];
                
                if (!m_card)
                {
                    MessageBox.Show("Karte wurde erfolgreich hinzugefügt.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Karte wurde erfolgreich geändert.", "Erfolg", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                m_card = true;
                m_btnAddCard.Text = "Karte ändern";
            }
            else
            {
                if(responseContent.Contains("ZU LANGSAM"))
                {
                    MessageBox.Show("Sie waren beim Scannen der Karte zu langsam und der Vorgang wurde nach 30 Sekunden abgebrochen!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            if (m_bearbeiten && m_arbeitsprofil.ID != (m_cmbxAProfil.SelectedItem as ClsArbeitsprofil).ID)
            {
                if (MessageBox.Show("Wollen Sie wirklich das Arbeitszeitprofil des Mitarbeiters wechseln?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    m_cmbxAProfil.SelectedItem = m_arbeitsprofil;
                    return;
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
        }
    }
}
