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
            //BGTextureName = "smallbg";

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
        List<ButtonBox> bb = new List<ButtonBox>();

        public MenuLoad(Game g)
            : base(g)
        {
            Name = "MenuLoad";

            for (int i = 0; i < 3; i++)
            {
                bb.Add(new ButtonBox(g));
                bb[i].Location = new Point(64, 48 + 128 * i);
                bb[i].Size = new System.Drawing.Size(512, 128);
                bb[i].WaitTextureBox.TextureName = "smallbg";
                bb[i].FocusTextureBox.TextureName = "smallbg";
                bb[i].DownTextureBox.TextureName = "smallbg";
                bb[i].UpTextureBox.TextureName = "smallbg";
                bb[i].WaitTextureBox.Size = new System.Drawing.Size(512, 128);
                bb[i].FocusTextureBox.Size = new System.Drawing.Size(512, 128);
                bb[i].DownTextureBox.Size = new System.Drawing.Size(512, 128);
                bb[i].UpTextureBox.Size = new System.Drawing.Size(512, 128);

                bb[i].WaitTextBox.Text = "存档" + (i + 1).ToString();
                bb[i].WaitTextBox.Location = new Point(32, 32);
                bb[i].FocusTextBox.Text = "存档" + (i + 1).ToString();
                bb[i].FocusTextBox.Location = new Point(32, 32);
                bb[i].FocusTextBox.FontColor = Color.CornflowerBlue;
                bb[i].DownTextBox.Text = "存档" + (i + 1).ToString();
                bb[i].DownTextBox.Location = new Point(32, 32);
                bb[i].DownTextBox.FontColor = Color.Yellow;
                bb[i].UpTextBox.Text = "存档" + (i + 1).ToString();
                bb[i].UpTextBox.Location = new Point(32, 32);

                ControlBoxes.Add(bb[i]);
            }

            bb[0].OnButtonUp += new ButtonBoxEvent(MenuLoad_OnButtonUp0);
            bb[1].OnButtonUp += new ButtonBoxEvent(MenuLoad_OnButtonUp1);
            bb[2].OnButtonUp += new ButtonBoxEvent(MenuLoad_OnButtonUp2);
        }


        void MenuLoad_OnButtonUp0(object arg)
        {
            game.AppendDotMetalXScript("load0");
            game.ExecuteScript();
        }
        void MenuLoad_OnButtonUp1(object arg)
        {
            game.AppendDotMetalXScript("load1");
            game.ExecuteScript();

        }
        void MenuLoad_OnButtonUp2(object arg)
        {
            game.AppendDotMetalXScript("load2");
            game.ExecuteScript();

        }

    }
    public class MenuCHR : FormBox
    {
        TextureBox head;
        TextBox nam; 
        TextBox exp; 
        TextBox lv; 
        TextBox hp; 
        TextBox def;
        TextBox str;
        TextBox agi;
        TextBox itl;
        TextBox chrlv;
        TextBox mtllv;

        TextBox dmg1;
        TextBox dmg2;

        public MenuCHR(Game g)
            : base(g)
        {
            Name = "MenuCHR";
            Location = new Point(64, 48);

            BGTextureBox.TextureName = "bigbg";
            BGTextureBox.Size = new Size(512, 512);
            //BGTextureBox.TextureFliterColor = Color.FromArgb(200, Color.White);

            head = new TextureBox(g);
            head.Location = new Point(32, 32);

            nam = new TextBox(g);
            nam.Location = new Point(32, 160);


            lv = new TextBox(g);
            lv.Location = new Point(200, 64-16);
            
            chrlv = new TextBox(g);
            chrlv.Location = new Point(200, 96-16);

            mtllv = new TextBox(g);
            mtllv.Location = new Point(200, 128-16);

            exp = new TextBox(g);
            exp.Location = new Point(32, 320);


            hp = new TextBox(g);
            hp.Location = new Point(200, 160);

            itl = new TextBox(g);
            itl.Location = new Point(32, 224 - 16);
            str = new TextBox(g);
            str.Location = new Point(32, 256 - 16);
            agi = new TextBox(g);
            agi.Location = new Point(32, 288 - 16);


            def = new TextBox(g);
            def.Location = new Point(200, 288 - 16);
            dmg1 = new TextBox(g);
            dmg1.Location = new Point(200, 224 - 16);
            dmg2 = new TextBox(g);
            dmg2.Location = new Point(200, 256 - 16);

            ControlBoxes.Add(head);
            ControlBoxes.Add(nam);
            ControlBoxes.Add(lv);
            ControlBoxes.Add(hp);

            ControlBoxes.Add(dmg1);
            ControlBoxes.Add(dmg2);
            ControlBoxes.Add(def);

            ControlBoxes.Add(str);
            ControlBoxes.Add(agi);
            ControlBoxes.Add(itl);

            ControlBoxes.Add(chrlv);
            ControlBoxes.Add(mtllv);

            ControlBoxes.Add(exp);
        }

        public void LoadContext(CHR chr)
        {
            chr.HeadTextureName = "head-" + chr.TextureName;

            this.head.TextureIndex = chr.HeadTextureIndex;
            this.head.TextureName = chr.HeadTextureName;

            this.nam.Text = "　　名字： " + chr.Name;

            this.hp.Text = "　　ＨＰ： " + chr.HP + " / " + chr.HPMax;

            this.lv.Text = "　　ＬＶ： " + chr.Level;
            this.chrlv.Text = "人战ＬＶ： " + chr.CHRLevel;
            this.mtllv.Text = "车战ＬＶ： " + chr.MTLLevel;

            this.exp.Text = "　经验值： " + chr.EXP;
            try
            {
                this.dmg1.Text = "攻击力１： " + chr.PrimaryWeapon.Damage;
                this.dmg2.Text = "攻击力２： " + chr.SecondryWeapon.Damage;
                this.def.Text = "　防御力： " + chr.Defense;
            }
            catch
            {
                this.dmg1.Text = "攻击力１： 0";
                this.dmg2.Text = "攻击力２： 0";
                this.def.Text = "　防御力： 0";
            }
            this.str.Text = "　　力量： " + chr.Strength;
            this.agi.Text = "　　敏捷： " + chr.Agility;
            this.itl.Text = "　　智力： " + chr.Intelligence;
        }
    }
    public class MenuBAG : FormBox
    {
        List<ButtonBox> bb = new List<ButtonBox>();
        public MenuBAG(Game g)
            : base(g)
        {
            Name = "MenuBAG";
            Location = new Point(64, 48);

            BGTextureBox.TextureName = "bigbg";
            BGTextureBox.Size = new Size(512, 512);

            for (int i = 0; i < 16; i++)
            {
                bb.Add(new ButtonBox(game));
            }
        }

        public void LoadBag(CHR chr)
        {
            ControlBoxes.Clear();
            ControlBoxes.Add(BGTextureBox);
            for (int i = 0; i < chr.Bag.Count; i++)
            {
                ////bb[i] = new ButtonBox(game);
                //if (i >= chr.Bag.Count)
                //{
                //    continue;
                //}
                if (chr.Bag[i] == null)
                {
                    continue;
                }
                bb[i].WaitTextBox.Text = "　　"+chr.Bag[i].Name;
                bb[i].FocusTextBox.Text = "＞　" + chr.Bag[i].Name;
                bb[i].FocusTextBox.FontColor = Color.CornflowerBlue;
                bb[i].DownTextBox.Text = "＞　" + chr.Bag[i].Name;
                bb[i].DownTextBox.FontColor = Color.Yellow;
                bb[i].UpTextBox.Text = "＞　" + chr.Bag[i].Name;
                bb[i].Location = new Point(32 + 128 * (i / 8), 64 + 32 * (i % 8));

                ControlBoxes.Add(bb[i]);
            }
        }
    }

    public class MH_MSGBox : MetalX.Resource.MSGBox
    {
        public MH_MSGBox(Game g)
            : base(g)
        {
            Location = new Point(64, 480 - 128 - 8);

            BGTextureBox.TextureName = "smallbg";
            BGTextureBox.Size = new Size(512, 128);
            //BGTextureBox.TextureFliterColor = Color.FromArgb(200, Color.White);

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

            BGTextureBox.TextureName = "smallbg";
            BGTextureBox.Size = new Size(512, 128);
            //BGTextureBox.TextureFliterColor = Color.FromArgb(200, Color.White);

            TextBox.Location = new Point(20, 20);
            TextBox.OneByOne = true;

            TextBox.FontName = "微软雅黑";
            TextBox.FontSize = 15;
        }
    }
}
