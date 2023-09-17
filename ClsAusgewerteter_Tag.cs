using System;

namespace TimeChip_App
{
    public class ClsAusgewerteter_Tag
    {
        int m_id, m_mitarbeiternummer, m_status; //Status: 0 = Aus Überstunden abziehen, 1 = Krank, 2 = Schule, 3 = Urlaub
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

        public ClsAusgewerteter_Tag()
        {
            //Wird gebraucht für die Zuweisung eines Werts zur Variable "tag" in ClsBerechnung.Berechnen(DateTime,ClsMitarbeiter,bool)
        }

        public int ID { get { return m_id; } set { m_id = value; } }
        public int MitarbeiterNummer { get { return m_mitarbeiternummer; } set { m_mitarbeiternummer = value; } }
        public TimeSpan Arbeitszeit { get { return m_arbeitszeit; } set { m_arbeitszeit = value; } }
        public DateTime Date { get { return m_date; } set { m_date = value; } }
        public int Status { get { return m_status; } set { m_status = value; } }
    }
}
