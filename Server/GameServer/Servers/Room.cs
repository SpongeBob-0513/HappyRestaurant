using System.Collections.Generic;

namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,
        WaitingStart,
        Playing,
        End
    }
    
    public class Room
    {
        private List<Client> clientRoom = new List<Client>(); // 储存当前房间的所有客户端
    }
}