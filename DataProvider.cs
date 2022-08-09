using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace TimeChip_App_1._0
{
    internal class DataProvider
    {
        private MySqlConnection m_conn;

        public bool OpenConnection()
        {
            string connectionstring = "SERVER=localhost;DATABASE=apotheke_time_chip;UID=root;PASSWORD=;";

            m_conn = new MySqlConnection(connectionstring);

            try
            {
                m_conn.Open();
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }
        public bool CloseConnection()
        {
            try
            {
                m_conn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Insert(string query)
        {
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, m_conn);
                cmd.ExecuteNonQuery();
                CloseConnection();
            }

            //IDs dann nach IDs aus Database setzten
        }

        public void InsertBuchung(ClsBuchung bug)
        {
            string query = "INSERT INTO buchungen (Buchungstyp, Zeit, Mitarbeiternummer) VALUES" +
                "(" + bug.Buchungstyp + ", " + bug.Zeit + ", " + bug.Mitarbeiternummer + ")";

            Insert(query);
        }
        public void InsertTag(ClsTag tag)
        {
            string query = "INSERT INTO tage (Name, Arbeitsbeginn, Arbeitsende, Arbeitszeit, Pausenbeginn, Pausenende, Pausendauer) VALUES" +
                "(" + tag.Name + ", " + tag.Arbeitsbeginn + ", " + tag.Arbeitsende + ", " + tag.Arbeitszeit + ", " + tag.Pausenbeginn + ", " + tag.Pausenende + ", " + tag.Pausendauer + ")";
            
            Insert(query);
        }
        public void InsertArbeitszeitprofil(ClsArbeitsprofil abzp)
        {
            string query = "INSERT INTO arbeitszeitprofile (Name, Montag, Dienstag, Mittwoch, Donnerstag, Freitag, Samstag, Sonntag, Urlaub, Gleitzeit)" + 
                "VALUES(" + abzp.Name + ", " + abzp.Montag + ", " + abzp.Dienstag + ", " + abzp.Mittwoch + ", " + abzp.Donnerstag + ", " + abzp.Freitag + ", " + 
                abzp.Samstag + ", " + abzp.Sonntag + ", " + abzp.Urlaub + ", " + abzp.Gleitzeit + ")";
            Insert(query);
        }
        public void InsertMitarbeiter(ClsMitarbeiter mtabtr)
        {
            string query = "INSERT INTO mitarbeiter (Vorname, Nachname, Arbeitsbeginn, Überstunden, Arbeitszeitprofil) VALUES(" +
                mtabtr.Vorname + ", " + mtabtr.Nachname + ", " + mtabtr.Arbeitsbeginn + ", " + mtabtr.Überstunden + ", " + mtabtr.Arbeitszeitprofil + ")";
            Insert(query);
        }

        public List<ClsBuchung> SelectAllBuchungen()
        {
            List<ClsBuchung> list = new List<ClsBuchung>();
            if (OpenConnection())
            {
                string query = "SELECT * FROM buchungen";

                MySqlCommand cmd = new MySqlCommand(query, m_conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsBuchung buchung = new ClsBuchung(reader.GetInt16("Buchungsnummer"), reader.GetInt16("Mitarbeiternummer"), StringToDateTime(reader.GetString("Zeit")), StringToBuchungstyp(reader.GetString("Buchungstyp")));
                    list.Add(buchung);
                }

                cmd.Dispose();
                reader.Close();
                CloseConnection();
            }
            return list;
        }
        public List<ClsTag> SelectAllTage()
        {
            List<ClsTag> list = new List<ClsTag>();

            if (OpenConnection())
            {
                string query = "SELECT * FROM tage";

                MySqlCommand cmd = new MySqlCommand(query, m_conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ClsTag Tag = new ClsTag(reader.GetInt16("ID"), reader.GetString("Name"), reader.GetTimeSpan("Arbeitsbeginn"),
                        reader.GetTimeSpan("Arbeitsende"), reader.GetTimeSpan("Pausenbeginn"),
                        reader.GetTimeSpan("Pausenende"), reader.GetTimeSpan("Arbeitszeit"),
                        reader.GetTimeSpan("Pausendauer"));
                    list.Add(Tag);
                }

                cmd.Dispose();
                reader.Close();
                CloseConnection();
            }

            return list;
        }
        public List<ClsArbeitsprofil> SelectAllArbeitszeitprofil()
        {
            List<ClsArbeitsprofil> list = new List<ClsArbeitsprofil>();

            if (OpenConnection())
            {
                string query = "SELECT * FROM arbeitszeitprofile";

                MySqlCommand cmd = new MySqlCommand(query, m_conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<ClsTag> Tage = SelectAllTage();
                while (reader.Read())
                {
                    ClsArbeitsprofil arbeitsprofil = new ClsArbeitsprofil(reader.GetInt16("ID"), reader.GetString("Name"),
                        Tage.Find(x => x.ID == reader.GetInt16("Montag")), Tage.Find(x => x.ID == reader.GetInt16("Dienstag")),
                        Tage.Find(x => x.ID == reader.GetInt16("Mittwoch")), Tage.Find(x => x.ID == reader.GetInt16("Donnerstag")),
                        Tage.Find(x => x.ID == reader.GetInt16("Freitag")), Tage.Find(x => x.ID == reader.GetInt16("Samstag")),
                        Tage.Find(x => x.ID == reader.GetInt16("Sonntag")), reader.GetInt16("Urlaub"), reader.GetBoolean("Gleitzeit"));
                    list.Add(arbeitsprofil);
                }

                cmd.Dispose();
                reader.Close();
                CloseConnection();
            }

            return list;
        }
        public List<ClsMitarbeiter> SelectAllMitarbeiter()
        {
            List<ClsMitarbeiter> list = new List<ClsMitarbeiter>();

            if (OpenConnection())
            {
                string query = "SELECT * FROM mitarbeiter";

                MySqlCommand cmd = new MySqlCommand(query, m_conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                List<ClsArbeitsprofil> abzt = SelectAllArbeitszeitprofil();
                while (reader.Read())
                {
                    ClsMitarbeiter mitarbeiter = new ClsMitarbeiter(reader.GetInt16("Mitarbeiternummer"),
                        reader.GetString("Vorname"), reader.GetString("Nachname"),
                        abzt.Find(x => x.ID == reader.GetInt16("Arbeitszeitprofil")), reader.GetDateTime("Arbeitsbeginn"),
                        reader.GetTimeSpan("Urlaub"), reader.GetTimeSpan("Überstunden"));
                    list.Add(mitarbeiter);
                }

                cmd.Dispose();
                reader.Close();
                CloseConnection();
            }

            return list;
        }

        public int Update(string query)
        {
            int result = 0;

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, m_conn);
                result = cmd.ExecuteNonQuery();

                cmd.Dispose();
                CloseConnection();
            }

            return result;
        }

        public int UpdateBuchung(ClsBuchung newBuchung, ClsBuchung oldBuchung)
        {
            string query = "UPDATE buchungen SET Mitarbeiternummer=" + newBuchung.Mitarbeiternummer + ", Zeit=" +
                newBuchung.Zeit.ToString("dd.MM.yy HH:mm:ss") + ", Buchungstyp=" + newBuchung.Buchungstyp.ToString() +
                " WHERE Buchungsnummer=" + oldBuchung.Buchungsnummer;

            return Update(query);
        }
        public int UpdateTag(ClsTag newTag, ClsTag oldTag)
        {
            string query = "UPDATE tage SET Name=" + newTag.Name + ", Arbeitsbeginn=" + newTag.Arbeitsbeginn +
                ", Arbeitsende=" + newTag.Arbeitsende + ", Pausenbeginn=" + newTag.Pausenbeginn +
                ", Pausenende=" + newTag.Pausenende + ", Arbeitszeit=" + newTag.Arbeitszeit +
                ", Pausendauer=" + newTag.Pausendauer + " WHERE ID=" + oldTag.ID;

            return Update(query);
        }
        public int UpdateArbeitszeitprofil(ClsArbeitsprofil newAbzp, ClsArbeitsprofil oldAbzp)
        {
            string query = "UPDATE arbeitszeitprofile SET Name=" + newAbzp.Name + ", Montag=" + newAbzp.Montag +
                ", Dienstag=" + newAbzp.Dienstag + ", Mittwoch=" + newAbzp.Mittwoch + ", Donnerstag=" + newAbzp.Donnerstag +
                ", Freitag=" + newAbzp.Freitag + ", Samstag=" + newAbzp.Samstag + ", Sonntag=" + newAbzp.Sonntag +
                ", Urlaub=" + newAbzp.Urlaub + ", Gleitzeit=" + newAbzp.Gleitzeit;

            return Update(query);
        }
        public int UpdateMitarbeiter(ClsMitarbeiter newMtbtr, ClsMitarbeiter oldMtbtr)
        {
            return 1;
        }


        //Helper Funktionen
        private DateTime StringToDateTime(string dt)
        {
            //"%d.%m.%Y %H:%M:%S"

            string[] split1 = dt.Split('.');
            string Day = split1[0];
            string Month = split1[1];
            string[] split2 = split1[2].Split(' ');
            string Year = split2[0];
            string[] split3 = split2[1].Split(':');
            string Hour = split3[0];
            string Minute = split3[1];
            string Second = split3[2];

            Debug.WriteLine(Day);
            Debug.WriteLine(Month);
            Debug.WriteLine(Year);
            Debug.WriteLine(Hour);
            Debug.WriteLine(Minute);
            Debug.WriteLine(Second);

            int day = Convert.ToInt32(Day);
            int month = Convert.ToInt32(Month);
            int year = Convert.ToInt32(Year);
            int hour = Convert.ToInt32(Hour);
            int minute = Convert.ToInt32(Minute);
            int second = Convert.ToInt32(Second);

            return new DateTime(year, month, day, hour, minute, second);
        }
        private Buchungstyp StringToBuchungstyp(string bt)
        {
            switch (bt)
            {
                case "Kommen":
                    return Buchungstyp.Kommen;
                default: //Wenn Kommen rausgefilter muss der Rest "Gehen" sein
                    return Buchungstyp.Gehen;
            }
        }
    }
}
