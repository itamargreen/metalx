using System;
using System.Collections.Generic;
using System.Text;
using MetalX.Net;
namespace MetalHunterServer
{
    class Program
    {

        static void Main(string[] args)
        {
            Program prog = new Program();
            MetalX.Net.Server server;
            server = new MetalX.Net.Server("127.0.0.1", 8415);
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
