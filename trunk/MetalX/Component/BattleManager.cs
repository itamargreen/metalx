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
        public bool PCOnTop = true;

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
                        //PCOnTop = false;
                        i++;
                        i = -i;


                        int fi = i;
                        int ti = 0;

                        BattleState fbs = BattleState.Fight;
                        BattleState tbs = BattleState.Hit;

                        game.ScriptManager.AppendCommand("btl montop");
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
                        int fi = i;

                        game.ScriptManager.AppendCommand("btl pctop"); 

                        game.ScriptManager.AppendCommand("gui appear MenuBattleCHR");
                        game.ScriptManager.AppendCommand("untilpress y");
                        game.ScriptManager.AppendCommand("gui disappear MenuBattleCHR");

                        string haveweapon = "null";
                        if (game.PCs[fi].Weapon != null)
                        {
                            if (game.PCs[fi].Weapon.Name != null)
                                if (game.PCs[fi].Weapon.Name != string.Empty)
                                {
                                    haveweapon = game.PCs[fi].Weapon.Name;
                                }
                        }
                        game.ScriptManager.AppendCommand("var haveweapon = " + haveweapon);
                        game.ScriptManager.AppendCommand("?var bs # weapon var haveweapon = !null");
                        game.ScriptManager.AppendCommand("?var haveweapon = null var bs = fight");
                                      
                        //提示
                        game.ScriptManager.AppendCommand("msg " + game.PCs[fi].Name + "　[bs]");
                        game.ScriptManager.AppendCommand("untilpress y n");
                        game.ScriptManager.AppendCommand("msg");
                        //动画
                        game.ScriptManager.AppendCommand("?var bs = fight pc 0 [bs] [tar_index]");
                        game.ScriptManager.AppendCommand("?var bs = weapon pc 0 [bs] [tar_index]");
                        game.ScriptManager.AppendCommand("?var bs = item pc 0 [bs] [item_index] [tar_index]");
                        game.ScriptManager.AppendCommand("delay 500");
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

            DrawBGTexture();

            if (PCOnTop)
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

            game.StopAudio(1);
            try
            {
                game.PlayMP3Audio(1, game.AudioFiles[game.SCN.BGMusicNames[0]].FullName, true);
            }
            catch { }
            game.ScriptManager.AppendCommand("break");
            game.ScriptManager.AppendCommand("gui disappear all");
            game.ScriptManager.AppendCommand("btl disableall");
            game.ScriptManager.AppendCommand("scn enableall");
            game.ScriptManager.Execute();
            //game.Monsters.Clear();
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
