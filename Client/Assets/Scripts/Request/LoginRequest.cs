using System;
using Common;
using Model;

namespace Request
{
    public class LoginRequest:BaseRequest
    {
        private LoginPanel _loginPanel;

        public override void Awake()
        {
            _requestCode = RequestCode.User;
            _actionCode = ActionCode.Login;
            _loginPanel = GetComponent<LoginPanel>();
            base.Awake();
        }

        public void SendRequest(string username, string password)
        {
            string data = username + "," + password;
            base.SendRequest(data);
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            string[] strs = data.Split(',');
            ReturnCode returnCode = (ReturnCode) int.Parse(strs[0]);
            _loginPanel.OnLoginResponse(returnCode);
            if (returnCode == ReturnCode.Success)
            {
                string username = strs[1];
                int totalCount = int.Parse(strs[2]);
                int maxScore = int.Parse(strs[3]);
                UserData userData = new UserData(username, totalCount, maxScore);
                facade.SetUserData(userData);
            }
        }
    }
}