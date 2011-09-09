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
public class newmap : Form
{
    // Fields
    private Button button1;
    private Button button2;
    private ComboBox comboBox1;
    private ComboBox comboBox2;
    private IContainer components;
    private GroupBox groupBox1;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    public int mapid;
    public int return_init_item;
    public int return_mapid;
    public int return_maplenth;
    public int return_mapwidth;
    private TextBox textBox1;
    private TextBox textBox2;

    // Methods
    public newmap()
    {
        this.InitializeComponent();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        try
        {
            int num = int.Parse(this.textBox1.Text.Trim());
            int num2 = int.Parse(this.textBox2.Text.Trim());
            if ((num == 0) || (num2 == 0))
            {
                MessageBox.Show("请检查长宽");
            }
            else
            {
                this.return_maplenth = num;
                this.return_mapwidth = num2;
                this.return_mapid = this.comboBox1.SelectedIndex;
                this.return_init_item = this.comboBox2.SelectedIndex + 1;
                base.DialogResult = DialogResult.OK;
            }
        }
        catch (Exception)
        {
            MessageBox.Show("请检查长宽");
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

    private void InitializeComponent()
    {
        this.label1 = new Label();
        this.groupBox1 = new GroupBox();
        this.label2 = new Label();
        this.comboBox1 = new ComboBox();
        this.label3 = new Label();
        this.label4 = new Label();
        this.label5 = new Label();
        this.textBox1 = new TextBox();
        this.textBox2 = new TextBox();
        this.label6 = new Label();
        this.comboBox2 = new ComboBox();
        this.button1 = new Button();
        this.button2 = new Button();
        this.groupBox1.SuspendLayout();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new Point(0x15, 9);
        this.label1.Name = "label1";
        this.label1.Size = new Size(0x2f, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "请选择:";
        this.groupBox1.Controls.Add(this.textBox2);
        this.groupBox1.Controls.Add(this.textBox1);
        this.groupBox1.Controls.Add(this.comboBox2);
        this.groupBox1.Controls.Add(this.comboBox1);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.label5);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.label6);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Location = new Point(12, 0x18);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new Size(0x233, 100);
        this.groupBox1.TabIndex = 1;
        this.groupBox1.TabStop = false;
        this.label2.AutoSize = true;
        this.label2.Location = new Point(9, 0x11);
        this.label2.Name = "label2";
        this.label2.Size = new Size(0x4d, 12);
        this.label2.TabIndex = 0;
        this.label2.Text = "使用与编号为";
        this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
        this.comboBox1.FormattingEnabled = true;
        this.comboBox1.Location = new Point(0x5c, 14);
        this.comboBox1.Name = "comboBox1";
        this.comboBox1.Size = new Size(60, 20);
        this.comboBox1.TabIndex = 1;
        this.label3.AutoSize = true;
        this.label3.Location = new Point(0x9e, 0x11);
        this.label3.Name = "label3";
        this.label3.Size = new Size(0x71, 12);
        this.label3.TabIndex = 0;
        this.label3.Text = "的地图相同的小图块";
        this.label4.AutoSize = true;
        this.label4.Location = new Point(0x1b3, 0x11);
        this.label4.Name = "label4";
        this.label4.Size = new Size(0x2f, 12);
        this.label4.TabIndex = 0;
        this.label4.Text = "地图长:";
        this.label5.AutoSize = true;
        this.label5.Location = new Point(0x1b3, 0x30);
        this.label5.Name = "label5";
        this.label5.Size = new Size(0x2f, 12);
        this.label5.TabIndex = 0;
        this.label5.Text = "地图宽:";
        this.textBox1.Location = new Point(0x1e8, 13);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new Size(0x42, 0x15);
        this.textBox1.TabIndex = 2;
        this.textBox1.Text = "2";
        this.textBox2.Location = new Point(0x1e8, 0x2d);
        this.textBox2.Name = "textBox2";
        this.textBox2.Size = new Size(0x42, 0x15);
        this.textBox2.TabIndex = 2;
        this.textBox2.Text = "2";
        this.label6.AutoSize = true;
        this.label6.Location = new Point(9, 0x36);
        this.label6.Name = "label6";
        this.label6.Size = new Size(0x53, 12);
        this.label6.TabIndex = 0;
        this.label6.Text = "初始地形编号:";
        this.comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
        this.comboBox2.FormattingEnabled = true;
        this.comboBox2.Location = new Point(0x5c, 0x33);
        this.comboBox2.Name = "comboBox2";
        this.comboBox2.Size = new Size(60, 20);
        this.comboBox2.TabIndex = 1;
        this.button1.Location = new Point(500, 0x98);
        this.button1.Name = "button1";
        this.button1.Size = new Size(0x4b, 0x17);
        this.button1.TabIndex = 2;
        this.button1.Text = "取消";
        this.button1.UseVisualStyleBackColor = true;
        this.button2.Location = new Point(0x1a3, 0x98);
        this.button2.Name = "button2";
        this.button2.Size = new Size(0x4b, 0x17);
        this.button2.TabIndex = 2;
        this.button2.Text = "确定";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new EventHandler(this.button2_Click);
        base.AcceptButton = this.button2;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton = this.button1;
        base.ClientSize = new Size(0x246, 0xbb);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.groupBox1);
        base.Controls.Add(this.label1);
        base.Name = "newmap";
        this.Text = "建地图";
        base.Load += new EventHandler(this.newmap_Load);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    private void newmap_Load(object sender, EventArgs e)
    {
        for (int i = 1; i < 240; i++)
        {
            this.comboBox1.Items.Add(i.ToString("X2"));
        }
        this.comboBox1.SelectedIndex = this.mapid;
        for (int j = 1; j < 0x80; j++)
        {
            this.comboBox2.Items.Add(j.ToString("X2"));
        }
        this.comboBox2.SelectedIndex = 0;
    }
}

 
 

}
