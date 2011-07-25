using System;
using System.Collections.Generic;
using System.Text;
using MetalX.Net;
namespace MetalHunterServer
{
    class Program
    {
        //private delegate void appendtextd(string str);
        //private void appendtext(string str)
        //{
        //    if (this.richTextBox1.InvokeRequired)
        //    {
        //        appendtextd md = new appendtextd(appendtext);
        //        this.Invoke(md, str);
        //    }
        //    else
        //    {
        //        this.richTextBox1.AppendText(DateTime.Now + ": " + str + "\n");
        //    }
        //}
        static void Main(string[] args)
        {
            Program prog = new Program();
            MetalX.Net.Server server;
            server = new MetalX.Net.Server("metalhunter.gicp.net", 8415);
            //server = new MetalX.Net.Server("metalhunter.gicp.net", 8415);
            server.OnClientConnected += new NetEvent(prog.OnClientConnected);
            server.OnClientDisconnected += new NetEvent(prog.OnClientDisconnected);
            server.OnDataReceived += new NetEvent(prog.OnDataReceived);
            server.OnDataSent += new NetEvent(prog.OnDataSent);
            server.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }
        void OnDataSent(Session s)
        {
            Console.WriteLine(s.Handle + " sent");
        }
        void OnDataReceived(Session s)
        {
            Console.WriteLine(s.Handle + " received " + Encoding.Default.GetString(s.Data));
            s.Socket.Send(s.Data);
        }
        void OnClientDisconnected(Session s)
        {
            Console.WriteLine(s.Handle + " disconnected");
        }
        void OnClientConnected(Session s)
        {
            Console.WriteLine(s.Handle + " connected");
        }
    }
}
