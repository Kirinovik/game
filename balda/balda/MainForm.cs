using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace BaldaGame
{
    public partial class MainForm : Form
    {
        private RadioButton rbtn5x5;
        private RadioButton rbtn7x7;
        private ComboBox cmbPlayers;
        private Button btnStartGame;
        private Button btnManageDictionary;

        private string dictionaryFile = "dictionary.txt";
        private List<string> dictionaryWords;

        public MainForm()
        {
            this.Text = "Настройка игры «Балда»";
            this.Size = new Size(350, 250);
            InitializeComponents();
            LoadDictionary();
        }

        private void InitializeComponents()
        {
            rbtn5x5 = new RadioButton
            {
                Text = "5 x 5",
                Location = new Point(20, 20),
                Checked = true
            };
            rbtn7x7 = new RadioButton
            {
                Text = "7 x 7",
                Location = new Point(140, 20)
            };

            cmbPlayers = new ComboBox
            {
                Location = new Point(20, 60),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 100
            };
            cmbPlayers.Items.AddRange(new object[] { "2", "3", "4" });
            cmbPlayers.SelectedIndex = 0;

            btnStartGame = new Button
            {
                Text = "Начать игру",
                Location = new Point(20, 100),
                Width = 120
            };
            btnStartGame.Click += btnStartGame_Click;


            btnManageDictionary = new Button
            {
                Text = "Словарь",
                Location = new Point(150, 100),
                Width = 120
            };
            btnManageDictionary.Click += btnManageDictionary_Click;

            // Добавляем элементы на форму
            this.Controls.Add(rbtn5x5);
            this.Controls.Add(rbtn7x7);
            this.Controls.Add(cmbPlayers);
            this.Controls.Add(btnStartGame);
            this.Controls.Add(btnManageDictionary);
        }

        private void LoadDictionary()
        {
            if (File.Exists(dictionaryFile))
                dictionaryWords = new List<string>(File.ReadAllLines(dictionaryFile));
            else
                dictionaryWords = new List<string>();
        }

        private void SaveDictionary()
        {
            File.WriteAllLines(dictionaryFile, dictionaryWords);
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            int gridSize = rbtn5x5.Checked ? 5 : 7;
            int players = int.Parse(cmbPlayers.SelectedItem.ToString());
            // Передача словаря в форму игры
            GameForm gameForm = new GameForm(gridSize, players, dictionaryWords);
            gameForm.Show();
        }

        private void btnManageDictionary_Click(object sender, EventArgs e)
        {
            ManageDictionaryForm dictForm = new ManageDictionaryForm(dictionaryWords);
            if (dictForm.ShowDialog() == DialogResult.OK)
            {
                // Обновление словаря после редактирования
                dictionaryWords = dictForm.DictionaryWords;
                SaveDictionary();
            }
        }
    }
}