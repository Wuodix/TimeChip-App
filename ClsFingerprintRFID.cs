namespace TimeChip_App
{
    public class ClsFingerprintRFID
    {
        private string m_rFIDUID;
        private int m_fingerprintID, m_iD;

        public ClsFingerprintRFID(int ID, string rfidUID, int fingerprintID)
        {
            m_iD = ID;
            m_rFIDUID = rfidUID;
            m_fingerprintID = fingerprintID;
        }

        public string RFIDUID { get { return m_rFIDUID; } set { m_rFIDUID = value; } }
        public int Fingerprint { get { return m_fingerprintID; } set { m_fingerprintID = value; } }
        public int ID { get { return m_iD; } set { m_iD = value; } }
    }
}
