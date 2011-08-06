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
        public BattleManager(Game g)
            : base(g)
        { 

        }

        public override void Code()
        {
        }

        public override void Draw()
        {
            if (game.Monsters == null)
            {
                return;
            }

            foreach (Define.Monster monster in game.Monsters)
            {                
                DrawMonster(monster);
            }

            DrawCHR(game.ME);
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
            Color color = Util.MixColor(ColorFilter, mon.BattleMovie.ColorFilter);
            game.DrawMetalXTexture(mon.BattleMovie.MXT, mon.BattleMovie.DrawZone, mon.BattleMovie.DrawLocation, mon.BattleMovie.TileSize2X, 0, color);
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
            Color color = Util.MixColor(ColorFilter, chr.BattleMovie.ColorFilter);
            game.DrawMetalXTexture(chr.BattleMovie.MXT, chr.BattleMovie.DrawZone, chr.BattleMovie.DrawLocation, chr.BattleMovie.TileSize2X, 0, color);
        }

        public void GetIn(List<string> monsterNames)
        {
            game.StopAudio();

            foreach (string name in monsterNames)
            {
                Define.Monster monster = game.LoadDotMXMonster(game.MonsterFiles[name].FullName);
                game.Monsters.Add(monster);
            }

            for (int i = 0; i < game.Monsters.Count; i++)
            {
                Define.Monster monster = game.Monsters[i];
                monster.BattleLocation = new Microsoft.DirectX.Vector3((i / 4 + 1) * 96 - monster.BattleSize.Width, (i % 4 + 1) * 96 - monster.BattleSize.Height, 0);
                monster.LoadBattleMovies(game);
            }

            game.ME.BattleLocation = new Vector3(540, 100, 0);
            game.ME.LoadBattleMovies(game);

            game.SceneManager.DisableAll();
            game.SceneManager.Visible = false;
            Visible = true;
        }

        public void GetOut()
        {
            game.Monsters.Clear();
            game.PlayMP3Audio(1, game.AudioFiles[game.SCN.BGMusicNames[0]].FullName, true);
            game.SceneManager.EnableAll();
            game.SceneManager.Visible = true;
            Visible = false;
        }
    }
}
