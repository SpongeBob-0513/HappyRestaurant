using System.Collections.Generic;
using System.Text;
using Common;

namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,
        WaitingPlay,
        Playing,
        End
    }
    
    public class Room
    {
        private List<Client> clientRoom = new List<Client>(); // 储存当前房间的所有客户端
        private RoomState state = RoomState.WaitingJoin;
        private Server server;

        public Room(Server server)
        {
            this.server = server;
        }

        public bool IsWaitingJoin()
        {
            return state == RoomState.WaitingJoin;
        }

        public void AddClient(Client client)
        {
            clientRoom.Add(client);
            client.Room = this;
            if (clientRoom.Count >= 2)
            {
                state = RoomState.WaitingPlay;
            }
        }

        public void RemoveClient(Client client)
        {
            client.Room = null;
            clientRoom.Remove(client);
            if (clientRoom.Count >= 2)
            {
                state = RoomState.WaitingPlay;
            }
            else
            {
                state = RoomState.WaitingJoin;
            }
        }

        public string GetHouseOwnerData()
        {
            return clientRoom[0].GetUserData();
        }

        public void Close(Client client)
        {
            if (client == clientRoom[0])
            {
                server.RemoveRoom(this);
            }
            else
            {
                clientRoom.Remove(client);
            }
        }

        public int GetId()
        {
            if (clientRoom.Count > 0)
            {
                return clientRoom[0].GetUserId();
            }

            return -1;
        }

        public string GetRoomData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var client in clientRoom)
            {
                sb.Append(client.GetUserData() + "|");
            }

            if (sb.Length > 0)
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        public void BroadcastMessage(Client excludeClient, ActionCode actionCode, string data)
        {
            foreach (var client in clientRoom)
            {
                if (client != excludeClient)
                {
                    server.SendRespoonse(client, actionCode, data);
                }
            }
        }

        public bool IsHouseOwner(Client client)
        {
            return client == clientRoom[0];
        }
    }
}