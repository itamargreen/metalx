using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using MetalX;
using MetalX.Define;

namespace MetalHunter
{
    public class LogoEngine : FormBox
    {
        public LogoEngine(Game g)
            : base(g)
        {
            Name = "LogoEngine";
            Location = new Point();
            BGTextureBox.TextureName = "logo_engine";
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
            //BGTextureName = "bg-256x64";

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
                bb[i].WaitTextureBox.TextureName = "bg_256x64";
                bb[i].FocusTextureBox.TextureName = "bg_256x64";
                bb[i].DownTextureBox.TextureName = "bg_256x64";
                bb[i].UpTextureBox.TextureName = "bg_256x64";
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


        void MenuLoad_OnButtonUp0(object sender, object arg)
        {
            game.AppendDotMetalXScript("load0");
            game.ExecuteScript();
        }
        void MenuLoad_OnButtonUp1(object sender, object arg)
        {
            game.AppendDotMetalXScript("load1");
            game.ExecuteScript();
        }
        void MenuLoad_OnButtonUp2(object sender, object arg)
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
        TextBox phy;
        TextBox chrlv;
        TextBox mtllv;

        TextBox dmg;

        ButtonBox b_weapon;
        List<ButtonBox> b_def;

        public MenuCHR(Game g)
            : base(g)
        {
            Name = "MenuCHR";
            Location = new Point(64, 48);

            BGTextureBox.TextureName = "bg_256x256";
            BGTextureBox.Size = new Size(512, 512);

            int line = 5;

            head = new TextureBox(g);
            head.Location = new Point(16, 16);
            
            nam = new TextBox(g);
            nam.Location = new Point(24, line++ * 24 - 4);

            exp = new TextBox(g);
            exp.Location = new Point(24, line++ * 24 - 4);


            lv = new TextBox(g);
            lv.Location = new Point(24, line++ * 24);

            hp = new TextBox(g);
            hp.Location = new Point(24, line++ * 24);

            
            itl = new TextBox(g);
            itl.Location = new Point(24, line++ * 24);

            str = new TextBox(g);
            str.Location = new Point(24, line++ * 24);

            agi = new TextBox(g);
            agi.Location = new Point(24, line++ * 24);

            phy = new TextBox(g);
            phy.Location = new Point(24, line++ * 24);


            mtllv = new TextBox(g);
            mtllv.Location = new Point(24, line++ * 24 + 4);

            chrlv = new TextBox(g);
            chrlv.Location = new Point(24, line++ * 24 + 4);

            line = 7;

            dmg = new TextBox(g);
            dmg.Location = new Point(224, line++ * 24);

            b_weapon = new ButtonBox(g);
            b_weapon.Location = new Point(224, line++ * 24);

            def = new TextBox(g);
            def.Location = new Point(224, line++ * 24);

            ControlBoxes.Add(head);
            ControlBoxes.Add(nam);
            ControlBoxes.Add(lv);
            ControlBoxes.Add(hp);

            ControlBoxes.Add(dmg);
            ControlBoxes.Add(def);

            ControlBoxes.Add(str);
            ControlBoxes.Add(agi);
            ControlBoxes.Add(itl);
            ControlBoxes.Add(phy);

            ControlBoxes.Add(chrlv);
            ControlBoxes.Add(mtllv);

            ControlBoxes.Add(exp);

            ControlBoxes.Add(b_weapon);

            b_def = new List<ButtonBox>();
            for (int i = 0; i < 7; i++)
            {
                b_def.Add(new ButtonBox(g));
                b_def[i].Location = new Point(224, line * 24 + i * 18);
                ControlBoxes.Add(b_def[i]);
            }
        }

        public void LoadContext(CHR chr)
        {
            chr.HeadTextureName = "icon_" + chr.TextureName;

            this.head.TextureIndex = chr.HeadTextureIndex;
            this.head.TextureName = chr.HeadTextureName;

            this.nam.Text = "名字：　" + chr.Name;

            this.hp.Text = "ＨＰ：　" + chr.HP + " / " + chr.HPMax;

            this.lv.Text = "ＬＶ：　" + chr.Level;
            this.chrlv.Text = "普通战斗ＬＶ：　" + chr.CBLevel;
            this.mtllv.Text = "驾驶战斗ＬＶ：　" + chr.MBLevel;

            this.exp.Text = "经验：　" + chr.EXP;
            try
            {
                this.dmg.Text = "攻击：　" + chr.Weapon.Damage + "　　命中：　" + chr.Accurate + "%";
                this.def.Text = "防御：　" + chr.Defense + "　　闪避：　" + chr.Missrate + "%";
            }
            catch
            {
                this.dmg.Text = "攻击：　0";
                this.def.Text = "防御：　0";
            }
            this.str.Text = "力量：　" + chr.Strength;
            this.agi.Text = "敏捷：　" + chr.Agility;
            this.itl.Text = "智力：　" + chr.Intelligence;
            this.phy.Text = "体质：　" + chr.Physique;

            if (chr.Equipments[(int)EquipmentCHRType.Weapon].Name != null && chr.Equipments[(int)EquipmentCHRType.Weapon].Name != "")
            {
                this.b_weapon.WaitTextBox.Text = "       " + chr.Equipments[(int)EquipmentCHRType.Weapon].Name;
                this.b_weapon.WaitTextBox.FontSize = 13;
                this.b_weapon.WaitTextureBox.TextureName = chr.Equipments[(int)EquipmentCHRType.Weapon].IconName;
                this.b_weapon.SameAsWait();
            }
            for (int i = 0; i < 7; i++)
            {
                if (chr.Equipments[i + 1].Name != null && chr.Equipments[i + 1].Name != "")
                {
                    this.b_def[i].WaitTextBox.Text = "       " + chr.Equipments[i + 1].Name;
                    this.b_def[i].WaitTextBox.FontSize = 13;
                    this.b_def[i].WaitTextureBox.TextureName = chr.Equipments[i + 1].IconName;
                    this.b_def[i].SameAsWait();
                }
            }
        }
    }
    public class MenuBAG : FormBox
    {
        List<ButtonBox> bb = new List<ButtonBox>();
        TextBox tb;
        public MenuBAG(Game g)
            : base(g)
        {
            BigStep = 8;

            Name = "MenuBAG";
            Location = new Point(64, 48);

            BGTextureBox.TextureName = "bg_256x256";
            BGTextureBox.Size = new Size(512, 512);

            for (int i = 0; i < 16; i++)
            {
                bb.Add(new ButtonBox(game));
            }

            tb = new TextBox(g);
            tb.OneByOne = true;
            tb.FontSize = 13;
            tb.Interval = 10;
            tb.Location = new Point(24, 192);

            this.OnFormBoxDisappear += new FormBoxEvent(MenuBAG_OnFormBoxDisappear);
        }

        void MenuBAG_OnFormBoxDisappear(object sender, object arg)
        {
            game.AppendScript("gui MenuBAGASK disappear");
            game.ExecuteScript();
        }

        public void LoadContext(CHR chr)
        {
            ControlBoxes.Clear();
            ControlBoxes.Add(BGTextureBox);
            ControlBoxes.Add(tb);
            for (int i = 0; i < chr.Bag.Count; i++)
            {
                if (chr.Bag[i] != null)
                {
                    bb[i].Index = i;
                    bb[i].Location = new Point(160 + 160 * (i / 8), 64 + 32 * (i % 8));

                    bb[i].WaitTextBox.Location = new Point(0, 4);
                    bb[i].WaitTextBox.Text = "　　　" + chr.Bag[i].Name;
                    bb[i].WaitTextBox.FontSize = 15;
                    bb[i].WaitTextureBox.TextureName = chr.Bag[i].IconName;
                    bb[i].WaitTextureBox.TextureIndex = -1;
                    bb[i].WaitTextureBox.Size = new System.Drawing.Size(48, 32);
                    bb[i].SameAsWait();

                    bb[i].OnButtonUp += new ButtonBoxEvent(MenuBAG_OnButtonUp);
                    bb[i].OnButtonFocus += new ButtonBoxEvent(MenuBAG_OnButtonFocus);

                    ControlBoxes.Add(bb[i]);
                }
            }
            if (NowButtonBoxIndex >= chr.Bag.Count)
            {
                FocusLastButtonBox();
            }
        }

        void MenuBAG_OnButtonFocus(object sender, object arg)
        {
            int i = ((ButtonBox)sender).Index;
            Item item = game.ME.BagSee(i);
            tb.Text = item.Description;
        }

        void MenuBAG_OnButtonUp(object sender, object arg)
        {
            game.ReturnScript(((ButtonBox)sender).Index);
            game.AppendScript("gui MenuBAGASK appear");
            game.ExecuteScript();
        }
    }
    public class MenuBAGASK : FormBox
    {
        ButtonBox BB1, BB2, BB3;
        public MenuBAGASK(Game g)
            : base(g)
        {
            Name = "MenuBAGASK";
            Location = new Point(64 + 24, 48 + 24);
            BGTextureBox.TextureName = "bg_64x64";
            BGTextureBox.Size = new System.Drawing.Size(128, 128);

            BB1 = new ButtonBox(g);
            BB1.Location = new Point(48, 24);
            BB1.UpTextBox.Text = BB1.DownTextBox.Text = BB1.FocusTextBox.Text = BB1.WaitTextBox.Text = "使用";
            BB1.FocusTextBox.FontColor = Color.CornflowerBlue;
            BB1.OnButtonUp += new ButtonBoxEvent(BB1_OnButtonUp);

            BB2 = new ButtonBox(g);
            BB2.Location = new Point(48, 48);
            BB2.UpTextBox.Text = BB2.DownTextBox.Text = BB2.FocusTextBox.Text = BB2.WaitTextBox.Text = "装备";
            BB2.FocusTextBox.FontColor = Color.CornflowerBlue;
            BB2.OnButtonUp += new ButtonBoxEvent(BB2_OnButtonUp);

            BB3 = new ButtonBox(g);
            BB3.Location = new Point(48, 72);
            BB3.UpTextBox.Text = BB3.DownTextBox.Text = BB3.FocusTextBox.Text = BB3.WaitTextBox.Text = "丢弃";
            BB3.FocusTextBox.FontColor = Color.CornflowerBlue;
            BB3.OnButtonUp += new ButtonBoxEvent(BB3_OnButtonUp);

            ControlBoxes.Add(BB1);
            ControlBoxes.Add(BB2);
            ControlBoxes.Add(BB3);
        }

        //使用
        void BB1_OnButtonUp(object sender, object arg)
        {
            int i = game.ScriptManager.RETURN.INT;
            Item item = game.ME.BagSee(i);
            if (item.ItemType == ItemType.Supply)
            {
                game.AppendScript(item.Script);
                game.ExecuteScript();
                game.ME.BagRemove(i);

                ((MenuBAG)game.FormBoxes["MenuBAG"]).LoadContext(game.ME);
                game.FormBoxManager.Disappear(Name);
            }
        }
        //装备
        void BB2_OnButtonUp(object sender, object arg)
        {
            int i = game.ScriptManager.RETURN.INT;
            game.ME.BagEquip(i);

            ((MenuBAG)game.FormBoxes["MenuBAG"]).LoadContext(game.ME);
            game.FormBoxManager.Disappear(Name);
        }
        //丢弃
        void BB3_OnButtonUp(object sender, object arg)
        {
            int i = game.ScriptManager.RETURN.INT;
            game.ME.BagRemove(i);

            ((MenuBAG)game.FormBoxes["MenuBAG"]).LoadContext(game.ME);
            game.FormBoxManager.Disappear(Name);
        }
    }

    public class MH_MSGBox : MetalX.Resource.MSGBox
    {
        public MH_MSGBox(Game g)
            : base(g)
        {
            Location = new Point(64, 480 - 128 - 8);

            BGTextureBox.TextureName = "bg_256x64";
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

            BGTextureBox.TextureName = "bg_256x64";
            BGTextureBox.Size = new Size(512, 128);

            TextBox.Location = new Point(20, 20);
            TextBox.OneByOne = true;

            TextBox.FontName = "微软雅黑";
            TextBox.FontSize = 15;
        }
    }
}
