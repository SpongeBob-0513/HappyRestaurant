using System;
using Common;

namespace Request
{
    public class UpdateResultRequest : BaseRequest
    {
        private RoomListPanel roomListPanel;
        private bool isUpdateResult = false;
        private int totalCount;
        private int maxScore;
        
        public override void Awake()
        {
            _actionCode = ActionCode.UpdateResult;
            roomListPanel = GetComponent<RoomListPanel>();
            base.Awake();
        }

        private void Update()
        {
            if (isUpdateResult)
            {
                roomListPanel.OnUpdateResultResponse(totalCount, maxScore);
                isUpdateResult = false;
            }
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            string[] strs = data.Split(',');
            totalCount = int.Parse(strs[0]);
            maxScore = int.Parse(strs[1]);
            isUpdateResult = true;
        }
    }
}