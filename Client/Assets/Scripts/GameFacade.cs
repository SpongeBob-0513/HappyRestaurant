using System;
using Manager;
using Net;
using UIFramework.Manager;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    private UIManager uiMng;
    private AudioManager audioMng;
    private PlayerManager playerMng;
    private CameraManager cameraMng;
    private RequestManager requestMng;
    private ClientManager clientMng;
    
    // Start is called before the first frame update
    void Start()
    {
        InitManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      private void InitManager()
    {
        uiMng = new UIManager();
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

    private void DestroryManager()
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
        DestroryManager();
    }
}
