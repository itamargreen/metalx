using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Microsoft.DirectX.DirectInput;

using MetalX;
using MetalX.Define;
using MetalX.Resource;

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
        protected override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyYES)
            {
                game.ScriptManager.AppendCommand("gui disappear " + Name);
                game.ScriptManager.Execute();
            }
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

        List<ButtonBox> b_equip;

        public MenuCHR(Game g)
            : base(g)
        {
            Name = "MenuCHR";
            Location = new Point(0, 0);

            BGTextureBox.Texture.Name = "bg_160x240";
            BGTextureBox.Size = new Size(320, 480);

            int line = 5;

            head = new TextureBox(g);
            head.Location = new Point(32, 16);
            head.Size = new Size(96, 96);
            
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

            line = 0;

            dmg = new TextBox(g);
            dmg.Location = new Point(160, (++line) * 24);

            //b_weapon = new ButtonBox(g);
            //b_weapon.Location = new Point(224, line++ * 32);
            //b_weapon.OnButtonFocus += new ButtonBoxEvent(MenuCHR_OnButtonFocus);
            //b_weapon.OnButtonUp += new ButtonBoxEvent(MenuCHR_OnButtonUp);

            def = new TextBox(g);
            def.Location = new Point(160, (line+=2) * 24);

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

            //ControlBoxes.Add(b_weapon);
            line = 5;

            b_equip = new List<ButtonBox>();
            for (int i = 0; i < 8; i++)
            {
                b_equip.Add(new ButtonBox(g));
                b_equip[i].OnButtonFocus += new ButtonBoxEvent(MenuCHR_OnButtonFocus);
                b_equip[i].OnButtonUp += new ButtonBoxEvent(MenuCHR_OnButtonUp);
                b_equip[i].Location = new Point(144, line * 24 + i * 24);
                ControlBoxes.Add(b_equip[i]);
            }

            this.OnFormBoxAppear += new FormBoxEvent(MenuCHR_OnFormBoxAppear);
            this.OnFormBoxDisappear += new FormBoxEvent(MenuCHR_OnFormBoxDisappear);
        }

        void LoadContext(CHR chr)
        {
            chr.HeadTextureName = chr.TextureName + "_icon";

            //this.head.Texture.Index = chr.HeadTextureIndex;
            this.head.Texture.Name = chr.HeadTextureName;

            this.nam.Text = "名字：　" + chr.Name;

            this.hp.Text = "ＨＰ：　" + chr.HP + " / " + chr.HPMax;

            this.lv.Text = "ＬＶ：　" + chr.Level;
            this.chrlv.Text = "徒步战ＬＶ：　" + chr.CBLevel;
            this.mtllv.Text = "驾驶战ＬＶ：　" + chr.MBLevel;

            this.exp.Text = "经验：　" + chr.EXP;
            try
            {
                this.dmg.Text = "攻击：　" + chr.Weapon.Damage.ToString("f1") + "\n命中：　" + chr.Accurate.ToString("f1") + "%";
                this.def.Text = "防御：　" + chr.Defense.ToString("f1") + "\n闪避：　" + chr.Missrate.ToString("f1") + "%";
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
            //if (chr.Equipments[(int)EquipmentCHRType.Weapon].Name != null)
            //{
            //    name = chr.Equipments[(int)EquipmentCHRType.Weapon].Name;
            //    icon_name = chr.Equipments[(int)EquipmentCHRType.Weapon].Icon.Name;
            //}
            //else
            //{
            //    name = "X";
            //    icon_name = "icon_" + ((EquipmentCHRType)(int)EquipmentCHRType.Weapon).ToString().ToLower();
            //}
            //this.b_weapon.Index = 0;
            //this.b_weapon.WaitTextBox.Location = new Point(48, 4);
            //this.b_weapon.WaitTextBox.Text = name;
            //this.b_weapon.WaitTextureBox.Texture.Name = icon_name;
            //this.b_weapon.WaitTextureBox.Size = new Size(48, 32);

            //this.b_weapon.SameAsWait();
            for (int i = 0; i < 8; i++)
            {
                if ( chr.Equipments[i].Name != null)
                {
                    name = chr.Equipments[i].Name;
                    icon_name = chr.Equipments[i].Icon.Name;
                }
                else
                {
                    name = "无";
                    icon_name = "icon_" + ((EquipmentCHRType)i).ToString().ToLower();
                }
                this.b_equip[i].Index = i;
                this.b_equip[i].WaitTextBox.Location = new Point(48, 4);
                this.b_equip[i].WaitTextBox.Text = name;
                this.b_equip[i].WaitTextureBox.Texture.Name = icon_name;
                this.b_equip[i].WaitTextureBox.Size = new Size(48, 32);
                this.b_equip[i].SameAsWait();
            }
        }

        protected override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyNO)
            {
                game.ScriptManager.AppendCommand("gui disappear " + Name);
                game.ScriptManager.Execute();
            }
            else if (k == game.Options.KeyRIGHT)
            {
                game.ScriptManager.AppendCommand("gui focuson MenuBAG");
                game.ScriptManager.Execute();
            }
        }

        void MenuCHR_OnFormBoxDisappear(object sender, object arg)
        {
            game.ScriptManager.AppendCommand("gui disappear MenuCHRASK");
            //game.ScriptManager.AppendCommand("gui disappear MenuBAG");
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
            game.ScriptManager.AppendCommand("var equip_index = " + ((ButtonBox)sender).Index);
            game.ScriptManager.AppendCommand("gui appear MenuCHRASK 192 352");
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
            game.ScriptManager.AppendCommand("gui appear MenuBAG arg me");
            game.ScriptManager.AppendCommand("untilpress y n");
            game.ScriptManager.AppendCommand("gui disappear MenuBAG");
            game.ScriptManager.Execute();
        }
        //卸下
        void BB2_OnButtonUp(object sender, object arg)
        {
            int i = int.Parse(game.ScriptManager.GetVariable("equip_index"));
            game.ME.BagUnequip((EquipmentCHRType)i);
            //game.ME.BagEquip(i);

            game.FormBoxManager.Disappear("MenuCHR");
            game.FormBoxManager.Appear("MenuCHR", game.ME);
            game.FormBoxManager.Disappear("MenuBAG");
            game.FormBoxManager.Appear("MenuBAG", game.ME);
        }

        protected override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyNO)
            {
                game.ScriptManager.AppendCommand("gui disappear " + Name);
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
            //BigStep = 8;

            Name = "MenuBAG";
            Location = new Point(320, 0);

            BGTextureBox.Texture.Name = "bg_160x240";
            BGTextureBox.Size = new Size(320, 480);

            for (int i = 0; i < 16; i++)
            {
                bb.Add(new ButtonBox(game));
            }

            tb = new TextBox(g);
            //tb.OneByOne = true;
            //tb.Interval = 10;
            tb.Location = new Point(184, 24);

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
                    //bb[i].Location = new Point(160 + 160 * (i / 8), 64 + 32 * (i % 8));              
                    bb[i].Location = new Point(24, 24 + 24 * i);

                    bb[i].WaitTextBox.Location = new Point(48, 4);
                    bb[i].WaitTextBox.Text = chr.Bag[i].Name;
                    //bb[i].WaitTextBox.FontSize = 15;
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

            if (NowButtonBoxIndex >= chr.Bag.Count)
            {
                FocusLastButtonBox();
            }
        }

        protected override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyNO)
            {
                game.ScriptManager.AppendCommand("gui disappear " + Name);
                game.ScriptManager.Execute();
            } 
            else if (k == game.Options.KeyLEFT)
            {
                game.ScriptManager.AppendCommand("gui focuson MenuCHR");
                game.ScriptManager.Execute();
            }
        }

        void MenuBAG_OnFormBoxAppear(object sender, object arg)
        {
            if (arg is CHR)
            {
                LoadContext((CHR)arg);
            }
            for (int i = 0; i < Index; i++)
            {
                FocusNextButtonBox();
            }
        }

        void MenuBAG_OnFormBoxDisappear(object sender, object arg)
        {
            game.ScriptManager.AppendCommand("gui disappear MenuBAGASK");
            //game.ScriptManager.AppendCommand("gui disappear MenuCHR");
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
            Index = ((ButtonBox)sender).Index;
            //game.ScriptManager.RETURN.INT = Index;

            game.ScriptManager.InsertCommand("var item_index = " + Index);

            game.ScriptManager.AppendCommand("gui appear MenuBAGASK 512 352");
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
            int i = int.Parse(game.ScriptManager.GetVariable("item_index"));
            Item item = game.ME.BagSee(i);
            if (item.ItemType == ItemType.Supply)
            {
                game.ScriptManager.AppendCommand(item.Script);
                game.ScriptManager.Execute();
                game.ME.BagRemove(i);

                game.FormBoxManager.Disappear("MenuBAG");
                game.FormBoxManager.Appear("MenuBAG", game.ME);
            }
        }
        //装备
        void BB2_OnButtonUp(object sender, object arg)
        {
            int i = int.Parse(game.ScriptManager.GetVariable("item_index"));
            game.ME.BagEquip(i);

            game.FormBoxManager.Disappear("MenuCHR");
            game.FormBoxManager.Appear("MenuCHR", game.ME);
            game.FormBoxManager.Disappear("MenuBAG");
            game.FormBoxManager.Appear("MenuBAG", game.ME);
        }
        //丢弃
        void BB3_OnButtonUp(object sender, object arg)
        {
            int i = int.Parse(game.ScriptManager.GetVariable("item_index"));
            game.ME.BagRemove(i);

            game.FormBoxManager.Disappear("MenuBAG");
            game.FormBoxManager.Appear("MenuBAG", game.ME);
        }

        protected override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyNO)
            {
                game.ScriptManager.AppendCommand("gui disappear " + Name);
                game.ScriptManager.Execute();
            }
        }
    }
    public class MenuBattleCHR : FormBox
    {
        ButtonBox BB1, BB2, BB3, BB4, BB5;
        public MenuBattleCHR(Game g)
            : base(g)
        {
            Name = "MenuBattleCHR";
            Location = new Point(640 - 128, 480 - 128);
            BGTextureBox.Texture.Name = "bg_64x64";
            BGTextureBox.Size = new System.Drawing.Size(128, 128);

            BB1 = new ButtonBox(g);
            BB1.Location = new Point(20, 16);
            BB1.WaitTextBox.Text = "攻击";
            BB1.SameAsWait();
            BB1.OnButtonUp += new ButtonBoxEvent(BB1_OnButtonUp);

            BB2 = new ButtonBox(g);
            BB2.Location = new Point(20, 32);
            BB2.WaitTextBox.Text = "道具";
            BB2.SameAsWait();
            BB2.OnButtonUp += new ButtonBoxEvent(BB2_OnButtonUp);

            BB3 = new ButtonBox(g);
            BB3.Location = new Point(20, 48);
            BB3.WaitTextBox.Text = "装备";
            BB3.SameAsWait();
            BB3.OnButtonUp += new ButtonBoxEvent(BB3_OnButtonUp);

            BB4 = new ButtonBox(g);
            BB4.Location = new Point(20, 64);
            BB4.WaitTextBox.Text = "乘降";
            BB4.SameAsWait();
            BB4.OnButtonUp += new ButtonBoxEvent(BB4_OnButtonUp);

            BB5 = new ButtonBox(g);
            BB5.Location = new Point(20, 80);
            BB5.WaitTextBox.Text = "逃跑";
            BB5.SameAsWait();
            BB5.OnButtonUp += new ButtonBoxEvent(BB5_OnButtonUp);

            ControlBoxes.Add(BB1);
            ControlBoxes.Add(BB2);
            ControlBoxes.Add(BB3);
            ControlBoxes.Add(BB4);
            ControlBoxes.Add(BB5);
        }

        void BB1_OnButtonUp(object sender, object arg)
        {
            game.ScriptManager.InsertCommand("var op_type = weapon");

            game.ScriptManager.InsertCommand("gui appear MenuMonster");
            game.ScriptManager.InsertCommand("untilpress y n");
            game.ScriptManager.InsertCommand("gui disappear MenuMonster");
        }

        void BB2_OnButtonUp(object sender, object arg)
        {
            game.ScriptManager.InsertCommand("var op_type = item");

            game.ScriptManager.InsertCommand("gui appear MenuBAG arg me");
            game.ScriptManager.InsertCommand("untilpress y");
            game.ScriptManager.InsertCommand("gui disappear MenuBAG");

            game.ScriptManager.InsertCommand("gui appear MenuMonster");
            game.ScriptManager.InsertCommand("untilpress y n");
            game.ScriptManager.InsertCommand("gui disappear MenuMonster");
        }

        void BB3_OnButtonUp(object sender, object arg)
        {
            //game.ScriptManager.RETURN.STRING = ("装备");
        }

        void BB4_OnButtonUp(object sender, object arg)
        {
            //game.ScriptManager.RETURN.STRING = ("乘降");
        }

        void BB5_OnButtonUp(object sender, object arg)
        {
            //game.ScriptManager.RETURN.STRING = ("逃跑");
        }

        //protected override void OnKeyUpCode(object sender, int key)
        //{
        //    Key k = (Key)key;
        //    if (k == game.Options.KeyYES)
        //    {
        //        game.FormBoxManager.Disappear(Name);
        //    }
        //}
    }
    public class MenuMonster : FormBox
    {
        void MenuMonster_OnButtonUp(object sender, object arg)
        {
            int pc_index = int.Parse(game.ScriptManager.GetVariable("pc_index"));
            int target_index = ((ButtonBox)sender).Index;
            int damage = 0;

            PC pc = game.PCs[pc_index];
            Monster mon = game.Monsters[target_index];

            game.ScriptManager.InsertCommand("var target_index = " + target_index);

            string op_type = game.ScriptManager.GetVariable("op_type");
            if (op_type == "weapon")
            {
                string haveweapon = "null";


                if (pc.Weapon != null)
                {
                    if (pc.Weapon.Name != null)
                        if (pc.Weapon.Name != string.Empty)
                        {
                            haveweapon = pc.Weapon.Name;
                        }
                }
                game.ScriptManager.InsertCommand("var haveweapon = " + haveweapon);
                game.ScriptManager.InsertCommand("?var op_type # weapon var haveweapon = not_null");
                game.ScriptManager.InsertCommand("?var haveweapon = null var op_type = fight");

                //计算伤害
                int roll = Util.Roll(100000);

                double acc = pc.Accurate - mon.Missrate;
                double str = pc.Strength - mon.Physique;
                if (acc < 0) acc = 0;
                if (str < 0) str = 0;

                if (roll < acc * 1000.0)
                {
                    if (roll < str * acc)
                    {
                        //暴击
                        damage = (int)(pc.Damage + pc.Damage * (100000.0 - roll) / 100000.0);
                    }
                    else
                    {
                        //非暴击
                        double x = (double)pc.Weapon.Quality;
                        x /= 100.0;
                        double y = Math.Abs(1.0 - x);
                        damage = (int)(pc.Damage * x + pc.Damage * y * (100000.0 - roll) / 100000.0);
                    }
                }
            }
            else if (op_type == "item")
            {
                int item_index = int.Parse(game.ScriptManager.GetVariable("item_index"));
                Item item = pc.BagSee(item_index);
                //pc.BagRemove(item_index);
                if (item is BattleItem)
                {
                    BattleItem bitem = (BattleItem)item;

                    int roll = Util.Roll(100000);

                    double acc = bitem.Accurate - mon.Missrate;
                    double str = pc.Strength - mon.Physique;
                    if (acc < 0) acc = 0;
                    if (str < 0) str = 0;

                    if (roll < acc * 1000.0)
                    {
                        if (roll < str * acc)
                        {
                            //暴击
                            damage = (int)(bitem.Damage + bitem.Damage * (100000.0 - roll) / 100000.0);
                        }
                        else
                        {
                            //非暴击
                            double x = (double)pc.Weapon.Quality;
                            x /= 100.0;
                            double y = Math.Abs(1.0 - x);
                            damage = (int)(bitem.Damage * x + bitem.Damage * y * (100000.0 - roll) / 100000.0);
                        }
                    }
                }
                game.ScriptManager.AppendCommand("pc [pc_index] bagremove [item_index]");
            }
            game.ScriptManager.AppendCommand("monster [target_index] -hp " + damage);
            
            game.ScriptManager.AppendCommand("msg monster　[target_index]　HP-=　" + damage);
            game.ScriptManager.AppendCommand("untilpress y n");
            game.ScriptManager.AppendCommand("msg");

            game.ScriptManager.AppendCommand("monster [target_index] gethp mhp");
            game.ScriptManager.AppendCommand("?var mhp < 1 msg monster　[target_index]　死了");
            game.ScriptManager.AppendCommand("?var mhp < 1 untilpress y n");
            game.ScriptManager.AppendCommand("?var mhp < 1 msg");
            game.ScriptManager.AppendCommand("?var mhp < 1 monster [target_index] dead");
        }
        
        MetalX.File.MetalXMovie movie;
        List<ButtonBox> bb = new List<ButtonBox>();

        public MenuMonster(Game g)
            : base(g)
        {
            Name = "MenuMonster";

            movie = game.LoadDotMXMovie(game.MovieFiles["cursor"].FullName);
            OnFormBoxDisappear += new FormBoxEvent(MenuMonster_OnFormBoxDisappear);
            OnFormBoxAppear += new FormBoxEvent(MenuMonster_OnFormBoxAppear);

            ControlBoxes.Clear();
            for (int i = 0; i < 16; i++)
            {
                bb.Add(new ButtonBox(game));
                bb[i].OnButtonFocus += new ButtonBoxEvent(MenuMonster_OnButtonFocus);
                bb[i].OnButtonUp += new ButtonBoxEvent(MenuMonster_OnButtonUp);
                //ControlBoxes.Add(bb[i]);
            }

        }

        void MenuMonster_OnFormBoxAppear(object sender, object arg)
        {
            ControlBoxes.Clear();
            for (int i = 0; i < game.Monsters.Count; i++)
            {
                bb[i].Index = i;
                bb[i].Visible = true;
                bb[i].Location = Util.Vector32Point(game.Monsters[i].BattleLocation);
                if (game.Monsters[i].HP > 0)
                {
                    ControlBoxes.Add(bb[i]);
                }
            }
            game.MovieManager.PlayMovie(movie, Util.Point2Vector3(((ButtonBox)ControlBoxes[0]).Location));
            //int first = -1;
            //for (int i = 0; i < 16; i++)
            //{
            //    //bb.Add(new ButtonBox(game));
            //    //ButtonBox bbb = ((ButtonBox)ControlBoxes[i]);
            //    ((ButtonBox)ControlBoxes[i]).Index = i;
            //    ((ButtonBox)ControlBoxes[i]).Visible = false;
            //    //if (i < game.Monsters.Count)
            //    try
            //    {
            //        if (game.Monsters[i].HP > 0)
            //        {
            //            if (first == -1)
            //            {
            //                first = i;
            //            }
            //            ((ButtonBox)ControlBoxes[i]).Visible = true;
            //            ((ButtonBox)ControlBoxes[i]).Location = Util.Vector32Point(game.Monsters[i].BattleLocation);
            //        }
            //    }
            //    catch { }
            //    //bb[i].OnButtonFocus += new ButtonBoxEvent(MenuMonster_OnButtonFocus);
            //    //bb[i].OnButtonUp += new ButtonBoxEvent(MenuMonster_OnButtonUp);
            //    //ControlBoxes.Add(bb[i]);
            //}
            //if (first > -1)
            //{
            //    game.MovieManager.PlayMovie(movie, Util.Point2Vector3(((ButtonBox)ControlBoxes[first]).Location));
            //}
        }

        void MenuMonster_OnFormBoxDisappear(object sender, object arg)
        {
            game.MovieManager.RemoveMovie("cursor");
        }
        
        void MenuMonster_OnButtonFocus(object sender, object arg)
        {
            ButtonBox bbs = (ButtonBox)sender;
            //if (bbs.Visible == false)
            //{
            //    FocusNextButtonBox();
            //}
            //else
            {
                Microsoft.DirectX.Vector3 loc = Util.Point2Vector3(bbs.Location);
                loc.X -= 48;
                loc.Y -= 32;
                game.MovieManager.PlayMovie("cursor", loc);
            }
        }

        protected override void OnKeyUpCode(object sender, int key)
        {
            Key k = (Key)key;
            if (k == game.Options.KeyNO)
            {
                game.FormBoxManager.Disappear(Name); 
                
                game.ScriptManager.InsertCommand("gui appear MenuBattleCHR");
                game.ScriptManager.InsertCommand("untilpress y");
                //game.ScriptManager.InsertCommand("gui disappear MenuBattleCHR");
            } 
            else if (k == game.Options.KeyYES)
            {
                //game.FormBoxManager.Disappear(Name);                
            }
        }

    }
    public class MH_MSGBox : MSGBox
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
    public class MH_ASKboolBox : ASKboolBox
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
