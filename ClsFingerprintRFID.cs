namespace TimeChip_App
{
    public class ClsFingerprintRFID
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
