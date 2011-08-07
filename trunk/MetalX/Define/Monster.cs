using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.DirectX;
using MetalX.File;
namespace MetalX.Define
{
    public enum BattleState
    {
        Stand = 0,
        Hit = 1,
        Defense = 2,
        Fight = 3,
        Fire = 4,
        Throw = 5,
    }
    [Serializable]
    public class Monster : CHR
    {
        public Monster()
            : base()
        {
        }

        public List<string> IteamNames = new List<string>();

        public BattleState AI()
        {
            BattleState bs;
            int seed = Util.Roll(2, 5);
            bs = (BattleState)seed;
            return bs;
        }
    }
}
