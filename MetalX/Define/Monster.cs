using System;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.DirectX;
using MetalX.File;
namespace MetalX.Define
{
    public enum BattleState
    {
        Stand = 0,      //loop

        Run = 1,        //op
        Weapon = 2,     //op
        Item = 3,       //op
        Fight = 4,      //op
        Block = 5,      //op

        Miss = 6,       //hit
        Hit = 7,        //hit
    }
    [Serializable]
    public class Monster : CHR
    {
        public Monster()
            : base()
        {
        }

        public List<string> IteamNames = new List<string>();

        public string ScriptInit="";
        public string ScriptAI="";

        //public BattleState AI()
        //{
        //    BattleState bs;
        //    int seed = Util.Roll(4);
        //    seed++;
        //    bs = (BattleState)seed;
        //    return bs;
        //}
    }
}
