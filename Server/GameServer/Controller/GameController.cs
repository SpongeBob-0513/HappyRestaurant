using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    public class GameController : BaseController
    {
        public GameController()
        {
            _requestCode = RequestCode.Game;
        }

        public string StartGame(string data, Client client, Server server)
        {
            if (client.IsHouseOwner())
            {
                Room room = client.Room;
                room.BroadcastMessage(client, ActionCode.StartGame, ((int) ReturnCode.Success).ToString());
                room.StartTimer();
                return ((int) ReturnCode.Success).ToString();
            }
            else
            {
                return ((int) ReturnCode.Fail).ToString();
            }
        }
        
        public string Move(string data, Client client, Server server)
        {
            Room room = client.Room;
            if (room != null)
                room.BroadcastMessage(client, ActionCode.Move, data);
            return null; // 发送请求的是需要其他同步移动的客户端，所以本身不需要返回同步信息，返回 null
        }
    }
}