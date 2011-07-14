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
            server = new MetalX.Net.Server("192.168.1.15", 1987);
            server.OnClientConnected+=new NetEvent(prog.OnClientConnected);
            server.OnClientDisconnected += new NetEvent(prog.OnClientDisconnected);
            server.OnDataReceived += new NetEvent(prog.OnDataReceived);
            //server.OnDataSent += new NetEvent(prog.OnDataSent);
            server.Start();

            while (true)
            {
                Console.ReadLine();
            }
        }

        void OnDataReceived(Session s)
        {
            //throw new NotImplementedException();
            Console.WriteLine(s.Data.Length.ToString());
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
