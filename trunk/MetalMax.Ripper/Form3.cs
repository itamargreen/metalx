using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MetalMax.Ripper
{
public class Form3 : Form
{
    // Fields
    private Button button1;
    private Button button2;
    private CheckBox checkBox1;
    private IContainer components;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    public int mapid;
    private NumericUpDown numericUpDown1;

    // Methods
    public Form3()
    {
        this.InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        base.DialogResult = DialogResult.Cancel;
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (this.checkBox1.Checked)
        {
            this.mapid = 0;
            base.DialogResult = DialogResult.OK;
        }
        else
        {
            this.mapid = ((int) this.numericUpDown1.Value) - 1;
            base.DialogResult = DialogResult.OK;
        }
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (this.checkBox1.Checked)
        {
            this.numericUpDown1.Enabled = false;
        }
        else
        {
            this.numericUpDown1.Enabled = true;
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.components != null))
        {
            this.components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void Form3_Load(object sender, EventArgs e)
    {
        this.label4.Text = (this.mapid + 1).ToString("X2");
        this.numericUpDown1.Maximum = this.mapid;
        Form1 owner = (Form1) base.Owner;
        if (owner.allmapdata[this.mapid].mapstoreshare == 0)
        {
            this.numericUpDown1.Enabled = false;
            this.checkBox1.Checked = true;
        }
        else
        {
            this.checkBox1.Checked = false;
            this.numericUpDown1.Enabled = true;
            this.numericUpDown1.Value = owner.allmapdata[this.mapid].mapstoreshare + 1;
        }
    }

    private void InitializeComponent()
    {
        this.label1 = new Label();
        this.numericUpDown1 = new NumericUpDown();
        this.label2 = new Label();
        this.checkBox1 = new CheckBox();
        this.button2 = new Button();
        this.button1 = new Button();
        this.label3 = new Label();
        this.label4 = new Label();
        this.numericUpDown1.BeginInit();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new Point(30, 0x3a);
        this.label1.Name = "label1";
        this.label1.Size = new Size(0x83, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "共享此地图的图形数据:";
        this.numericUpDown1.Hexadecimal = true;
        this.numericUpDown1.Location = new Point(0xa7, 0x38);
        int[] bits = new int[4];
        bits[0] = 0xef;
        this.numericUpDown1.Maximum = new decimal(bits);
        int[] numArray2 = new int[4];
        numArray2[0] = 1;
        this.numericUpDown1.Minimum = new decimal(numArray2);
        this.numericUpDown1.Name = "numericUpDown1";
        this.numericUpDown1.Size = new Size(0x53, 0x15);
        this.numericUpDown1.TabIndex = 1;
        int[] numArray3 = new int[4];
        numArray3[0] = 1;
        this.numericUpDown1.Value = new decimal(numArray3);
        this.label2.AutoSize = true;
        this.label2.ForeColor = Color.Red;
        this.label2.Location = new Point(0x37, 0x7b);
        this.label2.Name = "label2";
        this.label2.Size = new Size(0xb9, 12);
        this.label2.TabIndex = 2;
        this.label2.Text = "说明：共享的地图不占用图形空间";
        this.checkBox1.AutoSize = true;
        this.checkBox1.Location = new Point(0x20, 0x53);
        this.checkBox1.Name = "checkBox1";
        this.checkBox1.Size = new Size(0xa8, 0x10);
        this.checkBox1.TabIndex = 3;
        this.checkBox1.Text = "不共享，即使用单独的空间";
        this.checkBox1.UseVisualStyleBackColor = true;
        this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
        this.button2.Location = new Point(260, 0xcc);
        this.button2.Name = "button2";
        this.button2.Size = new Size(0x4b, 0x17);
        this.button2.TabIndex = 5;
        this.button2.Text = "确定";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new EventHandler(this.button2_Click);
        this.button1.DialogResult = DialogResult.Cancel;
        this.button1.Location = new Point(0x155, 0xcc);
        this.button1.Name = "button1";
        this.button1.Size = new Size(0x4b, 0x17);
        this.button1.TabIndex = 4;
        this.button1.Text = "取消";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new EventHandler(this.button1_Click);
        this.label3.AutoSize = true;
        this.label3.Location = new Point(30, 20);
        this.label3.Name = "label3";
        this.label3.Size = new Size(0x59, 12);
        this.label3.TabIndex = 0;
        this.label3.Text = "当前地图编号：";
        this.label4.AutoSize = true;
        this.label4.ForeColor = Color.FromArgb(0, 0, 0xc0);
        this.label4.Location = new Point(0x7d, 20);
        this.label4.Name = "label4";
        this.label4.Size = new Size(0x11, 12);
        this.label4.TabIndex = 0;
        this.label4.Text = "id";
        base.AcceptButton = this.button2;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton = this.button1;
        base.ClientSize = new Size(0x1a6, 0xef);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.checkBox1);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.numericUpDown1);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label1);
        base.Name = "Form3";
        base.StartPosition = FormStartPosition.CenterParent;
        this.Text = "共享图形";
        base.Load += new EventHandler(this.Form3_Load);
        this.numericUpDown1.EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
}

 
 

}
