﻿using Common;
using Model;

namespace Request
{
    public class JoinRoomRequest : BaseRequest
    {
        private RoomListPanel roomListPanel;
        
        public override void Awake()
        {
            _requestCode = RequestCode.Room;
            _actionCode = ActionCode.JoinRoom;
            roomListPanel = GetComponent<RoomListPanel>();
            base.Awake();
        }

        public void SendRequest(int id)
        {
            base.SendRequest(id.ToString());
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            string[] strs = data.Split('-');
            string[] strs2 = strs[0].Split(',');
            ReturnCode returnCode = (ReturnCode) int.Parse(strs2[0]);
            UserData ud1 = null;
            UserData ud2 = null;
            if (returnCode == ReturnCode.Success)
            {
                string[] udstrArray = strs[1].Split('|');
                ud1 = new UserData(udstrArray[0]);
                ud2 = new UserData(udstrArray[1]);
                
                RoleType roleType = (RoleType) int.Parse(strs2[1]);
                _facade.SetCurrentRoleType(roleType);
            }
            roomListPanel.OnJoinResponse(returnCode, ud1, ud2);
        }
    }
}