using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.File
{
    [Serializable]
    public class MetalXMovie : IDisposable
    {
        public MetalXTexture MXT = new MetalXTexture();
        public int FrameCount;
        public Size TileSizePixel;
        public double FrameInterval;
        /// <summary>
        /// 垂直的
        /// </summary>
        public bool Vertical = true;

        public int FrameIndex;
        public void NextFrame()
        {
            FrameIndex++;
            if (FrameIndex >= FrameCount)
            {
                FrameIndex = 0;
            }
        }

        public Rectangle DrawZone
        {
            get
            {
                Rectangle rect = new Rectangle();
                rect.Size = TileSizePixel;

                if (Vertical)
                {
                    rect.Y = FrameIndex * TileSizePixel.Height;
                }
                else
                {
                    rect.Y = FrameIndex * TileSizePixel.Height;

                }
                return rect;
            }
        }
        public void Dispose()
        {
            MXT.Dispose();
        }
        public MetalXMovie()
        {
 
        }
    }
}
