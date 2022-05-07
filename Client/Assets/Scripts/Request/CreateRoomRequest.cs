using Common;
using Model;
using UIPanel;

namespace Request
{
    public class CreateRoomRequest : BaseRequest
    {
        private RoomPanel roomPanel;
        
        public override void Awake()
        {
            _requestCode = RequestCode.Room;
            _actionCode = ActionCode.CreateRoom;
            base.Awake();
        }

        public void SetPanel(BasePanel panel)
        {
            roomPanel = panel as RoomPanel;
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
                roomPanel.SetP1ResSync();
            }
        }
    }
}