using System;
using System.Collections.Generic;
using System.Text;

namespace MetalX.Data
{
    [Serializable]
    public class Item
    {
        public Guid GUID = Guid.NewGuid();
        public string Name;
        public string Description;
        public string Icon;
        public int IconIndex;
        public int ReqLevel;
        public int ReqMLevel, ReqELevel, ReqBLevel;
        public float Weight;
        public float Money;
        public float Endure = 1;
        public float Upgrade = 0;
        public int UpgradeTime = 0;
        public Item GetClone()
        {
            return (Item)MemberwiseClone();
        }
    }
    [Serializable]
    public enum EquipmentCHRType
    {
        PrimaryWeapon = 0,
        SecondryWeapon = 1,
        Body = 2,
        Leg = 3,
        Foot = 4,
        Head = 5,
        Hand = 6,
        Other=7,
    }
    [Serializable]
    public enum EquipmentMTLType
    {
        PrimaryWeapon = 0,
        SecondryWeapon = 1,
        Motor = 2,
        Body = 3,
        FuelBox = 4,
        AmmoBox = 5,
        ECU = 6,
        Other=7,
    }
    [Serializable]
    public class EquipmentCHR : Item
    {
        public EquipmentCHRType EquipmentTyp;
        public int ExtMLevel, ExtELevel, ExtBLevel;

        int damage;
        int defense;
        float accurate;
        float missrate;
        int delay;//for ecu
        float moveSpeed;
        public float MoveSpeed
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
        public int Damage
        {
            get
            {
                return (int)(damage + damage * Upgrade);
            }
            set
            {
                damage = value;
            }
        }
        public int Defense
        {
            get
            {
                return (int)(defense + defense * Upgrade);
            }
            set
            {
                defense = value;
            }
        }
        public int Delay
        {
            get
            {
                return (int)(delay - delay * Upgrade);
            }
            set
            {
                delay = value;
            }
        }
        public float Accurate
        {
            get
            {
                return accurate + accurate * Upgrade;
            }
            set
            {
                accurate = value;
            }
        }
        public float Missrate
        {
            get
            {
                return missrate + missrate * Upgrade;
            }
            set
            {
                missrate = value;
            }
        }

        new public EquipmentCHR GetClone()
        {
            return (EquipmentCHR)MemberwiseClone();
        }
    }
    [Serializable]
    public class EquipmentMTL : Item
    {
        public EquipmentMTLType Type;

        int damage;
        int defense;
        float accurate;
        float missrate;
        float load;//for motor
        float ammoBoxCap, fuelBoxCap;
        int delay;//for ecu
        float moveSpeed;
        public float MoveSpeed
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

        public int Delay
        {
            get
            {
                return (int)(delay - delay * Upgrade);
            }
            set
            {
                delay = value;
            }
        }
        public float Accurate
        {
            get
            {
                return accurate + accurate * Upgrade;
            }
            set
            {
                accurate = value;
            }
        }
        public float Missrate
        {
            get
            {
                return missrate + missrate * Upgrade;
            }
            set
            {
                missrate = value;
            }
        }
        public float Load
        {
            get
            {
                return load + load * Upgrade / 10;
            }
            set
            {
                load = value;
            }
        }
        public int Damage
        {
            get
            {
                return (int)(damage + damage * Upgrade);
            }
            set
            {
                damage = value;
            }
        }
        public int Defense
        {
            get
            {
                return (int)(defense + defense * Upgrade);
            }
            set
            {
                defense = value;
            }
        }
        public float AmmoBoxCap
        {
            get
            {
                return ammoBoxCap + ammoBoxCap * Upgrade / 10;
            }
            set
            {
                ammoBoxCap = value;
            }
        }
        public float FuelBoxCap
        {
            get
            {
                return fuelBoxCap + fuelBoxCap * Upgrade / 10;
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
    [Serializable]
    public class Supply : Item
    {
        public int RecoverHP, RecoverMP;
        new public Supply GetClone()
        {
            return (Supply)MemberwiseClone();
        }
    }
}
