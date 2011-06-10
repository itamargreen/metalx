using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;

using MetalX.Component;
using MetalX.Data;
using MetalX.Resource;

namespace MetalX.SceneMaker2D
{
    public partial class Form1 : Form
    {
        Stack<object> stack = new Stack<object>(10);

        Cursor bc = Cursors.Cross;
        bool insFrame = false;
        Game game;
        SceneMaker2D sceneMaker2D;
        bool erasing = false;
        Rectangle right_rect;
        Rectangle left_rect;
        string openFileName;
        int scene_code_layer;
        bool drawing_code = false;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            splitContainer1.SplitterDistance = 320;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (game != null)
            {
                game.Dispose();
            }
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void splitContainer1_SplitterMoved(object sender, SplitterEventArgs e)
        {
            splitContainer1.SplitterDistance = 320;
        }

        private void 纹理工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DotMXTMaker().ShowDialog();
        }

        private void 模型工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DotMXMMaker().ShowDialog();
        }

        private void 音频工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DotMXAMaker().ShowDialog();
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog();
        }
        void backup(object obj)
        {
            if (stack.Count < 10)
            {
                stack.Push(obj);
            }
            else
            {
                Stack<object> bs = new Stack<object>();
                for (int i = 0; i < 9; i++)
                {
                    bs.Push(stack.Pop());
                }
                stack.Clear();
                for (int i = 0; i < 9; i++)
                {
                    stack.Push(bs.Pop());
                }
            }
        }
        private void ui_createscene_Click(object sender, EventArgs e)
        {
            new_scene(
                new Size(int.Parse(ui_scenew.Text), int.Parse(ui_sceneh.Text)),
                new Size(int.Parse(ui_sinw.Text), int.Parse(ui_sinh.Text)));
        }

        void new_scene(Size sizepixel, Size tilesizepixel)
        {
            left_rect = new Rectangle();
            right_rect = new Rectangle();
            if (game != null)
            {
                game.Exit();
            }
            game = new Game(pictureBox1);
            game.LoadAllDotMXT(@".\");
            game.LoadAllDotMXA(@".\");
            game.LoadAllDotPNG(@".\");
            game.LoadAllDotMP3(@".\");

            update_pic_list();
            update_mus_list();

            sceneMaker2D = new SceneMaker2D(game);

            int sw = sizepixel.Width;
            int sh = sizepixel.Height;
            int tw = tilesizepixel.Width;
            int th = tilesizepixel.Height;

            sceneMaker2D.scene = new Scene(new Size(sw, sh), new Size(tw, th));
            sceneMaker2D.scene.Name = ui_scenename.Text;
            sceneMaker2D.scene.FrameInterval = int.Parse(ui_framedelay.Text);

            ui_ly_slt.Items.Clear();
            int lc = int.Parse(ui_ly_count.Text);
            for (int i = 0; i < lc; i++)
            {
                sceneMaker2D.scene.TileLayers.Add(new TileLayer());
                ui_ly_slt.Items.Add(lc - 1 - i);
                ui_ly_slt.SetItemChecked(i, true);
            }
            ui_ly_slt.SetSelected(lc - 1, true);
            
            sceneMaker2D.scene.CodeLayers.Add(new CodeLayer());

            pictureBox1.Size = sceneMaker2D.scene.SizePixel;

            tabControl1.SelectedIndex = 1;

            game.MountGameCom(sceneMaker2D);
            game.GO();
        }
        void new_scene(string fileName)
        {
            left_rect = new Rectangle();
            right_rect = new Rectangle();
            if (game != null)
            {
                game.Exit();
            }
            game = new Game(pictureBox1);
            game.LoadAllDotMXT(@".\");
            game.LoadAllDotMXA(@".\");
            game.LoadAllDotPNG(@".\");
            game.LoadAllDotMP3(@".\");

            update_pic_list();
            update_mus_list();

            sceneMaker2D = new SceneMaker2D(game);

            sceneMaker2D.scene = game.LoadDotMXScene(fileName);

            ui_ly_slt.Items.Clear();
            for (int i = 0; i < sceneMaker2D.scene.TileLayers.Count; i++)
            {
                ui_ly_slt.Items.Add(sceneMaker2D.scene.TileLayers.Count - 1 - i);
                ui_ly_slt.SetItemChecked(i, true);
            }
            ui_ly_slt.SetSelected(sceneMaker2D.scene.TileLayers.Count - 1, true);

            pictureBox1.Size = sceneMaker2D.scene.SizePixel;

            tabControl1.SelectedIndex = 1;

            game.MountGameCom(sceneMaker2D);
            game.GO();
        }

        void update_pic_list()
        {
            ui_pic.Image = null;
            ui_pic_slt.Text = "";
            ui_pic_slt.Items.Clear();
            for (int i = 0; i < game.Textures.Count; i++)
            {
                ui_pic_slt.Items.Add(game.Textures[i].Name);
            }
        }
        void update_mus_list()
        {
            ui_mus_slt.Text = "";
            ui_mus_slt.Items.Clear();
            for (int i = 0; i < game.Audios.Count; i++)
            {
                ui_mus_slt.Items.Add(game.Audios[i].Name);
            }
        }

        private void ui_pic_slt_SelectedIndexChanged(object sender, EventArgs e)
        {
            ui_pic.Image = Image.FromStream(new System.IO.MemoryStream(game.Textures[ui_pic_slt.Text].TextureData));
            ui_pic.Size = new Size(game.Textures[ui_pic_slt.Text].SizePixel.Width + 2, game.Textures[ui_pic_slt.Text].SizePixel.Height + 2);
            ui_gridw.Text = game.Textures[ui_pic_slt.Text].TileSizePixel.Width.ToString();
            ui_gridh.Text = game.Textures[ui_pic_slt.Text].TileSizePixel.Height.ToString();
            ui_pic.Focus();
            sceneMaker2D.mxtName = ui_pic_slt.Text;
        }

        private void ui_pic_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                if (ui_showgrid.Checked)
                {
                    for (int i = 0; i <= game.Textures[ui_pic_slt.Text].SizePixel.Height; i += game.Textures[ui_pic_slt.Text].TileSizePixel.Height)
                    {
                        g.DrawLine(new Pen(Color.Blue), 0, i, ui_pic.Size.Width, i);
                    }
                    for (int i = 0; i <= game.Textures[ui_pic_slt.Text].SizePixel.Width; i += game.Textures[ui_pic_slt.Text].TileSizePixel.Width)
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

        private void ui_pic_MouseDown(object sender, MouseEventArgs e)
        {
            if (sceneMaker2D == null)
            {
                return;
            }
            if (sceneMaker2D.mxtName == null)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                Point p = pointround(e.Location, game.Textures[sceneMaker2D.mxtName].TileSizePixel);
                left_rect.Location = p;
                left_rect.Size = game.Textures[sceneMaker2D.mxtName].TileSizePixel;
                ui_pic.Refresh();
            }
        }

        private void ui_pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = pointround(e.Location, game.Textures[ui_pic_slt.Text].TileSizePixel);
                p = pointaddpoint(p, new Point(game.Textures[ui_pic_slt.Text].TileSizePixel));
                left_rect.Size = new Size(pointdelpoint(p, left_rect.Location));
                ui_pic.Refresh();
            }
        }

        private void ui_pic_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point p = pointround(e.Location, game.Textures[ui_pic_slt.Text].TileSizePixel);
                p = pointaddpoint(p, new Point(game.Textures[ui_pic_slt.Text].TileSizePixel));
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
        void paint_code(Point p, MouseEventArgs e, int l)
        {
            if (!(e.Button == MouseButtons.Left || e.Button == MouseButtons.Right))
            {
                return;
            }

            bool val = true;

            if (e.Button == MouseButtons.Left)
            {
                val = true;
            }
            else if (e.Button == MouseButtons.Right)
            {
                val = false;
            }

            try
            {
                if (sceneMaker2D.drawCodeLayer == 0)
                {
                    sceneMaker2D.scene.CodeLayers[0][p].CHRCanRch = val;
                }
                else if (sceneMaker2D.drawCodeLayer == 1)
                {
                    sceneMaker2D.scene.CodeLayers[0][p].MTLCanRch = val;
                }
                else if (sceneMaker2D.drawCodeLayer == 2)
                {
                    sceneMaker2D.scene.CodeLayers[0][p].SHPCanRch = val;
                }
                else if (sceneMaker2D.drawCodeLayer == 3)
                {
                    sceneMaker2D.scene.CodeLayers[0][p].FLTCanRch = val;
                }
                else if (sceneMaker2D.drawCodeLayer == 4)
                {
                    sceneMaker2D.scene.CodeLayers[0][p].DrawLayer = l;
                }
            }
            catch { }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tabControl2.SelectedIndex == 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (drawing_code)
                    { }
                    else
                    {
                        if (erasing)
                        {
                            if (sceneMaker2D.dragRect.Width > sceneMaker2D.scene.TileSizePixel.Width ||
                                sceneMaker2D.dragRect.Height > sceneMaker2D.scene.TileSizePixel.Height)
                            {
                                backup(Util.Serialize(sceneMaker2D.scene));
                                del_zone(sceneMaker2D.dragRect, sceneMaker2D.penRect);
                            }
                            else
                            {
                                backup(Util.Serialize(sceneMaker2D.scene));
                                del_tile(sceneMaker2D.penLoc, sceneMaker2D.penRect);
                            }
                        }
                        else
                        {
                            if (sceneMaker2D.dragRect.Width > sceneMaker2D.scene.TileSizePixel.Width ||
                                sceneMaker2D.dragRect.Height > sceneMaker2D.scene.TileSizePixel.Height)
                            {
                                backup(Util.Serialize(sceneMaker2D.scene));
                                paint_zone(sceneMaker2D.dragRect, sceneMaker2D.penRectPixel);
                            }
                            else
                            {
                                backup(Util.Serialize(sceneMaker2D.scene));
                                paint_tile(sceneMaker2D.penLoc, sceneMaker2D.penRectPixel);
                            }
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Point p = pointround(e.Location, sceneMaker2D.scene.TileSizePixel);
                    right_rect.Location = p;
                    right_rect.Size = sceneMaker2D.scene.TileSizePixel;
                    sceneMaker2D.dragRect = right_rect;
                }
            }
            else if (tabControl2.SelectedIndex == 1)
            {
                Point p = pointround(e.Location, sceneMaker2D.scene.TileSizePixel);
                
                paint_code(p, e,scene_code_layer);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                sceneMaker2D.penLoc = pointround(e.Location, sceneMaker2D.scene.TileSizePixel);
                ui_mouse_pos.Text = pointround2(e.Location, sceneMaker2D.scene.TileSizePixel).ToString();
            }
            catch
            { }
            if (tabControl2.SelectedIndex == 0)
            {
                if (e.Button == MouseButtons.Left)
                {
                    if (drawing_code)
                    { }
                    else
                    {
                        if (erasing)
                        {
                            backup(Util.Serialize(sceneMaker2D.scene));
                            del_tile(sceneMaker2D.penLoc, sceneMaker2D.penRect);
                        }
                        else
                        {
                            backup(Util.Serialize(sceneMaker2D.scene));
                            paint_tile(sceneMaker2D.penLoc, sceneMaker2D.penRectPixel);
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    try
                    {
                        Point p = pointround(e.Location, sceneMaker2D.scene.TileSizePixel);
                        p = pointaddpoint(p, new Point(sceneMaker2D.scene.TileSizePixel));
                        right_rect.Size = new Size(pointdelpoint(p, right_rect.Location));
                        sceneMaker2D.dragRect = right_rect;
                    }
                    catch { }
                }
            }
            else if (tabControl2.SelectedIndex == 1)
            {
                Point p = pointround(e.Location, sceneMaker2D.scene.TileSizePixel);
                //int l = 5;
                paint_code(p, e, scene_code_layer);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (tabControl2.SelectedIndex == 0)
            {

                if (e.Button == MouseButtons.Right)
                {
                    try
                    {
                        Point p = pointround(e.Location, sceneMaker2D.scene.TileSizePixel);
                        p = pointaddpoint(p, new Point(sceneMaker2D.scene.TileSizePixel));
                        right_rect.Size = new Size(pointdelpoint(p, right_rect.Location));
                        ui_linkzonex.Text = right_rect.Location.X.ToString();
                        ui_linkzoney.Text = right_rect.Location.Y.ToString();
                        ui_linkzonew.Text = right_rect.Size.Width.ToString();
                        // .Text = right_rect.Location.X.ToString();
                        ui_linkzoneh.Text = right_rect.Size.Height.ToString();
                        sceneMaker2D.dragRect = right_rect;
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
            else if (tabControl2.SelectedIndex == 1)
            {
            }
        }

        void paint_link(Point p)
        {
            sceneMaker2D.scene.CodeLayers[0][p].SceneFileName = ui_link_file.Text;
        }
        void paint_link(Rectangle slt_zone)
        {
            for (int y = slt_zone.Y; y < slt_zone.Bottom; y += sceneMaker2D.scene.TileSizePixel.Height)
            {
                for (int x = slt_zone.X; x < slt_zone.Right; x += sceneMaker2D.scene.TileSizePixel.Width)
                {
                    paint_link(new Point(x, y));
                }
            }
        }
        void del_tile(Point p, Rectangle rect)
        {
            if (sceneMaker2D.drawingLayer < 0)
            {
                return;
            }
            //backup(UtilLib.Serialize(sceneMaker2D.scene));
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

                    int i = contains_tile(pp);
                    if (i > -1)
                    {
                        sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles.RemoveAt(i);
                    }
                }
            }
            ui_tilecount.Text = sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles.Count.ToString();
        }
        void del_zone(Rectangle target_zone, Rectangle slt_zone)
        {
            for (int y = target_zone.Y; y < target_zone.Bottom; y += slt_zone.Height)
            {
                for (int x = target_zone.X; x < target_zone.Right; x += slt_zone.Width)
                {
                    del_tile(new Point(x, y), slt_zone);
                }
            }
        }
        void paint_tile(Point p, Rectangle rect)
        {
            if (sceneMaker2D.drawingLayer < 0)
            {
                return;
            }
            if (sceneMaker2D.mxtName == null)
            {
                return;
            }
            //backup(UtilLib.Serialize(sceneMaker2D.scene));
            int xo, yo;
            int xoo = 0, yoo = 0;
            for (yo = 0; yo < rect.Height; yo += sceneMaker2D.scene.TileSizePixel.Height)
            {
                xoo = 0;
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
                    tf.TextureIndex = game.Textures.GetIndex(tf.TextureFileName);
                    tf.DrawZone.Location = new Point(rect.X + xoo, rect.Y + yoo);
                    tf.DrawZone.Size = game.Textures[sceneMaker2D.mxtName].TileSizePixel;
                    //tf.DrawZone.Size = sceneMaker2D.scene.TileSizePixel;

                    Tile tile = new Tile();
                    tile.Location = pp;
                    tile.Frames.Add(tf);

                    int i = contains_tile(tile.Location);
                    if (i > -1)
                    {
                        if (insFrame)
                        {
                            sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles[i].Frames.Add(tf);
                            
                        }
                        else
                        {
                            sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles[i] = tile;
                        }
                    }
                    else
                    {
                        if (insFrame)
                        { }
                        else
                        {
                            sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles.Add(tile);
                        }
                    }     
                    xoo += game.Textures[sceneMaker2D.mxtName].TileSizePixel.Width;
                }
                yoo += game.Textures[sceneMaker2D.mxtName].TileSizePixel.Height;
            }
            insFrame = false;
            pictureBox1.Cursor = bc;
            ui_tilecount.Text = sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer].Tiles.Count.ToString();
        }
        void paint_zone(Rectangle target_zone, Rectangle slt_zone)
        {
            for (int y = target_zone.Y; y < target_zone.Bottom; y += slt_zone.Height)
            {
                for (int x = target_zone.X; x < target_zone.Right; x += slt_zone.Width)
                {
                    paint_tile(new Point(x, y), slt_zone);
                }
            }
        }
        int contains_tile(Point loc)
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
            int sl = ui_ly_slt.Items.Count - 1;
            sceneMaker2D.drawingLayer = sl - ui_ly_slt.SelectedIndex;
            ui_mouse_pos.Text = "选中层" + sceneMaker2D.drawingLayer.ToString();
        }
        
        private void ui_ly_slt_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (ui_ly_slt.SelectedIndex == -1)
            {
                return;
            }
            int sl = ui_ly_slt.Items.Count - 1;
            if (e.NewValue.ToString() == "Unchecked")
            {
                sceneMaker2D.scene.TileLayers[sl-ui_ly_slt.SelectedIndex].Visible = false;
            }
            else
            {
                sceneMaker2D.scene.TileLayers[sl-ui_ly_slt.SelectedIndex].Visible = true;
            }
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

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileName == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "MetalX Scene File|*.MXScene";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    openFileName = sfd.FileName;
                    Util.SaveObject(openFileName, sceneMaker2D.scene);
                    Text = openFileName;
                }
            }
            else
            {
                Util.SaveObject(openFileName, sceneMaker2D.scene);
            }
        }

        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MetalX Scene File|*.MXScene";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Text = openFileName = ofd.FileName;                
                new_scene(openFileName);
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "MetalX Scene File|*.MXScene";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                openFileName = sfd.FileName;
                Util.SaveObject(openFileName, sceneMaker2D.scene);
                Text = openFileName;
            }
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stack.Count > 0)
            {
                sceneMaker2D.scene = (Scene)Util.Deserialize((byte[])stack.Pop());
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sceneMaker2D == null)
            {
                return;
            }
            drawing_code = false;
            if (tabControl2.SelectedIndex == 1)
            {
                drawing_code = true;
            }
            sceneMaker2D.drawCode = drawing_code;
        }

        private void ui_codelayer_slt_SelectedIndexChanged(object sender, EventArgs e)
        {
            sceneMaker2D.drawCodeLayer = ui_codelayer_slt.SelectedIndex;
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)
            {
                if (sceneMaker2D.drawPen)
                    sceneMaker2D.drawPen = false;
                else
                    sceneMaker2D.drawPen = true;
            }
            scene_code_layer = e.KeyValue - 48;
            if (scene_code_layer < 0 || scene_code_layer > 9)
            {
                scene_code_layer = 0;
            }
        }

        private void tabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.I)
            {
                int i = contains_tile(sceneMaker2D.penLoc);
                if (i > -1)
                {
                    ui_aniframec.Text = "" + sceneMaker2D.scene.TileLayers[sceneMaker2D.drawingLayer][i].FrameCount;
                }
                if (insFrame)
                {
                    insFrame = false;
                    pictureBox1.Cursor = bc;
                }
                else
                {
                    if (pictureBox1.Cursor != System.Windows.Forms.Cursors.UpArrow)
                        bc = pictureBox1.Cursor;
                    insFrame = true;
                    pictureBox1.Cursor = System.Windows.Forms.Cursors.UpArrow;
                }
            } 
        }
        private void ui_link_file_DoubleClick(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "MetalX Scene File|*.MXScene";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ui_link_file.Text = System.IO.Path.GetFileName(ofd.FileName);
                }
            }
        }

        private void ui_link_add_Click(object sender, EventArgs e)
        {
            paint_link(right_rect);
        }

        private void 震动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sceneMaker2D.ShockScreen(1000);
        }

        private void 淡入ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sceneMaker2D.FallInSceen(1000);
        }

        private void 淡出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sceneMaker2D.FallOutSceen(1000);
        }

        private void 说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new HelpBox1().ShowDialog();
        }

        private void ui_play_Click(object sender, EventArgs e)
        {
            if (ui_mus_slt.Text == string.Empty)
            {
                return;
            }
            game.SoundManager.Play(ui_mus_slt.Text);
            timer1.Enabled = true;
        }

        private void ui_stop_Click(object sender, EventArgs e)
        {
            game.SoundManager.Stop();
            timer1.Enabled = false;
            ui_proc.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ui_proc.Value = (int)(game.SoundManager.Progress * 50);
            if (!game.SoundManager.Playing)
            {
                timer1.Enabled = false;
            }
        }

        private void ui_proc_Scroll(object sender, EventArgs e)
        {
            game.SoundManager.Progress = (double)ui_proc.Value / ui_proc.Maximum;
        }

        private void ui_loadmp3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "*.mp3|*.mp3";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    game.SoundManager.PlayMP3(ofd.FileName);
                    timer1.Enabled = true;
                }
            }
        }
    }
}
