using System.Diagnostics;
using System.Security.Principal;

namespace SimpleCalculator
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (IsAdministrator())
            {
                if (!EventLog.SourceExists("SimpleCalculator"))
                {
                    EventLog.CreateEventSource("SimpleCalculator", "Application");
                }
            }
            else
            {
                MessageBox.Show("Aplikacja musi być uruchomiona jako administrator, aby zapisywać zdarzenia w dzienniku zdarzeń.", "Brak uprawnień", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}