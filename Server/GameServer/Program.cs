using System;
using GameServer.Servers;

namespace GameServer
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 6688);
            server.Start();

            Console.ReadKey();
        }
    }
}