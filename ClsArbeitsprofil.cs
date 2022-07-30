using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace TimeChip_App_1._0
{
    public class ClsArbeitsprofil
    {
        public bool m_gleitzeit;
        public int m_urlaub, m_ID;
        private static int m_zaehler = 1;
        public ClsTag m_Montag, m_Dienstag, m_Mittwoch, m_Donnerstag, m_Freitag, m_Samstag, m_Sonntag;
        private string m_name;

        public ClsArbeitsprofil(string Name, ClsTag Montag, ClsTag Dienstag, ClsTag Mittwoch, ClsTag Donnerstag, ClsTag Freitag, ClsTag Samstag, ClsTag Sonntag, int Urlaub, bool Gleitzeit)
        {
            m_name = Name;
            m_Montag = Montag;
            m_Dienstag = Dienstag;
            m_Mittwoch = Mittwoch;
            m_Donnerstag = Donnerstag;
            m_Freitag = Freitag;
            m_Samstag = Samstag;
            m_Sonntag = Sonntag;
            m_urlaub = Urlaub;
            m_gleitzeit = Gleitzeit;

            m_ID = m_zaehler;
            m_zaehler++;
        }

        public string Name { get { return m_name; } set { m_name = value; } }
        public bool Gleitzeit { get { return m_gleitzeit; } set { m_gleitzeit = value; } }
        public int Urlaub { get { return m_urlaub; } set { m_urlaub = value; } }
        public ClsTag Montag { get { return m_Montag; } set { m_Montag = value; } }
        public ClsTag Dienstag { get { return m_Dienstag; } set { m_Dienstag = value; } }
        public ClsTag Mittwoch { get { return m_Mittwoch; } set { m_Mittwoch = value; } }
        public ClsTag Donnerstag { get { return m_Donnerstag; } set { m_Donnerstag = value; } }
        public ClsTag Freitag { get { return m_Freitag; } set { m_Freitag = value; } }
        public ClsTag Samstag { get { return m_Samstag; } set { m_Samstag = value; } }
        public ClsTag Sonntag { get { return m_Sonntag; } set { m_Sonntag = value; } }
        public int ID { get { return m_ID; } set { m_ID = value; } }
        public override string ToString()
        {
            return m_name;
        }
    }
}
