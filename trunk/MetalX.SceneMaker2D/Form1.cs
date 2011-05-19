﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;

namespace MetalX.SceneMaker2D
{
    public partial class Form1 : Form
    {
        MetalXGame metalXGame;
        SceneMaker2D sceneMaker2D;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 320;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (metalXGame != null)
            {
                metalXGame.Exit();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 折叠打开工具箱ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (splitContainer1.Panel1Collapsed)
            {
                splitContainer1.Panel1Collapsed = false;
            }
            else
            {
                splitContainer1.Panel1Collapsed = true;
            }
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainer1.SplitterDistance = 320;
        }

        private void splitContainer1_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            splitContainer1.SplitterDistance = 320;
        }

        private void 纹理工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DotMXTMaker f = new DotMXTMaker();
            f.ShowDialog();
        }

        private void 模型工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DotMXMMaker f = new DotMXMMaker();
            f.ShowDialog();
        }

        private void 音频工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 ab = new AboutBox1();
            ab.ShowDialog();
        }

        private void ui_createscene_Click(object sender, EventArgs e)
        {
            metalXGame = new MetalXGame(pictureBox1);
            metalXGame.LoadAllMXT(@".\");

            update_pic_list();

            int sw = int.Parse(ui_scenew.Text);
            int sh = int.Parse(ui_sceneh.Text);

            int tw = int.Parse(ui_sinw.Text);
            int th = int.Parse(ui_sinh.Text);

            sceneMaker2D = new SceneMaker2D(metalXGame);

            sceneMaker2D.scene = new Scene();

            sceneMaker2D.scene.Name = ui_scenename.Text;
            sceneMaker2D.scene.Size = new Size(sw, sh);
            sceneMaker2D.scene.TileSizePixel = new Size(tw, th);

            sceneMaker2D.scene.TileLayers.Add(new TileLayer());
            sceneMaker2D.scene.TileLayers.Add(new TileLayer());
            sceneMaker2D.scene.TileLayers.Add(new TileLayer());
            sceneMaker2D.scene.TileLayers.Add(new TileLayer());
            sceneMaker2D.scene.TileLayers.Add(new TileLayer());
            sceneMaker2D.scene.TileLayers.Add(new TileLayer());

            ui_ly_slt.Items.Add("6");
            ui_ly_slt.Items.Add("5");
            ui_ly_slt.Items.Add("4");
            ui_ly_slt.Items.Add("3");
            ui_ly_slt.Items.Add("2");
            ui_ly_slt.Items.Add("1");

            pictureBox1.Size = sceneMaker2D.scene.SizePixel;

            metalXGame.MountGameCom(sceneMaker2D);
            metalXGame.Start();
        }

        void update_pic_list()
        {
            for (int i = 0; i < metalXGame.Textures.Count; i++)
            {
                ui_pic_slt.Items.Add(metalXGame.Textures[i].Name);
            }
        }

        private void ui_pic_slt_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_pic.Image = Image.FromStream(new System.IO.MemoryStream(metalXGame.Textures[ui_pic_slt.Text].TextureData));
            ui_pic.Size = new Size(metalXGame.Textures[ui_pic_slt.Text].SizePixel.Width + 2, metalXGame.Textures[ui_pic_slt.Text].SizePixel.Height + 2);
            ui_gridw.Text = metalXGame.Textures[ui_pic_slt.Text].TileSizePixel.Width.ToString();
            ui_gridh.Text = metalXGame.Textures[ui_pic_slt.Text].TileSizePixel.Height.ToString();
            sceneMaker2D.mxtName = ui_pic_slt.Text;
        }

        private void ui_pic_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                if (ui_showgrid.Checked)
                {
                    for (int i = 0; i <= metalXGame.Textures[ui_pic_slt.Text].SizePixel.Height; i += metalXGame.Textures[ui_pic_slt.Text].TileSizePixel.Height)
                    {
                        g.DrawLine(new Pen(Color.Blue), 0, i, ui_pic.Size.Width, i);
                    }
                    for (int i = 0; i <= metalXGame.Textures[ui_pic_slt.Text].SizePixel.Width; i += metalXGame.Textures[ui_pic_slt.Text].TileSizePixel.Width)
                    {
                        g.DrawLine(new Pen(Color.Blue), i, 0, i, ui_pic.Size.Height);
                    }
                }
                g.DrawRectangle(new Pen(Color.Red, 2), left_rect);
            }
            catch { }
        }

        private void ui_showgrid_CheckedChanged(object sender, EventArgs e)
        {
            sceneMaker2D.drawGrid = ui_showgrid.Checked;
            ui_pic.Refresh();
        }

        Rectangle left_rect;
        private void ui_pic_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = pointround(e.Location, metalXGame.Textures[sceneMaker2D.mxtName].TileSizePixel);
                left_rect.Location = p;
                left_rect.Size = metalXGame.Textures[sceneMaker2D.mxtName].TileSizePixel;
                ui_pic.Refresh();
            }
        }

        private void ui_pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = pointround(e.Location, metalXGame.Textures[ui_pic_slt.Text].TileSizePixel);
                p = pointaddpoint(p, new Point(metalXGame.Textures[ui_pic_slt.Text].TileSizePixel));
                left_rect.Size = new Size(pointdelpoint(p, left_rect.Location));
                ui_pic.Refresh();
            }
        }
        private void ui_pic_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = pointround(e.Location, metalXGame.Textures[ui_pic_slt.Text].TileSizePixel);
                p = pointaddpoint(p, new Point(metalXGame.Textures[ui_pic_slt.Text].TileSizePixel));
                left_rect.Size = new Size(pointdelpoint(p, left_rect.Location));
                sceneMaker2D.penRect = left_rect;
                ui_pic.Refresh();
            }
        }

        Point pointround(Point p, Size s)
        {
            int x, y;
            x = p.X / s.Width;
            x *= s.Width;
            y = p.Y / s.Height;
            y *= s.Height;
            return new Point(x, y);
        }
        Point pointround2(Point p, Size s)
        {
            int x, y;
            x = p.X / s.Width;
            y = p.Y / s.Height;
            return new Point(x, y);
        }
        Point pointdelpoint(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }
        Point pointaddpoint(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        bool erasing = false;
        Rectangle right_rect;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (erasing)
                {
                    del_tile(sceneMaker2D.penLoc, sceneMaker2D.penRect);
                }
                else
                {
                    if (sceneMaker2D.drawRect.Width > sceneMaker2D.scene.TileSizePixel.Width ||
                        sceneMaker2D.drawRect.Height > sceneMaker2D.scene.TileSizePixel.Height)
                    {
                        //paint_tile(sceneMaker2D.penLoc, sceneMaker2D.drawRect);
                    }
                    else
                    {
                        paint_tile(sceneMaker2D.penLoc, sceneMaker2D.penRect);
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                Point p = pointround(e.Location, sceneMaker2D.scene.TileSizePixel);
                right_rect.Location = p;
                right_rect.Size = sceneMaker2D.scene.TileSizePixel;
                sceneMaker2D.drawRect = right_rect;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                sceneMaker2D.penLoc = pointround(e.Location, metalXGame.Textures[sceneMaker2D.mxtName].TileSizePixel);
                ui_mouse_pos.Text = pointround2(e.Location, metalXGame.Textures[sceneMaker2D.mxtName].TileSizePixel).ToString();
            }
            catch
            { } 
            if (e.Button == MouseButtons.Left)
            {
                if (erasing)
                {
                    del_tile(sceneMaker2D.penLoc, sceneMaker2D.penRect);
                }
                else
                {
                    paint_tile(sceneMaker2D.penLoc, sceneMaker2D.penRect);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                try
                {
                    Point p = pointround(e.Location, metalXGame.Textures[ui_pic_slt.Text].TileSizePixel);
                    p = pointaddpoint(p, new Point(metalXGame.Textures[ui_pic_slt.Text].TileSizePixel));
                    right_rect.Size = new Size(pointdelpoint(p, right_rect.Location));
                    sceneMaker2D.drawRect = right_rect;
                }
                catch { }
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                try
                {
                    Point p = pointround(e.Location, metalXGame.Textures[ui_pic_slt.Text].TileSizePixel);
                    p = pointaddpoint(p, new Point(metalXGame.Textures[ui_pic_slt.Text].TileSizePixel));
                    right_rect.Size = new Size(pointdelpoint(p, right_rect.Location));
                    sceneMaker2D.drawRect = right_rect;
                }
                catch { }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (erasing)
                {
                    pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
                    ui_cursorsat.Text = "铅笔";
                    erasing = false;
                }
                else
                {
                    pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
                    ui_cursorsat.Text = "橡皮";
                    erasing = true;
                }
            }
        }
        void del_tile(Point p, Rectangle rect)
        {
            int xo, yo;
            for (yo = 0; yo < rect.Height; yo += sceneMaker2D.scene.TileSizePixel.Height)
            {
                for (xo = 0; xo < rect.Width; xo += sceneMaker2D.scene.TileSizePixel.Width)
                {
                    Point pp = new Point();
                    pp.X = p.X + xo;
                    pp.Y = p.Y + yo;

                    if (pp.X < 0 || pp.Y < 0 || pp.X >= sceneMaker2D.scene.SizePixel.Width || pp.Y >= sceneMaker2D.scene.SizePixel.Height)
                    {
                        continue;
                    }

                    int i = containsTile(pp);
                    if (i > -1)
                    {
                        sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles.RemoveAt(i);
                    }
                }
            }
            try
            {
                ui_tilecount.Text = sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles.Count.ToString();
            }
            catch
            { }
        }
        void paint_tile(Point p, Rectangle rect)
        {
            int xo, yo;
            for (yo = 0; yo < rect.Height; yo += sceneMaker2D.scene.TileSizePixel.Height)
            {
                for (xo = 0; xo < rect.Width; xo += sceneMaker2D.scene.TileSizePixel.Width)
                {
                    Point pp = new Point();
                    pp.X = p.X + xo;
                    pp.Y = p.Y + yo;

                    if (pp.X < 0 || pp.Y < 0 || pp.X >= sceneMaker2D.scene.SizePixel.Width || pp.Y >= sceneMaker2D.scene.SizePixel.Height)
                    {
                        continue;
                    }

                    TileFrame tf = new TileFrame();
                    tf.TextureFileName = sceneMaker2D.mxtName;
                    tf.DrawZone.Location = new Point(rect.X + xo, rect.Y + yo);
                    tf.DrawZone.Size = sceneMaker2D.scene.TileSizePixel;

                    Tile tile = new Tile();
                    tile.Location = pp;
                    tile.Frames.Add(tf);

                    int i = containsTile(tile.Location);
                    if (i > -1)
                    {
                        sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles[i] = tile;
                    }
                    else
                    {
                        try
                        {
                            sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles.Add(tile);
                        }
                        catch { }
                    }
                }
            }
            try
            {
                ui_tilecount.Text = sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles.Count.ToString();
            }
            catch
            { }
        }
        void paint_zone(Rectangle rr)
        {
            for (int y = rr.Y; y < rr.Height; y += sceneMaker2D.scene.TileSizePixel.Height)
            {
                for (int x = rr.X; x < rr.Width; x += sceneMaker2D.scene.TileSizePixel.Width)
                {
                    //paint_tile(new Point(x, y));
                }
            }
        }
        int containsTile(Point loc)
        {
            try
            {
                int i = 0;
                foreach (Tile tile in sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles)
                {
                    if (tile.Location == loc)
                    {
                        return i;
                    }
                    i++;
                }
                return -1;
            }
            catch
            { return -1; }
        }

        private void ui_ly_slt_SelectedIndexChanged(object sender, EventArgs e)
        {
            sceneMaker2D.drawingLayer = ui_ly_slt.SelectedIndex;
            ui_mouse_pos.Text = "选中 " + sceneMaker2D.drawingLayer.ToString() + " 层";
        }
        
        private void ui_ly_slt_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue.ToString() == "Unchecked")
            {
                sceneMaker2D.scene.TileLayers[ui_ly_slt.SelectedIndex].Visible = false;
            }
            else
            {
                sceneMaker2D.scene.TileLayers[ui_ly_slt.SelectedIndex].Visible = true;
            }
        }
    }
}