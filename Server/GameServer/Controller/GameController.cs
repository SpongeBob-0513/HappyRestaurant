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
                return ((int) ReturnCode.Success).ToString();
            }
            else
            {
                return ((int) ReturnCode.Fail).ToString();
            }
        }
    }
}