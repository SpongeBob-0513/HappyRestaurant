using System.Collections.Generic;
using Common;
using Model;

namespace Request
{
    public class ListRoomRequest : BaseRequest
    {
        private RoomListPanel roomListPanel;

        public override void Awake()
        {
            _requestCode = RequestCode.Room;
            _actionCode = ActionCode.ListRoom;
            roomListPanel = GetComponent<RoomListPanel>();
            base.Awake();
        }

        public override void SendRequest()
        {
            base.SendRequest("r");
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data); 
            List<UserData> udList = new List<UserData>();
            if (data != "0")
            {
                string[] udArray = data.Split('|');
                foreach (var ud in udArray)
                {
                    string[] strs = ud.Split(',');
                    udList.Add(new UserData(int.Parse(strs[0]), strs[1], int.Parse(strs[2]), int.Parse(strs[3])));
                }
            }
            roomListPanel.LoadRoomItemSync(udList);
        }
    }
}