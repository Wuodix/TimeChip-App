using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChip_App_1._0
{
    public class ClsAusgewerteter_Tag
    {
        int m_id, m_mitarbeiternummer, m_status; //Status: 0 = Aus Überstunden abziehen, 1 = Nichts tun/Entschuldigt, 2 = Urlaub
        TimeSpan m_arbeitszeit;
        DateTime m_date;

        public ClsAusgewerteter_Tag(int id, int mitarbeiternummer, TimeSpan arbeitszeit, DateTime date, int status)
        {
            m_id = id;
            m_mitarbeiternummer = mitarbeiternummer;
            m_arbeitszeit = arbeitszeit;
            m_date = date;
            m_status = status;
        }

        public int ID { get { return m_id; } set { m_id = value; } }
        public int MitarbeiterNummer { get { return m_mitarbeiternummer; } set { m_mitarbeiternummer = value; } }
        public TimeSpan Arbeitszeit { get { return m_arbeitszeit; } set { m_arbeitszeit = value; } }
        public DateTime Date { get { return m_date; } set { m_date = value; } }
        public int Status { get { return m_status; } set { m_status = value; } }
    }
}
