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
public class 小地图元件 : Form
{
    // Fields
    private Button button1;
    private Button button2;
    private Button button3;
    private IContainer components;
    private Form1 f;
    private ListBox listBox1;
    private Panel panel2;
    private TextBox textBox1;

    // Methods
    public 小地图元件()
    {
        this.InitializeComponent();
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (this.listBox1.SelectedIndex >= 0)
        {
            int selectedIndex = this.listBox1.SelectedIndex;
            int index = int.Parse(this.listBox1.SelectedItem.ToString().Substring(0, this.listBox1.SelectedItem.ToString().IndexOf(':')));
            this.f.clipmapitems.RemoveAt(index);
            this.refresh();
            if (this.listBox1.SelectedIndex >= 0)
            {
                this.listBox1.SelectedIndex = selectedIndex - 1;
            }
        }
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (this.listBox1.SelectedIndex >= 0)
        {
            int selectedIndex = this.listBox1.SelectedIndex;
            int num2 = int.Parse(this.listBox1.SelectedItem.ToString().Substring(0, this.listBox1.SelectedItem.ToString().IndexOf(':')));
            this.f.clipmapitems[num2].name = this.textBox1.Text;
            this.refresh();
            if (this.listBox1.SelectedIndex >= 0)
            {
                this.listBox1.SelectedIndex = selectedIndex - 1;
            }
        }
    }

    private void button3_Click(object sender, EventArgs e)
    {
        this.writetofile();
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
        this.panel2 = new Panel();
        this.listBox1 = new ListBox();
        this.button1 = new Button();
        this.button2 = new Button();
        this.textBox1 = new TextBox();
        this.button3 = new Button();
        base.SuspendLayout();
        this.panel2.AutoSize = true;
        this.panel2.BackColor = Color.Black;
        this.panel2.BackgroundImageLayout = ImageLayout.None;
        this.panel2.Cursor = Cursors.Hand;
        this.panel2.Location = new Point(0x60, 3);
        this.panel2.Name = "panel2";
        this.panel2.Size = new Size(0x11d, 0x12e);
        this.panel2.TabIndex = 5;
        this.panel2.Click += new EventHandler(this.panel2_Click);
        this.listBox1.FormattingEnabled = true;
        this.listBox1.ItemHeight = 12;
        this.listBox1.Location = new Point(3, 15);
        this.listBox1.Name = "listBox1";
        this.listBox1.Size = new Size(0x57, 0x124);
        this.listBox1.TabIndex = 2;
        this.listBox1.SelectedIndexChanged += new EventHandler(this.listBox1_SelectedIndexChanged);
        this.button1.Location = new Point(3, 310);
        this.button1.Name = "button1";
        this.button1.Size = new Size(0x4b, 0x17);
        this.button1.TabIndex = 10;
        this.button1.Text = "删除选择项";
        this.button1.UseVisualStyleBackColor = true;
        this.button1.Click += new EventHandler(this.button1_Click);
        this.button2.Location = new Point(0x54, 310);
        this.button2.Name = "button2";
        this.button2.Size = new Size(0x4b, 0x17);
        this.button2.TabIndex = 11;
        this.button2.Text = "命名选择项";
        this.button2.UseVisualStyleBackColor = true;
        this.button2.Click += new EventHandler(this.button2_Click);
        this.textBox1.Location = new Point(0xa5, 310);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new Size(100, 0x15);
        this.textBox1.TabIndex = 12;
        this.button3.Location = new Point(0x10f, 310);
        this.button3.Name = "button3";
        this.button3.Size = new Size(110, 0x17);
        this.button3.TabIndex = 13;
        this.button3.Text = "记录到文件";
        this.button3.UseVisualStyleBackColor = true;
        this.button3.Click += new EventHandler(this.button3_Click);
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x189, 0x159);
        base.Controls.Add(this.button3);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.button2);
        base.Controls.Add(this.button1);
        base.Controls.Add(this.panel2);
        base.Controls.Add(this.listBox1);
        base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
        base.Name = "小地图元件";
        base.ShowIcon = false;
        base.ShowInTaskbar = false;
        this.Text = "小地图元件";
        base.TopMost = true;
        base.Load += new EventHandler(this.大地图元件_Load);
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.listBox1.SelectedIndex >= 0)
        {
            int num = int.Parse(this.listBox1.SelectedItem.ToString().Substring(0, this.listBox1.SelectedItem.ToString().IndexOf(':')));
            this.panel2.BackgroundImage = this.f.clipmapitems[num].ItemImage;
            this.panel2.Width = this.f.clipmapitems[num].width * 0x10;
            this.panel2.Height = this.f.clipmapitems[num].height * 0x10;
            this.textBox1.Text = this.f.clipmapitems[num].name;
            this.f.currentbigitemid = num;
            this.f.currentitemdata = null;
        }
    }

    private void panel2_Click(object sender, EventArgs e)
    {
        if (this.listBox1.SelectedIndex >= 0)
        {
            int num = int.Parse(this.listBox1.SelectedItem.ToString().Substring(0, this.listBox1.SelectedItem.ToString().IndexOf(':')));
            this.f.currentitemdata = new MapItemData(this.f.clipmapitems[num].width, this.f.clipmapitems[num].height, this.f.clipmapitems[num].mapid);
            this.f.currentitemdata.ItemImage = (Bitmap) this.f.clipmapitems[num].ItemImage.Clone();
            this.f.currentitemdata.name = this.f.clipmapitems[num].name;
            this.f.currentitemdata.subItems = (int[,]) this.f.clipmapitems[num].subItems.Clone();
            this.f.currentitemdata.ItemImage = Form1.VitrificationImage(this.f.currentitemdata.ItemImage, 0.7f);
        }
    }

    private void radioButton1_CheckedChanged(object sender, EventArgs e)
    {
        this.refresh();
    }

    public void refresh()
    {
        this.listBox1.Items.Clear();
        for (int i = 0; i < this.f.clipmapitems.Count; i++)
        {
            if (this.f.clipmapitems[i].mapid == this.f.currentmapid)
            {
                this.listBox1.Items.Add(i.ToString() + ":" + this.f.clipmapitems[i].name);
            }
        }
        this.f.currentitemdata = null;
    }

    private void writetofile()
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < this.f.clipmapitems.Count; i++)
        {
            builder.AppendFormat("{0},{1},{2},{3},", new object[] { this.f.clipmapitems[i].width, this.f.clipmapitems[i].height, this.f.clipmapitems[i].mapid, this.f.clipmapitems[i].name });
            for (int j = 0; j < this.f.clipmapitems[i].height; j++)
            {
                for (int k = 0; k < this.f.clipmapitems[i].width; k++)
                {
                    builder.Append(this.f.clipmapitems[i].subItems[k, j].ToString() + ",");
                }
            }
            builder.Length--;
            builder.AppendLine();
        }
        FileStream stream = new FileStream("item.txt", FileMode.Create, FileAccess.Write);
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(builder.ToString());
        writer.Flush();
        writer.Close();
    }

    private void 大地图元件_Load(object sender, EventArgs e)
    {
        this.f = (Form1) base.Owner;
        this.refresh();
    }
}

 
 

}
