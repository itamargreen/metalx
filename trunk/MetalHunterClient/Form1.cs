using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MetalX;
using MetalX.Net;
namespace MetalHunterClient
{
    public partial class Form1 : Form
    {
        Game game;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game();
            ui_ip.Text = game.Options.ServerIPAddress;
            ui_port.Text = game.Options.ServerPort.ToString();
            game.NetManager.OnDataReceived += new NetEvent(NetManager_OnDataReceived);
        }
        string str;
        void NetManager_OnDataReceived(Session s)
        {
            //throw new NotImplementedException();
            str += Encoding.Default.GetString(s.Data);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            game.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            game.NetManager.Connect(ui_ip.Text, int.Parse(ui_port.Text));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            game.NetManager.Send(Encoding.Default.GetBytes(ui_sendtxt.Text));
            ui_sendtxt.Text = "";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            richTextBox1.Text = str;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            game.NetManager.Send(Encoding.Default.GetBytes(DateTime.Now.GetHashCode().ToString()));
            //ui_sendtxt.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }
    }
}
