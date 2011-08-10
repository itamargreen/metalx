using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Microsoft.DirectX.DirectInput;

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
            BGTextureBox.Texture.Name = "logo_engine";
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
                bb[i].WaitTextureBox.Texture.Name = "bg_256x64";
                bb[i].FocusTextureBox.Texture.Name = "bg_256x64";
                bb[i].DownTextureBox.Texture.Name = "bg_256x64";
                bb[i].UpTextureBox.Texture.Name = "bg_256x64";
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
            game.ScriptManager.AppendDotMetalXScript("load0");
            game.ScriptManager.Execute();
        }
        void MenuLoad_OnButtonUp1(object sender, object arg)
        {
            game.ScriptManager.AppendDotMetalXScript("load1");
            game.ScriptManager.Execute();
        }
        void MenuLoad_OnButtonUp2(object sender, object arg)
        {
            game.ScriptManager.AppendDotMetalXScript("load2");
            game.ScriptManager.Execute();
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

            BGTextureBox.Texture.Name = "bg_256x256";
            BGTextureBox.Size = new Size(512, 512);

            int line = 5;

            head = new TextureBox(g);
            head.Location = new Point(32, 16);
            
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

            line = 1;

            dmg = new TextBox(g);
            dmg.Location = new Point(224, line++ * 32);

            b_weapon = new ButtonBox(g);
            b_weapon.Location = new Point(224, line++ * 32);
            b_weapon.OnButtonFocus += new ButtonBoxEvent(MenuCHR_OnButtonFocus);
            b_weapon.OnButtonUp += new ButtonBoxEvent(MenuCHR_OnButtonUp);

            def = new TextBox(g);
            def.Location = new Point(224, line++ * 32);

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
                b_def[i].OnButtonFocus += new ButtonBoxEvent(MenuCHR_OnButtonFocus);
                b_def[i].OnButtonUp += new ButtonBoxEvent(MenuCHR_OnButtonUp);
                b_def[i].Location = new Point(224, line * 32 + i * 32);
                ControlBoxes.Add(b_def[i]);
            }

            this.OnFormBoxAppear += new FormBoxEvent(MenuCHR_OnFormBoxAppear);
            this.OnFormBoxDisappear += new FormBoxEvent(MenuCHR_OnFormBoxDisappear);
        }

        void LoadContext(CHR chr)
        {
            chr.HeadTextureName = "icon_" + chr.TextureName;

            //this.head.Texture.Index = chr.HeadTextureIndex;
            this.head.Texture.Name = chr.HeadTextureName;

            this.nam.Text = "名字：　" + chr.Name;

            this.hp.Text = "ＨＰ：　" + chr.HP + " / " + chr.HPMax;

            this.lv.Text = "ＬＶ：　" + chr.Level;
            this.chrlv.Text = "普通战斗ＬＶ：　" + chr.CBLevel;
            this.mtllv.Text = "驾驶战斗ＬＶ：　" + chr.MBLevel;

            this.exp.Text = "经验：　" + chr.EXP;
            try
            {
                this.dmg.Text = "攻击：　" + chr.Weapon.Damage.ToString("f1") + "　　命中：　" + chr.Accurate.ToString("f1") + "%";
                this.def.Text = "防御：　" + chr.Defense.ToString("f1") + "　　闪避：　" + chr.Missrate.ToString("f1") + "%";
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

            string name;
            string icon_name;
            if (chr.Equipments[(int)EquipmentCHRType.Weapon].Name != null)
            {
                name = chr.Equipments[(int)EquipmentCHRType.Weapon].Name;
                icon_name = chr.Equipments[(int)EquipmentCHRType.Weapon].Icon.Name;
            }
            else
            {
                name = "X";
                icon_name = "icon_" + ((EquipmentCHRType)(int)EquipmentCHRType.Weapon).ToString().ToLower();
            }
            this.b_weapon.Index = 0;
            this.b_weapon.WaitTextBox.Location = new Point(48, 4);
            this.b_weapon.WaitTextBox.Text = name;
            this.b_weapon.WaitTextureBox.Texture.Name = icon_name;
            this.b_weapon.WaitTextureBox.Size = new Size(48, 32);

            this.b_weapon.SameAsWait();
            for (int i = 0; i < 7; i++)
            {
                if ( chr.Equipments[i + 1].Name != null)
                {
                    name = chr.Equipments[i + 1].Name;
                    icon_name = chr.Equipments[i + 1].Icon.Name;
                }
                else
                {
                    name = "X";
                    icon_name = "icon_" + ((EquipmentCHRType)i + 1).ToString().ToLower();
                }
                this.b_def[i].Index = i + 1;
                this.b_def[i].WaitTextBox.Location = new Point(48, 4);
                this.b_def[i].WaitTextBox.Text = name;
                this.b_def[i].WaitTextureBox.Texture.Name = icon_name;
                this.b_def[i].WaitTextureBox.Size = new Size(48, 32);
                this.b_def[i].SameAsWait();
            }
        }

        public override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyNO)
            {
                game.ScriptManager.AppendCommand("gui " + Name + " disappear");
                game.ScriptManager.Execute();
            }
        }

        void MenuCHR_OnFormBoxDisappear(object sender, object arg)
        {
            game.ScriptManager.AppendCommand("gui MenuCHRASK disappear");
            game.ScriptManager.Execute();
        }

        void MenuCHR_OnFormBoxAppear(object sender, object arg)
        {
            if (arg is CHR)
            {
                LoadContext((CHR)arg);
            }
        }

        void MenuCHR_OnButtonUp(object sender, object arg)
        {
            game.ScriptManager.RETURN.INT = ((ButtonBox)sender).Index;
            game.ScriptManager.AppendCommand("gui MenuCHRASK appear 64 304");
            game.ScriptManager.Execute();
        }

        void MenuCHR_OnButtonFocus(object sender, object arg)
        {
        }
    }
    public class MenuCHRASK : FormBox
    {
        ButtonBox BB1, BB2;
        public MenuCHRASK(Game g)
            : base(g)
        {
            Name = "MenuCHRASK";
            Location = new Point(576, 48);
            BGTextureBox.Texture.Name = "bg_64x64";
            BGTextureBox.Size = new System.Drawing.Size(128, 128);

            BB1 = new ButtonBox(g);
            BB1.Location = new Point(48, 24);
            BB1.UpTextBox.Text = BB1.DownTextBox.Text = BB1.FocusTextBox.Text = BB1.WaitTextBox.Text = "装备";
            BB1.FocusTextBox.FontColor = Color.CornflowerBlue;
            BB1.OnButtonUp += new ButtonBoxEvent(BB1_OnButtonUp);

            BB2 = new ButtonBox(g);
            BB2.Location = new Point(48, 24);
            BB2.UpTextBox.Text = BB2.DownTextBox.Text = BB2.FocusTextBox.Text = BB2.WaitTextBox.Text = "卸下";
            BB2.FocusTextBox.FontColor = Color.CornflowerBlue;
            BB2.OnButtonUp += new ButtonBoxEvent(BB2_OnButtonUp);

            //ControlBoxes.Add(BB1);
            ControlBoxes.Add(BB2);
        }

        //装备
        void BB1_OnButtonUp(object sender, object arg)
        {
            //int i = game.ScriptManager.RETURN.INT;
            //Item item = game.ME.BagSee(i);
            //if (item.ItemType == ItemType.Supply)
            //{
            //    game.ScriptManager.AppendCommand(item.Script);
            //    game.ScriptManager.Execute();
            //    game.ME.BagRemove(i);

            //    ((MenuBAG)game.FormBoxes["MenuBAG"]).LoadContext(game.ME);
            //    game.FormBoxManager.Disappear(Name);
            //}
            game.ScriptManager.AppendCommand("gui MenuBAG appear arg me");
            game.ScriptManager.AppendCommand("untilpress y n");
            game.ScriptManager.AppendCommand("gui MenuBAG disappear");
            game.ScriptManager.Execute();
        }
        //卸下
        void BB2_OnButtonUp(object sender, object arg)
        {
            int i = game.ScriptManager.RETURN.INT;
            game.ME.BagUnequip((EquipmentCHRType)i);
            //game.ME.BagEquip(i);

            game.FormBoxManager.Disappear(Name);
            game.FormBoxManager.Disappear("MenuCHR");
            game.FormBoxManager.Appear("MenuCHR", game.ME);
        }

        public override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyNO)
            {
                game.ScriptManager.AppendCommand("gui " + Name + " disappear");
                game.ScriptManager.Execute();
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

            BGTextureBox.Texture.Name = "bg_256x256";
            BGTextureBox.Size = new Size(512, 512);

            for (int i = 0; i < 16; i++)
            {
                bb.Add(new ButtonBox(game));
            }

            tb = new TextBox(g);
            tb.OneByOne = true;
            tb.FontSize = 13;
            tb.Interval = 10;
            tb.Location = new Point(24, 24);

            this.OnFormBoxDisappear += new FormBoxEvent(MenuBAG_OnFormBoxDisappear);
            this.OnFormBoxAppear += new FormBoxEvent(MenuBAG_OnFormBoxAppear);
        }

        void LoadContext(CHR chr)
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

                    bb[i].WaitTextBox.Location = new Point(48, 4);
                    bb[i].WaitTextBox.Text = chr.Bag[i].Name;
                    bb[i].WaitTextBox.FontSize = 15;
                    bb[i].WaitTextureBox.Texture.Name = chr.Bag[i].Icon.Name;
                    //bb[i].WaitTextureBox.Texture.Index = -1;
                    bb[i].WaitTextureBox.Size = new System.Drawing.Size(48, 32);
                    bb[i].SameAsWait();

                    bb[i].OnButtonUp += new ButtonBoxEvent(MenuBAG_OnButtonUp);
                    bb[i].OnButtonFocus += new ButtonBoxEvent(MenuBAG_OnButtonFocus);

                    ControlBoxes.Add(bb[i]);
                }
            }
            FocusNowButtonBox();
            //if (NowButtonBoxIndex >= chr.Bag.Count)
            //{
            //    FocusLastButtonBox();
            //}
        }

        public override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyNO)
            {
                game.ScriptManager.AppendCommand("gui " + Name + " disappear");
                game.ScriptManager.Execute();
            }
        }

        void MenuBAG_OnFormBoxAppear(object sender, object arg)
        {
            if (arg is CHR)
            {
                LoadContext((CHR)arg);
            }
        }

        void MenuBAG_OnFormBoxDisappear(object sender, object arg)
        {
            game.ScriptManager.AppendCommand("gui MenuBAGASK disappear");
            game.ScriptManager.Execute();
        }

        void MenuBAG_OnButtonFocus(object sender, object arg)
        {
            int i = ((ButtonBox)sender).Index;
            Item item = game.ME.BagSee(i);
            string str = "";
            if (item == null)
            {
                str = "";
            }
            else
            {
                str = item.Description;
            }
            tb.Text = str;
        }

        void MenuBAG_OnButtonUp(object sender, object arg)
        {
            game.ScriptManager.RETURN.INT = (((ButtonBox)sender).Index);
            game.ScriptManager.AppendCommand("gui MenuBAGASK appear 64 304");
            game.ScriptManager.Execute();
        }

    }
    public class MenuBAGASK : FormBox
    {
        ButtonBox BB1, BB2, BB3;
        public MenuBAGASK(Game g)
            : base(g)
        {
            Name = "MenuBAGASK";
            Location = new Point(64 + 24, 256 + 24);
            BGTextureBox.Texture.Name = "bg_64x64";
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
                game.ScriptManager.AppendCommand(item.Script);
                game.ScriptManager.Execute();
                game.ME.BagRemove(i);

                //((MenuBAG)game.FormBoxes["MenuBAG"]).LoadContext(game.ME);
                game.FormBoxManager.Disappear(Name);
                game.FormBoxManager.Disappear("MenuBAG");
                game.FormBoxManager.Appear("MenuBAG", game.ME);
            }
        }
        //装备
        void BB2_OnButtonUp(object sender, object arg)
        {
            int i = game.ScriptManager.RETURN.INT;
            game.ME.BagEquip(i);

            game.FormBoxManager.Disappear(Name);
            game.FormBoxManager.Disappear("MenuBAG");
            game.FormBoxManager.Appear("MenuBAG", game.ME);
        }
        //丢弃
        void BB3_OnButtonUp(object sender, object arg)
        {
            int i = game.ScriptManager.RETURN.INT;
            game.ME.BagRemove(i);

            game.FormBoxManager.Disappear(Name);
            game.FormBoxManager.Disappear("MenuBAG");
            game.FormBoxManager.Appear("MenuBAG", game.ME);
        }

        public override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyNO)
            {
                game.ScriptManager.AppendCommand("gui " + Name + " disappear");
                game.ScriptManager.Execute();
            }
        }
    }
    public class MenuBattleCHR : FormBox
    {
        ButtonBox BB1,BB2,BB3,BB4;
        public MenuBattleCHR(Game g)
            : base(g)
        {
            Name = "MenuBattleCHR";
            Location = new Point(24, 24);
            //BGTextureBox.Texture.Name = "bg_64x64";
            //BGTextureBox.Size = new System.Drawing.Size(128, 128);

            BB1 = new ButtonBox(g);
            BB1.Location = new Point(24, 24);
            BB1.WaitTextBox.Text = "攻击";
            BB1.SameAsWait();
            BB1.OnButtonUp += new ButtonBoxEvent(BB1_OnButtonUp);

            BB2 = new ButtonBox(g);
            BB2.Location = new Point(24, 48);
            BB2.WaitTextBox.Text = "道具";
            BB2.SameAsWait();
            BB2.OnButtonUp += new ButtonBoxEvent(BB2_OnButtonUp);

            BB3 = new ButtonBox(g);
            BB3.Location = new Point(24, 72);
            BB3.WaitTextBox.Text = "装备";
            BB3.SameAsWait();
            BB3.OnButtonUp += new ButtonBoxEvent(BB3_OnButtonUp);

            BB4 = new ButtonBox(g);
            BB4.Location = new Point(24, 96);
            BB4.WaitTextBox.Text = "乘降";
            BB4.SameAsWait();
            BB4.OnButtonUp += new ButtonBoxEvent(BB4_OnButtonUp);

            ControlBoxes.Add(BB1);
            ControlBoxes.Add(BB2);
            ControlBoxes.Add(BB3);
            ControlBoxes.Add(BB4);
        }

        void BB1_OnButtonUp(object sender, object arg)
        {
            game.ScriptManager.RETURN.STRING = ("攻击");
        }

        void BB2_OnButtonUp(object sender, object arg)
        {
            game.ScriptManager.RETURN.STRING = ("道具");
        }

        void BB3_OnButtonUp(object sender, object arg)
        {
            game.ScriptManager.RETURN.STRING = ("装备");
        }

        void BB4_OnButtonUp(object sender, object arg)
        {
            game.ScriptManager.RETURN.STRING = ("乘降");
        }
    }
    public class MH_MSGBox : MetalX.Resource.MSGBox
    {
        public MH_MSGBox(Game g)
            : base(g)
        {
            Location = new Point(64, 480 - 128 - 8);

            BGTextureBox.Texture.Name = "bg_256x64";
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

            BGTextureBox.Texture.Name = "bg_256x64";
            BGTextureBox.Size = new Size(512, 128);

            TextBox.Location = new Point(20, 20);
            TextBox.OneByOne = true;

            TextBox.FontName = "微软雅黑";
            TextBox.FontSize = 15;
        }
    }
}
