using System;
using System.Collections.Generic;
using System.Drawing;

using MetalX;
namespace MetalX.SceneMaker2D
{
    public class SceneMaker2D : MetalXGameCom
    {
        public bool drawGrid;
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
            scene = new Scene();
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
                            metalXGame.Textures[t.Frames[frameIndex].TextureFileName],  
                            t.Frames[frameIndex].DrawZone,
                            t.Location, 
                            scene.TileSizePixel,
                            t.Frames[frameIndex].ColorFilter);
                    }
                }
            }
            draw_pen();        
            if (drawGrid)
            {
                draw_grid();
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
        void draw_pen()
        {
            metalXGame.DrawMetalXTexture(
                metalXGame.Textures[mxtName],
                penRect, 
                penLoc,
                penRectPixel.Size, 
                Color.FromArgb(100, Color.White));
        }
    }
}
