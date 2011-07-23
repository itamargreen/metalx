using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

using MetalX;
using MetalHunter.GUI;

namespace MetalHunter
{
    class MetalHunter
    {
        Game game;

        void InitFormBoxes()
        {
            game.FormBoxes.LoadDotMXFormBox(new LogoEngine(game));
            game.FormBoxes.LoadDotMXFormBox(new LogoGame(game));
            game.FormBoxes.LoadDotMXFormBox(new MenuLoad(game));
        }

        public MetalHunter()
        {
            game = new Game("MetalHunter");

            game.InitData();
            game.InitCom();

            game.LoadAllDotPNG( new Size(16, 16));
            game.LoadAllDotMP3();

            game.LoadAllDotMXScene();

            InitFormBoxes();
            
            game.ExecuteMetalXScript("logo.mxscript");

            game.Start();
        }

        [STAThread]
        static void Main()
        {
            new MetalHunter();
        }
    }
}
