using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;

using MetalX;
using MetalX.Define;

namespace MetalHunter
{
    class MetalHunter
    {
        Game game;

        void InitFormBoxes()
        {
            game.FormBoxes.Add(new LogoEngine(game));
            game.FormBoxes.Add(new LogoGame(game));
            game.FormBoxes.Add(new MenuLoad(game));
            game.FormBoxes.Add(new MenuCHR(game));
            game.FormBoxes.Add(new MenuBAG(game));
            game.FormBoxes.Add(new MenuBAGASK(game));
            game.FormBoxes.Add(new MenuBattleCHR(game));

            game.OverLoadMessageBox(new MH_MSGBox(game));
            game.OverLoadASKboolBox(new MH_ASKboolBox(game));
        }

        void InitItems()
        {
            game.Items.Add(new 弹弓());
            game.Items.Add(new 狩猎弩());
            game.Items.Add(new 双管猎枪());

            game.Items.Add(new 运动服());
            game.Items.Add(new 运动裤());
            game.Items.Add(new 登山鞋());
            game.Items.Add(new 粗线手套());
            game.Items.Add(new 棒球帽());

            game.Items.Add(new 恢复胶囊小());
            game.Items.Add(new 爆竹());
        }

        public MetalHunter()
        {
            game = new Game("MetalHunter");

            game.InitData();
            game.InitCom();

            game.LoadAllDotPNG(new Size(16, 16));
            game.LoadAllDotMP3();

            game.LoadAllDotMXScene();
            game.LoadAllDotMXNPC();
            game.LoadAllDotMXScript();
            game.LoadAllDotMXMovie();
            game.LoadAllDotMXMonster();

            InitFormBoxes();
            InitItems();
            
            game.ScriptManager.AppendDotMetalXScript("logo");
            game.ScriptManager.Execute();

            game.FormBoxManager.OnKeyUp += new KeyboardEvent(FormBoxManager_OnKeyUp);
            game.SceneManager.OnKeyUp += new KeyboardEvent(SceneManager_OnKeyUp);

            game.Start();
        }

        void FormBoxManager_OnKeyUp(object sender, int key)
        {
        }

        void SceneManager_OnKeyUp(object sender, int key)
        {
            Key k = (Key)key;
            if (k == Key.C)
            {
                ((MenuCHR)game.FormBoxes["MenuCHR"]).LoadContext(game.ME);
                game.FormBoxManager.Appear("MenuCHR");
                //game.SceneManager.Controllable = false;
            }
            else if (k == Key.B)
            {
                game.FormBoxManager.Disappear("MenuCHR");
                ((MenuBAG)game.FormBoxes["MenuBAG"]).LoadContext(game.ME);
                game.FormBoxManager.Appear("MenuBAG");
                //game.SceneManager.Controllable = false;
            }
            //else if (k == Key.P)
            //{
            //    MetalX.File.MetalXMovie movie = (MetalX.File.MetalXMovie)Util.LoadObject(game.MovieFiles["firegun"].FullName);
            //    movie.MXT.Init(game.Devices.D3DDev);
            //    game.MovieManager.PlayMovie(movie, new Vector3(300, 120, 0), new Vector3(100, 120, 0), 300);
            //}
        }

        [STAThread]
        static void Main()
        {
            new MetalHunter();
        }
    }
}
