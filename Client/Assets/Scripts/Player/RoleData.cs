using Common;
using UnityEngine;

namespace Player
{
    public class RoleData
    {
        private const string PREFIX_PREFAB = "Prefabs/";
        public RoleType RoleType { get; private set; }
        public GameObject RolePrefab { get; private set; }
        public GameObject DishFoodPrefab { get; private set; }
        public Vector3 SpawnPosition { get; private set; }

        public RoleData(RoleType roleType, string rolePath, string dishFoodPath, Transform spawnPos)
        {
            this.RoleType = roleType;
            this.RolePrefab = Resources.Load(PREFIX_PREFAB + rolePath) as GameObject;
            this.DishFoodPrefab = Resources.Load(PREFIX_PREFAB + dishFoodPath) as GameObject;
            this.SpawnPosition = spawnPos.position;
        }
    }
}