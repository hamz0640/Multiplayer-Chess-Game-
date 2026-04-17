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
            this.components = new System.ComponentModel.Container();
            this.hostButton = new System.Windows.Forms.Button();
            this.ipEnterField = new System.Windows.Forms.TextBox();
            this.joinField = new System.Windows.Forms.Button();
            this.drawButton = new System.Windows.Forms.Button();
            this.resignButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.checkNetworkTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // hostButton
            // 
            this.hostButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(33)))), ((int)(((byte)(37)))));
            this.hostButton.FlatAppearance.BorderSize = 0;
            this.hostButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hostButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.hostButton.Location = new System.Drawing.Point(520, 43);
            this.hostButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.hostButton.Name = "hostButton";
            this.hostButton.Size = new System.Drawing.Size(145, 48);
            this.hostButton.TabIndex = 1;
            this.hostButton.Text = "Host Game";
            this.hostButton.UseVisualStyleBackColor = false;
            this.hostButton.Click += new System.EventHandler(this.hostButton_Click);
            // 
            // ipEnterField
            // 
            this.ipEnterField.Location = new System.Drawing.Point(520, 15);
            this.ipEnterField.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.ipEnterField.Name = "ipEnterField";
            this.ipEnterField.Size = new System.Drawing.Size(300, 22);
            this.ipEnterField.TabIndex = 0;
            // 
            // joinField
            // 
            this.joinField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(33)))), ((int)(((byte)(37)))));
            this.joinField.FlatAppearance.BorderSize = 0;
            this.joinField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.joinField.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.joinField.Location = new System.Drawing.Point(676, 43);
            this.joinField.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.joinField.Name = "joinField";
            this.joinField.Size = new System.Drawing.Size(145, 48);
            this.joinField.TabIndex = 2;
            this.joinField.Text = "Join Game";
            this.joinField.UseVisualStyleBackColor = false;
            this.joinField.Click += new System.EventHandler(this.joinButton_Click);
            // 
            // drawButton
            // 
            this.drawButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(33)))), ((int)(((byte)(37)))));
            this.drawButton.FlatAppearance.BorderSize = 0;
            this.drawButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.drawButton.Location = new System.Drawing.Point(520, 118);
            this.drawButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(301, 48);
            this.drawButton.TabIndex = 3;
            this.drawButton.Text = "Offer Draw";
            this.drawButton.UseVisualStyleBackColor = false;
            // 
            // resignButton
            // 
            this.resignButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(33)))), ((int)(((byte)(37)))));
            this.resignButton.FlatAppearance.BorderSize = 0;
            this.resignButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resignButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.resignButton.Location = new System.Drawing.Point(521, 171);
            this.resignButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.resignButton.Name = "resignButton";
            this.resignButton.Size = new System.Drawing.Size(300, 48);
            this.resignButton.TabIndex = 4;
            this.resignButton.Text = "Resign";
            this.resignButton.UseVisualStyleBackColor = false;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(520, 246);
            this.listView1.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(300, 190);
            this.listView1.TabIndex = 5;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // checkNetworkTimer
            // 
            this.checkNetworkTimer.Enabled = true;
            this.checkNetworkTimer.Interval = 1000;
            this.checkNetworkTimer.Tick += new System.EventHandler(this.checkNetworkTimerTick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(891, 486);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.resignButton);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.joinField);
            this.Controls.Add(this.ipEnterField);
            this.Controls.Add(this.hostButton);
            this.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.Name = "Form1";
            this.Text = "Form1";
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
        private System.Windows.Forms.Timer checkNetworkTimer;
    }
}

