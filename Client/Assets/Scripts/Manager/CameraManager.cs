using DG.Tweening;
using UnityEngine;

namespace Manager
{
    public class CameraManager:BaseManager
    {
        private GameObject cameraGo;
        private FollowTarget followTarget;
        private Vector3 originalPostition;
        private Vector3 originalRotation;

        public CameraManager(GameFacade gameFacade) : base(gameFacade)
        {
        }

        public override void OnInit()
        {
            base.OnInit();
            cameraGo = Camera.main.gameObject;
            followTarget = cameraGo.GetComponent<FollowTarget>();
            originalPostition = cameraGo.transform.position;
            originalRotation = cameraGo.transform.eulerAngles;
        }

        // public override void Update()
        // {
        //     base.Update();
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         FollowRole(GameObject.Find("Chef").transform);
        //     }
        //     if (Input.GetMouseButtonDown(1))
        //     {
        //         BackDefaultPos();
        //     }
        // }

        public void FollowRole()
        {
            followTarget.target = _gameFacade.GetCurrentRoleGameObject().transform;
            followTarget.enabled = true;
            
            cameraGo.transform.DORotate(new Vector3(30, 0, 0), 1f);
        }

        public void BackDefaultPos()
        {
            followTarget.enabled = false;
            cameraGo.transform.DOMove(originalPostition, 1f);
            cameraGo.transform.DORotate(originalRotation, 1f);
        }
    }
}