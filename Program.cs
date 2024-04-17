using System;
using System.Windows.Forms;
using System.Configuration;
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
                MessageBox.Show(Settings.Default.Server);
            }

            try
            {
                DataProvider.GetConnectionString();
                DlgArbeitszeitprofile load = new DlgArbeitszeitprofile();
                load.Show();
                load.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("App kann sich nicht zum Server verbinden! Bitte überprüfen Sie umgehend die Netzwerkverbindung und die Einstellungen, da die App andernfalls nicht richtig funktioniert!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Run(new FrmHaupt());
        }
    }
}
