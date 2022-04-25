using Common;
using GameServer.DAO;
using GameServer.Model;
using GameServer.Servers;

namespace GameServer.Controller
{
    public class UserController:BaseController
    {
        private UserDAO _userDAO = new UserDAO();
        public UserController()
        {
            _requestCode = RequestCode.User;
        }

        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            User user =  _userDAO.VerifyUser(client.MysqlConn, strs[0], strs[1]);
            if (user == null)
            {
                 return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                return ((int)ReturnCode.Success).ToString();
            }
        }
        
        public string Register(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];
            bool res = _userDAO.GetUsername(client.MysqlConn, username);
            if (res)
            {
                return ((int) ReturnCode.Fail).ToString();
            }
            _userDAO.AddUser(client.MysqlConn, username, password);
            return ((int) ReturnCode.Success).ToString();
        }
    }
}