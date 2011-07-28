using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using MetalX;
using MetalX.Data;

namespace MetalHunter
{
    public class LogoEngine : FormBox
    {
        public LogoEngine(Game g)
            : base(g)
        {
            Name = "LogoEngine";
            Location = new Point();
            BGTextureBox.TextureName = "ning-engine-logo";
            BGTextureBox.Size = new Size(640, 480);
        }
    }
    public class LogoGame : FormBox
    {
        public LogoGame(Game g)
            : base(g)
        {
            Name = "LogoGame";
            Location = new Point(0, 400);
            //Size = new Size(640, 120);
            //BGTextureName = "ning-dialogbg";

            TextBox tb1 = new TextBox(g);
            tb1.Location = new Point(16, 16);
            tb1.Text = "MetalHunter!";
            tb1.FontName = "Consolas";
            tb1.FontSize = 36;
            tb1.Interval = 200;
            tb1.OneByOne = true;

            ControlBoxes.Add(tb1);
        }
    }
    public class MenuLoad : FormBox
    {
        public MenuLoad(Game g)
            : base(g)
        {
            Name = "MenuLoad";

            ButtonBox bb1 = new ButtonBox(g);
            bb1.OnButtonDown += new ButtonBoxEvent(bb1_OnButtonDown);
            bb1.Location = new Point();
            bb1.Size = new System.Drawing.Size(512, 128);

            bb1.WaitTextureBox.TextureName = "ning-dialogbg";
            bb1.WaitTextureBox.Size = new System.Drawing.Size(512, 128);
            bb1.WaitTextBox.Location = new Point(32, 32);
            bb1.WaitTextBox.Text = "存档1";

            bb1.FocusTextureBox.TextureName = "ning-dialogbg";
            bb1.FocusTextureBox.Size = new System.Drawing.Size(512, 128);
            bb1.FocusTextBox.Location = new Point(32, 32);
            bb1.FocusTextBox.Text = "存档1";
            bb1.FocusTextBox.FontColor = Color.Pink;

            bb1.DownTextureBox.TextureName = "ning-dialogbg";
            bb1.DownTextureBox.Size = new System.Drawing.Size(512, 128);
            bb1.DownTextBox.Location = new Point(32, 32);
            bb1.DownTextBox.Text = "存档1";
            bb1.DownTextBox.FontColor = Color.Green;

            bb1.UpTextureBox.TextureName = "ning-dialogbg";
            bb1.UpTextureBox.Size = new System.Drawing.Size(512, 128);
            bb1.UpTextBox.Location = new Point(32, 32);
            bb1.UpTextBox.Text = "存档1";
            bb1.UpTextBox.FontColor = Color.Yellow;

            ButtonBox bb2 = new ButtonBox(g);
            bb2.OnButtonDown += new ButtonBoxEvent(bb2_OnButtonDown);
            bb2.Location = new Point(0, 128);
            bb2.Size = new System.Drawing.Size(512, 128);
            bb2.WaitTextureBox.TextureName = "ning-dialogbg";
            bb2.WaitTextureBox.Size = new System.Drawing.Size(512, 128);
            bb2.WaitTextBox.Location = new Point(32, 32);
            bb2.WaitTextBox.Text = "存档2";

            bb2.FocusTextureBox.TextureName = "ning-dialogbg";
            bb2.FocusTextureBox.Size = new System.Drawing.Size(512, 128);
            bb2.FocusTextBox.Location = new Point(32, 32);
            bb2.FocusTextBox.Text = "存档2";
            bb2.FocusTextBox.FontColor = Color.Pink;

            ButtonBox bb3 = new ButtonBox(g);
            bb3.OnButtonDown += new ButtonBoxEvent(bb3_OnButtonDown);
            bb3.Location = new Point(0, 256);
            bb3.Size = new System.Drawing.Size(512, 128);
            bb3.WaitTextureBox.TextureName = "ning-dialogbg";
            bb3.WaitTextureBox.Size = new System.Drawing.Size(512, 128);
            bb3.WaitTextBox.Location = new Point(32, 32);
            bb3.WaitTextBox.Text = "存档3";

            bb3.FocusTextureBox.TextureName = "ning-dialogbg";
            bb3.FocusTextureBox.Size = new System.Drawing.Size(512, 128);
            bb3.FocusTextBox.Location = new Point(32, 32);
            bb3.FocusTextBox.Text = "存档3";
            bb3.FocusTextBox.FontColor = Color.Pink;

            ButtonBox bb4 = new ButtonBox(g);
            bb4.Location = new Point(0, 384);
            bb4.Size = new System.Drawing.Size(512, 128);
            bb4.WaitTextureBox.TextureName = "ning-dialogbg";
            bb4.WaitTextureBox.Size = new System.Drawing.Size(512, 128);
            bb4.WaitTextBox.Location = new Point(32, 32);
            bb4.WaitTextBox.Text = "存档4";

            bb4.FocusTextureBox.TextureName = "ning-dialogbg";
            bb4.FocusTextureBox.Size = new System.Drawing.Size(512, 128);
            bb4.FocusTextBox.Location = new Point(32, 32);
            bb4.FocusTextBox.Text = "存档4";
            bb4.FocusTextBox.FontColor = Color.Pink;

            ControlBoxes.Add(bb1);
            ControlBoxes.Add(bb2);
            ControlBoxes.Add(bb3);
            //ControlBoxes.Add(bb4);
        }

        void bb3_OnButtonDown(object arg)
        {
            game.NetManager.Send(new byte[] { 50, 51, 52, 53 });
        }

        void bb2_OnButtonDown(object arg)
        {
            game.NetManager.Connect(game.Options.ServerIP, game.Options.ServerPort);
        }

        void bb1_OnButtonDown(object arg)
        {
            game.AppendDotMetalXScript("load1");
            game.ExecuteScript();
        }
    }

    public class MH_MSGBox : MetalX.Resource.MSGBox
    {
        public MH_MSGBox(Game g)
            : base(g)
        {
            Location = new Point(64, 480 - 128 - 8);

            BGTextureBox.TextureName = "ning-dialogbg";
            BGTextureBox.Size = new Size(512, 128);

            TextBox.Location = new Point(20, 20);
            TextBox.OneByOne = true;

            TextBox.FontName = "微软雅黑";
            TextBox.FontSize = 15;
        }
    }

    public class MH_ASKboolBox : MetalX.Resource.ASKboolBox
    {
        public MH_ASKboolBox(Game g)
            : base(g)
        {
            Location = new Point(64, 480 - 128 - 8);

            BGTextureBox.TextureName = "ning-dialogbg";
            BGTextureBox.Size = new Size(512, 128);

            TextBox.Location = new Point(20, 20);
            TextBox.OneByOne = true;

            TextBox.FontName = "微软雅黑";
            TextBox.FontSize = 15;
        }
    }
}
