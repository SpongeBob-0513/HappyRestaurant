using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
namespace Server.Server
{
    /// <summary>
    /// 监听客户端的连接
    /// </summary>
    public class Server
    {
        private IPEndPoint _ipEndPoint;
        private Socket serverSocket;
        private List<Client> clientList;
        
        public Server(){}

        public Server(string ipStr, int port)
        {
            SetIpAndPort(ipStr, port);
        }

        public void SetIpAndPort(string ipStr, int port)
        {
            _ipEndPoint = new IPEndPoint(IPAddress.Parse(ipStr), port);
        }

        public void Start()
        {
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
            serverSocket.Bind(_ipEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar); // 创建一个新的客户端连接
            Client client = new Client(clientSocket, this); // 在 Client 类中处理服务端客户端中的信息传递
            client.Start(); // 对应的客户端开始监听与服务器之间的消息传输
            clientList.Add(client);
        }

        public void RemoveClient(Client client)
        {
            lock (clientList)  // 防止多个访问 clientList 造成冲突
            {
                clientList.Remove(client); 
            }
        }
    }
    
}