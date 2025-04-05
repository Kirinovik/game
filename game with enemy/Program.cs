using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Запуск приложения с формой MainMenu
            Application.Run(new MainMenu());
        }
    }
}
