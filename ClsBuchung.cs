using System;

namespace TimeChip_App
{
    public enum Buchungstyp
    {
        Kommen,
        Gehen
    }

    public class ClsBuchung
    {
        private readonly int m_buchungsnummer;
        private int m_mtbtrID;
        private DateTime m_zeit;
        private Buchungstyp m_buchungstyp;

        public ClsBuchung(int buchungsnummer, int mtbtrID, DateTime zeit, Buchungstyp buchungstyp)
        {
            m_buchungsnummer = buchungsnummer;
            m_mtbtrID = mtbtrID;
            m_zeit = zeit;
            m_buchungstyp = buchungstyp;
        }

        public int Buchungsnummer { get { return m_buchungsnummer; }}
        public int MtbtrID { get { return m_mtbtrID; } set { m_mtbtrID = value; } }
        public DateTime Zeit { get { return m_zeit; } set { m_zeit = value; } }
        public Buchungstyp Buchungstyp { get { return m_buchungstyp; } set { m_buchungstyp = value; } }

        public override string ToString()
        {
            return Zeit.TimeOfDay.ToString(@"hh\:mm") + " - " + Buchungstyp.ToString();
        }
    }
}
