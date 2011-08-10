using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MetalX.SceneMaker2D
{
    public partial class MonsterMaker : Form
    {
        MetalX.Define.Monster mon;

        public MonsterMaker()
        {
            InitializeComponent();
        }

        private void ui_stand_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.mxmovie|*.mxmovie";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ui_stand.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            }  
        }

        private void ui_defense_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.mxmovie|*.mxmovie";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ui_block.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            } 
        }

        private void ui_hit_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.mxmovie|*.mxmovie";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ui_hit.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            } 
        }

        private void ui_fight_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.mxmovie|*.mxmovie";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ui_fight.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            } 
        }

        private void ui_fire_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.mxmovie|*.mxmovie";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ui_weapon.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            } 
        }

        private void ui_throw_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.mxmovie|*.mxmovie";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ui_item.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            } 
        }

        private void ui_load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "MetalX Monster File|*.mxmonster";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                mon = (MetalX.Define.Monster)Util.LoadObject(ofd.FileName);
                update_ui();
            } 
        }

        void update_ui()
        {
            ui_name.Text = mon.Name;

            ui_w.Text = mon.BattleSize.Width.ToString();
            ui_h.Text = mon.BattleSize.Height.ToString();

            ui_name.Text = mon.Name;
            ui_lv.Text = mon.Level.ToString();
            ui_hp.Text = mon.HP.ToString();
            ui_str.Text = mon.Strength.ToString();
            ui_phy.Text = mon.Physique.ToString();
            ui_int.Text = mon.Intelligence.ToString();
            ui_spd.Text = mon.Agility.ToString();
            ui_exp.Text = mon.EXP.ToString();
            //int i = 0;
            //i = (int)MetalX.Define.BattleState.Stand;
            //ui_stand.Text = mon.BattleMovieIndexers[i].Name;

            //i = (int)MetalX.Define.BattleState.Block;
            //ui_block.Text = mon.BattleMovieIndexers[i].Name;

            //i = (int)MetalX.Define.BattleState.Hit;
            //ui_hit.Text = mon.BattleMovieIndexers[i].Name;

            //i = (int)MetalX.Define.BattleState.Fight;
            //ui_fight.Text = mon.BattleMovieIndexers[i].Name;

            //i = (int)MetalX.Define.BattleState.Weapon;
            //ui_weapon.Text = mon.BattleMovieIndexers[i].Name;

            //i = (int)MetalX.Define.BattleState.Item;
            //ui_item.Text = mon.BattleMovieIndexers[i].Name;

            //i = (int)MetalX.Define.BattleState.Run;
            //ui_run.Text = mon.BattleMovieIndexers[i].Name;

            //i = (int)MetalX.Define.BattleState.Miss;
            //ui_miss.Text = mon.BattleMovieIndexers[i].Name;

            ui_scriptinit.Text = mon.ScriptInit;
            ui_ai.Text = mon.ScriptAI;
        }

        private void ui_save_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "MetalX Monster File|*.MXMonster";
            sfd.RestoreDirectory = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Util.SaveObject(sfd.FileName, mon);
            }
        }

        private void ui_yes_Click(object sender, EventArgs e)
        {
            mon = new Define.Monster();

            mon.Name = ui_name.Text;
            mon.Level = int.Parse(ui_lv.Text);
            mon.HP = int.Parse(ui_hp.Text);
            mon.Strength = int.Parse(ui_str.Text);
            mon.Physique = int.Parse(ui_phy.Text);
            mon.Intelligence = int.Parse(ui_int.Text);
            mon.Agility = int.Parse(ui_spd.Text);

            mon.BattleSize = new Size(int.Parse(ui_w.Text), int.Parse(ui_h.Text));

            mon.ScriptInit = ui_scriptinit.Text;
            mon.ScriptAI = ui_ai.Text;

            mon.EXP = int.Parse(ui_exp.Text);

            //int i = 0;
            //i = (int)MetalX.Define.BattleState.Stand;
            //mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_stand.Text);

            //i = (int)MetalX.Define.BattleState.Block;
            //mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_block.Text);

            //i = (int)MetalX.Define.BattleState.Hit;
            //mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_hit.Text);

            //i = (int)MetalX.Define.BattleState.Fight;
            //mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_fight.Text);

            //i = (int)MetalX.Define.BattleState.Weapon;
            //mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_weapon.Text);

            //i = (int)MetalX.Define.BattleState.Item;
            //mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_item.Text);

            //i = (int)MetalX.Define.BattleState.Run;
            //mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_run.Text);

            //i = (int)MetalX.Define.BattleState.Miss;
            //mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_miss.Text);
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }



    }
}
