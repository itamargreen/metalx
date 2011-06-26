using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using MetalX;
using MetalX.Data;

namespace MetalHunter
{
    class FB1 : FormBox
    {
        public FB1(Game g)
            : base(g)
        {
            this.Location = new Point(10, 10);
            this.Size=new Size(640, 120);
            this.BGTextureName = "dialog-bgtexture";
            MetalX.Data.TextBox TB1 = new MetalX.Data.TextBox(g);
            TB1.Text = "测试字体";
            TB1.OneByOne = true;
            this.ControlBoxes.Add(TB1);
        }
    }
    class MetalHunter
    {
        Game game;

        public void InitFormBoxes()
        {
            game.FormBoxes.LoadDotMXFormBox(new FB1(game));
        }

        public MetalHunter()
        {
            game = new Game("MetalHunter");
          
            game.Init();            

            game.LoadAllDotPNG(@".\");
            game.LoadAllDotMP3(@".\");

            InitFormBoxes();

            game.Scenes.LoadDotMXScene(game, @"scenes\test.mxscene");
            
            game.Start();
        }

        static void Main()
        {
            MetalHunter MH = new MetalHunter();
        }
    }
}
