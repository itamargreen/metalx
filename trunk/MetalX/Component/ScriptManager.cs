using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX.DirectInput;

using MetalX.Define;

namespace MetalX.Component
{
    public class ScriptManager : GameCom
    {
        public ScriptReturn RETURN = new ScriptReturn();

        public void Return(bool yes)
        {
            RETURN.BOOL = yes;
        }
        public void Return(int i)
        {
            RETURN.INT = i;
        }
        public void Return(string str)
        {
            RETURN.STRING = str;
        }

        Stack<string> cmdbak = new Stack<string>();
        string text = "";
        bool drawText = false;
        bool isBig = false;
        public bool Busy
        {
            get
            {
                if (commands.Count > 0 || inscommands.Count > 0)
                {
                    return true;
                }
                return false;
            }
        }
        string cur;
        string curcmd;
        string presskey = "";
        string[] texts
        {
            get
            {
                string[] strs = (text+cur).Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] tmp = new string[10];
                int j = 0;
                for (int i = strs.Length - 10; i < strs.Length; i++)
                {
                    tmp[j] = strs[i];
                    j++;
                }
                return tmp;
            }
        }

        //DateTime delayStartTime = DateTime.Now;
        //double delayTime = 0;
        //double delayLeftTime = 0;

        bool exe = false; 
        Queue<string> commands = new Queue<string>();
        Queue<string> tmpcommands = new Queue<string>();
        Queue<string> inscommands = new Queue<string>();
        bool block = false;
                
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
            if (block)
            {
                if (curcmd != null)
                {
                    execute(curcmd);
                }
                else
                {
                    block = false;
                }
            }
            else
            {
                if (exe)
                {
                    if (inscommands.Count > 0)
                    {
                        curcmd = inscommands.Dequeue();
                        presskey = "";
                        execute(curcmd);
                    }
                    else
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
                    cur = " ";
                }
                //game.DrawText(text + cur, new System.Drawing.Point(), ColorFilter);
                for (int i = 0; i < texts.Length; i++)
                {
                    game.DrawText(texts[i], new Point(0, i * 20), Color.White);
                }
                
            }
            //game.DrawText("FPS: " + game.AverageFPS.ToString("f1") + "    DrawMode: " + game.Options.TextureDrawMode, new System.Drawing.Point(0, 0), Color.Blue);
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
            #region sys
            if (kw[0] == "bool")
            {
                if (kw[1] == "true" || kw[1] == "t")
                {
                    if (RETURN.BOOL == false)
                    {
                        return;
                    }
                    for (int i = 2; i < kw.Length; i++)
                    {
                        kw[i - 2] = kw[i];
                    }
                }
                else if (kw[1] == "false" || kw[1] == "f")
                {
                    if (RETURN.BOOL)
                    {
                        return;
                    }
                    for (int i = 2; i < kw.Length; i++)
                    {
                        kw[i - 2] = kw[i];
                    }
                }
            }
            else if (kw[0] == "int")
            {
                int n = int.Parse(kw[1]);
                if (RETURN.INT == n)
                {
                    for (int i = 2; i < kw.Length; i++)
                    {
                        kw[i - 2] = kw[i];
                    }
                }
                else
                {
                    return;
                }
            }
            else if (kw[0] == "string")
            {
                if (RETURN.STRING == kw[1])
                {
                    for (int i = 2; i < kw.Length; i++)
                    {
                        kw[i - 2] = kw[i];
                    }
                }
                else
                {
                    return;
                }
            }


            if (kw[0] == "exit")
            {
                game.Exit();
            }
            else if (kw[0] == "roll")
            {
                int l = int.Parse(kw[1]);
                int h = int.Parse(kw[2]);
                int seed = Util.Roll(l, h);
                RETURN.INT = seed;
            }
            else if (kw[0] == "clr")
            {
                text = "";
            }
            else if (kw[0] == "terminal")
            {
                commands.Clear();
                exe = false;
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
            else if (kw[0] == "delay")
            {
                double ms = double.Parse(kw[1]);
                //delayLeftTime = delayTime = ms;
                //delayStartTime = DateTime.Now;
                Delay((int)ms);
            }
            else if (kw[0] == "mp3")
            {
                int l = int.Parse(kw[1]);
                bool loop = false; ;
                try
                {
                    loop = bool.Parse(kw[3]);
                }
                catch
                { }
                if (kw.Length > 2)
                {
                    game.PlayMP3Audio(l, game.AudioFiles[kw[2]].FullName, loop);
                }
                else
                {
                    game.StopAudio(l);
                }
            }
            else if (kw[0] == "vol")
            {
                game.SetVolume(int.Parse(kw[1]), int.Parse(kw[2]));
            }
            else if (kw[0] == "script")
            {
                int c = commands.Count;
                for (int i = 0; i < c; i++)
                {
                    tmpcommands.Enqueue(commands.Dequeue());
                }
                AppendDotMetalXScript(kw[1]);
                for (int i = 0; i < c; i++)
                {
                    commands.Enqueue(tmpcommands.Dequeue());
                }
            }
            else if (kw[0] == "untilstop")
            {
                double a;
                if (kw[1] == "me")
                {
                    a = game.ME.NeedMovePixel;
                }
                else
                {
                    a = game.GetNPC(kw[1]).NeedMovePixel;

                }
                if (a > 0)
                {
                    block = true;
                }
                else
                {
                    block = false;
                }
            }
            else if (kw[0] == "untilpress")
            {
                block = true;
                string key = kw[1];
                if (key == "yes" || key == "y")
                {
                    key = game.Options.KeyYES.ToString();
                }
                else if (key == "no" || key == "n")
                {
                    key = game.Options.KeyNO.ToString();
                }

                key = key.ToLower();
                if (presskey == key)
                {
                    block = false;
                }

                try
                {
                    string key2 = kw[2];
                    if (key2 == "yes" || key2 == "y")
                    {
                        key2 = game.Options.KeyYES.ToString();
                    }
                    else if (key2 == "no" || key2 == "n")
                    {
                        key2 = game.Options.KeyNO.ToString();
                    }
                    key2 = key2.ToLower();
                    if (presskey == key2)
                    {
                        block = false;
                    }
                }
                catch { }

            }

            #endregion
            #region scn
            else if (kw[0] == "scn")
            {
                int range = -100;
                try
                {
                    range = int.Parse(kw[3]);
                }
                catch
                { }

                if (kw[1] == "enter")
                {
                    Microsoft.DirectX.Vector3 v3 = new Microsoft.DirectX.Vector3();
                    v3.X = float.Parse(kw[3]);
                    v3.Y = float.Parse(kw[4]);
                    game.SceneManager.Enter(kw[2], v3, game.ME.RealDirection);
                }
                else if (kw[1] == "shock" && range != -100)
                {
                    double ms = double.Parse(kw[2]);
                    game.SceneManager.ShockScreen(ms, range);
                }
                //else if (kw[1] == "delay")
                //{
                //    game.SceneManager.Delay(int.Parse(kw[2]));
                //}
                else if (kw[1] == "shock")
                {
                    double ms = double.Parse(kw[2]);
                    game.SceneManager.ShockScreen(ms);
                }
                else if (kw[1] == "fallout")
                {
                    double ms = double.Parse(kw[2]);
                    game.SceneManager.FallOutSceen(ms);
                }
                else if (kw[1] == "fallin")
                {
                    double ms = double.Parse(kw[2]);
                    game.SceneManager.FallInSceen(ms);
                }
                else if (kw[1] == "setctrl")
                {
                    game.SceneManager.Controllable = true;
                }
                else if (kw[1] == "clrctrl")
                {
                    game.SceneManager.Controllable = false;
                }
            }
            #endregion
            #region battle
            else if (kw[0] == "btl")
            {
                int range = -100;
                try
                {
                    range = int.Parse(kw[3]);
                }
                catch
                { }

                if (kw[1] == "shock" && range != -100)
                {
                    double ms = double.Parse(kw[2]);
                    game.BattleManager.ShockScreen(ms, range);
                }
                else if (kw[1] == "shock")
                {
                    double ms = double.Parse(kw[2]);
                    game.BattleManager.ShockScreen(ms);
                }
                else if (kw[1] == "fallout")
                {
                    double ms = double.Parse(kw[2]);
                    game.BattleManager.FallOutSceen(ms);
                }
                else if (kw[1] == "fallin")
                {
                    double ms = double.Parse(kw[2]);
                    game.BattleManager.FallInSceen(ms);
                }
                //else if (kw[1] == "setctrl")
                //{
                //    game.BattleManager.Controllable = true;
                //}
                //else if (kw[1] == "clrctrl")
                //{
                //    game.BattleManager.Controllable = false;
                //}
            }
            #endregion
            #region gui
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
                else if (kw[2] == "appear")
                {
                    game.FormBoxManager.Appear(kw[1]);
                }
                else if (kw[2] == "disappear")
                {
                    if (kw[1] == "all")
                    {
                        game.FormBoxManager.DisappearAll();
                    }
                    else
                    {
                        game.FormBoxManager.Disappear(kw[1]);
                    }
                }
            }
            else if (kw[0] == "ask")
            {
                if (kw.Length == 1)
                {
                    game.FormBoxManager.Disappear("ASKboolBox");
                    game.FormBoxManager.Disappear("ASKintBox");
                }
                else
                {
                    if (kw[1] == "bool")
                    {
                        game.FormBoxManager.Appear("ASKboolBox", kw[2]);
                    }
                }
            }
            //else if (kw[0] == "check")
            //{
            //    if (kw[1] == "bool")
            //    {
            //        game.FormBoxManager.Appear("ASKboolBox", kw[2]);
            //    }
            //}
            else if (kw[0] == "msg")
            {
                if (kw.Length == 1)
                {
                    game.FormBoxManager.Disappear("MessageBox");
                }
                else
                {
                    string str = kw[1].Replace(@"n\", "\n");
                    game.FormBoxManager.Appear("MessageBox", str);
                }
            }
            #endregion
            #region npc
            else if (kw[0] == "npc")
            {
                NPC n = game.GetNPC(kw[1]);
                if (kw[2] == "dir")
                {
                    Direction dir = Direction.U;
                    if (kw[3] == "def")
                    {
                        n.RecoverDirection();
                    }
                    else
                    {
                        if (kw[3] == "u")
                        {
                            dir = Direction.U;
                        }
                        else if (kw[3] == "l")
                        {
                            dir = Direction.L;
                        }
                        else if (kw[3] == "d")
                        {
                            dir = Direction.D;
                        }
                        else
                        {
                            dir = Direction.R;
                        }
                        n.Face(dir);
                    }
                }
                else if (kw[2] == "move")
                {
                    int stp = 1;
                    try
                    {
                        stp = int.Parse(kw[3]);
                    }
                    catch
                    {
                    }
                    if (stp > 1)
                    {
                        for (int i = 0; i < stp; i++)
                        {
                            inscommands.Enqueue("npc " + kw[1] + " move");
                            if (i + 1 < stp)
                            {
                                inscommands.Enqueue("untilstop " + kw[1]);
                            }
                        }
                    }
                    else
                    {
                        n.Move(game.SCN, game.GetNPC(n), game.Options.TilePixelX);
                    }
                    //game.SceneManager.Move(n, stp);
                }
                else if (kw[2] == "hide")
                {
                    n.Invisible = true;
                }
                else if (kw[2] == "show")
                {
                    n.Invisible = false;
                }
            }
            #endregion
            #region pc
            else if (kw[0] == "me")
            {
                if (kw[1] == "skin")
                {
                    game.ME.TextureName = kw[2];
                    game.ME.TextureIndex = -1;
                }
                //else if (kw[1] == "use")
                //{
                //    int i = int.Parse(kw[2]);
                //    //game.ME.Gold += int.Parse(kw[2]);
                //}
                else if (kw[1] == "hp")
                {
                    int i = int.Parse(kw[2]);
                    game.ME.HP += i;
                    if (game.ME.HP > game.ME.HPMax)
                    {
                        game.ME.HP = game.ME.HPMax;
                    }
                }
                else if (kw[1] == "equip")
                {
                    int i = int.Parse(kw[2]);
                    game.ME.BagEquip(i);
                }
                else if (kw[1] == "unequip")
                {
                    int i = int.Parse(kw[2]);
                    game.ME.BagEquip(i);
                }
                else if (kw[1] == "bagremove")
                {
                    int i = int.Parse(kw[2]);
                    game.ME.BagRemove(i);
                }
                else if (kw[1] == "gold")
                {
                    game.ME.Gold += int.Parse(kw[2]);
                }
                else if (kw[1] == "bagadd")
                {
                    Item item;
                    int i = -1;
                    try
                    {
                        i = int.Parse(kw[2]);
                        item = game.Items[i].GetClone();
                    }
                    catch
                    {
                        item = game.Items[kw[2]].GetClone();
                    }
                    item.GUID = Guid.NewGuid();
                    game.ME.BagAdd(item);
                }
                else if (kw[1] == "jump")
                {
                    Microsoft.DirectX.Vector3 v3 = new Microsoft.DirectX.Vector3();
                    v3.X = float.Parse(kw[2]);
                    v3.Y = float.Parse(kw[3]);
                    game.ME.SetRealLocation(v3, game.Options.TilePixelX);
                    game.SceneManager.SceneJump(v3);
                }

                else if (kw[1] == "dir")
                {
                    Direction dir = Direction.U;

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
                    if (game.ME.Face(dir))
                    {
                        game.SCN.Face(game.ME.OppositeDirection);
                    }

                }
                else if (kw[1] == "move")
                {
                    int stp = 1;
                    try
                    {
                        stp = int.Parse(kw[2]);
                    }
                    catch
                    {
                    }
                    if (stp > 1)
                    {
                        for (int i = 0; i < stp; i++)
                        {
                            inscommands.Enqueue("me move");
                            if (i + 1 < stp)
                            {
                                inscommands.Enqueue("untilstop me");
                            }
                        }
                    }
                    else
                    {
                        if (game.ME.Move(game.SCN, game.GetNPC(game.ME), game.Options.TilePixelX))
                        {
                            game.SCN.Move(1, game.Options.TilePixelX);
                        }
                    }
                }
                else if (kw[1] == "fdir")
                {
                    Direction dir = Direction.U;

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
                    game.ME.ForceDirection = dir;
                    game.SCN.Face(Util.GetOppositeDirection(dir));
                }
                else if (kw[1] == "fmove")
                {
                    int stp = 1;
                    try
                    {
                        stp = int.Parse(kw[2]);
                    }
                    catch
                    {
                    }
                    if (stp > 1)
                    {
                        for (int i = 0; i < stp; i++)
                        {
                            inscommands.Enqueue("me fmove");
                            if (i + 1 < stp)
                            {
                                inscommands.Enqueue("untilstop me");
                            }
                        }
                    }
                    else
                    {
                        game.ME.ForceMove(game.Options.TilePixelX);
                        {
                            game.SCN.Move(1, game.Options.TilePixelX);
                        }
                    }
                }
                else if (kw[1] == "setrigor")
                {
                    game.ME.IsRigor = true;
                }
                else if (kw[1] == "clrrigor")
                {
                    game.ME.IsRigor = false;
                }
                else if (kw[1] == "battlesize")
                {
                    int w = int.Parse(kw[2]);
                    int h = int.Parse(kw[3]);
                    game.ME.BattleSize = new Size(w, h);
                }

                else if (kw[1] == "standmovie")
                {
                    int i = (int)BattleState.Stand;
                    game.ME.BattleMovieIndexers[i].Name = kw[2];
                }
                else if (kw[1] == "defensemovie")
                {
                    int i = (int)BattleState.Defense;
                    game.ME.BattleMovieIndexers[i].Name = kw[2];
                }
                else if (kw[1] == "hitmovie")
                {
                    int i = (int)BattleState.Hit;
                    game.ME.BattleMovieIndexers[i].Name = kw[2];
                }
                else if (kw[1] == "fightmovie")
                {
                    int i = (int)BattleState.Fight;
                    game.ME.BattleMovieIndexers[i].Name = kw[2];
                }
                else if (kw[1] == "firemovie")
                {
                    int i = (int)BattleState.Fire;
                    game.ME.BattleMovieIndexers[i].Name = kw[2];
                }
                else if (kw[1] == "throwmovie")
                {
                    int i = (int)BattleState.Throw;
                    game.ME.BattleMovieIndexers[i].Name = kw[2];
                }
                if (kw[1] == "stand")
                {
                    game.ME.SetBattleMovie(BattleState.Stand);
                }
                else if (kw[1] == "defense")
                {
                    game.ME.SetBattleMovie(BattleState.Defense);
                }
                else if (kw[1] == "hit")
                {
                    game.ME.SetBattleMovie(BattleState.Hit);
                }
                else if (kw[1] == "fight")
                {
                    game.ME.SetBattleMovie(BattleState.Fight);
                }
                else if (kw[1] == "fire")
                {
                    game.ME.SetBattleMovie(BattleState.Fire);
                }
                else if (kw[1] == "throw")
                {
                    game.ME.SetBattleMovie(BattleState.Throw);
                }
            }
            #endregion
            #region monster
            else if (kw[0] == "monster")
            {
                if (kw[2] == "stand")
                {
                    int i = int.Parse(kw[1]);
                    game.Monsters[i].SetBattleMovie(BattleState.Stand);
                }
                else if (kw[2] == "defense")
                {
                    int i = int.Parse(kw[1]);
                    game.Monsters[i].SetBattleMovie(BattleState.Defense);
                }
                else if (kw[2] == "hit")
                {
                    int i = int.Parse(kw[1]);
                    game.Monsters[i].SetBattleMovie(BattleState.Hit);
                }
                else if (kw[2] == "fight")
                {
                    int i = int.Parse(kw[1]);
                    game.Monsters[i].SetBattleMovie(BattleState.Fight);
                }
                else if (kw[2] == "fire")
                {
                    int i = int.Parse(kw[1]);
                    game.Monsters[i].SetBattleMovie(BattleState.Fire);
                }
                else if (kw[2] == "throw")
                {
                    int i = int.Parse(kw[1]);
                    game.Monsters[i].SetBattleMovie(BattleState.Throw);
                }
            }
            #endregion
            else
            {
                //TextBox tb = new TextBox(game);
                //tb.Text = kw[0];
                //game.FormBoxManager.Appear("MessageBox", tb);
            }
        }
        void appendCommand(string cmd)
        {
            if (cmd == null)
            {
                return;
            }
            //text += cmd + "\n";
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
        public void AppendCommand(string cmd)
        {
            if (cmd == null)
            {
                return;
            }
            text += cmd + "\n";
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
        //public void Execute(string cmd)
        //{
        //    AppendCommand(cmd);
        //    Execute();
        //}
        public void AppendDotMetalXScript(string fileName)
        {
            string cmds = System.IO.File.ReadAllText(game.ScriptFiles[fileName].FullName);
            text += cmds + "\n";
            AppendCommand(cmds);
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
            if (k == Key.LeftShift ||  k == Key.RightControl)
            {
                isBig = true;
            }
            //if (k== Key.Escape)
            //{
            //    game.Exit();
            //}

            if (k == Key.NumPad6)
            {
                game.Options.UVOffsetX += 0.1f;
            }
            else if (k == Key.NumPad4)
            {
                game.Options.UVOffsetX -= 0.1f;
            }
            else if (k == Key.NumPad8)
            {
                game.Options.UVOffsetY += 0.1f;
            }
            else if (k == Key.NumPad2)
            {
                game.Options.UVOffsetY -= 0.1f;
            }
            else if (k == Key.F1)
            {
                if (drawText)
                {
                    drawText = false;
                    //game.SceneManager.Controllable = true;
                    game.SceneManager.ColorFilter = Color.White;
                    //game.FormBoxManager.Controllable = true;
                    game.FormBoxManager.ColorFilter = Color.White;
                }
                else
                {
                    drawText = true;
                    //game.SceneManager.Controllable = false;
                    game.SceneManager.ColorFilter = Color.FromArgb(20,20,20);
                    //game.FormBoxManager.Controllable = false;
                    game.FormBoxManager.ColorFilter = Color.FromArgb(20, 20, 20);
                }
            }          
            else if (k == Key.F12)
            {
                if (game.Options.TextureDrawMode == TextureDrawMode.Direct2D)
                {
                    game.Options.TextureDrawMode = TextureDrawMode.Direct3D;
                }
                else
                {
                    game.Options.TextureDrawMode = TextureDrawMode.Direct2D;
                }
            }

            else if (k == Key.Return)
            {
                string[] cmds = text.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (cmds.Length > 0)
                {
                    string cmd = cmds[cmds.Length - 1];
                    cmdbak.Push(cmd);
                    text += "\n";
                    appendCommand(cmd);
                    if (isBig)
                    {
                        exe = true;
                    }
                }
            }
            else if (k == Key.Space)
            {
                if (drawText) 
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
                if (drawText)
                {
                    string tmp = k.ToString();
                    if (tmp.Length == 1)
                    {
                        if (!isBig)
                        {
                            tmp = tmp.ToLower();
                        }
                        text += tmp;
                    }
                    else if (tmp.Length == 2)
                    {
                        text += tmp.Substring(1);
                    }
                }
            }
        }
    }
}
