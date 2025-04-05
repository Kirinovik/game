using System;
using System.Windows.Forms;

namespace BaldaGame
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Запускаем форму главного меню
            Application.Run(new MainMenuForm());
        }
    }
}