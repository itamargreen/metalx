using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

using Microsoft.DirectX.DirectInput;

using MetalX;

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

            game.OverLoadMessageBox(new MH_MSGBox(game));
            game.OverLoadASKboolBox(new MH_ASKboolBox(game));
        }

        public MetalHunter()
        {
            game = new Game("MetalHunter");

            game.InitData();
            game.InitCom();

            game.LoadAllDotPNG(new Size(16, 16));
            game.LoadAllDotMP3();

            game.LoadAllDotMXScene();
            game.LoadAllDotMXNPC();
            game.LoadAllDotMXScript();

            InitFormBoxes();
            
            game.AppendDotMetalXScript("logo");
            game.ExecuteScript();

            game.Start();
        }

        [STAThread]
        static void Main()
        {
            new MetalHunter();
        }
    }
}
