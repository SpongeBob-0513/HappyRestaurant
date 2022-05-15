using System;
using Common;
using UnityEngine;

namespace Request
{
    public class MoveRequest : BaseRequest
    {
        private Transform localPlayerTransform;
        private PlayerMove localPlayerMove;
        private int syncRate = 30; // 同步频率  每秒 20 次
        
        private Transform remotePlayerTransform; // 需要同步的角色
        private Animator remotePlayerAnim;
       
        private bool isSyncRemotePlayer = false;
        private Vector3 pos;
        private Vector3 rotation;
        private bool isWalk;
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        public override void Awake()
        {
            _requestCode = RequestCode.Game;
            _actionCode = ActionCode.Move;
            base.Awake();
        }

        private void Start()
        {
            InvokeRepeating("SyncLocalPlayer", 1f, 1f / syncRate);
        }

        private void FixedUpdate()
        {
            if (isSyncRemotePlayer)
            {
                isSyncRemotePlayer = false;
                SyncRemotePlayer();
            }
        }

        public MoveRequest SetLocalPlayer(Transform localPlayerTransform, PlayerMove localPlayerMove)
        {
            this.localPlayerTransform = localPlayerTransform;
            this.localPlayerMove = localPlayerMove;
            return this;
        }

        public MoveRequest SetRemotePlayer(Transform remotePlayerTransform)
        {
            this.remotePlayerTransform = remotePlayerTransform;
            this.remotePlayerAnim = remotePlayerTransform.GetComponent<Animator>();
            return this;
        }
        
        // 向远程发起请求
        private void SyncLocalPlayer()
        {
            var position = localPlayerTransform.position;
            var eulerAngles = localPlayerTransform.eulerAngles;
            SendRequest(position.x, position.y, position.z, eulerAngles.x, eulerAngles.y, eulerAngles.z, localPlayerMove.isWalk);
        }
        
        // 同步远程的 move
        private void SyncRemotePlayer()
        {
            remotePlayerTransform.position = pos;
            remotePlayerTransform.eulerAngles = rotation;
            remotePlayerAnim.SetBool(IsWalk, isWalk);
        }

        public void SendRequest(float x, float y, float z, float rotationX, float rotationY, float 
        rotationZ, bool isMove)
        {
            string data = string.Format("{0},{1},{2}|{3},{4},{5}|{6}", x, y, z, rotationX, rotationY,
                rotationZ, isMove);
            base.SendRequest(data);
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            string[] strs = data.Split('|');
            pos = Parse(strs[0]);
            rotation = Parse(strs[1]);
            isWalk = strs[2] == "True";
            isSyncRemotePlayer = true;
        }

        private Vector3 Parse(string str)
        {
            string[] strs = str.Split(',');
            float x = float.Parse(strs[0]);
            float y = float.Parse(strs[1]);
            float z = float.Parse(strs[2]);
            return new Vector3(x, y, z);
        }
    }
}