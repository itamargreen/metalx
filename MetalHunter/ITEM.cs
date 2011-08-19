using System;
using System.Collections.Generic;
using System.Text;

using MetalX.Define;

namespace MetalHunter
{
    #region 道具
    public class 恢复胶囊小 : Item
    {
        public 恢复胶囊小()
        {
            ItemType = ItemType.Supply;

            Icon.Name = "icon_supplychr";

            Name = "恢复胶囊小";
            Worth = 30;

            string str = "";
            str += "品质：" + Quality.ToString();
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "能恢复ＨＰ的胶囊。";
            Description = str;

            Script = "pc 0 hp 100\nmsg ＨＰ恢复了１００\nuntilpress y n\nmsg";
        }
    }
    public class 火箭礼花 : BattleItem
    {
        public 火箭礼花():base()
        {
            //ItemType = ItemType.Battle;

            Icon.Name = "icon_battle";

            Name = "火箭礼花";
            Worth = 20;
            damage = 100;
            accurate = 85;

            string str = "";
            str += "品质：" + Quality.ToString();
            str += "\n";
            str += "攻击：" + Damage.ToString("f1");
            str += "\n";
            str += "命中：" + Accurate.ToString("f1") + "%";
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "没准能有攻击力。";
            Description = str;

            Script = "";

            ShotMovieIndexer.Name = "火箭礼花_shot";
            FlyMovieIndexer.Name = "火箭礼花_fly";
            HitMovieIndexer.Name = "boom_small";

            FlyTime = 400;
        }
    }
    #endregion
}
