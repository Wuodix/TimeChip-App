using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChip_App_1._0
{
    public enum Buchungstyp
    {
        Kommen,
        Gehen
    }

    public class ClsBuchung
    {
        private int m_buchungsnummer, m_mitarbeiternummer;
        private DateTime m_zeit;
        private Buchungstyp m_buchungstyp;

        public ClsBuchung(int buchungsnummer, int mitarbeiternummer, DateTime zeit, Buchungstyp buchungstyp)
        {
            m_buchungsnummer = buchungsnummer;
            m_mitarbeiternummer = mitarbeiternummer;
            m_zeit = zeit;
            m_buchungstyp = buchungstyp;
        }

        public int Buchungsnummer { get { return m_buchungsnummer; }}
        public int Mitarbeiternummer { get { return m_mitarbeiternummer; } set { m_mitarbeiternummer = value; } }
        public DateTime Zeit { get { return m_zeit; } set { m_zeit = value; } }
        public Buchungstyp Buchungstyp { get { return m_buchungstyp; } set { m_buchungstyp = value; } }

        public override string ToString()
        {
            return Zeit.TimeOfDay.ToString() + " - " + Buchungstyp.ToString();
        }
    }
}
