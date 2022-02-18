using System;
using System.Net.Sockets;

namespace Server.Server
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
        
        public Client(){}

        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
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
            
                msg.ReadMessage(count);
                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }

        private void Close()
        {
            if (clientSocket != null)
            {
                clientSocket.Close();
                server.RemoveClient(this);
            }
        }
    }
}