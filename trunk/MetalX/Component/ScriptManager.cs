using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.DirectX.DirectInput;

namespace MetalX.Component
{
    public class ScriptManager : GameCom
    {
        Queue<string> commands = new Queue<string>();
        public ScriptManager(Game g)
            : base(g)
        {
        }

        DateTime delayStartTime = DateTime.Now;
        TimeSpan delayEclipseTimeSpan
        {
            get
            {
                return DateTime.Now - delayStartTime;
            }
        }
        public override void Code()
        {
            if (delayLeftTime > 0)
            {
                delayLeftTime = delayTime - delayEclipseTimeSpan.TotalMilliseconds;
            }
            else
            {
                if (commands.Count > 0)
                {
                    execute(commands.Dequeue());
                }
            }
        }
        public override void Draw()
        {
            if (drawText)
                game.DrawText(text, new System.Drawing.Point(), ColorFilter);
            //game.DrawText("fps: " + game.AverageFPS + "\ndelay left time: " + delayLeftTime, new System.Drawing.Point(), ColorFilter);
        }
        double delayTime = 0;
        double delayLeftTime = 0;

        void execute(string cmd)
        {
            //cmd = cmd.ToLower();
            string[] kw = cmd.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            kw[0] = kw[0].ToLower();
            if (kw.Length == 1)
            {
                if (kw[0] == "exit")
                {
                    game.Exit();
                }
            }
            else if (kw.Length == 2)
            {
                if (kw[0] == "delay")
                {
                    double ms = double.Parse(kw[1]);
                    delayLeftTime = delayTime = ms;
                    delayStartTime = DateTime.Now;
                }
                else if (kw[0] == "appear")
                {
                    game.FormBoxManager.Appear(kw[1]);
                }
                else if (kw[0] == "disappear")
                {
                    game.FormBoxManager.Disappear(kw[1]);
                }
                else if (kw[0] == "mp3")
                {
                    game.PlayMP3(kw[1]);
                }
            }
            else if (kw.Length == 3)
            {
                if (kw[0] == "shock")
                {
                    double ms = double.Parse(kw[2]);
                    if (kw[1] == "scene")
                    {
                        game.SceneManager.ShockScreen(ms);
                    }
                    else if (kw[1] == "ui")
                    {
                        game.FormBoxManager.ShockScreen(ms);
                    }
                }
                else if (kw[0] == "fallout")
                {
                    double ms = double.Parse(kw[2]);
                    if (kw[1] == "scene")
                    {
                        game.SceneManager.FallOutSceen(ms);
                    }
                    else if (kw[1] == "ui")
                    {
                        game.FormBoxManager.FallOutSceen(ms);
                    }
                }
                else if (kw[0] == "fallin")
                {
                    double ms = double.Parse(kw[2]);
                    if (kw[1] == "scene")
                    {
                        game.SceneManager.FallInSceen(ms);
                    }
                    else if (kw[1] == "ui")
                    {
                        game.FormBoxManager.FallInSceen(ms);
                    }
                }

            }
            else if (kw.Length == 4)
            {
                if (kw[0] == "shock")
                {
                    double ms = double.Parse(kw[2]);
                    int range = int.Parse(kw[3]);
                    if (kw[1] == "scene")
                    {
                        game.SceneManager.ShockScreen(ms, range);
                    }
                    else if (kw[1] == "ui")
                    {
                        game.FormBoxManager.ShockScreen(ms, range);
                    }
                }
            }
        }
        public void Execute(string cmd)
        {
            string[] cmds = cmd.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string c in cmds)
            {
                if (c.Substring(0, 2) != "//")
                {
                    commands.Enqueue(c);
                }
            }
        }
        public void ExecuteDotMXScript(string fileName)
        {
            Execute(System.IO.File.ReadAllText(fileName + ".mxscript"));
        }

        string text = "";
        bool drawText = false;
        bool isBig = false;

        public override void OnKeyboardUpCode(int key)
        {
            Key k = (Key)key;
            if (k == Key.LeftShift)
            {
                isBig = false;
            }
        }
        public override void OnKeyboardDownCode(int key)
        {
            Key k = (Key)key;
            //if (k == Key.CapsLock)
            //{
            //    if (isBig)
            //    {
            //        isBig = false;
            //    }
            //    else
            //    {
            //        isBig = true;
            //    }
            //}
            if (k == Key.LeftShift)
            {
                isBig = true;
            }
            if (k == Key.F12)
            {
                if (drawText)
                {
                    drawText = false;
                }
                else
                {
                    drawText = true;
                }
            }
            else if (k == Key.Return)
            {
                try
                {
                    string[] cmds = text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    string cmd = cmds[cmds.Length - 1];
                    execute(cmd);
                    text += "\n";
                }
                catch
                { }
            }
            else if (k == Key.Space)
            {
                text += " ";
            }
            else if (k == Key.BackSpace)
            {
                if (text != string.Empty)
                {
                    text = text.Remove(text.Length - 1);
                }
            }
            else
            {
                if (drawText)
                {
                    string ks = "";
                    if (k == Key.A)
                    {
                        ks = "a";
                    }
                    else if (k == Key.B)
                    {
                        ks = "b";
                    }
                    else if (k == Key.C)
                    {
                        ks = "c";
                    }
                    else if (k == Key.D)
                    {
                        ks = "d";
                    }
                    else if (k == Key.E)
                    {
                        ks = "e";
                    }
                    else if (k == Key.F)
                    {
                        ks = "f";
                    }
                    else if (k == Key.G)
                    {
                        ks = "g";
                    }
                    else if (k == Key.H)
                    {
                        ks = "h";
                    }
                    else if (k == Key.I)
                    {
                        ks = "i";
                    }
                    else if (k == Key.J)
                    {
                        ks = "j";
                    }
                    else if (k == Key.K)
                    {
                        ks = "k";
                    }
                    else if (k == Key.L)
                    {
                        ks = "l";
                    }
                    else if (k == Key.M)
                    {
                        ks = "m";
                    }
                    else if (k == Key.N)
                    {
                        ks = "n";
                    }
                    else if (k == Key.O)
                    {
                        ks = "o";
                    }
                    else if (k == Key.P)
                    {
                        ks = "p";
                    }
                    else if (k == Key.Q)
                    {
                        ks = "q";
                    }
                    else if (k == Key.R)
                    {
                        ks = "r";
                    }
                    else if (k == Key.S)
                    {
                        ks = "s";
                    }
                    else if (k == Key.T)
                    {
                        ks = "t";
                    }
                    else if (k == Key.U)
                    {
                        ks = "u";
                    }
                    else if (k == Key.V)
                    {
                        ks = "v";
                    }
                    else if (k == Key.W)
                    {
                        ks = "w";
                    }
                    else if (k == Key.X)
                    {
                        ks = "x";
                    }
                    else if (k == Key.Y)
                    {
                        ks = "y";
                    }
                    else if (k == Key.Z)
                    {
                        ks = "z";
                    }
                    //else
                    //{
                    //    ks = k.ToString();
                    //}
                    if (isBig)
                    {
                        ks = ks.ToUpper();
                    }
                    text += ks;
                }
            }
        }
    }
}
