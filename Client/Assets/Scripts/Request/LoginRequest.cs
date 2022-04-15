using System;
using Common;

namespace Request
{
    public class LoginRequest:BaseRequest
    {
        private LoginPanel _loginPanel;
        
        private void Start()
        {
            _requestCode = RequestCode.User;
            _actionCode = ActionCode.Login;
            _loginPanel = GetComponent<LoginPanel>();
        }

        public void SendRequest(string username, string password)
        {
            string data = username + "," + password;
            base.SendRequest(data);
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            ReturnCode returnCode = (ReturnCode) int.Parse(data);
            _loginPanel.OnLoginResponse(returnCode);
        }
    }
}