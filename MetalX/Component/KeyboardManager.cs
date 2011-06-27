using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

using Microsoft.DirectX.DirectInput;

namespace MetalX.Component
{
    public class KeyboardManager : GameCom
    {
        //Thread keyboardthd;
        bool[] keyboardStateBackup = new bool[256];
        public KeyboardManager(Game g)
            : base(g)
        {
            //keyboardthd = new Thread(keyboardHandle);
            //keyboardthd.IsBackground = true;
            //keyboardthd.Start();
        }
        public override void Code()
        {
            KeyboardState keyboardState = game.Devices.DKeyboardDev.GetCurrentKeyboardState();
            for (int i = 0; i < 256; i++)
            {
                if (keyboardState[(Key)i])
                {
                    if (keyboardStateBackup[i])
                    {
                        foreach (GameCom gc in game.GameComs)
                        {
                            gc.SetKeyboardEvent(i, KeyState.DownHold);
                        }
                    }
                    else
                    {
                        foreach (GameCom gc in game.GameComs)
                        {
                            gc.SetKeyboardEvent(i, KeyState.Down);
                            gc.SetKeyboardEvent(i, KeyState.DownHold);
                            if ((Key)i == Key.Escape)
                            {
                                game.Exit();
                            }
                            else if ((Key)i == Key.F1)
                            {
                                game.SaveCheckPoint(1);
                            }
                            else if ((Key)i == Key.F2)
                            {
                                game.SaveCheckPoint(2);
                            }
                            else if ((Key)i == Key.F3)
                            {
                                game.SaveCheckPoint(3);
                            }
                            else if ((Key)i == Key.F4)
                            {
                                game.SaveCheckPoint(4);
                            }
                            else if ((Key)i == Key.F5)
                            {
                                game.LoadCheckPoint(1);
                            }
                            else if ((Key)i == Key.F6)
                            {
                                game.LoadCheckPoint(2);
                            }
                            else if ((Key)i == Key.F7)
                            {
                                game.LoadCheckPoint(3);
                            }
                            else if ((Key)i == Key.F8)
                            {
                                game.LoadCheckPoint(4);
                            }
                            else if ((Key)i == Key.O)
                            {
                                ShockScreen(1000);
                            }
                            else if ((Key)i == Key.U)
                            {
                                FallOutSceen(1000);
                            }
                            else if ((Key)i == Key.I)
                            {
                                FallInSceen(1000);
                            }

                        }
                    }
                    keyboardStateBackup[i] = true;
                }
                else
                {
                    if (keyboardStateBackup[i])
                    {
                        foreach (GameCom gc in game.GameComs)
                        {
                            gc.SetKeyboardEvent(i, KeyState.Up);
                        }
                    }
                    keyboardStateBackup[i] = false;
                }
            }            
        }
    }
}
