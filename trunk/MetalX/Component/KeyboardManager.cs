using System;
using System.Collections.ObjectModel;
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
