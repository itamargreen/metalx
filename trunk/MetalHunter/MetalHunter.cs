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
            Location = new Point(0, 400);
            //Size = new Size(640, 120);
            //BGTextureName = "dialog-bgtexture";

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
    class MenuLoad : FormBox
    {
        public MenuLoad(Game g)
            : base(g)
        {
            Name = "MenuLoad";

            ButtonBox bb1 = new ButtonBox(g);
            bb1.OnButtonDown += new ButtonBoxEvent(bb1_OnButtonDown);
            bb1.Location = new Point();
            bb1.Size = new System.Drawing.Size(640, 120);

            bb1.WaitTextureBox.TextureName = "dialog-bgtexture";
            bb1.WaitTextureBox.Size = new System.Drawing.Size(640, 120);
            bb1.WaitTextBox.Location = new Point(16, 16);
            bb1.WaitTextBox.Text = "存档1";

            bb1.FocusTextureBox.TextureName = "dialog-bgtexture";
            bb1.FocusTextureBox.Size = new System.Drawing.Size(640, 120);
            bb1.FocusTextBox.Location = new Point(16, 18);
            bb1.FocusTextBox.Text = "存档1";
            bb1.FocusTextBox.FontColor = Color.Pink;

            bb1.DownTextureBox.TextureName = "dialog-bgtexture";
            bb1.DownTextureBox.Size = new System.Drawing.Size(640, 120);
            bb1.DownTextBox.Location = new Point(16, 18);
            bb1.DownTextBox.Text = "存档1";
            bb1.DownTextBox.FontColor = Color.Green;

            bb1.UpTextureBox.TextureName = "dialog-bgtexture";
            bb1.UpTextureBox.Size = new System.Drawing.Size(640, 120);
            bb1.UpTextBox.Location = new Point(16, 18);
            bb1.UpTextBox.Text = "存档1";
            bb1.UpTextBox.FontColor = Color.Yellow;

            ButtonBox bb2 = new ButtonBox(g);
            bb2.OnButtonDown += new ButtonBoxEvent(bb2_OnButtonDown);
            bb2.Location = new Point(0,120);
            bb2.Size = new System.Drawing.Size(640, 120);
            bb2.WaitTextureBox.TextureName = "dialog-bgtexture";
            bb2.WaitTextureBox.Size = new System.Drawing.Size(640, 120);
            bb2.WaitTextBox.Location = new Point(16, 16);
            bb2.WaitTextBox.Text = "存档2";

            bb2.FocusTextureBox.TextureName = "dialog-bgtexture";
            bb2.FocusTextureBox.Size = new System.Drawing.Size(640, 120);
            bb2.FocusTextBox.Location = new Point(16, 18);
            bb2.FocusTextBox.Text = "存档2";
            bb2.FocusTextBox.FontColor = Color.Pink;

            ButtonBox bb3 = new ButtonBox(g);
            bb3.OnButtonDown += new ButtonBoxEvent(bb3_OnButtonDown);
            bb3.Location = new Point(0, 240);
            bb3.Size = new System.Drawing.Size(640, 120);
            bb3.WaitTextureBox.TextureName = "dialog-bgtexture";
            bb3.WaitTextureBox.Size = new System.Drawing.Size(640, 120);
            bb3.WaitTextBox.Location = new Point(16, 16);
            bb3.WaitTextBox.Text = "存档3";

            bb3.FocusTextureBox.TextureName = "dialog-bgtexture";
            bb3.FocusTextureBox.Size = new System.Drawing.Size(640, 120);
            bb3.FocusTextBox.Location = new Point(16, 18);
            bb3.FocusTextBox.Text = "存档3";
            bb3.FocusTextBox.FontColor = Color.Pink;
            
            ButtonBox bb4 = new ButtonBox(g);
            bb4.Location = new Point(0, 360);
            bb4.Size = new System.Drawing.Size(640, 120);
            bb4.WaitTextureBox.TextureName = "dialog-bgtexture";
            bb4.WaitTextureBox.Size = new System.Drawing.Size(640, 120);
            bb4.WaitTextBox.Location = new Point(16, 16);
            bb4.WaitTextBox.Text = "存档4";

            bb4.FocusTextureBox.TextureName = "dialog-bgtexture";
            bb4.FocusTextureBox.Size = new System.Drawing.Size(640, 120);
            bb4.FocusTextBox.Location = new Point(16, 18);
            bb4.FocusTextBox.Text = "存档4";
            bb4.FocusTextBox.FontColor = Color.Pink;

            ControlBoxes.Add(bb1);
            ControlBoxes.Add(bb2);
            ControlBoxes.Add(bb3);
            ControlBoxes.Add(bb4);
        }

        void bb3_OnButtonDown(object arg)
        {
            game.NetManager.Send(new byte[] { 50, 51, 52, 53 });
        }

        void bb2_OnButtonDown(object arg)
        {
            game.NetManager.Connect("192.168.1.15", 1987);
        }

        void bb1_OnButtonDown(object arg)
        {
            game.AppendScript(@"gui close all");
            game.AppendScript(@"enter scenes\mmr\test.mxscene 0 0");
            game.AppendScript(@"me jump 12 7");
            game.AppendScript(@"me skin mmr-chrs0001");
            game.ExecuteScript();
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
        }

        public MetalHunter()
        {
            game = new Game("MetalHunter");

            game.InitData();
            game.InitCom();

            game.LoadAllDotPNG(@".\", new Size(24, 24));

            InitFormBoxes();
            
            game.ExecuteMetalXScript("script");

            game.Start();
        }
        [STAThread]
        static void Main()
        {
            new MetalHunter();
        }
    }
}
