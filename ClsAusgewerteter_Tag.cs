using System;

namespace TimeChip_App
{
    public class ClsAusgewerteter_Tag
    {
        int m_id, m_mtbtrID, m_status; //Status: 0 = Aus Überstunden abziehen, 1 = Krank, 2 = Schule, 3 = Urlaub
        TimeSpan m_arbeitszeit;
        DateTime m_date;

        public ClsAusgewerteter_Tag(int id, int mtbtrID, TimeSpan arbeitszeit, DateTime date, int status)
        {
            m_id = id;
            m_mtbtrID = mtbtrID;
            m_arbeitszeit = arbeitszeit;
            m_date = date;
            m_status = status;
        }

        public ClsAusgewerteter_Tag()
        {
            //Wird gebraucht für die Zuweisung eines Werts zur Variable "tag" in ClsBerechnung.Berechnen(DateTime,ClsMitarbeiter,bool)
        }

        public int ID { get { return m_id; } set { m_id = value; } }
        public int MtbtrID { get { return m_mtbtrID; } set { m_mtbtrID = value; } }
        public TimeSpan Arbeitszeit { get { return m_arbeitszeit; } set { m_arbeitszeit = value; } }
        public DateTime Date { get { return m_date; } set { m_date = value; } }
        public int Status { get { return m_status; } set { m_status = value; } }
    }
}
