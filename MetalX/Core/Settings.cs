using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX
{
    public class Settings
    {
        float x;
        Size tileSize;
        string rootPath;
        Size windowSize;

        public float X
        {
            set
            {
                x = value;
            }
            get
            {
                return x;
            }
        }
        public Size TileSize
        {
            get
            {
                return tileSize;
            }
            set
            {
                tileSize = value;
            }
        }
        public Size TileSizeX
        {
            get
            {
                return new Size((int)(tileSize.Width * x), (int)(tileSize.Height * x));
            }
        }
        public string RootPath
        {
            get
            {
                return rootPath;
            }
        }
        public Size WindowSize
        {
            get
            {
                return windowSize;
            }
        }

        public Settings()
        {
            x = 1f;
            rootPath = @".\";
            windowSize = new Size(800, 600);
            tileSize = new Size(16, 16);
        }
    }
}
