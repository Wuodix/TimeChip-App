using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChip_App_1._0
{
    class ClsMitarbeiter
    {
        int m_nummer;
        private static int m_zaehler = 1;
        private string m_vorname, m_nachname;
        private DateTime m_arbeitsbeginn; //Tag an dem der Mitarbeiter in der Firma angefangen hat
        private TimeSpan m_überstunden;
        private ClsArbeitsprofil m_arbeitszeitprofil;

        public ClsMitarbeiter(string Vorname, string Nachname, ClsArbeitsprofil Arbeitszeitprofil, DateTime Arbeitsbeginn)
        {
            m_nummer = m_zaehler;
            m_zaehler++;

            m_vorname = Vorname;
            m_nachname = Nachname;
            m_arbeitszeitprofil = Arbeitszeitprofil;
            m_arbeitsbeginn = Arbeitsbeginn;
        }

        public string Vorname { get { return m_vorname; } set { m_vorname = value; } }
        public string Nachname { get { return m_nachname; } set { m_nachname = value; } }
        public ClsArbeitsprofil Arbeitszeitprofil { get { return m_arbeitszeitprofil; } set { m_arbeitszeitprofil = value; } }
        public TimeSpan Überstunden { get { return m_überstunden; } set { m_überstunden = value; } }
        public DateTime Arbeitsbeginn { get { return m_arbeitsbeginn; } set { m_arbeitsbeginn = value; } }
        public int Nummer { get { return m_nummer; } set { m_nummer = value; } }

        public override string ToString()
        {
            return m_nummer + " - " + m_vorname + " - " + m_nachname;
        }
    }
}
