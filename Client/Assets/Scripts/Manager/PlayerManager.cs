using System.Collections.Generic;
using Common;
using Model;
using Player;
using Request;
using UnityEngine;

namespace Manager
{
    public class PlayerManager : BaseManager
    {
        private UserData userData;
        private Dictionary<RoleType, RoleData> roleDataDict = new Dictionary<RoleType, RoleData>();
        private Transform RolePositions;
        private RoleType currentRoleType; // 当前角色类型
        private GameObject currentRoleGameObject; // 当前玩家在场景中对应的游戏体
        private GameObject playerSyncRequest; // 用来放置 玩家相关 Request 的游戏体
        private GameObject remoteRoleGameObject; // 远程玩家在场景中对应的游戏体

        public void SetCurrentRoleType(RoleType rt)
        {
            currentRoleType = rt;
        }

        public PlayerManager(GameFacade gameFacade) : base(gameFacade)
        {
        }

        public override void OnInit()
        {
            base.OnInit();

            RolePositions = GameObject.Find("RolePositions").transform;
            InitRoleDataDict();
        }

        public UserData UserData
        {
            get => userData;
            set => userData = value;
        }

        private void InitRoleDataDict()
        {
            roleDataDict.Add(RoleType.Chef1, new RoleData(RoleType.Chef1, "Chef", RolePositions.Find("Position1")));
            roleDataDict.Add(RoleType.Chef2, new RoleData(RoleType.Chef2, "Chef", RolePositions.Find("Position2")));
            //roleDataDict.Add(RoleType.Worker, new RoleData(RoleType.Worker, "Worker", RolePositions.Find("Positon3")));
        }

        public void SpawnRoles()
        {
            foreach (RoleData rd in roleDataDict.Values)
            {
                GameObject go = GameObject.Instantiate(rd.RolePrefab, rd.SpawnPosition, Quaternion.identity);
                if (rd.RoleType == currentRoleType)
                {
                    currentRoleGameObject = go;
                }
                else
                {
                    remoteRoleGameObject = go;
                }
            }
        }
        
        // 返回当前玩家的游戏体 方便对控制的玩家进行移动等操作
        public GameObject GetCurrentRoleGameObject()
        {
            return currentRoleGameObject;
        }

        public void AddControlScript()
        {
            currentRoleGameObject.AddComponent<PlayerMove>();
            currentRoleGameObject.AddComponent<PlayerCut>();
        }

        public void CreateSyncRequest()
        {
            playerSyncRequest = new GameObject("PlayerSyncRequest");
            playerSyncRequest.AddComponent<MoveRequest>().SetLocalPlayer(currentRoleGameObject.transform, 
            currentRoleGameObject.GetComponent<PlayerMove>()).SetRemotePlayer(remoteRoleGameObject.transform);
        }
    }
}