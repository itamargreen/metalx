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
                        int ti = 0;

                        BattleState fbs = BattleState.Fight;
                        BattleState tbs = BattleState.Hit;
                        //提示
                        game.AppendScript("msg " + game.Monsters[i].Name + "　" + fbs.ToString());
                        game.AppendScript("untilpress y n");
                        game.AppendScript("msg");
                        //攻击动画
                        game.AppendScript("monster " + i + " " + fbs.ToString().ToLower() + " " + ti);
                        game.AppendScript("mp3 2 " + game.Monsters[i].GetBattleMovie(fbs).BGSound.Name);
                        game.AppendScript("delay 500");
                        //被攻击动画
                        game.AppendScript("pc "+ tbs.ToString().ToLower());
                        game.AppendScript("delay 500");

                        //game.AppendScript("monster " + i + " stand");

                    }
                    else
                    {
                        PCFirst = true;

                        int fi = 0;
                        int ti = 0;

                        BattleState fbs = BattleState.Fight;
                        BattleState tbs = BattleState.Hit;
                        //提示
                        game.AppendScript("msg " + game.PCs[fi].Name + "　" + fbs.ToString());
                        game.AppendScript("untilpress y n");
                        game.AppendScript("msg");
                        //攻击动画
                        game.AppendScript("pc fight " + ti);
                        game.AppendScript("mp3 2 " + game.PCs[fi].GetBattleMovie(fbs).BGSound.Name);
                        game.AppendScript("delay 500");
                        //被攻击动画
                        game.AppendScript("monster " + ti + " " + tbs.ToString().ToLower());
                        game.AppendScript("delay 500");

                        //game.AppendScript("pc stand");
                    }
                }
                else
                {
                    InitOrder();

                    game.AppendScript("msg 第" + Round + "回合");
                    game.AppendScript("untilpress y n");
                    game.AppendScript("msg");
                }
                game.ExecuteScript();
            }
        }

        public override void Draw()
        {
            if (game.Monsters == null)
            {
                return;
            }

            DrawBGTexture();

            if (PCFirst)
            {
                foreach (Define.Monster monster in game.Monsters)
                {
                    DrawMonster(monster);
                }
                DrawCHR(game.ME);
            }
            else
            {
                DrawCHR(game.ME);
                foreach (Define.Monster monster in game.Monsters)
                {
                    DrawMonster(monster);
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
            if (mon.BattleMovie.NextFrame())
            {
                //mon.SetBattleMovie(BattleState.Stand);
            }
            Vector3 loc = mon.BattleMovie.DrawLocation;
            loc = Util.Vector3AddVector3(loc, this.ScreenOffsetPixel); 
            Color color = Util.MixColor(ColorFilter, mon.BattleMovie.ColorFilter);
            game.DrawMetalXTexture(mon.BattleMovie.MXT, mon.BattleMovie.DrawZone,loc , mon.BattleMovie.TileSize2X, 0, color);
        }

        void DrawCHR(CHR chr)
        {
            if (chr.BattleMovie == null)
            {
                return;
            }
            if (chr.BattleMovie.NextFrame())
            {
                //mon.SetBattleMovie(BattleState.Stand);
            }
            Vector3 loc = chr.BattleMovie.DrawLocation;
            loc = Util.Vector3AddVector3(loc, this.ScreenOffsetPixel);
            Color color = Util.MixColor(ColorFilter, chr.BattleMovie.ColorFilter);
            game.DrawMetalXTexture(chr.BattleMovie.MXT, chr.BattleMovie.DrawZone, loc, chr.BattleMovie.TileSize2X, 0, color);
        }

        public void GetIn(string bgt,string bgm)
        {
            BGTextureName = bgt;
            game.AppendScript("mp3 1 " + bgm + " loop");
            game.AppendScript("scn disableall");
            game.AppendScript("btl enableall");
            game.ExecuteScript();
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
            game.AppendScript("break");
            game.AppendScript("msg");
            game.AppendScript("btl disableall");
            game.AppendScript("scn enableall");
            game.ExecuteScript();
        }

        public override void OnKeyboardUpCode(object sender, int key)
        {
            Key k = (Key)key;

            if (k == Key.E)
            {
                GetOut();
            }
        }
    }
}
