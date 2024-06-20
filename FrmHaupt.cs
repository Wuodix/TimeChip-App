using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TimeChip_App.Properties;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace TimeChip_App
{
    public partial class FrmHaupt : Form
    {
        static BindingList<ClsMitarbeiter> m_mitarbeiterliste = new BindingList<ClsMitarbeiter>();
        static readonly BindingList<ClsBuchung> m_buchungsliste = new BindingList<ClsBuchung>();

        public FrmHaupt()
        {
            DataProvider.Log("______________________________________________________________________________________________________________", 0);
            DataProvider.Log("App wurde gestartet", 0);
            InitializeComponent();

            m_lbxMitarbeiter.DataSource = m_mitarbeiterliste;
            m_lbxBuchungen.DataSource= m_buchungsliste;

            m_cldKalender.SelectionStart = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Subtract(new TimeSpan(1, 0, 0, 0)).Day);

            try
            {
                UpdateMtbtrList();

                if (m_mitarbeiterliste.Count > 0)
                {
                    m_lbxMitarbeiter.SelectedIndex = 0;


                    UpdateLbxBuchungen();
                    ClsBerechnung.Berechnen();

                    UpdateDataView();
                    UpdateCldKalender();
                }
            }
            catch(MySql.Data.MySqlClient.MySqlException)
            {
            
            }

            //Tab Indexe
            {
                m_btnSettings.TabIndex = 1;
                m_btnPrint.TabIndex = 2;
                m_btnarbeitszeitprofil.TabIndex = 3;
                m_btnNeuerMitarbeiter.TabIndex = 4;
                m_btnBearbeiten.TabIndex = 5;
                m_btnLöschen.TabIndex = 6;
                m_lbxMitarbeiter.TabIndex = 7;
                m_btnRefresh.TabIndex = 8;
                m_btnNeueBuchung.TabIndex = 9;
                m_btnBuchungBearbeiten.TabIndex = 10;
                m_btnBuchungLöschen.TabIndex = 11;
                m_lbxBuchungen.TabIndex = 12;
                m_cldKalender.TabIndex = 13;
                m_btnSchule.TabIndex = 14;
                m_btnKrank.TabIndex = 15;
                m_btnUnentschuldigt.TabIndex = 16;
                m_btnUrlaub.TabIndex = 17;
            }
            //Tab Indexe

        }

        public static BindingList<ClsMitarbeiter> Mitarbeiterliste { get { return m_mitarbeiterliste; } set { m_mitarbeiterliste = value; } }

        private void BtnNeu_Click(object sender, EventArgs e)
        {
            DataProvider.Log("BtnNeuer Mtbtr gedrückt", 2);
            DlgMitarbeiter neu = new DlgMitarbeiter();
            neu.Neu();
            if (neu.ShowDialog() == DialogResult.OK)
            {
                ClsMitarbeiter mtbtr = DataProvider.InsertMitarbeiter(neu.Vorname, neu.Nachname, neu.CardUID, neu.Arbeitsbeginn, neu.Überstunden, neu.Arbeitzeitprofil, neu.Urlaub);

                DateTime compare = neu.Arbeitsbeginn;
                List<DateTime> Tage = new List<DateTime>();
                //Es werden alle Tage gesucht, die berechnet werden müssen
                while (compare.CompareTo(DateTime.Now.Date) < 0)
                {
                    Tage.Add(compare);
                    compare = compare.AddDays(1);
                }

                List<ClsAusgewerteter_Tag> ausgewerteteTage = new List<ClsAusgewerteter_Tag>();
                foreach(DateTime tag in Tage)
                {
                    ausgewerteteTage.Add(ClsBerechnung.Berechnen(tag, ref mtbtr, true));
                }
                DataProvider.UpdateMitarbeiter(mtbtr);
                if(ausgewerteteTage.Count > 0)
                {
                    DataProvider.InsertMultipleAusgewerteterTag(ausgewerteteTage);

                }

                UpdateMtbtrList();
                UpdateLbxBuchungen();
            }
        }

        private void BtnBearbeiten_Click(object sender, EventArgs e)
        {
            DataProvider.Log("Btn Mtbtr Bearbeiter gedrückt", 2);
            DlgMitarbeiter Bearbeiten = new DlgMitarbeiter();

            ClsMitarbeiter zubearbeitender = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;
            Bearbeiten.Bearbeiten(zubearbeitender);

            List<ClsArbeitsprofil> clsArbeitsprofils = DlgArbeitszeitprofile.ArbeitsprofilListe;
            ClsArbeitsprofil arbeitsprofil = clsArbeitsprofils.FindLast(x => x.ID.Equals(zubearbeitender.Arbeitszeitprofil.ID));
            Bearbeiten.Arbeitzeitprofil = arbeitsprofil;

            if (Bearbeiten.ShowDialog() == DialogResult.OK)
            {
                DateTime Einspieldatum = DateTime.Now;
                if (zubearbeitender.Arbeitsbeginn.CompareTo(DateTime.Now) < 0)
                {
                    if (zubearbeitender.Arbeitszeitprofil.ID != Bearbeiten.Arbeitzeitprofil.ID ||
                        zubearbeitender.Überstunden != Bearbeiten.Überstunden ||
                        zubearbeitender.Arbeitsbeginn != Bearbeiten.Arbeitsbeginn)
                    {
                        DlgEinspieldatum dlgEinspieldatum = new DlgEinspieldatum();
                        if (dlgEinspieldatum.ShowDialog() == DialogResult.OK)
                        {
                            Einspieldatum = dlgEinspieldatum.Einspieldatum;
                        }
                    }

                    if (zubearbeitender.Arbeitszeitprofil.ID != Bearbeiten.Arbeitzeitprofil.ID)
                    {
                        DataProvider.InsertAbzpMtbtr(Bearbeiten.Arbeitzeitprofil.ID, zubearbeitender.ID, Einspieldatum);
                        ClsAbzpMtbtr abzpMtbtr = DataProvider.SelectAbzpMtbtr(zubearbeitender.Arbeitszeitprofil.ID, zubearbeitender.ID).Find(x => x.Enddate == new DateTime(2001, 1, 1));
                        DataProvider.EndAbzpMtbtr(abzpMtbtr, Einspieldatum);
                    }
                }
                
                zubearbeitender.Vorname = Bearbeiten.Vorname;
                zubearbeitender.Nachname = Bearbeiten.Nachname;
                zubearbeitender.Arbeitsbeginn = Bearbeiten.Arbeitsbeginn;
                zubearbeitender.Arbeitszeitprofil = Bearbeiten.Arbeitzeitprofil;
                zubearbeitender.Urlaub = Bearbeiten.Urlaub;
                TimeSpan ÜberstundenBuffer = zubearbeitender.Überstunden;
                zubearbeitender.Überstunden = Bearbeiten.Überstunden;
                zubearbeitender.RFIDUID = Bearbeiten.CardUID;

                DataProvider.UpdateMitarbeiter(zubearbeitender);
                if(Einspieldatum.CompareTo(DateTime.Now.Date) < 0)
                {
                    ClsBerechnung.Berechnen(Einspieldatum, DateTime.Now.Date.Subtract(new TimeSpan(1,0,0,0)), zubearbeitender, ÜberstundenBuffer != Bearbeiten.Überstunden);
                }
                UpdateMtbtrList();
                UpdateDataView();
            }
        }

        private void BtnLöschen_Click(object sender, EventArgs e)
        {
            DataProvider.Log("Btn Mtbtr Löschen gedrückt", 2);
            if (MessageBox.Show("Wollen Sie wirklich den Mitarbeiter inklusive aller aufgezeichneten Daten löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataProvider.Log("Mitarbeiter Löschen von " + (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Log() + " zugestimmt", 0);
                DataProvider.DeleteMitarbeiter(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter);
                UpdateMtbtrList();
                UpdateLbxBuchungen();
            }
        }

        private void Btnarbeitszeitprofil_Click(object sender, EventArgs e)
        {
            DataProvider.Log("Abzp Fenster geöffnet", 2);
            DlgArbeitszeitprofile arbeitszeitprofile = new DlgArbeitszeitprofile();
            arbeitszeitprofile.ShowDialog();
            UpdateMtbtrList();
        }

        /// <summary>
        /// Erneuert die angezeigten Objekte in der ListBox in der alle Mitarbeiter angezeigt werden
        /// </summary>
        private void UpdateMtbtrList()
        {
            m_mitarbeiterliste.Clear();

            foreach(ClsMitarbeiter mtbtr in DataProvider.SelectAllMitarbeiter())
            {
                m_mitarbeiterliste.Add(mtbtr);
            }

            m_mitarbeiterliste.ResetBindings();
        }

        private void LbxMitarbeiterChanged(object sender, EventArgs e)
        {
            UpdateLbxBuchungen();
        }
        private void CldKalenderChanged(object sender, DateRangeEventArgs e)
        {
            UpdateLbxBuchungen();
        }

        /// <summary>
        /// Erneuert die angezeigten Objekte in der ListBox, in der die Buchungen des ausgewählten Mitarbeiters, die am ausgewählten Tag getätigt wurden, angezeicht werden
        /// </summary>
        private void UpdateLbxBuchungen()
        {
            if (m_lbxMitarbeiter.SelectedItem != null)
            {
                List<ClsBuchung> Buchungen = DataProvider.SelectAllBuchungenFromDay(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter, m_cldKalender.SelectionStart, "buchungen");

                //List<ClsBuchung> Buchungen2 = Buchungen.FindAll(x => x.Zeit.Date.Equals(m_cldKalender.SelectionStart));

                Buchungen.Sort(delegate (ClsBuchung x, ClsBuchung y)
                {
                    return x.Zeit.CompareTo(y.Zeit);
                });

                m_buchungsliste.Clear();
                foreach(ClsBuchung buchung in Buchungen)
                {
                    m_buchungsliste.Add(buchung);
                }
                
                m_buchungsliste.ResetBindings();

                UpdateCldKalender();
                UpdateDataView();
            }
        }

        private void BtnNeueBuchung_Click(object sender, EventArgs e)
        {
            DataProvider.Log("Btn Neue Buchung gedrückt", 2);
            DlgBuchung dlgBuchung = new DlgBuchung
            {
                Titel = "Buchung hinzufügen",
                OkKnopf = "Hinzufügen",
                Name = "Neu"
            };

            List<ClsMitarbeiter> clsMitarbeiters = m_mitarbeiterliste.ToList();
            ClsMitarbeiter mitarbeiter = clsMitarbeiters.FindLast(x => x.ID.Equals((m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).ID));
            dlgBuchung.Mitarbeiter = mitarbeiter;
            dlgBuchung.Datum = m_cldKalender.SelectionStart;

            if (dlgBuchung.ShowDialog() == DialogResult.OK) 
            {
                DataProvider.InsertBuchung(dlgBuchung.Buchungstyp, dlgBuchung.GetDateTime(), dlgBuchung.Mitarbeiter.ID);

                if(DataProvider.SelectAllBuchungenFromDay(mitarbeiter, dlgBuchung.GetDateTime(), "buchungen").Count > 1)
                {
                    DateTime date = new DateTime(dlgBuchung.GetDateTime().Year, dlgBuchung.GetDateTime().Month, dlgBuchung.GetDateTime().Day);
                    ClsBerechnung.Berechnen(date, ref mitarbeiter, false);
                }

                UpdateLbxBuchungen();
            }
        }

        private void BtnBuchungBearbeiten_Click(object sender, EventArgs e)
        {
            DataProvider.Log("Btn Buchung bearbeiten gedrückt", 2);
            if (m_lbxBuchungen.SelectedItem != null)
            {
                DlgBuchung dlgBuchung = new DlgBuchung();

                ClsBuchung zubearbeiten = m_lbxBuchungen.SelectedItem as ClsBuchung;
                dlgBuchung.Titel = "Buchung bearbeiten";
                dlgBuchung.OkKnopf = "Bearbeiten";
                dlgBuchung.Name = "Bearbeiten";
                dlgBuchung.SetBuchungstyp(zubearbeiten.Buchungstyp);
                dlgBuchung.Buchungstyp = zubearbeiten.Buchungstyp;
                dlgBuchung.SetDateTime(zubearbeiten.Zeit);
                dlgBuchung.Mitarbeiter = m_mitarbeiterliste.ToList().FindLast(x => x.ID.Equals(zubearbeiten.MtbtrID));

                if(dlgBuchung.ShowDialog() == DialogResult.OK)
                {
                    zubearbeiten.Buchungstyp = dlgBuchung.Buchungstyp;
                    zubearbeiten.Zeit = dlgBuchung.GetDateTime();
                    zubearbeiten.MtbtrID = dlgBuchung.Mitarbeiter.ID;
                    ClsMitarbeiter mtbtr = m_mitarbeiterliste.ToList().Find(x => x.ID.Equals(zubearbeiten.MtbtrID));

                    DataProvider.UpdateBuchung(zubearbeiten);
                    if (DataProvider.SelectAllBuchungenFromDay(mtbtr, dlgBuchung.GetDateTime(), "buchungen").Count > 1)
                    {
                        DateTime date = new DateTime(dlgBuchung.GetDateTime().Year, dlgBuchung.GetDateTime().Month, dlgBuchung.GetDateTime().Day);
                        ClsBerechnung.Berechnen(date, ref mtbtr, false);
                    }

                    UpdateLbxBuchungen();
                }
            }
        }

        private void BtnBuchungLöschen_Click(object sender, EventArgs e)
        {
            DataProvider.Log("Btn Buchung löschen gedrückt", 2);
            if (MessageBox.Show("Wollen Sie wirklich diese Buchung löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                ClsBuchung buchung = m_lbxBuchungen.SelectedItem as ClsBuchung;
                DataProvider.DeleteBuchung(buchung, "buchungen");

                ClsMitarbeiter mtbtr = m_mitarbeiterliste.ToList().Find(x => x.ID.Equals(buchung.MtbtrID));
                if (DataProvider.SelectAllBuchungenFromDay(mtbtr, buchung.Zeit, "buchungen").Count > 1)
                {
                    DateTime date = new DateTime(buchung.Zeit.Year,buchung.Zeit.Month, buchung.Zeit.Day);
                    ClsBerechnung.Berechnen(date, ref mtbtr, false);
                }
                UpdateLbxBuchungen();
            }
        }

        /// <summary>
        /// Erneuert die fett gedruckten Tage im Tagesauswahlfeld
        /// </summary>
        private void UpdateCldKalender()
        {
            List<DateTime> BoldedDates = new List<DateTime>();

            //Statt Select last ausgewerteter Tag, muss ein Tag des ausgewählten Monats selected werden
            DateTime ausgewählter_tag = m_cldKalender.SelectionStart;
            ClsMitarbeiter mitarbeiter = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;

            DateTime startdate = new DateTime(ausgewählter_tag.Year, ausgewählter_tag.Month, 1);
            DateTime enddate = new DateTime(ausgewählter_tag.Year, ausgewählter_tag.Month, DateTime.DaysInMonth(ausgewählter_tag.Year, ausgewählter_tag.Month));
            List<ClsAusgewerteter_Tag> AusgewerteteTage = DataProvider.SelectAusgewerteterTag(startdate, enddate, mitarbeiter.ID);
            List<ClsAbzpMtbtr> abzpmtbtrs = DataProvider.SelectAbzpMtbtr(mitarbeiter);

            for (int i = 1; i<=DateTime.DaysInMonth(ausgewählter_tag.Year, ausgewählter_tag.Month); i++)
            {
                DateTime tag = new DateTime(ausgewählter_tag.Year, ausgewählter_tag.Month, i);
                ClsBerechnung.GetAbzpofDate(ref mitarbeiter, tag, abzpmtbtrs);
                if(tag.CompareTo(DateTime.Now) < 0 && tag.CompareTo(mitarbeiter.Arbeitsbeginn) > 0)
                {
                    ClsAusgewerteter_Tag ausgewtag = AusgewerteteTage.Find(x => x.MtbtrID.Equals(mitarbeiter.ID) && x.Date.Equals(tag));

                    if(ausgewtag != null)
                    {

                        if (ausgewtag.Arbeitszeit.Ticks < ClsBerechnung.GetSollArbeitszeit(tag, mitarbeiter).Ticks)
                        {
                            BoldedDates.Add(tag);
                        }
                    }
                    else
                    {
                        BoldedDates.Add(tag);
                    }
                }
            }
            m_cldKalender.BoldedDates = BoldedDates.ToArray();
        }

        /// <summary>
        /// Erneuert die Informationen des rechten unteren Teils des Hauptbildschirms
        /// </summary>
        private void UpdateDataView()
        {
            if(m_lbxMitarbeiter.SelectedItems != null)
            {
                DateTime SelectedDay = m_cldKalender.SelectionStart;
                ClsMitarbeiter mitarbeiter = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;

                List<ClsAbzpMtbtr> abzpMtbtrs = DataProvider.SelectAbzpMtbtr(mitarbeiter);
                ClsBerechnung.GetAbzpofDate(ref mitarbeiter, SelectedDay, abzpMtbtrs);

                m_lblUrlaub.Text = StundenRunderStr(mitarbeiter.Urlaub);
                m_lblÜberstunden.Text = StundenRunderStr(mitarbeiter.Überstunden);

                m_lblSoll.Text = StundenRunderStr(ClsBerechnung.GetSollArbeitszeit(SelectedDay, mitarbeiter));

                ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(SelectedDay, mitarbeiter.ID);
                
                if(tag != null)
                {
                    m_lblIst.Text = StundenRunderStr(tag.Arbeitszeit);
                    switch (tag.Status)
                    {
                        case 0:
                            m_lblTagesstatus.Text = "Zeitausgleich";
                            break;
                        case 1:
                            m_lblTagesstatus.Text = "Krank";
                            break;
                        case 2:
                            m_lblTagesstatus.Text = "Schule";
                            break;
                        case 3:
                            m_lblTagesstatus.Text = "Urlaub";
                            break;
                    }
                }
                else
                {
                    m_lblIst.Text = "00:00";
                    m_lblTagesstatus.Text = "";
                }
            }
        }

        private void BtnSchule_Click(object sender, EventArgs e)
        {
            TagesStatusÄnderung(2);
        }

        private void BtnUrlaub_Click(object sender, EventArgs e)
        {
            TagesStatusÄnderung(3);
        }

        private void BtnUnentschuldigt_Click(object sender, EventArgs e)
        {
            TagesStatusÄnderung(0);
        }

        private void BtnKrank_Click(object sender, EventArgs e)
        {
            TagesStatusÄnderung(1);
        }

        /// <summary>
        /// Ändert den Status des aktuell in cldKalender ausgewählten Tages auf den Wert von 'Status'
        /// </summary>
        /// <param name="Status">Der Status auf den der Tag geändert werden soll; 0 = Zeitausgleich, 1 = Krank, 2 = Schule, 3 = Urlaub</param>
        private void TagesStatusÄnderung(int Status)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).ID);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = Status;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
                UpdateDataView();
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DataProvider.Log("Druckvorgang bzw Monatsübersicht gestartet", 0);
            m_druckzähler = 0;
            DateTime date = m_cldKalender.SelectionStart;
            ClsMitarbeiter mtbtr = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;
            string datestr = GetMonthStr(date) + " " + date.Year.ToString();

            List<ClsAbzpMtbtr> abzpMtbtrs = DataProvider.SelectAbzpMtbtr(mtbtr);

            ClsPrintTemplate doc = new ClsPrintTemplate(mtbtr.ToString(), datestr);
            TimeSpan Monat = new TimeSpan();
            TimeSpan GesSoll = new TimeSpan();
            TimeSpan GesIst = new TimeSpan();


            for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                DateTime date1 = new DateTime(date.Year, date.Month, i);
                ClsBerechnung.GetAbzpofDate(ref mtbtr, date1, abzpMtbtrs);
                List<ClsBuchung> buchungen = DataProvider.SelectAllBuchungenFromDay(mtbtr, date1, "buchungen");
                TimeSpan SollZeit = ClsBerechnung.GetSollArbeitszeit(date1, mtbtr);
                ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(date1, mtbtr.ID);
                TimeSpan IstZeit = new TimeSpan();
                if (tag != null)
                {
                    IstZeit = tag.Arbeitszeit;
                }
                else
                {
                    MessageBox.Show("Monat ist noch nicht beendet!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    DataProvider.Log("Monat nicht beendet", 0);
                    return;
                }

                string Status;
                switch (tag.Status)
                {
                    case 1:
                        Status = "Krank";
                        break;
                    case 2:
                        Status = "Schule";
                        break;
                    case 3:
                        Status = "Urlaub";
                        break;
                    default:
                        Status = "Zeitausgleich";
                        break;
                }
                TimeSpan Überstunden = new TimeSpan(IstZeit.Ticks - SollZeit.Ticks);
                if (tag.Status == 0)
                {
                    Monat = Monat.Add(Überstunden);
                }
                string Monatstr = StundenRunderStr(Monat);
                string Überstundenstr = Überstunden.ToString(@"hh\:mm");
                string SollZeitstr = SollZeit.ToString(@"hh\:mm");
                string IstZeitstr = IstZeit.ToString(@"hh\:mm");
                string Tag = GetDayofWeekStr(date1) + ", " + date1.Day;

                GesSoll = GesSoll.Add(SollZeit);
                GesIst = GesIst.Add(IstZeit);

                doc.AddLine(Tag, buchungen, SollZeitstr, IstZeitstr, Status, Überstundenstr, Monatstr);
            }

            string GesSollstr = StundenRunderStr(GesSoll);

            string GesIststr = StundenRunderStr(GesIst);

            string Monatsüberstunden = StundenRunderStr(Monat);

            (TimeSpan, TimeSpan) Monatsübersicht = DataProvider.SelectMonatsübersicht(new DateTime(date.Year, (date.Month+1)%12, 1), mtbtr.ID);
            string GesÜberstunden = StundenRunderStr(Monatsübersicht.Item1);
            string GesUrlaub = StundenRunderStr(Monatsübersicht.Item2);

#pragma warning disable IDE0017 // Initialisierung von Objekten vereinfachen
            WebBrowser PrintBrowser = new WebBrowser();
#pragma warning restore IDE0017 // Initialisierung von Objekten vereinfachen
            PrintBrowser.DocumentText = doc.GetDoc(GesSollstr, GesIststr, Monatsüberstunden, GesÜberstunden, GesUrlaub);
            PrintBrowser.DocumentCompleted += PrintBrower_DocumentCompleted;
        }

        /// <summary>
        /// Rechnet zurück wie viele Überstunden mtbtr am Ende des Monats von date hatte
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mtbtr"></param>
        /// <returns></returns>
        public TimeSpan GetOldÜberstunden(DateTime date, ClsMitarbeiter mtbtr)
        {
            TimeSpan aktuelleÜberstunden = mtbtr.Überstunden;
            DateTime lastDayofMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            DateTime Yesterday = DateTime.Today.Date.Subtract(new TimeSpan(1,0,0,0));
            List<ClsAusgewerteter_Tag> days = DataProvider.SelectAusgewerteterTag(lastDayofMonth, Yesterday, mtbtr.ID);
            List<ClsAbzpMtbtr> abzpmtbtrs = DataProvider.SelectAbzpMtbtr(mtbtr);

            foreach(ClsAusgewerteter_Tag tag in days)
            {
                ClsBerechnung.GetAbzpofDate(ref mtbtr, tag.Date, abzpmtbtrs);
                TimeSpan Üst = tag.Arbeitszeit - ClsBerechnung.GetSollArbeitszeit(tag.Date, mtbtr);
                aktuelleÜberstunden = aktuelleÜberstunden.Subtract(Üst);
            }

            return aktuelleÜberstunden;
        }

        /// <summary>
        /// Stellt eine Zeitspanne rein mit Stunden anstelle von Tagen dar. Zb.: 1 Tag 5h 25 min --> 29:25
        /// </summary>
        /// <param name="Zeit">Darzustellende Zeitspanne</param>
        /// <returns>Umgewandelte Zeitspanne als String</returns>
        public string StundenRunderStr(TimeSpan Zeit)
        {
            string result = Convert.ToInt32(Math.Round(Math.Abs(Zeit.TotalHours) - 0.49, 0, MidpointRounding.AwayFromZero)).ToString("D2") + ":" + Math.Abs(Zeit.Minutes).ToString("D2");

            if (Zeit.TotalHours < 0)
                result = "-" + result;

            return result;
        }

        int m_druckzähler = 0;
        /// <summary>
        /// Wird ausgeführt sobald der Print Browser das Laden der zu druckenden Seite vollendet hat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintBrower_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            m_druckzähler++;
            if (m_druckzähler == 1)
            {
                ((WebBrowser)sender).ShowPrintDialog();
                //((WebBrowser)sender).Print();
            }
            else
            {
                ((WebBrowser)sender).Dispose();
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            DataProvider.Log("Versucht Daten mithilfe des Refresh Knopfs zu aktualisieren", 1);
            DateTime date = m_cldKalender.SelectionStart;
            ClsMitarbeiter mtbtr = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(date, mtbtr.ID);

            if(tag != null)
            {
                ClsBerechnung.Berechnen(date, ref mtbtr, false,false,null,tag);
                UpdateCldKalender();
                UpdateDataView();
            }
            else
            {
                MessageBox.Show("Der Tag wurde noch nicht berechnet oder liegt in der Zukunft!","Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Gibt den Namen des Monats von date zurück. Zb.: 1 --> Jänner; 2 --> Februar etc.
        /// </summary>
        /// <param name="date">Datum, dessen Monats Name gesucht wird</param>
        /// <returns>Name des Monats</returns>
        public string GetMonthStr(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                    return "Jänner";
                case 2:
                    return "Februar";
                case 3:
                    return "März";
                case 4:
                    return "April";
                case 5:
                    return "Mai";
                case 6:
                    return "Juni";
                case 7:
                    return "Juli";
                case 8:
                    return "August";
                case 9:
                    return "September";
                case 10:
                    return "Oktober";
                case 11:
                    return "November";
                case 12:
                    return "Dezember";
            }
            return date.Month.ToString();
        }

        /// <summary>
        /// Gibt den Namen des Wochentags von date zurück
        /// </summary>
        /// <param name="date">Datum dessen Wochentags Name gesucht wird</param>
        /// <returns>Den Namen des Wochentags von date</returns>
        public string GetDayofWeekStr(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "Montag";
                case DayOfWeek.Tuesday:
                    return "Dienstag";
                case DayOfWeek.Wednesday:
                    return "Mittwoch";
                case DayOfWeek.Thursday:
                    return "Donnerstag";
                case DayOfWeek.Friday:
                    return "Freitag";
                case DayOfWeek.Saturday:
                    return "Samstag";
                case DayOfWeek.Sunday:
                    return "Sonntag";
            }
            return date.Day.ToString();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            DataProvider.Log("Settingsfenster wird geöffnet", 2);
            DlgSettings settings = new DlgSettings();

            string connectionstring;

            if (settings.ShowDialog() == DialogResult.OK)
            {
                connectionstring = "SERVER=";
                connectionstring += settings.IP;
                connectionstring += ";DATABASE=";
                connectionstring += settings.Database;
                connectionstring += ";UID=";
                connectionstring += settings.UID;
                connectionstring += ";Password=";
                connectionstring += settings.Password;
                connectionstring += ";";

                DataProvider.ConnectionString = connectionstring;
                DataProvider.SaveConnectionString();

                DataProvider.SaveBerechnungsdate(settings.Berechnungsdate);

                DataProvider.SaveArduinoIP(settings.ArduinoIP);

                DataProvider.SaveLogLevel(settings.LogLevel);

                if (MessageBox.Show("Die App wird neu gestartet um die Änderungen verarbeiten zu können!", "Achtung!", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    DataProvider.Log("App wegen Settingsänderung neugestartet", 0);
                    Application.Restart();
                }
            }
        }
    }
}
