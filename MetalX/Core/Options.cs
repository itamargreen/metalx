using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX
{
    public class Options
    {
        public TextureDrawMode TextureDrawMode = TextureDrawMode.Direct3D;
        public float X = 1f;
        public Size TileSize = new Size(48, 48);
        public string RootPath = @".\";
        public Size WindowSize = new Size(800, 600);

        public Size TileSizeX
        {
            get
            {
                return new Size((int)(TileSize.Width * X), (int)(TileSize.Height * X));
            }
        }

        public Options()
        {
        }
    }
}
