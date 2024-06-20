using System;
using System.Windows.Forms;
using System.Configuration;
using TimeChip_App.Properties;
using System.Reflection.Emit;
using System.Threading;

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
            Application.ThreadException += new ThreadExceptionEventHandler(ExceptionHandler);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandler);

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
            }
            catch (MySql.Data.MySqlClient.MySqlException)
            {
                MessageBox.Show("App kann sich nicht zum Server verbinden! Bitte überprüfen Sie umgehend die Netzwerkverbindung und die Einstellungen, da die App andernfalls nicht richtig funktioniert!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Application.Run(new FrmHaupt());
        }


        static void ExceptionHandler(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Handler caught: " + e.Exception.Message);
            DataProvider.Log("Handler caught: " + e.Exception.Message, 0);
        }

        static void ExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Domain Handler caught: " + (e.ExceptionObject as Exception).Message);
            DataProvider.Log("Handler caught: " + (e.ExceptionObject as Exception).Message, 0);
        }
    }
}
