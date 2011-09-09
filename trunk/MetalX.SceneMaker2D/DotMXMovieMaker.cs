using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

using MetalX.Define;

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
                mxmovie.MXT.Size = pictureBox1.Image.Size;
                label1.Text = mxmovie.MXT.Size.ToString();

                textBox1.Text = mxmovie.MXT.Size.Width.ToString();
                textBox2.Text = mxmovie.MXT.Size.Height.ToString();
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
        {
            timer1.Enabled = false;
            mxmovie.FrameCount = int.Parse(textBox3.Text);
            mxmovie.Name = ui_mname.Text;
            if (mxmovie.Vertical)
            {
                int n = mxmovie.MXT.Size.Height;
                n = n / mxmovie.FrameCount;
                textBox2.Text = n.ToString();
            }
            else
            {
                int n = mxmovie.MXT.Size.Width;
                n = n / mxmovie.FrameCount;
                textBox1.Text = n.ToString();
            }
            mxmovie.FrameInterval = int.Parse(textBox4.Text);
            timer1.Interval = (int)mxmovie.FrameInterval;
            mxmovie.TileSize = new Size(int.Parse(textBox1.Text), int.Parse(textBox2.Text));
            bm = new Bitmap(mxmovie.TileSize.Width, mxmovie.TileSize.Height);
            mxmovie.Loop = ui_loop.Checked;
            if (mxmovie.BGSound == null)
            {
                mxmovie.BGSound = new MemoryIndexer(ui_bgsound.Text);
            }
            else
            {
                mxmovie.BGSound.Name = ui_bgsound.Text;
            }
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            if (bm == null)
            {
                return;
            }
            Graphics g = e.Graphics;
            g.DrawImage(bm2, new Point());
            g.DrawString(" " + mxmovie.DrawZone.Location.ToString(), new System.Drawing.Font("微软雅黑", 10), Brushes.Red, new PointF(500, 0));
        }
        Bitmap bm;
        Bitmap bm2;
        private void timer1_Tick(object sender, EventArgs e)
        {
            draw();
        }
        void draw()
        {
            if (bm == null)
            {
                return;
            }
            Graphics g = Graphics.FromImage(bm2);
            g.Clear(Color.Transparent);
            g.DrawImage(pictureBox1.Image, new Rectangle(mxmovie.DrawLocationPoint, mxmovie.TileSize), mxmovie.DrawZone, GraphicsUnit.Pixel);
            mxmovie.NextFrame();
            pictureBox2.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mxmovie.PlayTime = mxmovie.MovieTime;
            mxmovie.Reset();
            draw();
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
                label1.Text = mxmovie.MXT.Size.ToString();

                textBox1.Text = mxmovie.TileSize.Width.ToString();
                textBox2.Text = mxmovie.TileSize.Height.ToString();

                textBox3.Text = mxmovie.FrameCount.ToString();
                textBox4.Text = mxmovie.FrameInterval.ToString();
                ui_mname.Text = mxmovie.Name;


                try
                {

                    ui_bgsound.Text = mxmovie.BGSound.Name;
                }
                catch { }

                ui_loop.Checked = mxmovie.Loop;

                setup();
                update_loc_list();
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

        private void ui_padd_Click(object sender, EventArgs e)
        {
            int x = int.Parse(ui_x.Text);
            int y = int.Parse(ui_y.Text);
            int z = int.Parse(ui_z.Text);
            double tp = double.Parse(ui_tp.Text);

            if (tp > mxmovie.MovieTime)
            {
                tp = mxmovie.MovieTime;
            }

            MetalX.Define.MovieFrameInfo mfi = new MovieFrameInfo();
            mfi.Location = new Microsoft.DirectX.Vector3(x, y, z);
            mfi.TimePoint = tp;
            //mxmovie.MovieFrameInfos.Add(mfi);
            update_loc_list();
        }

        private void ui_pdel_Click(object sender, EventArgs e)
        {
            int i = listBox1.SelectedIndex;
            if (i > -1)
            {
                //mxmovie.MovieFrameInfos.RemoveAt(i);
                update_loc_list();
            }
        }

        void update_loc_list()
        {

            listBox1.Items.Clear();
            //if (mxmovie.MovieFrameInfos == null)
            //{
            //    mxmovie.MovieFrameInfos = new List<MovieFrameInfo>();
            //}
            //foreach (MetalX.Define.MovieFrameInfo mfi in mxmovie.MovieFrameInfos)
            //{
            //    listBox1.Items.Add(mfi.Location + " " + mfi.TimePoint);
            //}
        }

        private void DotMXMovieMaker_Load(object sender, EventArgs e)
        {
            bm2=new Bitmap(640,480);
        }
    }
}
