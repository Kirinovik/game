using System;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class OptionsForm : Form
    {
        private Button btnBack; 
        private Label lblControlsInfo; 

        public OptionsForm()
        {
            InitializeComponent();
            AddContent(); 
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {

        }

        private void AddContent()
        {
            lblControlsInfo = new Label();
            lblControlsInfo.Text = "Управление: \nВверх - W\nВниз - S \nВлево - A \nВправо - D";
            lblControlsInfo.AutoSize = true;
            lblControlsInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblControlsInfo.Font = new System.Drawing.Font("Arial", 24);
            lblControlsInfo.Location = new System.Drawing.Point(300, 100);
            this.Controls.Add(lblControlsInfo);

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
