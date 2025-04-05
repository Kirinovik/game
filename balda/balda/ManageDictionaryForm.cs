using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace BaldaGame
{
    public partial class ManageDictionaryForm : Form
    {
        public List<string> DictionaryWords { get; private set; }
        private ListBox lstDictionary;
        private TextBox txtNewWord;
        private Button btnAddWord;
        private Button btnRemoveWord;
        private Button btnOK;

        public ManageDictionaryForm(List<string> words)
        {
            this.Text = "Управление словарём";
            this.Size = new Size(400, 300);
            DictionaryWords = new List<string>(words);
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            lstDictionary = new ListBox
            {
                Location = new Point(10, 10),
                Size = new Size(200, 200)
            };

            txtNewWord = new TextBox
            {
                Location = new Point(10, 220),
                Width = 200
            };

            btnAddWord = new Button
            {
                Text = "Добавить",
                Location = new Point(220, 10),
                Width = 100
            };
            btnAddWord.Click += btnAddWord_Click;

            btnRemoveWord = new Button
            {
                Text = "Удалить",
                Location = new Point(220, 50),
                Width = 100
            };
            btnRemoveWord.Click += btnRemoveWord_Click;

            btnOK = new Button
            {
                Text = "OK",
                Location = new Point(220, 90),
                Width = 100
            };
            btnOK.Click += btnOK_Click;

            this.Controls.Add(lstDictionary);
            this.Controls.Add(txtNewWord);
            this.Controls.Add(btnAddWord);
            this.Controls.Add(btnRemoveWord);
            this.Controls.Add(btnOK);

            RefreshListBox();
        }

        private void RefreshListBox()
        {
            lstDictionary.DataSource = null;
            lstDictionary.DataSource = DictionaryWords;
        }

        private void btnAddWord_Click(object sender, EventArgs e)
        {
            string newWord = txtNewWord.Text.Trim();
            if (!string.IsNullOrEmpty(newWord) && !DictionaryWords.Contains(newWord))
            {
                DictionaryWords.Add(newWord);
                RefreshListBox();
                txtNewWord.Clear();
            }
            else
            {
                MessageBox.Show("Введите новое слово или слово уже существует.");
            }
        }

        private void btnRemoveWord_Click(object sender, EventArgs e)
        {
            if (lstDictionary.SelectedItem != null)
            {
                DictionaryWords.Remove(lstDictionary.SelectedItem.ToString());
                RefreshListBox();
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void ManageDictionaryForm_Load(object sender, EventArgs e)
        {

        }
    }
}
