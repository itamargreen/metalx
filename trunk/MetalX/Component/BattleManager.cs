using System;
using System.Collections.Generic;
using System.Drawing;

using Microsoft.DirectX;

using MetalX.Define;
using MetalX.File;

namespace MetalX.Component
{
    public class BattleManager : GameCom
    {
        string BGTextureName;
        int Round = 0;

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
                        i++;
                        i = -i;

                        BattleState bs = game.Monsters[i].AI();

                        game.AppendScript("msg " + game.Monsters[i].Name + "　" + bs.ToString());
                        game.AppendScript("untilpress y n");
                        game.AppendScript("msg");

                        game.AppendScript("monster " + i + " " + bs.ToString().ToLower());
                        game.AppendScript("mp3 2 " + game.Monsters[i].GetBattleMovie(bs).BGSound.Name);
                        game.AppendScript("delay 1000");
                        game.AppendScript("monster " + i + " stand");

                    }
                    else
                    {
                        BattleState bs = BattleState.Fight;

                        game.AppendScript("msg " + game.ME.Name + "　" + bs.ToString());
                        game.AppendScript("untilpress y n");
                        game.AppendScript("msg");

                        game.AppendScript("me fight");
                        game.AppendScript("mp3 2 " + game.ME.GetBattleMovie(bs).BGSound.Name);
                        game.AppendScript("delay 1000");
                        game.AppendScript("me stand");
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


            foreach (Define.Monster monster in game.Monsters)
            {                
                DrawMonster(monster);
            }

            DrawCHR(game.ME);
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

        public void GetIn(string bgt, List<string> monsterNames)
        {
            BGTextureName = bgt;

            foreach (string name in monsterNames)
            {
                Define.Monster monster = game.LoadDotMXMonster(game.MonsterFiles[name].FullName);
                game.Monsters.Add(monster);
            }

            for (int i = 0; i < game.Monsters.Count; i++)
            {
                Define.Monster monster = game.Monsters[i];
                monster.BattleLocation = new Microsoft.DirectX.Vector3(32 + (i / 4) * 80, 192 + (i % 4) * 80 - monster.BattleSize.Height, 0);
                monster.LoadBattleMovies(game);
            }

            game.ME.BattleLocation = new Vector3(576 - game.ME.BattleSize.Width, 192 + 0 - game.ME.BattleSize.Height, 0);
            game.ME.LoadBattleMovies(game);

            game.SceneManager.DisableAll();
            Visible = true;
            Enable = true;
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
            game.PlayMP3Audio(1, game.AudioFiles[game.SCN.BGMusicNames[0]].FullName, true);
            game.Monsters.Clear();
            game.SceneManager.EnableAll();
            game.SceneManager.Visible = true;
            Visible = false;
            Enable = false;
        }
    }
}
