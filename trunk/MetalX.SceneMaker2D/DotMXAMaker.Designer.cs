namespace MetalX.SceneMaker2D
{
    partial class DotMXAMaker
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
            this.ui_savepath = new System.Windows.Forms.Button();
            this.ui_loadpath = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ui_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.ui_load = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ui_savepath
            // 
            this.ui_savepath.Location = new System.Drawing.Point(718, 539);
            this.ui_savepath.Name = "ui_savepath";
            this.ui_savepath.Size = new System.Drawing.Size(64, 23);
            this.ui_savepath.TabIndex = 22;
            this.ui_savepath.Text = "批量输出";
            this.ui_savepath.UseVisualStyleBackColor = true;
            // 
            // ui_loadpath
            // 
            this.ui_loadpath.Location = new System.Drawing.Point(718, 512);
            this.ui_loadpath.Name = "ui_loadpath";
            this.ui_loadpath.Size = new System.Drawing.Size(64, 23);
            this.ui_loadpath.TabIndex = 21;
            this.ui_loadpath.Text = "批量目录";
            this.ui_loadpath.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(12, 541);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(640, 21);
            this.textBox2.TabIndex = 20;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 514);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(640, 21);
            this.textBox1.TabIndex = 19;
            // 
            // ui_save
            // 
            this.ui_save.Location = new System.Drawing.Point(658, 539);
            this.ui_save.Name = "ui_save";
            this.ui_save.Size = new System.Drawing.Size(60, 23);
            this.ui_save.TabIndex = 18;
            this.ui_save.Text = "输出";
            this.ui_save.UseVisualStyleBackColor = true;
            this.ui_save.Click += new System.EventHandler(this.ui_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 499);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "label1";
            // 
            // ui_load
            // 
            this.ui_load.Location = new System.Drawing.Point(658, 512);
            this.ui_load.Name = "ui_load";
            this.ui_load.Size = new System.Drawing.Size(60, 23);
            this.ui_load.TabIndex = 16;
            this.ui_load.Text = "载入";
            this.ui_load.UseVisualStyleBackColor = true;
            this.ui_load.Click += new System.EventHandler(this.ui_load_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 460);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 24;
            this.label5.Text = "名字";
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(12, 475);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 21);
            this.textBox5.TabIndex = 23;
            // 
            // DotMXAMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 574);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.ui_savepath);
            this.Controls.Add(this.ui_loadpath);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.ui_save);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ui_load);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DotMXAMaker";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "音频插件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ui_savepath;
        private System.Windows.Forms.Button ui_loadpath;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button ui_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button ui_load;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox5;
    }
}