using ZstdSharp.Unsafe;

namespace TimeChip_App
{
    public class ClsFingerprintRFID
    {
        private string m_rFIDUID, m_fingerName;
        private int m_fingerprintID, m_iD, m_mtbtrID;

        public ClsFingerprintRFID(int ID, string rfidUID, int fingerprintID, string FingerName, int MtbtrID)
        {
            m_iD = ID;
            m_rFIDUID = rfidUID;
            m_fingerprintID = fingerprintID;
            m_fingerName = FingerName;
            m_mtbtrID = MtbtrID;
        }

        public string RFIDUID { get { return m_rFIDUID; } set { m_rFIDUID = value; } }
        public string FingerName { get { return m_fingerName; } set { m_fingerName = value; } }
        public int Fingerprint { get { return m_fingerprintID; } set { m_fingerprintID = value; } }
        public int ID { get { return m_iD; } set { m_iD = value; } }
        public int MtbtrID { get { return m_mtbtrID; } set { m_mtbtrID = value; } }
    }
}
