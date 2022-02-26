using System;
using System.Net.Sockets;
using Common;
using GameServer.Tool;
using MySql.Data.MySqlClient;

namespace GameServer.Servers
{
    /// <summary>
    /// 与客户端之间进行通信
    /// 接收客户端的信息 向客户端发送信息
    /// </summary>
    public class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message msg = new Message();
        private MySqlConnection mysqlConn;
        
        public Client(){}

        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            mysqlConn = ConnHelper.Connect();
        }

        public void Start()
        {
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);
                if (count == 0)
                {
                    Close(); // 没有接收到数据，断开连接
                }
            
                msg.ReadMessage(count, OnProssMessage);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private void OnProssMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        private void Close()
        {
            ConnHelper.CloseConnection(mysqlConn);
            if (clientSocket != null)
            {
                clientSocket.Close();
                server.RemoveClient(this);
            }
        }
        
        // 对返回给客户端的消息进行包装和发送
        public void Send(RequestCode requestCode, string data)
        {
            byte[] bytes = Message.PackData(requestCode, data);
            clientSocket.Send(bytes);
        }
    }
}