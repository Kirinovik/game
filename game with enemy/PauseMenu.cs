using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class PauseMenu : Form
    {
        private Form1 gameForm;

        public PauseMenu(Form1 gameForm)
        {
            InitializeComponent();
            this.gameForm = gameForm;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.Text = "Пауза";
            this.Size = new System.Drawing.Size(300, 450);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.LightBlue;

            Button resumeButton = new Button
            {
                Text = "Вернуться",
                Location = new System.Drawing.Point(50, 50),
                Size = new System.Drawing.Size(200, 40),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            resumeButton.Click += ResumeButton_Click;

            Button restartButton = new Button
            {
                Text = "Перезапустить",
                Location = new System.Drawing.Point(50, 100),
                Size = new System.Drawing.Size(200, 40),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            restartButton.Click += RestartButton_Click;

            Button optionsButton = new Button
            {
                Text = "Управление",
                Location = new System.Drawing.Point(50, 150),
                Size = new System.Drawing.Size(200, 40),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            optionsButton.Click += OptionsButton_Click;

            Button exitToMainMenuButton = new Button
            {
                Text = "Выйти в главное меню",
                Location = new System.Drawing.Point(50, 200),
                Size = new System.Drawing.Size(200, 40),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            exitToMainMenuButton.Click += ExitToMainMenuButton_Click;

            Button exitGameButton = new Button
            {
                Text = "Выйти на рабочий стол",
                Location = new System.Drawing.Point(50, 250),
                Size = new System.Drawing.Size(200, 40),
                Font = new Font("Arial", 12, FontStyle.Bold)
            };
            exitGameButton.Click += ExitGameButton_Click;

            this.Controls.Add(resumeButton);
            this.Controls.Add(restartButton);
            this.Controls.Add(optionsButton);
            this.Controls.Add(exitToMainMenuButton);
            this.Controls.Add(exitGameButton);

            this.ResumeLayout(false);
        }

        private void ResumeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            gameForm.RestartGame();
            this.Close();
        }

        private void OptionsButton_Click(object sender, EventArgs e)
        {
            using (OptionsForm optionsForm = new OptionsForm())
            {
                optionsForm.ShowDialog();
            }
        }

        private void ExitToMainMenuButton_Click(object sender, EventArgs e)
        {
            gameForm.Close();
            this.Close();
        }

        private void ExitGameButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                gameForm.StartGameTimer();
            }
            base.OnFormClosing(e);
        }
    }
}