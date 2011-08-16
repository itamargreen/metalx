namespace MetalX.SceneMaker2D
{
    partial class DotMXMovieMaker
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
            this.ui_load_texture = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.ui_loop = new System.Windows.Forms.CheckBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.ui_x = new System.Windows.Forms.TextBox();
            this.ui_y = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ui_padd = new System.Windows.Forms.Button();
            this.ui_pdel = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.ui_z = new System.Windows.Forms.TextBox();
            this.ui_tp = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ui_bgsound = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.ui_mname = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // ui_load_texture
            // 
            this.ui_load_texture.Location = new System.Drawing.Point(12, 12);
            this.ui_load_texture.Name = "ui_load_texture";
            this.ui_load_texture.Size = new System.Drawing.Size(111, 23);
            this.ui_load_texture.TabIndex = 0;
            this.ui_load_texture.Text = "载入素材";
            this.ui_load_texture.UseVisualStyleBackColor = true;
            this.ui_load_texture.Click += new System.EventHandler(this.ui_load_texture_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 41);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(256, 256);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(694, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(800, 23);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(692, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(798, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "height";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(906, 23);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 7;
            this.textBox3.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(904, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "帧数";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(616, 25);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "垂直排列";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(366, 180);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "设置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(366, 252);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(640, 480);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(444, 86);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 21);
            this.textBox4.TabIndex = 12;
            this.textBox4.Text = "100";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(442, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 13;
            this.label5.Text = "帧间隔（毫秒）";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(366, 223);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 14;
            this.button2.Text = "预览";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(155, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(113, 23);
            this.button3.TabIndex = 15;
            this.button3.Text = "载入MXMovie";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(914, 180);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(92, 66);
            this.button4.TabIndex = 16;
            this.button4.Text = "输出保存";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // ui_loop
            // 
            this.ui_loop.AutoSize = true;
            this.ui_loop.Checked = true;
            this.ui_loop.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ui_loop.Location = new System.Drawing.Point(366, 88);
            this.ui_loop.Name = "ui_loop";
            this.ui_loop.Size = new System.Drawing.Size(48, 16);
            this.ui_loop.TabIndex = 17;
            this.ui_loop.Text = "循环";
            this.ui_loop.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.Enabled = false;
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(530, 146);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(120, 100);
            this.listBox1.TabIndex = 18;
            // 
            // ui_x
            // 
            this.ui_x.Enabled = false;
            this.ui_x.Location = new System.Drawing.Point(720, 121);
            this.ui_x.Name = "ui_x";
            this.ui_x.Size = new System.Drawing.Size(100, 21);
            this.ui_x.TabIndex = 19;
            this.ui_x.Text = "0";
            // 
            // ui_y
            // 
            this.ui_y.Enabled = false;
            this.ui_y.Location = new System.Drawing.Point(720, 148);
            this.ui_y.Name = "ui_y";
            this.ui_y.Size = new System.Drawing.Size(100, 21);
            this.ui_y.TabIndex = 20;
            this.ui_y.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Enabled = false;
            this.label6.Location = new System.Drawing.Point(703, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "x";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(703, 151);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "y";
            // 
            // ui_padd
            // 
            this.ui_padd.Enabled = false;
            this.ui_padd.Location = new System.Drawing.Point(656, 146);
            this.ui_padd.Name = "ui_padd";
            this.ui_padd.Size = new System.Drawing.Size(38, 23);
            this.ui_padd.TabIndex = 23;
            this.ui_padd.Text = "add";
            this.ui_padd.UseVisualStyleBackColor = true;
            this.ui_padd.Click += new System.EventHandler(this.ui_padd_Click);
            // 
            // ui_pdel
            // 
            this.ui_pdel.Enabled = false;
            this.ui_pdel.Location = new System.Drawing.Point(656, 223);
            this.ui_pdel.Name = "ui_pdel";
            this.ui_pdel.Size = new System.Drawing.Size(38, 23);
            this.ui_pdel.TabIndex = 24;
            this.ui_pdel.Text = "del";
            this.ui_pdel.UseVisualStyleBackColor = true;
            this.ui_pdel.Click += new System.EventHandler(this.ui_pdel_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(703, 178);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 26;
            this.label8.Text = "z";
            // 
            // ui_z
            // 
            this.ui_z.Enabled = false;
            this.ui_z.Location = new System.Drawing.Point(720, 175);
            this.ui_z.Name = "ui_z";
            this.ui_z.Size = new System.Drawing.Size(100, 21);
            this.ui_z.TabIndex = 25;
            this.ui_z.Text = "0";
            // 
            // ui_tp
            // 
            this.ui_tp.Enabled = false;
            this.ui_tp.Location = new System.Drawing.Point(897, 121);
            this.ui_tp.Name = "ui_tp";
            this.ui_tp.Size = new System.Drawing.Size(100, 21);
            this.ui_tp.TabIndex = 27;
            this.ui_tp.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(826, 124);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "time_point";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(548, 71);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 12);
            this.label10.TabIndex = 30;
            this.label10.Text = "配音";
            // 
            // ui_bgsound
            // 
            this.ui_bgsound.Location = new System.Drawing.Point(550, 86);
            this.ui_bgsound.Name = "ui_bgsound";
            this.ui_bgsound.Size = new System.Drawing.Size(100, 21);
            this.ui_bgsound.TabIndex = 29;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(364, 8);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 32;
            this.label11.Text = "动画名";
            // 
            // ui_mname
            // 
            this.ui_mname.Location = new System.Drawing.Point(366, 23);
            this.ui_mname.Name = "ui_mname";
            this.ui_mname.Size = new System.Drawing.Size(100, 21);
            this.ui_mname.TabIndex = 31;
            // 
            // DotMXMovieMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 744);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.ui_mname);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ui_bgsound);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.ui_tp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ui_z);
            this.Controls.Add(this.ui_pdel);
            this.Controls.Add(this.ui_padd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ui_y);
            this.Controls.Add(this.ui_x);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.ui_loop);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ui_load_texture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "DotMXMovieMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DotMXMovieMaker";
            this.Load += new System.EventHandler(this.DotMXMovieMaker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ui_load_texture;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox ui_loop;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox ui_x;
        private System.Windows.Forms.TextBox ui_y;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button ui_padd;
        private System.Windows.Forms.Button ui_pdel;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox ui_z;
        private System.Windows.Forms.TextBox ui_tp;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox ui_bgsound;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox ui_mname;
    }
}