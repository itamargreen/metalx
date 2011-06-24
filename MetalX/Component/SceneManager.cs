using System;
using System.Collections.Generic;
using System.Drawing;

using MetalX.Data;

namespace MetalX.Component
{
    public class SceneManager : GameCom
    {
        int sceneIndex = -1;
        Scene scene
        {
            get
            {
                if (sceneIndex < 0)
                {
                    return null;
                }
                return game.Scenes[sceneIndex];
            }
        }
        Point sceneLocation = new Point();
        int frameIndex = 0;
        DateTime lastFrameBeginTime = DateTime.Now;

        public SceneManager(Game g)
            : base(g)
        {
        }

        public override void Code()
        {
            base.Code();
        }

        public override void Draw()
        {
            base.Draw();
            DrawTerrain(scene);
        }

        public void LoadScene(int i)
        {
            sceneIndex = i;
        }

        bool IsInWindow(Point pos)
        {
            Point p = Util.PointAddPoint(pos, sceneLocation);
            if (p.X < 0 - 1 || p.Y < 0 - 1 || p.X > game.Options.WindowSize.Width / game.Options.TileSizeX.Width + 1 || p.Y > game.Options.WindowSize.Height / game.Options.TileSizeX.Height + 1)
            {
                return false;
            }
            return true;
        }

        void DrawTerrain(Scene s)
        {
            if (s == null)
            {
                return;
            }
            foreach (TileLayer tl in s.TileLayers)
            {
                foreach (Tile t in tl.Tiles)
                {
                    if (IsInWindow(t.Location))
                    {
                        game.DrawMetalXTexture(
                            game.Textures[t[frameIndex].TextureIndex],
                            t[frameIndex].DrawZone,
                            Util.PointAddPoint(t.Location, base.GlobalOffset),
                            s.TileSizePixel,
                            Util.MixColor(t[frameIndex].ColorFilter, ColorFilter)
                        );
                    }
                }
            }
        }
    }
}
