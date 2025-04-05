using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class ShopForm : Form
    {
        private Label scoreLabel;
        private Action<int> _scoreUpdated;
        private Action<int> _increaseSpeed;
        private Action<int> _addHealth;
        private int currentScore;

        public ShopForm(int initialScore, Action<int> scoreUpdated = null, Action<int> increaseSpeed = null, Action<int> addHealth = null)
        {
            InitializeComponent();
            InitializeShop();
            currentScore = initialScore;
            UpdateScore(currentScore);
            _scoreUpdated = scoreUpdated;
            _increaseSpeed = increaseSpeed;
            _addHealth = addHealth;
        }

        private void InitializeShop()
        {
            this.Text = "Магазин";
            this.Size = new Size(400, 300);

            scoreLabel = new Label
            {
                Text = $"Монеты: {currentScore}",
                Font = new Font("Arial", 14),
                Location = new Point(20, 20),
                AutoSize = true
            };
            this.Controls.Add(scoreLabel);


            // Создаем панель для кнопок и картинок
            int buttonYOffset = 80;
            int iconSize = 50;

            // Кнопка для увеличения скорости
            PictureBox speedIcon = new PictureBox
            {
                Size = new Size(iconSize, iconSize),
                Location = new Point(50, buttonYOffset),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = WindowsFormsApp3.Properties.Resources.jordan // Убедитесь, что ресурс существует
            };
            this.Controls.Add(speedIcon);
            Button speedButton = new Button
            {
                Text = "Увеличить скорость",
                Location = new Point(120, buttonYOffset),
                Size = new Size(200, 30)
            };
            speedButton.Click += (s, e) => BuySpeedIncrease(20);
            this.Controls.Add(speedButton);

            // Метка с ценой для увеличения скорости
            Label speedPriceLabel = new Label
            {
                Text = "20 монет",
                Font = new Font("Arial", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(128, Color.LightGray), // Полупрозрачный белый цвет
                Location = new Point(120, buttonYOffset + 35), // Расположение под кнопкой
                AutoSize = true
            };
            this.Controls.Add(speedPriceLabel);

            PictureBox healthIcon = new PictureBox
            {
                Size = new Size(iconSize, iconSize),
                Location = new Point(50, buttonYOffset + 60),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = WindowsFormsApp3.Properties.Resources.basket // Убедитесь, что ресурс существует
            };
            this.Controls.Add(healthIcon);

            Button healthButton = new Button
            {
                Text = "Добавить здоровье",
                Location = new Point(120, buttonYOffset + 60),
                Size = new Size(200, 30)
            };
            healthButton.Click += (s, e) => BuyHealthIncrease(30);
            this.Controls.Add(healthButton);

            // Метка с ценой для добавления здоровья
            Label healthPriceLabel = new Label
            {
                Text = "30 монет",
                Font = new Font("Arial", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(128, Color.LightGray), // Полупрозрачный белый цвет
                Location = new Point(120, buttonYOffset + 95), // Расположение под кнопкой
                AutoSize = true
            };
            this.Controls.Add(healthPriceLabel);
        }

        private void UpdateScore(int newScore)
        {
            scoreLabel.Text = $"Монеты: {newScore}";
            currentScore = newScore;
        }

        private void BuySpeedIncrease(int cost)
        {
            if (currentScore >= cost && _increaseSpeed != null)
            {
                currentScore -= cost;
                UpdateScore(currentScore);
                _scoreUpdated?.Invoke(currentScore);
                _increaseSpeed?.Invoke(5); // Увеличиваем скорость на 5 единиц
                MessageBox.Show("Скорость увеличена!");
            }
            else
            {
                MessageBox.Show("Недостаточно монет для покупки.");
            }
        }

        private void BuyHealthIncrease(int cost)
        {
            if (currentScore >= cost && _addHealth != null)
            {
                currentScore -= cost;
                UpdateScore(currentScore);
                _scoreUpdated?.Invoke(currentScore);
                _addHealth?.Invoke(20); // Добавляем 20 единиц здоровья без ограничений
                MessageBox.Show("Здоровье восстановлено!");
            }
            else
            {
                MessageBox.Show("Недостаточно монет для покупки.");
            }
        }
    }
}