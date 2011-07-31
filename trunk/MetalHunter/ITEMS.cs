using System;
using System.Collections.Generic;
using System.Text;
using MetalX.Data;
namespace MetalHunter
{
    public static class ITEMS
    {
        static ITEMS()
        {
            弹弓 = new EquipmentCHR();
            弹弓.Name = "弹弓";
            弹弓.Damage = 10;
            弹弓.Accurate = 0.5f;
            弹弓.Description = "谁都能制作出来的建议弹弓";
        }
        public static EquipmentCHR 弹弓;
    }
}
