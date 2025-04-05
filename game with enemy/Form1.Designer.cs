namespace WindowsFormsApp2
{
    partial class Form1
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
            this.gamePanel = new System.Windows.Forms.Panel();
            this.health = new System.Windows.Forms.Label();
            this.scoreLabel = new System.Windows.Forms.Label();
            this.levelLabel = new System.Windows.Forms.Label();
            this.player = new System.Windows.Forms.PictureBox();
            this.canon = new System.Windows.Forms.PictureBox();
            this.gamePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.canon)).BeginInit();
            this.SuspendLayout();
            // 
            // gamePanel
            // 
            this.gamePanel.BackColor = System.Drawing.Color.LightGray;
            this.gamePanel.Controls.Add(this.health);
            this.gamePanel.Location = new System.Drawing.Point(0, 0);
            this.gamePanel.Name = "gamePanel";
            this.gamePanel.Size = new System.Drawing.Size(800, 600);
            this.gamePanel.TabIndex = 0;
            this.gamePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.gamePanel_Paint);
            // 
            // health
            // 
            this.health.AutoSize = true;
            this.health.BackColor = System.Drawing.Color.Transparent;
            this.health.Location = new System.Drawing.Point(57, 106);
            this.health.Name = "health";
            this.health.Size = new System.Drawing.Size(35, 13);
            this.health.TabIndex = 0;
            this.health.Text = "label1";
            // 
            // scoreLabel
            // 
            this.scoreLabel.AutoSize = true;
            this.scoreLabel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.scoreLabel.Location = new System.Drawing.Point(10, 10);
            this.scoreLabel.Name = "scoreLabel";
            this.scoreLabel.Size = new System.Drawing.Size(82, 22);
            this.scoreLabel.TabIndex = 1;
            this.scoreLabel.Text = "Score: 0";
            // 
            // levelLabel
            // 
            this.levelLabel.AutoSize = true;
            this.levelLabel.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.levelLabel.Location = new System.Drawing.Point(10, 40);
            this.levelLabel.Name = "levelLabel";
            this.levelLabel.Size = new System.Drawing.Size(77, 22);
            this.levelLabel.TabIndex = 2;
            this.levelLabel.Text = "Level: 1";
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Blue;
            this.player.Location = new System.Drawing.Point(50, 50);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(50, 50);
            this.player.TabIndex = 3;
            this.player.TabStop = false;
            // 
            // canon
            // 
            this.canon.Location = new System.Drawing.Point(0, 0);
            this.canon.Name = "canon";
            this.canon.Size = new System.Drawing.Size(100, 50);
            this.canon.TabIndex = 0;
            this.canon.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Controls.Add(this.levelLabel);
            this.Controls.Add(this.scoreLabel);
            this.Controls.Add(this.gamePanel);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.gamePanel.ResumeLayout(false);
            this.gamePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.canon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel gamePanel;
        private System.Windows.Forms.Label scoreLabel;
        private System.Windows.Forms.Label levelLabel;
        private System.Windows.Forms.PictureBox player;
        private System.Windows.Forms.PictureBox canon;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label health;
    }
}