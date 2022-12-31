using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChip_App_1._0
{
    public class ClsTag
    {
        private TimeSpan m_arbeitsbeginn, m_arbeitsende, m_pausenbeginn, m_pausenende, m_arbeitszeit, m_pausendauer;
        private string m_name;
        private int m_ID;
        private bool m_pause;

        public ClsTag(int ID, string Name, bool Pause, TimeSpan Arbeitsbeginn, TimeSpan Arbeitsende, TimeSpan Pausenbeginn, TimeSpan Pausenende, TimeSpan Arbeitszeit, TimeSpan Pausendauer)
        {
            m_name = Name;
            m_arbeitsbeginn = Arbeitsbeginn;
            m_arbeitsende = Arbeitsende;
            m_pausenbeginn = Pausenbeginn;
            m_pausenende = Pausenende;
            m_arbeitszeit = Arbeitszeit;
            m_pausendauer = Pausendauer;
            m_ID = ID;
            m_pause = Pause;
        }

        public string Name { get { return m_name; } set { m_name = value; } }
        public TimeSpan Arbeitsbeginn { get { return m_arbeitsbeginn; } set { m_arbeitsbeginn = value; } }
        public TimeSpan Arbeitsende { get { return m_arbeitsende; } set { m_arbeitsende = value; } }
        public TimeSpan Pausenbeginn { get { return m_pausenbeginn; } set { m_pausenbeginn = value; } }
        public TimeSpan Pausenende { get { return m_pausenende; } set { m_pausenende = value; } }
        public TimeSpan Arbeitszeit { get { return m_arbeitszeit; } set { m_arbeitszeit = value; } }
        public TimeSpan Pausendauer { get { return m_pausendauer; } set { m_pausendauer = value; } }
        public int ID { get { return m_ID; } set { m_ID = value; } }
        public bool Pause { get { return m_pause; } set { m_pause = value; } }

        public override string ToString()
        {
            return m_name;
        }
    }
}
