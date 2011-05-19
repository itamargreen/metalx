namespace MetalX.SceneMaker2D
{
    partial class DotMXMMaker
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.ui_load = new System.Windows.Forms.Button();
            this.ui_pack = new System.Windows.Forms.Button();
            this.ui_loadmodel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 480);
            this.panel1.TabIndex = 0;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // ui_load
            // 
            this.ui_load.Location = new System.Drawing.Point(12, 498);
            this.ui_load.Name = "ui_load";
            this.ui_load.Size = new System.Drawing.Size(111, 23);
            this.ui_load.TabIndex = 1;
            this.ui_load.Text = "载入.X文件";
            this.ui_load.UseVisualStyleBackColor = true;
            this.ui_load.Click += new System.EventHandler(this.ui_load_Click);
            // 
            // ui_pack
            // 
            this.ui_pack.Location = new System.Drawing.Point(129, 539);
            this.ui_pack.Name = "ui_pack";
            this.ui_pack.Size = new System.Drawing.Size(111, 23);
            this.ui_pack.TabIndex = 2;
            this.ui_pack.Text = "打包.MXM文件";
            this.ui_pack.UseVisualStyleBackColor = true;
            this.ui_pack.Click += new System.EventHandler(this.ui_pack_Click);
            // 
            // ui_loadmodel
            // 
            this.ui_loadmodel.Location = new System.Drawing.Point(12, 539);
            this.ui_loadmodel.Name = "ui_loadmodel";
            this.ui_loadmodel.Size = new System.Drawing.Size(111, 23);
            this.ui_loadmodel.TabIndex = 3;
            this.ui_loadmodel.Text = "载入.MXM文件";
            this.ui_loadmodel.UseVisualStyleBackColor = true;
            this.ui_loadmodel.Click += new System.EventHandler(this.ui_loadmodel_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(129, 498);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "载入.MXT文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(659, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "label1";
            // 
            // DotMXMMaker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 574);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ui_loadmodel);
            this.Controls.Add(this.ui_pack);
            this.Controls.Add(this.ui_load);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DotMXMMaker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MetalX.ModelViewer";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button ui_load;
        private System.Windows.Forms.Button ui_pack;
        private System.Windows.Forms.Button ui_loadmodel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}

