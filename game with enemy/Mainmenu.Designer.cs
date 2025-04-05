using System;

namespace WindowsFormsApp2
{
    partial class MainMenu
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удалён; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void MainMenu_Load(object sender, EventArgs e)
        {
            // Здесь можно разместить код, который должен выполняться при загрузке формы.
        }

        private void InitializeComponent()
        {
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnOptions = new System.Windows.Forms.Button();
            this.btnAuthors = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartGame
            // 
            this.btnStartGame.Location = new System.Drawing.Point(75, 41);
            this.btnStartGame.Margin = new System.Windows.Forms.Padding(2);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(150, 32);
            this.btnStartGame.TabIndex = 0;
            this.btnStartGame.Text = "Начало игры";
            this.btnStartGame.UseVisualStyleBackColor = true;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnOptions
            // 
            this.btnOptions.Location = new System.Drawing.Point(75, 89);
            this.btnOptions.Margin = new System.Windows.Forms.Padding(2);
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.Size = new System.Drawing.Size(150, 32);
            this.btnOptions.TabIndex = 1;
            this.btnOptions.Text = "Управление";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.btnOptions_Click);
            // 
            // btnAuthors
            // 
            this.btnAuthors.Location = new System.Drawing.Point(75, 138);
            this.btnAuthors.Margin = new System.Windows.Forms.Padding(2);
            this.btnAuthors.Name = "btnAuthors";
            this.btnAuthors.Size = new System.Drawing.Size(150, 32);
            this.btnAuthors.TabIndex = 2;
            this.btnAuthors.Text = "Об авторах";
            this.btnAuthors.UseVisualStyleBackColor = true;
            this.btnAuthors.Click += new System.EventHandler(this.btnAuthors_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(75, 187);
            this.btnExit.Margin = new System.Windows.Forms.Padding(2);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(150, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Выход";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 284);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAuthors);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.btnStartGame);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainMenu";
            this.Text = "Главное меню";
            this.Load += new System.EventHandler(this.MainMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.Button btnAuthors;
        private System.Windows.Forms.Button btnExit;
    }
}
