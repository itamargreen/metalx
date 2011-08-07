using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetalX.Define;
namespace MetalX.SceneMaker2D
{
    public partial class EditScript : Form
    {
        public Code code;
        public NPC npc;

        public EditScript()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (code != null)
            {
                code.Script = ui_script.Text;
            }
            else
            {
                npc.Script = ui_script.Text;
            }
            Close();
        }

        private void EditScript_Shown(object sender, EventArgs e)
        {
            if (code != null)
            {
                ui_script.Text = code.Script;
            }
            else
            {
                ui_script.Text = npc.Script;
            }
        }
    }
}
