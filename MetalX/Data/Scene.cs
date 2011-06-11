using System;
using System.Collections.Generic;
using System.Drawing;

namespace MetalX.Data
{
    [Serializable]
    public class Scene
    {
        /// <summary>
        /// 场景索引
        /// </summary>
        //public int Index;
        /// <summary>
        /// 场景名
        /// </summary>
        public string Name;
        ///// <summary>
        ///// 路径
        ///// </summary>
        //public string Path;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime;
        /// <summary>
        /// 版本
        /// </summary>
        public string Version;
        /// <summary>
        /// 帧间隔
        /// </summary>
        public int FrameInterval = 500;
        /// <summary>
        /// 场景尺寸
        /// </summary>
        public Size Size;
        /// <summary>
        /// 图元物理尺寸
        /// </summary>
        public Size TileSizePixel;
        /// <summary>
        /// 场景物理尺寸
        /// </summary>
        public Size SizePixel
        {
            get
            {
                return new Size(TileSizePixel.Width * Size.Width, TileSizePixel.Height * Size.Height);
            }
        }
        /// <summary>
        /// 场景位置
        /// </summary>
        //[NonSerialized]
        //public Location Location;

        /// <summary>
        /// 图元层
        /// </summary>
        public List<TileLayer> TileLayers = new List<TileLayer>();
        /// <summary>
        /// 代码层
        /// </summary>
        public List<CodeLayer> CodeLayers = new List<CodeLayer>();

        public Scene()
        { 
        }
        public Scene(Size s,Size tsp)
        {
            TileSizePixel = tsp;
            CodeLayers.Add(new CodeLayer());
            Size = s;
            int i=0;
            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    CodeLayers[0].Codes.Add(new Code());
                    CodeLayers[0].Codes[i++].Location = new Point(x * TileSizePixel.Width, y * TileSizePixel.Height);
                }
            }
        }

        //public Scene GetClone()
        //{
        //    return (Scene)MemberwiseClone();
        //}
    }
    [Serializable]
    public class TileLayer
    {
        /// <summary>
        /// 图元集合
        /// </summary>
        public List<Tile> Tiles = new List<Tile>();
        /// <summary>
        /// 可见行
        /// </summary>
        public bool Visible = true;
        public Tile this[int i]
        {
            get
            {
                return Tiles[i];
            }
        }
        public Tile this[Point p]
        {
            get
            {
                foreach (Tile t in Tiles)
                {
                    if (t.Location == p)
                    {
                        return t;
                    }
                }
                return null;
            }
        }
        //public void AddTile(Tile t)
        //{
        //    foreach (Tile tile in Tiles)
        //    {
        //        if (tile.Location == t.Location)
        //        {
        //            return;
        //        }
        //    }
        //    Tiles.Add(t);
        //}
        //public void DelTile(Tile t)
        //{
        //    Tiles.Remove(t);
        //}
        //public void DelTile(int i)
        //{
        //    Tiles.RemoveAt(i);
        //}
    }
    [Serializable]
    public class Tile
    {
        /// <summary>
        /// 位置
        /// </summary>
        public Point Location;
        /// <summary>
        /// 帧索引
        /// </summary>
        public int FrameIndex = 0;
        /// <summary>
        /// 帧间隔
        /// </summary>
        public int FrameInterval = 500;
        /// <summary>
        /// 帧集合
        /// </summary>
        public List<TileFrame> Frames = new List<TileFrame>();
        //public Rectangle DrawZone;
        public TileFrame this[int i]
        {
            get
            {
                i = i % Frames.Count;
                return Frames[i];
            }
        }
        /// <summary>
        /// 帧数
        /// </summary>
        public int FrameCount
        {
            get
            {
                return Frames.Count;
            }
        }
        ///// <summary>
        ///// 添加帧
        ///// </summary>
        ///// <param name="tf">帧</param>
        //public void AddFrame(TileFrame tf)
        //{
        //    if(Frames.Contains(tf))
        //    {
        //        return;
        //    }
        //    Frames.Add(tf);
        //}
        ///// <summary>
        ///// 删除帧
        ///// </summary>
        ///// <param name="tf">帧</param>
        //public void DelFrame(TileFrame tf)
        //{
        //    Frames.Remove(tf);
        //}
        ///// <summary>
        ///// 删除帧
        ///// </summary>
        ///// <param name="i">帧索引</param>
        //public void DelFrame(int i)
        //{
        //    Frames.RemoveAt(i);
        //}
    }
    [Serializable]
    public class TileFrame
    {
        /// <summary>
        /// 纹理索引
        /// </summary>
        //public int TextureIndex = -1;
        /// <summary>
        /// 纹理文件名
        /// </summary>
        public string TextureFileName;
        public int TextureIndex = -1;
        /// <summary>
        /// 纹理物理尺寸
        /// </summary>
        //Size TextureSize;
        /// <summary>
        /// 绘制物理区域
        /// </summary>
        public Rectangle DrawZone;        
        /// <summary>
        /// 颜色滤镜
        /// </summary>
        public Color ColorFilter = Color.FromArgb(255, Color.White);
    }
    [Serializable]
    public class CodeLayer
    {
        public List<Code> Codes = new List<Code>();
        public Code this[int x, int y]
        {
            get
            {
                for (int i = 0; i < Codes.Count; i++)
                {
                    if (Codes[i].Location == new Point(x, y))
                    {
                        return Codes[i];
                    }
                }
                return null;
            }
            set
            {
                for (int i = 0; i < Codes.Count; i++)
                {
                    if (Codes[i].Location == new Point(x, y))
                    {
                        Codes[i] = value;
                    }
                }
            }
        }
        public Code this[Point p]
        {
            get
            {
                for (int i = 0; i < Codes.Count; i++)
                {
                    if (Codes[i].Location == p)
                    {
                        return Codes[i];
                    }
                }
                return null;
            }
            set
            {
                for (int i = 0; i < Codes.Count; i++)
                {
                    if (Codes[i].Location == p)
                    {
                        Codes[i] = value;
                    }
                }
            }
        }
        public Code this[int i]
        {
            get
            {
                return Codes[i];
            }
        }
    }
    [Serializable]
    public class Code
    {
        /// <summary>
        /// 人物可到达
        /// </summary>
        public bool CHRCanRch = true;
        /// <summary>
        /// 乘船时可到达
        /// </summary>
        public bool SHPCanRch = true;
        /// <summary>
        /// 乘车时可到达
        /// </summary>
        public bool MTLCanRch = true;
        /// <summary>
        /// 乘飞机时可到达
        /// </summary>
        public bool FLTCanRch = true;
        /// <summary>
        /// 精灵绘制所在层
        /// </summary>
        public int DrawLayer = 0;
        /// <summary>
        /// 可上移
        /// </summary>
        public bool UCanMov = true;
        /// <summary>
        /// 可左移
        /// </summary>
        public bool LCanMov = true;
        /// <summary>
        /// 可下移
        /// </summary>
        public bool DCanMov = true;
        /// <summary>
        /// 可右移
        /// </summary>
        public bool RCanMov = true;
        /// <summary>
        /// 目标场景文件名
        /// </summary>
        public string SceneFileName;
        /// <summary>
        /// 默认逻辑位置
        /// </summary>
        public Point DefaultLocation;
        /// <summary>
        /// 默认朝向
        /// </summary>
        public Direction DefaultDirection;
        /// <summary>
        /// 位置
        /// </summary>
        public Point Location;
    }    
    [Serializable]
    public enum Direction
    {
        /// <summary>
        /// 上
        /// </summary>
        U = 0,
        /// <summary>
        /// 下
        /// </summary>
        L = 1,
        /// <summary>
        /// 左
        /// </summary>
        D = 2,
        /// <summary>
        /// 右
        /// </summary>
        R = 3,
    }
    //[Serializable]
    //public class Linker
    //{

    //    /// <summary>
    //    /// 触发逻辑区域
    //    /// </summary>
    //    public Rectangle Zone;
    //}
    //[Serializable]
    //public class Location
    //{
    //    /// <summary>
    //    /// 物理位置
    //    /// </summary>
    //    public Point Pixel;
    //    /// <summary>
    //    /// 逻辑位置
    //    /// </summary>
    //    public Point Logic;
    //    public Location()
    //    {
    //        Pixel = new Point();
    //        Logic = new Point();
    //    }
    //    public Location(Point pixel, Size unit)
    //    {
    //        Pixel = pixel;
    //        Logic = new Point(pixel.X / unit.Width, pixel.Y / unit.Height);
    //    }
    //    public Location(Point logic, Point physic)
    //    {
    //        Pixel = physic;
    //        Logic = logic;
    //    }
    //    public static Location operator +(Location pos1, Location pos2)
    //    {
    //        return new Location(new Point(pos1.Logic.X + pos2.Logic.X, pos1.Logic.Y + pos2.Logic.Y), new Point(pos1.Pixel.X + pos2.Pixel.X, pos1.Pixel.Y + pos2.Pixel.Y));
    //    }
    //    public static Location operator -(Location pos1, Location pos2)
    //    {
    //        return new Location(new Point(pos1.Logic.X - pos2.Logic.X, pos1.Logic.Y - pos2.Logic.Y), new Point(pos1.Pixel.X - pos2.Pixel.X, pos1.Pixel.Y - pos2.Pixel.Y));
    //    }
    //    public static Location operator *(Location pos1, float k)
    //    {
    //        float lx, ly, px, py;
    //        lx = pos1.Logic.X * k;
    //        ly = pos1.Logic.Y * k;
    //        px = pos1.Pixel.X * k;
    //        py = pos1.Pixel.Y * k;
    //        return new Location(new Point((int)lx, (int)ly), new Point((int)px, (int)py));
    //    }
    //    public static Location operator /(Location pos1, float k)
    //    {
    //        float lx, ly, px, py;
    //        lx = pos1.Logic.X / k;
    //        ly = pos1.Logic.Y / k;
    //        px = pos1.Pixel.X / k;
    //        py = pos1.Pixel.Y / k;
    //        return new Location(new Point((int)lx, (int)ly), new Point((int)px, (int)py));
    //    }
    //    public static bool operator ==(Location pos1, Location pos2)
    //    {
    //        if (pos1.Logic == pos2.Logic && pos1.Pixel == pos2.Pixel)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    public static bool operator !=(Location pos1, Location pos2)
    //    {
    //        if (pos1.Logic != pos2.Logic || pos1.Pixel != pos2.Pixel)
    //        {
    //            return true;
    //        }
    //        return false;
    //    }
    //    public override int GetHashCode()
    //    {
    //        return base.GetHashCode();
    //    }
    //    public override bool Equals(object obj)
    //    {
    //        return base.Equals(obj);
    //    }
    //    public Location GetClone()
    //    {
    //        return (Location)MemberwiseClone();
    //    }
    //}

}

