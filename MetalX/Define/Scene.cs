﻿using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;

namespace MetalX.Define
{
    [Serializable]
    public class Scene
    {
        public Tile BottomTile;
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
        public DateTime CreateTime = DateTime.Now;
        /// <summary>
        /// 版本
        /// </summary>
        public string Version;
        /// <summary>
        /// 帧间隔
        /// </summary>
        public int FrameInterval = 300;
        /// <summary>
        /// 场景尺寸
        /// </summary>
        public Size Size;
        /// <summary>
        /// 图元物理尺寸
        /// </summary>
        public Size TileSizePixel;
        public int TilePixelX
        {
            get
            {
                return TileSizePixel.Width;
            }
        }
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
        [NonSerialized]
        public Vector3 RealLocation=new Vector3();
        public Point RealLocationPoint
        {
            get
            {
                return new Point((int)RealLocation.X, (int)RealLocation.Y);
            }
        }
        [NonSerialized]
        public Vector3 RealLocationPixel = new Vector3();
        public Point RealLocationPixelPoint
        {
            get
            {
                return new Point((int)RealLocationPixel.X, (int)RealLocationPixel.Y);
            }
            set
            {
                RealLocationPixel.X = value.X;
                RealLocationPixel.Y = value.Y;
            }
        }
        public void SetRealLocation(Vector3 v3, int unit)
        {
            RealLocation = v3;
            RealLocationPixel = Util.Vector3MulInt(v3, unit);
        }
        //[NonSerialized]
        //public Location Location;

        /// <summary>
        /// 图元层
        /// </summary>
        public List<TileLayer> TileLayers = new List<TileLayer>();
        //public List<NPC> NPCs = new List<NPC>();
        //public NPC GetNPC(string name)
        //{
        //    for (int i = 0; i < NPCs.Count; i++)
        //    {
        //        if (NPCs[i].Name == name)
        //        {
        //            return NPCs[i];
        //        }
        //    }
        //    return null;
        //}
        //public NPC GetNPC(Vector3 v3)
        //{
        //    for (int i = 0; i < NPCs.Count; i++)
        //    {
        //        if (NPCs[i].IsDoor)
        //        {
        //            Vector3 v33 = NPCs[i].RealLocation;
        //            v33.Y++;
        //            if (v33 == v3)
        //            {
        //                return NPCs[i];
        //            }
        //        }
        //        //else if (NPCs[i].IsBox)
        //        //{ 
        //        //}
        //        else
        //        {
        //            if (NPCs[i].RealLocation == v3)
        //            {
        //                return NPCs[i];
        //            }
        //        }
        //    }
        //    return null;
        //}
        //public NPC GetNPC(Point p)
        //{
        //    return GetNPC(Util.Point2Vector3(p, 0));
        //}
        public List<string> BGMusicNames = new List<string>();
        public List<string> NPCNames = new List<string>();
        public List<MonsterZone> MonsterZones = new List<MonsterZone>();
        public int Fight(Vector3 loc)
        {
            if (MonsterZones == null)
            {
                return -1;
            }
            int i = 0;
            foreach (MonsterZone mz in MonsterZones)
            {
                if (mz.Fight(loc))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }
        /// <summary>
        /// 代码层
        /// </summary>
        public List<CodeLayer> CodeLayers = new List<CodeLayer>();
        public CodeLayer CodeLayer
        {
            get
            {
                return CodeLayers[0];
            }
        }
        [NonSerialized]
        public Tile[][][] Tiles;
        [NonSerialized]
        public Code[][] Codes;
        public Tile this[int layer, int y, int x]
        {
            get
            {
                return Tiles[layer][y][x];
            }
        }
        public Code this[int y, int x]
        {
            get
            {
                return Codes[y][x];
            }
        }
        public Code this[Vector3 loc]
        {
            get
            {
                return this[(int)loc.Y, (int)loc.X];
            }
        }
        void Init()
        {
            Tiles = new Tile[TileLayers.Count][][];
            for (int l = 0; l < TileLayers.Count; l++)
            {
                Tiles[l] = new Tile[Size.Height][];
                for (int y = 0; y < Size.Height; y++)
                {
                    Tiles[l][y] = new Tile[Size.Width];
                    for (int x = 0; x < Size.Width; x++)
                    {
                        Tiles[l][y][x] = TileLayers[l][x, y];
                    }
                }
            }
            Codes = new Code[Size.Height][];
            for (int y = 0; y < Size.Height; y++)
            {
                Codes[y] = new Code[Size.Width];
                for (int x = 0; x < Size.Width; x++)
                {
                    Codes[y][x] = CodeLayers[0][x, y];
                }
            }
        }
        public void Init(Size winSize)
        {
            if (BottomTile != null)
            {
                int w = winSize.Width / 2;
                int h = winSize.Height / 2;

                //for left
                for (int y = -h; y < Size.Height + h; y++)
                {
                    for (int x = -w; x < 0; x++)
                    {
                        Tile tile = BottomTile.GetClone();
                        tile.Location.X = x;
                        tile.Location.Y = y;
                        TileLayers[0].Tiles.Add(tile);
                    }
                }

                //for right
                for (int y = -h; y < Size.Height + h; y++)
                {
                    for (int x = Size.Width; x < Size.Width + w; x++)
                    {
                        Tile tile = BottomTile.GetClone();
                        tile.Location.X = x;
                        tile.Location.Y = y;
                        TileLayers[0].Tiles.Add(tile);
                    }
                }

                //for top
                for (int y = -h; y < 0; y++)
                {
                    for (int x = 0; x < Size.Width; x++)
                    {
                        Tile tile = BottomTile.GetClone();
                        tile.Location.X = x;
                        tile.Location.Y = y;
                        TileLayers[0].Tiles.Add(tile);
                    }
                }

                //for bottom
                for (int y = Size.Height; y < Size.Height + h; y++)
                {
                    for (int x = 0; x < Size.Width; x++)
                    {
                        Tile tile = BottomTile.GetClone();
                        tile.Location.X = x;
                        tile.Location.Y = y;
                        TileLayers[0].Tiles.Add(tile);
                    }
                }
            }
            Init();
        }
        public bool IsInScene(Vector3 p)
        {
            if (p.X < 0)
            {
                return false;
            }
            if (p.Y < 0)
            {
                return false;
            }
            if (p.X >= Size.Width)
            {
                return false;
            }
            if (p.Y >= Size.Height)
            {
                return false;
            }
            return true;
        }
        public Scene()
        {
        }
        public Scene(Size s, Size tsp)
        {
            TileSizePixel = tsp;
            CodeLayers.Add(new CodeLayer());
            Size = s;
            int i = 0;
            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    CodeLayer.Codes.Add(new Code());
                    CodeLayer.Codes[i++].Location = new Point(x, y);

                }
            }
        }
        [NonSerialized]
        double NeedMovePixel = 0;
        [NonSerialized]
        int NeedMoveStep = 0;
        [NonSerialized]
        Direction RealDirection;
        public void Face(Direction dir)
        {
            RealDirection = dir;
        }
        public void Move(int stp,int stepPixel)
        {
            NeedMoveStep += stp;
            if (RealDirection == Direction.U)
            {
                RealLocation.Y -= stp;
            }
            else if (RealDirection == Direction.L)
            {
                RealLocation.X -= stp;
            }
            else if (RealDirection == Direction.D)
            {
                RealLocation.Y += stp;
            }
            else if (RealDirection == Direction.R)
            {
                RealLocation.X += stp;
            }
            NeedMovePixel = stp * stepPixel;
        }
        public void MovePixel( double movespeed)
        {
            //if (NeedMovePixel == 0)
            //{
            //    if (NeedMoveStep > 0)
            //    {
            //        NeedMovePixel = NeedMoveStep * stepPixel;
            //        NeedMoveStep = 0;
            //    }
            //}
            if (NeedMovePixel > 0)
            {
                double movePixel = movespeed;
                if (NeedMovePixel < movespeed)
                {
                    movePixel = NeedMovePixel;
                }

                NeedMovePixel -= movespeed;

                if (RealDirection == Direction.U)
                {
                    RealLocationPixel.Y -= (float)movePixel;
                }
                else if (RealDirection == Direction.L)
                {
                    RealLocationPixel.X -= (float)movePixel;
                }
                else if (RealDirection == Direction.D)
                {
                    RealLocationPixel.Y += (float)movePixel;
                }
                else if (RealDirection == Direction.R)
                {
                    RealLocationPixel.X += (float)movePixel;
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
                return this[p.X, p.Y];
            }
        }
        public Tile this[Vector3 v]
        {
            get
            {
                return this[(int)v.X, (int)v.Y];
            }
        }
        public Tile this[int x, int y]
        {
            get
            {
                foreach (Tile t in Tiles)
                {
                    if (t.LocationPoint == new Point(x, y))
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
        public Vector3 Location;
        public Point LocationPoint
        {
            get
            {
                return new Point((int)Location.X, (int)Location.Y);
            }
        }
        public double Rotation = 0;
        //public Vector3 GetLocationPixel(int unit)
        //{
        //    Vector3 v3 = Location;
        //    v3.X *= unit;
        //    v3.Y *= unit;
        //    v3.Z *= unit;
        //    return v3;
        //}
        //public Point GetLocationPixelPoint(int unit)
        //{
        //    Vector3 v3 = GetLocationPixel(unit);
        //    return new Point((int)v3.X, (int)v3.Y);
        //}
        /// <summary>
        /// 帧索引
        /// </summary>
        public bool IsAnimation = false;
        //public int FrameIndex = 0;
        /// <summary>
        /// 帧间隔
        /// </summary>
        //public int FrameInterval = 500;
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
        public Tile GetClone()
        {
            return (Tile)MemberwiseClone();
        }
        /// <summary>
        /// 帧数
        /// </summary>
        //public int FrameCount
        //{
        //    get
        //    {
        //        return Frames.Count;
        //    }
        //}
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
        [NonSerialized]
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
        public double Rotation = 0;
        public Color ColorFilter = Color.FromArgb(255, Color.White);
    }
    [Serializable]
    public class CodeLayer
    {
        public List<Code> Codes = new List<Code>();
        public Code this[Vector3 v3]
        {
            get
            {
                return this[(int)v3.X, (int)v3.Y];
            }           
            set
            {
                this[(int)v3.X, (int)v3.Y] = value;
            }
        }
        public Code this[Point p]
        {
            get
            {
                return this[p.X, p.Y];
            }
            set
            {
                this[p.X, p.Y] = value;
            }
        }
        public Code this[int x, int y]
        {
            get
            {
                for (int i = 0; i < Codes.Count; i++)
                {
                    if (Codes[i].Location == new Point(x, y))
                    {
                        return this[i];
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
                        this[i] = value;
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
            set
            {
                Codes[i] = value;
            }

        }
    }
    [Serializable]
    public class Code
    {
        public bool IsDesk = false;
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
        public int DrawLayer = 3;
        public int RchDisappear = -1;
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
        public string Script = "";
        public bool ChangeSceneEffect = true;
    }
    [Serializable]
    public class MonsterZone
    {
        public string BGTextureName;
        public string BGMusicName;
        public string EnterSoundName;
        public Rectangle ZoneInfo = new Rectangle();
        public List<string> MonsterNames = new List<string>();
        public int FightChance = 0;
        bool IsIn(Vector3 loc)
        {
            if (loc.X >= ZoneInfo.X && loc.Y >= ZoneInfo.Y && loc.X < ZoneInfo.Right && loc.Y < ZoneInfo.Bottom)
            {
                return true;
            }
            return false;
        }
        bool IsIn(Point loc)
        {
            return IsIn(new Vector3(loc.X, loc.Y, 0));
        }
        public bool Fight(Vector3 loc)
        {
            bool inzone = IsIn(loc);
            if (inzone)
            {
                int seed = Util.Roll(0, 65535);
                if (seed < FightChance)
                {                    
                    return true;
                }
            }
            return false;

        }
        public List<string> RollMonsters()
        {
            List<string> monsterNames = new List<string>();
            int monsterCount = Util.Roll(1, 4);
            for (int i = 0; i < monsterCount; i++)
            {
                int mc = MonsterNames.Count;
                mc--;
                int j = Util.Roll(0, mc);
                monsterNames.Add(MonsterNames[j]);
            }
            return monsterNames;
        }
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

