using System;
using System.Collections.Generic;
using System.Windows.Forms;

using MetalX;
using MetalX.Data;

namespace MetalHunter
{
    class MetalHunter
    {
        Game game;

        public MetalHunter()
        {
            game = new Game("MetalHunter");
            game.Init();
            game.Start();
        }

        static void Main()
        {
            MetalHunter mh = new MetalHunter();
        }
    }
}
