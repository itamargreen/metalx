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
        public string TextureName;
        //public Size TileSizePixel = new Size(16, 16);

        float moveSpeed = 2f;
        public float NeedMovePixel = 0;
        public Direction Direction;

        public Vector3 LastLocation;
        public Vector3 RealLocation;
        public Vector3 RealLocationPixel;
        public void SetRealLocation(int x, int y, int z, int unit)
        {
            SetRealLocation(new Vector3(x, y, z), unit);
        }
        public void SetRealLocation(Vector3 v3, int unit)
        {
            RealLocation = v3;
            RealLocationPixel = Util.Vector3MulInt(RealLocation, unit);
        }
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
                    if (Direction == Direction.U)
                    {
                        v3.Y--;
                    }
                    else if (Direction == Direction.D)
                    {
                        v3.Y++;
                    }
                    else if (Direction == Direction.L)
                    {
                        v3.X--;
                    }
                    else if (Direction == Direction.R)
                    {
                        v3.X++;
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
                if (Direction == Direction.U)
                {
                    v3.Y++;
                }
                else if (Direction == Direction.L)
                {
                    v3.X++;
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
                        if (movedPixel > unit / 3*1)
                        {
                            v3.Y--;
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
                        v3.Y++;
                    }
                    else
                    {
                        if (movedPixel > unit / 3 *2)
                        {
                            v3.Y++;
                        }
                    }
                }
                else if (Direction == Direction.L)
                {
                    if (nextl > lastl)
                    { }
                    else
                    {
                        v3.X--;
                    }
                }
                else if (Direction == Direction.R)
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

        public string InSceneName;
        public int InSceneIndex = -1;

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

        List<EquipmentCHR> Equipments = new List<EquipmentCHR>(8);
        public EquipmentCHR PrimaryWeapon
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.PrimaryWeapon];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.PrimaryWeapon] = value;
            }
        }
        public EquipmentCHR SecondryWeapon
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.SecondryWeapon];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.SecondryWeapon] = value;
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
        public EquipmentCHR Other
        {
            get
            {
                return Equipments[(int)EquipmentCHRType.Other];
            }
            set
            {
                Equipments[(int)EquipmentCHRType.Other] = value;
            }
        }
        /// <summary>
        /// 伤害
        /// </summary>
        public int Damage
        {
            get
            {
                int damage = 0;
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
        public int Defense
        {
            get
            {
                int defense = 0;
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
        public int Delay
        {
            get
            {
                int delay = 0;
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
        public float Accurate
        {
            get
            {
                float accurate = 0;
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
        public float Missrate
        {
            get
            {
                float missrate = 0;
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
        public float MoveSpeed
        {
            get
            {
                float speed = 0;
                foreach (EquipmentCHR e in Equipments)
                {
                    speed += e.MoveSpeed;
                }
                speed += moveSpeed;
                return speed;
            }
        }

        public List<Item> Bag = new List<Item>();
        public void BagIn(Item item)
        {
            Bag.Add(item);
        }
        public void BagOut(Guid guid)
        {
            for (int i = 0; i < Bag.Count; i++)
            {
                if (Bag[i].GUID == guid)
                {
                    Bag.RemoveAt(i);
                    return;
                }
            }
        }
    }
    [Serializable]
    public class PC : CHR
    { }
    [Serializable]
    public class NPC : CHR
    {
        //public Direction DefaultDirection;
        //public Vector3 DefaultLocation;
        public string DialogText;
    }
}
