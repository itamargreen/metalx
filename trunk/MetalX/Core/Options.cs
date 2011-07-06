using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX
{
    public class Options
    {
        public TextureDrawMode TextureDrawMode = TextureDrawMode.Direct3D;
        public bool FullScreen = false;
        public float X = 1f;
        public string RootPath = @".\";
        public Size WindowSizePixel = new Size(640, 480);
        public Size TileSizePixel = new Size(48, 48);
        public int TilePixel
        {
            get { return TileSizePixelX.Width; }
        }
        public Size TileSizePixelX
        {
            get
            {
                return new Size((int)(TileSizePixel.Width * X), (int)(TileSizePixel.Height * X));
            }
        }
    }
}
