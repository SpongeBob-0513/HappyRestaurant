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
        
        private MakeFoodRequest makeFoodRequest;
        private ScoreRequest scoreRequest;

        public void UpdateResult(int totalCount, int maxScore)
        {
            userData.TotalCount = totalCount;
            userData.MaxScore = maxScore;
        }

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
            roleDataDict.Add(RoleType.Chef1,
                new RoleData(RoleType.Chef1, "Chef1", "CarrotCut", RolePositions.Find("Position1")));
            roleDataDict.Add(RoleType.Chef2,
                new RoleData(RoleType.Chef2, "Chef2", "CarrotSoup", RolePositions.Find("Position2")));
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

        private RoleData GetRoleData(RoleType rt)
        {
            RoleData rd = null;
            roleDataDict.TryGetValue(rt, out rd);
            return rd;
        }

        public void AddControlScript()
        {
            currentRoleGameObject.AddComponent<PlayerMove>();
            PlayerMakeFood playerMakeFood = currentRoleGameObject.AddComponent<PlayerMakeFood>();
            RoleType rt = currentRoleGameObject.GetComponent<PlayerInfo>().roleType;
            RoleData rd = GetRoleData(rt);
            playerMakeFood.dishFoodPrefab = rd.DishFoodPrefab;
            playerMakeFood.SetPlayMng(this);
        }

        public void CreateSyncRequest()
        {
            playerSyncRequest = new GameObject("PlayerSyncRequest");
            playerSyncRequest.AddComponent<MoveRequest>().SetLocalPlayer(currentRoleGameObject.transform,
                currentRoleGameObject.GetComponent<PlayerMove>()).SetRemotePlayer(remoteRoleGameObject.transform);
            makeFoodRequest = playerSyncRequest.AddComponent<MakeFoodRequest>();
            makeFoodRequest.playerMng = this;
            scoreRequest = playerSyncRequest.AddComponent<ScoreRequest>();
        }
        
        // 本地的菜品制作以及生成
        public void MakeFood(GameObject dishFoodPrefab, Vector3 pos, Quaternion rotation)
        {
            GameObject.Instantiate(dishFoodPrefab, pos, rotation).GetComponent<DishFood>().isLocal = true;
             makeFoodRequest.SendRequest(dishFoodPrefab.GetComponent<DishFood>().roleType, pos, rotation
            .eulerAngles);
        }
        
        // 远程的菜品制作以及生成
        public void RemoteMakeFood(RoleType rt, Vector3 pos, Vector3 rotation)
        {
            GameObject dishFoodPrefab = GetRoleData(rt).DishFoodPrefab;
            Transform transform = GameObject.Instantiate(dishFoodPrefab).GetComponent<Transform>();
            transform.position = pos;
            transform.eulerAngles = rotation;
        }

        public void SendScore(int score)
        {
            scoreRequest.SendRequest(score);
        }

        public void GameOver()
        {
            GameObject.Destroy(currentRoleGameObject);
            GameObject.Destroy(playerSyncRequest);
            GameObject.Destroy(remoteRoleGameObject);
            makeFoodRequest = null;
            scoreRequest = null;
        }
    }
}