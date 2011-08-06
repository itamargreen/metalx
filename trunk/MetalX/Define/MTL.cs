using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.Define
{
    class MTL
    {
        List<EquipmentMTL> Equipments = new List<EquipmentMTL>(8);
        public EquipmentMTL PrimaryWeapon
        {
            get
            {
                return Equipments[(int)EquipmentMTLType.PrimaryWeapon];
            }
            set
            {
                Equipments[(int)EquipmentMTLType.PrimaryWeapon] = value;
            }
        }
        public EquipmentMTL SecondryWeapon
        {
            get
            {
                return Equipments[(int)EquipmentMTLType.SecondryWeapon];
            }
            set
            {
                Equipments[(int)EquipmentMTLType.SecondryWeapon] = value;
            }
        }
        public EquipmentMTL SpecialWeapon
        {
            get
            {
                return Equipments[(int)EquipmentMTLType.SpecialWeapon];
            }
            set
            {
                Equipments[(int)EquipmentMTLType.SpecialWeapon] = value;
            }
        }
        public EquipmentMTL ECU
        {
            get
            {
                return Equipments[(int)EquipmentMTLType.ECU];
            }
            set
            {
                Equipments[(int)EquipmentMTLType.ECU] = value;
            }
        }
        public EquipmentMTL Engine
        {
            get
            {
                return Equipments[(int)EquipmentMTLType.Engine];
            }
            set
            {
                Equipments[(int)EquipmentMTLType.Engine] = value;
            }
        }
        public EquipmentMTL AmmoBox
        {
            get
            {
                return Equipments[(int)EquipmentMTLType.AmmoBox];
            }
            set
            {
                Equipments[(int)EquipmentMTLType.AmmoBox] = value;
            }
        }
        public EquipmentMTL FuelBox
        {
            get
            {
                return Equipments[(int)EquipmentMTLType.FuelBox];
            }
            set
            {
                Equipments[(int)EquipmentMTLType.FuelBox] = value;
            }
        }
        public EquipmentMTL Other
        {
            get
            {
                return Equipments[(int)EquipmentMTLType.Other];
            }
            set
            {
                Equipments[(int)EquipmentMTLType.Other] = value;
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
                foreach (EquipmentMTL e in Equipments)
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
                foreach (EquipmentMTL e in Equipments)
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
                foreach (EquipmentMTL e in Equipments)
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
                foreach (EquipmentMTL e in Equipments)
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
                foreach (EquipmentMTL e in Equipments)
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
                foreach (EquipmentMTL e in Equipments)
                {
                    speed += e.MoveSpeed;
                }
                return speed;
            }
        }
    }
}
