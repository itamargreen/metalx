using System;
using System.Collections.Generic;
using System.Drawing;

using MetalX;
using MetalX.Data;
using MetalX.Component;

namespace MetalX.SceneMaker2D
{
    public class SceneMaker2D : GameCom
    {
        public bool drawGrid;
        public bool drawCode;
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
            base.Draw();
            foreach (TileLayer tl in scene.TileLayers)
            {
                if (tl.Visible)
                {
                    foreach (Tile t in tl.Tiles)
                    {
                        game.DrawMetalXTexture(
                            game.Textures[t[frameIndex].TextureIndex],
                            t[frameIndex].DrawZone,
                            Util.PointAddPoint(t.LocationPoint, Util.Vector32Point( ScreenOffsetPixel)),
                            scene.TileSizePixel,
                            Util.MixColor(t[frameIndex].ColorFilter, ColorFilter)
                            );
                    }
                }
            }
            foreach (CodeLayer cl in scene.CodeLayers)
            {
                foreach (Code c in cl.Codes)
                {
                    if (c.SceneFileName != null)
                    {
                        draw_link(c.Location);
                    }
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

            game.DrawText(" FPS: " + game.AverageFPS.ToString("f1"), new Point(), Color.White);
        }
        public override void OnKeyboardDownCode(int key)
        {
            base.OnKeyboardDownCode(key);
            game.DrawText(key + " down", new Point(), Color.White);
            //if (key == 200)
            //{
            //    ((System.Windows.Forms.Panel)game.Devices.D3DDev..DeviceWindow.Parent).VerticalScroll.Value += 16;
            //}
        }
        public override void OnKeyboardDownHoldCode(int key)
        {
            base.OnKeyboardDownHoldCode(key);
            game.DrawText(key + " downhold", new Point(0,20), Color.White);
        }
        public override void OnKeyboardUpCode(int key)
        {
            base.OnKeyboardUpCode(key);
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
            if (drawCodeLayer == 0)
            {
                foreach (Code c in scene.CodeLayers[0].Codes)
                {
                    string str = "o";
                    if (!c.CHRCanRch)
                    {
                        str = "x";
                    }
                    game.DrawText(str, c.Location, Color.Red);
                }
            }
            else if (drawCodeLayer == 1)
            {
                foreach (Code c in scene.CodeLayers[0].Codes)
                {
                    string str = "o";
                    if (!c.MTLCanRch)
                    {
                        str = "x";
                    }
                    game.DrawText(str, c.Location, Color.Red);
                }
            }
            else if (drawCodeLayer == 2)
            {
                foreach (Code c in scene.CodeLayers[0].Codes)
                {
                    string str = "o";
                    if (!c.SHPCanRch)
                    {
                        str = "x";
                    }
                    game.DrawText(str, c.Location, Color.Red);
                }
            }
            else if (drawCodeLayer == 3)
            {
                foreach (Code c in scene.CodeLayers[0].Codes)
                {
                    string str = "o";
                    if (!c.FLTCanRch)
                    {
                        str = "x";
                    }
                    game.DrawText(str, c.Location, Color.Red);
                }
            }
            else if (drawCodeLayer == 4)
            {
                foreach (Code c in scene.CodeLayers[0].Codes)
                {
                    string str = c.DrawLayer.ToString();
                    game.DrawText(str, c.Location, Color.Red);
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
            game.DrawRect(new Rectangle(p, scene.TileSizePixel), Color.Green);
        }
    }
}
