using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX.DirectInput;

namespace MetalX
{
    public class Options
    {
        public Key KeyUP = Key.Up;
        public Key KeyDOWN = Key.Down;
        public Key KeyLEFT = Key.Left;
        public Key KeyRIGHT = Key.Right;
        public Key KeyYES = Key.Space;
        public Key KeyNO = Key.Escape;

        public TextureDrawMode TextureDrawMode = TextureDrawMode.Direct2D;
        public bool FullScreen = false;
        public float X = 1f;
        public string RootName = "data";
        public string RootPath
        {
            get
            {
                return @".\" + RootName + @"\";
            }
        }
        public Size WindowSize
        {
            get
            {
                return new Size(WindowSizePixel.Width / TileSizePixelX.Width, WindowSizePixel.Height / TileSizePixelX.Height);
            }

        }
        public Size WindowSizePixel = new Size(640, 480);
        public Size TileSizePixel = new Size(48, 48);
        public float UVOffsetX = 0.5f;
        public float UVOffsetY = 0.5f;
        public string ServerIP = "127.0.0.1";
        public int ServerPort = 8415;
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
        public int SpriteOffsetPixel
        {
            get
            {
                return 0;
                //return -TilePixel / 3;
            }
        }
    }
}
