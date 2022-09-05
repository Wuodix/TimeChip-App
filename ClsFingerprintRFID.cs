using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChip_App_1._0
{
    internal class ClsFingerprintRFID
    {
        private string m_RFIDUID;
        private int m_FingerprintID, m_ID;

        public ClsFingerprintRFID(int ID, string rfidUID, int fingerprintID)
        {
            m_ID = ID;
            m_RFIDUID = rfidUID;
            m_FingerprintID = fingerprintID;
        }

        public string RFIDUID { get { return m_RFIDUID; } set { m_RFIDUID = value; } }
        public int Fingerprint { get { return m_FingerprintID; } set { m_FingerprintID = value; } }
        public int ID { get { return m_ID; } set { m_ID = value; } }
    }
}
