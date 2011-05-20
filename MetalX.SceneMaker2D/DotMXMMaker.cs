using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;
using Microsoft.DirectX.Direct3D;


namespace MetalX.SceneMaker2D
{
    public partial class DotMXMMaker : Form
    {
        MetalXGame game;
        ModelMaker modelViewer;

        public DotMXMMaker()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new MetalXGame(panel1);
            modelViewer = new ModelMaker(game);
            game.MountGameCom(modelViewer);
            label1.Text = depth.ToString();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            game.Exit();
        }

        private float depth = -192f, angleY = 0f, angleX = 0f;
        private float downAngleY = 0f, downAngleX = 0f;
        private Point downPoint;

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!downPoint.Equals(Point.Empty))
            {
                float x = (float)Math.PI * 2f * (float)(downPoint.X - e.X) / (float)Width;
                angleY = downAngleY + x;

                float y = (float)Math.PI * 2f * (float)(downPoint.Y - e.Y) / (float)Height;
                angleX = downAngleX + y;
            }
            modelViewer.xyz = new Vector3(angleX, angleY, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            downPoint = new Point(e.X, e.Y);
            downAngleY = angleY;
            downAngleX = angleX;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            downPoint = Point.Empty;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta > 0)
            {
                depth += 10f;
            }
            else if (e.Delta < 0)
            {
                depth -= 10f;
            }
            label1.Text = depth.ToString();
            game.SetCamera(new Vector3(0, 0, depth), new Vector3());
        }

        private void ui_pack_Click(object sender, EventArgs e)
        {
            //UtilityLibrary.SaveObject((Math.Abs(DateTime.Now.GetHashCode())).ToString() + ".Model", mg.model);            
            UtilLib.SaveObject(filename + ".MXM", modelViewer.model);
        }
        string filename;
        private void ui_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.X|*.X";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName != string.Empty)
                {
                    modelViewer.model = game.LoadDotX(ofd.FileName);
                    filename = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                    game.Start();
                }
            }
        }

        private void ui_loadmodel_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MetalX Model File|*.MXM";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName != string.Empty)
                {
                    modelViewer.model = game.LoadDotMXM(ofd.FileName);
                    filename = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                    game.Start();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MetalX Model File|*.MXM";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (ofd.FileName != string.Empty)
                {
                    modelViewer.t = game.LoadDotMXT(ofd.FileName);
                    game.Start();
                }
            }
        }
    }
}
