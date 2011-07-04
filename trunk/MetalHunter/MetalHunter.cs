﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Drawing;

using MetalX;
using MetalX.Data;

namespace MetalHunter
{
    class LogoEngine : FormBox
    {
        public LogoEngine(Game g)
            :base(g)
        {
            Name = "LogoEngine";
            Location = new Point();
            BGTextureBox.TextureName = "engine-logo";
            BGTextureBox.Size = new Size(640, 480);
        }
    }
    class LogoGame : FormBox
    {
        public LogoGame(Game g)
            : base(g)
        {
            Name = "LogoGame";
            Location = new Point(0, 360);
            //Size = new Size(640, 120);
            //BGTextureName = "dialog-bgtexture";

            TextBox tb1 = new TextBox(g);
            tb1.Location = new Point(16, 16);
            tb1.Text = "MetalHunter!";
            tb1.TextFont = "Consolas";
            tb1.TextFontSize = 36;
            tb1.Interval = 200;
            tb1.OneByOne = true;

            ControlBoxes.Add(tb1);
        }
    }
    class MenuLoad : FormBox
    {
        public MenuLoad(Game g)
            : base(g)
        {
            Name = "MenuLoad";

            ButtonBox bb1 = new ButtonBox(g);
            bb1.Location = new Point();
            bb1.Size = new System.Drawing.Size(640, 120);
            bb1.WaitTextureBox.TextureName = "dialog-bgtexture";
            bb1.WaitTextureBox.Size = new System.Drawing.Size(640, 120);
            bb1.WaitTextBox.Location = new Point(16, 16);
            bb1.WaitTextBox.Text = "存档1";

            ButtonBox bb2 = new ButtonBox(g);
            bb2.Location = new Point(0,120);
            bb2.Size = new System.Drawing.Size(640, 120);
            bb2.WaitTextureBox.TextureName = "dialog-bgtexture";
            bb2.WaitTextureBox.Size = new System.Drawing.Size(640, 120);
            bb2.WaitTextBox.Location = new Point(16, 16);
            bb2.WaitTextBox.Text = "存档2";

            ButtonBox bb3 = new ButtonBox(g);
            bb3.Location = new Point(0, 240);
            bb3.Size = new System.Drawing.Size(640, 120);
            bb3.WaitTextureBox.TextureName = "dialog-bgtexture";
            bb3.WaitTextureBox.Size = new System.Drawing.Size(640, 120);
            bb3.WaitTextBox.Location = new Point(16, 16);
            bb3.WaitTextBox.Text = "存档3"; 
            
            ButtonBox bb4 = new ButtonBox(g);
            bb4.Location = new Point(0, 360);
            bb4.Size = new System.Drawing.Size(640, 120);
            bb4.WaitTextureBox.TextureName = "dialog-bgtexture";
            bb4.WaitTextureBox.Size = new System.Drawing.Size(640, 120);
            bb4.WaitTextBox.Location = new Point(16, 16);
            bb4.WaitTextBox.Text = "存档4";

            ControlBoxes.Add(bb1);
            ControlBoxes.Add(bb2);
            ControlBoxes.Add(bb3);
            ControlBoxes.Add(bb4);
        }
    }
    class MessageBox : FormBox
    {
        public MessageBox(Game g)
            : base(g)
        {
            Name = "MessageBox";
            Location = new Point(0, 360);

            TextBox tb1 = new TextBox(g);
            tb1.Location = new Point(16, 16);
            tb1.Text = "MessageBox!";
            tb1.TextFont = "Consolas";
            tb1.Interval = 200;
            tb1.OneByOne = true;

            ControlBoxes.Add(tb1);
        }
    }
    class MetalHunter
    {
        Game game;

        void InitFormBoxes()
        {
            game.FormBoxes.LoadDotMXFormBox(new LogoEngine(game));
            game.FormBoxes.LoadDotMXFormBox(new LogoGame(game));
            game.FormBoxes.LoadDotMXFormBox(new MenuLoad(game));
            game.FormBoxes.LoadDotMXFormBox(new MessageBox(game));
        }

        public MetalHunter()
        {
            game = new Game("MetalHunter");

            game.InitData();
            game.InitCom();

            game.LoadAllDotPNG(@".\", new Size(16, 16));

            InitFormBoxes();
            
            game.ExecuteMetalXScript("script");

            game.Start();
        }

        static void Main()
        {
            new MetalHunter();
        }
    }
}
