using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class AuthorsForm : Form
    {
        private Button btnBack; // Добавляем кнопку "Назад"
        private Label lbl;

        public AuthorsForm()
        {
            InitializeComponent();
            AddBackButton(); // Вызываем метод для добавления кнопки
        }

        private void AuthorsForm_Load(object sender, EventArgs e)
        {
            
        }

        private void AddBackButton()
        {
            lbl = new Label();
            lbl.Text = "У НАС БЕТА ТЕСТ";
            lbl.AutoSize = true;
            lbl.Size = new System.Drawing.Size(20, 40);
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl.Location = new System.Drawing.Point(250, 20);
            lbl.Font = new System.Drawing.Font("TimesNewRoman", 30);
            this.Controls.Add(lbl);

            lbl = new Label();
            lbl.Text = "Издатель: \nБалда Interteimant \nСтудия разработчиков: \nGibridX";
            lbl.AutoSize = true;
            lbl.Size = new System.Drawing.Size(20,40);
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl.Location = new System.Drawing.Point(250, 80);
            lbl.Font = new System.Drawing.Font("Arial", 24);
            this.Controls.Add(lbl);

            lbl = new Label();
            lbl.Text = "Никакие права не защищены";
            lbl.AutoSize = true;
            lbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lbl.Location = new System.Drawing.Point(345, 420);
            lbl.Font = new System.Drawing.Font("Arial", 8);
            this.Controls.Add(lbl);

            btnBack = new Button();
            btnBack.Text = "Назад";
            btnBack.Size = new System.Drawing.Size(100, 40);
            btnBack.Location = new System.Drawing.Point(10, 400); 
            btnBack.Click += new EventHandler(btnBack_Click);

            this.Controls.Add(btnBack); 
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
