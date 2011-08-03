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
    public class Item
    {
        public Guid GUID = Guid.NewGuid();
        public string Name;
        public string Description;
        public string IconName;
        public int IconIndex;
        public int ReqLevel;
        public int ReqMBLevel;
        public int ReqCBLevel;
        public float Weight;
        public float Worth;
        public float Endure = 1;
        public float Upgrade = 0;
        public int UpgradeTime = 0;
        public string Script;
        public ItemType ItemType;
        public Item GetClone()
        {
            return (Item)MemberwiseClone();
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
    public class EquipmentCHR : Item
    {
        public EquipmentCHR()
        {
            ItemType = ItemType.Equipment;
        }
        public EquipmentCHRType Type;
        //public int ExtMLevel, ExtELevel, ExtBLevel;

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
        public EquipmentMTL()
        {
            ItemType = ItemType.Equipment;
        }
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
    //[Serializable]
    //public class ScriptItem : Item
    //{
    //    public string Script;
    //    new public ScriptItem GetClone()
    //    {
    //        return (ScriptItem)MemberwiseClone();
    //    }
    //}
}
