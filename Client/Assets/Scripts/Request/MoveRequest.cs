using System;
using Common;
using Tools;
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
        private float forward;

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
            SendRequest(position.x, position.y, position.z, eulerAngles.x, eulerAngles.y, eulerAngles.z, 
            localPlayerMove.forward);
        }
        
        // 同步远程的 move
        private void SyncRemotePlayer()
        {
            remotePlayerTransform.position = pos;
            remotePlayerTransform.eulerAngles = rotation;
            remotePlayerAnim.SetFloat("Forward", forward);
        }

        public void SendRequest(float x, float y, float z, float rotationX, float rotationY, float 
        rotationZ, float forward)
        {
            string data = string.Format("{0},{1},{2}|{3},{4},{5}|{6}", x, y, z, rotationX, rotationY,
                rotationZ, forward);
            base.SendRequest(data);
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            string[] strs = data.Split('|');
            pos = UnityTools.ParseVector3(strs[0]);
            rotation = UnityTools.ParseVector3(strs[1]);
            forward = float.Parse(strs[2]);
            isSyncRemotePlayer = true;
        }
    }
}