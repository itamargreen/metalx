using System;
using System.Collections.Generic;
using System.Drawing;

using MetalX;
using MetalX.Format;
using MetalX.Data;
using MetalX.Framework;

namespace MetalX.SceneMaker2D
{
    public class SceneMaker2D : MetalXGameCom
    {
        public bool drawGrid;
        public bool drawCode;
        public int drawCodeLayer=0;
        public Rectangle dragRect;
        public string mxtName;
        public int mxtIndex;
        public Point penLoc;
        public Rectangle penRect;
        public Rectangle penRectPixel
        {
            get
            {
                if (mxtName == null)
                {
                    return new Rectangle(penRect.Location, scene.TileSizePixel);
                }
                float wx = penRect.Width / metalXGame.Textures[mxtName].TileSizePixel.Width;
                float hx = penRect.Height / metalXGame.Textures[mxtName].TileSizePixel.Height;
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

        public Scene scene;

        public SceneMaker2D(MetalXGame metalx)
            : base(metalx)
        {
            drawGrid = false;
            frameIndex = 0;
            //scene = new Scene();
        }

        public override void Code()
        {
            base.Code();
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
                        metalXGame.DrawMetalXTexture(
                            metalXGame.Textures[t.Frames[frameIndex].TextureIndex],  
                            t.Frames[frameIndex].DrawZone,
                            t.Location, 
                            scene.TileSizePixel,
                            t.Frames[frameIndex].ColorFilter);
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
            draw_pen();        
            if (drawGrid||drawCode)
            {
                draw_grid();
            }
            if (drawCode)
            {
                draw_code();
            }
            metalXGame.DrawRect(dragRect, Color.Red);

            //metalXGame.DrawLine(100, 0, 100, 384, Color.White);
            //metalXGame.DrawText(" 场景名:" + scene.Name + " 尺寸:" + scene.Size + " 图元尺寸:" + scene.TileSize, new Point(), Color.White);
        }
        void draw_grid()
        {
            for (int i = 0; i <= scene.SizePixel.Width; i += scene.TileSizePixel.Width)
            {
                metalXGame.DrawLine(i, 0, i, scene.SizePixel.Height, Color.Blue);
            }
            for (int i = 0; i <= scene.SizePixel.Height; i += scene.TileSizePixel.Height)
            {
                metalXGame.DrawLine(0, i, scene.SizePixel.Width, i, Color.Blue);
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
                    metalXGame.DrawText(str, c.Location, Color.Red);
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
                    metalXGame.DrawText(str, c.Location, Color.Red);
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
                    metalXGame.DrawText(str, c.Location, Color.Red);
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
                    metalXGame.DrawText(str, c.Location, Color.Red);
                }
            }
            else if (drawCodeLayer == 4)
            {
                foreach (Code c in scene.CodeLayers[0].Codes)
                {
                    string str = c.DrawLayer.ToString();
                    metalXGame.DrawText(str, c.Location, Color.Red);
                }
            }
            //for (int i = 0; i <= scene.SizePixel.Height; i += scene.TileSizePixel.Height)
            //{
            //    for (int j = 0; j <= scene.SizePixel.Width; j += scene.TileSizePixel.Width)
            //    {
            //        metalXGame.DrawText("1", new Point(j, i), Color.Black);
            //    }
            //}
        }
        void draw_pen()
        {
            metalXGame.DrawMetalXTexture(
                metalXGame.Textures[mxtName],
                penRect, 
                penLoc,
                penRectPixel.Size, 
                Color.FromArgb(100, Color.White));
        }
        void draw_link(Point p)
        {
            metalXGame.DrawRect(new Rectangle(p, scene.TileSizePixel), Color.Green);
        }
    }
}
