using System;
using System.Collections.Generic;
using System.Drawing;

using MetalX;
using MetalX.Data;
using MetalX.Component;

using Microsoft.DirectX;

namespace MetalX.SceneMaker2D
{
    public class SceneMaker2D : SceneManager
    {
        public bool edit_frame;
        public int edit_frame_index;
        public bool drawGrid;
        public bool drawCod;
        public bool drawCode
        {
            get
            {
                return drawCod;
            }
            set
            {
                drawCod = value;
                if (value)
                {
                    ColorFilter = Color.FromArgb(200, ColorFilter);
                }
                else
                {
                    ColorFilter = Color.White;
                }
            }
        }
        public bool drawPen;
        public int drawCodeLayer=0;
        public Rectangle dragRect;
        public string mxtName;
        public int mxtIndex = -1;
        public Point penLoc;
        public Rectangle penRect;
        public Rectangle penRectPixel
        {
            get
            {
                if (mxtIndex == -1)
                {
                    return new Rectangle(penRect.Location, SCN.TileSizePixel);
                }
                float wx = penRect.Width / game.Textures[mxtIndex].TileSizePixel.Width;
                float hx = penRect.Height / game.Textures[mxtIndex].TileSizePixel.Height;
                return new Rectangle(penRect.Location, new Size((int)(wx * SCN.TileSizePixel.Width), (int)(hx * SCN.TileSizePixel.Height)));
            }
        }
        public Size penRectLogic
        {
            get
            {
                return new Size(penRect.Width / SCN.TileSizePixel.Width, penRect.Height / SCN.TileSizePixel.Height);
            }
        }
        public int drawingLayer = -1;

        
        //int frameIndex;
        //DateTime lastFrameBeginTime = DateTime.Now;


        public SceneMaker2D(Game metalx)
            : base(metalx)
        {
            drawGrid = false;
            drawPen = true;
            frameIndex = 0;
            drawCode = false;
            drawCodeLayer = 0;

            dragRect = new Rectangle();
            penRect = new Rectangle();

            game.Textures.Add(game.LoadDotMXT(Properties.Resources.o), "smo");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources.x), "smx");

            game.Textures.Add(game.LoadDotMXT(Properties.Resources._0), "sm0");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources._1), "sm1");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources._2), "sm2");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources._3), "sm3");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources._4), "sm4");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources._5), "sm5");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources._6), "sm6");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources._7), "sm7");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources._8), "sm8");
            game.Textures.Add(game.LoadDotMXT(Properties.Resources._9), "sm9");
        }

        public override void Code()
        {
            base.Code();
        }
        void draw_scene()
        {
            foreach (TileLayer tl in SCN.TileLayers)
            {
                if (tl.Visible)
                {
                    foreach (Tile t in tl.Tiles)
                    {
                        //Point p = t.GetLocationPixelPoint(scene.TilePixel);
                        int fi = 0;
                        if (edit_frame == false)
                        {
                            fi = frameIndex;
                            game.DrawMetalXTexture(
                               game.Textures[t[fi].TextureIndex],
                               t[fi].DrawZone,
                               Util.PointMulInt(t.LocationPoint, SCN.TilePixel),
                               SCN.TileSizePixel,
                               0,
                               ColorFilter
                               );
                        }
                        else
                        {
                            fi = edit_frame_index;

                            if (t.Frames.Count>fi)
                            {
                                game.DrawMetalXTexture(
                                    game.Textures[t.Frames[fi].TextureIndex],
                                    t.Frames[fi].DrawZone,
                                    Util.PointMulInt(t.LocationPoint, SCN.TilePixel),
                                    SCN.TileSizePixel,
                                    0,
                                    ColorFilter
                                    );
                            }
                        }
                    }
                }
            }
        }

        public override void Draw()
        {
            //base.Draw();
            //DrawSceneTest(SCN);
            draw_scene();
            //foreach (CodeLayer cl in SCN.CodeLayers)
            {
                foreach (Code c in SCN.CodeLayer.Codes)
                {
                    if (c.SceneFileName != null)
                    {
                        draw_link(c.Location);
                    }
                }
            }
            if (NPCs != null)
            {
                foreach (NPC npc in NPCs)
                {
                    draw_npc(npc);
                }
            }
            if(drawPen)
            draw_pen();        
            if (drawGrid||drawCode)
            {
                draw_grid();
            }            
            if (drawCode)
            {
                draw_code();
            }
            game.DrawRect(dragRect, Color.Red);

            //game.DrawText(" FPS: " + game.AverageFPS.ToString("f1"), new Point(), Color.White);
        }
        void draw_npc(NPC npc)
        {
            if (npc == null)
            {
                return;
            }
            if (npc.TextureName == null)
            {
                return;
            }
            if (npc.Invisible)
            {
                return;
            }
            //if (npc.TextureIndex < 0)
            {
                npc.TextureIndex = game.Textures.GetIndex(npc.TextureName);
            }
            if (npc.TextureIndex < 0)
            {
                return;
            }
            Rectangle dz = new Rectangle();
            dz.Y = (int)npc.RealDirection * game.Textures[npc.TextureIndex].TileSizePixel.Height;
            if (npc.NeedMovePixel > 0)
            {
                dz.X = (((int)((float)game.Options.TilePixel - npc.NeedMovePixel)) / (game.Options.TileSizePixelX.Width / 4) + 1) * game.Textures[npc.TextureIndex].TileSizePixel.Width;
            }
            else
            {
                dz.X = 0;
            }
            dz.Size = game.Textures[npc.TextureIndex].TileSizePixel;
            Vector3 v31 = npc.RealLocationPixel;
            v31.Y += game.Options.SpriteOffsetPixel;
            v31.X += SCN.RealLocationPixel.X;
            v31.Y += SCN.RealLocationPixel.Y;
            v31.Z += SCN.RealLocationPixel.Z;
            v31 = Util.Vector3AddVector3(v31, ScreenOffsetPixel);
            game.DrawMetalXTexture(
                game.Textures[npc.TextureIndex],
                dz,
                v31,
                game.Options.TileSizePixelX,
                0,
                Color.White);
        }

        void draw_grid()
        {
            for (int i = 0; i <= SCN.SizePixel.Width; i += SCN.TileSizePixel.Width)
            {
                game.DrawLine(i, 0, i, SCN.SizePixel.Height, Color.Blue);
            }
            for (int i = 0; i <= SCN.SizePixel.Height; i += SCN.TileSizePixel.Height)
            {
                game.DrawLine(0, i, SCN.SizePixel.Width, i, Color.Blue);
            }
        }
        void draw_code()
        {
            Color cf = Color.White;
            Point o = new Point(8, 8);
            if (drawCodeLayer == 0)
            {
                foreach (Code c in SCN.CodeLayer.Codes)
                {
                    cf = Color.White;
                    string str = "smo";
                    if (!c.CHRCanRch)
                    {
                        str = "smx";
                        cf = Color.Yellow;
                    }
                    game.DrawMetalXTexture(game.Textures[str], new Rectangle(new Point(), new Size(16, 16)), Util.PointAddPoint(o, Util.PointMulInt(c.Location, SCN.TilePixel)),0, cf);
                }
            }
            else if (drawCodeLayer == 1)
            {
                foreach (Code c in SCN.CodeLayer.Codes)
                {
                    cf = Color.White;
                    string str = "smo";
                    if (!c.MTLCanRch)
                    {
                        str = "smx";
                        cf = Color.Yellow;
                    }
                    game.DrawMetalXTexture(game.Textures[str], new Rectangle(new Point(), new Size(16, 16)), Util.PointAddPoint(o, Util.PointMulInt(c.Location, SCN.TilePixel)),0, cf);
                }
            }
            else if (drawCodeLayer == 2)
            {
                foreach (Code c in SCN.CodeLayer.Codes)
                {
                    cf = Color.White;
                    string str = "smo";
                    if (!c.SHPCanRch)
                    {
                        str = "smx";
                        cf = Color.Yellow;
                    }
                    game.DrawMetalXTexture(game.Textures[str], new Rectangle(new Point(), new Size(16, 16)), Util.PointAddPoint(o, Util.PointMulInt(c.Location, SCN.TilePixel)),0, cf);
                }

            }
            else if (drawCodeLayer == 3)
            {
                foreach (Code c in SCN.CodeLayer.Codes)
                {
                    cf = Color.White;
                    string str = "smo";
                    if (!c.FLTCanRch)
                    {
                        str = "smx";
                        cf = Color.Yellow;
                    }
                    game.DrawMetalXTexture(game.Textures[str], new Rectangle(new Point(), new Size(16, 16)), Util.PointAddPoint(o, Util.PointMulInt(c.Location, SCN.TilePixel)),0, cf);
                }

            }
            else if (drawCodeLayer == 4)
            {
                foreach (Code c in SCN.CodeLayer.Codes)
                {
                    string str = c.DrawLayer.ToString();
                    str = "sm" + str;
                    game.DrawMetalXTexture(game.Textures[str], new Rectangle(new Point(), new Size(16, 16)), Util.PointAddPoint(o, Util.PointMulInt(c.Location, SCN.TilePixel)),0, cf);
                }
            }
            else if (drawCodeLayer == 5)
            {
                foreach (Code c in SCN.CodeLayer.Codes)
                {
                    cf = Color.White;
                    string str = "smo";
                    if (!c.IsDesk)
                    {
                        str = "smx";
                        cf = Color.Yellow;
                    }
                    game.DrawMetalXTexture(game.Textures[str], new Rectangle(new Point(), new Size(16, 16)), Util.PointAddPoint(o, Util.PointMulInt(c.Location, SCN.TilePixel)),0, cf);
                }

            }
        }
        void draw_pen()
        {
            game.DrawMetalXTexture(
                game.Textures[mxtIndex],
                penRect, 
                penLoc,
                penRectPixel.Size, 
                0,
                Color.FromArgb(100, Color.White));
        }
        void draw_link(Point p)
        {
            p = Util.PointMulInt(p, SCN.TilePixel);
            game.DrawRect(new Rectangle(p, SCN.TileSizePixel), Color.Green);
        }
    }
}
