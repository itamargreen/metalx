using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX
{
    public class Options
    {
        public TextureDrawMode TextureDrawMode = TextureDrawMode.Direct2D;
        public float X = 1f;
        public string RootPath = @".\";
        public Size WindowSize = new Size(640, 480);
        public Size TileSize = new Size(32, 32);

        public Size TileSizeX
        {
            get
            {
                return new Size((int)(TileSize.Width * X), (int)(TileSize.Height * X));
            }
        }
    }
}
