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

            game.SceneManager.OnKeyboardUp += new KeyboardEvent(SceneManager_OnKeyboardUp);
            
            game.Start();
        }

        void SceneManager_OnKeyboardUp(object sender, int key)
        {
            MetalX.Component.SceneManager sm = (MetalX.Component.SceneManager)sender;
            Key k = (Key)key;
            if (k == Key.J)
            {
                if (sm.npc != null)
                {
                    if (sm.me.CanControl == false)
                    {
                        return;
                    }
                    game.AppendScript("freezeme");
                    sm.npc.FocusOnMe(sm.me);
                    if (sm.npc.IsBox)
                    {
                        if (sm.npc.Bag.Count > 0)
                        {
                            game.AppendScript(sm.npc.Code);
                            game.AppendScript("unfreezeme");
                            game.ExecuteScript();
                        }
                        else
                        {
                            game.AppendScript("npc say 什么都没有");
                            game.AppendScript("untilpress j");
                            game.AppendScript("gui fallout 500\ndelay 500");
                            game.AppendScript("gui close NPCsay");
                            game.AppendScript("gui fallin 0");
                            game.AppendScript("unfreezeme");
                            game.ExecuteScript();
                        }
                    }
                    else
                    {
                        if (sm.npc.Code == null)
                        {
                            game.AppendScript("npc say " + sm.npc.DialogText);
                            game.AppendScript("untilpress j");
                            game.AppendScript("unfreezeme");
                            game.ExecuteScript();

                        }
                        else
                        {
                            game.AppendScript(sm.npc.Code);
                            game.AppendScript("unfreezeme");
                            game.ExecuteScript();
                        }
                    }
                }
                //if (sm.IsNobody() == false)
                //{
                //    if (sm.me.IsTalking)
                //    {
                //        sm.npc.FocusOnMe(sm.me);
                //        game.AppendAndExecuteScript("npc say " + sm.npc.DialogText);
                //    }
                //    else
                //    {
                //        sm.npc.RecoverDirection();
                //        game.AppendAndExecuteScript("gui close NPCTalk");
                //    }
                //}
            }
            //else if (k == Key.K)
            //{
            //    if (sm.npc != null)
            //    {
            //        game.AppendAndExecuteScript("unfreezeme");
            //        sm.npc.RecoverDirection();
            //        game.AppendAndExecuteScript("gui close NPCTalk");
            //    }
            //}
        }

        [STAThread]
        static void Main()
        {
            new MetalHunter();
        }
    }
}
