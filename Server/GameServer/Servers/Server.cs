using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Common;
using GameServer.Controller;

namespace GameServer.Servers
{
    /// <summary>
    /// 监听客户端的连接
    /// </summary>
    public class Server
    {
        private IPEndPoint _ipEndPoint;
        private Socket serverSocket;
        private List<Client> clientList = new List<Client>();
        private ControllerManager controllerManager;

        public Server()
        {
            
        }

        public Server(string ipStr, int port)
        {
            controllerManager = new ControllerManager(this);
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

        public void SendRespoonse(Client client, ActionCode actionCode, string data)
        {
            client.Send(actionCode, data);
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            controllerManager.HandleRequest(requestCode, actionCode, data, client); // 减少耦合，client 只与 server 来进行交互
        }
    }
    
}