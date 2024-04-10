using System;

namespace TimeChip_App
{
    public class ClsMitarbeiter
    {
        int m_id, m_mitarbeiternummmer;
        private string m_vorname, m_nachname;
        private DateTime m_arbeitsbeginn;
        private TimeSpan m_überstunden, m_urlaub;
        private ClsArbeitsprofil m_arbeitszeitprofil;

        /// <summary>
        /// Wird in Cls Berechnung gebraucht um keinen vollständigen Mitarbeiter erstellen zu müssen
        /// </summary>
        public ClsMitarbeiter() { }
        public ClsMitarbeiter(int ID, int Mitarbeiternummer, string Vorname, string Nachname, ClsArbeitsprofil Arbeitszeitprofil, DateTime Arbeitsbeginn, TimeSpan Urlaub, TimeSpan Überstunden)
        {
            m_id = ID;
            m_mitarbeiternummmer = Mitarbeiternummer;
            m_vorname = Vorname;
            m_nachname = Nachname;
            m_arbeitszeitprofil = Arbeitszeitprofil;
            m_arbeitsbeginn = Arbeitsbeginn;
            m_urlaub = Urlaub;
            m_überstunden = Überstunden;
        }

        public string Vorname { get { return m_vorname; } set { m_vorname = value; } }
        public string Nachname { get { return m_nachname; } set { m_nachname = value; } }
        public ClsArbeitsprofil Arbeitszeitprofil { get { return m_arbeitszeitprofil; } set { m_arbeitszeitprofil = value; } }
        public TimeSpan Überstunden { get { return m_überstunden; } set { m_überstunden = value; } }
        public TimeSpan Urlaub { get { return m_urlaub; } set { m_urlaub = value; } }
        /// <summary>
        /// Tag an dem der Mitarbeiter in der Firma angefangen hat
        /// </summary>
        public DateTime Arbeitsbeginn { get { return m_arbeitsbeginn; } set { m_arbeitsbeginn = value; } }
        /// <summary>
        /// Wird von der Datenbank zugewiesen
        /// </summary>
        public int ID { get { return m_id; } set { m_id = value; } }
        /// <summary>
        /// Wird vom Arduino zugewiesen
        /// </summary>
        public int Mitarbeiternummer { get { return m_mitarbeiternummmer; } set { m_mitarbeiternummmer = value; } }

        public override string ToString()
        {
            return m_mitarbeiternummmer + " - " + m_vorname + " " + m_nachname;
        }

        public string Log()
        {
            return Mitarbeiternummer + " - " + Vorname + " - " + Nachname + " - " + Überstunden + " - " + Urlaub;
        }
    }
}
