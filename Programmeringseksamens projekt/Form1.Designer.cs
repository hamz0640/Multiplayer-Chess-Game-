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
            this.moveList = new System.Windows.Forms.ListView();
            this.checkNetworkTimer = new System.Windows.Forms.Timer(this.components);
            this.MoveHistoryBackground = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.resignButton = new System.Windows.Forms.Button();
            this.drawButton = new System.Windows.Forms.Button();
            this.CurrentTurn = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.Wp = new System.Windows.Forms.Label();
            this.Wn = new System.Windows.Forms.Label();
            this.Wb = new System.Windows.Forms.Label();
            this.Wr = new System.Windows.Forms.Label();
            this.Wq = new System.Windows.Forms.Label();
            this.Bp = new System.Windows.Forms.Label();
            this.Bn = new System.Windows.Forms.Label();
            this.Bb = new System.Windows.Forms.Label();
            this.Br = new System.Windows.Forms.Label();
            this.Bq = new System.Windows.Forms.Label();
            this.WpC = new System.Windows.Forms.Label();
            this.BpC = new System.Windows.Forms.Label();
            this.BnC = new System.Windows.Forms.Label();
            this.BbC = new System.Windows.Forms.Label();
            this.BrC = new System.Windows.Forms.Label();
            this.BqC = new System.Windows.Forms.Label();
            this.WnC = new System.Windows.Forms.Label();
            this.WbC = new System.Windows.Forms.Label();
            this.WrC = new System.Windows.Forms.Label();
            this.WqC = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // hostButton
            // 
            this.hostButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.hostButton.FlatAppearance.BorderSize = 0;
            this.hostButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hostButton.Font = new System.Drawing.Font("Berlin Sans FB Demi", 12F);
            this.hostButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.hostButton.Location = new System.Drawing.Point(486, 74);
            this.hostButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.hostButton.Name = "hostButton";
            this.hostButton.Size = new System.Drawing.Size(98, 35);
            this.hostButton.TabIndex = 1;
            this.hostButton.Text = "Host Game";
            this.hostButton.UseVisualStyleBackColor = false;
            this.hostButton.Click += new System.EventHandler(this.hostButton_Click);
            // 
            // ipEnterField
            // 
            this.ipEnterField.Location = new System.Drawing.Point(486, 42);
            this.ipEnterField.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.ipEnterField.Name = "ipEnterField";
            this.ipEnterField.Size = new System.Drawing.Size(219, 21);
            this.ipEnterField.TabIndex = 0;
            this.ipEnterField.Text = "127.0.0.1";
            // 
            // joinField
            // 
            this.joinField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.joinField.FlatAppearance.BorderSize = 0;
            this.joinField.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.joinField.Font = new System.Drawing.Font("Berlin Sans FB Demi", 12F);
            this.joinField.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.joinField.Location = new System.Drawing.Point(607, 74);
            this.joinField.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.joinField.Name = "joinField";
            this.joinField.Size = new System.Drawing.Size(98, 35);
            this.joinField.TabIndex = 2;
            this.joinField.Text = "Join Game";
            this.joinField.UseVisualStyleBackColor = false;
            this.joinField.Click += new System.EventHandler(this.joinButton_Click);
            // 
            // moveList
            // 
            this.moveList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.moveList.HideSelection = false;
            this.moveList.Location = new System.Drawing.Point(500, 213);
            this.moveList.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.moveList.Name = "moveList";
            this.moveList.Size = new System.Drawing.Size(192, 194);
            this.moveList.TabIndex = 5;
            this.moveList.UseCompatibleStateImageBehavior = false;
            // 
            // checkNetworkTimer
            // 
            this.checkNetworkTimer.Enabled = true;
            this.checkNetworkTimer.Interval = 1000;
            this.checkNetworkTimer.Tick += new System.EventHandler(this.checkNetworkTimerTick);
            // 
            // MoveHistoryBackground
            // 
            this.MoveHistoryBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.MoveHistoryBackground.Location = new System.Drawing.Point(488, 174);
            this.MoveHistoryBackground.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.MoveHistoryBackground.Name = "MoveHistoryBackground";
            this.MoveHistoryBackground.Size = new System.Drawing.Size(217, 247);
            this.MoveHistoryBackground.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.label2.Font = new System.Drawing.Font("Berlin Sans FB Demi", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(499, 184);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 29);
            this.label2.TabIndex = 7;
            this.label2.Text = "Move History";
            // 
            // resignButton
            // 
            this.resignButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.resignButton.FlatAppearance.BorderSize = 0;
            this.resignButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.resignButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.resignButton.Image = global::Programmeringseksamens_projekt.Properties.Resources.flag_48;
            this.resignButton.Location = new System.Drawing.Point(642, 121);
            this.resignButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.resignButton.Name = "resignButton";
            this.resignButton.Size = new System.Drawing.Size(50, 42);
            this.resignButton.TabIndex = 4;
            this.resignButton.UseVisualStyleBackColor = false;
            this.resignButton.Click += new System.EventHandler(this.resignButton_Click);
            // 
            // drawButton
            // 
            this.drawButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.drawButton.FlatAppearance.BorderSize = 0;
            this.drawButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.drawButton.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.drawButton.Image = global::Programmeringseksamens_projekt.Properties.Resources.handshake_48;
            this.drawButton.Location = new System.Drawing.Point(502, 121);
            this.drawButton.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.drawButton.Name = "drawButton";
            this.drawButton.Size = new System.Drawing.Size(52, 42);
            this.drawButton.TabIndex = 3;
            this.drawButton.UseVisualStyleBackColor = false;
            this.drawButton.Click += new System.EventHandler(this.drawButton_Click);
            // 
            // CurrentTurn
            // 
            this.CurrentTurn.AutoSize = true;
            this.CurrentTurn.Font = new System.Drawing.Font("Berlin Sans FB Demi", 12F);
            this.CurrentTurn.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.CurrentTurn.Location = new System.Drawing.Point(96, 59);
            this.CurrentTurn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.CurrentTurn.Name = "CurrentTurn";
            this.CurrentTurn.Size = new System.Drawing.Size(143, 24);
            this.CurrentTurn.TabIndex = 8;
            this.CurrentTurn.Text = "White to move";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.label1.Font = new System.Drawing.Font("Berlin Sans FB Demi", 9F);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(674, 192);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 18);
            this.label1.TabIndex = 9;
            this.label1.Text = "?/?";
            // 
            // TitleLabel
            // 
            this.TitleLabel.Font = new System.Drawing.Font("Berlin Sans FB Demi", 25F);
            this.TitleLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.TitleLabel.Image = global::Programmeringseksamens_projekt.Properties.Resources.wK;
            this.TitleLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.TitleLabel.Location = new System.Drawing.Point(63, 9);
            this.TitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.TitleLabel.Size = new System.Drawing.Size(419, 36);
            this.TitleLabel.TabIndex = 12;
            this.TitleLabel.Text = "Ultimate Chess Pro 2026";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Berlin Sans FB Demi", 12F);
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(10, 59);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 24);
            this.label3.TabIndex = 13;
            this.label3.Text = "White";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Berlin Sans FB Demi", 12F);
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(10, 270);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 24);
            this.label4.TabIndex = 14;
            this.label4.Text = "Black";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.label5.Location = new System.Drawing.Point(7, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 154);
            this.label5.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.label6.Location = new System.Drawing.Point(7, 294);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(51, 154);
            this.label6.TabIndex = 16;
            // 
            // Wp
            // 
            this.Wp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Wp.Image = global::Programmeringseksamens_projekt.Properties.Resources.wP;
            this.Wp.Location = new System.Drawing.Point(10, 87);
            this.Wp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Wp.Name = "Wp";
            this.Wp.Size = new System.Drawing.Size(28, 30);
            this.Wp.TabIndex = 17;
            // 
            // Wn
            // 
            this.Wn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Wn.Image = global::Programmeringseksamens_projekt.Properties.Resources.wN;
            this.Wn.Location = new System.Drawing.Point(10, 117);
            this.Wn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Wn.Name = "Wn";
            this.Wn.Size = new System.Drawing.Size(28, 30);
            this.Wn.TabIndex = 18;
            // 
            // Wb
            // 
            this.Wb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Wb.Image = global::Programmeringseksamens_projekt.Properties.Resources.wB;
            this.Wb.Location = new System.Drawing.Point(10, 148);
            this.Wb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Wb.Name = "Wb";
            this.Wb.Size = new System.Drawing.Size(28, 30);
            this.Wb.TabIndex = 19;
            // 
            // Wr
            // 
            this.Wr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Wr.Image = global::Programmeringseksamens_projekt.Properties.Resources.wR;
            this.Wr.Location = new System.Drawing.Point(10, 178);
            this.Wr.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Wr.Name = "Wr";
            this.Wr.Size = new System.Drawing.Size(28, 30);
            this.Wr.TabIndex = 20;
            // 
            // Wq
            // 
            this.Wq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Wq.Image = global::Programmeringseksamens_projekt.Properties.Resources.wQ;
            this.Wq.Location = new System.Drawing.Point(10, 209);
            this.Wq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Wq.Name = "Wq";
            this.Wq.Size = new System.Drawing.Size(28, 30);
            this.Wq.TabIndex = 21;
            // 
            // Bp
            // 
            this.Bp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Bp.Image = global::Programmeringseksamens_projekt.Properties.Resources.bP;
            this.Bp.Location = new System.Drawing.Point(10, 294);
            this.Bp.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Bp.Name = "Bp";
            this.Bp.Size = new System.Drawing.Size(28, 30);
            this.Bp.TabIndex = 22;
            // 
            // Bn
            // 
            this.Bn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Bn.Image = global::Programmeringseksamens_projekt.Properties.Resources.bN;
            this.Bn.Location = new System.Drawing.Point(10, 325);
            this.Bn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Bn.Name = "Bn";
            this.Bn.Size = new System.Drawing.Size(28, 30);
            this.Bn.TabIndex = 23;
            // 
            // Bb
            // 
            this.Bb.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Bb.Image = global::Programmeringseksamens_projekt.Properties.Resources.bB;
            this.Bb.Location = new System.Drawing.Point(10, 355);
            this.Bb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Bb.Name = "Bb";
            this.Bb.Size = new System.Drawing.Size(28, 30);
            this.Bb.TabIndex = 24;
            // 
            // Br
            // 
            this.Br.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Br.Image = global::Programmeringseksamens_projekt.Properties.Resources.bR;
            this.Br.Location = new System.Drawing.Point(10, 386);
            this.Br.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Br.Name = "Br";
            this.Br.Size = new System.Drawing.Size(28, 30);
            this.Br.TabIndex = 25;
            // 
            // Bq
            // 
            this.Bq.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.Bq.Image = global::Programmeringseksamens_projekt.Properties.Resources.bQ;
            this.Bq.Location = new System.Drawing.Point(10, 416);
            this.Bq.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Bq.Name = "Bq";
            this.Bq.Size = new System.Drawing.Size(28, 30);
            this.Bq.TabIndex = 26;
            // 
            // WpC
            // 
            this.WpC.AutoSize = true;
            this.WpC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.WpC.Location = new System.Drawing.Point(36, 105);
            this.WpC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WpC.Name = "WpC";
            this.WpC.Size = new System.Drawing.Size(21, 13);
            this.WpC.TabIndex = 27;
            this.WpC.Text = "x0";
            // 
            // BpC
            // 
            this.BpC.AutoSize = true;
            this.BpC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.BpC.Location = new System.Drawing.Point(36, 313);
            this.BpC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BpC.Name = "BpC";
            this.BpC.Size = new System.Drawing.Size(21, 13);
            this.BpC.TabIndex = 28;
            this.BpC.Text = "x0";
            // 
            // BnC
            // 
            this.BnC.AutoSize = true;
            this.BnC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.BnC.Location = new System.Drawing.Point(36, 343);
            this.BnC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BnC.Name = "BnC";
            this.BnC.Size = new System.Drawing.Size(21, 13);
            this.BnC.TabIndex = 29;
            this.BnC.Text = "x0";
            // 
            // BbC
            // 
            this.BbC.AutoSize = true;
            this.BbC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.BbC.Location = new System.Drawing.Point(36, 373);
            this.BbC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BbC.Name = "BbC";
            this.BbC.Size = new System.Drawing.Size(21, 13);
            this.BbC.TabIndex = 30;
            this.BbC.Text = "x0";
            // 
            // BrC
            // 
            this.BrC.AutoSize = true;
            this.BrC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.BrC.Location = new System.Drawing.Point(36, 404);
            this.BrC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BrC.Name = "BrC";
            this.BrC.Size = new System.Drawing.Size(21, 13);
            this.BrC.TabIndex = 31;
            this.BrC.Text = "x0";
            // 
            // BqC
            // 
            this.BqC.AutoSize = true;
            this.BqC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.BqC.Location = new System.Drawing.Point(36, 436);
            this.BqC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BqC.Name = "BqC";
            this.BqC.Size = new System.Drawing.Size(21, 13);
            this.BqC.TabIndex = 32;
            this.BqC.Text = "x0";
            // 
            // WnC
            // 
            this.WnC.AutoSize = true;
            this.WnC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.WnC.Location = new System.Drawing.Point(36, 135);
            this.WnC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WnC.Name = "WnC";
            this.WnC.Size = new System.Drawing.Size(21, 13);
            this.WnC.TabIndex = 33;
            this.WnC.Text = "x0";
            // 
            // WbC
            // 
            this.WbC.AutoSize = true;
            this.WbC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.WbC.Location = new System.Drawing.Point(36, 166);
            this.WbC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WbC.Name = "WbC";
            this.WbC.Size = new System.Drawing.Size(21, 13);
            this.WbC.TabIndex = 34;
            this.WbC.Text = "x0";
            // 
            // WrC
            // 
            this.WrC.AutoSize = true;
            this.WrC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.WrC.Location = new System.Drawing.Point(36, 197);
            this.WrC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WrC.Name = "WrC";
            this.WrC.Size = new System.Drawing.Size(21, 13);
            this.WrC.TabIndex = 35;
            this.WrC.Text = "x0";
            // 
            // WqC
            // 
            this.WqC.AutoSize = true;
            this.WqC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(58)))), ((int)(((byte)(57)))));
            this.WqC.Location = new System.Drawing.Point(36, 229);
            this.WqC.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WqC.Name = "WqC";
            this.WqC.Size = new System.Drawing.Size(21, 13);
            this.WqC.TabIndex = 36;
            this.WqC.Text = "x0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(45)))));
            this.ClientSize = new System.Drawing.Size(732, 485);
            this.Controls.Add(this.WqC);
            this.Controls.Add(this.WrC);
            this.Controls.Add(this.WbC);
            this.Controls.Add(this.WnC);
            this.Controls.Add(this.BqC);
            this.Controls.Add(this.BrC);
            this.Controls.Add(this.BbC);
            this.Controls.Add(this.BnC);
            this.Controls.Add(this.BpC);
            this.Controls.Add(this.WpC);
            this.Controls.Add(this.Bq);
            this.Controls.Add(this.Br);
            this.Controls.Add(this.Bb);
            this.Controls.Add(this.Bn);
            this.Controls.Add(this.Bp);
            this.Controls.Add(this.Wq);
            this.Controls.Add(this.Wr);
            this.Controls.Add(this.Wb);
            this.Controls.Add(this.Wn);
            this.Controls.Add(this.Wp);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CurrentTurn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.moveList);
            this.Controls.Add(this.resignButton);
            this.Controls.Add(this.drawButton);
            this.Controls.Add(this.joinField);
            this.Controls.Add(this.ipEnterField);
            this.Controls.Add(this.hostButton);
            this.Controls.Add(this.MoveHistoryBackground);
            this.Font = new System.Drawing.Font("Berlin Sans FB Demi", 7F);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.Name = "Form1";
            this.Text = "Ultimate Chess Pro 2026 ♔";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button hostButton;
        private System.Windows.Forms.TextBox ipEnterField;
        private System.Windows.Forms.Button joinField;
        private System.Windows.Forms.Button drawButton;
        private System.Windows.Forms.Button resignButton;
        private System.Windows.Forms.ListView moveList;
        private System.Windows.Forms.Timer checkNetworkTimer;
        private System.Windows.Forms.Label MoveHistoryBackground;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label CurrentTurn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label Wp;
        private System.Windows.Forms.Label Wn;
        private System.Windows.Forms.Label Wb;
        private System.Windows.Forms.Label Wr;
        private System.Windows.Forms.Label Wq;
        private System.Windows.Forms.Label Bp;
        private System.Windows.Forms.Label Bn;
        private System.Windows.Forms.Label Bb;
        private System.Windows.Forms.Label Br;
        private System.Windows.Forms.Label Bq;
        private System.Windows.Forms.Label WpC;
        private System.Windows.Forms.Label BpC;
        private System.Windows.Forms.Label BnC;
        private System.Windows.Forms.Label BbC;
        private System.Windows.Forms.Label BrC;
        private System.Windows.Forms.Label BqC;
        private System.Windows.Forms.Label WnC;
        private System.Windows.Forms.Label WbC;
        private System.Windows.Forms.Label WrC;
        private System.Windows.Forms.Label WqC;
    }
}

