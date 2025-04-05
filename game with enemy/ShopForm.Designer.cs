namespace WindowsFormsApp2 // Исправлено: должно совпадать с пространством имен основного проекта
{
    partial class ShopForm // Убедитесь, что класс объявлен как partial
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ShopForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F); // Убедитесь, что значения корректны
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450); // Можно изменить размер
            this.Name = "ShopForm"; // Имя должно совпадать с классом
            this.Text = "Магазин"; // Можно изменить заголовок окна
            this.ResumeLayout(false); // Убедитесь, что метод завершен
        }

        #endregion
    }
}