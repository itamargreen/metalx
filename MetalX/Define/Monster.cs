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
        Defense = 1,
        Hit = 2,
        Fight = 3,
        Fire = 4,
        Throw = 5,
    }
    [Serializable]
    public class Monster : CHR
    {


        public Monster()
        {
            for (int i = 0; i < 8; i++)
            {
                BattleMovieIndexers.Add(new MemoryIndexer());
                BattleMovies.Add(new MetalXMovie());
            }
        }
    }
}
