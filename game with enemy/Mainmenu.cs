using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (Form1 gameForm = new Form1())  // Используем using, чтобы форма корректно уничтожалась после закрытия
            {
                gameForm.ShowDialog();
            }
            if (!this.IsDisposed) this.Show(); // Проверяем, не уничтожено ли главное меню перед показом
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (OptionsForm optionsForm = new OptionsForm())
            {
                optionsForm.ShowDialog();
            }
            if (!this.IsDisposed) this.Show();
        }

        private void btnAuthors_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (AuthorsForm authorsForm = new AuthorsForm())
            {
                authorsForm.ShowDialog();
            }
            if (!this.IsDisposed) this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
