using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.DirectX;
using MetalX.Define;
namespace MetalX.File
{
    [Serializable]
    public class MetalXMovie : IDisposable
    {
        public MetalXTexture MXT = new MetalXTexture();
        public Color ColorFilter = Color.White;
        public int FrameCount;
        public Size TileSize;
        public Size TileSize2X
        {
            get
            {
                return new Size(TileSize.Width * 2, TileSize.Height * 2);
            }
        }
        public double FrameInterval;
        public bool Loop = false;
        public MemoryIndexer BGSound = new MemoryIndexer();
        //public bool Finish
        //{
        //    get
        //    {
 
        //    }
        //}

        public double MovieTime
        {
            get
            {
                return FrameInterval* FrameCount;
            }
        }
        /// <summary>
        /// 垂直的
        /// </summary>
        public bool Vertical = true;

        int frameIndex = 0;
        //public int FrameIndex
        //{
        //    get
        //    {
        //        return frameIndex;
        //    }
        //}
        public bool NextFrame()
        {
            if (FrameTimeSpan > FrameInterval)
            {
                LastFrameTime = DateTime.Now;
                frameIndex++;

                if (frameIndex >= FrameCount)
                {
                    if (Loop == false)
                    {
                        frameIndex--;
                        return true;
                    }
                    else
                    {
                        frameIndex = 0;
                    }
                }
            }
            return false;
        }
        [NonSerialized]
        public DateTime BeginTime;
        public double WholeTimeSpan
        {
            get
            {
                return (DateTime.Now - BeginTime).TotalMilliseconds;
            }
        }
        [NonSerialized]
        public DateTime LastFrameTime;
        public double FrameTimeSpan
        {
            get
            {
                return (DateTime.Now - LastFrameTime).TotalMilliseconds;
            }
        }
        [NonSerialized]
        public Vector3 BeginLocation;
        [NonSerialized]
        public Vector3 EndLocation;
        public Vector3 Path
        {
            get
            {
                return Util.Vector3SubVector3(EndLocation, BeginLocation);
            }
        }
        [NonSerialized]
        public List<MovieFrameInfo> MovieFrameInfos = new List<MovieFrameInfo>();
        [NonSerialized]
        public double PlayTime;

        public void Reset()
        {
            LastFrameTime = BeginTime = DateTime.Now;
            frameIndex = 0;
        }
        public Vector3 DrawLocation
        {
            get
            {
                TimeSpan ts = DateTime.Now - BeginTime;

                double tl = ts.TotalMilliseconds;
                if (tl > PlayTime)
                {
                    tl = PlayTime;
                }

                Vector3 loc = Util.Vector3MulDouble(Path, tl);
                loc = Util.Vector3DivDouble(loc, PlayTime);
                loc = Util.Vector3AddVector3(loc, BeginLocation);
                return loc; 
            }
        }
        public Point DrawLocationPoint
        {
            get
            {
                return Util.Vector32Point(DrawLocation);
            }
        }
        public Rectangle DrawZone
        {
            get
            {
                Rectangle rect = new Rectangle();

                if (Vertical)
                {
                    rect.Y = frameIndex * TileSize.Height;
                }
                else
                {
                    rect.X = frameIndex * TileSize.Width;
                }

                rect.Size = TileSize;

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
