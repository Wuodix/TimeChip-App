using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace TimeChip_App
{
    public partial class FrmHaupt : Form
    {
        static BindingList<ClsMitarbeiter> m_mitarbeiterliste = new BindingList<ClsMitarbeiter>();
        static readonly BindingList<ClsBuchung> m_buchungsliste = new BindingList<ClsBuchung>();

        public FrmHaupt()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);
            
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
        }

        static void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Handler caught: " + ((Exception)e.ExceptionObject).Message);
        }

        public static BindingList<ClsMitarbeiter> Mitarbeiterliste { get { return m_mitarbeiterliste; } set { m_mitarbeiterliste = value; } }

        private void BtnNeu_Click(object sender, EventArgs e)
        {
            DlgMitarbeiter neu = new DlgMitarbeiter();
            neu.Neu();
            if (neu.ShowDialog() == DialogResult.OK)
            {
                DataProvider.InsertMitarbeiter(neu.Mitarbeiternummer,neu.Vorname, neu.Nachname, neu.Arbeitsbeginn, neu.Überstunden, neu.Arbeitzeitprofil, neu.Urlaub);
                UpdateMtbtrList();
                UpdateLbxBuchungen();
            }
        }

        private void BtnBearbeiten_Click(object sender, EventArgs e)
        {
            DlgMitarbeiter Bearbeiten = new DlgMitarbeiter();

            ClsMitarbeiter zubearbeitender = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;
            Bearbeiten.Bearbeiten(zubearbeitender);

            Debug.WriteLine(zubearbeitender.Überstunden);

            List<ClsArbeitsprofil> clsArbeitsprofils = DlgArbeitszeitprofile.ArbeitsprofilListe.ToList();
            ClsArbeitsprofil arbeitsprofil = clsArbeitsprofils.FindLast(x => x.ID.Equals(zubearbeitender.Arbeitszeitprofil.ID));
            Bearbeiten.Arbeitzeitprofil = arbeitsprofil;

            if (Bearbeiten.ShowDialog() == DialogResult.OK)
            {
                zubearbeitender.Vorname = Bearbeiten.Vorname;
                zubearbeitender.Nachname = Bearbeiten.Nachname;
                zubearbeitender.Arbeitsbeginn = Bearbeiten.Arbeitsbeginn;
                zubearbeitender.Arbeitszeitprofil = Bearbeiten.Arbeitzeitprofil;
                zubearbeitender.Mitarbeiternummer = Bearbeiten.Mitarbeiternummer;
                zubearbeitender.Urlaub = Bearbeiten.Urlaub;
                zubearbeitender.Überstunden = Bearbeiten.Überstunden;

                Debug.WriteLine("FrmHaupt Urlaub: " + zubearbeitender.Urlaub);

                DataProvider.UpdateMitarbeiter(zubearbeitender);
                UpdateMtbtrList();
                UpdateDataView();
            }
        }

        private void BtnLöschen_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Wollen Sie wirklich den Mitarbeiter inklusive aller aufgezeichneten Daten löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataProvider.DeleteMitarbeiter(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter);
                UpdateMtbtrList();
                UpdateLbxBuchungen();
            }
        }

        private void Btnarbeitszeitprofil_Click(object sender, EventArgs e)
        {
            DlgArbeitszeitprofile arbeitszeitprofile = new DlgArbeitszeitprofile();
            arbeitszeitprofile.ShowDialog();
            UpdateMtbtrList();
        }

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

        private void UpdateLbxBuchungen()
        {
            if (m_lbxMitarbeiter.SelectedItem != null)
            {
                List<ClsBuchung> Buchungen = DataProvider.SelectAllBuchungenFromDay(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter, m_cldKalender.SelectionStart, "buchungen");

                //List<ClsBuchung> Buchungen2 = Buchungen.FindAll(x => x.Zeit.Date.Equals(m_cldKalender.SelectionStart));

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
                DataProvider.InsertBuchung(dlgBuchung.Buchungstyp, dlgBuchung.GetDateTime(), dlgBuchung.Mitarbeiter.Mitarbeiternummer);

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
                dlgBuchung.Mitarbeiter = m_mitarbeiterliste.ToList().FindLast(x => x.Mitarbeiternummer.Equals(zubearbeiten.Mitarbeiternummer));

                if(dlgBuchung.ShowDialog() == DialogResult.OK)
                {
                    zubearbeiten.Buchungstyp = dlgBuchung.Buchungstyp;
                    zubearbeiten.Zeit = dlgBuchung.GetDateTime();
                    zubearbeiten.Mitarbeiternummer = dlgBuchung.Mitarbeiter.Mitarbeiternummer;
                    ClsMitarbeiter mtbtr = m_mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(zubearbeiten.Mitarbeiternummer));

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
            if(MessageBox.Show("Wollen Sie wirklich diese Buchung löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                ClsBuchung buchung = m_lbxBuchungen.SelectedItem as ClsBuchung;
                DataProvider.DeleteBuchung(buchung, "buchungen");

                ClsMitarbeiter mtbtr = m_mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(buchung.Mitarbeiternummer));
                if (DataProvider.SelectAllBuchungenFromDay(mtbtr, buchung.Zeit, "buchungen").Count > 1)
                {
                    DateTime date = new DateTime(buchung.Zeit.Year,buchung.Zeit.Month, buchung.Zeit.Day);
                    ClsBerechnung.Berechnen(date, ref mtbtr, false);
                }
                UpdateLbxBuchungen();
            }
        }

        private void UpdateCldKalender()
        {
            List<DateTime> BoldedDates = new List<DateTime>();

            //Statt Select last ausgewerteter Tag, muss ein Tag des ausgewählten Monats selected werden
            DateTime ausgewählter_tag = m_cldKalender.SelectionStart;
            ClsMitarbeiter mitarbeiter = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;

            DateTime startdate = new DateTime(ausgewählter_tag.Year, ausgewählter_tag.Month, 1);
            DateTime enddate = new DateTime(ausgewählter_tag.Year, ausgewählter_tag.Month, DateTime.DaysInMonth(ausgewählter_tag.Year, ausgewählter_tag.Month));
            List<ClsAusgewerteter_Tag> AusgewerteteTage = DataProvider.SelectAusgewerteteTage(startdate, enddate, mitarbeiter.Mitarbeiternummer);

            for (int i = 1; i<=DateTime.DaysInMonth(ausgewählter_tag.Year, ausgewählter_tag.Month); i++)
            {
                DateTime tag = new DateTime(ausgewählter_tag.Year, ausgewählter_tag.Month, i);
                if(tag.CompareTo(DateTime.Now) < 0 && tag.CompareTo(mitarbeiter.Arbeitsbeginn) > 0)
                {
                    ClsAusgewerteter_Tag ausgewtag = AusgewerteteTage.Find(x => x.MitarbeiterNummer.Equals(mitarbeiter.Mitarbeiternummer) && x.Date.Equals(tag));

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

        private void UpdateDataView()
        {
            if(m_lbxMitarbeiter.SelectedItems != null)
            {
                DateTime SelectedDay = m_cldKalender.SelectionStart;
                ClsMitarbeiter mitarbeiter = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;

                m_lblUrlaub.Text = StundenRunderStr(mitarbeiter.Urlaub);
                m_lblÜberstunden.Text = StundenRunderStr(mitarbeiter.Überstunden);

                m_lblSoll.Text = StundenRunderStr(ClsBerechnung.GetSollArbeitszeit(SelectedDay, mitarbeiter));

                ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(SelectedDay, mitarbeiter.Mitarbeiternummer);
                
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
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 2;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
                UpdateDataView();
            }
        }

        private void BtnUrlaub_Click(object sender, EventArgs e)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 3;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
                UpdateDataView();
            }
        }

        private void BtnUnentschuldigt_Click(object sender, EventArgs e)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 0;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
                UpdateDataView();
            }
        }

        private void BtnKrank_Click(object sender, EventArgs e)
        {
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(m_cldKalender.SelectionStart, (m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).Mitarbeiternummer);
            if (tag != null)
            {
                int alterStatus = tag.Status;
                tag.Status = 1;

                ClsBerechnung.TagesStatusÄnderung(tag, alterStatus);
                UpdateDataView();
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            m_druckzähler = 0;
            DateTime date = m_cldKalender.SelectionStart;
            ClsMitarbeiter mtbtr = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;
            string datestr = GetMonthStr(date) + " " + date.Year.ToString();

            ClsPrintTemplate doc = new ClsPrintTemplate(mtbtr.ToString(), datestr);
            TimeSpan Monat = new TimeSpan();
            TimeSpan GesSoll = new TimeSpan();
            TimeSpan GesIst = new TimeSpan();


            for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                DateTime date1 = new DateTime(date.Year, date.Month, i);
                List<ClsBuchung> buchungen = DataProvider.SelectAllBuchungenFromDay(mtbtr, date1, "buchungen");
                TimeSpan SollZeit = ClsBerechnung.GetSollArbeitszeit(date1, mtbtr);
                ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(date1, mtbtr.Mitarbeiternummer);
                TimeSpan IstZeit = new TimeSpan();
                if (tag != null)
                {
                    IstZeit = tag.Arbeitszeit;
                }
                else
                {
                    MessageBox.Show("Monat ist noch nicht beendet!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            string GesÜberstunden = StundenRunderStr(GetOldÜberstunden(date, mtbtr));

            string GesUrlaub = mtbtr.Urlaub.TotalHours.ToString() + ":00";

            WebBrowser PrintBrowser = new WebBrowser();
            PrintBrowser.DocumentText = doc.GetDoc(GesSollstr, GesIststr, Monatsüberstunden, GesÜberstunden, GesUrlaub);
            PrintBrowser.DocumentCompleted += PrintBrower_DocumentCompleted;
        }

        public TimeSpan GetOldÜberstunden(DateTime date, ClsMitarbeiter mtbtr)
        {
            TimeSpan aktuelleÜberstunden = mtbtr.Überstunden;
            DateTime lastDayofMonth = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
            for (DateTime today = DateTime.Today.Subtract(new TimeSpan(1,0,0,0)); today.CompareTo(lastDayofMonth) > 0; today = today.Subtract(new TimeSpan(1, 0, 0, 0)))
            {
                ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(today, mtbtr.Mitarbeiternummer);
                TimeSpan Üst = tag.Arbeitszeit - ClsBerechnung.GetSollArbeitszeit(today, mtbtr);
                aktuelleÜberstunden = aktuelleÜberstunden.Subtract(Üst);
            }

            return aktuelleÜberstunden;
        }

        public string StundenRunderStr(TimeSpan Zeit)
        {
            string result = Convert.ToInt32(Math.Round(Math.Abs(Zeit.TotalHours) - 0.49, 0, MidpointRounding.AwayFromZero)).ToString("D2") + ":" + Math.Abs(Zeit.Minutes).ToString("D2");

            if (Zeit.TotalHours < 0)
                result = "-" + result;

            return result;
        }

        int m_druckzähler = 0;
        private void PrintBrower_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            m_druckzähler++;
            Debug.WriteLine(m_druckzähler);
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
            DateTime date = m_cldKalender.SelectionStart;
            ClsMitarbeiter mtbtr = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;
            ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(date, mtbtr.Mitarbeiternummer);

            if(tag != null)
            {
                ClsBerechnung.Berechnen(date, ref mtbtr, false);
                UpdateCldKalender();
                UpdateDataView();
            }
            else
            {
                MessageBox.Show("Der Tag wurde noch nicht berechnet oder liegt in der Zukunft!","Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void Settings_Click(object sender, EventArgs e)
        {
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

                if (MessageBox.Show("Die App wird neu gestartet um die Änderungen verarbeiten zu können!", "Achtung!", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    Application.Restart();
                }
            }
        }
    }
}
