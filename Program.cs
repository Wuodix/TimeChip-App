using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TimeChip_App_1._0
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DlgArbeitszeitprofile load = new DlgArbeitszeitprofile();
            load.Show();
            load.Close();
            DlgTag load2 = new DlgTag();
            load2.Show();
            load2.Close();
            Application.Run(new FrmHaupt());
        }
    }
}
