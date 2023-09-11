using System;
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

            try
            {
                DataProvider.GetConnectionString();
                DlgArbeitszeitprofile load = new DlgArbeitszeitprofile();
                load.Show();
                load.Close();
                DlgTag load2 = new DlgTag();
                load2.Show();
                load2.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("App kann sich nicht zum Server verbinden! Bitte überprüfen Sie umgehend die Netzwerkverbindung und die Einstellungen, da die App andernfalls nicht richtig funktioniert!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Run(new FrmHaupt());
        }
    }
}
