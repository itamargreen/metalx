using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.DirectX.DirectInput;

using MetalX.Data;

namespace MetalX.Component
{
    public class ScriptManager : GameCom
    {
        Stack<string> cmdbak = new Stack<string>();
        string text = "";
        bool drawText = false;
        bool isBig = false; 
        string cur;
        string curcmd;
        string presskey = "";

        //DateTime delayStartTime = DateTime.Now;
        //double delayTime = 0;
        //double delayLeftTime = 0;

        bool exe = false; Queue<string> commands = new Queue<string>();
        bool loop = false;
                
        //TimeSpan delayEclipseTimeSpan
        //{
        //    get
        //    {
        //        return DateTime.Now - delayStartTime;
        //    }
        //}      
        
        public ScriptManager(Game g)
            : base(g)
        {            
        }
        
        public override void Code()
        {
            //if (delayLeftTime > 0)
            //{
            //    delayLeftTime = delayTime - delayEclipseTimeSpan.TotalMilliseconds;
            //}
            //else
            if (loop)
            {
                if (curcmd != null)
                {
                    execute(curcmd);
                }
                else
                {
                    loop = false;
                }
            }
            else
            {
                if (exe)
                {
                    if (commands.Count > 0)
                    {
                        curcmd = commands.Dequeue();
                        presskey = "";
                        execute(curcmd);
                    }
                    else
                    {
                        exe = false;
                    }
                }
            }

        }
        public override void Draw()
        {
            if (drawText)
            {
                if (DateTime.Now.Millisecond < 500)
                {
                    cur = "_";
                }
                else
                {
                    cur = "";
                }
                game.DrawText(text + cur, new System.Drawing.Point(), ColorFilter);
            }
            game.DrawText("FPS: " + game.AverageFPS.ToString("f1"), new System.Drawing.Point(550, 0), ColorFilter);
        }

        void execute(string cmd)
        {
            //cmd = cmd.ToLower();
            string[] kw = cmd.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (kw.Length == 0)
            {
                return;
            }
            kw[0] = kw[0].ToLower();
            if (kw.Length == 1)
            {
                if (kw[0] == "exit")
                {
                    game.Exit();
                }
                else if (kw[0] == "clr")
                {
                    text = "";
                }
                else if (kw[0] == "fullscreen")
                {
                    game.ToggleToFullScreen();
                }
                else if (kw[0] == "comctrl")
                {
                    game.SceneManager.Controllable = false;
                    game.FormBoxManager.Controllable = false;
                }
                else if (kw[0] == "userctrl")
                {
                    game.SceneManager.Controllable = true;
                    game.FormBoxManager.Controllable = true;
                }
                else if (kw[0] == "freezeme")
                {
                    game.SceneManager.me.Freeze();
                }
                else if (kw[0] == "unfreezeme")
                {
                    game.SceneManager.me.Unfreeze();
                }
            }
            else if (kw.Length == 2)
            {
                if (kw[0] == "delay")
                {
                    double ms = double.Parse(kw[1]);
                    //delayLeftTime = delayTime = ms;
                    //delayStartTime = DateTime.Now;
                    Delay((int)ms);
                }
                else if (kw[0] == "mp3")
                {
                    game.PlayMP3(2, kw[1]);
                }
                else if (kw[0] == "script")
                {
                    game.ExecuteMetalXScript(kw[1] + ".mxscript");
                }
                else if (kw[0] == "msg")
                {
                    TextBox tb = new TextBox(game);
                    tb.Text = kw[1];
                    game.FormBoxManager.Appear("MessageBox", tb);
                }
                else if (kw[0] == "untilstop")
                {
                    Scene s = game.SceneManager.scene;
                    float a = s.GetNPC(kw[1]).NeedMovePixel;
                    if (a > 0)
                    {
                        loop = true;
                    }
                    else
                    {
                        loop = false;
                    }
                }
                else if (kw[0] == "untilpress")
                {
                    if (presskey == kw[1])
                    {
                        loop = false;
                    }
                    else
                    {
                        loop = true;
                    }                    
                }
            }
            else if (kw.Length == 3)
            {
                if (kw[0] == "scene")
                {
                    double ms = double.Parse(kw[2]);
                    if (kw[1] == "shock")
                    {
                        game.SceneManager.ShockScreen(ms);
                    }
                    else if (kw[1] == "fallout")
                    {
                        game.SceneManager.FallOutSceen(ms);
                    }
                    else if (kw[1] == "fallin")
                    {
                        game.SceneManager.FallInSceen(ms);
                    }
                }
                else if (kw[0] == "gui")
                {

                    if (kw[1] == "shock")
                    {
                        double ms = double.Parse(kw[2]);
                        game.FormBoxManager.ShockScreen(ms);
                    }
                    else if (kw[1] == "fallout")
                    {
                        double ms = double.Parse(kw[2]);
                        game.FormBoxManager.FallOutSceen(ms);
                    }
                    else if (kw[1] == "fallin")
                    {
                        double ms = double.Parse(kw[2]);
                        game.FormBoxManager.FallInSceen(ms);
                    }
                    else if (kw[1] == "close")
                    {
                        if (kw[2] == "all")
                        {
                            game.FormBoxManager.DisappearAll();
                        }
                        else
                        {
                            game.FormBoxManager.Disappear(kw[2]);
                        }
                    }
                    else if (kw[1] == "appear")
                    {
                        game.FormBoxManager.Appear(kw[2]);
                    }
                    else if (kw[1] == "disappear")
                    {
                        game.FormBoxManager.Disappear(kw[2]);
                    }
                }
                else if (kw[0] == "me")
                {
                    if (kw[1] == "skin")
                    {
                        game.SceneManager.MeSkin(kw[2]);
                    }
                    else if (kw[1] == "gold")
                    {
                        game.SceneManager.me.Gold += int.Parse(kw[2]);
                    }
                    else if (kw[1] == "bagin")
                    {
                        //查找item，然后添加
                        game.SceneManager.me.BagIn(null);
                    }
                }
                else if (kw[0] == "npc")
                {
                    if (kw[1] == "say")
                    {
                        TextBox tb = new TextBox(game);
                        tb.Text = kw[2];
                        game.FormBoxManager.Appear("NPCsay", tb);
                    }
                }
                else if (kw[0] == "move")
                {
                    Direction dir;
                    if (kw[2] == "u")
                    {
                        dir = Direction.U;
                    }
                    else if (kw[2] == "l")
                    {
                        dir = Direction.L;
                    }
                    else if (kw[2] == "d")
                    {
                        dir = Direction.D;
                    }
                    else
                    {
                        dir = Direction.R;
                    }
                    Scene s = game.SceneManager.scene;
                    game.SceneManager.CHRMove(s, s.GetNPC(kw[1]), dir, 1);
                }

            }
            else if (kw.Length == 4)
            {
                if (kw[0] == "scene")
                {
                    double ms = double.Parse(kw[2]);
                    int range = int.Parse(kw[3]);
                    if (kw[1] == "shock")
                    {
                        game.SceneManager.ShockScreen(ms, range);
                    }
                }

                else if (kw[0] == "me")
                {
                    if (kw[1] == "jump")
                    {
                        Microsoft.DirectX.Vector3 v3 = new Microsoft.DirectX.Vector3();
                        v3.X = float.Parse(kw[2]);
                        v3.Y = float.Parse(kw[3]);
                        game.SceneManager.SceneJump(v3);
                    }

                }
                else if (kw[0] == "move")
                {
                    Direction dir;
                    if (kw[2] == "u")
                    {
                        dir = Direction.U;
                    }
                    else if (kw[2] == "l")
                    {
                        dir = Direction.L;
                    }
                    else if (kw[2] == "d")
                    {
                        dir = Direction.D;
                    }
                    else
                    {
                        dir = Direction.R;
                    }
                    Scene s = game.SceneManager.scene;
                    int stp = int.Parse(kw[3]);
                    game.SceneManager.CHRMove(s, s.GetNPC(kw[1]), dir, stp);
                }
            }
            else if (kw.Length == 5)
            {
                if (kw[0] == "scene")
                {
                    if (kw[1] == "jump")
                    {
                        Microsoft.DirectX.Vector3 v3 = new Microsoft.DirectX.Vector3();
                        v3.X = float.Parse(kw[3]);
                        v3.Y = float.Parse(kw[4]);
                        game.SceneManager.Enter(kw[2], v3);
                    }
                }
            }
        }

        public void AppendCommand(string cmd)
        {
            string[] cmds = cmd.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string c in cmds)
            {
                try
                {
                    if (c.Substring(0, 2) != "//")
                    {
                        
                        commands.Enqueue(c);
                    }
                }
                catch { }
            }
        }
        public void Execute()
        {
            exe = true;
        }        
        public void Execute(string cmd)
        {
            AppendCommand(cmd);
            Execute();
        }
        public void ExecuteDotMXScript(string fileName)
        {
            string cmds = System.IO.File.ReadAllText(game.Options.RootPath + fileName);
            text += cmds + "\n";
            Execute(cmds);
        }

        public override void OnKeyboardUpCode(object sender, int key)
        {
            Key k = (Key)key;
            presskey = k.ToString().ToLower();
            if (k == Key.LeftShift || k == Key.RightControl)
            {
                isBig = false;
            }
        }
        public override void OnKeyboardDownCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == Key.LeftShift || k == Key.RightShift || k == Key.LeftControl || k == Key.RightControl)
            {
                isBig = true;
            }
            if (k== Key.Escape)
            {
                game.Exit();
            }
            //else if (k== Key.F1)
            //{
            //    game.LoadCheckPoint(1);
            //}
            //else if (k== Key.F2)
            //{
            //    game.LoadCheckPoint(2);
            //}
            //else if (k== Key.F3)
            //{
            //    game.LoadCheckPoint(3);
            //}
            //else if (k== Key.F4)
            //{
            //    game.LoadCheckPoint(4);
            //}
            //else if (k== Key.F5)
            //{
            //    game.SaveCheckPoint(1);
            //}
            //else if (k== Key.F6)
            //{
            //    game.SaveCheckPoint(2);
            //}
            //else if (k== Key.F7)
            //{
            //    game.SaveCheckPoint(3);
            //}
            //else if (k== Key.F8)
            //{
            //    game.SaveCheckPoint(4);
            //}
            else if (k == Key.F1)
            {
                game.Options.UVOffsetX += 0.1f;
            }
            else if (k == Key.F2)
            {
                game.Options.UVOffsetX -= 0.1f;
            }
            else if (k == Key.F3)
            {
                game.Options.UVOffsetY += 0.1f;
            }
            else if (k == Key.F4)
            {
                game.Options.UVOffsetY -= 0.1f;
            }
            else if (k == Key.F9)
            {
                game.Options.TextureDrawMode = TextureDrawMode.Direct3D;
            }
            else if (k == Key.F10)
            {
                game.Options.TextureDrawMode = TextureDrawMode.Direct2D;
            }
            else if (k == Key.F11)
            {
                //game.Options.TextureDrawMode = TextureDrawMode.Direct3D;
            }
            else if (k == Key.F12)
            {
                if (drawText)
                {
                    drawText = false;
                    game.SceneManager.Controllable = true;
                }
                else
                {
                    drawText = true;
                    game.SceneManager.Controllable = false;
                }
            }
            else if (k == Key.Return)
            {
                string[] cmds = text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (cmds.Length > 0)
                {
                    string cmd = cmds[cmds.Length - 1];
                    cmdbak.Push(cmd);
                    if (isBig)
                    {
                        Execute(cmd);
                    }
                    else
                    {
                        AppendCommand(cmd);
                        text += "\n";
                    }
                }
            }
            else if (k == Key.Space)
            {
                text += " ";
            }
            else if (k == Key.Up)
            {
                if (cmdbak.Count > 0)
                {
                    text += cmdbak.Pop();
                }
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
                if (!drawText)
                {
                    
                }
                else
                {
                    string ks = "";
                    #region convert key
                    //if (k == Key.A)
                    //{
                    //    ks = "a";
                    //}
                    //else if (k == Key.B)
                    //{
                    //    ks = "b";
                    //}
                    //else if (k == Key.C)
                    //{
                    //    ks = "c";
                    //}
                    //else if (k == Key.D)
                    //{
                    //    ks = "d";
                    //}
                    //else if (k == Key.E)
                    //{
                    //    ks = "e";
                    //}
                    //else if (k == Key.F)
                    //{
                    //    ks = "f";
                    //}
                    //else if (k == Key.G)
                    //{
                    //    ks = "g";
                    //}
                    //else if (k == Key.H)
                    //{
                    //    ks = "h";
                    //}
                    //else if (k == Key.I)
                    //{
                    //    ks = "i";
                    //}
                    //else if (k == Key.J)
                    //{
                    //    ks = "j";
                    //}
                    //else if (k == Key.K)
                    //{
                    //    ks = "k";
                    //}
                    //else if (k == Key.L)
                    //{
                    //    ks = "l";
                    //}
                    //else if (k == Key.M)
                    //{
                    //    ks = "m";
                    //}
                    //else if (k == Key.N)
                    //{
                    //    ks = "n";
                    //}
                    //else if (k == Key.O)
                    //{
                    //    ks = "o";
                    //}
                    //else if (k == Key.P)
                    //{
                    //    ks = "p";
                    //}
                    //else if (k == Key.Q)
                    //{
                    //    ks = "q";
                    //}
                    //else if (k == Key.R)
                    //{
                    //    ks = "r";
                    //}
                    //else if (k == Key.S)
                    //{
                    //    ks = "s";
                    //}
                    //else if (k == Key.T)
                    //{
                    //    ks = "t";
                    //}
                    //else if (k == Key.U)
                    //{
                    //    ks = "u";
                    //}
                    //else if (k == Key.V)
                    //{
                    //    ks = "v";
                    //}
                    //else if (k == Key.W)
                    //{
                    //    ks = "w";
                    //}
                    //else if (k == Key.X)
                    //{
                    //    ks = "x";
                    //}
                    //else if (k == Key.Y)
                    //{
                    //    ks = "y";
                    //}
                    //else if (k == Key.Z)
                    //{
                    //    ks = "z";
                    //}
                    //else if (k == Key.D0 || k == Key.NumPad0)
                    //{
                    //    ks = "0";
                    //}
                    //else if (k == Key.D1 || k == Key.NumPad1)
                    //{
                    //    ks = "1";
                    //}
                    //else if (k == Key.D2 || k == Key.NumPad2)
                    //{
                    //    ks = "2";
                    //}
                    //else if (k == Key.D3 || k == Key.NumPad3)
                    //{
                    //    ks = "3";
                    //}
                    //else if (k == Key.D4 || k == Key.NumPad4)
                    //{
                    //    ks = "4";
                    //}
                    //else if (k == Key.D5 || k == Key.NumPad5)
                    //{
                    //    ks = "5";
                    //}
                    //else if (k == Key.D6 || k == Key.NumPad6)
                    //{
                    //    ks = "6";
                    //}
                    //else if (k == Key.D7 || k == Key.NumPad7)
                    //{
                    //    ks = "7";
                    //}
                    //else if (k == Key.D8 || k == Key.NumPad8)
                    //{
                    //    ks = "8";
                    //}
                    //else if (k == Key.D9 || k == Key.NumPad9)
                    //{
                    //    ks = "9";
                    //}
                    //else if (k == Key.Minus)
                    //{
                    //    ks = "-";
                    //}
                    #endregion
                    //else
                    //{
                    ks = k.ToString();
                    //}
                    if (!isBig)
                    {
                        ks = ks.ToLower();
                    }
                    text += ks;
                }
            }
        }
    }
}
