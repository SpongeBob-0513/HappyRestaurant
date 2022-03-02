using System;
using System.Net.Sockets;
using Manager;
using UnityEngine;

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

        public override void OnInit()
        {
            base.OnInit();
            
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(IP, PORT);
            }
            catch (Exception e)
            {
                Debug.LogWarning("无法连接到服务器端!!" + e);
                throw;
            }
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