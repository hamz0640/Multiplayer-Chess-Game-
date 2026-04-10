namespace Programmeringseksamens_projekt
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
            this.hostButton = new System.Windows.Forms.Button();
            this.ipEnterField = new System.Windows.Forms.TextBox();
            this.joinField = new System.Windows.Forms.Button();
            this.drawButton = new System.Windows.Forms.Button();
            this.resignButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // hostButton
            // 
            this.hostButton.Location = new System.Drawing.Point(484, 43);
            this.hostButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.hostButton.Name = "hostButton";
            this.hostButton.Size = new System.Drawing.Size(145, 48);
            this.hostButton.TabIndex = 1;
            this.hostButton.Text = "Host Game";
            this.hostButton.UseVisualStyleBackColor = true;
            this.hostButton.Click += new System.EventHandler(this.hostButton_Click);
            // 
            // ipEnterField
            // 
            this.ipEnterField.Location = new System.Drawing.Point(484, 14);
            this.ipEnterField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ipEnterField.Name = "ipEnterField";
            this.ipEnterField.Size = new System.Drawing.Size(300, 22);
            this.ipEnterField.TabIndex = 0;
            // 
            // joinField
            // 
            this.joinField.Location = new System.Drawing.Point(640, 43);
            this.joinField.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.joinField.Name = "joinField";
            this.joinField.Size = new System.Drawing.Size(145, 48);
            this.joinField.TabIndex = 2;
            this.joinField.Text = "Join Game";
            this.joinField.UseVisualStyleBackColor = true;
            this.joinField.Click += new System.EventHandler(this.joinButton_Click);
            // 
            // drawButton
            // 
            this.drawButton.Location = new System.Drawing.Point(484, 118);
            this.drawButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(301, 48);
            this.drawButton.TabIndex = 3;
            this.drawButton.Text = "Offer Draw";
            this.drawButton.UseVisualStyleBackColor = true;
            // 
            // resignButton
            // 
            this.resignButton.Location = new System.Drawing.Point(485, 171);
            this.resignButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.resignButton.Name = "resignButton";
            this.resignButton.Size = new System.Drawing.Size(300, 48);
            this.resignButton.TabIndex = 4;
            this.resignButton.Text = "Resign";
            this.resignButton.UseVisualStyleBackColor = true;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(484, 246);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(300, 190);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Sienna;
            this.pictureBox1.Image = global::Programmeringseksamens_projekt.Properties.Resources.wR;
            this.pictureBox1.Location = new System.Drawing.Point(54, 103);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 50);
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.resignButton);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.joinField);
            this.Controls.Add(this.ipEnterField);
            this.Controls.Add(this.hostButton);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button hostButton;
        private System.Windows.Forms.TextBox ipEnterField;
        private System.Windows.Forms.Button joinField;
        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.Button resignButton;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

