using System;
using System.Drawing;
using System.Windows.Forms;

namespace BaldaGame
{
    public partial class RulesForm : Form
    {
        public RulesForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Правила игры";
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblRules = new Label
            {
                Text =
                    "Правила игры «Балда»:\n\n" +
                    "1. Игроки по очереди добавляют буквы на игровое поле.\n" +
                    "2. Цель игры — составить как можно больше слов из букв на поле.\n" +
                    "3. Каждое новое слово должно быть связано с уже существующими буквами.\n" +
                    "4. За каждое составленное слово игрок получает очки, равные длине слова.\n" +
                    "5. Побеждает игрок, набравший наибольшее количество очков.",
                Location = new Point(20, 20),
                AutoSize = true,
                Font = new Font("Arial", 12)
            };

            this.Controls.Add(lblRules);
        }
    }
}