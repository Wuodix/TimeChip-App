using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeChip_App_1._0
{
    public class ClsFingerprintRFID
    {
        private string m_rFIDUID, m_fingerName;
        private int m_fingerprintID, m_iD, m_mtbtrID;

        public ClsFingerprintRFID(int ID, string rfidUID, int fingerprintID, int mtbtrID, string fingerName)
        {
            m_iD = ID;
            m_rFIDUID = rfidUID;
            m_fingerprintID = fingerprintID;
            m_mtbtrID = mtbtrID;
            m_fingerName = fingerName;
        }

        public string RFIDUID { get { return m_rFIDUID; } set { m_rFIDUID = value; } }
        public string FingerName { get { return m_fingerName; } set { m_fingerName = value; } }
        public int Fingerprint { get { return m_fingerprintID; } set { m_fingerprintID = value; } }
        public int ID { get { return m_iD; } set { m_iD = value; } }
        public int MtbtrID { get {  return m_mtbtrID; } set { m_mtbtrID = value; } } 

        public string ToFinger()
        {
            return m_fingerprintID + " - " + m_fingerName;
        }
    }
}
