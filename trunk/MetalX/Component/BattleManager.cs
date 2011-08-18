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
                if (MonstersAllDead())
                {
                    int exp = 0;
                    for (int i = 0; i < game.Monsters.Count; i++)
                    {
                        exp += game.Monsters[i].EXP;
                    }
                    for (int i = 0; i < game.PCs.Count; i++)
                    {
                        game.PCs[i].EXP += exp;
                    } 
                    game.ScriptManager.AppendCommand("mp3 1");
                    game.ScriptManager.AppendCommand("mp3 2 win");
                    game.ScriptManager.AppendCommand(@"msg 消灭了所有怪物n\获得经验" + exp + "点");
                    game.ScriptManager.AppendCommand("untilpress y n");
                    game.ScriptManager.AppendCommand("msg");
                    game.ScriptManager.AppendCommand("btl getout");
                }
                else if (PCsAllDead())
                {
                }
                else
                {
                    if (Order.Count > 0)
                    {
                        int i = GetOrder();
                        if (i < 0)
                        {
                            i++;
                            i = -i;
                            Monster mon = game.Monsters[i];
                            if (mon.HP > 0)
                            {
                                game.ScriptManager.AppendCommand("var op_type = weapon");
                                game.ScriptManager.AppendCommand("var mon_index = " + i);
                                game.ScriptManager.AppendCommand("var target_index = 0");

                                BattleState fbs = BattleState.Fight;
                                BattleState tbs = BattleState.Hit;

                                game.ScriptManager.AppendCommand("btl montop");
                                //提示
                                game.ScriptManager.AppendCommand("msg [mon_index]　" + fbs.ToString());
                                game.ScriptManager.AppendCommand("untilpress y n");
                                game.ScriptManager.AppendCommand("msg");
                                //攻击动画
                                game.ScriptManager.AppendCommand("monster [mon_index] " + fbs.ToString().ToLower() + " [target_index]");
                                game.ScriptManager.AppendCommand("delay 500");
                                //被攻击动画
                                game.ScriptManager.AppendCommand("pc [target_index] " + tbs.ToString().ToLower());
                                game.ScriptManager.AppendCommand("delay 500");
                            }
                        }
                        else
                        {
                            PC pc = game.PCs[i];
                            if (pc.HP > 0)
                            {
                                game.ScriptManager.AppendCommand("var op_type = weapon");
                                game.ScriptManager.AppendCommand("var target_index = 0");
                                game.ScriptManager.AppendCommand("var pc_index = 0");

                                game.ScriptManager.AppendCommand("btl pctop");

                                game.ScriptManager.AppendCommand("gui appear MenuBattleCHR");
                                game.ScriptManager.AppendCommand("untilpress y");
                                game.ScriptManager.AppendCommand("gui disappear MenuBattleCHR");

                                //提示
                                game.ScriptManager.AppendCommand("msg " + pc.Name + "　[op_type]");
                                game.ScriptManager.AppendCommand("untilpress y n");
                                game.ScriptManager.AppendCommand("msg");
                                //动画
                                game.ScriptManager.AppendCommand("?var op_type = fight pc [pc_index] [op_type] [target_index]");
                                game.ScriptManager.AppendCommand("?var op_type = weapon pc [pc_index] [op_type] [target_index]");
                                game.ScriptManager.AppendCommand("?var op_type = item pc [pc_index] [op_type] [item_index] [target_index]");

                                game.ScriptManager.AppendCommand("delay 500");
                            }
                        }
                    }
                    else
                    {
                        InitOrder();

                        game.ScriptManager.AppendCommand("msg 第" + Round + "回合");
                        game.ScriptManager.AppendCommand("untilpress y n");
                        game.ScriptManager.AppendCommand("msg");
                    }
                    
                }game.ScriptManager.Execute();
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
            if (mon.HP < 0)
            {
                return;
            }

            if (mon.BattleMovie == null)
            {
                return;
            }

            Vector3 loc = mon.BattleMovie.DrawLocation;
            loc = Util.Vector3AddVector3(loc, this.ScreenOffsetPixel);
            Color color = Util.MixColor(ColorFilter, mon.BattleMovie.ColorFilter);
            game.DrawMetalXTexture(mon.BattleMovie.MXT, mon.BattleMovie.DrawZone, loc, mon.BattleMovie.TileSize2X, 0, color);
            game.DrawText(mon.HP.ToString(), Util.Vector32Point(loc), Color.White);
        }

        void DrawCHR(CHR chr)
        {
            Vector3 loc = chr.BattleMovie.DrawLocation;
            loc = Util.Vector3AddVector3(loc, this.ScreenOffsetPixel);
            Color color = Util.MixColor(ColorFilter, chr.BattleMovie.ColorFilter);
            game.DrawMetalXTexture(chr.BattleMovie.MXT, chr.BattleMovie.DrawZone, loc, chr.BattleMovie.TileSize2X, 0, color);
        }
        public bool MonstersAllDead()
        {
            for (int i = 0; i < game.Monsters.Count; i++)
            {
                if (game.Monsters[i].HP > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public bool PCsAllDead()
        {
            for (int i = 0; i < game.PCs.Count; i++)
            {
                if (game.PCs[i].HP > 0)
                {
                    return false;
                }
            }
            return true;
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
