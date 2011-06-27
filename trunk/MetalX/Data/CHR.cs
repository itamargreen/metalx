using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;

namespace MetalX.Data
{
    [Serializable]
    public class CHR
    {
        public string Name;

        public int TextureIndex = -1;
        public string TextureFileName;
        public Size TileSizePixel = new Size(24, 24);

        public float MoveSpeed = 3f;
        public float NeedMovePixel = 0;
        public Direction Direction;

        public Vector3 LastLocation;
        public Vector3 RealLocation;
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

        public Vector3 GetStayLocation(int unit)
        {
            Vector3 v3 = new Vector3(
                    (int)RealLocation.X / unit * unit,
                    (int)RealLocation.Y / unit * unit,
                    (int)RealLocation.Z / unit * unit);
            if (NeedMovePixel > 0)
            {
                if (Direction == Direction.U)
                {
                    v3 = new Vector3(v3.X, v3.Y + unit, v3.Z);
                }
                else if (Direction == Direction.L)
                {
                    v3 = new Vector3(v3.X + unit, v3.Y, v3.Z);
                }
            }
            return v3;
        }
        public Vector3 GetFrontLocation(int unit)
        {
            Vector3 v3 = NextLocation;
            if (NeedMovePixel == 0)
            {
                if (Direction == Direction.U)
                {
                    v3.Y -= unit;
                }
                else if (Direction == Direction.D)
                {
                    v3.Y += unit;
                }
                else if (Direction == Direction.L)
                {
                    v3.X -= unit;
                }
                else if (Direction == Direction.R)
                {
                    v3.X += unit;
                }
            }
            return v3;
        }
        public Vector3 GetDrawLocation(int unit, int lastl, int nextl)
        {
            Vector3 v3 = GetStayLocation(unit);
            float movedPixel = unit - NeedMovePixel;
            if (NeedMovePixel > 0)
            {
                if (Direction == Direction.U)
                {
                    if (nextl > lastl)
                    {
                        if (movedPixel > unit / 3)
                        {
                            int yo = -unit;
                            v3.Y += yo;
                        }
                    }
                    else
                    {
 
                    }
                }
                else if (Direction == Direction.D)
                {
                    if (nextl > lastl)
                    {
                        int yo = unit;
                        v3.Y += yo;
                    }
                    else
                    {
                        if (movedPixel > unit / 3 * 2)
                        {
                            int yo = unit;
                            v3.Y += yo;
                        }
                    }
                }
                else if (Direction == Direction.L)
                {
                    if (nextl > lastl)
                    { }
                    else
                    {
                        int xo = -unit;
                        v3.X += xo;
                    }
                }
                else if (Direction == Direction.R)
                {
                    if (nextl > lastl)
                    { }
                    else
                    {
                        int xo = unit;
                        v3.X += xo;
                    }
                }
            }

            return v3;
        }

        public string InSceneName;
        public int InSceneIndex;

        public bool CanMove;
        public bool CanTurn;

        public int Gold;

        public int Level;

        public int MLevel;
        public int ELevel;
        public int BLevel;

        public int HP;
        public int HPMax;
        public int MP;
        public int MPMax;

        public List<Item> Bag = new List<Item>();

        public void BagIn(Item item)
        {
            Bag.Add(item);
        }
        public void BagOut(int i)
        {
            Bag.RemoveAt(i);
        }
    }
    [Serializable]
    public class PC : CHR
    { }
    [Serializable]
    public class NPC : CHR
    { }
}
