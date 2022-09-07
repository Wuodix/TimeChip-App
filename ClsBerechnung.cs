using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeChip_App_1._0
{
    public class ClsBerechnung
    {
        static DateTime m_nächsteBerechnung;
        static DateTime m_letzteBerechnung;

        public static DateTime NächsteBerechnung { get { return m_nächsteBerechnung; } set { m_nächsteBerechnung = value; } }

        public static void Berechnung(bool forced)
        {
            if (m_nächsteBerechnung.CompareTo(DateTime.Now) < 0 || forced)
            {
                Debug.WriteLine("BERECHNUNG!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                
                List<ClsBuchung> buchungen = DataProvider.SelectAllBuchungen("buchungen_temp");

                foreach (ClsBuchung buchung in buchungen)
                {
                    DataProvider.InsertBuchung(buchung.Buchungstyp, buchung.Zeit, buchung.Mitarbeiternummer, "buchungen");
                    DataProvider.DeleteBuchung(buchung, "buchungen_temp");
                }

                m_letzteBerechnung = DateTime.Now;
                m_nächsteBerechnung = m_letzteBerechnung.AddDays(1);
            }
        }
    }
}
