using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaldaGame
{
    public partial class MainMenuForm : Form
    {
        private Timer animationTimer; // Таймер для анимации
        private string word = "БАЛДА"; // Слово для анимации
        private int currentIndex = 0; // Текущий индекс буквы
        private Label lblWord; // Метка для отображения слова

        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Главное меню";
            this.Size = new Size(450, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Метка для анимации слова "БАЛДА"
            lblWord = new Label
            {
                Text = "",
                Location = new Point(80, 30),
                AutoSize = true,
                Font = new Font("Comic Sans MS", 50, FontStyle.Bold),
                ForeColor = Color.DimGray,
                BackColor = Color.Transparent
            };
            this.Controls.Add(lblWord);

            // Кнопка "Начать игру"
            Button btnStartSetup = new Button
            {
                Text = "Начать игру",
                Location = new Point(150, 180),
                Width = 150,
                Height = 50,
                Font = new Font("Arial", 12)
            };
            btnStartSetup.Click += BtnStartSetup_Click;

            // Кнопка "Правила игры"
            Button btnRules = new Button
            {
                Text = "Правила игры",
                Location = new Point(150, 250),
                Width = 150,
                Height = 50,
                Font = new Font("Arial", 12)
            };
            btnRules.Click += BtnRules_Click;

            // Кнопка "Выход"
            Button btnExit = new Button
            {
                Text = "Выход",
                Location = new Point(150, 320),
                Width = 150,
                Height = 50,
                Font = new Font("Arial", 12)
            };
            btnExit.Click += BtnExit_Click;

            this.Controls.Add(btnStartSetup);
            this.Controls.Add(btnRules);
            this.Controls.Add(btnExit);

            
            animationTimer = new Timer
            {
                Interval = 400
            };
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (currentIndex < word.Length)
            {
                lblWord.Text += word[currentIndex];
                currentIndex++;
            }
            else
            {
                animationTimer.Stop();
            }
        }

        private void BtnStartSetup_Click(object sender, EventArgs e)
        {
            MainForm setupForm = new MainForm();
            setupForm.Show();
            this.Hide();
        }

        private void BtnRules_Click(object sender, EventArgs e)
        {
            RulesForm rulesForm = new RulesForm();
            rulesForm.ShowDialog();
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}