using Common;
using Model;

namespace Request
{
    public class UpdateRoomRequest : BaseRequest
    {
        private RoomPanel roomPanel;
        public override void Awake()
        {
            _requestCode = RequestCode.Room;
            _actionCode = ActionCode.UpdateRoom;
            roomPanel = GetComponent<RoomPanel>();
            base.Awake();
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            UserData ud1 = null;
            UserData ud2 = null;
            string[] udstrArray = data.Split('|');
            ud1 = new UserData(udstrArray[0]);
            ud2 = new UserData(udstrArray[1]);
            roomPanel.SetAllPlayerResSync(ud1, ud2);
        }
    }
}