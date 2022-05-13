using Common;

namespace Request
{
    public class StartGameRequest : BaseRequest
    {
        private RoomPanel roomPanel;
        
        public override void Awake()
        {
            _requestCode = RequestCode.Game;
            _actionCode = ActionCode.StartGame;
            roomPanel = GetComponent<RoomPanel>();
            base.Awake();
        }

        public override void SendRequest()
        {
            base.SendRequest("r");
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            ReturnCode returnCode = (ReturnCode) int.Parse(data);
            roomPanel.OnStartResponse(returnCode);
        }
    }
}