using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MetalMax.Ripper
{
public class addpoint : Form
{
    // Fields
    private Button button1;
    private Button button2;
    private IContainer components;
    private Label label19;
    private Label label20;
    private Label label21;
    private Label label23;
    private Label label24;
    public int map_X;
    public int map_Y;
    public int mapid;
    private NumericUpDown nu_point_x;
    private NumericUpDown un_point_map;
    private NumericUpDown un_point_x1;
    private NumericUpDown un_point_y;
    private NumericUpDown un_point_y1;
    public int X;
    public int Y;

    // Methods
    public addpoint()
    {
        this.InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        this.X = (int) this.nu_point_x.Value;
        this.Y = (int) this.un_point_y.Value;
        this.mapid = (int) this.un_point_map.Value;
        this.map_X = (int) this.un_point_x1.Value;
        this.map_Y = (int) this.un_point_y1.Value;
        base.DialogResult = DialogResult.OK;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.un_point_map = new NumericUpDown();
        this.un_point_x1 = new NumericUpDown();
        this.nu_point_x = new NumericUpDown();
        this.label24 = new Label();
        this.label20 = new Label();
        this.label21 = new Label();
        this.label23 = new Label();
        this.label19 = new Label();
        this.un_point_y1 = new NumericUpDown();
        this.un_point_y = new NumericUpDown();
        this.button1 = new Button();
        this.button2 = new Button();
        this.un_point_map.BeginInit();
        this.un_point_x1.BeginInit();
        this.nu_point_x.BeginInit();
        this.un_point_y1.BeginInit();
        this.un_point_y.BeginInit();
        base.SuspendLayout();
        this.un_point_map.Hexadecimal = true;
        this.un_point_map.Location = new Point(150, 0x6a);
        int[] bits = new int[4];
        bits[0] = 0xef;
        this.un_point_map.Maximum = new decimal(bits);
        this.un_point_map.Name = "un_point_map";
        this.un_point_map.Size = new Size(0x33, 0x15);
        this.un_point_map.TabIndex = 10;
        this.un_point_x1.Hexadecimal = true;
        this.un_point_x1.Location = new Point(150, 0x85);
        int[] numArray2 = new int[4];
        numArray2[0] = 0xff;
        this.un_point_x1.Maximum = new decimal(numArray2);
        this.un_point_x1.Name = "un_point_x1";
        this.un_point_x1.Size = new Size(0x33, 0x15);
        this.un_point_x1.TabIndex = 11;
        this.nu_point_x.Hexadecimal = true;
        this.nu_point_x.Location = new Point(150, 0x1a);
        int[] numArray3 = new int[4];
        numArray3[0] = 0xff;
        this.nu_point_x.Maximum = new decimal(numArray3);
        this.nu_point_x.Name = "nu_point_x";
        this.nu_point_x.Size = new Size(0x33, 0x15);
        this.nu_point_x.TabIndex = 12;
        this.label24.AutoSize = true;
        this.label24.Location = new Point(12, 0xa4);
        this.label24.Name = "label24";
        this.label24.Size = new Size(0x83, 12);
        this.label24.TabIndex = 13;
        this.label24.Text = "切换地图后人物的初始Y";
        this.label20.AutoSize = true;
        this.label20.Location = new Point(0x54, 0x38);
        this.label20.Name = "label20";
        this.label20.Size = new Size(0x3b, 12);
        this.label20.TabIndex = 14;
        this.label20.Text = "入口坐标Y";
        this.label21.AutoSize = true;
        this.label21.Location = new Point(0x36, 110);
        this.label21.Name = "label21";
        this.label21.Size = new Size(0x59, 12);
        this.label21.TabIndex = 0x11;
        this.label21.Text = "切换到地图编号";
        this.label23.AutoSize = true;
        this.label23.Location = new Point(12, 0x88);
        this.label23.Name = "label23";
        this.label23.Size = new Size(0x83, 12);
        this.label23.TabIndex = 0x12;
        this.label23.Text = "切换地图后人物的初始X";
        this.label19.AutoSize = true;
        this.label19.Location = new Point(0x54, 0x1c);
        this.label19.Name = "label19";
        this.label19.Size = new Size(0x3b, 12);
        this.label19.TabIndex = 0x10;
        this.label19.Text = "入口坐标X";
        this.un_point_y1.Hexadecimal = true;
        this.un_point_y1.Location = new Point(150, 160);
        int[] numArray4 = new int[4];
        numArray4[0] = 0xff;
        this.un_point_y1.Maximum = new decimal(numArray4);
        this.un_point_y1.Name = "un_point_y1";
        this.un_point_y1.Size = new Size(0x33, 0x15);
        this.un_point_y1.TabIndex = 8;
        this.un_point_y.Hexadecimal = true;
        this.un_point_y.Location = new Point(150, 0x36);
        int[] numArray5 = new int[4];
        numArray5[0] = 0xff;
        this.un_point_y.Maximum = new decimal(numArray5);
        this.un_point_y.Name = "un_point_y";
        this.un_point_y.Size = new Size(0x33, 0x15);
        this.un_point_y.TabIndex = 9;
        this.button1.Location = new Point(0xa4, 0xd7);
        this.button1.Name = "button1";
        this.button1.Size = new Size(0x33, 0x17);
        this.button1.TabIndex = 0x13;
        this.button1.Text = "确定";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new EventHandler(this.button1_Click);
        this.button2.DialogResult = DialogResult.Cancel;
        this.button2.Location = new Point(0xdd, 0xd7);
        this.button2.Name = "button2";
        this.button2.Size = new Size(0x33, 0x17);
        this.button2.TabIndex = 0x13;
        this.button2.Text = "取消";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new EventHandler(this.button2_Click);
        base.AcceptButton = this.button1;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton = this.button2;
        base.ClientSize = new Size(0x113, 0xf2);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.un_point_map);
        base.Controls.Add(this.un_point_x1);
        base.Controls.Add(this.nu_point_x);
        base.Controls.Add(this.label24);
        base.Controls.Add(this.label20);
        base.Controls.Add(this.label21);
        base.Controls.Add(this.label23);
        base.Controls.Add(this.label19);
        base.Controls.Add(this.un_point_y1);
        base.Controls.Add(this.un_point_y);
        base.Name = "addpoint";
        this.Text = "添加入口";
        this.un_point_map.EndInit();
        this.un_point_x1.EndInit();
        this.nu_point_x.EndInit();
        this.un_point_y1.EndInit();
        this.un_point_y.EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}

}
