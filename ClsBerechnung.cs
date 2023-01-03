﻿using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Tls;
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
            //TEMP
            DataProvider.WriteBerechnungsDateToCSV(new DateTime(2022, 12, 19, 00, 00, 01));

            DateTime lastBerechnung = DataProvider.ReadBerechnungsDateFromCSV();

            if (lastBerechnung.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)) < 0)
            {
                Debug.WriteLine("BERECHNUNG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");

                DateTime compare = new DateTime(lastBerechnung.Year, lastBerechnung.Month, lastBerechnung.Day, 0, 0, 0);

                // Eine Liste aus Buchungen der einzelnen Tage
                List<List<ClsBuchung>> TagesBuchungen = new List<List<ClsBuchung>>();
                List<DateTime> Tage = new List<DateTime>();

                //Es werden alle Tage gesucht, die berechnet werden müssen
                while (compare.CompareTo(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0)) < 0)
                {
                    Debug.WriteLine(compare.ToString());
                    TagesBuchungen.Add(buchungen1.FindAll(x => x.Zeit.ToShortDateString().Equals(compare.ToShortDateString())));

                    Tage.Add(compare);
                    compare = compare.AddDays(1);
                }

                foreach(DateTime tag in Tage)
                {
                    foreach (ClsMitarbeiter mitarbeiter in FrmHaupt.Mitarbeiterliste)
                    {
                        ClsMitarbeiter mtbtr = mitarbeiter;
                        Berechnen(tag, ref mtbtr, true);
                    }
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

        public static ClsAusgewerteter_Tag Berechnen(DateTime day, ref ClsMitarbeiter mtbtr, bool ersteBerechnung)
        {
            List<ClsBuchung> buchungen;
            if (!ersteBerechnung)
            {
                buchungen = DataProvider.SelectAllBuchungenFromDay(mtbtr, day,"buchungen");
            }
            else
            {
                buchungen = DataProvider.SelectAllBuchungenFromDay(mtbtr, day, "buchungen_temp");
            }

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
                        if (buchung.Zeit.CompareTo(new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsbeginn.Hours, Arbeitsbeginn.Minutes, 0)) < 0 && Arbeitsbeginn.Hours != 0)
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
                        if (buchung.Zeit.CompareTo(new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsende.Hours, Arbeitsende.Minutes, 0)) > 0 && Arbeitsende.Hours != 0)
                        {
                            Arbeitszeit += new TimeSpan(new TimeSpan(Arbeitsende.Hours, Arbeitsende.Minutes, 0).Ticks - temp.TimeOfDay.Ticks);
                        }
                        else
                        {
                            Arbeitszeit += new TimeSpan(buchung.Zeit.Ticks - temp.Ticks);
                        }

                        Debug.WriteLine("Erste Zeit: " + temp);
                        Debug.WriteLine("Zweite Zeit: " + buchung.Zeit);
                        Debug.WriteLine("Durchrechnen Arbeitszeit: " + Arbeitszeit);
                        temp = new DateTime(1);
                        break;
                }

                first = false;
            }

            Debug.WriteLine("Arbeitszeit vor Pause Abzug: " + Arbeitszeit.ToString());
            TimeSpan Überstunden = new TimeSpan(0);

            //Pausenabzug
            bool pauseberechnet = false;
            if (GetPauseOfDayOfWeek(day, mtbtr))
            {
                if (buchungen.Count != 0)
                {
                    //Erste Buchung liegt vor Pausenbeginn
                    if (buchungen[0].Zeit.TimeOfDay.CompareTo(GetPausenbeginnOfDayOfWeek(day, mtbtr)) < 0)
                    {
                        //Letzte Buchung liegt nach Pausenende
                        if (buchungen.LastOrDefault().Zeit.TimeOfDay.CompareTo(GetPausenendeOfDayOfWeek(day, mtbtr)) > 0)
                        {
                            Arbeitszeit -= GetSollPausendauer(day, mtbtr);
                            Überstunden = Arbeitszeit - GetSollArbeitszeit(day, mtbtr);

                            pauseberechnet = true;
                        }
                    }
                }
                else
                {
                    pauseberechnet = true;
                }
            }
            else
            {
                pauseberechnet = true;
            }

            if (!pauseberechnet)
            {
                //Wenn nicht: Zeit, die letze Buchung nach Pausenbeginn liegt, als Pausendauer abziehen
                TimeSpan Pausendauer = buchungen.LastOrDefault().Zeit.TimeOfDay.Subtract(GetPausenbeginnOfDayOfWeek(day, mtbtr));
                Debug.WriteLine("Pause bei schleißige mtbtr");

                Arbeitszeit -= Pausendauer;
                Überstunden = Arbeitszeit - GetSollArbeitszeit(day, mtbtr);
            }

            Debug.WriteLine("Arbeitszeit nach Pause Abzug: " + Arbeitszeit.ToString());
            Debug.WriteLine("Überstunden: " + Überstunden.ToString());

            mtbtr.Überstunden += Überstunden;
            DataProvider.UpdateMitarbeiter(mtbtr);

            return DataProvider.InsertAusgewerteterTag(day, mtbtr.Mitarbeiternummer, Arbeitszeit, 0);
        }

        public static void TagesStatusÄnderung(ClsAusgewerteter_Tag tag, int alterStatus)
        {
            ClsMitarbeiter mitarbeiter = FrmHaupt.Mitarbeiterliste.ToList().Find(x => x.Mitarbeiternummer.Equals(tag.MitarbeiterNummer));
            TimeSpan Überstunden = tag.Arbeitszeit - GetSollArbeitszeit(tag.Date, mitarbeiter);
            //Status Codes:
            //0 = Überstunden
            //1 = Krank
            //2 = Schule
            //3 = Urlaub

            //In case 1: Steht nichts, da es immer das Gleiche macht, wie case 2: Deshalb steht dort nicht mal ein break
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
                            Debug.WriteLine(mitarbeiter.Urlaub);
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
                                //Einen Tag Urlaub hinzufügen und += Überstunden
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



        /// <summary>
        /// Berechnet bei Bedarf den jährlichen Urlaub eines Mitarbeiters
        /// Wird nicht verwendet, da die Urlaubsberechnung nicht allgemeingültig ist und daher von Hand vollzogen wird
        /// </summary>
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
                    //mitarbeiter.Urlaub += new TimeSpan(mitarbeiter.Arbeitszeitprofil.Urlaub * 8, 0, 0);
                    dates.Add(new DateTime(DateTime.Now.Year + 1, mitarbeiter.Arbeitsbeginn.Month, mitarbeiter.Arbeitsbeginn.Day));
                    MitarbeiterIDs.Add(mitarbeiter.ID);
                }
                else
                {
                    DateTime CompareDate = dates[IDIndex];

                    if (CompareDate.CompareTo(DateTime.Now) < 0)
                    {
                        //mitarbeiter.Urlaub += new TimeSpan(mitarbeiter.Arbeitszeitprofil.Urlaub * 8, 0, 0);
                        int year = DateTime.Now.Year + 1;
                        dates[IDIndex] = new DateTime(year, dates[IDIndex].Month, dates[IDIndex].Day);
                    }

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
                case DayOfWeek.Sunday:
                    return mitarbeiter.Arbeitszeitprofil.Sonntag.Arbeitszeit;
                default:
                    return new TimeSpan(0);
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
                case DayOfWeek.Sunday:
                    return mitarbeiter.Arbeitszeitprofil.Sonntag.Pausendauer;
                default:
                    return new TimeSpan(0);
            }
        }
        private static TimeSpan GetPausenbeginnOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return mitarbeiter.Arbeitszeitprofil.Montag.Pausenbeginn;
                case DayOfWeek.Tuesday:
                    return mitarbeiter.Arbeitszeitprofil.Dienstag.Pausenbeginn;
                case DayOfWeek.Wednesday:
                    return mitarbeiter.Arbeitszeitprofil.Mittwoch.Pausenbeginn;
                case DayOfWeek.Thursday:
                    return mitarbeiter.Arbeitszeitprofil.Donnerstag.Pausenbeginn;
                case DayOfWeek.Friday:
                    return mitarbeiter.Arbeitszeitprofil.Freitag.Pausenbeginn;
                case DayOfWeek.Saturday:
                    return mitarbeiter.Arbeitszeitprofil.Samstag.Pausenbeginn;
                case DayOfWeek.Sunday:
                    return mitarbeiter.Arbeitszeitprofil.Sonntag.Pausenbeginn;
                default:
                    return new TimeSpan(0);
            }
        }
        private static TimeSpan GetPausenendeOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return mitarbeiter.Arbeitszeitprofil.Montag.Pausenende;
                case DayOfWeek.Tuesday:
                    return mitarbeiter.Arbeitszeitprofil.Dienstag.Pausenende;
                case DayOfWeek.Wednesday:
                    return mitarbeiter.Arbeitszeitprofil.Mittwoch.Pausenende;
                case DayOfWeek.Thursday:
                    return mitarbeiter.Arbeitszeitprofil.Donnerstag.Pausenende;
                case DayOfWeek.Friday:
                    return mitarbeiter.Arbeitszeitprofil.Freitag.Pausenende;
                case DayOfWeek.Saturday:
                    return mitarbeiter.Arbeitszeitprofil.Samstag.Pausenende;
                case DayOfWeek.Sunday:
                    return mitarbeiter.Arbeitszeitprofil.Sonntag.Pausenende;
                default:
                    return new TimeSpan(0);
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
                default:
                    return new TimeSpan(0);
            }
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
                default:
                    return new TimeSpan(0);
            }
        }
        private static bool GetPauseOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return mitarbeiter.Arbeitszeitprofil.Montag.Pause;
                case DayOfWeek.Tuesday:
                    return mitarbeiter.Arbeitszeitprofil.Dienstag.Pause;
                case DayOfWeek.Wednesday:
                    return mitarbeiter.Arbeitszeitprofil.Mittwoch.Pause;
                case DayOfWeek.Thursday:
                    return mitarbeiter.Arbeitszeitprofil.Donnerstag.Pause;
                case DayOfWeek.Friday:
                    return mitarbeiter.Arbeitszeitprofil.Freitag.Pause;
                case DayOfWeek.Saturday:
                    return mitarbeiter.Arbeitszeitprofil.Samstag.Pause;
                case DayOfWeek.Sunday:
                    return mitarbeiter.Arbeitszeitprofil.Sonntag.Pause;
                default:
                    return false;
            }
        }
    }
}