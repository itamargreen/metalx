﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;

using Microsoft.DirectX.DirectInput;
using Microsoft.DirectX;

using MetalX.Define;

namespace MetalX.Component
{
    public class ScriptManager : GameCom
    {
        //bool gsc_bak = false;
        //List<string> vars = new List<string>();
        Hashtable vars = new Hashtable();
        public string GetVariable(string name)
        {
            return vars[name].ToString();
        }
        public void SetVariable(string name, string value)
        {
            if (vars.ContainsKey(name))
            {
                vars[name] = value;
            }
            else
            {
                vars.Add(name, value);
            }
        }
        
        //public ScriptReturn RETURN = new ScriptReturn();

        //public void Return(bool yes)
        //{
        //    RETURN.BOOL = yes;
        //}
        //public void Return(int i)
        //{
        //    RETURN.INT = i;
        //}
        //public void Return(string str)
        //{
        //    RETURN.STRING = str;
        //}

        string text = "";
        bool drawText = false;
        bool isBig = false;
        public bool Busy
        {
            get
            {
                if (appque.Count > 0 || insque.Count > 0)
                {
                    return true;
                }
                return false;
                //return exe;
            }
        }
        string cursor;
        string curcmd;
        string presskey = "";
        string[] texts
        {
            get
            {
                string[] strs = (text + cursor).Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                string[] tmp = new string[20];
                int j = 0;
                for (int i = strs.Length - 20; i < strs.Length; i++)
                {
                    //try
                    {
                        tmp[j] = strs[i];
                    }
                    //catch { }
                    j++;
                }
                return tmp;
            }
        }

        //DateTime delayStartTime = DateTime.Now;
        //double delayTime = 0;
        //double delayLeftTime = 0;

        bool exe = false; 
        //Queue<string> commands = new Queue<string>();
        //Queue<string> tmpcommands = new Queue<string>();
        //Queue<string> inscommands = new Queue<string>();
        bool block = false;

        List<string> appque = new List<string>();
        List<string> insque = new List<string>();

        //int ins_pointer = 0;
        void ins_que(string cmd)
        {
            insque.Add(cmd);
        }
        void app_que(string cmd)
        {
            appque.Add(cmd);
        }
        string de_que()
        {
            string str;
            try
            {
                if (insque.Count > 0)
                {
                    str = insque[0];
                    insque.RemoveAt(0);
                }
                else
                {
                    str = appque[0];
                    appque.RemoveAt(0);
                }
            }
            catch
            {
                return null;
            }
            return str;
        }
                
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
                    presskey = "";
                    text += curcmd + "\n";
                    //if (text.Length > 1024)
                    //{
                    //    text = "";
                    //}
                    curcmd = de_que();
                    if (curcmd == null)
                    {
                        exe = false;
                    }
                    execute(curcmd);
                }
            }

        }
        public override void Draw()
        {
            if (drawText)
            {
                if (DateTime.Now.Millisecond < 500)
                {
                    cursor = "_";
                }
                else
                {
                    cursor = " ";
                }
                //game.DrawText(text + cur, new System.Drawing.Point(), ColorFilter);
                for (int i = 0; i < texts.Length; i++)
                {
                    game.DrawText(texts[i], new Point(0, i * 20), Color.White);
                }
                
            }
            //game.DrawText("FPS: " + game.AverageFPS.ToString("f1") + "    DrawMode: " + game.Options.TextureDrawMode, new System.Drawing.Point(0, 0), Color.Blue);
            //game.DrawText(game.SceneManager.Controllable.ToString(), new System.Drawing.Point(0, 0), Color.Blue);
        }

        void execute(string cmd)
        {
            if (cmd == null)
            {
                return; 
            }
            //cmd = cmd.ToLower();
            string[] kw = cmd.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (kw.Length == 0)
            {
                return;
            }
            kw[0] = kw[0].ToLower();
            #region sys

            if (kw[0] == "?var")
            {
                string name = kw[1];
                string value = kw[3];
                if (kw[2] == "=")
                {
                    string val = "";
                    try
                    {
                        val = vars[name].ToString();
                    }
                    catch
                    {
                        return;
                    }
                    if (val == value)
                    {
                        string[] nkw = new string[kw.Length - 4];
                        for (int i = 4; i < kw.Length; i++)
                        {
                            nkw[i - 4] = kw[i];
                        }
                        kw = nkw;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (kw[2] == "#")
                {
                    string val = "";
                    try
                    {
                        val = vars[name].ToString();
                    }
                    catch
                    {
                        return;
                    }
                    if (val != value)
                    {
                        string[] nkw = new string[kw.Length - 4];
                        for (int i = 4; i < kw.Length; i++)
                        {
                            nkw[i - 4] = kw[i];
                        }
                        kw = nkw;
                    }
                    else
                    {
                        return;
                    }
                }
                else if (kw[2] == "<")
                {
                    string val = "";
                    try
                    {
                        val = vars[name].ToString();
                    }
                    catch
                    {
                        return;
                    }
                    if (int.Parse(val) < int.Parse(value))
                    {
                        string[] nkw = new string[kw.Length - 4];
                        for (int i = 4; i < kw.Length; i++)
                        {
                            nkw[i - 4] = kw[i];
                        }
                        kw = nkw;
                    }
                    else
                    {
                        return;
                    }
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
                SetVariable("roll", seed.ToString());
                //RETURN.INT = seed;
            }
            //else if (kw[0] == "exe")
            //{
            //    exe = true;
            //}
            else if (kw[0] == "clr")
            {
                text = "";
            }
            else if (kw[0] == "var")
            {
                if (kw[2] == "=")
                {
                    SetVariable(kw[1], kw[3]);
                }
            }
            //else if (kw[0] == "return")
            //{
            //    try
            //    {
            //        int i = int.Parse(kw[1]);
            //        RETURN.INT = i;
            //    }
            //    catch
            //    {
            //        try
            //        {
            //            bool y = bool.Parse(kw[1]);
            //            RETURN.BOOL = y;
            //        }
            //        catch
            //        {
            //            RETURN.STRING = kw[1];
            //        }
            //    }
            //}            
            else if (kw[0] == "fullscreen")
            {
                game.ToggleToFullScreen();
            }
            //else if (kw[0] == "comctrl")
            //{
            //    game.SceneManager.Controllable = false;
            //    game.FormBoxManager.Controllable = false;
            //}
            //else if (kw[0] == "userctrl")
            //{
            //    game.SceneManager.Controllable = true;
            //    game.FormBoxManager.Controllable = true;
            //}
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
                bool loop = false;
                try
                {
                    if (kw[3] == "loop")
                    {
                        loop = true;
                    }
                }
                catch { }
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
                //int c = cmdque.Count;
                //for (int i = 0; i < c; i++)
                //{
                //    tmpcommands.Enqueue(commands.Dequeue());
                //}
                InsertDotMetalXScript(kw[1]);
                //for (int i = 0; i < c; i++)
                //{
                //    commands.Enqueue(tmpcommands.Dequeue());
                //}
            }
            else if (kw[0] == "untilstop")
            {
                double a;
                if (kw[1] == "pc")
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
                            ins_que("npc " + kw[1] + " move 1");
                            if (i + 1 < stp)
                            {
                                ins_que("untilstop " + kw[1]);
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
            else if (kw[0] == "pc")
            {
                //if (kw[1].Contains("var."))
                //{
                //    string name = kw[1].Substring(4);
                //    kw[1] = vars[name].ToString();
                //}
                PC pc = game.ME;

                getValueByName(ref kw[1]);
                int c = int.Parse(kw[1]);
                if (game.PCs.Count > 0)
                {
                    pc = game.PCs[c];
                }

                getValueByName(ref kw[2]);
                if (kw.Length > 3)
                {
                    getValueByName(ref kw[3]);
                }
                if (kw[2] == "skin")
                {
                    pc.TextureName = kw[3];
                    pc.TextureIndex = -1;
                }
                //else if (kw[1] == "use")
                //{
                //    int i = int.Parse(kw[2]);
                //    //game.ME.Gold += int.Parse(kw[2]);
                //}
                else if (kw[2] == "freeze")
                {
                    pc.CanCtrl = false;
                }
                else if (kw[2] == "unfreeze")
                {
                    pc.CanCtrl = true;
                }
                else if (kw[2] == "+hp")
                {
                    pc.HP += int.Parse(kw[3]);
                }
                else if (kw[2] == "-hp")
                {
                    pc.HP -= int.Parse(kw[3]);
                }
                else if (kw[2] == "equip")
                {
                    int i = int.Parse(kw[3]);
                    pc.BagEquip(i);
                }
                else if (kw[2] == "unequip")
                {
                    int i = int.Parse(kw[3]);
                    pc.BagEquip(i);
                }
                else if (kw[2] == "unequip")
                {
                    int i = int.Parse(kw[3]);
                    pc.BagUnequip((EquipmentCHRType)i);
                }
                else if (kw[2] == "bagremove")
                {
                    int i = int.Parse(kw[3]);
                    pc.BagRemove(i);
                }
                else if (kw[2] == "+gold")
                {
                    pc.Gold += int.Parse(kw[3]);
                }
                else if (kw[2] == "bagadd")
                {
                    Item item;
                    int i = -1;
                    try
                    {
                        i = int.Parse(kw[3]);
                        item = game.Items[i].GetClone();
                    }
                    catch
                    {
                        item = game.Items[kw[3]].GetClone();
                    }
                    item.GUID = Guid.NewGuid();
                    pc.BagAdd(item);
                }
                else if (kw[2] == "jump")
                {
                    Microsoft.DirectX.Vector3 v3 = new Microsoft.DirectX.Vector3();
                    v3.X = float.Parse(kw[3]);
                    v3.Y = float.Parse(kw[4]);
                    pc.SetRealLocation(v3, game.Options.TilePixelX);
                    game.SceneManager.SceneJump(v3);
                }

                else if (kw[2] == "dir")
                {
                    Direction dir = Direction.U;

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
                    if (pc.Face(dir))
                    {
                        game.SCN.Face(pc.OppositeDirection);
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
                            ins_que("pc 0 move 1");
                            if (i + 1 < stp)
                            {
                                ins_que("untilstop pc");
                            }
                        }
                    }
                    else
                    {
                        if (pc.Move(game.SCN, game.GetNPC(pc), game.Options.TilePixelX))
                        {
                            game.SCN.Move(1, game.Options.TilePixelX);
                        }
                    }
                }
                else if (kw[2] == "fdir")
                {
                    Direction dir = Direction.U;

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
                    pc.ForceDirection = dir;
                    game.SCN.Face(Util.GetOppositeDirection(dir));
                }
                else if (kw[2] == "fmove")
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
                            ins_que("pc fmove");
                            if (i + 1 < stp)
                            {
                                ins_que("untilstop pc");
                            }
                        }
                    }
                    else
                    {
                        pc.ForceMove(game.Options.TilePixelX);
                        {
                            game.SCN.Move(1, game.Options.TilePixelX);
                        }
                    }
                }
                else if (kw[2] == "setrigor")
                {
                    pc.IsRigor = true;
                }
                else if (kw[2] == "clrrigor")
                {
                    pc.IsRigor = false;
                }
                else if (kw[2] == "battlesize")
                {
                    int w = int.Parse(kw[3]);
                    int h = int.Parse(kw[4]);
                    pc.BattleSize = new Size(w, h);
                }

                else if (kw[2] == "standmovie")
                {
                    int i = (int)BattleState.Stand;
                    pc.BattleMovieIndexers[i].Name = kw[3];
                }
                else if (kw[2] == "blockmovie")
                {
                    int i = (int)BattleState.Block;
                    pc.BattleMovieIndexers[i].Name = kw[3];
                }
                else if (kw[2] == "hitmovie")
                {
                    int i = (int)BattleState.Hit;
                    pc.BattleMovieIndexers[i].Name = kw[3];
                }
                else if (kw[2] == "fightmovie")
                {
                    int i = (int)BattleState.Fight;
                    pc.BattleMovieIndexers[i].Name = kw[3];
                }
                else if (kw[2] == "weaponmovie")
                {
                    int i = (int)BattleState.Weapon;
                    pc.BattleMovieIndexers[i].Name = kw[3];
                }
                else if (kw[2] == "itemmovie")
                {
                    int i = (int)BattleState.Item;
                    pc.BattleMovieIndexers[i].Name = kw[3];
                }
                else if (kw[2] == "missmovie")
                {
                    int i = (int)BattleState.Miss;
                    pc.BattleMovieIndexers[i].Name = kw[3];
                }
                else if (kw[2] == "runmovie")
                {
                    int i = (int)BattleState.Run;
                    pc.BattleMovieIndexers[i].Name = kw[3];
                }
                else if (kw[2] == "stand")
                {
                    pc.BattleState = BattleState.Stand;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[pc.BattleMovieIndexer.Name].FullName);
                    pc.SetBattleMovie(mov);

                }
                else if (kw[2] == "block")
                {
                    pc.BattleState = BattleState.Block;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[pc.BattleMovieIndexer.Name].FullName);
                    pc.SetBattleMovie(mov);
                }
                else if (kw[2] == "hit")
                {
                    pc.BattleState = BattleState.Hit;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[pc.BattleMovieIndexer.Name].FullName);
                    pc.SetBattleMovie(mov);
                }
                else if (kw[2] == "fight")
                {
                    int t = int.Parse(kw[3]);
                    Monster mon = game.Monsters[t];
                    Microsoft.DirectX.Vector3 v3 = mon.BattleLocation;
                    v3.X += (mon.BattleSize.Width / 2 + pc.BattleSize.Width / 2);
                    //v3.Y += mon.BattleSize.Height;
                    //v3.Y -= game.ME.BattleSize.Height;

                    pc.BattleState = BattleState.Fight;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[pc.BattleMovieIndexer.Name].FullName);
                    pc.SetBattleMovie(mov, pc.BattleLocation, v3, 1);
                    try
                    {
                        game.PlayMP3Audio(2, game.AudioFiles[mov.BGSound.Name].FullName);
                    }
                    catch { }
                }
                else if (kw[2] == "weapon")
                {
                    int t = int.Parse(kw[3]);

                    Monster mon = game.Monsters[t];

                    pc.BattleState = BattleState.Weapon;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[pc.BattleMovieIndexer.Name].FullName);
                    pc.SetBattleMovie(mov);

                    Vector3 floc = pc.BattleWeaponLocation;
                    InsertCommand("?var op_type = weapon movie play " + pc.Weapon.ShotMovieIndexer.Name + " " + floc.X + " " + floc.Y);
                    InsertCommand("delay " + mov.MovieTime);

                    Vector3 tloc = mon.BattleLocation;
                    double bt = pc.Weapon.FlyTime;
                    if (bt == 0)
                    {
                        //InsertCommand("monster " + t + " hit");
                        //InsertCommand("?var bs = weapon movie play " + pc.Weapon.FlyMovieIndexer.Name + " " + tloc.X + " " + tloc.Y);
                    }
                    else
                    {
                        InsertCommand("monster " + t + " hit");
                        InsertCommand("?var op_type = weapon movie play " + pc.Weapon.FlyMovieIndexer.Name + " " + floc.X + " " + floc.Y + " " + tloc.X + " " + tloc.Y + " " + bt);
                    }
                    InsertCommand("delay " + bt);
                    InsertCommand("?var op_type = weapon movie play " + pc.Weapon.HitMovieIndexer.Name + " " + tloc.X + " " + tloc.Y);
                }
                else if (kw[2] == "item")
                {
                    int item_index = int.Parse(kw[3]);
                    getValueByName(ref kw[4]);
                    int t = int.Parse(kw[4]);

                    Monster mon = game.Monsters[t];

                    pc.BattleState = BattleState.Item;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[pc.BattleMovieIndexer.Name].FullName);
                    pc.SetBattleMovie(mov);

                    Vector3 floc = pc.BattleWeaponLocation;
                    InsertCommand("?var op_type = item movie play " + pc.Bag[item_index].ShotMovieIndexer.Name + " " + floc.X + " " + floc.Y);
                    InsertCommand("delay " + mov.MovieTime);

                    Vector3 tloc = mon.BattleLocation;
                    double bt = pc.Bag[item_index].FlyTime;
                    if (bt == 0)
                    {
                        //InsertCommand("monster " + t + " hit");
                        //InsertCommand("?var bs = item movie play " + pc.Bag[item_index].FlyMovieIndexer.Name + " " + tloc.X + " " + tloc.Y);
                    }
                    else
                    {
                        InsertCommand("monster " + t + " hit");
                        InsertCommand("?var op_type = item movie play " + pc.Bag[item_index].FlyMovieIndexer.Name + " " + floc.X + " " + floc.Y + " " + tloc.X + " " + tloc.Y + " " + bt);
                    }
                    InsertCommand("delay " + bt);
                    InsertCommand("?var op_type = item movie play " + pc.Bag[item_index].HitMovieIndexer.Name + " " + tloc.X + " " + tloc.Y);
                }
            }
            #endregion
            #region monster
            else if (kw[0] == "monster")
            {
                getValueByName(ref kw[1]);
                int i = int.Parse(kw[1]);
                Monster mon = game.Monsters[i];
                if (kw[2] == "weapon")
                {
                    mon.BattleState = BattleState.Weapon;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[mon.BattleMovieIndexer.Name].FullName);
                    mon.SetBattleMovie(mov);
                }
                else if (kw[2] == "item")
                {
                    mon.BattleState = BattleState.Item;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[mon.BattleMovieIndexer.Name].FullName);
                    mon.SetBattleMovie(mov);
                }
                else if (kw[2] == "fight")
                {
                    getValueByName(ref kw[3]);
                    int ti = int.Parse(kw[3]);
                    PC pc = game.PCs[ti];
                    Microsoft.DirectX.Vector3 v3 = pc.BattleLocation;
                    v3.X -= (mon.BattleSize.Width / 2 + pc.BattleSize.Width / 2);
                    //v3.Y += pc.BattleSize.Height;
                    //v3.Y -= mon.BattleSize.Height;

                    mon.BattleState = BattleState.Fight;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[mon.BattleMovieIndexer.Name].FullName);
                    mon.SetBattleMovie(mov, mon.BattleLocation, v3, 1);
                    try
                    {
                        game.PlayMP3Audio(2, game.AudioFiles[mov.BGSound.Name].FullName);
                    }
                    catch { }
                }
                else if (kw[2] == "run")
                {
                    mon.BattleState = BattleState.Run;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[mon.BattleMovieIndexer.Name].FullName);
                    mon.SetBattleMovie(mov);
                }
                else if (kw[2] == "miss")
                {
                    mon.BattleState = BattleState.Miss;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[mon.BattleMovieIndexer.Name].FullName);
                    mon.SetBattleMovie(mov);
                }
                else if (kw[2] == "block")
                {
                    mon.BattleState = BattleState.Block;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[mon.BattleMovieIndexer.Name].FullName);
                    mon.SetBattleMovie(mov);
                }
                else if (kw[2] == "hit")
                {
                    mon.BattleState = BattleState.Hit;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[mon.BattleMovieIndexer.Name].FullName);
                    mon.SetBattleMovie(mov);
                }
                else if (kw[2] == "stand")
                {
                    mon.BattleState = BattleState.Stand;
                    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[mon.BattleMovieIndexer.Name].FullName);
                    mon.SetBattleMovie(mov);
                }
                else if (kw[2] == "bagadd")
                {
                    Item item;
                    int j = -1;
                    try
                    {
                        j = int.Parse(kw[3]);
                        item = game.Items[j].GetClone();
                    }
                    catch
                    {
                        item = game.Items[kw[3]].GetClone();
                    }
                    item.GUID = Guid.NewGuid();
                    mon.BagAdd(item);
                }
                else if (kw[2] == "standmovie")
                {
                    int j = (int)BattleState.Stand;
                    mon.BattleMovieIndexers[j].Name = kw[3];
                }
                else if (kw[2] == "blockmovie")
                {
                    int j = (int)BattleState.Block;
                    mon.BattleMovieIndexers[j].Name = kw[3];
                }
                else if (kw[2] == "hitmovie")
                {
                    int j = (int)BattleState.Hit;
                    mon.BattleMovieIndexers[j].Name = kw[3];
                }
                else if (kw[2] == "fightmovie")
                {
                    int j = (int)BattleState.Fight;
                    mon.BattleMovieIndexers[j].Name = kw[3];
                }
                else if (kw[2] == "weaponmovie")
                {
                    int j = (int)BattleState.Weapon;
                    mon.BattleMovieIndexers[j].Name = kw[3];
                }
                else if (kw[2] == "itemmovie")
                {
                    int j = (int)BattleState.Item;
                    mon.BattleMovieIndexers[j].Name = kw[3];
                }
                else if (kw[2] == "missmovie")
                {
                    int j = (int)BattleState.Miss;
                    mon.BattleMovieIndexers[j].Name = kw[3];
                }
                else if (kw[2] == "runmovie")
                {
                    int j = (int)BattleState.Run;
                    mon.BattleMovieIndexers[j].Name = kw[3];
                }
                else if (kw[2] == "+hp")
                {
                    mon.HP += int.Parse(kw[3]);
                }
                else if (kw[2] == "-hp")
                {
                    mon.HP -= int.Parse(kw[3]);
                    if (mon.HP < 0)
                    {
                        mon.HP = 0;
                    }
                }
                else if (kw[2] == "gethp")
                {
                    SetVariable(kw[3], mon.HP.ToString());
                }
                else if (kw[2] == "dead")
                {
                    mon.HP = -1;
                    //game.Monsters.RemoveAt(i);
                }
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
                    //gsc_bak = game.SceneManager.Controllable;
                    game.SceneManager.Controllable = false;
                }
                else if (kw[1] == "enableall")
                {
                    game.SceneManager.EnableAll();
                }
                else if (kw[1] == "disableall")
                {
                    game.SceneManager.DisableAll();
                }
                else if (kw[1] == "visible")
                {
                    game.SceneManager.Visible = true;
                }
                else if (kw[1] == "invisible")
                {
                    game.SceneManager.Visible = false;
                }
                else if (kw[1] == "enable")
                {
                    game.SceneManager.Enable = true;
                }
                else if (kw[1] == "disable")
                {
                    game.SceneManager.Enable = false;
                }
                //else if (kw[1] == "ctrl")
                //{
                //    game.SceneManager.Controllable = true;
                //}
                //else if (kw[1] == "ctrless")
                //{
                //    game.SceneManager.Controllable = false;
                //}
            }
            #endregion
            #region btl
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
                else if (kw[1] == "getout")
                {
                    game.BattleManager.GetOut();
                }
                else if (kw[1] == "pctop")
                {
                    game.BattleManager.PCOnTop = true;
                }
                else if (kw[1] == "montop")
                {
                    game.BattleManager.PCOnTop = false;
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
                else if (kw[1] == "enableall")
                {
                    game.BattleManager.EnableAll();
                }
                else if (kw[1] == "disableall")
                {
                    game.BattleManager.DisableAll();
                }
            }
            #endregion
            else if (kw[0] == "movie")
            {
                try
                {
                    if (kw.Length >= 7)
                    {
                        if (kw[1] == "play")
                        {
                            double offsettime = 1;
                            MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[kw[2]].FullName);
                            int x = int.Parse(kw[3]);
                            int y = int.Parse(kw[4]);
                            int tx = int.Parse(kw[5]);
                            int ty = int.Parse(kw[6]);
                            try
                            {
                                offsettime = int.Parse(kw[7]);
                            }
                            catch { }
                            Vector3 f = new Vector3(x, y, 0);
                            Vector3 t = new Vector3(tx, ty, 0);
                            game.MovieManager.PlayMovie(mov, f, t, offsettime);

                            if (mov.BGSound.Name != null)
                                if (mov.BGSound.Name != string.Empty)
                                    game.PlayMP3Audio(2, game.AudioFiles[mov.BGSound.Name].FullName);
                        }
                    }
                    else
                    {
                        if (kw[1] == "play")
                        {
                            MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[kw[2]].FullName);
                            int x = int.Parse(kw[3]);
                            int y = int.Parse(kw[4]);
                            Vector3 f = new Vector3(x, y, 0);
                            game.MovieManager.PlayMovie(mov, f);

                            if (mov.BGSound.Name != null)
                                if (mov.BGSound.Name != string.Empty)
                                    game.PlayMP3Audio(2, game.AudioFiles[mov.BGSound.Name].FullName);
                        }
                        else if (kw[1] == "set")
                        {
                            int x = int.Parse(kw[3]);
                            int y = int.Parse(kw[4]);
                            Vector3 f = new Vector3(x, y, 0);
                            game.MovieManager.PlayMovie(kw[2], f);
                        }
                    }
                }
                catch { }
                //else if (kw[1] == "bplay")
                //{
                //    int i = int.Parse(kw[2]);
                //    int x = int.Parse(kw[4]);
                //    int y = int.Parse(kw[5]);
                //    Vector3 f = new Vector3(x, y, 0);
                //    MetalX.File.MetalXMovie mov = game.LoadDotMXMovie(game.MovieFiles[kw[3]].FullName);
                //    if (mov.BGSound != null)
                //    {
                //        inscommands.Enqueue("mp3 2 " + mov.BGSound.Name);
                //    }
                //    game.MovieManager.BattlePlayMovie(i, mov, f, f, 1);
                //}
            }
            #region gui
            else if (kw[0] == "gui")
            {

                if (kw[1] == "shock")
                {
                    double ms = double.Parse(kw[2]);
                    game.FormBoxManager.ShockScreen(ms);
                }
                else if (kw[1] == "focuson")
                {
                    game.FormBoxManager.FocusOn(kw[2]);
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
                else if (kw[1] == "appear")
                {
                    if (kw.Length == 5)
                    {
                        if (kw[3] == "arg")
                        {
                            if (kw[4] == "me")
                            {
                                game.FormBoxManager.Appear(kw[2], game.ME);
                            }
                        }
                        else
                        {
                            int x = 0;
                            int y = 0;
                            try
                            {
                                x = int.Parse(kw[3]);
                                y = int.Parse(kw[4]);
                                game.FormBoxManager.Appear(kw[2], new Point(x, y));
                            }
                            catch
                            {
                            }
                        }
                    }
                    else
                    {
                        game.FormBoxManager.Appear(kw[2]);
                    }
                }
                else if (kw[1] == "disappear")
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
                    if (kw[2] == "bool")
                    {
                        game.FormBoxManager.Appear("ASKboolBox", kw[1]);
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
                //if (kw[1].Contains("var."))
                //{
                //    string name = kw[1].Substring(4);
                //    kw[1].re = vars[name].ToString();
                //}
                if (kw.Length == 1)
                {
                    game.FormBoxManager.Disappear("MessageBox");
                }
                else
                {
                    getValueByName(ref kw[1]);
                    string str = kw[1].Replace(@"n\", "\n");
                    game.FormBoxManager.Appear("MessageBox", str);
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

        bool getValueByName(ref string input)
        {
            if (input.Contains("["))
            {
                if (input.Contains("]"))
                {
                    int left = input.IndexOf("[");
                    int right = input.IndexOf("]");
                    string name = input.Substring(left + 1, right - left - 1);
                    input = input.Replace("[" + name + "]", vars[name].ToString());
                    return true;
                }
            }
            return false;
        }
        //void appendCommand(string cmd)
        //{
        //    if (cmd == null)
        //    {
        //        return;
        //    }
        //    //text += cmd + "\n";
        //    string[] cmds = cmd.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        //    foreach (string c in cmds)
        //    {
        //        try
        //        {
        //            if (c.Substring(0, 2) != "//")
        //            {

        //                commands.Enqueue(c);
        //            }
        //        }
        //        catch { }
        //    }
        //}
        public void InsertCommand(string cmd)
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
                        ins_que(c);
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
            //text += cmd + "\n";
            string[] cmds = cmd.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string c in cmds)
            {
                try
                {
                    if (c.Substring(0, 2) != "//")
                    {
                        if (c == "break")
                        {
                            insque.Clear();
                            appque.Clear();
                            exe = false;
                            block = false;
                        }
                        else
                        {
                            app_que(c);
                        }
                    }
                }
                catch { }
            }
        }
        public void Execute()
        {
            exe = true;
        }  
        public void AppendDotMetalXScript(string fileName)
        {
            string cmds = System.IO.File.ReadAllText(game.ScriptFiles[fileName].FullName);
            AppendCommand(cmds);
        }
        public void InsertDotMetalXScript(string fileName)
        {
            string cmd = System.IO.File.ReadAllText(game.ScriptFiles[fileName].FullName);
            string[] cmds = cmd.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            appque.InsertRange(0, cmds);
            //foreach (string c in cmds)
            //{
            //    try
            //    {
            //        if (c.Substring(0, 2) != "//")
            //        {
            //            appque.InsertRange(0, cmds);
            //        }
            //    }
            //    catch { }
            //}
            //InsertCommand(cmds);

        }

        public override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            presskey = k.ToString().ToLower();
            if (k == Key.LeftShift || k == Key.RightControl)
            {
                isBig = false;
            }
        }
        public override void OnKeyDownCode(object sender, int key)
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
                    game.BattleManager.ColorFilter = Color.White;
                    //game.FormBoxManager.Controllable = true;
                    game.FormBoxManager.ColorFilter = Color.White;
                }
                else
                {
                    drawText = true;
                    //game.SceneManager.Controllable = false;
                    game.SceneManager.ColorFilter = Color.FromArgb(20, 20, 20);
                    game.BattleManager.ColorFilter = Color.FromArgb(20, 20, 20);
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
                    //cmdbak.Push(cmd);
                    text += "\n";
                    AppendCommand(cmd);
                    if (isBig)
                    {
                        Execute();
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
                //if (cmdbak.Count > 0)
                //{
                //    text += cmdbak.Pop();
                //}
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
