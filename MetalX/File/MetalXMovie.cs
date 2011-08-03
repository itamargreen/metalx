using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.DirectX;
namespace MetalX.File
{
    [Serializable]
    public class MetalXMovie : IDisposable
    {
        public MetalXTexture MXT = new MetalXTexture();
        public int FrameCount;
        public Size TileSizePixel;
        public double FrameInterval;
        public bool Loop = false;
        public List<Vector3> Locations = new List<Vector3>();
        /// <summary>
        /// 垂直的
        /// </summary>
        public bool Vertical = true;

        int FrameIndex;
        public void NextFrame()
        {
            if (Loop == false)
            {
                if (FrameIndex + 1 < FrameCount)
                {
                    FrameIndex++;
                }
            }
            else
            {
                FrameIndex++;
                if (FrameIndex >= FrameCount)
                {
                    FrameIndex = 0;
                }
            }
        }
        public void Reset()
        {
            FrameIndex = 0;
        }

        public Rectangle DrawZone
        {
            get
            {
                Rectangle rect = new Rectangle();

                #region for size
                rect.Size = TileSizePixel;

                if (Vertical)
                {
                    rect.Y = FrameIndex * TileSizePixel.Height;
                }
                else
                {
                    rect.Y = FrameIndex * TileSizePixel.Height;

                }
                #endregion
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
