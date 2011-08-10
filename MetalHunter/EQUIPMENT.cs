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
            EquipmentType = EquipmentCHRType.Weapon;
            Icon.Name = "sling_icon";

            Name = "弹弓";
            Damage = 10;
            Accurate = 50;
            Worth = 8;

            string str = "";
            str += "品质：" + ItemLevel.ToString();
            str += "\n";          
            str += "攻击：" + Damage.ToString("f1");
            str += "\n";
            str += "命中：" + Accurate.ToString("f1") + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "通常是给小孩玩的。";
            Description = str;

            ShotMovieIndexer.Name = "sling_shot";
            HitMovieIndexer.Name = "sling_hit";
            BulletTime = 200;
        }
    }

    public class 狩猎弩 : EquipmentCHR
    {
        public 狩猎弩()
        {
            EquipmentType = EquipmentCHRType.Weapon;
            Icon.Name = "bow_icon";

            Name = "狩猎弩";
            Damage = 30;
            Accurate = 75f;
            Worth = 100;
            string str = "";
            str += "品质：" + ItemLevel.ToString();
            str += "\n";         
            str += "攻击：" + Damage.ToString("f1");
            str += "\n";
            str += "命中：" + Accurate.ToString("f1") + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "很强劲的努，\n拿在手里就有勇气\n了。";
            Description = str;

            ShotMovieIndexer.Name = "bow_shot";
            HitMovieIndexer.Name = "bow_hit";
            BulletTime = 200;
        }
    }

    public class M16突击步枪 : EquipmentCHR
    {
        public M16突击步枪()
        {
            EquipmentType = EquipmentCHRType.Weapon;
            Icon.Name = "m16_icon";

            Name = "M16突击步枪";
            Damage = 50;
            Accurate = 85f;
            Worth = 2200;
            string str = "";
            str += "品质：" + ItemLevel.ToString();
            str += "\n"; 
            str += "攻击：" + Damage.ToString("f1");
            str += "\n";
            str += "命中：" + Accurate.ToString("f1") + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "威力强大！";
            Description = str;

            ShotMovieIndexer.Name = "m16_shot";
            HitMovieIndexer.Name = "rife_hit";
        }
    }
    #endregion
    #region 衣
    public class 运动服 : EquipmentCHR
    {
        public 运动服()
        {
            EquipmentType = EquipmentCHRType.Body;
            Icon.Name = "icon_body";

            Name = "运动服";
            Defense = 5;
            Missrate = 2.5f;
            Worth = 190;
            string str = ""; str += "品质：" + ItemLevel.ToString();  str += "\n"; 

            str += "防御：" + Defense.ToString("f1");
            str += "\n";
            str += "闪避："+ Missrate.ToString("f1") + "%";
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
            EquipmentType = EquipmentCHRType.Leg;
            Icon.Name = "icon_leg";

            Name = "运动裤";
            Defense = 5;
            Missrate = 2.5f;
            Worth = 180;
            string str = ""; str += "品质：" + ItemLevel.ToString(); str += "\n"; 
            str += "防御：" + Defense.ToString("f1");
            str += "\n";
            str += "闪避："+ Missrate.ToString("f1") + "%";
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
            EquipmentType = EquipmentCHRType.Foot;
            Icon.Name = "icon_foot";

            Name = "登山鞋";
            Defense = 5;
            Missrate = 1.5f;
            Worth = 220;
            string str = ""; str += "品质：" + ItemLevel.ToString(); str += "\n"; 
            str += "防御：" + Defense.ToString("f1");
            str += "\n";
            str += "闪避："+ Missrate.ToString("f1") + "%";
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
            EquipmentType = EquipmentCHRType.Hand;
            Icon.Name = "icon_hand";

            Name = "粗线手套";
            Defense = 1;
            Accurate = 1f;
            Worth = 8;
            string str = ""; str += "品质：" + ItemLevel.ToString(); str += "\n"; 
            str += "防御：" + Defense.ToString("f1");
            str += "\n";
            str += "命中：" + Accurate.ToString("f1") + "%";
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
            EquipmentType = EquipmentCHRType.Head;
            Icon.Name = "icon_head";

            Name = "棒球帽";
            Defense = 1;
            Missrate = 1f;
            Worth = 38;
            string str = ""; str += "品质：" + ItemLevel.ToString(); str += "\n"; 
            str += "防御：" + Defense.ToString("f1");
            str += "\n";
            str += "闪避："+ Missrate.ToString("f1") + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "很漂亮的帽子。";
            Description = str;
        }
    }
    #endregion
}
