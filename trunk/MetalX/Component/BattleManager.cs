using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

using MetalX.Define;
using MetalX.File;

namespace MetalX.Component
{
    public class BattleManager : GameCom
    {
        string BGTextureName;
        int Round = 0;
        bool PCFirst = true;

        public BattleManager(Game g)
            : base(g)
        {
            DisableAll();
        }

        public override void Code()
        {
            for (int i = 0; i < game.PCs.Count; i++)
            {
                if (game.PCs[i].BattleMovie.NextFrame() == false)
                {
                    game.ScriptManager.InsertCommand("pc " + i + " stand");
                }
            }
            for (int i = 0; i < game.Monsters.Count; i++)
            {
                if (game.Monsters[i].BattleMovie != null)
                    if (game.Monsters[i].BattleMovie.NextFrame() == false)
                    {
                        game.ScriptManager.InsertCommand("monster " + i + " stand");
                    }
            }
            if (game.ScriptManager.Busy)
            { }
            else
            {
                if (Order.Count > 0)
                {
                    int i = GetOrder();
                    if (i < 0)
                    {
                        PCFirst = false;
                        i++;
                        i = -i;


                        int fi = i;
                        int ti = 0;

                        BattleState fbs = BattleState.Fight;
                        BattleState tbs = BattleState.Hit;
                        //提示
                        game.ScriptManager.AppendCommand("msg " + game.Monsters[fi].Name + "　" + fbs.ToString());
                        game.ScriptManager.AppendCommand("untilpress y n");
                        game.ScriptManager.AppendCommand("msg");
                        //攻击动画
                        game.ScriptManager.AppendCommand("monster " + fi + " " + fbs.ToString().ToLower() + " " + ti);
                        game.ScriptManager.AppendCommand("delay 500");
                        //被攻击动画
                        game.ScriptManager.AppendCommand("pc 0 " + tbs.ToString().ToLower());
                        game.ScriptManager.AppendCommand("delay 500");
                    }
                    else
                    {
                        PCFirst = true;
                        int fi = i;

                        game.ScriptManager.AppendCommand("gui MenuBattleCHR appear");
                        game.ScriptManager.AppendCommand("untilpress y");
                        game.ScriptManager.AppendCommand("gui MenuBattleCHR disappear");

                        int ti = 0;

                        BattleState tbs = BattleState.Hit;

                        game.ScriptManager.AppendCommand("var optype = RETURN");
                        string haveweapon = "null";
                        if (game.PCs[fi].Weapon != null)
                        {
                            if (game.PCs[fi].Weapon.Name != null)
                                if (game.PCs[fi].Weapon.Name != string.Empty)
                                {
                                haveweapon = game.PCs[fi].Weapon.Name;
                            }
                        }
                        game.ScriptManager.AppendCommand("?var optype = 攻击 var haveweapon = " + haveweapon);
                        game.ScriptManager.AppendCommand("var bs = weapon");
                        game.ScriptManager.AppendCommand("?var haveweapon = null var bs = fight");

                        //if (game.ScriptRETURN.STRING == "攻击")
                        //{
                        //    if (game.PCs[fi].Weapon == null)
                        //    {
                        //        fbs = BattleState.Fight;
                        //        tbs = BattleState.Hit;
                        //    }
                        //    else
                        //    {
                        //        fbs = BattleState.Weapon;
                        //        tbs = BattleState.Hit;
                        //    }
                        //}                            
                        //提示
                        game.ScriptManager.AppendCommand("msg " + game.PCs[fi].Name + "　[bs]");
                        game.ScriptManager.AppendCommand("untilpress y n");
                        game.ScriptManager.AppendCommand("msg");
                        game.ScriptManager.AppendCommand("pc 0 [bs]" + " " + ti);
                        Vector3 floc = game.PCs[fi].BattleWeaponLocation;
                        game.ScriptManager.AppendCommand("?var bs = weapon movie play " + game.PCs[fi].Weapon.ShotMovieIndexer.Name + " " + floc.X + " " + floc.Y);
                        game.ScriptManager.AppendCommand("delay 500");
                        //被攻击动画
                        game.ScriptManager.AppendCommand("monster " + ti + " " + tbs.ToString().ToLower());
                        Vector3 tloc = game.Monsters[ti].BattleLocation;
                        game.ScriptManager.AppendCommand("?var bs = weapon movie play " + game.PCs[fi].Weapon.HitMovieIndexer.Name + " " + tloc.X + " " + tloc.Y);
                        game.ScriptManager.AppendCommand("delay 500");

                        //game.ScriptManager.AppendCommand("pc 0 stand");
                    }
                }
                else
                {
                    InitOrder();

                    game.ScriptManager.AppendCommand("msg 第" + Round + "回合");
                    game.ScriptManager.AppendCommand("untilpress y n");
                    game.ScriptManager.AppendCommand("msg");
                }
                game.ScriptManager.Execute();
            }
        }

        public override void Draw()
        {
            if (game.Monsters == null)
            {
                return;
            }

            //DrawBGTexture();

            if (PCFirst)
            {
                foreach (Monster mon in game.Monsters)
                {
                    DrawMonster(mon);
                }
                foreach (PC pc in game.PCs)
                {
                    DrawCHR(pc);
                }
            }
            else
            {
                foreach (PC pc in game.PCs)
                {
                    DrawCHR(pc);
                }
                foreach (Monster mon in game.Monsters)
                {
                    DrawMonster(mon);
                }
            }
        }

        void DrawBGTexture()
        {
            if (BGTextureName == null)
            {
                return;
            }
            Vector3 loc = new Vector3();
            loc = Util.Vector3AddVector3(loc, this.ScreenOffsetPixel);
            game.DrawMetalXTexture(game.Textures[BGTextureName], new Rectangle(0, 0, 320, 240), loc, game.Options.WindowSizePixel, 0, ColorFilter);
        }

        void DrawMonster(Monster mon)
        {
            if (mon.BattleMovie == null)
            {
                return;
            }

            Vector3 loc = mon.BattleMovie.DrawLocation;
            loc = Util.Vector3AddVector3(loc, this.ScreenOffsetPixel);
            Color color = Util.MixColor(ColorFilter, mon.BattleMovie.ColorFilter);
            game.DrawMetalXTexture(mon.BattleMovie.MXT, mon.BattleMovie.DrawZone, loc, mon.BattleMovie.TileSize2X, 0, color);
        }

        void DrawCHR(CHR chr)
        {
            Vector3 loc = chr.BattleMovie.DrawLocation;
            loc = Util.Vector3AddVector3(loc, this.ScreenOffsetPixel);
            Color color = Util.MixColor(ColorFilter, chr.BattleMovie.ColorFilter);
            game.DrawMetalXTexture(chr.BattleMovie.MXT, chr.BattleMovie.DrawZone, loc, chr.BattleMovie.TileSize2X, 0, color);
        }

        public void GetIn(string bgt,string bgm)
        {
            BGTextureName = bgt;
            game.ScriptManager.AppendCommand("mp3 1 " + bgm + " loop");
            game.ScriptManager.AppendCommand("scn disableall");
            game.ScriptManager.AppendCommand("btl enableall");
            game.ScriptManager.AppendCommand("pc 0 stand");

            for (int i = 0; i < game.Monsters.Count; i++)
            {
                game.ScriptManager.AppendCommand("monster " + i + " stand");
            }

            game.ScriptManager.Execute();
        }
        List<int> Order = new List<int>();
        void InitOrder()
        {
            Order.Clear();
            int c = game.Monsters.Count;
            for (int i = -c; i < 1; i++)
            {
                Order.Add(i);
            }
            Round++;
        }
        int GetOrder()
        {
            int seed = 0;
            int r = 0;
            seed = Util.Roll(0, Order.Count - 1);
            r = Order[seed];
            Order.RemoveAt(seed);
            return r;
        }
        public void GetOut()
        {
            Round = 0;
            Order.Clear();
            game.Monsters.Clear();

            game.PlayMP3Audio(1, game.AudioFiles[game.SCN.BGMusicNames[0]].FullName, true);
            game.ScriptManager.AppendCommand("break");
            game.ScriptManager.AppendCommand("gui all disappear");
            game.ScriptManager.AppendCommand("btl disableall");
            game.ScriptManager.AppendCommand("scn enableall");
            game.ScriptManager.Execute();
        }

        public override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;

            if (k == Key.E)
            {
                GetOut();
            }
        }
    }
}
