using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TimeChip_App_1._0
{
    public class ClsBerechnung
    {

        public static void Berechnen()
        {
            List<ClsBuchung> buchungen1 = DataProvider.SelectAllBuchungen("buchungen_temp");
            DataProvider.WriteBerechnungsDateToCSV(new DateTime(2022, 09, 03, 05, 22, 3));
            DateTime lastBerechnung = DataProvider.ReadBerechnungsDateFromCSV();
            if (lastBerechnung.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)) < 0)
            {
                Debug.WriteLine("BERECHNUNG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                DateTime compare = new DateTime(lastBerechnung.Year, lastBerechnung.Month, lastBerechnung.Day, 0, 0, 0);

                // Eine Liste aus Buchungen der einzelnen Tage
                List<List<ClsBuchung>> TagesBuchungen = new List<List<ClsBuchung>>();
                List<DateTime> Tage = new List<DateTime>();

                while (compare.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)) < 0)
                {
                    Debug.WriteLine(compare.ToString());
                    TagesBuchungen.Add(buchungen1.FindAll(x => x.Zeit.ToShortDateString().Equals(compare.ToShortDateString())));

                    Tage.Add(compare);
                    compare = compare.AddDays(1);
                }

                int i = 0;
                foreach(List<ClsBuchung> tag in TagesBuchungen)
                {
                    Debug.WriteLine("NÄCHSTER TAG !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                    foreach(ClsMitarbeiter mitarbeiter in FrmHaupt.Mitarbeiterliste)
                    {
                        List<ClsBuchung> buchungen3 = tag.FindAll(x => x.Mitarbeiternummer.Equals(mitarbeiter.Mitarbeiternummer));
                        buchungen3.Sort(Comparer<ClsBuchung>.Create((x, y) => x.Buchungsnummer.CompareTo(y.Buchungsnummer)));

                        bool first = true;
                        DateTime temp = new DateTime(0);
                        TimeSpan Arbeitszeit = new TimeSpan(0);
                        foreach (ClsBuchung buchung in buchungen3)
                        {
                            Debug.WriteLine(buchung.Buchungsnummer);

                            switch (buchung.Buchungstyp)
                            {
                                case Buchungstyp.Kommen:
                                    TimeSpan Arbeitsbeginn = GetArbeitsbeginnOfDayOfWeek(buchung.Zeit, mitarbeiter);
                                    if (buchung.Zeit.CompareTo(new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsbeginn.Hours, Arbeitsbeginn.Minutes, 0)) < 0 && first && mitarbeiter.Arbeitszeitprofil.Gleitzeit == false)
                                    {
                                        temp = new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsbeginn.Hours, Arbeitsbeginn.Minutes, 0);
                                        Debug.WriteLine("HI");
                                        break;
                                    }
                                    Debug.WriteLine("HI2");
                                    temp = buchung.Zeit;
                                    break;
                                case Buchungstyp.Gehen:
                                    if (first || temp.Ticks == 1) { break; }
                                    TimeSpan Arbeitsende = GetArbeitsendeOfDayOfWeek(buchung.Zeit, mitarbeiter);
                                    if(buchung.Zeit.CompareTo(new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsende.Hours, Arbeitsende.Minutes, 0)) > 0 && mitarbeiter.Arbeitszeitprofil.Gleitzeit == false)
                                    {
                                        Arbeitszeit += new TimeSpan(new TimeSpan(Arbeitsende.Hours, Arbeitsende.Minutes, 0).Ticks - temp.Ticks);
                                        temp = new DateTime(1);
                                        break;
                                    }
                                    Arbeitszeit += new TimeSpan(buchung.Zeit.Ticks - temp.Ticks);
                                    Debug.WriteLine("Erste Zeit: " + temp);
                                    Debug.WriteLine("Zweite Zeit: " + buchung.Zeit);
                                    Debug.WriteLine("Durchrechnen Arbeitszeit: " + Arbeitszeit);
                                    temp = new DateTime(1);
                                    break;
                            }
                            first = false;
                        }

                        Debug.WriteLine("Arbeitszeit vor Pause Abzug: " + Arbeitszeit.ToString());
                        TimeSpan Überstunden = mitarbeiter.Überstunden;

                        switch (Tage[i].DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                Arbeitszeit -= mitarbeiter.Arbeitszeitprofil.Montag.Pausendauer;
                                Überstunden = Arbeitszeit - mitarbeiter.Arbeitszeitprofil.Montag.Arbeitszeit;
                                break;
                            case DayOfWeek.Tuesday:
                                Arbeitszeit -= mitarbeiter.Arbeitszeitprofil.Dienstag.Pausendauer;
                                Überstunden = Arbeitszeit - mitarbeiter.Arbeitszeitprofil.Dienstag.Arbeitszeit;
                                break;
                            case DayOfWeek.Wednesday:
                                Arbeitszeit -= mitarbeiter.Arbeitszeitprofil.Mittwoch.Pausendauer;
                                Überstunden = Arbeitszeit - mitarbeiter.Arbeitszeitprofil.Mittwoch.Arbeitszeit;
                                break;
                            case DayOfWeek.Thursday:
                                Arbeitszeit -= mitarbeiter.Arbeitszeitprofil.Donnerstag.Pausendauer;
                                Überstunden = Arbeitszeit - mitarbeiter.Arbeitszeitprofil.Donnerstag.Arbeitszeit;
                                break;
                            case DayOfWeek.Friday:
                                Arbeitszeit -= mitarbeiter.Arbeitszeitprofil.Freitag.Pausendauer;
                                Überstunden = Arbeitszeit - mitarbeiter.Arbeitszeitprofil.Freitag.Arbeitszeit;
                                break;
                            case DayOfWeek.Saturday:
                                Arbeitszeit -= mitarbeiter.Arbeitszeitprofil.Samstag.Pausendauer;
                                Überstunden = Arbeitszeit - mitarbeiter.Arbeitszeitprofil.Samstag.Arbeitszeit;
                                break;
                            case DayOfWeek.Sunday:
                                Arbeitszeit -= mitarbeiter.Arbeitszeitprofil.Sonntag.Pausendauer;
                                Überstunden = Arbeitszeit - mitarbeiter.Arbeitszeitprofil.Sonntag.Arbeitszeit;
                                break;
                        }

                        Debug.WriteLine("Arbeitszeit nach Pause Abzug: " + Arbeitszeit.ToString());
                        Debug.WriteLine("Überstunden: " + Überstunden.ToString());

                        DataProvider.InsertAusgewerteterTag(Tage[i], mitarbeiter.Mitarbeiternummer, Arbeitszeit, 0);

                        mitarbeiter.Überstunden += Überstunden;
                        DataProvider.UpdateMitarbeiter(mitarbeiter);
                    }

                    i++;
                }

                DataProvider.WriteBerechnungsDateToCSV(DateTime.Now);
                
                /*
                List<ClsBuchung> buchungen = DataProvider.SelectAllBuchungen("buchungen_temp");
                foreach (DateTime date in Tage)
                {
                    List<ClsBuchung> Buchungen = buchungen.FindAll(x => x.Zeit.ToShortDateString().Equals(date.ToShortDateString()));

                    foreach(ClsBuchung buchung in Buchungen)
                    {
                        DataProvider.InsertBuchung(buchung.Buchungstyp, buchung.Zeit, buchung.Mitarbeiternummer);
                        DataProvider.DeleteBuchung(buchung, "buchungen_temp");
                    }
                }
                */
            }
        }

        private static TimeSpan GetArbeitsbeginnOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return mitarbeiter.Arbeitszeitprofil.Montag.Arbeitsbeginn;
                case DayOfWeek.Tuesday:
                    return mitarbeiter.Arbeitszeitprofil.Dienstag.Arbeitsbeginn;
                case DayOfWeek.Wednesday:
                    return mitarbeiter.Arbeitszeitprofil.Mittwoch.Arbeitsbeginn;
                case DayOfWeek.Thursday:
                    return mitarbeiter.Arbeitszeitprofil.Donnerstag.Arbeitsbeginn;
                case DayOfWeek.Friday:
                    return mitarbeiter.Arbeitszeitprofil.Freitag.Arbeitsbeginn;
                case DayOfWeek.Saturday:
                    return mitarbeiter.Arbeitszeitprofil.Samstag.Arbeitsbeginn;
                case DayOfWeek.Sunday:
                    return mitarbeiter.Arbeitszeitprofil.Sonntag.Arbeitsbeginn;
            }

            return new TimeSpan(0);
        }
        private static TimeSpan GetArbeitsendeOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return mitarbeiter.Arbeitszeitprofil.Montag.Arbeitsende;
                case DayOfWeek.Tuesday:
                    return mitarbeiter.Arbeitszeitprofil.Dienstag.Arbeitsende;
                case DayOfWeek.Wednesday:
                    return mitarbeiter.Arbeitszeitprofil.Mittwoch.Arbeitsende;
                case DayOfWeek.Thursday:
                    return mitarbeiter.Arbeitszeitprofil.Donnerstag.Arbeitsende;
                case DayOfWeek.Friday:
                    return mitarbeiter.Arbeitszeitprofil.Freitag.Arbeitsende;
                case DayOfWeek.Saturday:
                    return mitarbeiter.Arbeitszeitprofil.Samstag.Arbeitsende;
                case DayOfWeek.Sunday:
                    return mitarbeiter.Arbeitszeitprofil.Sonntag.Arbeitsende;
            }

            return new TimeSpan(0);
        }

        public static void Berechnen(DateTime day, ClsMitarbeiter mtbtr)
        {
            List<ClsBuchung> buchungen = DataProvider.SelectAllBuchungenFromDay(mtbtr.Mitarbeiternummer, day);

            buchungen.Sort(Comparer<ClsBuchung>.Create((x, y) => x.Buchungsnummer.CompareTo(y.Buchungsnummer)));
            bool first = true;
            DateTime temp = new DateTime(0);
            TimeSpan Arbeitszeit = new TimeSpan(0);

            foreach (ClsBuchung buchung in buchungen)
            {
                Debug.WriteLine(buchung.Buchungsnummer);

                switch (buchung.Buchungstyp)
                {
                    case Buchungstyp.Kommen:
                        TimeSpan Arbeitsbeginn = GetArbeitsbeginnOfDayOfWeek(buchung.Zeit, mtbtr);
                        if (buchung.Zeit.CompareTo(new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsbeginn.Hours, Arbeitsbeginn.Minutes, 0)) < 0 && first && mtbtr.Arbeitszeitprofil.Gleitzeit == false)
                        {
                            temp = new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsbeginn.Hours, Arbeitsbeginn.Minutes, 0);
                            Debug.WriteLine("HI");
                            break;
                        }
                        Debug.WriteLine("HI2");
                        temp = buchung.Zeit;
                        break;
                    case Buchungstyp.Gehen:
                        if (first || temp.Ticks == 1) { break; }
                        TimeSpan Arbeitsende = GetArbeitsendeOfDayOfWeek(buchung.Zeit, mtbtr);
                        if (buchung.Zeit.CompareTo(new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsende.Hours, Arbeitsende.Minutes, 0)) > 0 && mtbtr.Arbeitszeitprofil.Gleitzeit == false)
                        {
                            Arbeitszeit += new TimeSpan(new TimeSpan(Arbeitsende.Hours, Arbeitsende.Minutes, 0).Ticks - temp.Ticks);
                            temp = new DateTime(1);
                            break;
                        }
                        Arbeitszeit += new TimeSpan(buchung.Zeit.Ticks - temp.Ticks);
                        Debug.WriteLine("Erste Zeit: " + temp);
                        Debug.WriteLine("Zweite Zeit: " + buchung.Zeit);
                        Debug.WriteLine("Durchrechnen Arbeitszeit: " + Arbeitszeit);
                        temp = new DateTime(1);
                        break;
                }
                first = false;
            }

            Debug.WriteLine("Arbeitszeit vor Pause Abzug: " + Arbeitszeit.ToString());
            TimeSpan Überstunden = mtbtr.Überstunden;

            Arbeitszeit -= GetSollPausendauer(day, mtbtr);
            Überstunden = Arbeitszeit - GetSollArbeitszeit(day, mtbtr);

            Debug.WriteLine("Arbeitszeit nach Pause Abzug: " + Arbeitszeit.ToString());
            Debug.WriteLine("Überstunden: " + Überstunden.ToString());

            DataProvider.InsertAusgewerteterTag(day, mtbtr.Mitarbeiternummer, Arbeitszeit, 0);

            mtbtr.Überstunden += Überstunden;
            DataProvider.UpdateMitarbeiter(mtbtr);
        }

        public static void TagesStatusÄnderung(ClsAusgewerteter_Tag tag, int alterStatus)
        {
            ClsMitarbeiter mitarbeiter = FrmHaupt.Mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(tag.MitarbeiterNummer));
            TimeSpan Überstunden = tag.Arbeitszeit - GetSollArbeitszeit(tag.Date, mitarbeiter);
            switch (alterStatus)
            {
                case 0:
                    mitarbeiter.Überstunden -= Überstunden;
                    switch (tag.Status)
                    {
                        case 0:
                            break;
                        case 1:
                        case 2:
                            //Überstunden wieder hinzufügen (-=: -2h Überstunden wieder hinzufügen --> Überstunden -= -2h)
                            break;
                        case 3:
                            //Überstunden wieder hinzufügen und Urlaub einen Tag abziehen
                            mitarbeiter.Urlaub = mitarbeiter.Urlaub.Subtract(new TimeSpan(8, 0, 0));
                            break;
                    }
                    break;
                case 1:
                case 2:
                    switch (tag.Status)
                    {
                        case 0:
                            //Überstunden abziehen (+=: -2h Überstunden abziehen --> Überstunden += -2h)
                            mitarbeiter.Überstunden += Überstunden;
                            break;
                        case 1:
                        case 2:
                            break;
                        case 3:
                            //Urlaub einen Tag abziehen
                            mitarbeiter.Urlaub = mitarbeiter.Urlaub.Subtract(new TimeSpan(8,0,0));
                            break;
                    }
                    break;
                case 3:
                    {
                        switch (tag.Status)
                        {
                            case 0:
                                //Einen Tag Urlaub hinzufügen und += Überstunden (siehe case 1/0)
                                mitarbeiter.Urlaub = mitarbeiter.Urlaub.Add(new TimeSpan(8,0,0));
                                mitarbeiter.Überstunden += Überstunden;
                                break;
                            case 1:
                            case 2:
                                //Einen Tag Urlaub hinzufügen
                                mitarbeiter.Urlaub = mitarbeiter.Urlaub.Add(new TimeSpan(8,0,0));
                                break;
                            case 3:
                                break;
                        }
                    }
                    break;
            }
            DataProvider.UpdateMitarbeiter(mitarbeiter);
            DataProvider.UpdateAusgewerteterTag(tag);
        }

        public static TimeSpan GetSollArbeitszeit(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return mitarbeiter.Arbeitszeitprofil.Montag.Arbeitszeit;
                case DayOfWeek.Tuesday:
                    return mitarbeiter.Arbeitszeitprofil.Dienstag.Arbeitszeit;
                case DayOfWeek.Wednesday:
                    return mitarbeiter.Arbeitszeitprofil.Mittwoch.Arbeitszeit;
                case DayOfWeek.Thursday:
                    return mitarbeiter.Arbeitszeitprofil.Donnerstag.Arbeitszeit;
                case DayOfWeek.Friday:
                    return mitarbeiter.Arbeitszeitprofil.Freitag.Arbeitszeit;
                case DayOfWeek.Saturday:
                    return mitarbeiter.Arbeitszeitprofil.Samstag.Arbeitszeit;
                default:
                    return mitarbeiter.Arbeitszeitprofil.Sonntag.Arbeitszeit;
            }
        }
        private static TimeSpan GetSollPausendauer(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return mitarbeiter.Arbeitszeitprofil.Montag.Pausendauer;
                case DayOfWeek.Tuesday:
                    return mitarbeiter.Arbeitszeitprofil.Dienstag.Pausendauer;
                case DayOfWeek.Wednesday:
                    return mitarbeiter.Arbeitszeitprofil.Mittwoch.Pausendauer;
                case DayOfWeek.Thursday:
                    return mitarbeiter.Arbeitszeitprofil.Donnerstag.Pausendauer;
                case DayOfWeek.Friday:
                    return mitarbeiter.Arbeitszeitprofil.Freitag.Pausendauer;
                case DayOfWeek.Saturday:
                    return mitarbeiter.Arbeitszeitprofil.Samstag.Pausendauer;
                default:
                    return mitarbeiter.Arbeitszeitprofil.Sonntag.Pausendauer;
            }
        }

        public static void Urlaubsberechnung()
        {
            List<ClsMitarbeiter> mitarbeiters = DataProvider.SelectAllMitarbeiter();
            List<int> MitarbeiterIDs = new List<int>();
            List<DateTime> dates = DataProvider.ReadDateFromCSV(ref MitarbeiterIDs);


            foreach (ClsMitarbeiter mitarbeiter in mitarbeiters)
            {
                int IDIndex = MitarbeiterIDs.FindIndex(x => x.Equals(mitarbeiter.ID));

                if(IDIndex == -1)
                {
                    mitarbeiter.Urlaub += new TimeSpan(mitarbeiter.Arbeitszeitprofil.Urlaub * 8, 0, 0);
                    dates.Add(new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day));
                    MitarbeiterIDs.Add(mitarbeiter.ID);
                }
                DateTime CompareDate = dates[IDIndex];
                
                if(CompareDate.CompareTo(DateTime.Now) < 0)
                {
                    mitarbeiter.Urlaub += new TimeSpan(mitarbeiter.Arbeitszeitprofil.Urlaub * 8, 0, 0);
                    int year = DateTime.Now.Year + 1;
                    dates[IDIndex] = new DateTime(year, dates[IDIndex].Month, dates[IDIndex].Day);
                }
            }

            List<int> Removeindexes = new List<int>();
            foreach(int ID in MitarbeiterIDs)
            {
                int index = mitarbeiters.FindIndex(x => x.ID.Equals(ID));

                if(index == -1)
                {
                    Removeindexes.Add(MitarbeiterIDs.FindIndex(x => x.Equals(ID)));
                }
            }
            foreach(int index in Removeindexes)
            {
                MitarbeiterIDs.RemoveAt(index);
                dates.RemoveAt(index);
            }

            List<string> strings = new List<string>();

            for(int i = 0; i < MitarbeiterIDs.Count; i++)
            {
                strings.Add(MitarbeiterIDs[i].ToString() + ";" + dates[i].ToString("dd.MM.yyyy"));
            }

            DataProvider.WriteDateToCSV(strings);
        }
    }
}
