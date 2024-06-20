using System;

namespace TimeChip_App
{
    public class ClsArbeitsprofil
    {
        private bool m_gleitzeit, m_ruhestand;
        private readonly int m_id;
        private ClsTag m_montag, m_dienstag, m_mittwoch, m_donnerstag, m_freitag, m_samstag, m_sonntag;
        private string m_name;

        public ClsArbeitsprofil(int ID, string Name, ClsTag Montag, ClsTag Dienstag, ClsTag Mittwoch, ClsTag Donnerstag, ClsTag Freitag, ClsTag Samstag, ClsTag Sonntag, bool Gleitzeit, bool Ruhestand)
        {
            m_name = Name;
            m_montag = Montag;
            m_dienstag = Dienstag;
            m_mittwoch = Mittwoch;
            m_donnerstag = Donnerstag;
            m_freitag = Freitag;
            m_samstag = Samstag;
            m_sonntag = Sonntag;
            m_gleitzeit = Gleitzeit;
            m_id = ID;
            m_ruhestand = Ruhestand;
        }

        public string Name { get { return m_name; } set { m_name = value; } }
        public bool Gleitzeit { get { return m_gleitzeit; } set { m_gleitzeit = value; } }
        public bool Ruhestand { get { return m_ruhestand; } set { m_ruhestand = value; } }
        public ClsTag Montag { get { return m_montag; } set { m_montag = value; } }
        public ClsTag Dienstag { get { return m_dienstag; } set { m_dienstag = value; } }
        public ClsTag Mittwoch { get { return m_mittwoch; } set { m_mittwoch = value; } }
        public ClsTag Donnerstag { get { return m_donnerstag; } set { m_donnerstag = value; } }
        public ClsTag Freitag { get { return m_freitag; } set { m_freitag = value; } }
        public ClsTag Samstag { get { return m_samstag; } set { m_samstag = value; } }
        public ClsTag Sonntag { get { return m_sonntag; } set { m_sonntag = value; } }
        public int ID { get { return m_id; } }


        public override string ToString()
        {
            return ID + " "  + m_name;
        }

        public string Log() { return ToString(); }
    }
}
