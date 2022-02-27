using System;
using Manager;
using Net;
using UIFramework.Manager;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private UIManeger uiMng;
    private AudioManager audioMng;
    private PlayerManager playerMng;
    private CameraManager cameraMng;
    private RequestManager requestMng;
    private ClientManager clientMng;
    
    // Start is called before the first frame update
    void Start()
    {
        InitMng();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      private void InitMng()
    {
        uiMng = new UIManeger();
        audioMng = new AudioManager();
        playerMng = new PlayerManager();
        cameraMng = new CameraManager();
        requestMng = new RequestManager();
        clientMng = new ClientManager();
        
        uiMng.OnInit();
        audioMng.OnInit();
        playerMng.OnInit();
        cameraMng.OnInit();
        cameraMng.OnInit();
        requestMng.OnInit();
        clientMng.OnInit();
    }

    private void DestroryMng()
    {
        uiMng.OnDestroy();
        audioMng.OnDestroy();
        playerMng.OnDestroy();
        cameraMng.OnDestroy();
        cameraMng.OnDestroy();
        requestMng.OnDestroy();
        clientMng.OnDestroy();
    }

    private void OnDestroy()
    {
        DestroryMng();
    }
}
