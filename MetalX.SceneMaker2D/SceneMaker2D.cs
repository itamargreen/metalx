using System;
using System.Collections.Generic;
using System.Drawing;

using MetalX;
namespace MetalX.SceneMaker2D
{
    public class SceneMaker2D : MetalXGameCom
    {
        bool drawGrid;
        Scene scene;
        int frameIndex;

        public SceneMaker2D(MetalXGame metalx)
            : base(metalx)
        {
            drawGrid = false;
            frameIndex = 0;
        }

        public override void Code()
        {
            base.Code();
        }

        public override void Draw()
        {
            base.Draw();
            if (drawGrid)
            { 
                
            }
            foreach (TileLayer tl in scene.TileLayers)
            {
                if (tl.Visible)
                {
                    foreach (Tile t in tl.Tiles)
                    {
                        metalXGame.DrawMetalXTexture(metalXGame.Textures[t.Frames[frameIndex].TextureIndex], t.Location, t.Frames[frameIndex].ColorFilter);
                    }
                }
            }
            //metalXGame.DrawText(" 场景名:" + scene.Name + " 尺寸:" + scene.Size + " 图元尺寸:" + scene.TileSize, new Point(), Color.White);
        }

        public void LoadScene(Scene s)
        {
            this.scene = s;
        }
    }
}
