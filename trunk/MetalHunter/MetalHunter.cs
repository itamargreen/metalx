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
            game.FormBoxes.LoadDotMXFormBox(new MenuCHR(game));

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

            game.FormBoxManager.OnKeyboardUp += new KeyboardEvent(FormBoxManager_OnKeyboardUp);
            game.SceneManager.OnKeyboardUp += new KeyboardEvent(SceneManager_OnKeyboardUp);

            game.Start();
        }

        void FormBoxManager_OnKeyboardUp(object sender, int key)
        {
            Key k = (Key)key;
            if (k == Key.C)
            {
                if (game.FormBoxes["MenuCHR"].Visible)
                {
                    game.FormBoxManager.Disappear("MenuCHR");
                    game.SceneManager.ME.Unfreeze();
                }
                else
                {
                    game.FormBoxManager.Appear("MenuCHR");
                    game.SceneManager.ME.Freeze();
                }
            }
        }

        void SceneManager_OnKeyboardUp(object sender, int key)
        {
            //throw new NotImplementedException();
            
        }

        [STAThread]
        static void Main()
        {
            new MetalHunter();
        }
    }
}
