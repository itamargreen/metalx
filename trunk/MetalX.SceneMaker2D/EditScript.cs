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
            code.Script = ui_script.Text;
            Close();
        }

        private void EditScript_Shown(object sender, EventArgs e)
        {
            ui_script.Text = code.Script;
        }
    }
}
