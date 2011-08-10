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
            str += "品质：" + ItemLevel.ToString();
            str += "\n";
            str += "价值：" + Worth + "Ｇ";
            str += "\n";
            str += "\n";
            str += "能恢复ＨＰ的胶囊。";
            Description = str;

            Script = "pc 0 hp 100\nmsg ＨＰ恢复了１００\nuntilpress y n\nmsg";
        }
    }
    public class 爆竹 : Item
    {
        public 爆竹()
        {
            ItemType = ItemType.Battle;

            Icon.Name = "icon_battle";

            Name = "爆竹";
            Worth = 20;

            string str = "";
            str += "品质：" + ItemLevel.ToString();
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
