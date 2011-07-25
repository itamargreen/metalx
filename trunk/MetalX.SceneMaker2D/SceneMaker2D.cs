using System;
using System.Collections.Generic;
using System.Drawing;

using MetalX;
using MetalX.Data;
using MetalX.Component;

using Microsoft.DirectX;

namespace MetalX.SceneMaker2D
{
    public class SceneMaker2D : GameCom
    {
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
                    return new Rectangle(penRect.Location, scene.TileSizePixel);
                }
                float wx = penRect.Width / game.Textures[mxtIndex].TileSizePixel.Width;
                float hx = penRect.Height / game.Textures[mxtIndex].TileSizePixel.Height;
                return new Rectangle(penRect.Location, new Size((int)(wx * scene.TileSizePixel.Width), (int)(hx * scene.TileSizePixel.Height)));
            }
        }
        public Size penRectLogic
        {
            get
            {
                return new Size(penRect.Width / scene.TileSizePixel.Width, penRect.Height / scene.TileSizePixel.Height);
            }
        }
        public int drawingLayer = -1;

        
        int frameIndex;
        DateTime lastFrameBeginTime = DateTime.Now;

        public Scene scene;

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
        }

        public override void Code()
        {
            base.Code();
            TimeSpan ts = DateTime.Now - lastFrameBeginTime;
            if (ts.TotalMilliseconds > scene.FrameInterval)
            {
                frameIndex++;
                lastFrameBeginTime = DateTime.Now;
            }
        }

        public override void Draw()
        {
            //base.Draw();
            foreach (TileLayer tl in scene.TileLayers)
            {
                if (tl.Visible)
                {
                    foreach (Tile t in tl.Tiles)
                    {
                        //Point p = t.GetLocationPixelPoint(scene.TilePixel);
                        game.DrawMetalXTexture(
                            game.Textures[t[frameIndex].TextureIndex],
                            t[frameIndex].DrawZone,
                            Util.PointMulInt(t.LocationPoint, scene.TilePixel),
                            scene.TileSizePixel,
                            ColorFilter
                            );
                    }
                }
            }
            //foreach (CodeLayer cl in scene.CodeLayers)
            {
                foreach (Code c in scene.CodeLayer.Codes)
                {
                    if (c.SceneFileName != null)
                    {
                        draw_link(c.Location);
                    }
                }
            }
            if (scene.NPCs != null)
            {
                foreach (NPC npc in scene.NPCs)
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
            //if (npc.TextureIndex < 0)
            {
                npc.TextureIndex = game.Textures.GetIndex(npc.TextureName);
            }
            Rectangle dz = new Rectangle();
            dz.Y = (int)npc.Direction * game.Textures[npc.TextureIndex].TileSizePixel.Height;
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
            v31.X += scene.RealLocationPixel.X;
            v31.Y += scene.RealLocationPixel.Y;
            v31.Z += scene.RealLocationPixel.Z;
            v31 = Util.Vector3AddVector3(v31, ScreenOffsetPixel);
            game.DrawMetalXTexture(
                game.Textures[npc.TextureIndex],
                dz,
                v31,
                game.Options.TileSizePixelX,
                Color.White);
        }
        public override void OnKeyboardDownCode(object sender, int key)
        {
            //base.OnKeyboardDownCode(key);
            game.DrawText(key + " down", new Point(), Color.White);
            //if (key == 200)
            //{
            //    ((System.Windows.Forms.Panel)game.Devices.D3DDev..DeviceWindow.Parent).VerticalScroll.Value += 16;
            //}
        }
        public override void OnKeyboardDownHoldCode(object sender, int key)
        {
            //base.OnKeyboardDownHoldCode(this,key);
            game.DrawText(key + " downhold", new Point(0,20), Color.White);
        }
        public override void OnKeyboardUpCode(object sender, int key)
        {
            //base.OnKeyboardUpCode(key);
            game.DrawText(key + " up", new Point(0,40), Color.White);
        }
        void draw_grid()
        {
            for (int i = 0; i <= scene.SizePixel.Width; i += scene.TileSizePixel.Width)
            {
                game.DrawLine(i, 0, i, scene.SizePixel.Height, Color.Blue);
            }
            for (int i = 0; i <= scene.SizePixel.Height; i += scene.TileSizePixel.Height)
            {
                game.DrawLine(0, i, scene.SizePixel.Width, i, Color.Blue);
            }
        }
        void draw_code()
        {
            Point o = new Point(8, 8);
            if (drawCodeLayer == 0)
            {
                foreach (Code c in scene.CodeLayer.Codes)
                {
                    string str = "o";
                    if (!c.CHRCanRch)
                    {
                        str = "x";
                    }
                    game.DrawText(str, Util.PointAddPoint(o, Util.PointMulInt(c.Location, scene.TilePixel)), Color.White);
                }
            }
            else if (drawCodeLayer == 1)
            {
                foreach (Code c in scene.CodeLayer.Codes)
                {
                    string str = "o";
                    if (!c.MTLCanRch)
                    {
                        str = "x";
                    }
                    game.DrawText(str, Util.PointAddPoint(o, Util.PointMulInt(c.Location, scene.TilePixel)), Color.White);
                }
            }
            else if (drawCodeLayer == 2)
            {
                foreach (Code c in scene.CodeLayer.Codes)
                {
                    string str = "o";
                    if (!c.SHPCanRch)
                    {
                        str = "x";
                    }
                    game.DrawText(str, Util.PointAddPoint(o, Util.PointMulInt(c.Location, scene.TilePixel)), Color.White);
                }
            }
            else if (drawCodeLayer == 3)
            {
                foreach (Code c in scene.CodeLayer.Codes)
                {
                    string str = "o";
                    if (!c.FLTCanRch)
                    {
                        str = "x";
                    }
                    game.DrawText(str, Util.PointAddPoint(o, Util.PointMulInt(c.Location, scene.TilePixel)), Color.White);
                }
            }
            else if (drawCodeLayer == 4)
            {
                foreach (Code c in scene.CodeLayer.Codes)
                {
                    string str = c.DrawLayer.ToString();
                    game.DrawText(str, Util.PointAddPoint(o, Util.PointMulInt(c.Location, scene.TilePixel)), Color.White);
                }
            }
            else if (drawCodeLayer == 5)
            {
                foreach (Code c in scene.CodeLayer.Codes)
                {
                    string str = c.RchDisappear.ToString();
                    game.DrawText(str, Util.PointAddPoint(o, Util.PointMulInt(c.Location, scene.TilePixel)), Color.White);
                }
            }
            //for (int i = 0; i <= scene.SizePixel.Height; i += scene.TileSizePixel.Height)
            //{
            //    for (int j = 0; j <= scene.SizePixel.Width; j += scene.TileSizePixel.Width)
            //    {
            //        game.DrawText("1", new Point(j, i), Color.Black);
            //    }
            //}
        }
        void draw_pen()
        {
            game.DrawMetalXTexture(
                game.Textures[mxtIndex],
                penRect, 
                penLoc,
                penRectPixel.Size, 
                Color.FromArgb(100, Color.White));
        }
        void draw_link(Point p)
        {
            p = Util.PointMulInt(p, scene.TilePixel);
            game.DrawRect(new Rectangle(p, scene.TileSizePixel), Color.Green);
        }
    }
}
