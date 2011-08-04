using System;
using System.Collections.Generic;
using System.Text;

using MetalX.Define;

namespace MetalHunter
{
    #region 武器
    public class 弹弓 : EquipmentCHR
    {
        public 弹弓()
        {
            Type = EquipmentCHRType.Weapon;
            IconName = "icon_weapon";

            Name = "弹弓";
            Damage = 10;
            Accurate = 50f;
            Worth = 8;

            string str = "";
            str += "攻击：" + Damage;
            str += "\n";
            str += "命中：" + Accurate + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "通常是给小孩玩的。";
            Description = str;
        }
    }

    public class 狩猎弩 : EquipmentCHR
    {
        public 狩猎弩()
        {
            Type = EquipmentCHRType.Weapon;
            IconName = "icon_weapon";

            Name = "狩猎弩";
            Damage = 30;
            Accurate = 75f;
            Worth = 100;
            string str = "";
            str += "攻击：" + Damage;
            str += "\n";
            str += "命中：" + Accurate + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "很强劲的努，\n拿在手里就有勇气\n了。";
            Description = str;
        }
    }

    public class 双管猎枪 : EquipmentCHR
    {
        public 双管猎枪()
        {
            Type = EquipmentCHRType.Weapon;
            IconName = "icon_weapon";

            Name = "双管猎枪";
            Damage = 50;
            Accurate = 85f;
            Worth = 2200;
            string str = "";
            str += "攻击：" + Damage;
            str += "\n";
            str += "命中：" + Accurate + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "威力强大！";
            Description = str;
        }
    }
    #endregion
    #region 衣
    public class 运动服 : EquipmentCHR
    {
        public 运动服()
        {
            Type = EquipmentCHRType.Body;
            IconName = "icon_body";

            Name = "运动服";
            Defense = 5;
            Missrate = 2.5f;
            Worth = 190;
            string str = "";
            str += "防御：" + Defense;
            str += "\n";
            str += "闪避：" + Missrate  + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "姐姐给买的。";
            Description = str;
        }
    }
    #endregion
    #region 裤
    public class 运动裤 : EquipmentCHR
    {
        public 运动裤()
        {
            Type = EquipmentCHRType.Leg;
            IconName = "icon_leg";

            Name = "运动裤";
            Defense = 5;
            Missrate = 2.5f;
            Worth = 180;
            string str = "";
            str += "防御：" + Defense;
            str += "\n";
            str += "闪避：" + Missrate  + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "姐姐给买的。";
            Description = str;
        }
    }
    #endregion
    #region 鞋
    public class 登山鞋 : EquipmentCHR
    {
        public 登山鞋()
        {
            Type = EquipmentCHRType.Foot;
            IconName = "icon_foot";

            Name = "登山鞋";
            Defense = 5;
            Missrate = 1.5f;
            Worth = 220;
            string str = "";
            str += "防御：" + Defense;
            str += "\n";
            str += "闪避：" + Missrate + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "只记得挺贵的。";
            Description = str;
        }
    }
    #endregion
    #region 手
    public class 粗线手套 : EquipmentCHR
    {
        public 粗线手套()
        {
            Type = EquipmentCHRType.Hand;
            IconName = "icon_hand";

            Name = "粗线手套";
            Defense = 1;
            Accurate = 1f;
            Worth = 8;
            string str = "";
            str += "防御：" + Defense;
            str += "\n";
            str += "命中：" + Accurate + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "学校发的，\n几乎没什么用处，\n或许只有修车的\n时候才用得上。";
            Description = str;
        }
    }
    #endregion
    #region 头
    public class 棒球帽 : EquipmentCHR
    {
        public 棒球帽()
        {
            Type = EquipmentCHRType.Head;
            IconName = "icon_head";

            Name = "棒球帽";
            Defense = 1;
            Missrate = 1f;
            Worth = 38;
            string str = "";
            str += "防御：" + Defense;
            str += "\n";
            str += "闪避：" + Missrate + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "很漂亮的帽子。";
            Description = str;
        }
    }
    #endregion
    #region 道具
    public class 恢复胶囊小 : Item
    {
        public 恢复胶囊小()
        {
            ItemType = ItemType.Supply;

            IconName = "icon_supplychr";

            Name = "恢复胶囊小";
            Worth = 30;

            string str = "";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "能恢复ＨＰ的胶囊。";
            Description = str;

            Script = "me hp 100\nmsg ＨＰ恢复了１００\nuntilpress y n\nmsg";
        }
    }
    public class 爆竹 : Item
    {
        public 爆竹()
        {
            ItemType = ItemType.Battle;

            IconName = "icon_battle";

            Name = "爆竹";
            Worth = 20;

            string str = "";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "没准能有攻击力。";
            Description = str;

            Script = "";


        }
    }
    #endregion
}
