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
            game.FormBoxes.LoadDotMXFormBox(new MenuBAG(game));

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
                    game.SceneManager.ME.CanControl = true;
                }
                else
                {
                    ((MenuCHR)game.FormBoxes["MenuCHR"]).LoadContext(game.SceneManager.ME);
                    game.FormBoxManager.Appear("MenuCHR");
                    game.SceneManager.ME.CanControl = false;
                }
            } 
            else if (k == Key.B)
            {
                if (game.FormBoxes["MenuBAG"].Visible)
                {
                    game.FormBoxManager.Disappear("MenuBAG");
                    game.SceneManager.ME.CanControl = true;
                }
                else
                {
                    ((MenuBAG)game.FormBoxes["MenuBAG"]).LoadBag(game.SceneManager.ME);
                    game.FormBoxManager.Appear("MenuBAG");
                    game.SceneManager.ME.CanControl = false;
                }
            }
            else if (k == Key.A)
            {
                game.SceneManager.ME.BagIn(ITEMS.弹弓);
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
