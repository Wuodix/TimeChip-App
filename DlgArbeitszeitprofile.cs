using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Windows.Forms;

namespace TimeChip_App
{
    public partial class DlgArbeitszeitprofile : Form
    {
        static List<ClsArbeitsprofil> m_arbeitsprofilliste = new List<ClsArbeitsprofil>();
        private ClsTag m_kopierterTag = null;
        public DlgArbeitszeitprofile()
        {
            InitializeComponent();

            UpdateAbzpList();

            m_cmbxAbzp.SelectedItem = null;


            //*TODO*
            //Tab Indexe für Controls in Tabelle festlegen
            //*TODO*
        }

        public static List<ClsArbeitsprofil> ArbeitsprofilListe { get { return m_arbeitsprofilliste; } set { m_arbeitsprofilliste = value; } }

        private void BtnNeu_Click(object sender, EventArgs e)
        {
            ClearTable();
        }

        /// <summary>
        /// Aktualisiert das ausgewähle Abzp mit den neu eingegebenen Daten
        /// </summary>
        private void AbzpAktualisieren()
        {
            ClsArbeitsprofil Aktualisieren = m_cmbxAbzp.SelectedItem as ClsArbeitsprofil;

            Aktualisieren.Name = m_tbxName.Text;
            Aktualisieren.Gleitzeit = m_cbGleitzeit.Checked;

            List<ClsTag> Tagetemp = ReadalleTagevonTabelle();
            List<ClsTag> abzpTage = new List<ClsTag>
            {
                Aktualisieren.Montag,
                Aktualisieren.Dienstag,
                Aktualisieren.Mittwoch,
                Aktualisieren.Donnerstag,
                Aktualisieren.Freitag,
                Aktualisieren.Samstag,
                Aktualisieren.Sonntag
            };

            List<ClsTag[]> GleicheTage = new List<ClsTag[]>();
            List<ClsTag> ZulöschendeTage= new List<ClsTag>();

            for(int i = 0; i < Tagetemp.Count; i++)
            {
                if (CompareDays(Tagetemp[i], abzpTage[i]))
                {
                    GleicheTage.Add(new ClsTag[] { Tagetemp[i], abzpTage[i] });
                }
                else
                {
                    ZulöschendeTage.Add(abzpTage[i]);
                    GleicheTage.Add(new ClsTag[] { null, null });
                }
            }

            for(int i = 0 ; i < 7 ; i++)
            {
                ClsTag tag;
                if (GleicheTage[i][0] == null)
                {
                    tag = DataProvider.InsertTag(Tagetemp[i]);
                }
                else
                {
                    tag = abzpTage[i];
                }

                switch (i)
                {
                    case 0 :
                        Aktualisieren.Montag = tag; break;
                    case 1 :
                        Aktualisieren.Dienstag = tag; break;
                    case 2 :
                        Aktualisieren.Mittwoch = tag; break;
                    case 3:
                        Aktualisieren.Donnerstag = tag; break;
                    case 4 :
                        Aktualisieren.Freitag = tag; break;
                    case 5 :
                        Aktualisieren.Samstag = tag; break;
                    case 6:
                        Aktualisieren.Sonntag = tag; break;
                }
            }

            DataProvider.UpdateArbeitszeitprofil(Aktualisieren);

            foreach (ClsTag tag in ZulöschendeTage)
            {
                DataProvider.DeleteTag(tag);
            }

            UpdateAbzpList();
            ClearTable();
        }

        private bool CompareDays(ClsTag x, ClsTag y)
        {
            bool gleich = false;

            if(x.Arbeitsbeginn == y.Arbeitsbeginn && x.Arbeitsende == y.Arbeitsende && x.Arbeitszeit == y.Arbeitszeit && x.Pause == y.Pause &&
                x.Pausenbeginn == y.Pausenbeginn && x.Pausenende == y.Pausenende && x.Pausendauer == y.Pausendauer)
            {
                gleich = true;
            }
            return gleich;
        }

        private void BtnSpeichern_Click(object sender, EventArgs e)
        {
            if(m_cmbxAbzp.Text != string.Empty)
            {
                AbzpAktualisieren();
                return;
            }

            List<ClsTag> Tagetemp = ReadalleTagevonTabelle();

            List<ClsTag> Tage = new List<ClsTag>();
            foreach(ClsTag t in Tagetemp)
            {
                Tage.Add(DataProvider.InsertTag(t.Arbeitsbeginn, t.Arbeitsende, t.Arbeitszeit, t.Pause, t.Pausenbeginn, t.Pausenende, t.Pausendauer));
            }

            DataProvider.InsertArbeitszeitprofil(m_tbxName.Text, Tage[0], Tage[1], Tage[2], Tage[3], Tage[4], Tage[5], Tage[6], m_cbGleitzeit.Checked);
            UpdateAbzpList();
            ClearTable();
        }

        /// <summary>
        /// Liest alle eingegebenen Daten aus der Tabelle aus gibt sie als Liste aus Tagen zurück
        /// </summary>
        /// <returns>Die eingegebenen Daten als Liste aus Tagen</returns>
        private List<ClsTag> ReadalleTagevonTabelle()
        {
            List<ClsTag> Tage = new List<ClsTag>();
            for (int i = 1; i <= 7; i++)
            {
                Tage.Add(ReadTagvonTabelle(i));
            }
            return Tage;
        }

        /// <summary>
        /// Liest einen einzelnen Tag aus der Tabelle aus
        /// </summary>
        /// <param name="Row">Die Zeile aus der der Tag ausgelesen werden soll (1-7)</param>
        /// <returns>Der ausgelesene Tag</returns>
        private ClsTag ReadTagvonTabelle(int Row)
        {
            List<TimeSpan> Werte = new List<TimeSpan>();

            for(int i = 1; i<=7; i++)
            {
                if (i != 4)
                {
                    Werte.Add((m_tlpTabelle.GetControlFromPosition(i,Row) as DateTimePicker).Value.TimeOfDay);
                }
                else
                {
                    Werte.Add(new TimeSpan(0));
                }
            }

            bool Pause = (m_tlpTabelle.GetControlFromPosition(4, Row) as CheckBox).Checked;

            return new ClsTag(1, Werte[0], Werte[1], Werte[2], Pause, Werte[4], Werte[5], Werte[6]);
        }

        /// <summary>
        /// Löscht alle eingegebenen Daten aus der Tabelle
        /// </summary>
        private void ClearTable()
        {
            for(int i = 1; i<=7; i++)
            {
                for(int j = 1;j<=7; j++)
                {
                    if(j != 4)
                    {
                        (m_tlpTabelle.GetControlFromPosition(j, i) as DateTimePicker).Value = DateTime.Today;
                    }
                    else
                    {
                        (m_tlpTabelle.GetControlFromPosition(j, i) as CheckBox).Checked = false;
                    }
                }
            }

            m_cmbxAbzp.Text = string.Empty;
            m_cbGleitzeit.Checked = false;
            m_tbxName.Text = string.Empty;
        }

        private void BtnLöschen_Click(object sender, EventArgs e)
        {
            ClsArbeitsprofil abzp = m_cmbxAbzp.SelectedItem as ClsArbeitsprofil;
            List<ClsMitarbeiter> mtbtrs = DataProvider.SelectAllMitarbeiter();

            bool fehler = false;

            foreach (ClsMitarbeiter mtbtr in mtbtrs)
            {
                if (mtbtr.Arbeitszeitprofil.ID == abzp.ID) { fehler = true; break; }
            }

            if (fehler)
            {
                MessageBox.Show("Das Arbeitszeitprofil wird noch verwendet und kann deshalb nicht gelöscht werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (MessageBox.Show("Wollen Sie wirklich das Arbeitszeitprofil löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataProvider.DeleteArbeitszeitprofil(abzp);
                UpdateAbzpList();
            }
        }

        /// <summary>
        /// Aktualisiert die Objekte, die in der Listbox, die sämtliche Arbeitszeitprofile anzeigt, angezeigt werden
        /// </summary>
        private void UpdateAbzpList()
        {
            m_arbeitsprofilliste.Clear();

            foreach (ClsArbeitsprofil abzp in DataProvider.SelectAllArbeitszeitprofil())
            {
                m_arbeitsprofilliste.Add(abzp);
            }
            UpdateAbzpCmbx();
        }

        /// <summary>
        /// Aktualisiert die Objekte in der Combobox, mit der man die erstellten Arbeitszeitprofile zur Bearbeitung auswählen kann
        /// </summary>
        private void UpdateAbzpCmbx()
        {
            m_cmbxAbzp.Items.Clear();
            foreach(ClsArbeitsprofil abzp in m_arbeitsprofilliste)
            {
                m_cmbxAbzp.Items.Add(abzp);
            }
        }

        /// <summary>
        /// Aktualisiert die Daten in den Feldern der Tabelle um das ausgewählte Abzp anzeigen zu können, sollte ein neues ausgewählt werden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbxAbzp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(m_arbeitsprofilliste.Count > 0 && m_cmbxAbzp.SelectedItem != null)
            {
                ClsArbeitsprofil auswählen = m_cmbxAbzp.SelectedItem as ClsArbeitsprofil;

                FillTable(auswählen);
            }
        }

        /// <summary>
        /// Füllt die ganze Tabelle mit den Daten eines Arbeitszeitprofils
        /// </summary>
        /// <param name="abzp">Das Arbeitszeitprofil, das in die Tabelle geladen werden soll</param>
        private void FillTable(ClsArbeitsprofil abzp)
        {
            m_tbxName.Text = abzp.Name;
            m_cbGleitzeit.Checked = abzp.Gleitzeit;

            FillLineinTable(abzp.Montag, 1);
            FillLineinTable(abzp.Dienstag, 2);
            FillLineinTable(abzp.Mittwoch, 3);
            FillLineinTable(abzp.Donnerstag, 4);
            FillLineinTable(abzp.Freitag, 5);
            FillLineinTable(abzp.Samstag, 6);
            FillLineinTable(abzp.Sonntag, 7);
        }

        /// <summary>
        /// Füllt eine Zeile in der darstellenden Tabelle mit den Daten eines Tages
        /// </summary>
        /// <param name="tag">Der Tag dessen Daten eingetragen werden sollen</param>
        /// <param name="Zeile">Der Wochentag in den die Daten eingetragen werden sollen. 1 = Montag, 2 = Dienstag...</param>
        private void FillLineinTable(ClsTag tag, int Zeile)
        {
            for(int i = 1; i <= 7; i++)
            {

                Control control = m_tlpTabelle.GetControlFromPosition(i, Zeile);
                switch (i)
                {
                    case 1:
                        (control as DateTimePicker).Value = DateTime.Today.Add(tag.Arbeitsbeginn); break;
                    case 2:
                        (control as DateTimePicker).Value = DateTime.Today.Add(tag.Arbeitsende); break;
                    case 3:
                        (control as DateTimePicker).Value = DateTime.Today.Add(tag.Arbeitszeit); break;
                    case 4:
                        (control as CheckBox).Checked = tag.Pause; break;
                    case 5:
                        (control as DateTimePicker).Value = DateTime.Today.Add(tag.Pausenbeginn); break;
                    case 6:
                        (control as DateTimePicker).Value = DateTime.Today.Add(tag.Pausenende); break;
                    case 7:
                        (control as DateTimePicker).Value = DateTime.Today.Add(tag.Pausendauer); break;
                }
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
            int Reihe = m_tlpTabelle.GetPositionFromControl((sender as Button).Parent as Control).Row;

            m_kopierterTag = ReadTagvonTabelle(Reihe);
        }

        private void BtnPaste_Click(object sender, EventArgs e)
        {
            if(m_kopierterTag == null) return;
            int Reihe = m_tlpTabelle.GetPositionFromControl((sender as Button).Parent as Control).Row;

            FillLineinTable(m_kopierterTag, Reihe);
        }
    }
}
