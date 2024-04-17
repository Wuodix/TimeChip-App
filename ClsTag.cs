using System;

namespace TimeChip_App
{
    public class ClsTag
    {
        private TimeSpan m_arbeitsbeginn, m_arbeitsende, m_pausenbeginn, m_pausenende, m_arbeitszeit, m_pausendauer;
        private int m_id;
        private bool m_pause;

        public ClsTag(int ID, TimeSpan Arbeitsbeginn, TimeSpan Arbeitsende, TimeSpan Arbeitszeit, bool Pause, TimeSpan Pausenbeginn, TimeSpan Pausenende, TimeSpan Pausendauer)
        {
            m_arbeitsbeginn = Arbeitsbeginn;
            m_arbeitsende = Arbeitsende;
            m_pausenbeginn = Pausenbeginn;
            m_pausenende = Pausenende;
            m_arbeitszeit = Arbeitszeit;
            m_pausendauer = Pausendauer;
            m_id = ID;
            m_pause = Pause;
        }

        public TimeSpan Arbeitsbeginn { get { return m_arbeitsbeginn; } set { m_arbeitsbeginn = value; } }
        public TimeSpan Arbeitsende { get { return m_arbeitsende; } set { m_arbeitsende = value; } }
        public TimeSpan Pausenbeginn { get { return m_pausenbeginn; } set { m_pausenbeginn = value; } }
        public TimeSpan Pausenende { get { return m_pausenende; } set { m_pausenende = value; } }
        public TimeSpan Arbeitszeit { get { return m_arbeitszeit; } set { m_arbeitszeit = value; } }
        public TimeSpan Pausendauer { get { return m_pausendauer; } set { m_pausendauer = value; } }
        public int ID { get { return m_id; } set { m_id = value; } }
        public bool Pause { get { return m_pause; } set { m_pause = value; } }
    }
}
