using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

using Microsoft.DirectX.DirectInput;

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

            NPCTalk npctalk = new NPCTalk(game);
            int i = game.FormBoxes.GetIndex("MessageBox");
            game.FormBoxes[i] = npctalk;
            game.FormBoxes[i].Name = "MessageBox";
        }

        public MetalHunter()
        {
            game = new Game("MetalHunter");

            game.InitData();
            game.InitCom();

            game.LoadAllDotPNG(new Size(16, 16));
            game.LoadAllDotMP3();

            game.LoadAllDotMXScene();

            InitFormBoxes();
            
            game.ExecuteMetalXScript("logo.mxscript");

            game.SceneManager.OnKeyboardUp += new KeyboardEvent(SceneManager_OnKeyboardUp);
            
            game.Start();
        }

        void SceneManager_OnKeyboardUp(object sender, int key)
        {
            MetalX.Component.SceneManager sm = (MetalX.Component.SceneManager)sender;
        }

        [STAThread]
        static void Main()
        {
            new MetalHunter();
        }
    }
}
