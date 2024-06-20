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
    public partial class DlgFingerName : Form
    {
        public DlgFingerName()
        {
            InitializeComponent();
        }

        public string FingerName { get { return m_tbxFingerName.Text; } }
    }
}
