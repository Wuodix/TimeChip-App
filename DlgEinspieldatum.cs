using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeChip_App
{
    public partial class DlgEinspieldatum : Form
    {
        public DlgEinspieldatum()
        {
            InitializeComponent();
        }

        public DateTime Einspieldatum { get { return m_dtpEinspieldatum.Value.Date; } }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            if(m_dtpEinspieldatum.Value.CompareTo(DateTime.Now) > 0)
            {
                MessageBox.Show("Das ausgewählte Datum liegt in der Zukunft! Es können Änderungen leider nur in der Vergangenheit eingespielt werden.", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
