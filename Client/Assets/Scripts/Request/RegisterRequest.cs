using Common;

namespace Request
{
    public class RegisterRequest: BaseRequest
    {
        private RegisterPanel registerPanel;
        
        public override void Awake()
        {
            _requestCode = RequestCode.User;
            _actionCode = ActionCode.Register;
            registerPanel = GetComponent<RegisterPanel>();
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
            ReturnCode returnCode = (ReturnCode) int.Parse(data);
            registerPanel.OnRegisterResponse(returnCode);
        }
    }
}