using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;

using MetalX.File;

namespace MetalX.Define
{
    [Serializable]
    public class CHR
    {
        public CHR()
        {
            for (int i = 0; i < 8; i++)
            {
                Equipments.Add(new EquipmentCHR());
                BattleMovieIndexers.Add(new MemoryIndexer());
                //BattleMovies.Add(new MetalXMovie());
            }
        }
        public string Name = "noname";

        public int TextureIndex = -1;
        public string TextureName;
        public int HeadTextureIndex = -1;
        public string HeadTextureName;
        public Size Size = new Size(1, 1);
        public bool Invisible = false;
        [NonSerialized]
        bool isForceMove = false;

        double moveSpeed = 2;
        [NonSerialized]
        public int NeedMoveStep = 0;
        [NonSerialized]
        public double NeedMovePixel = 0;
        [NonSerialized]
        public Direction LastDirection;
        //[NonSerialized]
        [NonSerialized]
        public Direction ForceDirection;
        public Direction OppositeDirection
        {
            get
            {
                if (RealDirection == Direction.U)
                {
                    return Direction.D;
                }
                else if (RealDirection == Direction.L)
                {
                    return Direction.R;
                }
                else if (RealDirection == Direction.D)
                {
                    return Direction.U;
                }
                else
                {
                    return Direction.L;
                }
            }
        }

        [NonSerialized]
        public Vector3 LastLocation;
        //[NonSerialized]
        public Vector3 RealLocation;
        public Direction RealDirection;
        //[NonSerialized]
        public Vector3 RealLocationPixel;
        public Color ColorFilter = Color.White;
        public void SetRealLocation(int x, int y, int z, int unit)
        {
            SetRealLocation(new Vector3(x, y, z), unit);
        }
        public void SetRealLocation(Vector3 v3, int unit)
        {
            NextLocation = LastLocation = RealLocation = v3;
            RealLocationPixel = Util.Vector3MulInt(RealLocation, unit);
        }
        [NonSerialized]
        public Vector3 NextLocation;
        #region old
        //public Vector3 GetLogicLastLocation(int unit)
        //{
        //    Vector3 v3 = new Vector3(
        //             (int)LastLocation.X / unit * unit,
        //             (int)LastLocation.Y / unit * unit,
        //             (int)LastLocation.Z / unit * unit);
        //    if (NeedMovePixel > 0)
        //    {
        //        if (Direction == Direction.U)
        //        {

        //            return new Vector3(v3.X, v3.Y + unit, v3.Z);
        //        }
        //        else if (Direction == Direction.D)
        //        {
        //            return new Vector3(v3.X, v3.Y - unit, v3.Z);
        //        }
        //    }
        //    return v3;
        //}
        //public Vector3 GetLogicLocation(int unit, int lastlayer, int nextlayer)
        //{
        //    Vector3 v3 = new Vector3(
        //            (int)Location.X / unit * unit,
        //            (int)Location.Y / unit * unit,
        //            (int)Location.Z / unit * unit);
        //    if (NeedMovePixel > 0)
        //    {
        //        if (Direction == Direction.U)
        //        {
        //            if (nextlayer < lastlayer)
        //            {
        //                if ((unit - (int)NeedMovePixel) <= unit / 3 * 2)
        //                {
        //                    return new Vector3(v3.X, v3.Y + unit, v3.Z);
        //                }
        //            }
        //        }
        //        else if (Direction == Direction.D)
        //        {
        //            if (nextlayer > lastlayer)
        //            {
        //                if ((unit - (int)NeedMovePixel) > unit / 3)
        //                {
        //                    return new Vector3(v3.X, v3.Y - unit, v3.Z);
        //                }
        //            }
        //        }
        //        else if (Direction == Direction.L)
        //        {
        //            if (nextlayer < lastlayer)
        //            {
        //                return new Vector3(v3.X + unit, v3.Y, v3.Z);
        //            }
        //        }
        //        else if (Direction == Direction.R)
        //        {
        //            if (nextlayer > lastlayer)
        //            {
        //                return new Vector3(v3.X - unit, v3.Y, v3.Z);
        //            }
        //        }
        //    }
        //    return v3;
        //}
        //public Vector3 GetLogicNextLocation(int unit)
        //{
        //    return new Vector3(
        //            (int)NextLocation.X / unit * unit,
        //            (int)NextLocation.Y / unit * unit,
        //            (int)NextLocation.Z / unit * unit);
        //}
        #endregion

        public Vector3 FrontLocation
        {
            get
            {
                Vector3 v3 = NextLocation;
                if (NeedMovePixel == 0)
                {
                    if (RealDirection == Direction.U)
                    {
                        v3.Y--;
                    }
                    else if (RealDirection == Direction.D)
                    {
                        v3.Y++;
                    }
                    else if (RealDirection == Direction.L)
                    {
                        v3.X--;
                    }
                    else if (RealDirection == Direction.R)
                    {
                        v3.X++;
                    }
                }
                return v3;
            }
        }
        public Vector3 ForceFrontLocation
        {
            get
            {
                Vector3 v3 = NextLocation;
                if (NeedMovePixel == 0)
                {
                    if (ForceDirection == Direction.U)
                    {
                        v3.Y--;
                    }
                    else if (ForceDirection == Direction.D)
                    {
                        v3.Y++;
                    }
                    else if (ForceDirection == Direction.L)
                    {
                        v3.X--;
                    }
                    else if (ForceDirection == Direction.R)
                    {
                        v3.X++;
                    }
                }
                return v3;
            }
        }
        public Vector3 RangeLocation
        {
            get
            {
                Vector3 v3 = NextLocation;
                if (NeedMovePixel == 0)
                {
                    if (RealDirection == Direction.U)
                    {
                        v3.Y -= 2;
                    }
                    else if (RealDirection == Direction.D)
                    {
                        v3.Y += 2;
                    }
                    else if (RealDirection == Direction.L)
                    {
                        v3.X -= 2;
                    }
                    else if (RealDirection == Direction.R)
                    {
                        v3.X += 2;
                    }
                }
                return v3;
            }
        }
        public Vector3 GetStayLocation(int unit)
        {
            Vector3 v3 = Util.Vector3DivInt(RealLocationPixel, unit);
            if (NeedMovePixel > 0)
            {
                if (RealDirection == Direction.U)
                {
                    v3.Y++;
                }
                else if (RealDirection == Direction.L)
                {
                    v3.X++;
                }
            }
            return v3;
        }
        public Vector3 GetDrawLocation(int unit, int lastl, int nextl)
        {
            Vector3 v3 = GetStayLocation(unit);
            double movedPixel = unit - NeedMovePixel;
            if (NeedMovePixel > 0)
            {
                if (RealDirection == Direction.U)
                {
                    if (nextl > lastl)
                    {
                        if (movedPixel > unit)//unit / 3*1)
                        {
                            v3.Y--;
                        }
                    }
                    else
                    {

                    }
                }
                else if (RealDirection == Direction.D)
                {
                    if (nextl > lastl)
                    {
                        v3.Y++;
                    }
                    else
                    {
                        if (movedPixel > 0)//unit / 3 *2)
                        {
                            v3.Y++;
                        }
                    }
                }
                else if (RealDirection == Direction.L)
                {
                    if (nextl > lastl)
                    { }
                    else
                    {
                        v3.X--;
                    }
                }
                else if (RealDirection == Direction.R)
                {
                    if (nextl > lastl)
                    { }
                    else
                    {
                        v3.X++;
                    }
                }
            }

            return v3;
        }

        [NonSerialized]
        public string InSceneName;
        [NonSerialized]
        public int InSceneIndex = -1;

        public bool CanMove = true;
        public bool CanTurn = true;
        public bool IsRigor = false;
        //public bool CanControl = true;
        
        //public void Freeze()
        //{
        //    CanTurn = false;
        //    CanMove = false;
        //    CanControl = false;
        //}
        //public void Unfreeze()
        //{
        //    CanTurn = true;
        //    CanMove = true;
        //    CanControl = true;
        //}

        public int Gold;

        public int EXP;
        public int Level;

        public int MBLevel;//车战LV
        //public int ELevel;
        public int CBLevel;//人战LV

        public int Strength;
        public int Agility;
        public int Intelligence;
        public int Physique;

        public int HP;
        public int HPMax = 100;

        public List<EquipmentCHR> Equipments = new List<EquipmentCHR>(8);
        public EquipmentCHR Weapon
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.Weapon];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.Weapon] = value;
            }
        }      
        public EquipmentCHR Body
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.Body];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.Body] = value;
            }
        }
        public EquipmentCHR Leg
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.Leg];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.Leg] = value;
            }
        }

        public EquipmentCHR Foot
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.Foot];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.Foot] = value;
            }
        }
        public EquipmentCHR Head
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.Head];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.Head] = value;
            }
        }
        public EquipmentCHR Hand
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.Hand];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.Hand] = value;
            }
        }

        public EquipmentCHR Ring
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.Ring];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.Ring] = value;
            }
        }
        public EquipmentCHR Necklace
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.Necklace];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.Necklace] = value;
            }
        }
        /// <summary>
        /// 伤害
        /// </summary>
        public double Damage
        {
            get
            {
                double damage = 0;
                foreach (EquipmentCHR e in Equipments)
                {
                    damage += e.Damage;
                }
                return damage;
            }
        }
        /// <summary>
        /// 防御
        /// </summary>
        public double Defense
        {
            get
            {
                double defense = 0;
                foreach (EquipmentCHR e in Equipments)
                {
                    defense += e.Defense;
                }
                return defense;
            }
        }
        /// <summary>
        /// 反应延迟
        /// </summary>
        public double Delay
        {
            get
            {
                double delay = 0;
                foreach (EquipmentCHR e in Equipments)
                {
                    delay += e.Delay;
                }
                return delay;
            }
        }
        /// <summary>
        /// 命中率
        /// </summary>
        public double Accurate
        {
            get
            {
                double accurate = 0;
                foreach (EquipmentCHR e in Equipments)
                {
                    accurate += e.Accurate;
                }
                return accurate;
            }
        }
        /// <summary>
        /// 躲闪率
        /// </summary>
        public double Missrate
        {
            get
            {
                double missrate = 0;
                foreach (EquipmentCHR e in Equipments)
                {
                    missrate += e.Missrate;
                }
                return missrate;
            }
        }
        /// <summary>
        /// 移动速度
        /// </summary>
        public double MoveSpeed
        {
            get
            {
                double speed = 0;
                foreach (EquipmentCHR e in Equipments)
                {
                    speed += e.MoveSpeed;
                }
                speed += moveSpeed;
                return speed;
            }
        }


        List<Item> bag = new List<Item>();

        public List<Item> Bag
        {
            get
            {
                return bag;
            }
        }
        public int BagItemCount
        {
            get
            {
                return bag.Count;
            }
        }
        public int BagItemCapacity = 16;

        public void BagAdd(Item item)
        {
            if (bag.Count < BagItemCapacity)
            {
                bag.Add(item);
            }
        }
        public Item BagSee(int i)
        {
            Item item = bag[i].GetClone();
            //bag.RemoveAt(i);
            return item;
        }
        public void BagRemove(int i)
        {
            bag.RemoveAt(i);
        }
        public void BagEquip(int i)
        {
            EquipmentCHR item = null;
            try
            {
                item = (EquipmentCHR)BagSee(i);
                bag.RemoveAt(i);
            }
            catch
            {
                //BagIn(item);
                return;
            }
            EquipmentCHR item_tmp;
            item_tmp = Equipments[(int)item.EquipmentType].GetClone();
            if (item_tmp.Name != null)
            {
                BagAdd(item_tmp);
            }
            Equipments[(int)item.EquipmentType] = item;
        }

        public bool Face(Direction dir)
        {
            if (CanTurn == false)
            {
                return false;
            }
            LastDirection = RealDirection;
            RealDirection = dir;
            return true;
        }
        //public void ForceFace(Direction dir)
        //{
        //    ForceMoveDirection = dir;
        //}
        public void ForceMove(int stepPixel)
        {
            LastLocation = RealLocation;
            RealLocation = NextLocation = ForceFrontLocation;

            NeedMoveStep = 1;
            NeedMovePixel += 1 * stepPixel;
            isForceMove = true;
        }
        public bool Move(Scene s, NPC n, int stepPixel)
        {
            if (CanMove == false)
            {
                return false;
            }
            if (s.IsInScene(FrontLocation) == false)
            {
                return false;
            }
            if (n != null)
            {
                if (n.IsDoor == false)
                {
                    return false;
                }
            }
            try
            {
                if (s[FrontLocation].CHRCanRch == false)
                {
                    return false;
                }
            }
            catch
            {
                //return false;
            }

            LastLocation = RealLocation;
            RealLocation = NextLocation = FrontLocation;
            NeedMoveStep = 1;
            NeedMovePixel += 1 * stepPixel;
            return true;
        }
        public void MovePixel()
        {
            if (NeedMovePixel > 0)
            {
                if (isForceMove)
                {
                    double movePixel = MoveSpeed;
                    if (NeedMovePixel < MoveSpeed)
                    {
                        movePixel = NeedMovePixel;
                    }

                    NeedMovePixel -= movePixel;

                    if (ForceDirection == Direction.U)
                    {
                        RealLocationPixel.Y -= (float)movePixel;
                    }
                    else if (ForceDirection == Direction.L)
                    {
                        RealLocationPixel.X -= (float)movePixel;
                    }
                    else if (ForceDirection == Direction.D)
                    {
                        RealLocationPixel.Y += (float)movePixel;
                    }
                    else if (ForceDirection == Direction.R)
                    {
                        RealLocationPixel.X += (float)movePixel;
                    }

                    if (NeedMovePixel <= 0)
                    {
                        isForceMove = false;
                    }
                }
                else
                {
                    double movePixel = MoveSpeed;
                    if (NeedMovePixel < MoveSpeed)
                    {
                        movePixel = NeedMovePixel;
                    }

                    NeedMovePixel -= movePixel;

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
        }

        public Size BattleSize;
        [NonSerialized]
        public Vector3 BattleLocation;
        [NonSerialized]
        public BattleState BattleState;
        [NonSerialized]
        public BattleState BattleStateBackup;
        [NonSerialized]
        public MetalXMovie BattleMovie = new MetalXMovie();
        public void SetBattleMovie(MetalXMovie movie, Vector3 fl, Vector3 tl, double playtime)
        {
            fl.X -= movie.TileSize2X.Width / 2;
            fl.Y -= movie.TileSize2X.Height;
            tl.X -= movie.TileSize2X.Width / 2;
            tl.Y -= movie.TileSize2X.Height;

            BattleMovie = movie;
            BattleMovie.BeginLocation = fl;
            BattleMovie.EndLocation = tl;
            BattleMovie.PlayTime = playtime;
            BattleMovie.Reset();
        }
        public void SetBattleMovie(MetalXMovie movie, Vector3 loc)
        {
            SetBattleMovie(movie, loc, loc, 1);
        }
        public void SetBattleMovie(MetalXMovie movie)
        {
            SetBattleMovie(movie, BattleLocation);
        }
        [NonSerialized]
        public List<MemoryIndexer> BattleMovieIndexers = new List<MemoryIndexer>();
        public MemoryIndexer BattleMovieIndexer
        {
            get
            {
                return BattleMovieIndexers[(int)BattleState];
            }
        }
        public Vector3 BattleWeaponLocation;
        public bool BattleShowWeapon = false;
        //public MetalXMovie BattleMovie
        //{
        //    get
        //    {
        //        try
        //        {
        //            MetalXMovie movie = BattleMovies[(int)BattleState];
        //            if (movie.NextFrame() == false)
        //            {
        //                BattleState = BattleState.Stand;
        //                movie = BattleMovies[(int)BattleState];
        //            }
        //            return movie;
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
        //}
        //public MetalXMovie GetBattleMovie(BattleState bs)
        //{
        //    int i = (int)bs;
        //    return BattleMovies[i];
        //}

        //public void SetBattleMovie(BattleState bs, Vector3 from, Vector3 to, double timespan)
        //{
        //    BattleStateBackup = BattleState;
        //    BattleState = bs;
        //    int i = (int)bs;
        //    BattleLocation = BattleMovies[i].BeginLocation = from;
        //    BattleMovies[i].EndLocation = to;
        //    BattleMovies[i].PlayTime = timespan;
        //    BattleMovies[i].Reset();
        //}
        //public void SetBattleMovie(BattleState bs, Vector3 from, Vector3 to)
        //{
        //    BattleStateBackup = BattleState;
        //    BattleState = bs;
        //    int i = (int)bs;
        //    BattleLocation = BattleMovies[i].BeginLocation = from;
        //    BattleMovies[i].EndLocation = to;
        //    BattleMovies[i].PlayTime = BattleMovies[i].MovieTime;
        //    BattleMovies[i].Reset();
        //}
        //public void SetBattleMovie(BattleState bs, Vector3 loc, double timespan)
        //{
        //    SetBattleMovie(bs, BattleLocation, loc, timespan);
        //}
        //public void SetBattleMovie(BattleState bs, double timespan)
        //{
        //    SetBattleMovie(bs, BattleLocation, timespan);
        //}
        //public void SetBattleMovie(BattleState bs, Vector3 loc)
        //{
        //    SetBattleMovie(bs, BattleLocation, loc);
        //}
        //public void SetBattleMovie(BattleState bs)
        //{
        //    SetBattleMovie(bs, BattleLocation);
        //}

        //public void LoadBattleMovies(Game game)
        //{
        //    BattleMovies = new List<MetalXMovie>();
        //    for (int i = 0; i < BattleMovieIndexers.Count; i++)
        //    {
        //        if (BattleMovieIndexers[i].Name == null)
        //        {
        //            continue;
        //        }

        //        File.MetalXMovie movie = game.LoadDotMXMovie(game.MovieFiles[BattleMovieIndexers[i].Name].FullName);

        //        movie.BeginLocation = BattleLocation;
        //        movie.EndLocation = BattleLocation;
        //        movie.PlayTime = movie.MovieTime;
        //        movie.Reset();

        //        BattleMovies.Add(movie);
        //    }
        //}
    }
    [Serializable]
    public class PC : CHR
    {
        public PC()
            : base()
        {
            BattleShowWeapon = true;
        }
    }
    [Serializable]
    public class NPC : CHR
    {
        public bool IsDoor = false;
        public bool IsBox = false;
        public Direction DefaultDirection;
        //public Vector3 DefaultLocation;
        //public string Dialog;
        public string Script;
        public void RecoverDirection()
        {
            //if (IsBox)
            //{
            //    if (bag.Count == 0)
            //    {
            //        return;
            //    }
            //}
            RealDirection = DefaultDirection;
        }
        public void FocusOnMe(PC me)
        {
            if (IsBox)
            {
                RealDirection = Direction.L;
            }
            else if (IsDoor)
            {
                //me.RealDirection = Direction.L;
            }
            else
            {
                if (me.RealDirection == Direction.U)
                {
                    RealDirection = Direction.D;
                }
                else if (me.RealDirection == Direction.L)
                {
                    RealDirection = Direction.R;
                }
                else if (me.RealDirection == Direction.D)
                {
                    RealDirection = Direction.U;
                }
                else if (me.RealDirection == Direction.R)
                {
                    RealDirection = Direction.L;
                }
            }
        }
    }
}
