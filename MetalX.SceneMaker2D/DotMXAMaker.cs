using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetalX.File;
namespace MetalX.SceneMaker2D
{
    public partial class DotMXAMaker : Form
    {
        public DotMXAMaker()
        {
            InitializeComponent();
        }

        private void ui_load_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "*.mp3|*.mp3";
                ofd.RestoreDirectory = true;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    if (ofd.FileName != string.Empty)
                    {
                        textBox1.Text = ofd.FileName;
                        textBox2.Text = System.IO.Path.GetDirectoryName(ofd.FileName) + @"\" + System.IO.Path.GetFileNameWithoutExtension(ofd.FileName) + ".MXA";

                        textBox5.Text = System.IO.Path.GetFileNameWithoutExtension(textBox1.Text);
                    }
                }
            }
        }

        private void ui_save_Click(object sender, EventArgs e)
        {
            using (MetalXAudio mxa = new MetalXAudio())
            {
                mxa.Name = textBox5.Text;
                mxa.AudioData = System.IO.File.ReadAllBytes(textBox1.Text);
                Util.SaveObject(textBox2.Text, mxa);
                MessageBox.Show("输出成功");
            }
        }
    }
}
