    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO; // Добавлено для работы с Stream
    using System.Linq;
    using System.Reflection; // Добавлено для работы с Assembly
    using System.Windows.Forms;
using WindowsFormsApp3;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private Dictionary<Keys, bool> keyStates = new Dictionary<Keys, bool>
            {
                { Keys.W, false },
                { Keys.S, false },
                { Keys.A, false },
                { Keys.D, false },
                { Keys.Q, false } // Добавляем Q для стрельбы
            };

        private List<PictureBox> coins = new List<PictureBox>();
        private List<PictureBox> enemies = new List<PictureBox>();
        private List<PictureBox> bullets = new List<PictureBox>(); // Список снарядов
        private int score = 0;
        private Timer gameTimer;
        private int speed = 10;
        private int enemySpeed = 5;
        private bool isGameActive = true;
        private int level = 1; // Номер уровня
        private int coinsPerLevel = 10; // Количество монет на уровне
        private bool isPlayerActive = true; // Флаг, указывающий, активен ли игрок
        private PictureBox portal;
        private bool portalActive = false;
        private Point playerLastPosition = new Point(50, 50); // Начальная позиция
        private int playerHealth = 100; // Начальное здоровье
        private Label healthLabel;
        private bool isInvulnerable = false;
        private int invulnerabilityDuration = 500; 
        private int invulnerabilityCounter = 0;
        private PictureBox shopButton;

        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            this.KeyDown += Form1_KeyDown;
            this.KeyUp += Form1_KeyUp;
            this.Text = "Война с логистами (У НАС БЕТА)";
            this.KeyPreview = true;

            InitializeShopButton();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Код, который должен выполняться при загрузке формы
            InitializeGame();
        }

        private void InitializeShopButton()
        {
            shopButton = new PictureBox
            {
                Size = new Size(150, 120),
                Location = new Point(650, 10),
                BackColor = Color.LightGray,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false
            };

            shopButton.Image = WindowsFormsApp3.Properties.Resources.kfc;

            shopButton.Click += (s, e) =>
            {
                var shopForm = new ShopForm(
                    score,
                    OnScoreUpdated, // Обновление счета
                    IncreasePlayerSpeed, // Увеличение скорости
                    AddPlayerHealth // Добавление здоровья
                );
                shopForm.Show();
            };
            this.Controls.Add(shopButton);
        }

        // Метод для увеличения скорости игрока
        private void IncreasePlayerSpeed(int amount)
        {
            speed += amount;
            MessageBox.Show($"Текущая скорость: {speed}");
        }

        // Метод для добавления здоровья игроку
        private void AddPlayerHealth(int amount)
        {
            playerHealth += amount; // Просто добавляем здоровье без ограничений
            healthLabel.Text = $"Health: {playerHealth}"; // Обновляем метку здоровья
        }

        // Добавьте метод для обновления счёта в Form1:
        private void OnScoreUpdated(int newScore)
        {
            score = newScore;
            scoreLabel.Text = $"Score: {score}";
        }


        private void InitializeGame()
        {
            if (gameTimer != null)
            {
                gameTimer.Stop();
                gameTimer.Dispose();
            }

            // Очистка
            portal?.Dispose();
            portal = null;
            portalActive = false;
            coins.Clear();
            enemies.Clear();
            bullets.Clear();
            gamePanel.Controls.Clear();

            // Настройка игровой панели
            gamePanel.Size = new Size(800, 600);
            gamePanel.Location = new Point(0, 0);
            gamePanel.BackColor = Color.LightGray;

            // Инициализация меток
            scoreLabel.Text = $"Score: {score}";
            scoreLabel.Location = new Point(10, 10);
            levelLabel.Text = $"Level: {level}";
            levelLabel.Location = new Point(10, 40);

            // Инициализация здоровья
            healthLabel = new Label 
            {
                Text = $"Health: {playerHealth}",
                Location = new Point(10, 70),
                Font = new Font("Arial", 14),
                ForeColor = Color.Black,
                Visible = true,
                AutoSize = true
            };

            // Добавление элементов на панель
            gamePanel.Controls.Add(scoreLabel);
            gamePanel.Controls.Add(healthLabel);
            healthLabel.BringToFront();
            gamePanel.Controls.Add(levelLabel);
            gamePanel.Controls.Add(player);

            player.Size = new Size(50, 50);
            player.Location = playerLastPosition;
            player.BackColor = Color.Transparent;

            player.Location = playerLastPosition;

            try
            {
                player.Image = WindowsFormsApp3.Properties.Resources.max_tac;
                player.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
                player.BackColor = Color.Blue;
            }

            CreateCoins(); // Создаем монеты
            CreateEnemies(level); // Создаем врагов

            gameTimer = new Timer();
            gameTimer.Interval = 20;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();

            isGameActive = true;
            isPlayerActive = true;
        }

        private void CreatePortal()
        {
            Random random = new Random();
            int x = 0, y = 0;
            bool placed = false;
            int maxAttempts = 100;


            for (int i = 0; i < maxAttempts && !placed; i++)
            {
                x = random.Next(50, gamePanel.Width - 50 - 50);
                y = random.Next(50, gamePanel.Height - 50 - 50);

                // Исправление: Проверка прямоугольников вместо точек
                Rectangle portalRect = new Rectangle(x, y, 50, 50);
                if (!player.Bounds.IntersectsWith(portalRect) &&
                    !coins.Any(c => c.Bounds.IntersectsWith(portalRect)) &&
                    !enemies.Any(e => e.Bounds.IntersectsWith(portalRect)))
                {
                    placed = true;
                }
            }

            if (!placed)
                throw new Exception("Не удалось разместить портал");

            portal = new PictureBox
            {
                Size = new Size(50, 50),
                Location = new Point(x, y),
                BackColor = Color.Cyan,
                Tag = "portal", // Добавьте этот атрибут
                Visible = true
            };

            try
            {
                // Исправление: Добавлены точка с запятой
                portal.Image = WindowsFormsApp3.Properties.Resources.portal;
                portal.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            catch
            {
                portal.BackColor = Color.Cyan;
            }

            gamePanel.Controls.Add(portal);
        }

        // Измените метод CheckCollision() следующим образом:
        private void CheckCollision()
        {
            foreach (var coin in coins.ToList())
            {
                if (player.Bounds.IntersectsWith(coin.Bounds))
                {
                    gamePanel.Controls.Remove(coin);
                    coins.Remove(coin);
                    score++;
                    scoreLabel.Text = $"Score: {score}";

                    if (coins.Count == 0 && !portalActive)
                    {
                        CreatePortal();
                        portalActive = true;

                        // Показываем кнопку
                        shopButton.Visible = true;
                        shopButton.BringToFront(); // Убедитесь, что она поверх других элементов

                        
                    }
                }
            }

            foreach (var bullet in bullets.ToList())
            {
                foreach (var enemy in enemies.ToList())
                {
                    if (bullet.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        gamePanel.Controls.Remove(enemy);
                        enemies.Remove(enemy);
                        gamePanel.Controls.Remove(bullet);
                        bullets.Remove(bullet);
                        score += 10;
                        scoreLabel.Text = $"Score: {score}";
                    }
                }
            }

            if (portalActive && portal != null)
            {
                if (player.Bounds.IntersectsWith(portal.Bounds))
                {
                    // Сохраняем позицию портала как стартовую для следующего уровня
                    playerLastPosition = portal.Location; // Используем Location портала
                    NextLevel();
                }
            }

            if (coins.Count == 0 && portalActive && player.Bounds.IntersectsWith(portal.Bounds))
            {
                shopButton.Visible = true; // Показываем кнопку

                // Добавляем задержку перед переходом на следующий уровень
                System.Threading.Timer timer = new System.Threading.Timer((e) =>
                {
                    NextLevel();
                }, null, 2000, System.Threading.Timeout.Infinite); // 2 секунды задержки
            }

            // Проверка врагов
            foreach (var enemy in enemies.ToList())
            {
                if (player.Bounds.IntersectsWith(enemy.Bounds))
                {
                    if (!isInvulnerable)
                    {
                        // Наносим урон и устанавливаем неосязаемость
                        playerHealth -= 10;
                        healthLabel.Text = $"Health: {playerHealth}";

                        if (playerHealth <= 0)
                        {
                            gameTimer.Stop();
                            MessageBox.Show("Вы проиграли! Здоровье закончилось.", "Проигрыш");
                            RestartGame();
                        }
                        else
                        {
                            isInvulnerable = true;
                            invulnerabilityCounter = invulnerabilityDuration; // Обновляем счётчик
                            player.BackColor = Color.Red; // Визуальная анимация
                        }
                    }
                }
            }
        }

        // Измените метод NextLevel():
        private void NextLevel()
        {
            level++;
            coins.Clear();
            enemies.Clear();
            portal?.Dispose();
            portal = null;
            portalActive = false;

            // Добавьте:
            shopButton.Visible = false; // Скрываем кнопку при переходе на следующий уровень

            levelLabel.Text = $"Level: {level}";
            InitializeGame();
        }
        private void CreateCoins()
        {
            Random random = new Random();
            int maxAttempts = 100;

            for (int i = 0; i < coinsPerLevel; i++)
            {
                bool coinPlaced = false;

                for (int attempt = 0; attempt < maxAttempts && !coinPlaced; attempt++)
                {
                    int x = random.Next(50, gamePanel.Width - 50 - 30); // Учитываем ширину монеты
                    int y = random.Next(50, gamePanel.Height - 50 - 30); // Учитываем высоту монеты

                    PictureBox coin = new PictureBox
                    {
                        Size = new Size(30, 30),
                        Location = new Point(x, y),
                        BackColor = Color.Transparent, // Устанавливаем прозрачный фон
                        Tag = "coin",
                        Visible = true // Убедитесь, что монета видима
                    };

                    coin.SizeMode = PictureBoxSizeMode.StretchImage;

                    // Загрузка изображения из ресурсов
                    try
                    {
                        coin.Image = WindowsFormsApp3.Properties.Resources.sok;// Замените 'sok' на имя вашего ресурса
                        coin.BackColor = Color.Transparent; // Убедитесь, что фон прозрачен
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
                        coin.BackColor = Color.Yellow; // В случае ошибки используем желтый цвет
                    }

                    if (!player.Bounds.IntersectsWith(coin.Bounds) &&
                        coins.All(c => !c.Bounds.IntersectsWith(coin.Bounds)) &&
                        enemies.All(e => !e.Bounds.IntersectsWith(coin.Bounds)))
                    {
                        gamePanel.Controls.Add(coin);
                        coins.Add(coin);
                        coin.BringToFront(); // Перемещаем монету на передний план
                        coinPlaced = true;
                    }

                }

                if (!coinPlaced)
                {
                    throw new InvalidOperationException("Не удалось разместить монету после максимального количества попыток.");
                }
            }
        }

        private void CreateEnemies(int level)
        {
            Random random = new Random();
            int maxAttempts = 100;
            int enemiesPerLevel = Math.Max(1, level); // Минимальное количество врагов равно 1

            enemies.Clear(); // Очистим список врагов перед созданием новых

            for (int i = 0; i < enemiesPerLevel; i++)
            {
                bool enemyPlaced = false;

                for (int attempt = 0; attempt < maxAttempts && !enemyPlaced; attempt++)
                {
                    int x = random.Next(50, gamePanel.Width - 50 - 50); // Учитываем ширину врага
                    int y = random.Next(50, gamePanel.Height - 50 - 50); // Учитываем высоту врага

                    PictureBox enemy = new PictureBox
                    {
                        Size = new Size(50, 50),
                        Location = new Point(x, y),
                        Tag = "enemy",
                        Visible = true // Убедитесь, что враг видим
                    };

                    try
                    {
                        // Загрузка изображения из ресурсов
                        enemy.Image = WindowsFormsApp3.Properties.Resources.negr; // Замените 'your_enemy_image' на имя вашего ресурса
                        enemy.SizeMode = PictureBoxSizeMode.StretchImage; // Автоматическое масштабирование изображения
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка загрузки изображения: " + ex.Message);
                        enemy.BackColor = Color.Red; // В случае ошибки используем красный цвет
                    }

                    if (!player.Bounds.IntersectsWith(enemy.Bounds) &&
                        coins.All(c => !c.Bounds.IntersectsWith(enemy.Bounds)) &&
                        enemies.All(e => !e.Bounds.IntersectsWith(enemy.Bounds)))
                    {
                        gamePanel.Controls.Add(enemy);
                        enemies.Add(enemy);
                        enemy.BringToFront(); // Перемещаем врага на передний план
                        enemyPlaced = true;
                    }
                }

                if (!enemyPlaced)
                {
                    throw new InvalidOperationException("Не удалось разместить врага после максимального количества попыток.");
                }
            }
        }



        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (player == null || gamePanel == null) return;
            if (!isGameActive || !isPlayerActive) return;

            int stepX = 0;
            int stepY = 0;

            if (keyStates[Keys.W]) stepY -= speed;
            if (keyStates[Keys.S]) stepY += speed;
            if (keyStates[Keys.A]) stepX -= speed;
            if (keyStates[Keys.D]) stepX += speed;

            MovePlayer(stepX, stepY);
            MoveEnemies();
            CheckCollision();

            // Обработка неуязвимости
            if (isInvulnerable)
            {
                invulnerabilityCounter -= gameTimer.Interval;
                if (invulnerabilityCounter <= 0)
                {
                    isInvulnerable = false;
                    player.BackColor = Color.Transparent;
                }
            }

            // Перемещаем снаряды вне зависимости от состояния неуязвимости
            for (int i = 0; i < bullets.Count; i++)
            {
                PictureBox bullet = bullets[i];
                var direction = (Tuple<double, double>)bullet.Tag;
                bullet.Left += (int)direction.Item1;
                bullet.Top += (int)direction.Item2;

                // Проверка выхода за границы
                if (bullet.Top < 0 || bullet.Bottom > gamePanel.Height ||
                    bullet.Left < 0 || bullet.Right > gamePanel.Width)
                {
                    gamePanel.Controls.Remove(bullet);
                    bullets.RemoveAt(i);
                    i--; // Коррекция индекса после удаления// Уменьшаем индекс, чтобы не пропустить следующий элемент
                }
            }
        }
    


            private void MovePlayer(int deltaX, int deltaY)
            {
                int newLeft = Math.Max(0, Math.Min(gamePanel.Width - player.Width, player.Left + deltaX));
                int newTop = Math.Max(0, Math.Min(gamePanel.Height - player.Height, player.Top + deltaY));

                player.Left = newLeft;
                player.Top = newTop;
            }

            private void MoveEnemies()
            {
                foreach (var enemy in enemies)
                {
                    int deltaX = player.Left - enemy.Left;
                    int deltaY = player.Top - enemy.Top;

                    double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                    if (distance > 0)
                    {
                        // Округляем значения до ближайшего целого числа
                        double normalizedDeltaX = deltaX / distance;
                        double normalizedDeltaY = deltaY / distance;

                        int newX = (int)Math.Round(enemy.Left + normalizedDeltaX * enemySpeed);
                        int newY = (int)Math.Round(enemy.Top + normalizedDeltaY * enemySpeed);

                        // Убедитесь, что координаты в пределах игрового поля
                        newX = Math.Max(0, Math.Min(gamePanel.Width - enemy.Width, newX));
                        newY = Math.Max(0, Math.Min(gamePanel.Height - enemy.Height, newY));

                        enemy.Left = newX;
                        enemy.Top = newY;
                    }
                }
            }

            private void Form1_KeyDown(object sender, KeyEventArgs e)
            {
                if (!isPlayerActive) return; // Если игрок неактивен, ничего не делаем

                if (keyStates.ContainsKey(e.KeyCode))
                {
                    keyStates[e.KeyCode] = true; // Устанавливаем состояние клавиши как "нажата"
                }

                if (e.KeyCode == Keys.Escape)
                {
                    PauseGame(); // Вызываем паузу при нажатии ESC
                }

                if (e.KeyCode == Keys.Q && keyStates[Keys.Q])
                {
                    Shoot(Cursor.Position); // Вызываем метод стрельбы при нажатии Q
                }
            }

            private void Form1_KeyUp(object sender, KeyEventArgs e)
            {
                if (keyStates.ContainsKey(e.KeyCode))
                {
                    keyStates[e.KeyCode] = false; // Сбрасываем состояние клавиши как "отпущена"
                }
            }

            private void PauseGame()
            {
                gameTimer.Stop();
                PauseMenu pauseMenu = new PauseMenu(this);
                pauseMenu.ShowDialog();
                gameTimer.Start();
            }

            private void ResetKeyStates()
            {
                foreach (var key in keyStates.Keys.ToList())
                {
                    keyStates[key] = false;
                }
            }

            public void StartGameTimer()
            {
                gameTimer?.Start();
            }


        public void RestartGame()
        {
            gameTimer.Stop();
            level = 1;
            score = 0;
            playerHealth = 100;
            isInvulnerable = false;
            invulnerabilityCounter = 0;
            healthLabel.Text = $"Health: {playerHealth}";
            playerLastPosition = new Point(50, 50);

            coins.Clear();
            enemies.Clear();
            bullets.Clear();
            gamePanel.Controls.Clear();

            // Скрываем кнопку при рестарте
            shopButton.Visible = false;

            foreach (var key in keyStates.Keys.ToList())
            {
                keyStates[key] = false;
            }

            InitializeGame();
            gameTimer.Start();
        }

        private void Shoot(Point targetLocation)
            {
                PictureBox bullet = new PictureBox
                {
                    Size = new Size(5, 5),
                    BackColor = Color.Black,
                    Tag = "bullet",
                    Visible = true // Убедитесь, что снаряд видим
                };

                // Преобразуем координаты курсора относительно gamePanel
                Point cursorLocation = gamePanel.PointToClient(targetLocation);

                // Расчёт направления снаряда
                int deltaX = cursorLocation.X - (player.Left + player.Width / 2);
                int deltaY = cursorLocation.Y - player.Top;

                double distance = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
                if (distance > 0)
                {
                    double angle = Math.Atan2(deltaY, deltaX);
                    double speed = 10; // Скорость снаряда

                    bullet.Location = new Point(
                        player.Left + player.Width / 2 - bullet.Width / 2,
                        player.Top - bullet.Height
                    );

                    bullet.Tag = new Tuple<double, double>(speed * Math.Cos(angle), speed * Math.Sin(angle));

                    gamePanel.Controls.Add(bullet);
                    bullets.Add(bullet);
                }
            }

            protected override void OnFormClosing(FormClosingEventArgs e)
            {
                gameTimer?.Stop();
                gameTimer?.Dispose();
                base.OnFormClosing(e);
            }

            private void gamePanel_Paint(object sender, PaintEventArgs e)
            {

            }
        }
    }