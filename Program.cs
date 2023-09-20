using System;
using System.Windows.Forms;
using TimeChip_App.Properties;

namespace TimeChip_App
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

            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
                Settings.Default.Save();
            }

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
