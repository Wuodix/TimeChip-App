using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChip_App
{
    public class ClsAbzpMtbtr
    {
        private readonly int m_id;
        private int m_abzpID, m_mtbtrID;
        private DateTime m_startdate, m_enddate;

        public ClsAbzpMtbtr(int ID, int AbzpID, int MtbtrID, DateTime Startdate, DateTime Enddate)
        {
            m_id = ID;
            m_abzpID = AbzpID;
            m_mtbtrID = MtbtrID;
            m_startdate = Startdate;
            m_enddate = Enddate;
        }

        public int ID { get { return m_id; }}
        public int AbzpID { get { return m_abzpID; } set { m_abzpID = value; } }
        public int MtbtrID { get { return m_mtbtrID; } set { m_mtbtrID = value; } }
        public DateTime Startdate { get { return m_startdate; } set { m_startdate = value; } }
        public DateTime Enddate { get { return m_enddate; } set { m_enddate = value; } }

        public override string ToString()
        {
            return ID.ToString() + " AbzpID: " + AbzpID.ToString() + " MtbtrID: " + MtbtrID.ToString();
        }
    }
}
