using System.Text;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    public class RoomController:BaseController
    {
        public RoomController()
        {
            _requestCode = RequestCode.Room;
        }

        public string CreateRoom(string data, Client client, Server server)
        {
            server.CreateRoom(client);
            return ((int) ReturnCode.Success).ToString();
        }
        
        public string ListRoom(string data, Client client, Server server)
        {
            StringBuilder sb = new StringBuilder();
            foreach (Room room in server.GetRoomList())
            {
                if (room.IsWaitingJoin())
                {
                    sb.Append(room.GetHouseOwnerData() + "|");
                }
            }

            if (sb.Length == 0)
            {
                sb.Append("0");
            }
            else
            {
                sb.Remove(sb.Length - 1, 1);
            }

            return sb.ToString();
        }

        public string JoinRoom(string data, Client client, Server server)
        {
            int id = int.Parse(data);
            Room room = server.GetRoomById(id);
            if (room == null)
            {
                return ((int) ReturnCode.NotFound).ToString();
            }
            else if(room.IsWaitingJoin() == false)
            {
                return ((int) ReturnCode.Fail).ToString();
            }
            else
            {
                room.AddClient(client);
                string roomData = room.GetRoomData();
                room.BroadcastMessage(client, ActionCode.UpdateRoom, roomData);  // 广播给房主有玩家加入 更新房间信息
                return ((int) ReturnCode.Success).ToString() + "-" + roomData;
            }
        }

        public string QuitRoom(string data, Client client, Server server)
        {
            bool isHouseOwner = client.IsHouseOwner();
            if (isHouseOwner)
            {
                //TODO 房主退出
                return "";
            }
            else
            {
                client.Room.RemoveClient(client);
                //TODO 广播退出房间的消息
                return ((int) ReturnCode.Success).ToString();
            }
        }
    }
}