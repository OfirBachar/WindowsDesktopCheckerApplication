
namespace Checkers
{
    partial class FormLogin
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
            this.buttonPlay = new System.Windows.Forms.Button();
            this.LabelBoardSize = new System.Windows.Forms.Label();
            this.LabelPlayers = new System.Windows.Forms.Label();
            this.LabelPlayer1 = new System.Windows.Forms.Label();
            this.LabelPlayer2 = new System.Windows.Forms.Label();
            this.textBoxName1 = new System.Windows.Forms.TextBox();
            this.textBoxName2 = new System.Windows.Forms.TextBox();
            this.radioButtonSize6 = new System.Windows.Forms.RadioButton();
            this.radioButtonSize10 = new System.Windows.Forms.RadioButton();
            this.radioButtonSize8 = new System.Windows.Forms.RadioButton();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonPlay
            // 
            this.buttonPlay.Location = new System.Drawing.Point(398, 391);
            this.buttonPlay.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.buttonPlay.Name = "buttonPlay";
            this.buttonPlay.Size = new System.Drawing.Size(208, 56);
            this.buttonPlay.TabIndex = 0;
            this.buttonPlay.Text = "Done";
            this.buttonPlay.UseVisualStyleBackColor = true;
            this.buttonPlay.Click += new System.EventHandler(this.buttonPlay_Click);
            // 
            // LabelBoardSize
            // 
            this.LabelBoardSize.AutoSize = true;
            this.LabelBoardSize.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelBoardSize.Location = new System.Drawing.Point(4, 14);
            this.LabelBoardSize.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelBoardSize.Name = "LabelBoardSize";
            this.LabelBoardSize.Size = new System.Drawing.Size(200, 45);
            this.LabelBoardSize.TabIndex = 1;
            this.LabelBoardSize.Text = "Board Size:";
            // 
            // LabelPlayers
            // 
            this.LabelPlayers.AutoSize = true;
            this.LabelPlayers.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.LabelPlayers.Location = new System.Drawing.Point(4, 152);
            this.LabelPlayers.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelPlayers.Name = "LabelPlayers";
            this.LabelPlayers.Size = new System.Drawing.Size(143, 45);
            this.LabelPlayers.TabIndex = 2;
            this.LabelPlayers.Text = "Players:";
            // 
            // LabelPlayer1
            // 
            this.LabelPlayer1.AutoSize = true;
            this.LabelPlayer1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.LabelPlayer1.Location = new System.Drawing.Point(57, 217);
            this.LabelPlayer1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelPlayer1.Name = "LabelPlayer1";
            this.LabelPlayer1.Size = new System.Drawing.Size(157, 45);
            this.LabelPlayer1.TabIndex = 3;
            this.LabelPlayer1.Text = "Player 1:";
            // 
            // LabelPlayer2
            // 
            this.LabelPlayer2.AutoSize = true;
            this.LabelPlayer2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.LabelPlayer2.Location = new System.Drawing.Point(110, 279);
            this.LabelPlayer2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.LabelPlayer2.Name = "LabelPlayer2";
            this.LabelPlayer2.Size = new System.Drawing.Size(157, 45);
            this.LabelPlayer2.TabIndex = 4;
            this.LabelPlayer2.Text = "Player 2:";
            // 
            // textBoxName1
            // 
            this.textBoxName1.Location = new System.Drawing.Point(336, 229);
            this.textBoxName1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.textBoxName1.Name = "textBoxName1";
            this.textBoxName1.Size = new System.Drawing.Size(267, 38);
            this.textBoxName1.TabIndex = 5;
            // 
            // textBoxName2
            // 
            this.textBoxName2.Enabled = false;
            this.textBoxName2.Location = new System.Drawing.Point(336, 279);
            this.textBoxName2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.textBoxName2.Name = "textBoxName2";
            this.textBoxName2.Size = new System.Drawing.Size(267, 38);
            this.textBoxName2.TabIndex = 6;
            this.textBoxName2.Text = "[Computer]";
            // 
            // radioButtonSize6
            // 
            this.radioButtonSize6.AutoSize = true;
            this.radioButtonSize6.Location = new System.Drawing.Point(60, 81);
            this.radioButtonSize6.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.radioButtonSize6.Name = "radioButtonSize6";
            this.radioButtonSize6.Size = new System.Drawing.Size(112, 36);
            this.radioButtonSize6.TabIndex = 7;
            this.radioButtonSize6.TabStop = true;
            this.radioButtonSize6.Text = "6 x 6";
            this.radioButtonSize6.UseVisualStyleBackColor = true;
            this.radioButtonSize6.Click += new System.EventHandler(this.radioButtonSize_Click);
            // 
            // radioButtonSize10
            // 
            this.radioButtonSize10.AutoSize = true;
            this.radioButtonSize10.Location = new System.Drawing.Point(398, 81);
            this.radioButtonSize10.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.radioButtonSize10.Name = "radioButtonSize10";
            this.radioButtonSize10.Size = new System.Drawing.Size(144, 36);
            this.radioButtonSize10.TabIndex = 8;
            this.radioButtonSize10.TabStop = true;
            this.radioButtonSize10.Text = "10 x 10";
            this.radioButtonSize10.UseVisualStyleBackColor = true;
            this.radioButtonSize10.Click += new System.EventHandler(this.radioButtonSize_Click);
            // 
            // radioButtonSize8
            // 
            this.radioButtonSize8.AutoSize = true;
            this.radioButtonSize8.Location = new System.Drawing.Point(240, 81);
            this.radioButtonSize8.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.radioButtonSize8.Name = "radioButtonSize8";
            this.radioButtonSize8.Size = new System.Drawing.Size(112, 36);
            this.radioButtonSize8.TabIndex = 9;
            this.radioButtonSize8.TabStop = true;
            this.radioButtonSize8.Text = "8 x 8";
            this.radioButtonSize8.UseVisualStyleBackColor = true;
            this.radioButtonSize8.Click += new System.EventHandler(this.radioButtonSize_Click);
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(60, 287);
            this.checkBoxPlayer2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(34, 33);
            this.checkBoxPlayer2.TabIndex = 10;
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(631, 474);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.radioButtonSize8);
            this.Controls.Add(this.radioButtonSize10);
            this.Controls.Add(this.radioButtonSize6);
            this.Controls.Add(this.textBoxName2);
            this.Controls.Add(this.textBoxName1);
            this.Controls.Add(this.LabelPlayer2);
            this.Controls.Add(this.LabelPlayer1);
            this.Controls.Add(this.LabelPlayers);
            this.Controls.Add(this.LabelBoardSize);
            this.Controls.Add(this.buttonPlay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.Name = "FormLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonPlay;
        private System.Windows.Forms.Label LabelBoardSize;
        private System.Windows.Forms.Label LabelPlayers;
        private System.Windows.Forms.Label LabelPlayer1;
        private System.Windows.Forms.Label LabelPlayer2;
        private System.Windows.Forms.TextBox textBoxName1;
        private System.Windows.Forms.TextBox textBoxName2;
        private System.Windows.Forms.RadioButton radioButtonSize6;
        private System.Windows.Forms.RadioButton radioButtonSize10;
        private System.Windows.Forms.RadioButton radioButtonSize8;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
    }
}