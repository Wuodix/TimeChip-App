using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TimeChip_App_1._0
{
    public partial class FrmHaupt : Form
    {
        static BindingList<ClsMitarbeiter> m_mitarbeiterliste = new BindingList<ClsMitarbeiter>();
        static BindingList<ClsBuchung> m_buchungsliste = new BindingList<ClsBuchung>();
        private StreamReader m_printStream;

        public FrmHaupt()
        {
            InitializeComponent();

            m_lbxMitarbeiter.DataSource = m_mitarbeiterliste;
            m_lbxBuchungen.DataSource= m_buchungsliste;

            UpdateMtbtrList();

            m_lbxMitarbeiter.SelectedIndex = 1;

            UpdateLbxBuchungen();

            ClsBerechnung.Berechnen();

            UpdateDataView();
            UpdateCldKalender();
        }

        public static BindingList<ClsMitarbeiter> Mitarbeiterliste { get { return m_mitarbeiterliste; } set { m_mitarbeiterliste = value; } }

        private void m_btnNeu_Click(object sender, EventArgs e)
        {
            DlgMitarbeiter neu = new DlgMitarbeiter();
            neu.Titel = "Neuer:e Mitarbeiter:in";
            neu.OkKnopf = "Erstellen";
            neu.Name = "Neu";
            neu.BtnAddFinger = "Finger hinzufügen";
            neu.BtnAddCard = "Karte hinzufügen";
            neu.Bearbeiten = false;
            if (neu.ShowDialog() == DialogResult.OK)
            {
                DataProvider.InsertMitarbeiter(neu.Mitarbeiternummer,neu.Vorname, neu.Nachname, neu.Arbeitsbeginn, new TimeSpan(0), neu.Arbeitzeitprofil, neu.GetUrlaub());
                UpdateMtbtrList();
                UpdateLbxBuchungen();
            }
        }

        private void m_btnBearbeiten_Click(object sender, EventArgs e)
        {
            DlgMitarbeiter Bearbeiten = new DlgMitarbeiter();

            /*
            for(int i = 0; i<m_arbeitsprofilliste.Count; i++)
            {
                Bearbeiten.m_cmbxAProfil.Items.Add(m_arbeitsprofilliste[i]);
            }
            */

            ClsMitarbeiter zubearbeitender = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;
            Bearbeiten.Titel = "Mitarbeiter:in bearbeiten";
            Bearbeiten.OkKnopf = "Bearbeiten";
            Bearbeiten.Name = "Bearbeiten";
            Bearbeiten.BtnAddFinger = "Finger ändern";
            Bearbeiten.BtnAddCard = "Karte ändern";
            Bearbeiten.Vorname = zubearbeitender.Vorname;
            Bearbeiten.Nachname = zubearbeitender.Nachname;
            Bearbeiten.Arbeitsbeginn = zubearbeitender.Arbeitsbeginn;
            Bearbeiten.Bearbeiten = true;
            Bearbeiten.Mitarbeiternummer = zubearbeitender.Mitarbeiternummer;
            Bearbeiten.SetUrlaub(zubearbeitender.Urlaub);

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
                zubearbeitender.Urlaub = Bearbeiten.GetUrlaub();

                DataProvider.UpdateMitarbeiter(zubearbeitender);
                m_mitarbeiterliste.ResetBindings();
            }
        }

        private void m_btnLöschen_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Wollen Sie wirklich den Mitarbeiter inklusive aller aufgezeichneten Daten löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DataProvider.DeleteMitarbeiter(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter);
                UpdateMtbtrList();
                UpdateLbxBuchungen();
            }
        }

        private void m_btnarbeitszeitprofil_Click(object sender, EventArgs e)
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

        private void m_lbxMitarbeiterChanged(object sender, EventArgs e)
        {
            UpdateLbxBuchungen();
        }
        private void m_cldKalenderChanged(object sender, DateRangeEventArgs e)
        {
            UpdateLbxBuchungen();
        }

        private void UpdateLbxBuchungen()
        {
            if (m_lbxMitarbeiter.SelectedItem != null)
            {
                List<ClsBuchung> Buchungen = DataProvider.SelectAllBuchungenFromDay(m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter, m_cldKalender.SelectionStart, "buchungen_temp");

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

        private void m_btnNeueBuchung_Click(object sender, EventArgs e)
        {
            DlgBuchung dlgBuchung = new DlgBuchung();

            dlgBuchung.Titel = "Buchung hinzufügen";
            dlgBuchung.OkKnopf = "Hinzufügen";
            dlgBuchung.Name = "Neu";

            List<ClsMitarbeiter> clsMitarbeiters = m_mitarbeiterliste.ToList();
            ClsMitarbeiter mitarbeiter = clsMitarbeiters.FindLast(x => x.ID.Equals((m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter).ID));
            dlgBuchung.Mitarbeiter = mitarbeiter;
            dlgBuchung.Datum = m_cldKalender.SelectionStart;

            if (dlgBuchung.ShowDialog() == DialogResult.OK) 
            {
                DataProvider.InsertBuchung(dlgBuchung.Buchungstyp, dlgBuchung.GetDateTime(), dlgBuchung.Mitarbeiter.Mitarbeiternummer);

                if(DataProvider.SelectAllBuchungenFromDay(mitarbeiter, dlgBuchung.GetDateTime(), "buchungen_temp").Count > 1)
                {
                    ClsBerechnung.Berechnen(dlgBuchung.Datum, ref mitarbeiter, false);
                }

                UpdateLbxBuchungen();
            }
        }

        private void m_btnBuchungBearbeiten_Click(object sender, EventArgs e)
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
                    if (DataProvider.SelectAllBuchungenFromDay(mtbtr, dlgBuchung.GetDateTime(), "buchungen_temp").Count > 1)
                    {
                        ClsBerechnung.Berechnen(dlgBuchung.GetDateTime(), ref mtbtr, false);
                    }

                    UpdateLbxBuchungen();
                }
            }
        }

        private void m_btnBuchungLöschen_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Wollen Sie wirklich diese Buchung löschen?", "Achtung", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes){
                ClsBuchung buchung = m_lbxBuchungen.SelectedItem as ClsBuchung;
                DataProvider.DeleteBuchung(buchung, "buchungen");

                ClsMitarbeiter mtbtr = m_mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(buchung.Mitarbeiternummer));
                if (DataProvider.SelectAllBuchungenFromDay(mtbtr, buchung.Zeit, "buchungen").Count > 1)
                {
                    ClsBerechnung.Berechnen(buchung.Zeit, ref mtbtr, false);
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

            for (int i = 1; i<=DateTime.DaysInMonth(ausgewählter_tag.Year, ausgewählter_tag.Month); i++)
            {
                DateTime tag = new DateTime(ausgewählter_tag.Year, ausgewählter_tag.Month, i);
                if(tag.CompareTo(DateTime.Now) < 0 && tag.CompareTo(mitarbeiter.Arbeitsbeginn) > 0)
                {
                    ClsAusgewerteter_Tag ausgewtag = DataProvider.SelectAusgewerteterTag(tag, mitarbeiter.Mitarbeiternummer);

                    if(ausgewtag != null)
                    {
                        Debug.WriteLine(ausgewtag.Date);
                        Debug.WriteLine(ausgewtag.Arbeitszeit);
                        Debug.WriteLine(ClsBerechnung.GetSollArbeitszeit(tag, mitarbeiter));

                        if (ausgewtag.Arbeitszeit.Ticks < ClsBerechnung.GetSollArbeitszeit(tag, mitarbeiter).Ticks)
                        {
                            BoldedDates.Add(tag);
                        }
                    }
                    else
                    {
                        BoldedDates.Add(tag);
                        Debug.WriteLine(tag.ToString());
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

                string urlaub = mitarbeiter.Urlaub.TotalHours.ToString() + ":00";

                m_lblUrlaub.Text = urlaub;
                m_lblÜberstunden.Text = mitarbeiter.Überstunden.ToString();

                m_lblSoll.Text = ClsBerechnung.GetSollArbeitszeit(SelectedDay, mitarbeiter).ToString(@"hh\:mm");

                ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(SelectedDay, mitarbeiter.Mitarbeiternummer);
                
                if(tag != null)
                {
                    Debug.WriteLine(tag.ID);
                    Debug.WriteLine(tag.Status);
                    m_lblIst.Text = tag.Arbeitszeit.ToString(@"hh\:mm");
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

        private void m_btnSchule_Click(object sender, EventArgs e)
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

        private void m_btnUrlaub_Click(object sender, EventArgs e)
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

        private void m_btnUnentschuldigt_Click(object sender, EventArgs e)
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

        private void m_btnKrank_Click(object sender, EventArgs e)
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

        private void m_btnPrint_Click(object sender, EventArgs e)
        {
            m_Druckzähler = 0;
            DateTime date = m_cldKalender.SelectionStart;
            ClsMitarbeiter mtbtr = m_lbxMitarbeiter.SelectedItem as ClsMitarbeiter;

            using (StreamWriter sw = new StreamWriter("Export.html", false))
            {
                ClsPrintTemplate doc = new ClsPrintTemplate();
                TimeSpan Monat = new TimeSpan();
                TimeSpan GesSoll = new TimeSpan();
                TimeSpan GesIst = new TimeSpan();

                
                for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
                {
                    DateTime date1 = new DateTime(date.Year, date.Month, i);
                    List<ClsBuchung> buchungen = DataProvider.SelectAllBuchungenFromDay(mtbtr, date1, "buchungen_temp");
                    TimeSpan SollZeit = ClsBerechnung.GetSollArbeitszeit(date1, mtbtr);
                    ClsAusgewerteter_Tag tag = DataProvider.SelectAusgewerteterTag(date1, mtbtr.Mitarbeiternummer);
                    TimeSpan IstZeit = new TimeSpan();
                    if(tag != null)
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
                    TimeSpan Überstunden = new TimeSpan(IstZeit.Ticks-SollZeit.Ticks);
                    if(tag.Status == 0)
                    {
                        Monat = Monat.Add(Überstunden);
                    }
                    string Monatstr = Math.Round(Math.Abs(Monat.TotalHours) - 0.5, 0,MidpointRounding.AwayFromZero).ToString() + ":" + Math.Abs(Monat.Minutes).ToString("D2");
                    if (Monat.TotalHours < 0)
                        Monatstr = "-" + Monatstr;
                    string Überstundenstr = Überstunden.ToString(@"hh\:mm");
                    if (Überstunden.TotalHours < 0)
                        Überstundenstr = "-" + Überstundenstr;
                    string SollZeitstr = SollZeit.ToString(@"hh\:mm");
                    string IstZeitstr = IstZeit.ToString(@"hh\:mm");
                    string Tag = date1.DayOfWeek.ToString() + ", " + date1.Day;

                    GesSoll = GesSoll.Add(SollZeit);
                    GesIst = GesIst.Add(IstZeit);

                    doc.AddLine(Tag,buchungen, SollZeitstr, IstZeitstr, Status, Überstundenstr, Monatstr);
                }

                string GesSollstr = Math.Round(Math.Abs(GesSoll.TotalHours) - 0.5, 0, MidpointRounding.AwayFromZero).ToString() + ":" + Math.Abs(GesSoll.Minutes).ToString("D2");

                string GesIststr = Math.Round(Math.Abs(GesIst.TotalHours) - 0.5, 0, MidpointRounding.AwayFromZero).ToString() + ":" + Math.Abs(GesIst.Minutes).ToString("D2");

                string Monatsüberstunden = Math.Round(Math.Abs(Monat.TotalHours) - 0.5, 0, MidpointRounding.AwayFromZero).ToString() + ":" + Math.Abs(Monat.Minutes).ToString("D2");
                if (Monat.TotalHours < 0)
                    Monatsüberstunden = "-" + Monatsüberstunden;
                string GesÜberstunden = Math.Round(Math.Abs(mtbtr.Überstunden.TotalHours) - 0.5, 0, MidpointRounding.AwayFromZero).ToString() + ":" + Math.Abs(mtbtr.Überstunden.Minutes).ToString("D2");
                if (mtbtr.Überstunden.TotalHours < 0)
                    GesÜberstunden = "-" + GesÜberstunden;
                string GesUrlaub = mtbtr.Urlaub.TotalHours.ToString() + ":00";

                sw.WriteLine(doc.GetDoc(GesSollstr,GesIststr,Monatsüberstunden,GesÜberstunden,GesUrlaub));
            }

            WebBrowser PrintBrowser = new WebBrowser();
            string Filepath = Directory.GetCurrentDirectory().ToString() + @"\Export.html";
            PrintBrowser.Url = new Uri(Filepath);
            PrintBrowser.DocumentCompleted += PrintBrower_DocumentCompleted;
        }
        int m_Druckzähler = 0;
        private void PrintBrower_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            m_Druckzähler++;
            Debug.WriteLine(m_Druckzähler);
            if (m_Druckzähler == 1)
            {
                ((WebBrowser)sender).ShowPrintDialog();
                //((WebBrowser)sender).Print();
            }
            else
            {
                ((WebBrowser)sender).Dispose();
            }
        }

        private void m_btnRefresh_Click(object sender, EventArgs e)
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
    }
}
