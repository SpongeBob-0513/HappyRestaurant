using System.Timers;
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
        
        public string MakeFood(string data, Client client, Server server)
        {
            Room room = client.Room;
            if (room != null)
                room.BroadcastMessage(client, ActionCode.MakeFood, data);
            return null; // 发送请求的是需要其他同步移动的客户端，所以本身不需要返回同步信息，返回 null
        }
        
        public string Score(string data, Client client, Server server)
        {
            client.Room.targetDishCount -= 1;
            int score = int.Parse(data);
            client.Room.totalScore += score;
            if (client.Room.targetDishCount <= 0)
            {
                client.Room.BroadcastMessage(null, ActionCode.GameOver, client.Room.totalScore.ToString());
                client.Room.UpdateResult(client.Room.totalScore);
                client.Room.Close(); // 如果已经达到 10 道菜 游戏结束 返回房间的总得分
            }
            return null;
        }

        public string QuitPlaying(string data, Client client, Server server)
        {
            Room room = client.Room;
            if (room != null)
            {
                room.BroadcastMessage(null, ActionCode.QuitPlaying, "r");
                room.Close();
            }

            return null;
        }
    }
}