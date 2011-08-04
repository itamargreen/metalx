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
        public int FrameCount;
        public Size TileSizePixel;
        public double FrameInterval;
        public bool Loop = false;
        //public List<MovieFrameInfo> MovieFrameInfos = new List<MovieFrameInfo>();
        public int MovieTime
        {
            get
            {
                return (int)FrameInterval* FrameCount;
            }
        }
        /// <summary>
        /// 垂直的
        /// </summary>
        public bool Vertical = true;

        int frameIndex;
        public int FrameIndex
        {
            get
            {
                return frameIndex;
            }
        }
        public void NextFrame()
        {
            if (Loop == false)
            {
                if (frameIndex + 1 < FrameCount)
                {
                    frameIndex++;
                }
            }
            else
            {
                frameIndex++;
                if (frameIndex >= FrameCount)
                {
                    frameIndex = 0;
                }
            }
        }
        DateTime beginTime;
        public void Reset()
        {
            beginTime = DateTime.Now;
            frameIndex = 0;
        }

        public Vector3 DrawLocation
        {
            get
            {
                //TimeSpan ts = DateTime.Now - beginTime;
                Vector3 loc = new Vector3();
                //Vector3 sloc = new Vector3();
                //Vector3 eloc = new Vector3();
                //int st = 0, et = 0;
                //int tr = 1;
                //for (int i = 0; i < MovieFrameInfos.Count; i++)
                //{
                //    if (MovieFrameInfos[i].TimePoint > ts.TotalMilliseconds)
                //    {
                //        i++;
                //        if (i < MovieFrameInfos.Count)
                //        {
                //            if (MovieFrameInfos[i].TimePoint < ts.TotalMilliseconds)
                //            {
                //                sloc = MovieFrameInfos[i].Location;
                //                st = MovieFrameInfos[i].TimePoint;
                //                eloc = MovieFrameInfos[i].Location;
                //                et = MovieFrameInfos[i].TimePoint;
                //                tr = et - st;
                //                float xr = eloc.X - sloc.X;
                //                float yr = eloc.Y - sloc.Y;
                //                float zr = eloc.Z - sloc.Z;

                //                int ct = (int)ts.TotalMilliseconds - st;

                //                float cx = ct * xr / tr;
                //                float cy = ct * yr / tr;
                //                float cz = ct * zr / tr;

                //                loc = Util.Vector3AddVector3(sloc, new Vector3(cx, cy, cz));
                //                break;
                //            }
                //        }
                //    }
                //}

                
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

                #region for size
                rect.Size = TileSizePixel;

                if (Vertical)
                {
                    rect.Y = frameIndex * TileSizePixel.Height;
                }
                else
                {
                    rect.Y = frameIndex * TileSizePixel.Height;

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
