using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

public partial class GameForm : Form
{
    private List<Button> selectedCells = new List<Button>();
    private List<string> dictionaryWords;
    private Color defaultColor = Color.White;
    private Color selectedColor = Color.LightBlue;
    private Color validWordColor = Color.LightGreen;

    private int gridSize;  // Размер поля (5 или 7)
    private int playerCount;  // Количество игроков
    private int currentPlayer = 0;  // Текущий игрок (индекс начинается с 0)
    private Label playerTurnLabel;  // Отображение текущего игрока
    private Dictionary<int, int> playerScores;  // Словарь для хранения очков игроков
    private Label[] scoreLabels;  // Массив меток для отображения очков
    private bool letterAddedThisTurn = false;  // Флаг для проверки добавления буквы

    public GameForm(int gridSize, int playerCount, List<string> dictionary)
    {
        InitializeComponent();

        this.gridSize = gridSize;
        this.playerCount = playerCount;
        this.dictionaryWords = dictionary;

        InitializeGameBoard();
        AddBaldaToCenter();  // Добавляем слово "БАЛДА" по центру горизонтально
        InitializePlayerTurnLabel();  // Инициализация отображения текущего игрока
        InitializeScores();  // Инициализация системы очков

        // Устанавливаем размеры формы
        this.ClientSize = new Size(
            20 + gridSize * 40 + 150, // Ширина: отступ + сетка + место для счета
            20 + gridSize * 40 + 40 // Высота: отступ + сетка + отступ
        );
    }


    private void InitializeScores()
    {
        playerScores = new Dictionary<int, int>();
        scoreLabels = new Label[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            playerScores[i] = 0; // Начальные очки для каждого игрока

            // Создаем метку для отображения очков
            scoreLabels[i] = new Label
            {
                Text = $"Игрок {i + 1}: 0 очков",
                Font = new Font("Arial", 12),
                ForeColor = Color.Black,
                AutoSize = true,
                Location = new Point(gridSize * 40 + 40, 20 + i * 30) 
            };
            this.Controls.Add(scoreLabels[i]);
        }
        }

    private void UpdateScores(int playerIndex, int points)
    {
        playerScores[playerIndex] += points; // Добавляем очки игроку
        scoreLabels[playerIndex].Text = $"Игрок {playerIndex + 1}: {playerScores[playerIndex]} очков";
    }

    private void InitializePlayerTurnLabel()
    {
        playerTurnLabel = new Label
        {
            Text = $"Ход игрока {currentPlayer + 1}",
            Font = new Font("Arial", 14),
            ForeColor = Color.Black,
            AutoSize = true,
            Location = new Point(20, gridSize * 40 + 40) // Под игровым полем
        };
        this.Controls.Add(playerTurnLabel);
    }

    private void UpdatePlayerTurnLabel()
    {
        playerTurnLabel.Text = $"Ход игрока {currentPlayer + 1}";
    }

    private void InitializeComponent()
    {
        this.SuspendLayout();
        // 
        // GameForm
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 1000);
        this.Name = "GameForm";
        this.Text = "Игра Балда";
        this.ResumeLayout(false);
    }

    private void InitializeGameBoard()
    {
        int buttonSize = 40;
        int startX = 20;
        int startY = 20;

        for (int row = 0; row < this.gridSize; row++)
        {
            for (int col = 0; col < this.gridSize; col++)
            {
                Button cell = new Button
                {
                    Width = buttonSize,
                    Height = buttonSize,
                    Left = startX + col * buttonSize,
                    Top = startY + row * buttonSize,
                    Tag = new Point(row, col),
                    Font = new Font("Arial", 14),
                    BackColor = defaultColor
                };
                cell.MouseClick += Cell_Click;
                this.Controls.Add(cell);
            }
        }

        this.ClientSize = new Size(
            startX * 2 + this.gridSize * buttonSize,
            startY * 2 + this.gridSize * buttonSize + 50);
    }

    private void AddBaldaToCenter()
    {
        int centerRow = gridSize / 2;

        string word = "БАЛДА";

        int startCol = (gridSize - word.Length) / 2;

        for (int i = 0; i < word.Length; i++)
        {
            int col = startCol + i; 
            Button cell = GetButtonAt(centerRow, col);
            if (cell != null)
            {
                cell.Text = word[i].ToString();
                cell.BackColor = defaultColor; 
            }
        }
    }

    private Button GetButtonAt(int row, int col)
    {
        // Ищем кнопку по строке и столбцу
        foreach (Button btn in this.Controls)
        {
            Point position = (Point)btn.Tag;
            if (position.X == row && position.Y == col)
                return btn;
        }
        return null;
    }

    private void ResetSelection()
    {
        foreach (var btn in selectedCells)
        {
            btn.BackColor = defaultColor; // Возвращаем обычный цвет
        }
        selectedCells.Clear(); // Очищаем список выделенных букв
    }


    private void Cell_Click(object sender, MouseEventArgs e)
    {
        Button clickedCell = sender as Button;
        if (clickedCell == null) return;

        if (e.Button == MouseButtons.Left)
        {
            if (string.IsNullOrEmpty(clickedCell.Text))
            {
                
                if (!letterAddedThisTurn)
                {
                    clickedCell.BackColor = defaultColor; 

                    TextBox textBox = new TextBox
                    {
                        Bounds = clickedCell.Bounds,
                        MaxLength = 1,
                        TextAlign = HorizontalAlignment.Center,
                        Font = clickedCell.Font
                    };

                    this.Controls.Add(textBox);
                    textBox.Focus();

                    textBox.LostFocus += (s, ev) =>
                    {
                        if (!string.IsNullOrEmpty(textBox.Text))
                        {
                            clickedCell.Text = textBox.Text.ToUpper(); 
                            letterAddedThisTurn = true; 
                        }

                        this.Controls.Remove(textBox);
                        textBox.Dispose();
                    };
                }
                else
                {
                    MessageBox.Show("Вы уже добавили букву в этот ход!");
                }
            }
            else
            {
                ToggleSelection(clickedCell); 
            }
        }
        else if (e.Button == MouseButtons.Right)
        {
            ResetSelection(); 
        }
    }

    private void CheckWord()
    {
        string word = string.Join("", selectedCells.Select(b => b.Text)); 

        if (dictionaryWords.Contains(word))
        {
            // Если слово найдено в словаре, выделяем клетки другим цветом
            foreach (var btn in selectedCells)
            {
                btn.BackColor = validWordColor; 
            }

            // Добавляем очки игроку
            int points = word.Length; 
            UpdateScores(currentPlayer, points);

            // Сброс состояния хода
            selectedCells.Clear(); 
            letterAddedThisTurn = false; 

            // Переключаемся на следующего игрока
            currentPlayer = (currentPlayer + 1) % playerCount;
            UpdatePlayerTurnLabel();
        }
        else
        {
            foreach (var btn in selectedCells)
            {
                btn.BackColor = selectedColor; 
            }
        }
    }

    private void ToggleSelection(Button cell)
    {
        if (selectedCells.Contains(cell))
        {
            // Отменяем выбор буквы
            cell.BackColor = defaultColor;
            selectedCells.Remove(cell);
        }
        else
        {
            // Выбираем букву
            cell.BackColor = selectedColor;
            selectedCells.Add(cell);
        }

        CheckWord(); // Проверяем слово
    }
}

