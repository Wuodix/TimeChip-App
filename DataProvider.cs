using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using TimeChip_App.Properties;

namespace TimeChip_App
{
    public class DataProvider
    {
        private static string m_connectionString = null;

        public static string ConnectionString { get { return m_connectionString; } set { m_connectionString = value; } }

        /// <summary>
        /// Fügt eine Buchung in die Datenbank ein
        /// </summary>
        /// <param name="buchungstyp"></param>
        /// <param name="zeit"></param>
        /// <param name="MtbtrID"></param>
        /// <returns>Die eingefügte Buchung als ClsBuchungs-Objekt</returns>
        public static ClsBuchung InsertBuchung(Buchungstyp buchungstyp, DateTime zeit, int MtbtrID)
        {
            string query = "INSERT INTO `buchungen` (`Buchungstyp`, `Zeit`, `MtbtrID`) VALUES(@buchungst, @zeit, @mtbtrid)";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("buchungst", buchungstyp.ToString());
            cmd.Parameters.AddWithValue("zeit", zeit);
            cmd.Parameters.AddWithValue("mtbtrid", MtbtrID);

            ExecuteNonQuery(cmd);

            int id = SelectLastBuchung().Buchungsnummer;

            Log("Buchung mit ID " + id + " wurde inserted",4);

            return new ClsBuchung(id, MtbtrID, zeit, buchungstyp);
        }

        /// <summary>
        /// Überträgt eine Liste aus Buchungen aus buchungen_temp in die buchungen Tabelle
        /// </summary>
        /// <param name="buchungen">Die Liste der zu übertragenden Buchungen</param>
        public static void TransferBuchungs(List<ClsBuchung> buchungen)
        {
            string query = "INSERT INTO `buchungen` (`Buchungstyp`, `Zeit`, `MtbtrID`) VALUES ";
            string query1 = "DELETE FROM `buchungen_temp` WHERE Buchungsnummer IN (";

            int i = 0;

            foreach (ClsBuchung buchung in buchungen)
            {
                if(i != 0)
                {
                    query += ", ";
                    query1 += ", ";
                }
                string paraname = "@zeit" + i;
                query += "('" + buchung.Buchungstyp + "', " + paraname + ", '" + buchung.MtbtrID + "')";
                query1 += buchung.Buchungsnummer;

                i++;
            }

            query += ";";
            query1 += ");";

            MySqlCommand cmd = new(query);

            int k = 0;
            foreach(ClsBuchung buchung in buchungen)
            {
                string paraname = "zeit" + k;
                cmd.Parameters.AddWithValue(paraname, buchung.Zeit);
                k++;
            }

            ExecuteNonQuery(cmd);
            ExecuteNonQuery(query1);

            Log(buchungen.Count + " Buchungen wurden von temp in Buchungen übertragen",4);
        }

        /// <summary>
        /// Hilfe um bereits erstellten Tag mit zufälliger ID in die Datenbank einfach einfügen zu können
        /// </summary>
        /// <param name="tag">Der einzufügende Tag</param>
        /// <returns>Der eingefügte Tag mit der richtigen von der Datenbank zugewiesenen ID</returns>
        public static ClsTag InsertTag(ClsTag tag)
        {
            return InsertTag(tag.Arbeitsbeginn, tag.Arbeitsende, tag.Arbeitszeit, tag.Pause, tag.Pausenbeginn, tag.Pausenende, tag.Pausendauer);
        }

        /// <summary>
        /// Fügt einen Tag in die Datenbank ein
        /// </summary>
        /// <param name="name"></param>
        /// <param name="arbeitsbeginn"></param>
        /// <param name="arbeitsende"></param>
        /// <param name="arbeitszeit"></param>
        /// <param name="pausenbeginn"></param>
        /// <param name="pausenende"></param>
        /// <param name="pausendauer"></param>
        /// <param name="pause"></param>
        /// <returns>Der eingefügte Tag als ClsTag-Objekt</returns>
        public static ClsTag InsertTag(TimeSpan arbeitsbeginn, TimeSpan arbeitsende, TimeSpan arbeitszeit, bool pause, TimeSpan pausenbeginn, TimeSpan pausenende, TimeSpan pausendauer)
        {
            string query = "INSERT INTO tage (Arbeitsbeginn, Arbeitsende, Arbeitszeit, Pausenbeginn, Pausenende, Pausendauer, Pause) VALUES (@abbeginn, " +
                "@abende, @abzeit, @pbeginn, @pende, @pdauer, @pause)";


            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("abbeginn", arbeitsbeginn);
            cmd.Parameters.AddWithValue("abende", arbeitsende);
            cmd.Parameters.AddWithValue("abzeit", arbeitszeit);
            cmd.Parameters.AddWithValue("pbeginn", pausenbeginn);
            cmd.Parameters.AddWithValue("pende", pausenende);
            cmd.Parameters.AddWithValue("pdauer", pausendauer);
            cmd.Parameters.AddWithValue("pause", pause);
            
            ExecuteNonQuery(cmd);

            int id = SelectAllTage().Last().ID;

            Log("Tag mit ID " + id + " wurde in die Datenbank eingefügt", 4);

            return new ClsTag(id, arbeitsbeginn, arbeitsende, arbeitszeit, pause, pausenbeginn, pausenende, pausendauer);
        }

        /// <summary>
        /// Fügt ein Arbeitszeitprofil in die Datenbank ein
        /// </summary>
        /// <param name="name"></param>
        /// <param name="montag"></param>
        /// <param name="dienstag"></param>
        /// <param name="mittwoch"></param>
        /// <param name="donnerstag"></param>
        /// <param name="freitag"></param>
        /// <param name="samstag"></param>
        /// <param name="sonntag"></param>
        /// <param name="gleitzeit"></param>
        /// <returns>Das eingefügte Arbeitszeitprofil als ClsArbeitszeitprofil-Objekt</returns>
        public static ClsArbeitsprofil InsertArbeitszeitprofil(string name, ClsTag montag, ClsTag dienstag, ClsTag mittwoch, ClsTag donnerstag, ClsTag freitag, ClsTag samstag, ClsTag sonntag, bool gleitzeit, bool ruhestand)
        {   
            string query = "INSERT INTO arbeitszeitprofile (Name, Montag, Dienstag, Mittwoch, Donnerstag, Freitag, Samstag, Sonntag, Gleitzeit, Ruhestand)" +
                "VALUES(@name, @montag, @dienstag, @mittwoch, @donnerstag, @freitag, @samstag, @sonntag, @gleitzeit, @ruhestand)";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("montag", montag.ID);
            cmd.Parameters.AddWithValue("dienstag", dienstag.ID);
            cmd.Parameters.AddWithValue("mittwoch", mittwoch.ID);
            cmd.Parameters.AddWithValue("donnerstag", donnerstag.ID);
            cmd.Parameters.AddWithValue("freitag", freitag.ID);
            cmd.Parameters.AddWithValue("samstag", samstag.ID);
            cmd.Parameters.AddWithValue("sonntag", sonntag.ID);
            cmd.Parameters.AddWithValue("gleitzeit", gleitzeit);
            cmd.Parameters.AddWithValue("ruhestand", ruhestand);

            ExecuteNonQuery(cmd);

            int id = SelectAllArbeitszeitprofil().Last().ID;

            Log("Abzp mit ID " + id + " wurde in die Datenbank eingefügt", 4);

            return new ClsArbeitsprofil(id, name, montag, dienstag, mittwoch, donnerstag, freitag, samstag, sonntag, gleitzeit, ruhestand);
        }

        /// <summary>
        /// Fügt einen Mitarbeiter in die Datenbank ein
        /// </summary>
        /// <param name="vorname"></param>
        /// <param name="nachname"></param>
        /// <param name="arbeitsbeginn">Der Tag an dem der Mitarbeiter zu arbeiten beginnt</param>
        /// <param name="überstunden"></param>
        /// <param name="abzp"></param>
        /// <param name="urlaub">Der verfügbare Urlaub des Mitarbeiters</param>
        /// <returns>Der eingefügte Mitarbeiter als ClsMitarbeiter-Objekt</returns>
        public static ClsMitarbeiter InsertMitarbeiter(string vorname, string nachname, string rfiduid, DateTime arbeitsbeginn, TimeSpan überstunden, ClsArbeitsprofil abzp, TimeSpan urlaub)
        {
            string query = "INSERT INTO mitarbeiter (Vorname, Nachname, Arbeitsbeginn, Ueberstunden, Arbeitszeitprofil, Urlaub, RFIDUID) VALUES(" +
                "@vorname, @nachname, @arbeitsbeginn, @überstunden, @arbeitszeitp, @urlaub, @rfiduid)";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("vorname", vorname);
            cmd.Parameters.AddWithValue("nachname", nachname);
            cmd.Parameters.AddWithValue("arbeitsbeginn", arbeitsbeginn);
            cmd.Parameters.AddWithValue("überstunden", überstunden);
            cmd.Parameters.AddWithValue("arbeitszeitp", abzp.ID);
            cmd.Parameters.AddWithValue("urlaub", urlaub);
            cmd.Parameters.AddWithValue("rfiduid", rfiduid);

            ExecuteNonQuery(cmd);

            int id = SelectAllMitarbeiter().Last().ID;

            Log("Mtbtr mit ID " + id + " wurde in die Datenbank eingefügt", 4);

            return new ClsMitarbeiter(id, vorname, nachname, rfiduid, abzp, arbeitsbeginn, urlaub, überstunden);
        }

        /// <summary>
        /// Fügt eine FingerprintID und eine RFIDUID in die Datenbank ein um eine Verbindung zwischen den beiden herstellen zu können
        /// </summary>
        /// <param name="FingerprintID"></param>
        /// <param name="RFIDUID"></param>
        /// <returns>Die eingefügten Daten in einem ClsFingerprintRFID-Objekt</returns>
        public static ClsFingerprintRFID InsertFingerRFIDUID(int FingerprintID, string Fingername, int MtbtrID)
        {
            string query = "INSERT INTO fingerprintrfid (MtbtrID, Fingerprint, FingerName) VALUES (@mtbtrid, @fingerprint, @fingername)";
            
            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("mtbtrid", MtbtrID);
            cmd.Parameters.AddWithValue("fingerprint", FingerprintID);
            cmd.Parameters.AddWithValue("fingername", Fingername);

            ExecuteNonQuery(cmd);

            int id = SelectAllFingerprintRFID().Last().ID;

            Log("FingerprintRFIDObjekt mit ID " + id + " wurde in die Datenbank eingefügt", 4);

            return new ClsFingerprintRFID(id, FingerprintID, Fingername, MtbtrID);
        }

        /// <summary>
        /// Fügt die Daten eines ausgewerteten Tages in die Datenbank ein beziehungsweise aktualisiert das Objekt, sollte es bereits einen ausgewerteten Tag für das betroffene Datum geben
        /// </summary>
        /// <param name="date"></param>
        /// <param name="MtbtrID"></param>
        /// <param name="Arbeitszeit"></param>
        /// <param name="Status">0 = Zeitausgleich, 1 = Krank, 2 = Schule, 3 = Urlaub</param>
        /// <returns>Die eingefügten Daten als ClsAusgewerteter_Tag-Objekt</returns>
        public static ClsAusgewerteter_Tag InsertAusgewerteterTag(DateTime date, int MtbtrID, TimeSpan Arbeitszeit, int Status)
        {
            ClsAusgewerteter_Tag ausgTag = SelectAusgewerteterTag(date, MtbtrID);
            int id;
            if (ausgTag == null)
            {
                string query = "INSERT INTO ausgewertete_tage (Datum, MtbtrID, Arbeitszeit, Status) VALUES (@date, @mtbtrid, @abzeit, @status)";

                MySqlCommand cmd = new(query);
                cmd.Parameters.AddWithValue("date", date);
                cmd.Parameters.AddWithValue("mtbtrid", MtbtrID);
                cmd.Parameters.AddWithValue("abzeit", Arbeitszeit);
                cmd.Parameters.AddWithValue("status", Status);

                ExecuteNonQuery(cmd);

                id = SelectAusgewerteterTag(date, MtbtrID).ID;

                Log("Ausgewerteter Tag mit ID " + id + " wurde in die Datenbank eingefügt", 4);
            }
            else
            {
                UpdateAusgewerteterTag(date, MtbtrID, Arbeitszeit, ausgTag.Status);
                id = SelectAusgewerteterTag(date, MtbtrID).ID;
            }

            return new ClsAusgewerteter_Tag(id, MtbtrID, Arbeitszeit, date, Status);
        }

        /// <summary>
        /// Fügt eine Liste aus ausgewerteten Tagen in die Datenbank ein
        /// </summary>
        /// <param name="Tage"></param>
        public static void InsertMultipleAusgewerteterTag(List<ClsAusgewerteter_Tag> Tage)
        {
            string query = "INSERT INTO ausgewertete_tage (Datum, MtbtrID, Arbeitszeit, Status) VALUES ";

            int i = 0;
            foreach(ClsAusgewerteter_Tag tag in Tage)
            {
                if(i != 0)
                {
                    query += ", ";
                }
                string paraname = "@zeit" + i;
                string paraname1 = "@arbeitszeit" + i;
                query += "(" + paraname + ", " + tag.MtbtrID + ", " + paraname1 + ", " + tag.Status + ")";

                i++;
            }
            query += ";";

            MySqlCommand cmd = new(query);
            i = 0;
            foreach(ClsAusgewerteter_Tag tag in Tage)
            {
                string paraname = "zeit" + i;
                string paraname1 = "arbeitszeit" + i;

                cmd.Parameters.AddWithValue(paraname, tag.Date);
                cmd.Parameters.AddWithValue(paraname1, tag.Arbeitszeit);

                i++;
            }

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log(Tage.Count + " ausgewertete Tage wurden gemeinsam in die Datenbank eingefügt und es wurden " + veränderteDatensätze + " Datensätze verändert", 4);
        }

        /// <summary>
        /// Fügt ein AbzpMtbtrObjekt in die Datenbank ein
        /// </summary>
        /// <param name="AbzpID"></param>
        /// <param name="MtbtrID"></param>
        /// <returns>Die eingefügten Daten als ClsAbzpMtbtr Objekt</returns>
        public static ClsAbzpMtbtr InsertAbzpMtbtr(int AbzpID, int MtbtrID, DateTime startdate)
        {
            string query = "INSERT INTO abzpmtbtr (MtbtrID, AbzpID, Startdatum, Enddatum) VALUES (@mtbtrid, @abzpid, @startdatum, @enddatum)";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("mtbtrid", MtbtrID);
            cmd.Parameters.AddWithValue("abzpid", AbzpID);
            cmd.Parameters.AddWithValue("startdatum", startdate);
            cmd.Parameters.AddWithValue("enddatum", new DateTime(2001,1,1));

            ExecuteNonQuery(cmd);

            int id = SelectLastAbzpMtbtr().ID;

            Log("AbzpMtbtr Objekt mit ID " + id + " wurde in die Datenbank eingefügt", 4);

            return new ClsAbzpMtbtr(id, AbzpID, MtbtrID, DateTime.Today, new DateTime(2001, 1, 1));
        }

        /// <summary>
        /// Fügt mehrere Monatsübersichten gleichzeitig in die Datenbank ein
        /// </summary>
        /// <param name="monatsübersichten">(Überstunden, Urlaub, der 1. Tag des Monats mit Uhrzeit 00:00:00, ID des Mtbtrs</param>
        /// <returns>Die Anzahl der eingefügten Zeilen in der Datenbank</returns>
        public static int InsertMultipeMonatsübersichten(List<(TimeSpan, TimeSpan, DateTime, int)> monatsübersichten)
        {
            string query = "INSERT INTO monatsuebersichten (Monat, MtbtrID, Monatsueberstunden, Monatsurlaub) VALUES ";

            int i = 0;
            foreach((TimeSpan, TimeSpan, DateTime, int) monat in monatsübersichten)
            {
                if(i!=0) { query += ", "; }

                query += "(" + monat.Item3 + ", " + monat.Item4 + ", " + monat.Item1 + ", " + monat.Item2 + ")";
                i++;
            }
            query = ";";

            MySqlCommand cmd = new(query);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log(monatsübersichten.Count + " Monatsübersichten wurden gemeinsam in die Datenbank eingefügt und dabei wurden " + veränderteDatensätze + " Datensätze verändert", 4);
            return veränderteDatensätze;
        }

        /// <summary>
        /// Ruft alle Buchungen, die in Table gespeichert wurden ab
        /// </summary>
        /// <param name="Table">Tabelle mit Buchungen des Arduinos: buchungen_temp; Tabelle mit den Buchungen bereits berechneter Tage: buchungen</param>
        /// <returns></returns>
        public static List<ClsBuchung> SelectAllBuchungen(string Table)
        {
            string query = "SELECT * FROM " + Table;

            MySqlCommand cmd = new(query);

            return SelectBuchung(cmd, Table);
        }

        /// <summary>
        /// Ruft alle Buchungen aus Table ab, die von mtbtr getätigt wurden
        /// </summary>
        /// <param name="mtbtr"></param>
        /// <param name="Table">Tabelle mit Buchungen des Arduinos: buchungen_temp; Tabelle mit den Buchungen bereits berechneter Tage: buchungen</param>
        /// <returns></returns>
        public static List<ClsBuchung> SelectAllBuchungenFromMtbtr(ClsMitarbeiter mtbtr, string Table)
        {
            string query = "SELECT * FROM " + Table + " WHERE MtbtrID=" + mtbtr.ID;

            MySqlCommand cmd = new(query);

            return SelectBuchung(cmd, Table);
        }

        /// <summary>
        /// Ruft alle Buchungen aus Table ab, die mitbtr an date getätigt hat
        /// </summary>
        /// <param name="mtbtr"></param>
        /// <param name="date"></param>
        /// <param name="Table">Tabelle mit Buchungen des Arduinos: buchungen_temp; Tabelle mit den Buchungen bereits berechneter Tage: buchungen</param>
        /// <returns></returns>
        public static List<ClsBuchung> SelectAllBuchungenFromDay(ClsMitarbeiter mtbtr, DateTime date, string Table)
        {
            if(Table == "buchungen")
            {
                string query = "SELECT * FROM " + Table + " WHERE MtbtrID=@mtbtrid AND (Zeit BETWEEN @zeit1 AND @zeit2)";

                MySqlCommand cmd = new(query);

                DateTime dt1 = new(date.Year, date.Month, date.Day, 0, 0, 0);
                DateTime dt2 = new(date.Year, date.Month, date.Day, 23, 59, 59);

                cmd.Parameters.AddWithValue("mtbtrid", mtbtr.ID);
                cmd.Parameters.AddWithValue("zeit1", dt1);
                cmd.Parameters.AddWithValue("zeit2", dt2);
                cmd.Parameters.AddWithValue("table", Table);

                return SelectBuchung(cmd, Table);
            }
            else if (Table == "buchungen_temp")
            {
                List<ClsBuchung> buchungen = SelectAllBuchungenFromMtbtr(mtbtr, "buchungen_temp");

                return buchungen.FindAll(x => x.Zeit.ToShortDateString().Equals(date.ToShortDateString()));
            }
            return [];
        }

        /// <summary>
        /// Ruft die zuletzt hinzugefügte Buchung ab
        /// </summary>
        /// <returns></returns>
        public static ClsBuchung SelectLastBuchung()
        {
            string query = "SELECT * FROM buchungen WHERE Buchungsnummer=(SELECT max(Buchungsnummer) FROM buchungen)";

            MySqlCommand cmd = new(query);

            return SelectBuchung(cmd, "buchungen").FirstOrDefault();
        }

        /// <summary>
        /// Führt den angegebenen Command in der Datenbank aus
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="Table">Die Tabelle in der der Command ausgeführt werden soll (buchungen/buchungen_temp)</param>
        /// <returns>Die angefragten Buchungen in einer Liste</returns>
        public static List<ClsBuchung> SelectBuchung(MySqlCommand cmd, string Table)
        {
            List<ClsBuchung> list = [];
            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();

                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime datetime;
                    if (Table == "buchungen")
                    {
                        datetime = reader.GetDateTime("Zeit");
                    }
                    else
                    {
                        datetime = StringToDateTime(reader.GetString("Zeit"));
                    }

                    ClsBuchung buchung = new(reader.GetInt16("Buchungsnummer"),
                        reader.GetInt16("MtbtrID"), datetime,
                        StringToBuchungstyp(reader.GetString("Buchungstyp")));
                    list.Add(buchung);
                }

                cmd.Dispose();
                reader.Close();
            }

            Log("Es wurde ein Select Buchung Command in der Datenbank ausgeführt und dabei " + list.Count + " Objekte zurückgegeben",4);

            return list;
        }

        /// <summary>
        /// Ruft alle gespeicherten Arbeitstage ab 
        /// </summary>
        /// <returns>Die Datensätze als Liste aus ClsTag Objekten</returns>
        public static List<ClsTag> SelectAllTage()
        {
            List<ClsTag> list = [];

            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM tage";

                MySqlCommand cmd = new(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsTag Tag = new(reader.GetInt16("ID"), reader.GetTimeSpan("Arbeitsbeginn"),
                        reader.GetTimeSpan("Arbeitsende"), reader.GetTimeSpan("Arbeitszeit"),
                        reader.GetBoolean("Pause"),
                        reader.GetTimeSpan("Pausenbeginn"), reader.GetTimeSpan("Pausenende"),
                        reader.GetTimeSpan("Pausendauer"));
                    list.Add(Tag);
                }

                cmd.Dispose();
                reader.Dispose();
            }

            Log("Es wurden alle " + list.Count + " Tage aus der Datenbank abgerufen", 4);

            return list;
        }

        /// <summary>
        /// Ruft alle gespeicherten Arbeitszeitprofifle ab
        /// </summary>
        /// <returns></returns>
        public static List<ClsArbeitsprofil> SelectAllArbeitszeitprofil()
        {
            string query = "SELECT * FROM arbeitszeitprofile";

            MySqlCommand cmd = new(query);

            return SelectArbeitszeitprofil(cmd);
        }

        /// <summary>
        /// Führt den angegebenen Command in der Datenbank aus
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>Die angefragten Abzps in eines Liste</returns>
        public static List<ClsArbeitsprofil> SelectArbeitszeitprofil(MySqlCommand cmd)
        {
            List<ClsArbeitsprofil> list = [];

            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();

                cmd.Connection = conn;
                MySqlDataReader reader = cmd.ExecuteReader();

                List<ClsTag> Tage = SelectAllTage();
                while (reader.Read())
                {
                    ClsArbeitsprofil arbeitsprofil = new(reader.GetInt16("ID"), reader.GetString("Name"),
                        Tage.Find(x => x.ID == reader.GetInt16("Montag")), Tage.Find(x => x.ID == reader.GetInt16("Dienstag")),
                        Tage.Find(x => x.ID == reader.GetInt16("Mittwoch")), Tage.Find(x => x.ID == reader.GetInt16("Donnerstag")),
                        Tage.Find(x => x.ID == reader.GetInt16("Freitag")), Tage.Find(x => x.ID == reader.GetInt16("Samstag")),
                        Tage.Find(x => x.ID == reader.GetInt16("Sonntag")), reader.GetBoolean("Gleitzeit"), reader.GetBoolean("Ruhestand"));

                    list.Add(arbeitsprofil);
                }

                cmd.Dispose();
                reader.Dispose();
            }

            Log("Es wurde ein Select Abzp Command in der Datenbank ausgeführt und dabei wurden " + list.Count + " Objekte zurückgegeben", 4);

            return list;
        }

        /// <summary>
        /// Ruft alle gespeicherten Mitarbeiter ab
        /// </summary>
        /// <returns></returns>
        public static List<ClsMitarbeiter> SelectAllMitarbeiter()
        {
            List<ClsMitarbeiter> list = [];

            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();
                
                string query = "SELECT * FROM mitarbeiter";

                MySqlCommand cmd = new(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<ClsArbeitsprofil> abzt = SelectAllArbeitszeitprofil();
                while (reader.Read())
                {
                    ClsMitarbeiter mitarbeiter = new(reader.GetInt16("ID"),
                        reader.GetString("Vorname"), reader.GetString("Nachname"), reader.GetString("RFIDUID"),
                        abzt.Find(x => x.ID == reader.GetInt16("Arbeitszeitprofil")), reader.GetDateTime("Arbeitsbeginn"),
                        reader.GetTimeSpan("Urlaub"), reader.GetTimeSpan("Ueberstunden"));
                    list.Add(mitarbeiter);
                }

                cmd.Dispose();
                reader.Dispose();
            }

            Log("Es wurden alle " + list.Count + " Mtbtr aus der Datenbank abgerufen", 4);

            return list;
        }

        /// <summary>
        /// Ruft alle gespeicherten ClsFingerprintRFID-Objekte ab
        /// </summary>
        /// <returns></returns>
        public static List<ClsFingerprintRFID> SelectAllFingerprintRFID()
        {
            string query = "SELECT * FROM fingerprintrfid";

            return SelectFingerprintRFID(new MySqlCommand(query));
        }

        /// <summary>
        /// Ruft alle FingerprintRFID Objekte eines Mitarbeiters ab
        /// </summary>
        /// <param name="MtbtrID"></param>
        /// <returns></returns>
        public static List<ClsFingerprintRFID> SelectFingerprintRFIDofMtbtr(int MtbtrID)
        {
            string query = "SELECT * FROM fingerprintrfid WHERE MtbtrID=" + MtbtrID;

            return SelectFingerprintRFID(new MySqlCommand(query));
        }

        /// <summary>
        /// Führt den angegebenen Command aus und gibt eine Liste an ClsFingerprintRFID zurück
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static List<ClsFingerprintRFID> SelectFingerprintRFID(MySqlCommand cmd)
        {
            List<ClsFingerprintRFID> list = [];

            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();

                cmd.Connection = conn;

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsFingerprintRFID fingerprintRFID = new(reader.GetInt32("ID"), reader.GetInt32("Fingerprint"),
                        reader.GetString("Fingername"), reader.GetInt32("MtbtrID"));

                    list.Add(fingerprintRFID);
                }

                cmd.Dispose();
                reader.Dispose();
            }

            Log("Es wurde ein Select FingerprintRFID Command in der Datenbank ausgeführt und dabei wurden " + list.Count + " Objekte zurückgegeben", 4);

            return list;
        }

        /// <summary>
        /// Ruft den ausgewerteten Tag von Mtbtr an date ab
        /// </summary>
        /// <param name="date"></param>
        /// <param name="MtbtrID"></param>
        /// <returns>Die abgefragten Daten als ClsAusgewerteter_Tag-Objekt</returns>
        public static ClsAusgewerteter_Tag SelectAusgewerteterTag(DateTime date, int MtbtrID)
        {
            string query = "SELECT * FROM ausgewertete_tage WHERE Datum=@date AND MtbtrID=@mtbtrid";

            MySqlCommand cmd = new(query);

            cmd.Parameters.AddWithValue("date", date);
            cmd.Parameters.AddWithValue("mtbtrid", MtbtrID);

            return SelectAusgewerteterTag(cmd).FirstOrDefault();
        }

        /// <summary>
        /// Ruft alle ausgewerteten Tage vom Mitarbeiter mit MtbtrID zwischen startdate und enddate ab einschließlich dem startdate und dem enddate
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="MtbtrID"></param>
        /// <returns></returns>
        public static List<ClsAusgewerteter_Tag> SelectAusgewerteterTag(DateTime startdate, DateTime enddate, int MtbtrID)
        {
            string query = "SELECT * FROM ausgewertete_tage WHERE (Datum BETWEEN @date1 AND @date2) AND MtbtrID=@mtbtrid";

            MySqlCommand cmd = new(query);

            cmd.Parameters.AddWithValue("date1", startdate);
            cmd.Parameters.AddWithValue("date2", enddate);
            cmd.Parameters.AddWithValue("mtbtrid", MtbtrID);

            return SelectAusgewerteterTag(cmd);
        }

        /// <summary>
        /// Ruft den zuletzt hinzugefügten ausgewerteten Tag ab
        /// </summary>
        /// <returns></returns>
        public static ClsAusgewerteter_Tag SelectLastAusgewerteterTag()
        {
            string query = "SELECT * FROM ausgewertete_tage WHERE ID=(SELECT max(ID) FROM ausgewertete_tage)";

            MySqlCommand cmd = new(query);

            return SelectAusgewerteterTag(cmd).FirstOrDefault();
        }

        /// <summary>
        /// Ruft alle ausgewerteten Tage von mtbtr ab
        /// </summary>
        /// <param name="mtbtr"></param>
        /// <returns></returns>
        public static List<ClsAusgewerteter_Tag> SelectAusgewerteterTag(ClsMitarbeiter mtbtr)
        {
            string query = "SELECT * FROM ausgewertete_tage WHERE MtbtrID=" + mtbtr.ID;

            MySqlCommand cmd = new(query);

            return SelectAusgewerteterTag(cmd);
        }

        /// <summary>
        /// Führt den angegebenen Command in der Datenbank aus
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>Die angefragten ausgewerteten Tage als Liste</returns>
        public static List<ClsAusgewerteter_Tag> SelectAusgewerteterTag(MySqlCommand cmd)
        {
            List<ClsAusgewerteter_Tag> list = [];

            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();
                cmd.Connection = conn;

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsAusgewerteter_Tag ausgewerteterTag = new(reader.GetInt32("ID"), reader.GetInt32("MtbtrID"),
                        reader.GetTimeSpan("Arbeitszeit"), reader.GetDateTime("Datum"), reader.GetInt32("Status"));

                    list.Add(ausgewerteterTag);
                }
            }

            Log("Es wurde ein Select ausgewerteter Tag Command in der Datenbank ausgeführt und dabei wurden " + list.Count + " Objekte zurückgegeben", 4);

            return list;
        }

        /// <summary>
        /// Ruft das zuletzt hinzugefügte AbzpMtbtr Objekt ab
        /// </summary>
        /// <returns></returns>
        public static ClsAbzpMtbtr SelectLastAbzpMtbtr()
        {
            string query = "SELECT * FROM abzpmtbtr WHERE ID=(SELECT max(ID) FROM abzpmtbtr)";

            MySqlCommand cmd = new(query);

            return SelectAbzpMtbtr(cmd).FirstOrDefault();
        }

        /// <summary>
        /// Sucht alle AbzpMtbtr Objekte mit der mit der angegebenen Kombination aus MtbtrID und AbzpID
        /// </summary>
        /// <param name="AbzpID"></param>
        /// <param name="MtbtrID"></param>
        /// <returns>Eine Liste der gefundenen Objekte</returns>
        public static List<ClsAbzpMtbtr> SelectAbzpMtbtr(int AbzpID, int MtbtrID)
        {
            string query = "SELECT * FROM abzpmtbtr WHERE MtbtrID=@mtbtrID AND AbzpID=@abzpID";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("mtbtrID", MtbtrID);
            cmd.Parameters.AddWithValue("abzpID", AbzpID);

            return SelectAbzpMtbtr(cmd);
        }

        /// <summary>
        /// Sucht alle AbzpMtbtr Objekte, die das angegebene Abzp enthalten
        /// </summary>
        /// <param name="AbzpID"></param>
        /// <returns></returns>
        public static List<ClsAbzpMtbtr> SelectAbzpMtbtr(int AbzpID)
        {
            string query = "SELECT * FROM abzpmtbtr WHERE AbzpID=@abzpID";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("abzpID", AbzpID);

            return SelectAbzpMtbtr(cmd);
        }

        /// <summary>
        /// Ruft alle AbzpMtbtr Objekte ab, die zu einem bestimmten Mtbtr gehören
        /// </summary>
        /// <param name="mtbtr"></param>
        /// <returns></returns>
        public static List<ClsAbzpMtbtr> SelectAbzpMtbtr(ClsMitarbeiter mtbtr)
        {
            string query = "SELECT * FROM abzpmtbtr WHERE MtbtrID=@mtbtrid";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("mtbtrid", mtbtr.ID);

            return SelectAbzpMtbtr(cmd);
        }

        /// <summary>
        /// Führt den angegebenen Command in der Datenbank aus und gibt die ausgewählten AbzpMtbtr Objekte in einer Liste zurück
        /// </summary>
        /// <param name="cmd">Der auszuführende Command mit hinzugefügten Parametern</param>
        /// <returns></returns>
        public static List<ClsAbzpMtbtr> SelectAbzpMtbtr(MySqlCommand cmd)
        {
            List<ClsAbzpMtbtr> list = [];

            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();

                cmd.Connection = conn;

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsAbzpMtbtr abzpMtbtr = new(reader.GetInt32("ID"), reader.GetInt32("AbzpID"), reader.GetInt32("MtbtrID"), reader.GetDateTime("Startdatum"), reader.GetDateTime("Enddatum"));

                    list.Add(abzpMtbtr);
                }
            }

            Log("Es wurde ein Select AbzpMtbtr Command ausgeführt und dabei wurden " + list.Count + " Objekte zurückgegeben", 4);

            return list;
        }

        /// <summary>
        /// Liest die Monatsüberstunden und den Urlaub am Ende eines Monats eines Mitarbeiters aus
        /// </summary>
        /// <param name="monat">Der 1. des Monats mit einer Zeit von 00:00:00</param>
        /// <param name="mtbtrID"></param>
        /// <returns>Ein Variablentupel mit: (Überstunden,Urlaub)</returns>
        public static (TimeSpan,TimeSpan) SelectMonatsübersicht(DateTime monat, int mtbtrID)
        {
            List<(TimeSpan, TimeSpan)> values = [];

            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM monatsuebersicht WHERE monat=@monat AND MtbtrID=@mtbtrID";

                MySqlCommand cmd = new(query, conn);

                cmd.Parameters.AddWithValue("@monat", monat);
                cmd.Parameters.AddWithValue("mtbtrID", mtbtrID);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    values.Add((reader.GetTimeSpan("Monatsueberstunden"), reader.GetTimeSpan("Monatsurlaub")));
                }
            }

            Log("Es wurde die Monatsübersicht von " + mtbtrID + " vom Monat " + monat.ToString("Y") + " abgerufen", 4);

            return values.FirstOrDefault();
        }

        /// <summary>
        /// Aktualisiert alle Daten der Buchung mit der ID von Buchung auf die Daten von Buchung
        /// </summary>
        /// <param name="Buchung"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int UpdateBuchung(ClsBuchung Buchung)
        {
            string query = "UPDATE buchungen SET MtbtrID=@mtbtrid, Buchungstyp=@btyp, Zeit=@zeit WHERE Buchungsnummer=@bnr";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("mtbtrid", Buchung.MtbtrID);
            cmd.Parameters.AddWithValue("btyp", Buchung.Buchungstyp.ToString());;
            cmd.Parameters.AddWithValue("zeit", Buchung.Zeit);
            cmd.Parameters.AddWithValue("bnr", Buchung.Buchungsnummer);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log("Es wurde eine Buchung geupdatet und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Aktualisiert alle Daten des Tages mit der ID von Tag auf die Daten von Tag
        /// </summary>
        /// <param name="Tag"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int UpdateTag(ClsTag Tag)
        {
            string query = "UPDATE tage SET Arbeitsbeginn=@abbeginn, Arbeitsende=@abende, Arbeitszeit=@abzeit, Pausenbeginn=@pbeginn, Pausenende=" +
                "@pende, Pausendauer=@pdauer, Pause=@pause WHERE ID=@id";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("abbeginn", Tag.Arbeitsbeginn);
            cmd.Parameters.AddWithValue("abende", Tag.Arbeitsende);
            cmd.Parameters.AddWithValue("abzeit", Tag.Arbeitszeit);
            cmd.Parameters.AddWithValue("pbeginn", Tag.Pausenbeginn);
            cmd.Parameters.AddWithValue("pende", Tag.Pausenende);
            cmd.Parameters.AddWithValue("pdauer", Tag.Pausendauer);
            cmd.Parameters.AddWithValue("pause", Tag.Pause);
            cmd.Parameters.AddWithValue("id", Tag.ID);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log("Es wurde ein Tag geupdatet und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Aktualisiert alle Daten des Arbeitsprofils mit der ID von Abzp auf die Daten von Abzp
        /// </summary>
        /// <param name="Abzp"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int UpdateArbeitszeitprofil(ClsArbeitsprofil Abzp)
        {
            string query = "UPDATE arbeitszeitprofile SET Name=@name, Montag=@mo, Dienstag=@di, Mittwoch=@mi, Donnerstag=@do, Freitag=@fr, Samstag=@sa, " +
                "Sonntag=@so, Gleitzeit=@gleitzeit, Ruhestand=@ruhestand WHERE ID=@id";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("name", Abzp.Name);
            cmd.Parameters.AddWithValue("mo", Abzp.Montag.ID);
            cmd.Parameters.AddWithValue("di", Abzp.Dienstag.ID);
            cmd.Parameters.AddWithValue("mi", Abzp.Mittwoch.ID);
            cmd.Parameters.AddWithValue("do", Abzp.Donnerstag.ID);
            cmd.Parameters.AddWithValue("fr", Abzp.Freitag.ID);
            cmd.Parameters.AddWithValue("sa", Abzp.Samstag.ID);
            cmd.Parameters.AddWithValue("so", Abzp.Sonntag.ID);
            cmd.Parameters.AddWithValue("gleitzeit", Abzp.Gleitzeit);
            cmd.Parameters.AddWithValue("id", Abzp.ID);
            cmd.Parameters.AddWithValue("ruhestand", Abzp.Ruhestand);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log("Es wurde ein Abzp geupdatet und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Aktualisiert alle Daten des Mitarbeiters mit der ID von Mtbtr auf die Daten von Mtbtr
        /// </summary>
        /// <param name="Mtbtr"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int UpdateMitarbeiter(ClsMitarbeiter Mtbtr)
        {
            string query = "UPDATE mitarbeiter SET Vorname=@vorname, Nachname=@nachname, Arbeitszeitprofil=@abzp, Arbeitsbeginn=@abbeginn, Urlaub=@urlaub, " +
                "Ueberstunden=@überstunden, RFIDUID=@rfiduid WHERE ID=@id";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("vorname", Mtbtr.Vorname);
            cmd.Parameters.AddWithValue("nachname", Mtbtr.Nachname);
            cmd.Parameters.AddWithValue("abzp", Mtbtr.Arbeitszeitprofil.ID);
            cmd.Parameters.AddWithValue("abbeginn", Mtbtr.Arbeitsbeginn);
            cmd.Parameters.AddWithValue("urlaub", Mtbtr.Urlaub);
            cmd.Parameters.AddWithValue("überstunden", Mtbtr.Überstunden);
            cmd.Parameters.AddWithValue("rfiduid", Mtbtr.RFIDUID);
            cmd.Parameters.AddWithValue("id", Mtbtr.ID);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log("Es wurde ein Mtbtr geupdatet und dabei " + veränderteDatensätze + " Datensätze verändert", 4);  

            return veränderteDatensätze;
        }

        /// <summary>
        /// Aktualisiert alle Daten des ClsFingerprintRFID-Objekts mit der ID von fingerprintRFID auf die Daten von fingerprintRFID
        /// </summary>
        /// <param name="fingerprintRFID"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int UpdateFingerprintRFID(ClsFingerprintRFID fingerprintRFID)
        {
            string query = "UPDATE fingerprintrfid SET Fingerprint=@finger, MtbtrID=@mtbtrid, FingerName=@fingername WHERE ID=@id";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("finger", fingerprintRFID.Fingerprint);
            cmd.Parameters.AddWithValue("mtbtrid", fingerprintRFID.MtbtrID);
            cmd.Parameters.AddWithValue("fingername", fingerprintRFID.FingerName);
            cmd.Parameters.AddWithValue("id", fingerprintRFID.ID);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log("Es wurde ein FingerprintRFID Objekt geupdatet und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Updated mehrere FingerprintRFID Objekte auf einmal in der Datenbank
        /// </summary>
        /// <param name="fingerprintRFIDs"></param>
        /// <param name="RFIDUID"></param>
        /// <returns>Die Anzahl der veränderten Datensätze</returns>
        public static int UpdateMultipleFingerprintRFID(List<ClsFingerprintRFID> fingerprintRFIDs, string RFIDUID)
        {
            string query = "UPDATE fingerprintrfid SET RFIDUID=@uid WHERE ID IN ( ";

            foreach(ClsFingerprintRFID f in fingerprintRFIDs)
            {
                query += f.ID;
                query += ", ";
            }

            query += ")";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("uid", RFIDUID);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log("Es wurden " + fingerprintRFIDs.Count + " FingerprintRFID Objekte geupdatet und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Aktualisiert alle Daten des ausgewerteten Tages an date vom Mitarbeiter mit MtbtrID auf die eingegebenen Daten
        /// </summary>
        /// <param name="date"></param>
        /// <param name="MtbtrID"></param>
        /// <param name="Arbeitszeit"></param>
        /// <param name="Status"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int UpdateAusgewerteterTag(DateTime date, int MtbtrID, TimeSpan Arbeitszeit, int Status)
        {
            string query = "UPDATE ausgewertete_tage SET Datum=@date, MtbtrID=@mbtrid, Arbeitszeit=@abzeit, Status=@status WHERE Datum=@date AND MtbtrID=@mbtrid";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("date", date);
            cmd.Parameters.AddWithValue("mbtrid", MtbtrID);
            cmd.Parameters.AddWithValue("abzeit", Arbeitszeit);
            cmd.Parameters.AddWithValue("status", Status);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log("Es wurde ein ausgewerteter Tag mit Hilfe von seinen Werten geupdatet und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Aktualisiert alle Daten des ausgewerteten Tages mit der ID von tag auf die Daten von tag
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int UpdateAusgewerteterTag(ClsAusgewerteter_Tag tag)
        {
            string query = "UPDATE ausgewertete_tage SET Datum=@date, MtbtrID=@mbtrid, Arbeitszeit=@abzeit, Status=@status WHERE ID=@id";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("date", tag.Date);
            cmd.Parameters.AddWithValue("mbtrid", tag.MtbtrID);
            cmd.Parameters.AddWithValue("abzeit", tag.Arbeitszeit);
            cmd.Parameters.AddWithValue("status", tag.Status);
            cmd.Parameters.AddWithValue("id", tag.ID);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log("Es wurde der ausgewertete Tag " + tag.ID + " geupdatet und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Fügt das angegebene Datum als Enddatum beim angefügten AbzpMtbtr Objekt hinzu
        /// </summary>
        /// <param name="abzpMtbtr"></param>
        /// <param name="Enddate">Das gewünschte Enddatum des Abzps</param>
        /// <returns>Das mitgegebene AbzpMtbtr Objekt mit hinzugefügtem Enddatum</returns>
        public static ClsAbzpMtbtr EndAbzpMtbtr(ClsAbzpMtbtr abzpMtbtr, DateTime Enddate)
        {
            string query = "UPDATE abzpmtbtr SET Enddatum=@enddatum WHERE ID=@id";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("enddatum", Enddate);
            cmd.Parameters.AddWithValue("id", abzpMtbtr.ID);

            abzpMtbtr.Enddate = DateTime.Today;

            ExecuteNonQuery(cmd);

            Log("Es wurde dem AbzpMtbtrObjekt " + abzpMtbtr.ID + " das Enddatum "+ Enddate.ToString("d")  + " hinzugefügt", 4);
            return abzpMtbtr;
        }

        /// <summary>
        /// Löscht die Buchung mit der ID von buchung aus Table
        /// </summary>
        /// <param name="buchung"></param>
        /// <param name="Table"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteBuchung(ClsBuchung buchung, string Table)
        {
            string query = "DELETE FROM " + Table + " WHERE Buchungsnummer=" + buchung.Buchungsnummer;

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurde die Buchung " + buchung.Buchungsnummer + " gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht mehrere Buchungen auf einmal aus der Datenbank. Geht schneller als alle Buchungen einzeln zu löschen
        /// </summary>
        /// <param name="buchungen"></param>
        /// <param name="Table"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteMultipleBuchungen(List<ClsBuchung> buchungen, string Table)
        {
            string query = "DELETE FROM " + Table + " WHERE Buchungsnummer IN (";

            int i = 0;
            foreach(ClsBuchung buchung in buchungen)
            {
                if (i != 0)
                {
                    query += ", ";
                }
                query += buchung.Buchungsnummer;

                i++;
            }

            query += ");";

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurden " + buchungen.Count + " Buchungen gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht alle Buchungen eines Mitarbeiters auf einmal aus dem angegebenen Table
        /// </summary>
        /// <param name="mtbtr"></param>
        /// <param name="Table"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteMultipleBuchungen(ClsMitarbeiter mtbtr, string Table)
        {
            string query = "DELETE FROM " + Table + " WHERE MtbtrID = " + mtbtr.ID;

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurden die Buchungen des Mtbtr " + mtbtr.Log() + " gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht den Tag mit der ID von tag aus der Datenbank
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteTag(ClsTag tag)
        {
            string query = "DELETE FROM tage WHERE ID=" + tag.ID;

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurde der Tag " + tag.ID + " gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht alle Tage, die zu einem Arbeitszeitprofil gehören
        /// </summary>
        /// <param name="abzp"></param>
        /// <returns></returns>
        public static int DeleteMultipleTag(ClsArbeitsprofil abzp)
        {
            string query = "DELETE FROM tage WHERE ID in (@mo, @di, @mi, @do, @fr, @sa, @so)";

            MySqlCommand cmd = new(query);
            cmd.Parameters.AddWithValue("mo", abzp.Montag.ID);
            cmd.Parameters.AddWithValue("di", abzp.Dienstag.ID);
            cmd.Parameters.AddWithValue("mi", abzp.Mittwoch.ID);
            cmd.Parameters.AddWithValue("do", abzp.Donnerstag.ID);
            cmd.Parameters.AddWithValue("fr", abzp.Freitag.ID);
            cmd.Parameters.AddWithValue("sa", abzp.Samstag.ID);
            cmd.Parameters.AddWithValue("so", abzp.Sonntag.ID);

            int veränderteDatensätze = ExecuteNonQuery(cmd);

            Log("Es wurden die Tage des abzp " + abzp.ID + " gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Schickt das Arbeitszeitprofil in den Ruhestand und überprüft ob das Abzp noch von AbzpMtbtr Objekten verwendet wird. Sollte das Abzp nicht mehr verwendet
        /// werden, wird es aus der Datenbank gelöscht.
        /// </summary>
        /// <param name="abzp"></param>
        public static void DeleteArbeitszeitprofil(ClsArbeitsprofil abzp)
        {
            abzp.Ruhestand = true;

            UpdateArbeitszeitprofil(abzp);

            if(SelectAbzpMtbtr(abzp.ID).Count == 0)
            {
                string query = "DELETE FROM arbeitszeitprofile WHERE ID=" + abzp.ID;

                ExecuteNonQuery(query);

                DeleteMultipleTag(abzp);

                Log("Das Abzp " + abzp.ID + " und alle seine zugehörigen Tage wurden gelöscht", 4);
            }
            else
            {
                Log("Das Abzp " + abzp.ID + " wurde in den Ruhestand geschickt", 4);
            }

            return;
        }

        /// <summary>
        /// Löscht den Mitarbeiter mit der ID von mtbtr aus der Datenbank inkl. aller ausgewerteter Tage, AbzpMtbtr Objekte und Buchungen in buchungen_temp und buchungen, die mit dem
        /// Mitarbeiter verbunden sind
        /// </summary>
        /// <param name="mtbtr"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteMitarbeiter(ClsMitarbeiter mtbtr)
        {
            string query = "DELETE FROM mitarbeiter WHERE ID=" + mtbtr.ID;

            DeleteMultipleAusgewerteterTag(mtbtr);
            DeleteMultipleBuchungen(mtbtr, "buchungen_temp");
            DeleteMultipleBuchungen(mtbtr, "buchungen");
            DeleteMultipleAbzpMtbtr(mtbtr);
            DeleteFingerprintRFID(mtbtr);

            Log("Der Mtbtr " + mtbtr.ID + " wurde inkl aller seiner buchungen, ausgewerteten Tagen, AbzpMtbtr und FingerprintRFID Objekten gelöscht", 4);

            return ExecuteNonQuery(query);
        }

        /// <summary>
        /// Löscht das ClsFingerprintRFID-Objekt mit der ID von fingerprintRFID aus der Datenbank
        /// </summary>
        /// <param name="fingerprintRFID"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteFingerprintRFID(ClsFingerprintRFID fingerprintRFID)
        {
            string query = "DELETE FROM fingerprintrfid WHERE ID=" + fingerprintRFID.ID;

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurde das FingerprintRFID Objekt " + fingerprintRFID.ID + " gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht alle FingerprintRFID Objekte eines Mitarbeiters mithilfe der ID des Mitarbeiters
        /// </summary>
        /// <param name="mtbtr"></param>
        /// <returns>Die Anzahl der veränderten Datensätze in der Datenbank</returns>
        public static int DeleteFingerprintRFID(ClsMitarbeiter mtbtr)
        {
            string query = "DELETE FROM fingerprintrfid WHERE MtbtrID=" + mtbtr.ID;

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurden alle FingerprintRFID Objekte eines Mitarbeiters gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht ein ClsFingerprintRFID-Objekt aus der Datenbank
        /// </summary>
        /// <param name="Fingerprint">Die ID des zu löschenden Objekts</param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteFingerprintRFID(int Fingerprint)
        {
            string query = "DELETE FROM fingerprintrfid WHERE Fingerprint=" + Fingerprint;

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurde das FingerprintRFID Objekt vom Finger " + Fingerprint + " gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht den ausgewerteten Tag mit der ID von tag aus der Datenbank
        /// </summary>
        /// <param name="tag"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteAusgewerteterTag(ClsAusgewerteter_Tag tag)
        {
            string query = "DELETE FROM ausgewertete_tage WHERE ID=" + tag.ID;

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurde der ausgewertete Tag " + tag.ID + " gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht mehrere ausgewertete Tage aus der Datenbank. Geht schneller als alle einzeln zu löschen
        /// </summary>
        /// <param name="ausgewTage"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteMultipleAusgewerteterTag(List<ClsAusgewerteter_Tag> ausgewTage)
        {
            string query = "DELETE FROM 'ausgewertete_tage' WHERE ID IN (";

            int i = 0;
            foreach (ClsAusgewerteter_Tag ausgewerteter_Tag in ausgewTage)
            {
                if (i != 0)
                {
                    query += ", ";
                }
                query += ausgewerteter_Tag.ID;

                i++;
            }

            query += ");";

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurden " + ausgewTage.Count + " ausgewertete Tage gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht alle ausgewerteten Tage eines Mitarbeiters auf einmal
        /// </summary>
        /// <param name="mtbtr"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteMultipleAusgewerteterTag(ClsMitarbeiter mtbtr)
        {
            string query = "DELETE FROM ausgewertete_tage WHERE MtbtrID=" + mtbtr.ID;

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurden die ausgewerteten Tage des Mtbtrs " + mtbtr.Log() + " gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Löscht alle AbzpMtbtr Objekte eines Mitarbeiters
        /// </summary>
        /// <param name="mtbtr"></param>
        /// <returns>Die Anzahl veränderter Datensätze in der Datenbank</returns>
        public static int DeleteMultipleAbzpMtbtr(ClsMitarbeiter mtbtr)
        {
            string query = "DELETE FROM abzpmtbtr WHERE MtbtrID=" + mtbtr.ID;

            int veränderteDatensätze = ExecuteNonQuery(query);

            Log("Es wurden die abzpMtbtrs des Mtbtrs " + mtbtr.Log() + " gelöscht und dabei " + veränderteDatensätze + " Datensätze verändert", 4);

            return veränderteDatensätze;
        }

        /// <summary>
        /// Schreibt einen Log in eine dafür vorgesehene Datei
        /// </summary>
        /// <param name="log"></param>
        /// <param name="LogLevel">Das Level wie wichtig der Log ist.
        /// 0 Allgemeine Funktionsweise wird immer geloggt
        /// 1 Erweiterte Funktionsweise
        /// 2 Immer dann wenn ein Fenster geöffnet wird bzw eine Helper Funktion aufgerufen wird
        /// 4 Alles in DataProvider</param>
        public static void Log(string log, int LogLevel)
        {
            if(LogLevel != 0)
            {
                switch (Settings.Default.LogLevel)
                {
                    case 1:
                        if (LogLevel != 1) return;
                        break;
                    case 2:
                        if (LogLevel != 2) return;
                        break;
                    case 3:
                        if (LogLevel != 1 && LogLevel != 2) return;
                        break;
                    case 4:
                        if(LogLevel != 4)return;
                        break;
                    case 5:
                        if(LogLevel != 1 && LogLevel != 4)return;
                        break;
                    default:
                        return;
                }
            }

            string Logs = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),"TimeChipApp_Logs");
            string name = "logs_" + DateTime.Today.ToString("d") + ".txt";
            string filepath = Path.Combine(Logs, name);

            if (!Directory.Exists(Logs))
            {
                Directory.CreateDirectory(Logs);
            }

            StreamWriter sw;
            if (File.Exists(filepath))
            {
                sw = new StreamWriter(filepath, true);
            }
            else
            {
                sw = new StreamWriter(filepath, false);
            }

            sw.WriteLine(DateTime.Now.ToString("HH:mm:ss.fff")+"> " + log);

            sw.Close();
            sw.Dispose();
        }

        /// <summary>
        /// Speichert die MitarbeiterID und den zugehörigen Tag, wann der Urlaub berechnet werden soll
        /// </summary>
        /// <param name="strings">Liste aus Strings mit dem Aufbau: MtbtrID;Urlaubstag</param>
        public static void WriteDateToCSV(List<string> strings)
        {
            using StreamWriter sw = new(@"Resources\date.csv", false);
            foreach (string s in strings)
            {
                sw.WriteLine(s);
            }
        }

        /// <summary>
        /// Liest Daten von voriger Funktion wieder aus
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public static List<DateTime> ReadDateFromCSV(ref List<int> IDs)
        {
            List<DateTime> list = [];
            using (StreamReader sr = new(@"Resources\date.csv"))
            {
                while (!sr.EndOfStream)
                {
                    string zeile = sr.ReadLine();

                    string[] teile = zeile.Split(';');
                    int MitarbeiterID = Convert.ToInt32(teile[0]);

                    string[] teile1 = teile[1].Split('.');
                    int day = Convert.ToInt32(teile1[0]);
                    int month = Convert.ToInt32(teile1[1]);
                    int year = Convert.ToInt32(teile1[2]);

                    IDs.Add(MitarbeiterID);
                    list.Add(new DateTime(year, month, day, 0, 0, 0));
                }
            }

            return list;
        }

        /// <summary>
        /// Speichert das Datum an dem zuletzt die Arbeitszeit und die Überstunden aller Mitarbeiter der vergangenen Tage berechnet wurden in den Application Settings
        /// </summary>
        /// <param name="date"></param>
        public static void SaveBerechnungsdate(DateTime date)
        {
            Settings.Default.Berechnungsdate = date;
            Settings.Default.Save();

            Log("Das Berechnungsdate " + date.ToString("d") + " wurde gespeichert", 4);
        }

        /// <summary>
        /// Liest dasan dem zuletzt die Arbeitszeit und die Überstunden aller Mitarbeiter der vergangenen Tage berechnet wurden wieder aus den Application Settings aus
        /// </summary>
        /// <returns></returns>
        public static DateTime ReadBerechnungsdate()
        {
            DateTime Berechnungsdate = Settings.Default.Berechnungsdate;
            if (Berechnungsdate == new DateTime(1999, 01, 01, 0, 1, 0))
            {
                Settings.Default.Berechnungsdate = DateTime.Now;
                Settings.Default.Save();
            }

            Log("Das Berechnungsdate " + Settings.Default.Berechnungsdate.ToString("d") + " wurde ausgelesen", 4);
            return Settings.Default.Berechnungsdate;
        }

        //Helper Funktionen____________________________________________________________________________________________________________________________________

        /// <summary>
        /// Wandelt einen String, der vom Arduino in die buchungen_temp Tabelle gespeichert wurde in ein DateTime um
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static DateTime StringToDateTime(string dt)
        {
            //"%d.%m.%Y-%H:%M:%S"

            string[] split1 = dt.Split('.');
            string Day = split1[0];
            string Month = split1[1];
            string[] split2 = split1[2].Split('-');
            string Year = split2[0];
            string[] split3 = split2[1].Split(':');
            string Hour = split3[0];
            string Minute = split3[1];
            string Second = split3[2];

            int day = Convert.ToInt32(Day);
            int month = Convert.ToInt32(Month);
            int year = Convert.ToInt32(Year);
            int hour = Convert.ToInt32(Hour);
            int minute = Convert.ToInt32(Minute);
            int second = Convert.ToInt32(Second);

            return new DateTime(year, month, day, hour, minute, second);
        }

        /// <summary>
        /// Wandelt einen String, der in der Datenbank gespeichert wurde in das dazugehörige Buchungstyp-Objekt
        /// </summary>
        /// <param name="bt"></param>
        /// <returns></returns>
        private static Buchungstyp StringToBuchungstyp(string bt)
        {
            return bt switch
            {
                "Kommen" => Buchungstyp.Kommen,
                //Wenn Kommen rausgefilter muss der Rest "Gehen" sein
                _ => Buchungstyp.Gehen,
            };
        }

        /// <summary>
        /// Liest die Daten für den ConnectionString für die Verbindung mit der Datenbank aus den Application Settings aus und setzt diesen daraus
        /// zusammen und speichert ihn in der m_connectionString Variable
        /// </summary>
        public static void GetConnectionString()
        {
            string connectionstring = "";

            connectionstring += "SERVER=";
            connectionstring += Settings.Default.Server;
            connectionstring += ";DATABASE=";
            connectionstring += Settings.Default.Database;
            connectionstring += ";UID=";
            connectionstring += Settings.Default.UID;
            connectionstring += ";Password=";
            var Bytes = Convert.FromBase64String(Settings.Default.Password);
            connectionstring += Encoding.UTF8.GetString(Bytes);
            connectionstring += ";";

            m_connectionString =  connectionstring;
        }

        /// <summary>
        /// Speichert die Daten, die im ConnectionString enthalten sind in den Application Settings
        /// </summary>
        public static void SaveConnectionString()
        {
            if(m_connectionString == null)
            {
                return;
            }

            string[] teile = m_connectionString.Split('=', ';');

            Settings.Default.Server = teile[1];
            Settings.Default.Database = teile[3];
            Settings.Default.UID = teile[5];

            var Bytes = Encoding.UTF8.GetBytes(teile[7]);
            string password = Convert.ToBase64String(Bytes);

            Settings.Default.Password = password;
            Settings.Default.Save();
        }

        /// <summary>
        /// Speichert die IP des Arduinos in den Settings
        /// </summary>
        /// <param name="IP">Die IP auf die zugegriffen wird. Format: '10.100.128.10'</param>
        public static void SaveArduinoIP(string IP)
        {
            Settings.Default.ArduinoIP = IP;
            Settings.Default.Save();
        }

        /// <summary>
        /// Speichert das angegebene Log Level in den Settings
        /// </summary>
        /// <param name="LogLevel"></param>
        public static void SaveLogLevel(int LogLevel)
        {
            Settings.Default.LogLevel = LogLevel;
            Settings.Default.Save();
        }

        /// <summary>
        /// Liest die eingespeicherte IP des Arduinos aus den Settings aus
        /// </summary>
        /// <returns>Die IP im Format: '10.100.128.10'</returns>
        public static string ReadArduinoIP()
        {
            return Settings.Default.ArduinoIP;
        }

        /// <summary>
        /// Führt eine SQL-Anweisung in der Datenbank aus
        /// </summary>
        /// <param name="query"></param>
        /// <returns>Die Anzahl der veränderten Datensätze in der Datenbank</returns>
        public static int ExecuteNonQuery(string query)
        {
            int result = 0;

            if(m_connectionString is null)
            {
                GetConnectionString();
            }

            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new(query, conn);
                result = cmd.ExecuteNonQuery();

                cmd.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Führt einen MySqlCommand in der Datenbank aus
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns>Die Anzahl der veränderten Datensätze in der Datenbank</returns>
        public static int ExecuteNonQuery(MySqlCommand cmd)
        {
            int result = 0;

            if (m_connectionString is null)
            {
                GetConnectionString();
            }

            using (MySqlConnection conn = new(m_connectionString))
            {
                conn.Open();

                cmd.Connection = conn;
                result = cmd.ExecuteNonQuery();

                cmd.Dispose();
            }

            return result;
        }

        /// <summary>
        /// Sendet Daten an den Arduino und empfängt dessen Antwort
        /// </summary>
        /// <param name="text">Die zu sendenden Daten</param>
        /// <returns>Die Antwort, die vom Arduino zurückgesendet wurde</returns>
        public static string SendRecieveHTTP(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text + "\n\r\n");
            string URL = "http://" + ReadArduinoIP() + "/", responseContent;

            WebRequest webRequest = WebRequest.Create(URL);
            webRequest.Method = "POST";
            webRequest.ContentLength = data.Length;
            webRequest.Timeout = 1000;

            try
            {
                using (Stream strm = webRequest.GetRequestStream())
                {
                    strm.Write(data, 0, data.Length);
                }

                using (WebResponse response = webRequest.GetResponse())
                {
                    using Stream strm = response.GetResponseStream();
                    using StreamReader sr99 = new(strm);
                    responseContent = sr99.ReadToEnd();
                }

                return responseContent;
            }
            catch
            {
                MessageBox.Show("Der Arduino kann nicht erreicht werden!", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "-1";
            }
        }
    }
}
