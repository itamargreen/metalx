using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.Define
{
    [Serializable]
    public enum ItemType
    {
        Tool = 0,
        Supply = 1,
        Battle = 2,
        Equipment = 3,
    }
    [Serializable]
    public enum Quality
    {
        SSS = 130,
        SS = 120,
        S = 110,
        A = 100,
        B = 90,
        C = 80,
        D = 70,
    }
    [Serializable]
    public class Item
    {
        public MemoryIndexer ShotMovieIndexer = new MemoryIndexer();
        public MemoryIndexer FlyMovieIndexer = new MemoryIndexer();
        public MemoryIndexer HitMovieIndexer = new MemoryIndexer();
        public double FlyTime = 0;
        public Guid GUID = Guid.NewGuid();
        public string Name;
        public MemoryIndexer Icon = new MemoryIndexer();
        public string Description;
        public int ReqLevel;
        public int ReqMBLevel;
        public int ReqCBLevel;
        public double Weight;
        public double Worth;
        public string Script;
        public ItemType ItemType;
        public Quality Quality = Quality.C;
        public Item GetClone()
        {
            return (Item)MemberwiseClone();
        }
    }
    [Serializable]
    public class BattleItem : Item
    {
        public BattleItem()
            : base()
        {
            ItemType = ItemType.Battle;
        }
        protected double damage;
        public double Damage
        {
            get
            {
                //double t = (double)Quality / 100;
                double t = 1;
                return t * damage;
            }
            set
            {
                damage = value;
            }
        }
        protected double accurate;
        public double Accurate
        {
            get
            {
                //double t = (double)Quality / 100;
                double t = 1;
                return t * accurate;
            }
            set
            {
                accurate = value;
            }
        }
    }
    [Serializable]
    public class Equipment : Item
    {

        public Equipment()
            : base()
        {
            ItemType = ItemType.Equipment;
        }

        protected double damage;
        protected double defense;
        protected double accurate;
        protected double missrate;
        protected double delay;
        protected double moveSpeed;

        public double Damage
        {
            get
            {
                //double t = (double)Quality / 100;
                double t = 1;
                return t * damage;
            }
            set
            {
                damage = value;
            }
        }
        public double Defense
        {
            get
            {
                //double t = (double)Quality / 100;
                double t = 1;
                return t * defense;
            }
            set
            {
                defense = value;
            }
        }
        public double Accurate
        {
            get
            {
                //double t = (double)Quality / 100;
                double t = 1;
                return t * accurate;
            }
            set
            {
                accurate = value;
            }
        }
        public double Missrate
        {
            get
            {
                //double t = (double)Quality / 100;
                double t = 1;
                return t * missrate;
            }
            set
            {
                missrate = value;
            }
        }

        public double Delay
        {
            get
            {
                return delay;
            }
            set
            {
                delay = value;
            }
        }
        public double MoveSpeed
        {
            get
            {
                return moveSpeed;
            }
            set
            {
                moveSpeed = value;
            }
        }

        //public double Endure = 1;
        //public double Upgrade = 0;
        //public int UpgradeTime = 0;
        new public Equipment GetClone()
        {
            return (Equipment)MemberwiseClone();
        }
    }
    [Serializable]
    public enum EquipmentCHRType
    {
        Weapon = 0,
        Body = 1,
        Leg = 2,
        Foot = 3,
        Head = 4,
        Hand = 5,
        Ring = 6,
        Necklace = 7,
    }
    [Serializable]
    public enum EquipmentMTLType
    {
        PrimaryWeapon = 0,
        SecondryWeapon = 1,
        SpecialWeapon = 2,
        Engine = 3,
        ECU = 4,
        FuelBox = 5,
        AmmoBox = 6,
        Other = 7,
    }
    [Serializable]
    public class EquipmentCHR : Equipment
    {
        public EquipmentCHR()
            : base()
        { }
        public EquipmentCHRType EquipmentType;
        new public EquipmentCHR GetClone()
        {
            return (EquipmentCHR)MemberwiseClone();
        }
    }
    [Serializable]
    public class EquipmentMTL : Equipment
    {
        public EquipmentMTL()
            : base()
        {
        }

        public EquipmentMTLType EquipmentType;
        double load;
        double ammoBoxCap, fuelBoxCap;
        public double Load
        {
            get
            {
                return load;
            }
            set
            {
                load = value;
            }
        }
        public double AmmoBoxCap
        {
            get
            {
                return ammoBoxCap;
            }
            set
            {
                ammoBoxCap = value;
            }
        }
        public double FuelBoxCap
        {
            get
            {
                return fuelBoxCap;
            }
            set
            {
                fuelBoxCap = value;
            }
        }

        new public EquipmentMTLType GetClone()
        {
            return (EquipmentMTLType)MemberwiseClone();
        }

    }

}
