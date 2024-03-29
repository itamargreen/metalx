﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

using Microsoft.DirectX.DirectInput;

using MetalX.Define;

namespace MetalX.Component
{
    public class KeyboardManager : GameCom
    {
        //Thread keyboardthd;
        bool[] keyboardStateBackup = new bool[256];
        public KeyboardManager(Game g)
            : base(g)
        {
            DisableAll();
            Enable = true;
            //keyboardthd = new Thread(keyboardHandle);
            //keyboardthd.IsBackground = true;
            //keyboardthd.Start();
        }
        public override void Code()
        {
            KeyboardState keyboardState = game.Devices.DKeyboardDev.GetCurrentKeyboardState();
            for (int i = 0; i < keyboardStateBackup.Length; i++)
            {
                if (keyboardState[(Key)i])
                {
                    if (keyboardStateBackup[i])
                    {
                        foreach (GameCom gc in game.GameComs)
                        {
                            if (gc.Controllable)
                                gc.SetKeyboardEvent(i, KeyState.DownHold);
                        }
                    }
                    else
                    {
                        foreach (GameCom gc in game.GameComs)
                        {
                            if (gc.Controllable)
                                gc.SetKeyboardEvent(i, KeyState.Down);
                            //gc.SetKeyboardEvent(i, KeyState.DownHold);
                            
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
                            if (gc.Controllable)
                                gc.SetKeyboardEvent(i, KeyState.Up);
                        }
                    }
                    keyboardStateBackup[i] = false;
                }
            }
        }
    }
}
