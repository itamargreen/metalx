using System;
using System.Collections.ObjectModel;
using System.Text;

namespace MetalX.Data
{
    [Serializable]
    public class Item
    {
        public string Name;
        public string Description;
        public string Icon;
        public int IconIndex;
        public int ReqLevel;
        public int ReqMLevel, ReqELevel, ReqBLevel;
        public double Weight;
        public int Gold;
        public int Endure;
        public int Upgrade;
        public Item GetClone()
        {
            return (Item)MemberwiseClone();
        }
    }
    [Serializable]
    public enum EquipmentCHRType
    {
        Weapon,
        Body,
        Leg,
        Foot,
        Head,
        Hand,
    }
    [Serializable]
    public enum EquipmentMTLType
    {
        Motor,
        Body,
        Primary,
        Secondry,
        FuelBox,
        AmmoBox,
        ECU,
    }
    [Serializable]
    public class EquipmentCHR : Item
    {

        public int ExtMLevel, ExtELevel, ExtBLevel;

        int damage;
        int defense;
        double accurate;
        double missrate;
        int delay;//for ecu

        public int Damage
        {
            get
            {
                return damage + damage * Upgrade / 10;
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
                return defense + defense * Upgrade / 10;
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
                return delay - delay * Upgrade / 10;
            }
            set
            {
                delay = value;
            }
        }
        public double Accurate
        {
            get
            {
                return accurate + accurate * Upgrade / 10;
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
                return missrate + missrate * Upgrade / 10;
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
        double accurate;
        double missrate;
        double load;//for motor
        double ammoBoxCap, fuelBoxCap;
        int delay;//for ecu

        public int Delay
        {
            get
            {
                return delay - delay * Upgrade / 10;
            }
            set
            {
                delay = value;
            }
        }
        public double Accurate
        {
            get
            {
                return accurate + accurate * Upgrade / 10;
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
                return missrate + missrate * Upgrade / 10;
            }
            set
            {
                missrate = value;
            }
        }
        public double Load
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
                return damage + damage * Upgrade / 10;
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
                return defense + defense * Upgrade / 10;
            }
            set
            {
                defense = value;
            }
        }
        public double AmmoBoxCap
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
        public double FuelBoxCap
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
        public int RHP, RMP;
        new public Supply GetClone()
        {
            return (Supply)MemberwiseClone();
        }
    }
}
