﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using ZstdSharp.Unsafe;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TimeChip_App
{
    public class ClsBerechnung
    {
        /// <summary>
        /// Berechnet die Arbeitszeit und die Überstunden aller Mitarbeiter an allen Tagen seit der letzten Berechnung und aktualisiert diese Daten in der Datenbank.
        /// Zusätzlich werden die Buchungen aus buchungen_temp in die buchungen Tabelle übertragen
        /// </summary>
        public static void Berechnen()
        {
            DataProvider.Log("Normale Berechnung gestartet", 0);

            List <ClsBuchung> buchungen = DataProvider.SelectAllBuchungen("buchungen_temp");

            DateTime lastBerechnung = DataProvider.ReadBerechnungsdate();

            if (lastBerechnung.CompareTo(DateTime.Now.Date) < 0)
            {
                DateTime compare = lastBerechnung.Date;

                // Eine Liste aus Buchungen der einzelnen Tage
                List<List<ClsBuchung>> TagesBuchungen = new List<List<ClsBuchung>>();
                List<DateTime> Tage = new List<DateTime>();

                //Es werden alle Tage gesucht, die berechnet werden müssen
                while (compare.CompareTo(DateTime.Now.Date) < 0)
                {
                    TagesBuchungen.Add(buchungen.FindAll(x => x.Zeit.ToShortDateString().Equals(compare.ToShortDateString())));

                    Tage.Add(compare);
                    compare = compare.AddDays(1);
                }

                ClsMitarbeiter mtbtr = new ClsMitarbeiter();
                List<ClsAusgewerteter_Tag> ausgewerteteTage = new List<ClsAusgewerteter_Tag>();
                List<(TimeSpan, TimeSpan, DateTime, int)> Monatsübersichten = new List<(TimeSpan, TimeSpan, DateTime, int)>();
                foreach (ClsMitarbeiter mitarbeiter in FrmHaupt.Mitarbeiterliste)
                {
                    DataProvider.Log(mitarbeiter.Log() + " wird berechnet",0);
                    mtbtr = mitarbeiter;
                    List<ClsAbzpMtbtr> abzpmtbtrs = DataProvider.SelectAbzpMtbtr(mtbtr);
                    foreach(DateTime tag in Tage)
                    {
                        if(tag.Day == 1)
                        {
                            Monatsübersichten.Add((mtbtr.Überstunden, mtbtr.Urlaub, tag.Date, mtbtr.ID));
                        }

                        GetAbzpofDate(ref mtbtr, tag, abzpmtbtrs);
                        List<ClsBuchung> TagBuchungen = buchungen.FindAll(x => x.Zeit.ToShortDateString().Equals(tag.ToShortDateString()) && x.MtbtrID.Equals(mitarbeiter.ID));
                        ausgewerteteTage.Add(Berechnen(tag, ref mtbtr, true, false,TagBuchungen));
                    }

                    DataProvider.UpdateMitarbeiter(mtbtr);

                    DataProvider.Log("Neue Überstunden: " + mtbtr.Überstunden,0);
                }

                DataProvider.InsertMultipleAusgewerteterTag(ausgewerteteTage);
                DataProvider.SaveBerechnungsdate(DateTime.Now);


                List<ClsBuchung> TransferBuchungen = new List<ClsBuchung>();
                foreach (DateTime date in Tage)
                {
                    TransferBuchungen.AddRange(buchungen.FindAll(x => x.Zeit.ToShortDateString().Equals(date.ToShortDateString()))); 
                }
                if(TransferBuchungen.Count != 0)
                {
                    DataProvider.TransferBuchungs(TransferBuchungen);
                }
                
                if(Monatsübersichten.Count != 0)
                {
                    DataProvider.InsertMultipeMonatsübersichten(Monatsübersichten);
                }
            }
        }

        /// <summary>
        /// Berechnet die Tage von einem Mitarbeiter, die sich in einer festgelegten Zeitspanne befinden
        /// *TODO*
        /// Funktion effizienter schreiben, dass (so wie beim normalen bearbeiten) die ausgewerteten Tage nachher in einem geupdatet werden und nicht alle extra
        /// *TODO*
        /// </summary>
        /// <param name="startdate">Der erste zu berechnende Tag</param>
        /// <param name="enddate">Der letzte zu berechnende Tag</param>
        /// <param name="mtbtr">Der Mitarbeiter, dessen Tage berechnet werden müssen</param>
        /// <param name="alteabziehen">Ob die alten Überstunden des Mtbtrs vor dem Anwenden der neuen Überstunden abgezogen werden sollen</param>
        public static void Berechnen(DateTime startdate, DateTime enddate, ClsMitarbeiter mtbtr, bool alteabziehen)
        {
            DataProvider.Log("Zeitspanne von " + startdate + " bis " + enddate + " von " + mtbtr.Log() + " berechnen", 1);
            List<ClsAbzpMtbtr> abzpMtbtrs = DataProvider.SelectAbzpMtbtr(mtbtr);
            for(DateTime date = startdate; date <= enddate;date = date.Add(new TimeSpan(1, 0, 0, 0)))
            {
                GetAbzpofDate(ref mtbtr, date, abzpMtbtrs);
                Berechnen(date, ref mtbtr, false,alteabziehen);
            }
        }

        /// <summary>
        /// Berechnet die Arbeitszeit und die Überstunden eines Mitarbeiters an einem bestimmten Tag und aktualisiert diese Daten gegebenenfalls in der Datenbank
        /// </summary>
        /// <param name="day">Der zu berechnende Tag</param>
        /// <param name="mtbtr">Der Mitarbeiter dessen Daten berechnet werden sollen</param>
        /// <param name="ersteBerechnung">Gibt an ob der Tag bereits berechnet wurde oder ob dies die erste Berechnung ist</param>
        /// <param name="TagesBuchungen">Eine Liste mit den Buchungen von mtbtr an day</param>
        /// <returns>Die berechneten Daten als Ausgewerteter Tag</returns>
        public static ClsAusgewerteter_Tag Berechnen(DateTime day, ref ClsMitarbeiter mtbtr, bool ersteBerechnung, bool timespanBerechnung = false, List<ClsBuchung> TagesBuchungen = null, ClsAusgewerteter_Tag Tag1 = null)
        {
            List<ClsBuchung> buchungen;
            ClsAusgewerteter_Tag tag = new ClsAusgewerteter_Tag();
            if(TagesBuchungen is null)
            {
                if (!ersteBerechnung)
                {
                    buchungen = DataProvider.SelectAllBuchungenFromDay(mtbtr, day, "buchungen");
                    if(Tag1 != null)
                    {
                        tag = Tag1;

                    }
                    else
                    {
                        tag = DataProvider.SelectAusgewerteterTag(day, mtbtr.ID);
                    }
                }
                else
                {
                    buchungen = DataProvider.SelectAllBuchungenFromDay(mtbtr, day, "buchungen_temp");
                }
            }
            else
            {
                buchungen = TagesBuchungen;
            }


            buchungen.Sort(Comparer<ClsBuchung>.Create((x, y) => x.Zeit.TimeOfDay.CompareTo(y.Zeit.TimeOfDay)));
            bool first = true;
            DateTime temp = new DateTime(0);
            TimeSpan Arbeitszeit = new TimeSpan(0);

            foreach (ClsBuchung buchung in buchungen)
            {
                switch (buchung.Buchungstyp)
                {
                    case Buchungstyp.Kommen:
                        TimeSpan Arbeitsbeginn = GetArbeitsbeginnOfDayOfWeek(buchung.Zeit, mtbtr);
                        if (buchung.Zeit.CompareTo(new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsbeginn.Hours, Arbeitsbeginn.Minutes, Arbeitsbeginn.Seconds)) < 0 && 
                            Arbeitsbeginn.TotalSeconds != 0)
                        {
                            temp = new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsbeginn.Hours, Arbeitsbeginn.Minutes, Arbeitsbeginn.Seconds);
                            break;
                        }
                        temp = buchung.Zeit;

                        break;
                    case Buchungstyp.Gehen:
                        if (first || temp.Ticks == 1) { break; }
                        TimeSpan Arbeitsende = GetArbeitsendeOfDayOfWeek(buchung.Zeit, mtbtr);
                        if (buchung.Zeit.CompareTo(new DateTime(buchung.Zeit.Year, buchung.Zeit.Month, buchung.Zeit.Day, Arbeitsende.Hours, Arbeitsende.Minutes, 0)) > 0 && 
                            Arbeitsende.TotalSeconds != 0)
                        {
                            Arbeitszeit += new TimeSpan(new TimeSpan(Arbeitsende.Hours, Arbeitsende.Minutes, 0).Ticks - temp.TimeOfDay.Ticks);
                        }
                        else
                        {
                            Arbeitszeit += new TimeSpan(buchung.Zeit.Ticks - temp.Ticks);
                        }

                        temp = new DateTime(1);
                        break;
                }

                first = false;
            }

            TimeSpan Überstunden = new TimeSpan(0);

            //Pausenabzug
            DataProvider.Log("Pausenberechnung", 1);
            if(GetPauseOfDayOfWeek(day, mtbtr))
            {
                if(buchungen.Count > 0)
                {

                }
            }

            if (GetPauseOfDayOfWeek(day, mtbtr))
            {
                if (buchungen.Count != 0)
                {
                    if (GetPausenbeginnOfDayOfWeek(day, mtbtr).TotalSeconds == 0 || GetPausenendeOfDayOfWeek(day, mtbtr).TotalSeconds == 0)
                    {
                        Arbeitszeit -= GetSollPausendauer(day, mtbtr);
                    }
                    else
                    {
                        //Erste Buchung liegt vor Pausenbeginn
                        if (buchungen[0].Zeit.TimeOfDay.CompareTo(GetPausenbeginnOfDayOfWeek(day, mtbtr)) < 0)
                        {
                            //Letzte liegt vor Pausenbeginn
                            //Wird nichts gemacht

                            //Letzte Buchung liegt nach Pausenende
                            if (buchungen.LastOrDefault().Zeit.TimeOfDay.CompareTo(GetPausenendeOfDayOfWeek(day, mtbtr)) > 0)
                            {
                                Arbeitszeit -= GetSollPausendauer(day, mtbtr);

                            }
                            else if(buchungen.LastOrDefault().Zeit.TimeOfDay.CompareTo(GetPausenbeginnOfDayOfWeek(day,mtbtr))>0)
                            {     //Letzte liegt nach Pausenbeginn
                                //Wenn nicht: Zeit, die letze Buchung nach Pausenbeginn liegt, als Pausendauer abziehen
                                TimeSpan Pausendauer = buchungen.LastOrDefault().Zeit.TimeOfDay.Subtract(GetPausenbeginnOfDayOfWeek(day, mtbtr));

                                Arbeitszeit -= Pausendauer;
                            }
                        }
                        else //Erste liegt nach Pausenbeginn
                        {
                            //Letzte Buchung liegt nach Pausenende
                            if (buchungen.LastOrDefault().Zeit.TimeOfDay.CompareTo(GetPausenendeOfDayOfWeek(day, mtbtr)) > 0)
                            {
                                //Zeit, die erste vor Pausenende liegt als Pausendauer
                                TimeSpan Pausendauer = GetPausenendeOfDayOfWeek(day, mtbtr).Subtract(buchungen[0].Zeit.TimeOfDay);

                                Arbeitszeit -= Pausendauer;
                            }
                            else
                            {
                                Arbeitszeit = new TimeSpan(0);
                            }
                        }
                    }

                }
            }

            Überstunden = Arbeitszeit - GetSollArbeitszeit(day, mtbtr);

            if(ersteBerechnung == false)
            {
                TimeSpan oldÜberstunden = new TimeSpan(tag.Arbeitszeit.Ticks - GetSollArbeitszeit(day, mtbtr).Ticks);

                if (!timespanBerechnung)
                {
                    mtbtr.Überstunden -= oldÜberstunden;

                }
                if(tag.Status == 0)
                {
                    mtbtr.Überstunden += Überstunden;
                }

                DataProvider.UpdateMitarbeiter(mtbtr);

                DataProvider.UpdateAusgewerteterTag(day, mtbtr.ID, Arbeitszeit, tag.Status);

                DataProvider.Log("Nicht die erste Berechnung von " + mtbtr.Log() + " mit Arbeitszeit von " + Arbeitszeit + " und Überstunden von " + Überstunden, 1);

                return DataProvider.SelectAusgewerteterTag(day, mtbtr.ID);
            }
            mtbtr.Überstunden += Überstunden;

            DataProvider.Log(day.ToString("d") +  " Arbeitszeit: " + Arbeitszeit + " Überstunden: " + Überstunden, 1);

            return new ClsAusgewerteter_Tag(1, mtbtr.ID, Arbeitszeit, day, tag.Status);
        }

        /// <summary>
        /// Berechnet bei etwaiger Statusänderung eines Tages die Änderung in den Überstunden bzw. beim verbleibenden Urlaub eines Mitarbeiters
        /// </summary>
        /// <param name="tag">Der Tag, dessen Status geändert wurde</param>
        /// <param name="alterStatus">Der Wert des Status vor der Änderung; 0 = Zeitausgleich, 1 = Krank, 2 = Schule, 3 = Urlaub</param>
        public static void TagesStatusÄnderung(ClsAusgewerteter_Tag tag, int alterStatus)
        {
            DataProvider.Log("Tagesstatusänderung von " + tag.ID + " von " + alterStatus + " zu " + tag.Status, 2);
            ClsMitarbeiter mitarbeiter = FrmHaupt.Mitarbeiterliste.ToList().Find(x => x.ID.Equals(tag.MtbtrID));
            List<ClsAbzpMtbtr> abzpMtbtrs = DataProvider.SelectAbzpMtbtr(mitarbeiter);
            GetAbzpofDate(ref mitarbeiter, tag.Date, abzpMtbtrs);
            TimeSpan Überstunden = tag.Arbeitszeit - GetSollArbeitszeit(tag.Date, mitarbeiter);
            //Status Codes:
            //0 = Zeitausgleich
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
                            mitarbeiter.Urlaub = mitarbeiter.Urlaub.Subtract(GetSollArbeitszeit(tag.Date, mitarbeiter));
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
                            mitarbeiter.Urlaub = mitarbeiter.Urlaub.Subtract(GetSollArbeitszeit(tag.Date, mitarbeiter));
                            break;
                    }
                    break;
                case 3:
                    {
                        switch (tag.Status)
                        {
                            case 0:
                                //Einen Tag Urlaub hinzufügen und += Überstunden
                                mitarbeiter.Urlaub = mitarbeiter.Urlaub.Add(GetSollArbeitszeit(tag.Date, mitarbeiter));
                                mitarbeiter.Überstunden += Überstunden;
                                break;
                            case 1:
                            case 2:
                                //Einen Tag Urlaub hinzufügen
                                mitarbeiter.Urlaub = mitarbeiter.Urlaub.Add(GetSollArbeitszeit(tag.Date, mitarbeiter));
                                break;
                            case 3:
                                break;
                        }
                    }
                    break;
            }
            DataProvider.UpdateMitarbeiter(mitarbeiter);
            DataProvider.UpdateAusgewerteterTag(tag);

            DataProvider.Log("Nach Tagesstatusänderung Überstunden: " + mitarbeiter.Überstunden + " Urlaub: " + mitarbeiter.Urlaub, 2); 
        }



        /// <summary>
        /// Berechnet bei Bedarf den jährlichen Urlaub eines Mitarbeiters
        /// Wird nicht verwendet, da die Urlaubsberechnung nicht allgemeingültig ist und daher für jeden Mitarbeiter einzeln von Hand vollzogen wird
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

        /// <summary>
        /// Sucht das Arbeitszeitprofil, das ein Mitarbeiter zu einem bestimmten Datum gehabt hat
        /// </summary>
        /// <param name="mtbtr">Der Mitarbeiter dessen Abzp gesucht und geändert wird</param>
        /// <param name="date"></param>
        /// <param name="abzpMtbtrs">Eine Liste aller abzpMtbtrs Objekte eines bestimmten Mitarbeiters</param>
        public static void GetAbzpofDate(ref ClsMitarbeiter mtbtr, DateTime date, List<ClsAbzpMtbtr> abzpMtbtrs)
        {
            DataProvider.Log("GetAbzpofDate von " + mtbtr.Vorname + " " + mtbtr.Nachname + " an " + date, 2);
            List<ClsAbzpMtbtr> startvor = abzpMtbtrs.FindAll(x=>x.Startdate.CompareTo(date)<0);
            List<ClsAbzpMtbtr> endnach = startvor.FindAll(x=>x.Enddate.CompareTo(date)>0);

            if(endnach.Count != 0)
            {
                mtbtr.Arbeitszeitprofil = DlgArbeitszeitprofile.ArbeitsprofilListe.Find(x => x.ID.Equals(endnach[0].AbzpID));
            }
        }

        /// <summary>
        /// Ruft die Arbeitszeit ab, die mitarbeiter an date laut Arbeitszeitprofil zu arbeiten hat
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        public static TimeSpan GetSollArbeitszeit(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            DataProvider.Log("GetSollArbeitszeit von " + mitarbeiter.Log() + " an " +date, 2);
            FeiertagLogic feiertage = FeiertagLogic.GetInstance(date.Year);

            if (feiertage.isFeiertag(date))
            {
                return new TimeSpan();
            }
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

        /// <summary>
        /// Ruft die Dauer der Pause ab, die mitarbeiter an date zu verrichten hat
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        private static TimeSpan GetSollPausendauer(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            DataProvider.Log("GetSollPausendauer von " + mitarbeiter.Log() + " an " + date, 2);
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

        /// <summary>
        /// Ruft die Uhrzeit ab, zu der die Pause von mitarbeiter an date beginnt
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        private static TimeSpan GetPausenbeginnOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            DataProvider.Log("GetPausenbeginn von " + mitarbeiter.Log() + " an " + date, 2);
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
        }/// <summary>
        /// Ruft die Uhrzeit ab, zu der die Pause von mitarbeiter an date endet
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        private static TimeSpan GetPausenendeOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            DataProvider.Log("GetPausenende von " + mitarbeiter.Log() + " an " + date, 2);
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

        /// <summary>
        /// Ruft die Uhrzeit ab an der die Arbeitszeit von mitarbeiter an date beginnt
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        private static TimeSpan GetArbeitsbeginnOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            DataProvider.Log("GetArbeitsbeginn von " + mitarbeiter.Log() + " an " + date, 2 );
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

        /// <summary>
        /// Ruft die Uhrzeit ab, zu der die Arbeitszeit von mitarbeiter an date endet
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        private static TimeSpan GetArbeitsendeOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            DataProvider.Log("GetArbeitsende von " + mitarbeiter.Log() + " an " + date, 2);
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

        /// <summary>
        /// Ruft ab ob mitarbeiter an date eine Pause hat
        /// </summary>
        /// <param name="date"></param>
        /// <param name="mitarbeiter"></param>
        /// <returns></returns>
        private static bool GetPauseOfDayOfWeek(DateTime date, ClsMitarbeiter mitarbeiter)
        {
            DataProvider.Log("GetPause von " + mitarbeiter.Log() + " an " + date, 2);
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
