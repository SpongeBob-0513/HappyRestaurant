using System;
using Common;
using Manager;
using Tools;
using UnityEngine;

namespace Request
{
    public class MakeFoodRequest : BaseRequest
    {
        public PlayerManager playerMng;
        private bool isMakeFood = false;
        private RoleType rt;
        private Vector3 pos;
        private Vector3 rotation;
        
        public override void Awake()
        {
            _requestCode = RequestCode.Game;
            _actionCode = ActionCode.MakeFood;
            base.Awake();
        }

        private void Update()
        {
            if (isMakeFood)
            {
                playerMng.RemoteMakeFood(rt, pos, rotation);
                isMakeFood = false;
            }
        }

        // 把需要同步的 食物类型位置和旋转角度 发送给服务器来实现同步
        public void SendRequest(RoleType rt, Vector3 pos, Vector3 rotation) 
        {
            string data = string.Format("{0}|{1},{2},{3}|{4},{5},{6}", (int) rt, pos.x, pos.y, pos.z,
                rotation.x, rotation.y, rotation.y);
            base.SendRequest(data);
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            string[] strs = data.Split('|');
            RoleType rt = (RoleType) int.Parse(strs[0]);
            Vector3 pos = UnityTools.ParseVector3(strs[1]);
            Vector3 rotation = UnityTools.ParseVector3(strs[2]);
            isMakeFood = true;
            this.rt = rt;
            this.pos = pos;
            this.rotation = rotation;
        }
    }
}