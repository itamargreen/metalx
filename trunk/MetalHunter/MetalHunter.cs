using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using MetalX;
using MetalX.Data;

namespace MetalHunter
{
    class EngineLogo : FormBox
    {
        public EngineLogo(Game g)
            :base(g)
        {
            Name = "EngineLogo";
            Location = new Point();
            Size = new Size(640, 480);
            BGTextureName = "engine-logo";
        }
    }
    class FB1 : FormBox
    {
        public FB1(Game g)
            : base(g)
        {
            Name = "DialogBox";
            Location = new Point(0,360);
            Size = new Size(640, 120);
            BGTextureName = "dialog-bgtexture";

            MetalX.Data.TextBox TB1 = new MetalX.Data.TextBox(g);
            TB1.Location = new Point(16, 16);
            TB1.Text = "MetalHunter!";
            TB1.Interval = 200;
            TB1.OneByOne = true;

            ControlBoxes.Add(TB1);
        }

        public override void OnFormBoxDisappearCode()
        {
            game.Exit();
        }
    }
    class MetalHunter
    {
        Game game;

        void InitFormBoxes()
        {
            game.FormBoxes.LoadDotMXFormBox(new EngineLogo(game));
            game.FormBoxes.LoadDotMXFormBox(new FB1(game));
        }

        void ShowLogo()
        {
            List<FormBoxes2Play> fb2p = new List<FormBoxes2Play>();
            List<TextureEffect> lte = new List<TextureEffect>();
            lte.Add(new TextureEffect(TextureEffectType.Shock, 3000, false));
            lte.Add(new TextureEffect(TextureEffectType.FallIn, 1000, true));
            lte.Add(new TextureEffect(TextureEffectType.None, 1000, true));
            lte.Add(new TextureEffect(TextureEffectType.FallOut, 1000, true));
            fb2p.Add(new FormBoxes2Play("EngineLogo", lte));
            fb2p.Add(new FormBoxes2Play("DialogBox", lte));

            game.PlayFormBox(fb2p);
        }

        public MetalHunter()
        {
            game = new Game("MetalHunter");

            game.InitData();
            game.InitCom();

            game.LoadAllDotPNG(@".\", new Size(16, 16));
            game.LoadAllDotMP3(@".\");

            InitFormBoxes();

            ShowLogo();

            game.Start();
        }

        static void Main()
        {
            MetalHunter MH = new MetalHunter();
        }
    }
}
