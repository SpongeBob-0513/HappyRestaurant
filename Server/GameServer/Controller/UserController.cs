using Common;
using GameServer.DAO;
using GameServer.Model;
using GameServer.Servers;

namespace GameServer.Controller
{
    public class UserController:BaseController
    {
        private UserDAO userDAO = new UserDAO();
        private ResultDAO resultDAO = new ResultDAO();
        public UserController()
        {
            _requestCode = RequestCode.User;
        }

        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            User user =  userDAO.VerifyUser(client.MysqlConn, strs[0], strs[1]);
            if (user == null)
            {
                 return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                Result result = resultDAO.GetResultByUserId(client.MysqlConn, user.Id);
                client.SetUserData(user, result);
                return string.Format("{0}, {1}, {2}, {3}", ((int)ReturnCode.Success).ToString(), user.Username, result
                    .TotalCount, result.MaxScore); // 字符串组拼
            }
        }
        
        public string Register(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            string username = strs[0];
            string password = strs[1];
            bool res = userDAO.GetUsername(client.MysqlConn, username);
            if (res)
            {
                return ((int) ReturnCode.Fail).ToString();
            }
            userDAO.AddUser(client.MysqlConn, username, password);
            return ((int) ReturnCode.Success).ToString();
        }
    }
}