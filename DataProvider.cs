using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace TimeChip_App_1._0
{
    public class DataProvider
    {
        private static readonly string m_connectionString = "SERVER=10.100.128.1;DATABASE=apotheke_time_chip;UID=Hauptapp;Password=oo/1X)ZV1jlmTyEm;";

        public static ClsBuchung InsertBuchung(Buchungstyp buchungstyp, DateTime zeit, int mitarbeiternr)
        {
            string query = "INSERT INTO `buchungen` (`Buchungstyp`, `Zeit`, `Mitarbeiternummer`) VALUES(@buchungst, @zeit, @mitarbeiternr)";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("buchungst", buchungstyp.ToString());
            cmd.Parameters.AddWithValue("zeit", zeit);
            cmd.Parameters.AddWithValue("mitarbeiternr", mitarbeiternr);

            ExecuteNonQuery(cmd);

            List<ClsMitarbeiter> mtbtrs = SelectAllMitarbeiter();
            ClsMitarbeiter mtbtr = mtbtrs.Find(x => x.Mitarbeiternummer.Equals(mitarbeiternr));

            return new ClsBuchung(SelectAllBuchungenFromDay(mtbtr, zeit, "buchungen").Last().Buchungsnummer, mitarbeiternr, zeit, buchungstyp);
        }
        public static void TransferBuchungs(List<ClsBuchung> buchungen)
        {
            string query = "INSERT INTO `buchungen` (`Buchungstyp`, `Zeit`, `Mitarbeiternummer`) VALUES ";
            string query1 = "DELETE FROM `buchungen_temp` WHERE Buchungsnummer IN (";

            int i = 0;

            foreach (ClsBuchung buchung in buchungen)
            {
                if(buchungen.FindIndex(x => x.Buchungsnummer.Equals(buchung.Buchungsnummer)) != 0)
                {
                    query += ", ";
                    query1 += ", ";
                }
                string paraname = "@zeit" + i;
                query += "('" + buchung.Buchungstyp + "', " + paraname + ", '" + buchung.Mitarbeiternummer + "')";
                query1 += buchung.Buchungsnummer;

                i++;
            }

            query += ";";
            query1 += ");";

            MySqlCommand cmd = new MySqlCommand(query);

            int k = 0;
            foreach(ClsBuchung buchung in buchungen)
            {
                string paraname = "zeit" + k;
                cmd.Parameters.AddWithValue(paraname, buchung.Zeit);
                k++;
            }

            ExecuteNonQuery(cmd);
            ExecuteNonQuery(query1);
        }
        public static ClsTag InsertTag(string name, TimeSpan arbeitsbeginn, TimeSpan arbeitsende, TimeSpan arbeitszeit, TimeSpan pausenbeginn, TimeSpan pausenende, TimeSpan pausendauer, bool pause)
        {
            string query = "INSERT INTO tage (Name, Arbeitsbeginn, Arbeitsende, Arbeitszeit, Pausenbeginn, Pausenende, Pausendauer, Pause) VALUES (@name,  @abbeginn, " +
                "@abende, @abzeit, @pbeginn, @pende, @pdauer, @pause)";

            
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("abbeginn", arbeitsbeginn);
            cmd.Parameters.AddWithValue("abende", arbeitsende);
            cmd.Parameters.AddWithValue("abzeit", arbeitszeit);
            cmd.Parameters.AddWithValue("pbeginn", pausenbeginn);
            cmd.Parameters.AddWithValue("pende", pausenende);
            cmd.Parameters.AddWithValue("pdauer", pausendauer);
            cmd.Parameters.AddWithValue("pause", pause);
            
            ExecuteNonQuery(cmd);

            return new ClsTag(SelectAllTage().Last().ID, name, pause, arbeitsbeginn, arbeitsende, pausenbeginn, pausenende, arbeitszeit, pausendauer);
        }
        public static ClsArbeitsprofil InsertArbeitszeitprofil(string name, ClsTag montag, ClsTag dienstag, ClsTag mittwoch, ClsTag donnerstag, ClsTag freitag, ClsTag samstag, ClsTag sonntag, bool gleitzeit)
        {
            string query = "INSERT INTO arbeitszeitprofile (Name, Montag, Dienstag, Mittwoch, Donnerstag, Freitag, Samstag, Sonntag, Gleitzeit)" +
                "VALUES(@name, @montag, @dienstag, @mittwoch, @donnerstag, @freitag, @samstag, @sonntag, @gleitzeit)";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("montag", montag.ID);
            cmd.Parameters.AddWithValue("dienstag", dienstag.ID);
            cmd.Parameters.AddWithValue("mittwoch", mittwoch.ID);
            cmd.Parameters.AddWithValue("donnerstag", donnerstag.ID);
            cmd.Parameters.AddWithValue("freitag", freitag.ID);
            cmd.Parameters.AddWithValue("samstag", samstag.ID);
            cmd.Parameters.AddWithValue("sonntag", sonntag.ID);
            cmd.Parameters.AddWithValue("gleitzeit", gleitzeit);


            ExecuteNonQuery(cmd);

            return new ClsArbeitsprofil(SelectAllArbeitszeitprofil().Last().ID, name, montag, dienstag, mittwoch, donnerstag, freitag, samstag, sonntag, gleitzeit);
        }
        public static ClsMitarbeiter InsertMitarbeiter(int mitarbeiternummer, string vorname, string nachname, DateTime arbeitsbeginn, TimeSpan überstunden, ClsArbeitsprofil abzp, TimeSpan urlaub)
        {
            string query = "INSERT INTO mitarbeiter (Mitarbeiternummer, Vorname, Nachname, Arbeitsbeginn, Ueberstunden, Arbeitszeitprofil, Urlaub) VALUES(" +
                "@mitarbeiternr, @vorname, @nachname, @arbeitsbeginn, @überstunden, @arbeitszeitp, @urlaub)";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("mitarbeiternr", mitarbeiternummer);
            cmd.Parameters.AddWithValue("vorname", vorname);
            cmd.Parameters.AddWithValue("nachname", nachname);
            cmd.Parameters.AddWithValue("arbeitsbeginn", arbeitsbeginn);
            cmd.Parameters.AddWithValue("überstunden", überstunden);
            cmd.Parameters.AddWithValue("arbeitszeitp", abzp.ID);
            cmd.Parameters.AddWithValue("urlaub", urlaub);

            ExecuteNonQuery(cmd);

            return new ClsMitarbeiter(SelectAllMitarbeiter().Last().ID, mitarbeiternummer, vorname, nachname, abzp, arbeitsbeginn, urlaub, überstunden);
        }
        public static ClsFingerprintRFID InsertFingerRFIDUID(int FingerprintID, string RFIDUID)
        {
            string query = "INSERT INTO fingerprintrfid (Fingerprint, RFIDUID) VALUES (@fingerprint, @uid)";
            
            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("fingerprint", FingerprintID);
            cmd.Parameters.AddWithValue("uid", RFIDUID);

            ExecuteNonQuery(cmd);

            return new ClsFingerprintRFID(SelectAllFingerprintRFID().Last().ID, RFIDUID, FingerprintID);
        }
        public static ClsAusgewerteter_Tag InsertAusgewerteterTag(DateTime date, int Mitarbeiternummer, TimeSpan Arbeitszeit, int Status)
        {
            ClsAusgewerteter_Tag ausgTag = SelectAusgewerteterTag(date, Mitarbeiternummer);
            if (ausgTag == null)
            {
                string query = "INSERT INTO ausgewertete_tage (Datum, Mitarbeiternummer, Arbeitszeit, Status) VALUES (@date, @mtbtrnr, @abzeit, @status)";

                MySqlCommand cmd = new MySqlCommand(query);
                cmd.Parameters.AddWithValue("date", date);
                cmd.Parameters.AddWithValue("mtbtrnr", Mitarbeiternummer);
                cmd.Parameters.AddWithValue("abzeit", Arbeitszeit);
                cmd.Parameters.AddWithValue("status", Status);

                ExecuteNonQuery(cmd);
            }
            else
            {
                UpdateAusgewerteterTag(date, Mitarbeiternummer, Arbeitszeit, ausgTag.Status);
            }

            return new ClsAusgewerteter_Tag(SelectAusgewerteterTag(date, Mitarbeiternummer).ID, Mitarbeiternummer, Arbeitszeit, date, Status);
        }
        public static void InsertMultipleAusgewerteterTag(List<ClsAusgewerteter_Tag> Tage)
        {
            string query = "INSERT INTO ausgewertete_tage (Datum, Mitarbeiternummer, Arbeitszeit, Status) VALUES ";

            int i = 0;
            foreach(ClsAusgewerteter_Tag tag in Tage)
            {
                if(i != 0)
                {
                    query += ", ";
                }
                string paraname = "@zeit" + i;
                string paraname1 = "@arbeitszeit" + i;
                query += "(" + paraname + ", " + tag.MitarbeiterNummer + ", " + paraname1 + ", " + tag.Status + ")";

                i++;
            }
            query += ";";

            MySqlCommand cmd = new MySqlCommand(query);
            i = 0;
            foreach(ClsAusgewerteter_Tag tag in Tage)
            {
                string paraname = "zeit" + i;
                string paraname1 = "arbeitszeit" + i;

                cmd.Parameters.AddWithValue(paraname, tag.Date);
                cmd.Parameters.AddWithValue(paraname1, tag.Arbeitszeit);

                i++;
            }

            ExecuteNonQuery(cmd);
        }
        public static List<ClsBuchung> SelectAllBuchungen(string Table)
        {
            List<ClsBuchung> list = new List<ClsBuchung>();
            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();
                
                string query = "SELECT * FROM " + Table;

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DateTime datetime;
                    if(Table == "buchungen")
                    {
                        datetime = reader.GetDateTime("Zeit");
                    }
                    else
                    {
                        datetime = StringToDateTime(reader.GetString("Zeit"));
                    }
                    
                    ClsBuchung buchung = new ClsBuchung(reader.GetInt16("Buchungsnummer"),
                        reader.GetInt16("Mitarbeiternummer"), datetime,
                        StringToBuchungstyp(reader.GetString("Buchungstyp")));
                    list.Add(buchung);
                }

                cmd.Dispose();
                reader.Close();
            }
            return list;
        }
        public static List<ClsBuchung> SelectAllBuchungenFromMtbtr(ClsMitarbeiter mtbtr, string Table)
        {
            List<ClsBuchung> list = new List<ClsBuchung>();
            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM " + Table + " WHERE Mitarbeiternummer=" + mtbtr.Mitarbeiternummer;

                MySqlCommand cmd = new MySqlCommand(query, conn);
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

                    ClsBuchung buchung = new ClsBuchung(reader.GetInt16("Buchungsnummer"),
                        reader.GetInt16("Mitarbeiternummer"), datetime,
                        StringToBuchungstyp(reader.GetString("Buchungstyp")));
                    list.Add(buchung);
                }

                cmd.Dispose();
                reader.Close();
            }
            return list;
        }
        public static List<ClsTag> SelectAllTage()
        {
            List<ClsTag> list = new List<ClsTag>();

            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM tage";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsTag Tag = new ClsTag(reader.GetInt16("ID"), reader.GetString("Name"), reader.GetBoolean("Pause"), 
                        reader.GetTimeSpan("Arbeitsbeginn"),
                        reader.GetTimeSpan("Arbeitsende"), reader.GetTimeSpan("Pausenbeginn"),
                        reader.GetTimeSpan("Pausenende"), reader.GetTimeSpan("Arbeitszeit"),
                        reader.GetTimeSpan("Pausendauer"));
                    list.Add(Tag);
                }

                cmd.Dispose();
                reader.Dispose();
            }

            return list;
        }
        public static List<ClsArbeitsprofil> SelectAllArbeitszeitprofil()
        {
            List<ClsArbeitsprofil> list = new List<ClsArbeitsprofil>();

            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM arbeitszeitprofile";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<ClsTag> Tage = SelectAllTage();
                while (reader.Read())
                {
                    ClsArbeitsprofil arbeitsprofil = new ClsArbeitsprofil(reader.GetInt16("ID"), reader.GetString("Name"),
                        Tage.Find(x => x.ID == reader.GetInt16("Montag")), Tage.Find(x => x.ID == reader.GetInt16("Dienstag")),
                        Tage.Find(x => x.ID == reader.GetInt16("Mittwoch")), Tage.Find(x => x.ID == reader.GetInt16("Donnerstag")),
                        Tage.Find(x => x.ID == reader.GetInt16("Freitag")), Tage.Find(x => x.ID == reader.GetInt16("Samstag")),
                        Tage.Find(x => x.ID == reader.GetInt16("Sonntag")), reader.GetBoolean("Gleitzeit"));
                    list.Add(arbeitsprofil);
                }

                cmd.Dispose();
                reader.Dispose();
            }

            return list;
        }
        public static List<ClsMitarbeiter> SelectAllMitarbeiter()
        {
            List<ClsMitarbeiter> list = new List<ClsMitarbeiter>();

            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();
                
                string query = "SELECT * FROM mitarbeiter";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<ClsArbeitsprofil> abzt = SelectAllArbeitszeitprofil();
                while (reader.Read())
                {
                    ClsMitarbeiter mitarbeiter = new ClsMitarbeiter(reader.GetInt16("ID"), reader.GetInt16("Mitarbeiternummer"),
                        reader.GetString("Vorname"), reader.GetString("Nachname"),
                        abzt.Find(x => x.ID == reader.GetInt16("Arbeitszeitprofil")), reader.GetDateTime("Arbeitsbeginn"),
                        reader.GetTimeSpan("Urlaub"), reader.GetTimeSpan("Ueberstunden"));
                    list.Add(mitarbeiter);
                }

                cmd.Dispose();
                reader.Dispose();
            }

            return list;
        }
        public static List<ClsFingerprintRFID> SelectAllFingerprintRFID()
        {
            List<ClsFingerprintRFID> list = new List<ClsFingerprintRFID>();

            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM fingerprintrfid";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsFingerprintRFID fingerprintRFID = new ClsFingerprintRFID(reader.GetInt32("ID"), reader.GetString("RFIDUID"), reader.GetInt32("Fingerprint"));

                    list.Add(fingerprintRFID);
                }

                cmd.Dispose();
                reader.Dispose();
            }

            return list;
        }

        public static List<ClsBuchung> SelectAllBuchungenFromDay(ClsMitarbeiter mtbtr, DateTime date, string Table)
        {
            List<ClsBuchung> list = new List<ClsBuchung>();
            if (Table == "buchungen")
            {
                string query = "SELECT * FROM buchungen WHERE Mitarbeiternummer=@mtbtrnr AND (Zeit BETWEEN @zeit1 AND @zeit2)";

                using (MySqlConnection conn = new MySqlConnection(m_connectionString))
                {
                    conn.Open();

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    DateTime dt1 = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                    DateTime dt2 = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);

                    cmd.Parameters.AddWithValue("mtbtrnr", mtbtr.Mitarbeiternummer);
                    cmd.Parameters.AddWithValue("zeit1", dt1);
                    cmd.Parameters.AddWithValue("zeit2", dt2);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ClsBuchung buchung = new ClsBuchung(reader.GetInt16("Buchungsnummer"),
                            reader.GetInt16("Mitarbeiternummer"), reader.GetDateTime("Zeit"),
                            StringToBuchungstyp(reader.GetString("Buchungstyp")));

                        list.Add(buchung);
                    }

                    cmd.Dispose();
                    reader.Dispose();
                }
            }
            else if(Table == "buchungen_temp")
            {
                List<ClsBuchung> buchungen = SelectAllBuchungenFromMtbtr(mtbtr, "buchungen_temp");

                list = buchungen.FindAll(x => x.Zeit.ToShortDateString().Equals(date.ToShortDateString()));
            }

            return list;
        }
        public static ClsAusgewerteter_Tag SelectAusgewerteterTag(DateTime date, int Mitarbeiternr)
        {
            List<ClsAusgewerteter_Tag> list = new List<ClsAusgewerteter_Tag>();

            string query = "SELECT * FROM ausgewertete_tage WHERE Datum=@date AND Mitarbeiternummer=@mtbtrnr";

            using(MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("date", date);
                cmd.Parameters.AddWithValue("mtbtrnr", Mitarbeiternr);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsAusgewerteter_Tag ausgewerteterTag = new ClsAusgewerteter_Tag(reader.GetInt32("ID"), reader.GetInt32("Mitarbeiternummer"),
                        reader.GetTimeSpan("Arbeitszeit"), reader.GetDateTime("Datum"), reader.GetInt32("Status"));

                    list.Add(ausgewerteterTag);
                }
            }

            return list.FirstOrDefault();
        }
        public static List<ClsAusgewerteter_Tag> SelectAusgewerteteTage(DateTime startdate, DateTime enddate, int Mitarbeiternr)
        {
            List<ClsAusgewerteter_Tag> list = new List<ClsAusgewerteter_Tag>();

            string query = "SELECT * FROM ausgewertete_tage WHERE (Datum BETWEEN @date1 AND @date2) AND Mitarbeiternummer=@mtbtrnr";

            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                cmd.Parameters.AddWithValue("date1", startdate);
                cmd.Parameters.AddWithValue("date2", enddate);
                cmd.Parameters.AddWithValue("mtbtrnr", Mitarbeiternr);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsAusgewerteter_Tag ausgewerteterTag = new ClsAusgewerteter_Tag(reader.GetInt32("ID"), reader.GetInt32("Mitarbeiternummer"),
                        reader.GetTimeSpan("Arbeitszeit"), reader.GetDateTime("Datum"), reader.GetInt32("Status"));

                    list.Add(ausgewerteterTag);
                }
            }

            return list;
        }
        public static ClsAusgewerteter_Tag SelectLastAusgewerteterTag()
        {
            List<ClsAusgewerteter_Tag> list = new List<ClsAusgewerteter_Tag>();

            string query = "SELECT * FROM ausgewertete_tage WHERE ID=(SELECT max(ID) FROM ausgewertete_tage)";

            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsAusgewerteter_Tag ausgewerteterTag = new ClsAusgewerteter_Tag(reader.GetInt32("ID"), reader.GetInt32("Mitarbeiternummer"),
                        reader.GetTimeSpan("Arbeitszeit"), reader.GetDateTime("Datum"), reader.GetInt32("Status"));

                    list.Add(ausgewerteterTag);
                }
            }

            return list.FirstOrDefault();
        }
        public static List<ClsAusgewerteter_Tag> SelectAusgewerteteTage(ClsMitarbeiter mtbtr)
        {
            List<ClsAusgewerteter_Tag> list = new List<ClsAusgewerteter_Tag>();

            string query = "SELECT * FROM ausgewertete_tage WHERE Mitarbeiternummer=" + mtbtr.Mitarbeiternummer;

            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsAusgewerteter_Tag ausgewerteterTag = new ClsAusgewerteter_Tag(reader.GetInt32("ID"), reader.GetInt32("Mitarbeiternummer"),
                        reader.GetTimeSpan("Arbeitszeit"), reader.GetDateTime("Datum"), reader.GetInt32("Status"));

                    list.Add(ausgewerteterTag);
                }
            }

            return list;
        }

        public static int UpdateBuchung(ClsBuchung Buchung)
        {
            string query = "UPDATE buchungen SET Mitarbeiternummer=@mtbtrnr, Buchungstyp=@btyp, Zeit=@zeit WHERE Buchungsnummer=@bnr";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("mtbtrnr", Buchung.Mitarbeiternummer);
            cmd.Parameters.AddWithValue("btyp", Buchung.Buchungstyp.ToString());;
            cmd.Parameters.AddWithValue("zeit", Buchung.Zeit);
            cmd.Parameters.AddWithValue("bnr", Buchung.Buchungsnummer);

            return ExecuteNonQuery(cmd);
        }
        public static int UpdateTag(ClsTag Tag)
        {
            string query = "UPDATE tage SET Name=@name, Arbeitsbeginn=@abbeginn, Arbeitsende=@abende, Arbeitszeit=@abzeit, Pausenbeginn=@pbeginn, Pausenende=" +
                "@pende, Pausendauer=@pdauer, Pause=@pause WHERE ID=@id";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("name", Tag.Name);
            cmd.Parameters.AddWithValue("abbeginn", Tag.Arbeitsbeginn);
            cmd.Parameters.AddWithValue("abende", Tag.Arbeitsende);
            cmd.Parameters.AddWithValue("abzeit", Tag.Arbeitszeit);
            cmd.Parameters.AddWithValue("pbeginn", Tag.Pausenbeginn);
            cmd.Parameters.AddWithValue("pende", Tag.Pausenende);
            cmd.Parameters.AddWithValue("pdauer", Tag.Pausendauer);
            cmd.Parameters.AddWithValue("pause", Tag.Pause);
            cmd.Parameters.AddWithValue("id", Tag.ID);

            return ExecuteNonQuery(cmd);
        }
        public static int UpdateArbeitszeitprofil(ClsArbeitsprofil Abzp)
        {
            string query = "UPDATE arbeitszeitprofile SET Name=@name, Montag=@mo, Dienstag=@di, Mittwoch=@mi, Donnerstag=@do, Freitag=@fr, Samstag=@sa, " +
                "Sonntag=@so, Gleitzeit=@gleitzeit WHERE ID=@id";

            MySqlCommand cmd = new MySqlCommand(query);
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

            return ExecuteNonQuery(cmd);
        }
        public static int UpdateMitarbeiter(ClsMitarbeiter Mtbtr)
        {
            string query = "UPDATE mitarbeiter SET Vorname=@vorname, Nachname=@nachname, Arbeitszeitprofil=@abzp, Arbeitsbeginn=@abbeginn, Urlaub=@urlaub, " +
                "Ueberstunden=@überstunden WHERE ID=@id";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("vorname", Mtbtr.Vorname);
            cmd.Parameters.AddWithValue("nachname", Mtbtr.Nachname);
            cmd.Parameters.AddWithValue("abzp", Mtbtr.Arbeitszeitprofil.ID);
            cmd.Parameters.AddWithValue("abbeginn", Mtbtr.Arbeitsbeginn);
            cmd.Parameters.AddWithValue("urlaub", Mtbtr.Urlaub);
            cmd.Parameters.AddWithValue("überstunden", Mtbtr.Überstunden);
            cmd.Parameters.AddWithValue("id", Mtbtr.ID);

            return ExecuteNonQuery(cmd);
        }
        public static int UpdateFingerprintRFID(ClsFingerprintRFID fingerprintRFID)
        {
            string query = "UPDATE fingerprintrfid SET Fingerprint=@finger, RFIDUID=@uid WHERE ID=@id";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("finger", fingerprintRFID.Fingerprint);
            cmd.Parameters.AddWithValue("uid", fingerprintRFID.RFIDUID);
            cmd.Parameters.AddWithValue("id", fingerprintRFID.ID);

            return ExecuteNonQuery(cmd);
        }
        public static int UpdateAusgewerteterTag(DateTime date, int Mitarbeiternummer, TimeSpan Arbeitszeit, int Status)
        {
            string query = "UPDATE ausgewertete_tage SET Datum=@date, Mitarbeiternummer=@mbtrnr, Arbeitszeit=@abzeit, Status=@status WHERE Datum=@date AND Mitarbeiternummer=@mbtrnr";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("date", date);
            cmd.Parameters.AddWithValue("mbtrnr", Mitarbeiternummer);
            cmd.Parameters.AddWithValue("abzeit", Arbeitszeit);
            cmd.Parameters.AddWithValue("status", Status);

            return ExecuteNonQuery(cmd);
        }
        public static int UpdateAusgewerteterTag(ClsAusgewerteter_Tag tag)
        {
            string query = "UPDATE ausgewertete_tage SET Datum=@date, Mitarbeiternummer=@mbtrnr, Arbeitszeit=@abzeit, Status=@status WHERE ID=@id";

            MySqlCommand cmd = new MySqlCommand(query);
            cmd.Parameters.AddWithValue("date", tag.Date);
            cmd.Parameters.AddWithValue("mbtrnr", tag.MitarbeiterNummer);
            cmd.Parameters.AddWithValue("abzeit", tag.Arbeitszeit);
            cmd.Parameters.AddWithValue("status", tag.Status);
            cmd.Parameters.AddWithValue("id", tag.ID);

            return ExecuteNonQuery(cmd);
        }

        public static int DeleteBuchung(ClsBuchung buchung, string Table)
        {
            string query = "DELETE FROM " + Table + " WHERE Buchungsnummer=" + buchung.Buchungsnummer;

            return ExecuteNonQuery(query);
        }
        public static int DeleteTag(ClsTag tag)
        {
            string query = "DELETE FROM tage WHERE ID=" + tag.ID;

            return ExecuteNonQuery(query);
        }
        public static int DeleteArbeitszeitprofil(ClsArbeitsprofil abzp)
        {
            string query = "DELETE FROM arbeitszeitprofile WHERE ID=" + abzp.ID;

            return ExecuteNonQuery(query);
        }
        public static int DeleteMitarbeiter(ClsMitarbeiter mtbtr)
        {
            string query = "DELETE FROM mitarbeiter WHERE ID=" + mtbtr.ID;

            //Alle Buchungen des Mitarbeiters löschen
            List<ClsAusgewerteter_Tag> ausgewTage = SelectAusgewerteteTage(mtbtr);
            List<ClsBuchung> buchungen_temp = SelectAllBuchungenFromMtbtr(mtbtr, "buchungen_temp");
            List<ClsBuchung> buchungen = SelectAllBuchungenFromMtbtr(mtbtr, "buchungen");

            foreach(ClsAusgewerteter_Tag tag in ausgewTage)
            {
                DeleteAusgewerteterTag(tag);
            }
            foreach(ClsBuchung buchung in buchungen_temp)
            {
                DeleteBuchung(buchung, "buchungen_temp");
            }
            foreach(ClsBuchung buchung in buchungen)
            {
                DeleteBuchung(buchung, "buchungen");
            }

            int Result = ExecuteNonQuery(query);
            Result += DeleteFingerprintRFID(mtbtr.Mitarbeiternummer);

            return Result;
        }
        public static int DeleteFingerprintRFID(ClsFingerprintRFID fingerprintRFID)
        {
            string query = "DELETE FROM fingerprintrfid WHERE ID=" + fingerprintRFID.ID;

            return ExecuteNonQuery(query);
        }
        public static int DeleteFingerprintRFID(int Fingerprint)
        {
            string query = "DELETE FROM fingerprintrfid WHERE Fingerprint=" + Fingerprint;

            return ExecuteNonQuery(query);
        }
        public static int DeleteAusgewerteterTag(ClsAusgewerteter_Tag tag)
        {
            string query = "DELETE FROM ausgewertete_tage WHERE ID=" + tag.ID;

            return ExecuteNonQuery(query);
        }

        public static void Log(string log)
        {
            string name = @"Logs\logs" + DateTime.Now.Date.ToString();
            StreamWriter sw;
            if (File.Exists(name))
            {
                sw = new StreamWriter(name, true);
            }
            else
            {
                sw = new StreamWriter(name, false);
            }

            sw.WriteLine(log);

            sw.Close();
            sw.Dispose();
        }
        /// <summary>
        /// Speichert die MitarbeiterID und den zugehörigen Tag, wann der Urlaub berechnet werden soll
        /// </summary>
        /// <param name="strings">Liste aus MtbtrID;Urlaubstag</param>
        public static void WriteDateToCSV(List<string> strings)
        {
            using (StreamWriter sw = new StreamWriter(@"Resources\date.csv", false))
            {
                foreach(string s in strings)
                {
                    sw.WriteLine(s);
                }
            }
        }
        /// <summary>
        /// Liest Daten von voriger Funktion wieder aus
        /// </summary>
        /// <param name="IDs"></param>
        /// <returns></returns>
        public static List<DateTime> ReadDateFromCSV(ref List<int> IDs)
        {
            List<DateTime> list = new List<DateTime>();
            using(StreamReader sr = new StreamReader(@"Resources\date.csv"))
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

        public static void WriteBerechnungsDateToCSV(DateTime date)
        {
            using (StreamWriter sw = new StreamWriter(@"Resources\Berechnungsdate.csv", false))
            {
                sw.WriteLine(date.ToString());
            }
        }

        public static DateTime ReadBerechnungsDateFromCSV()
        {
            DateTime date = new DateTime();
            if (File.Exists(@"Resources\Berechnungsdate.csv"))
            {
                using (StreamReader sr = new StreamReader(@"Resources\Berechnungsdate.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string zeile = sr.ReadLine();

                        date = Convert.ToDateTime(zeile);
                    }
                }
            }
            return date;
        }

        //Helper Funktionen
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
        private static Buchungstyp StringToBuchungstyp(string bt)
        {
            switch (bt)
            {
                case "Kommen":
                    return Buchungstyp.Kommen;
                default: //Wenn Kommen rausgefilter muss der Rest "Gehen" sein
                    return Buchungstyp.Gehen;
            }
        }
        public static int ExecuteNonQuery(string query)
        {
            int result = 0;

            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                result = cmd.ExecuteNonQuery();

                cmd.Dispose();
            }

            return result;
        }
        public static int ExecuteNonQuery(MySqlCommand cmd)
        {
            int result = 0;

            using (MySqlConnection conn = new MySqlConnection(m_connectionString))
            {
                conn.Open();

                cmd.Connection = conn;
                result = cmd.ExecuteNonQuery();

                cmd.Dispose();
            }

            return result;
        }

        public static string SendRecieveHTTP(string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text + "\n\r\n");
            string URL = "http://10.100.128.21/", responseContent;

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
                    using (Stream strm = response.GetResponseStream())
                    {
                        using (StreamReader sr99 = new StreamReader(strm))
                        {
                            responseContent = sr99.ReadToEnd();
                        }
                    }
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
