using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using MetalX;

namespace MetalX.SceneMaker2D
{
    public partial class DotMXTMaker : Form
    {
        public DotMXTMaker()
        {
            InitializeComponent();
        }

        private void ui_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.png|*.png";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName != string.Empty)
                {
                    Image img = Image.FromFile(ofd.FileName);

                    pictureBox1.Size = img.Size;
                    label1.Text = img.Size.ToString();
                    pictureBox1.Image = img;
                    textBox1.Text = ofd.FileName;

                    textBox2.Text = System.IO.Path.GetDirectoryName(ofd.FileName) + @"\" + System.IO.Path.GetFileNameWithoutExtension(ofd.FileName) + ".MXT";

                    textBox5.Text = System.IO.Path.GetFileNameWithoutExtension(textBox1.Text);
                }
            }
        }

        private void DotMXTMaker_Load(object sender, EventArgs e)
        {
            label1.Text = "";
        }

        private void ui_save_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == string.Empty)
            {
                return;
            }
            MetalXTexture mxt = new MetalXTexture();
            mxt.Name = textBox5.Text;
            mxt.SizePixel = pictureBox1.Size;
            mxt.TextureData = System.IO.File.ReadAllBytes(textBox1.Text);
            mxt.TileSizePixel = new System.Drawing.Size(w, h);
            UtilLib.SaveObject(textBox2.Text, mxt);
            MessageBox.Show("输出成功");
        }
        int w = 16, h = 16;
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (checkBox1.Checked)
            {
                for (int x = 0; x < pictureBox1.Width; x += w)
                {
                    g.DrawLine(new Pen(Color.Blue), new Point(x, 0), new Point(x, pictureBox1.Height));
                }
                for (int y = 0; y < pictureBox1.Height; y += h)
                {
                    g.DrawLine(new Pen(Color.Blue), new Point(0, y), new Point(pictureBox1.Width, y));
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            w = int.Parse(textBox3.Text);
            pictureBox1.Refresh();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            h = int.Parse(textBox4.Text);
            pictureBox1.Refresh();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
        }
    }
}
