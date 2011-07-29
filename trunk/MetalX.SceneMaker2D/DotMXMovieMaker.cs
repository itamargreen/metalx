using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace MetalX.SceneMaker2D
{
    public partial class DotMXMovieMaker : Form
    {
        MetalX.File.MetalXMovie mxmovie = new File.MetalXMovie();

        public DotMXMovieMaker()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
        }

        private void ui_load_texture_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.png|*.png";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                mxmovie.MXT = new File.MetalXTexture(ofd.FileName);
                pictureBox1.Image = Image.FromStream(mxmovie.MXT.TextureDataStream);
                mxmovie.MXT.SizePixel = pictureBox1.Image.Size;
                label1.Text = mxmovie.MXT.SizePixel.ToString();

                textBox1.Text = mxmovie.MXT.SizePixel.Width.ToString();
                textBox2.Text = mxmovie.MXT.SizePixel.Height.ToString();
            }            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mxmovie.Vertical = checkBox1.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            setup();
        }

        void setup()
        {             mxmovie.FrameCount = int.Parse(textBox3.Text);
            if (mxmovie.Vertical)
            {
                int n = mxmovie.MXT.SizePixel.Height;
                n = n / mxmovie.FrameCount;
                textBox2.Text = n.ToString();
            }
            else
            {
                int n = mxmovie.MXT.SizePixel.Width;
                n = n / mxmovie.FrameCount;
                textBox2.Text = n.ToString();
            }
            mxmovie.FrameInterval = double.Parse(textBox4.Text);
            timer1.Interval = (int)mxmovie.FrameInterval;
            mxmovie.TileSizePixel = new Size(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            bm = new Bitmap(mxmovie.TileSizePixel.Width, mxmovie.TileSizePixel.Height);

        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (bm == null)
            {
                return;
            }
            Graphics g = e.Graphics;
            //g.Clear(Color.White);
            g.DrawImage(bm, new Point());
            g.DrawString(mxmovie.DrawZone.Location.ToString(), new System.Drawing.Font("微软雅黑", 12), Brushes.Red, new PointF(10, 100));
            //pictureBox2.Refresh();
            
        }
        Bitmap bm;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bm == null)
            {
                return;
            }
            mxmovie.NextFrame();
            Graphics g = Graphics.FromImage(bm);
            g.Clear(Color.Transparent);
            g.DrawImage(pictureBox1.Image, new Rectangle(new Point(), mxmovie.TileSizePixel), mxmovie.DrawZone, GraphicsUnit.Pixel);
            pictureBox2.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MetalX Movie File|*.mxmovie";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                mxmovie = (MetalX.File.MetalXMovie)Util.LoadObject(ofd.FileName);
                pictureBox1.Image = Image.FromStream(mxmovie.MXT.TextureDataStream);
                //mxmovie.MXT.SizePixel = pictureBox1.Image.Size;
                label1.Text = mxmovie.MXT.SizePixel.ToString();

                textBox1.Text = mxmovie.TileSizePixel.Width.ToString();
                textBox2.Text = mxmovie.TileSizePixel.Height.ToString();

                textBox3.Text = mxmovie.FrameCount.ToString();
                textBox4.Text = mxmovie.FrameInterval.ToString();

                setup();
            } 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "MetalX Movie File|*.MXMovie";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Util.SaveObject(sfd.FileName, mxmovie);
            }
        }
    }
}
