namespace MetalX.SceneMaker2D
{
    partial class MonsterMaker
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
            this.label1 = new System.Windows.Forms.Label();
            this.ui_w = new System.Windows.Forms.TextBox();
            this.ui_h = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ui_load = new System.Windows.Forms.Button();
            this.ui_save = new System.Windows.Forms.Button();
            this.ui_stand = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ui_defense = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ui_hit = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ui_fight = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ui_fire = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ui_throw = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ui_yes = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "宽";
            // 
            // ui_w
            // 
            this.ui_w.Location = new System.Drawing.Point(12, 84);
            this.ui_w.Name = "ui_w";
            this.ui_w.Size = new System.Drawing.Size(100, 21);
            this.ui_w.TabIndex = 1;
            // 
            // ui_h
            // 
            this.ui_h.Location = new System.Drawing.Point(118, 84);
            this.ui_h.Name = "ui_h";
            this.ui_h.Size = new System.Drawing.Size(100, 21);
            this.ui_h.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(116, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "高";
            // 
            // ui_load
            // 
            this.ui_load.Location = new System.Drawing.Point(12, 12);
            this.ui_load.Name = "ui_load";
            this.ui_load.Size = new System.Drawing.Size(75, 23);
            this.ui_load.TabIndex = 4;
            this.ui_load.Text = "载入";
            this.ui_load.UseVisualStyleBackColor = true;
            this.ui_load.Click += new System.EventHandler(this.ui_load_Click);
            // 
            // ui_save
            // 
            this.ui_save.Location = new System.Drawing.Point(931, 12);
            this.ui_save.Name = "ui_save";
            this.ui_save.Size = new System.Drawing.Size(75, 23);
            this.ui_save.TabIndex = 5;
            this.ui_save.Text = "输出";
            this.ui_save.UseVisualStyleBackColor = true;
            this.ui_save.Click += new System.EventHandler(this.ui_save_Click);
            // 
            // ui_stand
            // 
            this.ui_stand.Location = new System.Drawing.Point(12, 154);
            this.ui_stand.Name = "ui_stand";
            this.ui_stand.Size = new System.Drawing.Size(100, 21);
            this.ui_stand.TabIndex = 7;
            this.ui_stand.DoubleClick += new System.EventHandler(this.ui_stand_DoubleClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 139);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "站立动画";
            // 
            // ui_defense
            // 
            this.ui_defense.Location = new System.Drawing.Point(12, 193);
            this.ui_defense.Name = "ui_defense";
            this.ui_defense.Size = new System.Drawing.Size(100, 21);
            this.ui_defense.TabIndex = 9;
            this.ui_defense.DoubleClick += new System.EventHandler(this.ui_defense_DoubleClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 178);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "防御动画";
            // 
            // ui_hit
            // 
            this.ui_hit.Location = new System.Drawing.Point(12, 232);
            this.ui_hit.Name = "ui_hit";
            this.ui_hit.Size = new System.Drawing.Size(100, 21);
            this.ui_hit.TabIndex = 11;
            this.ui_hit.DoubleClick += new System.EventHandler(this.ui_hit_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 217);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "被击中动画";
            // 
            // ui_fight
            // 
            this.ui_fight.Location = new System.Drawing.Point(12, 271);
            this.ui_fight.Name = "ui_fight";
            this.ui_fight.Size = new System.Drawing.Size(100, 21);
            this.ui_fight.TabIndex = 13;
            this.ui_fight.DoubleClick += new System.EventHandler(this.ui_fight_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "格斗动画";
            // 
            // ui_fire
            // 
            this.ui_fire.Location = new System.Drawing.Point(12, 310);
            this.ui_fire.Name = "ui_fire";
            this.ui_fire.Size = new System.Drawing.Size(100, 21);
            this.ui_fire.TabIndex = 15;
            this.ui_fire.DoubleClick += new System.EventHandler(this.ui_fire_DoubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 295);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 14;
            this.label7.Text = "开火动画";
            // 
            // ui_throw
            // 
            this.ui_throw.Location = new System.Drawing.Point(12, 349);
            this.ui_throw.Name = "ui_throw";
            this.ui_throw.Size = new System.Drawing.Size(100, 21);
            this.ui_throw.TabIndex = 17;
            this.ui_throw.DoubleClick += new System.EventHandler(this.ui_throw_DoubleClick);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 334);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 16;
            this.label8.Text = "投掷动画";
            // 
            // ui_yes
            // 
            this.ui_yes.Location = new System.Drawing.Point(680, 12);
            this.ui_yes.Name = "ui_yes";
            this.ui_yes.Size = new System.Drawing.Size(75, 23);
            this.ui_yes.TabIndex = 18;
            this.ui_yes.Text = "确定";
            this.ui_yes.UseVisualStyleBackColor = true;
            this.ui_yes.Click += new System.EventHandler(this.ui_yes_Click);
            // 
            // MonsterMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 744);
            this.Controls.Add(this.ui_yes);
            this.Controls.Add(this.ui_throw);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.ui_fire);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ui_fight);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ui_hit);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.ui_defense);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ui_stand);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ui_save);
            this.Controls.Add(this.ui_load);
            this.Controls.Add(this.ui_h);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ui_w);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MonsterMaker";
            this.Text = "MonsterMaker";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ui_w;
        private System.Windows.Forms.TextBox ui_h;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ui_load;
        private System.Windows.Forms.Button ui_save;
        private System.Windows.Forms.TextBox ui_stand;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ui_defense;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ui_hit;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ui_fight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox ui_fire;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox ui_throw;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button ui_yes;
    }
}