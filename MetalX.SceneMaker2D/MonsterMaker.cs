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
                ui_defense.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
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
                ui_fire.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
            } 
        }

        private void ui_throw_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "*.mxmovie|*.mxmovie";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                ui_throw.Text = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
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

            int i = 0;
            i = (int)MetalX.Define.BattleState.Stand;
            ui_stand.Text = mon.BattleMovieIndexers[i].Name;

            i = (int)MetalX.Define.BattleState.Defense;
            ui_defense.Text = mon.BattleMovieIndexers[i].Name;

            i = (int)MetalX.Define.BattleState.Hit;
            ui_hit.Text = mon.BattleMovieIndexers[i].Name;

            i = (int)MetalX.Define.BattleState.Fight;
            ui_fight.Text = mon.BattleMovieIndexers[i].Name;

            i = (int)MetalX.Define.BattleState.Fire;
            ui_fire.Text = mon.BattleMovieIndexers[i].Name;

            i = (int)MetalX.Define.BattleState.Throw;
            ui_throw.Text = mon.BattleMovieIndexers[i].Name;
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
            mon.BattleSize = new Size(int.Parse(ui_w.Text), int.Parse(ui_h.Text));

            int i = 0;
            i = (int)MetalX.Define.BattleState.Stand;
            mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_stand.Text);

            i = (int)MetalX.Define.BattleState.Defense;
            mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_defense.Text);

            i = (int)MetalX.Define.BattleState.Hit;
            mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_hit.Text);

            i = (int)MetalX.Define.BattleState.Fight;
            mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_fight.Text);

            i = (int)MetalX.Define.BattleState.Fire;
            mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_fire.Text);

            i = (int)MetalX.Define.BattleState.Throw;
            mon.BattleMovieIndexers[i] = new Define.MemoryIndexer(ui_throw.Text);
        }
    }
}
