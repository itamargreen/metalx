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
            Accurate = 0.5f;
            Description = "通常是给小孩玩的。";
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
            Accurate = 0.75f;
            Description = "很强劲的努，拿在手里就有勇气了。";
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
            Accurate = 0.85f;
            Description = "威力强大！";
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
            Missrate = 0.025f;
            Description = "姐姐给买的。";
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
            Missrate = 0.025f;
            Description = "姐姐给买的。";
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
            Missrate = 0.025f;
            Description = "只记得挺贵的。";
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
            Accurate = 0.01f;
            Description = "学校发的，几乎没什么用处，或许只有修车的时候才用得上。";
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
            Accurate = 0.01f;
            Description = "这帽子挺漂亮的。";
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
            Description = "能恢复ＨＰ的胶囊。";

            Script = "me hp 100";
        }
    }
    public class 爆竹 : Item
    {
        public 爆竹()
        {
            ItemType = ItemType.Battle;

            IconName = "icon_battle";

            Name = "爆竹";
            Description = "没准能有攻击力。";

            Script = "me hp 100\nmsg 恢复了100ＨＰ\nuntilpress y n\nmsg";


        }
    }
    #endregion
}
