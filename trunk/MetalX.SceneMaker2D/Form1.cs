using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.DirectX;

using MetalX;
using MetalX.Component;
using MetalX.Data;
using MetalX.Resource;

namespace MetalX.SceneMaker2D
{
    public partial class Form1 : Form
    {
        List<object> stack = new List<object>(10);

        Cursor bc = Cursors.Cross;
        //bool insFrame = false;
        Game game;
        SceneMaker2D sceneMaker2D;
        bool erasing = false;
        Rectangle right_rect;
        Rectangle left_rect;
        string openFileName;
        int scene_code_layer;
        bool drawing_code = false;

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

        void backup(object obj)
        {
            stack.Add(obj);
            if (stack.Count > 10)
            {
                stack.RemoveAt(0);
            }
            //if (stack.Count < 10)
            //{
            //    stack.Push(obj);
            //}
            //else
            //{
            //    Stack<object> bs = new Stack<object>();
            //    for (int i = 0; i < 9; i++)
            //    {
            //        bs.Push(stack.Pop());
            //    }
            //    stack.Clear();
            //    for (int i = 0; i < 9; i++)
            //    {
            //        stack.Push(bs.Pop());
            //    }
            //}
        }

        int contains_tile(Vector3 loc)
        {
            int i = 0;
            foreach (Tile tile in game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles)
            {
                if (tile.Location == loc)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        void paint_tile(Point p, Rectangle rect)
        {
            if (sceneMaker2D.drawingLayer < 0)
            {
                return;
            }
            if (sceneMaker2D.mxtIndex == -1)
            {
                return;
            }
            int xo, yo;
            int xoo = 0, yoo = 0;
            for (yo = 0; yo < rect.Height; yo += game.SCN.TileSizePixel.Height)
            {
                xoo = 0;
                for (xo = 0; xo < rect.Width; xo += game.SCN.TileSizePixel.Width)
                {
                    Vector3 pp = new Vector3();
                    pp.X = p.X + xo;
                    pp.Y = p.Y + yo;
                    pp.Z = 0;

                    if (pp.X < 0 || pp.Y < 0 || pp.X >= game.SCN.SizePixel.Width || pp.Y >= game.SCN.SizePixel.Height)
                    {
                        continue;
                    }

                    TileFrame tf = new TileFrame();
                    tf.TextureFileName = sceneMaker2D.mxtName;
                    tf.TextureIndex = sceneMaker2D.mxtIndex;
                    tf.DrawZone.Location = new Point(rect.X + xoo, rect.Y + yoo);
                    tf.DrawZone.Size = game.Textures[sceneMaker2D.mxtIndex].TileSizePixel;

                    Tile tile = new Tile();
                    tile.Location = Util.Vector3DivInt(pp, game.SCN.TilePixel);
                    tile.Frames.Add(tf);

                    int i = contains_tile(tile.Location);
                    if (i > -1)
                    {
                        bool err = false;
                        do
                        {
                            try
                            {
                                game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles[i].Frames[sceneMaker2D.edit_frame_index] = tf;
                                err = false;
                            }
                            catch
                            {
                                game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles[i].Frames.Add(new TileFrame());
                                err = true;
                            }
                        }
                        while (err);
                        //if (insFrame)
                        //{
                        //    game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles[i].Frames.Add(tf);
                        //}
                        //else
                        //{
                        //    game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles[i] = tile;
                        //}
                    }
                    else
                    {
                        game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles.Add(tile);
                    }
                    xoo += game.Textures[sceneMaker2D.mxtIndex].TileSizePixel.Width;
                }
                yoo += game.Textures[sceneMaker2D.mxtIndex].TileSizePixel.Height;
            }
            ui_cursorsat.Text = "铅笔";
            pictureBox1.Cursor = bc;
            ui_tilecount.Text = game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles.Count.ToString();
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
                p = Util.PointDivInt(p, game.SCN.TilePixel);
                if (sceneMaker2D.drawCodeLayer == 0)
                {
                    game.SCN.CodeLayer[p].CHRCanRch = val;
                }
                else if (sceneMaker2D.drawCodeLayer == 1)
                {
                    game.SCN.CodeLayer[p].MTLCanRch = val;
                }
                else if (sceneMaker2D.drawCodeLayer == 2)
                {
                    game.SCN.CodeLayer[p].SHPCanRch = val;
                }
                else if (sceneMaker2D.drawCodeLayer == 3)
                {
                    game.SCN.CodeLayer[p].FLTCanRch = val;
                }
                else if (sceneMaker2D.drawCodeLayer == 4)
                {
                    game.SCN.CodeLayer[p].DrawLayer = l;
                }
                else if (sceneMaker2D.drawCodeLayer == 5)
                {
                    game.SCN.CodeLayer[p].IsDesk = val;
                }
            }
            catch { }
        }
        void paint_link(Point p)
        {
            p = Util.PointDivInt(p, game.SCN.TilePixel);
            game.SCN.CodeLayer[p].SceneFileName = ui_link_file.Text;
            game.SCN.CodeLayer[p].ChangeSceneEffect = ui_chgscneff.Checked;
            game.SCN.CodeLayer[p].DefaultLocation = new Point(int.Parse(ui_linkdefx.Text), int.Parse(ui_linkdefy.Text));
            Direction dir = Direction.U;
            if (ui_linkdefdir.Text == "U")
            {
                dir = Direction.U;
            }
            else if (ui_linkdefdir.Text == "L")
            {
                dir = Direction.L;
            }
            else if (ui_linkdefdir.Text == "D")
            {
                dir = Direction.D;
            }
            else if (ui_linkdefdir.Text == "R")
            {
                dir = Direction.R;
            }
            game.SCN.CodeLayer[p].DefaultDirection = dir;
        }
        void paint_link(Rectangle slt_zone)
        {
            for (int y = slt_zone.Y; y < slt_zone.Bottom; y += game.SCN.TileSizePixel.Height)
            {
                for (int x = slt_zone.X; x < slt_zone.Right; x += game.SCN.TileSizePixel.Width)
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
            int xo, yo;
            for (yo = 0; yo < rect.Height; yo += game.SCN.TileSizePixel.Height)
            {
                for (xo = 0; xo < rect.Width; xo += game.SCN.TileSizePixel.Width)
                {
                    Vector3 pp = new Vector3();
                    pp.X = p.X + xo;
                    pp.Y = p.Y + yo;
                    pp.Z = 0;

                    if (pp.X < 0 || pp.Y < 0 || pp.X >= game.SCN.SizePixel.Width || pp.Y >= game.SCN.SizePixel.Height)
                    {
                        continue;
                    }
                    pp = Util.Vector3DivInt(pp, game.SCN.TilePixel);
                    int i = contains_tile(pp);
                    if (i > -1)
                    {
                        game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles.RemoveAt(i);
                    }
                }
            }
            ui_tilecount.Text = game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles.Count.ToString();
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
            //insFrame = false;
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
        void del_link(Point p)
        {
            p = Util.PointDivInt(p, game.SCN.TilePixel);
            game.SCN.CodeLayer[p].SceneFileName = null;
            //game.SCN.CodeLayer[p].
        }

        void update_pic_list()
        {
            ui_pic.Image = null;

            ui_pic_slt.Text = "";
            ui_pic_slt.Items.Clear();
            for (int i = 0; i < game.Textures.Count; i++)
            {
                try
                {
                    if (game.Textures[i].FileIndexer.FolderName.Contains(ui_tilefilter.Text))
                    {
                        ui_pic_slt.Items.Add(game.Textures[i].Name);
                    }
                }
                catch
                { }
            }
        }
        void update_mus_list()
        {
            ui_mus_slt.Text = "";
            //ui_mus_slt.Items.Clear();

            try
            {
                ui_mus_slt.Text = game.SCN.MusicNames[0];
            }
            catch
            { }
            //for (int i = 0; i < game.Audios.Count; i++)
            //{
            //    ui_mus_slt.Items.Add(game.Audios[i].Name);
            //}
        }
        void update_link_info(Point pp)
        {
            ui_linkzonew.Text = (right_rect.Size.Width / game.SCN.TileSizePixel.Width).ToString();
            ui_linkzoneh.Text = (right_rect.Size.Height / game.SCN.TileSizePixel.Height).ToString();
            ui_linkdefx.Text = game.SCN.CodeLayer[pp].DefaultLocation.X.ToString();
            ui_linkdefy.Text = game.SCN.CodeLayer[pp].DefaultLocation.Y.ToString();
            ui_link_file.Text = game.SCN.CodeLayer[pp].SceneFileName;
            ui_linkdefdir.Text = game.SCN.CodeLayer[pp].DefaultDirection.ToString();
            ui_chgscneff.Checked = game.SCN.CodeLayer[pp].ChangeSceneEffect;

        }
        void update_info()
        {
            ui_scenename.Text = game.SCN.Name;
            ui_ly_count.Text = game.SCN.TileLayers.Count.ToString();
            ui_scenew.Text = game.SCN.Size.Width.ToString();
            ui_sceneh.Text = game.SCN.Size.Height.ToString();
            ui_sinw.Text = game.SCN.TileSizePixel.Width.ToString();
            ui_sinh.Text = game.SCN.TileSizePixel.Height.ToString();
            ui_framedelay.Text = game.SCN.FrameInterval.ToString();
        }
        void create_layer_selecter()
        {
            ui_ly_slt.Items.Clear();
            int lc = int.Parse(ui_ly_count.Text);
            for (int i = 0; i < lc; i++)
            {
                game.SCN.TileLayers.Add(new TileLayer());
                ui_ly_slt.Items.Add(lc - 1 - i);
                ui_ly_slt.SetItemChecked(i, true);
            }
            ui_ly_slt.SetSelected(lc - 1, true);
        }
        void update_layer_selecter()
        {
            ui_ly_slt.Items.Clear();
            for (int i = 0; i < game.SCN.TileLayers.Count; i++)
            {
                ui_ly_slt.Items.Add(game.SCN.TileLayers.Count - 1 - i);
                ui_ly_slt.SetItemChecked(i, true);
            }
            ui_ly_slt.SetSelected(game.SCN.TileLayers.Count - 1, true);
        }
        void new_scene(Size size, Size tilesizepixel)
        {
            left_rect = new Rectangle();
            right_rect = new Rectangle();
            if (game != null)
            {
                game.Stop();
            }

            pictureBox1.Size = new Size(size.Width * tilesizepixel.Width, size.Height * tilesizepixel.Height);

            game = new Game(pictureBox1);
            game.InitCom();
            game.InitData();
            game.LoadAllDotPNG(new Size(tilesizepixel.Width / 2, tilesizepixel.Height / 2));
            game.LoadAllDotMXNPC();

            update_pic_list();
            update_mus_list();

            sceneMaker2D = new SceneMaker2D(game);

            int sw = size.Width;
            int sh = size.Height;
            int tw = tilesizepixel.Width;
            int th = tilesizepixel.Height;

            game.SCN = new Scene(new Size(sw, sh), new Size(tw, th));
           
            game.SCN.Name = ui_scenename.Text;
            game.SCN.FrameInterval = int.Parse(ui_framedelay.Text);

            create_layer_selecter();

            tabControl1.SelectedIndex = 1;

            game.MountGameCom(sceneMaker2D);
            game.Start();
        }
        void open_scene(string fileName, bool isxml)
        {
            left_rect = new Rectangle();
            right_rect = new Rectangle();
            if (game != null)
            {
                game.Stop();
            }
            game = new Game(pictureBox1);
            game.InitData();
            game.InitCom();
            game.LoadAllDotPNG(new Size(game.Options.TileSizePixel.Width / 2, game.Options.TileSizePixel.Height / 2));
            game.LoadAllDotMXNPC();

            sceneMaker2D = new SceneMaker2D(game);

            if (isxml)
            {
                game.SCN = game.LoadDotXMLScene(fileName);
            }
            else
            {
                game.SCN = game.LoadDotMXScene(fileName);
            }

            update_pic_list();
            update_mus_list();
            update_npc_list();
            update_info();
            update_layer_selecter();

            pictureBox1.Size = game.SCN.SizePixel;

            tabControl1.SelectedIndex = 1;

            game.MountGameCom(sceneMaker2D);
            game.Start();
        }
        //void open_scenexml(string fileName)
        //{
        //    left_rect = new Rectangle();
        //    right_rect = new Rectangle();
        //    if (game != null)
        //    {
        //        game.Stop();
        //    }
        //    game = new Game(pictureBox1);
        //    game.InitData();
        //    game.InitCom();
        //    game.LoadAllDotPNG(new Size(game.Options.TileSizePixel.Width / 2, game.Options.TileSizePixel.Height / 2));
        //    //game.LoadAllDotMP3(@".\");

        //    sceneMaker2D = new SceneMaker2D(game);

        //    game.SCN = game.LoadDotXMLScene(fileName);
        //    game.SCN.Init();

        //    update_pic_list();
        //    update_mus_list();
        //    update_npc_list();
        //    update_info();
        //    update_layer_selecter();

        //    pictureBox1.Size = game.SCN.SizePixel;

        //    tabControl1.SelectedIndex = 1;

        //    game.MountGameCom(sceneMaker2D);
        //    game.Start();
        //}

        private void ui_createscene_Click(object sender, EventArgs e)
        {
            new_scene(
                new Size(int.Parse(ui_scenew.Text), int.Parse(ui_sceneh.Text)),
                new Size(int.Parse(ui_sinw.Text), int.Parse(ui_sinh.Text)));
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Options o = (Options)Util.LoadObjectXML("options.xml", typeof(Options));
                ui_sinw.Text = o.TileSizePixelX.Width + "";
                ui_sinh.Text = o.TileSizePixelX.Height + "";
                toolStripComboBox1.Text = o.TextureDrawMode.ToString();
                splitContainer1.SplitterDistance = 320;
            canpaint(toolStripButton1.Text);

                //pictureBox1.ContextMenuStrip = menuStrip2;
            }
            catch { }
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

        private void ui_pic_slt_SelectedIndexChanged(object sender, EventArgs e)
        {
            sceneMaker2D.mxtName = ui_pic_slt.Text;
            sceneMaker2D.mxtIndex = game.Textures.GetIndex(sceneMaker2D.mxtName);
            ui_pic.Image = Image.FromStream(new System.IO.MemoryStream(game.Textures[sceneMaker2D.mxtIndex].TextureData));
            ui_pic.Size = new Size(game.Textures[sceneMaker2D.mxtIndex].SizePixel.Width + 2, game.Textures[sceneMaker2D.mxtIndex].SizePixel.Height + 2);
            ui_gridw.Text = game.Textures[sceneMaker2D.mxtIndex].TileSizePixel.Width.ToString();
            ui_gridh.Text = game.Textures[sceneMaker2D.mxtIndex].TileSizePixel.Height.ToString();
            ui_pic.Focus();
        }

        private void ui_pic_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                if (ui_showgrid.Checked)
                {
                    for (int i = 0; i <= game.Textures[sceneMaker2D.mxtIndex].SizePixel.Height; i += game.Textures[sceneMaker2D.mxtIndex].TileSizePixel.Height)
                    {
                        g.DrawLine(new Pen(Color.Blue), 0, i, ui_pic.Size.Width, i);
                    }
                    for (int i = 0; i <= game.Textures[sceneMaker2D.mxtIndex].SizePixel.Width; i += game.Textures[sceneMaker2D.mxtIndex].TileSizePixel.Width)
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
            if (sceneMaker2D == null)
            {
                return;
            }
            sceneMaker2D.drawGrid = ui_showgrid.Checked;
            ui_pic.Refresh();
        }

        private void ui_pic_MouseDown(object sender, MouseEventArgs e)
        {
            if (sceneMaker2D == null)
            {
                return;
            }
            if (sceneMaker2D.mxtIndex <0)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                Point p = pointround(e.Location, game.Textures[sceneMaker2D.mxtIndex].TileSizePixel);
                left_rect.Location = p;
                left_rect.Size = game.Textures[sceneMaker2D.mxtIndex].TileSizePixel;
                ui_pic.Refresh();
            }
        }

        private void ui_pic_MouseMove(object sender, MouseEventArgs e)
        {
            if (sceneMaker2D == null)
            {
                return;
            }
            if (sceneMaker2D.mxtIndex < 0)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                Point p = pointround(e.Location, game.Textures[sceneMaker2D.mxtIndex].TileSizePixel);
                p = pointaddpoint(p, new Point(game.Textures[sceneMaker2D.mxtIndex].TileSizePixel));
                left_rect.Size = new Size(pointdelpoint(p, left_rect.Location));
                ui_pic.Refresh();
            }
        }

        private void ui_pic_MouseUp(object sender, MouseEventArgs e)
        {
            if (sceneMaker2D.mxtIndex < 0)
            {
                return;
            }
            if (e.Button == MouseButtons.Left)
            {
                Point p = pointround(e.Location, game.Textures[sceneMaker2D.mxtIndex].TileSizePixel);
                p = pointaddpoint(p, new Point(game.Textures[sceneMaker2D.mxtIndex].TileSizePixel));
                left_rect.Size = new Size(pointdelpoint(p, left_rect.Location));
                sceneMaker2D.penRect = left_rect;
                ui_pic.Refresh();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (tabControl2.SelectedIndex == 0)
            {
                //ui_is_ani.Checked = game.SCN.TileLayers[sceneMaker2D.drawingLayer][sceneMaker2D.penLoc].IsAnimation;
                if (e.Button == MouseButtons.Left)
                {

                    if (drawing_code)
                    { }
                    else
                    {
                        if (erasing)
                        {
                            if (sceneMaker2D.dragRect.Width > game.SCN.TileSizePixel.Width ||
                                sceneMaker2D.dragRect.Height > game.SCN.TileSizePixel.Height)
                            {
                                backup(Util.Serialize(game.SCN));
                                del_zone(sceneMaker2D.dragRect, sceneMaker2D.penRectPixel);
                            }
                            else
                            {
                                backup(Util.Serialize(game.SCN));
                                del_tile(sceneMaker2D.penLoc, sceneMaker2D.penRectPixel);
                            }
                        }
                        else if (sceneMaker2D.drawPen) 
                        {
                            if (sceneMaker2D.dragRect.Width > game.SCN.TileSizePixel.Width ||
                                sceneMaker2D.dragRect.Height > game.SCN.TileSizePixel.Height)
                            {
                                backup(Util.Serialize(game.SCN));
                                paint_zone(sceneMaker2D.dragRect, sceneMaker2D.penRectPixel);
                            }
                            else
                            {
                                backup(Util.Serialize(game.SCN));
                                paint_tile(sceneMaker2D.penLoc, sceneMaker2D.penRectPixel);
                            }
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    Point pp = pointround2(e.Location, game.SCN.TileSizePixel);
                    ui_linkzonex.Text = pp.X.ToString();
                    ui_linkzoney.Text = pp.Y.ToString();

                    Point p = pointround(e.Location, game.SCN.TileSizePixel);
                    right_rect.Location = p;
                    right_rect.Size = game.SCN.TileSizePixel;
                    sceneMaker2D.dragRect = right_rect;

                    try
                    {
                        //Point pp = pointround2(e.Location, game.SCN.TileSizePixel);
                        ui_is_ani.Checked = game.SCN.TileLayers[sceneMaker2D.drawingLayer][pp].IsAnimation;
                    }
                    catch
                    { }
                }
            }
            else if (tabControl2.SelectedIndex == 1)
            {
                Point p = pointround(e.Location, game.SCN.TileSizePixel);
                paint_code(p, e, scene_code_layer);

            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                sceneMaker2D.penLoc = pointround(e.Location, game.SCN.TileSizePixel);
                ui_mousex.Text = pointround2(e.Location, game.SCN.TileSizePixel).X+"  ";
                ui_mousey.Text = pointround2(e.Location, game.SCN.TileSizePixel).Y+"  ";

                ui_is_ani.Checked = game.SCN.TileLayers[sceneMaker2D.drawingLayer][sceneMaker2D.penLoc].IsAnimation;

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
                            backup(Util.Serialize(game.SCN));
                            del_tile(sceneMaker2D.penLoc, sceneMaker2D.penRectPixel);
                        }
                        else if (sceneMaker2D.drawPen) 
                        {
                            backup(Util.Serialize(game.SCN));
                            paint_tile(sceneMaker2D.penLoc, sceneMaker2D.penRectPixel);
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    try
                    {
                        Point p = pointround(e.Location, game.SCN.TileSizePixel);
                        p = pointaddpoint(p, new Point(game.SCN.TileSizePixel));
                        right_rect.Size = new Size(pointdelpoint(p, right_rect.Location));
                        sceneMaker2D.dragRect = right_rect;
                    }
                    catch { }
                }
            }
            else if (tabControl2.SelectedIndex == 1)
            {
                Point p = pointround(e.Location, game.SCN.TileSizePixel);
                paint_code(p, e, scene_code_layer);
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (tabControl2.SelectedIndex == 0)
            {
                //ui_is_ani.Checked = game.SCN.TileLayers[sceneMaker2D.drawingLayer][sceneMaker2D.penLoc].IsAnimation;
                if (e.Button == MouseButtons.Right)
                {
                    try
                    {
                        Point p = pointround(e.Location, game.SCN.TileSizePixel);

                        p = pointaddpoint(p, new Point(game.SCN.TileSizePixel));
                        right_rect.Size = new Size(pointdelpoint(p, right_rect.Location));

                        sceneMaker2D.dragRect = right_rect;

                        Point pp = pointround2(e.Location, game.SCN.TileSizePixel);

                        update_link_info(pp);
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

        private void ui_ly_slt_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sl = ui_ly_slt.Items.Count - 1;
            sceneMaker2D.drawingLayer = sl - ui_ly_slt.SelectedIndex;
            ui_tilecount.Text = "选中层" + sceneMaker2D.drawingLayer.ToString();
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
                game.SCN.TileLayers[sl - ui_ly_slt.SelectedIndex].Visible = false;
            }
            else
            {
                game.SCN.TileLayers[sl - ui_ly_slt.SelectedIndex].Visible = true;
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
        void saveto(string filename)
        {
            if (filename == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "MetalX Scene File|*.MXScene|XML Scene File|*.XMLScene";
                sfd.RestoreDirectory = true;
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    openFileName = sfd.FileName;
                    if (game.SCN.CodeLayers.Count > 1)
                    {
                        game.SCN.CodeLayers.RemoveAt(1);
                    }
                    //for (int l = 0; l < game.SCN.TileLayers.Count; l++)
                    //{
                    //    for (int i = 0; i < game.SCN.TileLayers[l].Tiles.Count; i++)
                    //    {
                    //        Vector3 loc = game.SCN.TileLayers[l][i].Location;
                    //        loc.X /= game.SCN.TilePixel;
                    //        loc.Y /= game.SCN.TilePixel;
                    //        loc.Z /= game.SCN.TilePixel;
                    //        game.SCN.TileLayers[l][i].Location = loc;
                    //    }
                    //}
                    //for (int i = 0; i < game.SCN.CodeLayer.Codes.Count; i++)
                    //{
                    //    Point loc = game.SCN.CodeLayer.Codes[i].Location;
                    //    loc.X /= game.SCN.TilePixel;
                    //    loc.Y /= game.SCN.TilePixel;
                    //    game.SCN.CodeLayer.Codes[i].Location = loc;
                    //}
                    if (sfd.FilterIndex == 1)
                    {
                        Util.SaveObject(openFileName, game.SCN);
                    }
                    else if (sfd.FilterIndex == 2)
                    {
                        Util.SaveObjectXML(openFileName, game.SCN);
                    }
                    Text = openFileName;
                    MessageBox.Show("保存成功");
                }
            }
            else
            {
                Util.SaveObject(openFileName, game.SCN);
                MessageBox.Show("保存成功");
            }
        }
        //void savetoxml(string filename)
        //{
        //    if (filename == null)
        //    {
        //        SaveFileDialog sfd = new SaveFileDialog();
        //        sfd.Filter = "MetalX Scene File|*.MXScene";
        //        sfd.RestoreDirectory = true;
        //        if (sfd.ShowDialog() == DialogResult.OK)
        //        {
        //            openFileName = sfd.FileName;
        //            if (game.SCN.CodeLayers.Count > 1)
        //            {
        //                game.SCN.CodeLayers.RemoveAt(1);
        //            }
        //            Util.SaveObjectXML(openFileName + ".xml", game.SCN);
        //            Text = openFileName;
        //        }
        //    }
        //    else
        //    {
        //        Util.SaveObject(openFileName, game.SCN);
        //    }
        //}


        private void 打开ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MetalX Scene File|*.MXScene|XML Scene File|*.XMLScene";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Text = openFileName = ofd.FileName;
                bool xml = true;
                if (ofd.FilterIndex == 1)
                {
                    xml = false;
                }
                open_scene(openFileName, xml);
            }
        }

        private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveto(openFileName);
        }
        private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveto(null);
        }

        private void 撤销ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (stack.Count > 0)
            {
                int i = stack.Count - 1;
                game.SCN = (Scene)Util.Deserialize((byte[])stack[i]);
                stack.RemoveAt(i);
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //contextMenuStrip1.Opacity = 100;
            //sceneMaker2D.drawPen = true;
            canpaint("绘制中");
            if (sceneMaker2D == null)
            {
                return;
            }
            drawing_code = false;
            if (tabControl2.SelectedIndex == 1)
            {
                drawing_code = true;
                sceneMaker2D.ColorFilter = Color.FromArgb(100, Color.Red);
                //contextMenuStrip1.Opacity = 0;
                //sceneMaker2D.drawPen = false;
                canpaint("查看中");

            }
            sceneMaker2D.drawCode = drawing_code;
        }

        private void ui_codelayer_slt_SelectedIndexChanged(object sender, EventArgs e)
        {
            sceneMaker2D.drawCodeLayer = ui_codelayer_slt.SelectedIndex;
        }

        private void tabControl1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up)
                {
                    splitContainer1.Panel2.VerticalScroll.Value -= 10;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    splitContainer1.Panel2.HorizontalScroll.Value -= 10;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    splitContainer1.Panel2.VerticalScroll.Value += 10;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    splitContainer1.Panel2.HorizontalScroll.Value += 10;
                }
                else if (e.KeyCode == Keys.P)
                {

                }
                else if (e.KeyCode == Keys.Subtract)
                {
                    scene_code_layer = -1;
                }
                else if (e.KeyCode == Keys.Home)
                {
                    for (int i = 0; i < game.SCN.CodeLayer.Codes.Count; i++)
                    {
                        game.SCN.CodeLayer.Codes[i].RchDisappear = -1;
                    }
                }
                scene_code_layer = e.KeyValue - 48;
                if (scene_code_layer < 0 || scene_code_layer > 9)
                {
                    scene_code_layer = 0;
                }
            }
            catch
            { }
        }

        private void tabControl1_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.I)
            //{
            //    int i = contains_tile(Util.Vector3DivInt(Util.Point2Vector3(sceneMaker2D.penLoc, 0), game.Options.TilePixel));
            //    if (i > -1)
            //    {
            //        ui_aniframec.Text = "" + game.SCN.TileLayers[sceneMaker2D.drawingLayer][i].FrameCount;

            //        if (insFrame)
            //        {
            //            insFrame = false;
            //            ui_cursorsat.Text = "铅笔";
            //            pictureBox1.Cursor = bc;
            //        }
            //        else
            //        {
            //            if (pictureBox1.Cursor != System.Windows.Forms.Cursors.UpArrow)
            //                bc = pictureBox1.Cursor;
            //            insFrame = true;
            //            ui_cursorsat.Text = "插入帧";
            //            pictureBox1.Cursor = System.Windows.Forms.Cursors.UpArrow;
            //        }
            //    }
            //}
        }

        private void ui_link_file_DoubleClick(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "MetalX Scene File|*.MXScene";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ui_link_file.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                }
            }
        }

        private void ui_link_add_Click(object sender, EventArgs e)
        {
            paint_link(right_rect);
        }

        private void ui_link_del_Click(object sender, EventArgs e)
        {
            del_link(right_rect.Location);
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
            game.PlayMetalXAudio(1, ui_mus_slt.Text);
            timer1.Enabled = true;
        }

        private void ui_stop_Click(object sender, EventArgs e)
        {
            game.StopAudio();
            timer1.Enabled = false;
            ui_proc.Value = 0;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ui_proc.Value = (int)(game.AudioPlayingProgress * 50);
            if (!game.AudioIsPlaying)
            {
                timer1.Enabled = false;
            }
        }

        private void ui_proc_Scroll(object sender, EventArgs e)
        {
            game.AudioPlayingProgress = (double)ui_proc.Value / ui_proc.Maximum;
        }

        private void ui_loadmp3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "*.mp3|*.mp3";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    game.PlayMP3Audio(1, ofd.FileName, ui_loop.Checked);
                    timer1.Enabled = true;
                }
            }
        }

        private void ui_chg_tile_size_Click(object sender, EventArgs e)
        {
            game.Textures[sceneMaker2D.mxtIndex].TileSizePixel = new Size(int.Parse(ui_gridw.Text), int.Parse(ui_gridh.Text));
        }

        private void 显示隐藏网格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ui_showgrid.Checked)
            {
                ui_showgrid.Checked = false;
            }
            else
            {
                ui_showgrid.Checked = true;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
        }

        private void 放大ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            game.Options.X += 1;
            game.SetCamera(new Vector3(0, 0, 22.5f), new Vector3(), game.Options.X);
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }
            if (toolStripComboBox1.Text == "Direct3D")
            {
                game.Options.TextureDrawMode = TextureDrawMode.Direct3D;
            }
            else if (toolStripComboBox1.Text == "Direct2D")
            {
                game.Options.TextureDrawMode = TextureDrawMode.Direct2D;
            }
        }

        private void 输出optionsxmlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (game == null)
            {
                return;
            }
            Util.SaveObjectXML("options.xml", game.Options);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Point p = Util.PointDivInt(sceneMaker2D.penLoc, game.Options.TilePixel);
            game.SCN.TileLayers[sceneMaker2D.drawingLayer][p].IsAnimation = true;
            ui_is_ani.Checked = game.SCN.TileLayers[sceneMaker2D.drawingLayer][p].IsAnimation;
        }

        private void 取消动画图元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point p = Util.PointDivInt(sceneMaker2D.penLoc, game.Options.TilePixel);
            game.SCN.TileLayers[sceneMaker2D.drawingLayer][p].IsAnimation = false;
            ui_is_ani.Checked = game.SCN.TileLayers[sceneMaker2D.drawingLayer][p].IsAnimation;
        }
        #region npc operation
        void update_npc_list()
        {
            //ui_npclist.Items.Clear();
            //foreach (FileLink fl in game.NPCFiles)
            //{
            //    ui_npclist.Items.Add(fl.Name);
            //}
            game.LoadAllDotMXNPC();
            sceneMaker2D.InitNPCs();
            ui_npclist.Items.Clear();
            foreach (NPC npc in game.NPCs)
            {
                ui_npclist.Items.Add(npc.Name + " (" + npc.RealLocation.X + "," + npc.RealLocation.Y + ")");
            }
        }

        private void ui_npcpic_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "*.png|*.png";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    ui_npctexturename.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                }
            }
        }
        private void ui_npcadd_Click(object sender, EventArgs e)
        {
            NPC npc = new NPC();

            npc.CanMove = ui_canmove.Checked;
            npc.CanTurn = ui_canturn.Checked;
            npc.TextureName = ui_npctexturename.Text;
            npc.Name = ui_npcname.Text;
            npc.Script = ui_npccode.Text;
            npc.IsBox = ui_npcisbox.Checked;
            npc.IsDoor = ui_npcisdoor.Checked;
            npc.Size = new Size(int.Parse(ui_npcw.Text), int.Parse(ui_npch.Text));

            npc.SetRealLocation(int.Parse(ui_npcx.Text), int.Parse(ui_npcy.Text), 0, game.Options.TilePixel);
            Direction dir = Direction.U;
            if (ui_npcdir.Text == "U")
            {
                dir = Direction.U;
            }
            else if (ui_npcdir.Text == "L")
            {
                dir = Direction.L;
            }
            else if (ui_npcdir.Text == "D")
            {
                dir = Direction.D;
            }
            else if (ui_npcdir.Text == "R")
            {
                dir = Direction.R;
            }
            npc.DefaultDirection = npc.RealDirection = dir;
            //npc.Dialog = ui_npctxt.Text;

            if (game.NPCs == null)
            {
                game.NPCs = new List<NPC>();
            }
            game.NPCs.Add(npc);

            if (game.SCN.NPCNames == null)
            {
                game.SCN.NPCNames = new List<string>();
            }
            game.SCN.NPCNames.Add(npc.Name);

            //game.LoadAllDotMXNPC();            
            //string fn = Guid.NewGuid().ToString();

            Util.SaveObject(game.Options.RootPath + "npcs\\" + npc.Name + ".MXNPC", npc);

            update_npc_list();
        }

        private void ui_npcdel_Click(object sender, EventArgs e)
        {
            int i = ui_npclist.SelectedIndex;
            if (i < 0)
            {
                return;
            }
            game.NPCs.RemoveAt(i);

            System.IO.File.Delete(game.Options.RootPath + "npcs\\" + game.SCN.NPCNames[i] + ".MXNPC");

            game.SCN.NPCNames.RemoveAt(i);
            update_npc_list();
        }

        private void ui_npclist_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = ui_npclist.SelectedIndex;
            if (i < 0)
            {
                return;
            }
            NPC npc = game.NPCs[i];

            ui_canmove.Checked = npc.CanMove;
            ui_canturn.Checked = npc.CanTurn;

            ui_npccode.Text = npc.Script;
            ui_npcname.Text = npc.Name;
            ui_npcx.Text = npc.RealLocation.X + "";
            ui_npcy.Text = npc.RealLocation.Y + "";
            //ui_npctxt.Text = npc.DialogText;
            ui_npctexturename.Text = npc.TextureName;
            ui_npcisbox.Checked = npc.IsBox;

            ui_npcisdoor.Checked = npc.IsDoor;

            ui_npcw.Text = npc.Size.Width + "";
            ui_npch.Text = npc.Size.Height + "";

            if (npc.RealDirection == Direction.U)
            {
                ui_npcdir.Text = "U";
            }
            else if (npc.RealDirection == Direction.L)
            {
                ui_npcdir.Text = "L";
            }
            else if (npc.RealDirection == Direction.D)
            {
                ui_npcdir.Text = "D";
            }
            else if (npc.RealDirection == Direction.R)
            {
                ui_npcdir.Text = "R";
            }
        }
        #endregion

        private void ui_npcmodify_Click(object sender, EventArgs e)
        {
            int i = ui_npclist.SelectedIndex;
            if (i < 0)
            {
                return;
            }
            NPC npc = new NPC();
            npc.CanMove = ui_canmove.Checked;
            npc.CanTurn = ui_canturn.Checked;
            npc.IsBox = ui_npcisbox.Checked;
            npc.Script = ui_npccode.Text;
            npc.TextureName = ui_npctexturename.Text;
            npc.Name = ui_npcname.Text;
            npc.IsBox = ui_npcisbox.Checked;
            npc.IsDoor = ui_npcisdoor.Checked;
            npc.SetRealLocation(int.Parse(ui_npcx.Text), int.Parse(ui_npcy.Text), 0, game.Options.TilePixel);
            npc.Size = new Size(int.Parse(ui_npcw.Text), int.Parse(ui_npch.Text));
            Direction dir = Direction.U;
            if (ui_npcdir.Text == "U")
            {
                dir = Direction.U;
            }
            else if (ui_npcdir.Text == "L")
            {
                dir = Direction.L;
            }
            else if (ui_npcdir.Text == "D")
            {
                dir = Direction.D;
            }
            else if (ui_npcdir.Text == "R")
            {
                dir = Direction.R;
            }
            npc.DefaultDirection = npc.RealDirection = dir;
            //npc.DialogText = ui_npctxt.Text;

            if (game.NPCs == null)
            {
                game.NPCs = new List<NPC>();
            }
            game.NPCs[i] = npc;

            System.IO.File.Delete(game.Options.RootPath + "npcs\\" + game.SCN.NPCNames[i] + ".MXNPC");

            game.SCN.NPCNames[i] = npc.Name;

            Util.SaveObject(game.Options.RootPath + "npcs\\" + npc.Name + ".MXNPC", npc);

            update_npc_list();
        }

        private void ui_mus_add_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "*.mp3|*.mp3";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string fn = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                    game.SCN.MusicNames = new List<string>();
                    game.SCN.MusicNames.Add(fn);

                    ui_mus_slt.Text = fn;
                }
            }
        }

        private void ui_changescene_Click(object sender, EventArgs e)
        {
            game.SCN.Name = ui_scenename.Text;
            game.SCN.TileSizePixel = new Size(int.Parse(ui_sinw.Text), int.Parse(ui_sinh.Text));
            game.SCN.Size = new Size(int.Parse(ui_scenew.Text), int.Parse(ui_sceneh.Text));
            game.SCN.FrameInterval = int.Parse(ui_framedelay.Text);
            update_info();
        }

        private void ui_ly_del_Click(object sender, EventArgs e)
        {
            int li = ui_ly_slt.Items.Count - 1 - ui_ly_slt.SelectedIndex;
            game.SCN.TileLayers.RemoveAt(li);
            update_layer_selecter();
        }

        private void ui_ly_add_Click(object sender, EventArgs e)
        {
            game.SCN.TileLayers.Add(new TileLayer());
            update_layer_selecter();

        }

        private void 设为打底层图元ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point p = Util.PointDivInt(sceneMaker2D.penLoc, game.Options.TilePixel);
            game.SCN.BottomTile = game.SCN.TileLayers[sceneMaker2D.drawingLayer][p];

        }

        private void ui_init_code_layer_Click(object sender, EventArgs e)
        {
            //if (game.SCN.CodeLayers.Count > 0)
            {
                game.SCN.CodeLayers.Clear();
                game.SCN.CodeLayers.Add(new CodeLayer());
                //Size = s;
                int i = 0;
                for (int y = 0; y < game.SCN.Size.Height; y++)
                {
                    for (int x = 0; x < game.SCN.Size.Width; x++)
                    {
                        game.SCN.CodeLayer.Codes.Add(new Code());
                        game.SCN.CodeLayer.Codes[i++].Location = new Point(x, y);
                    }
                }
            }
        }

        private void ui_mus_del_Click(object sender, EventArgs e)
        {
            game.SCN.MusicNames.Clear();
        }

        private void ui_filte_Click(object sender, EventArgs e)
        {
            update_pic_list();
        }

        private void ui_npctexturename_DoubleClick(object sender, EventArgs e)
        {
            ui_npctexturename.Text = ui_pic_slt.Text;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            canpaint(toolStripButton1.Text);
        }

        void canpaint(string arg)
        {
            if (arg == "绘制中")
            {
                contextMenuStrip1.Enabled = true;
                contextMenuStrip1.Opacity = 100;
                toolStripButton1.Text = "查看中";
                sceneMaker2D.drawPen = false;
            }
            else
            {
                contextMenuStrip1.Enabled = false;
                contextMenuStrip1.Opacity = 0;
                toolStripButton1.Text = "绘制中";
                sceneMaker2D.drawPen = true;
            } 
        }

        private void 设置连接点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

        private void 编辑脚本ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int x = int.Parse(ui_linkzonex.Text);
            int y = int.Parse(ui_linkzoney.Text);
            EditScript es = new EditScript();
            es.code = game.SCN.CodeLayer[x,y];
            es.ShowDialog();
        }

        private void 动画ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new DotMXMovieMaker().ShowDialog();
        }

        private void ui_slt_frame_SelectedIndexChanged(object sender, EventArgs e)
        {
            sceneMaker2D.edit_frame_index = int.Parse(ui_slt_frame.Text);
        }

        private void ui_edit_frame_CheckedChanged(object sender, EventArgs e)
        {
            sceneMaker2D.edit_frame = ui_edit_frame.Checked;
        }

        private void ui_move_Click(object sender, EventArgs e)
        {
            if (sceneMaker2D == null)
            {
                return;
            }
            if (game.SCN == null)
            {
                return;
            }
            int step = int.Parse(ui_movestep.Text);
            Direction dir = Direction.U;
            if (ui_movedir.Text == "U")
            {
                dir = Direction.U;
            }
            else if (ui_movedir.Text == "L")
            {
                dir = Direction.L;
            }
            else if (ui_movedir.Text == "D")
            {
                dir = Direction.D;
            }
            else if (ui_movedir.Text == "R")
            {
                dir = Direction.R;
            }
            if (ui_moveall.Checked)
            {
                for (int l = 0; l < game.SCN.TileLayers.Count; l++)
                {
                    for (int i = 0; i < game.SCN.TileLayers[l].Tiles.Count; i++)
                    {
                        Tile t = game.SCN.TileLayers[l].Tiles[i];
                        if (dir == Direction.U)
                        {
                            t.Location.Y--;
                        }
                        else if (dir == Direction.L)
                        {
                            t.Location.X--;
                        }
                        else if (dir == Direction.D)
                        {
                            t.Location.Y++;
                        }
                        else if (dir == Direction.R)
                        {
                            t.Location.X++;
                        }

                    }
                }
                for (int l = 0; l < game.SCN.TileLayers.Count; l++)
                {
                    for (int i = 0; i < game.SCN.TileLayers[l].Tiles.Count; i++)
                    {
                        Tile t = game.SCN.TileLayers[l].Tiles[i];
                        if (game.SCN.IsInScene(t.Location) == false)
                        {
                            game.SCN.TileLayers[l].Tiles.RemoveAt(i);
                        }
                    }
                }
            }
            else
            {
                int l = sceneMaker2D.drawingLayer;
                //for (int l = 0; l < game.SCN.TileLayers.Count; l++)
                {
                    for (int i = 0; i < game.SCN.TileLayers[l].Tiles.Count; i++)
                    {
                        Tile t = game.SCN.TileLayers[l].Tiles[i];
                        if (dir == Direction.U)
                        {
                            t.Location.Y--;
                        }
                        else if (dir == Direction.L)
                        {
                            t.Location.X--;
                        }
                        else if (dir == Direction.D)
                        {
                            t.Location.Y++;
                        }
                        else if (dir == Direction.R)
                        {
                            t.Location.X++;
                        }

                    }
                }
                //for (int l = 0; l < game.SCN.TileLayers.Count; l++)
                {
                    for (int i = 0; i < game.SCN.TileLayers[l].Tiles.Count; i++)
                    {
                        Tile t = game.SCN.TileLayers[l].Tiles[i];
                        if (game.SCN.IsInScene(t.Location) == false)
                        {
                            game.SCN.TileLayers[l].Tiles.RemoveAt(i);
                        }
                    }
                }

                ui_tilecount.Text = game.SCN.TileLayers[sceneMaker2D.drawingLayer].Tiles.Count.ToString();
            }

            //foreach (TileLayer tl in game.SCN.TileLayers)
            //{
            //    foreach (Tile t in tl.Tiles)
            //    {
                    
            //    }
            //}
        }

    }
}
