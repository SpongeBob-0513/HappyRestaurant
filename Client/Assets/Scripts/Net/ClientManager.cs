using System;
using System.Net.Sockets;
using Manager;
using UnityEngine;
using Common;

namespace Net
{
    /// <summary>
    /// 这个是用来管理跟服务器端的 Socket 连接
    /// </summary>
    public class ClientManager : BaseManager
    {
        private const string IP = "127.0.0.1";
        private const int PORT = 6688;

        private Socket clientSocket;
        private Message msg = new Message();

        public ClientManager(GameFacade gameFacade) : base(gameFacade)
        {
        }

        public override void OnInit()
        {
            base.OnInit();

            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(IP, PORT);
                Start();
            }
            catch (Exception e)
            {
                Debug.LogWarning("无法连接到服务器端!!" + e);
                throw;
            }
        }

        private void Start()
        {
            clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback,
                null); // 异步的消息接收
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                int count = clientSocket.EndReceive(ar);

                msg.ReadMessage(count, processDataCallback);
                
                Start(); // 继续监听消息接收
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void processDataCallback(ActionCode actionCode, string data)
        {
            _gameFacade.HandleResponse(actionCode, data);
        }

        public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
        {
            byte[] bytes = Message.PackData(requestCode, actionCode, data);
            clientSocket.Send(bytes);
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            try
            {
                clientSocket.Close();
            }
            catch (Exception e)
            {
                Debug.LogWarning("无法关闭跟服务器端的连接!!" + e);
                throw;
            }
        }
    }
}