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
            game.FormBoxes.LoadDotMXFormBox(new NPCTalk(game));
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

            game.SceneManager.OnKeyboardDown += new KeyboardEvent(SceneManager_OnKeyboardDown);
            
            game.Start();
        }

        void SceneManager_OnKeyboardDown(object sender, int key)
        {
            MetalX.Component.SceneManager sm = (MetalX.Component.SceneManager)sender;
            Key k = (Key)key;
            if (k == Key.J)
            {
                if (sm.IsNobody() == false)
                {
                    if (sm.me.IsTalking)
                    {
                        sm.npc.TurnToMe(sm.me);
                        game.AppendAndExecuteScript("npc say " + sm.npc.DialogText);
                    }
                    else
                    {
                        sm.npc.RecoverDirection();
                        game.AppendAndExecuteScript("gui close NPCTalk");
                    }
                }
            }
            else if (k == Key.K)
            {
                if (sm.IsNobody() == false)
                {
                    sm.npc.RecoverDirection();
                    game.AppendAndExecuteScript("gui close NPCTalk");
                }
            }

        }

        [STAThread]
        static void Main()
        {
            new MetalHunter();
        }
    }
}
