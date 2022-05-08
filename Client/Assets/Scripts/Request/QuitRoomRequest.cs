using Common;

namespace Request
{
    public class QuitRoomRequest : BaseRequest
    {
        private RoomPanel roomPanel;
        
        public override void Awake()
        {
            _requestCode = RequestCode.Room;
            _actionCode = ActionCode.QuitRoom;
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
            if (returnCode == ReturnCode.Success)
            {
                roomPanel.OnExitResponse();
            }
        }
    }
}