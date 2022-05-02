using Common;
using Manager;
using Model;
using Net;
using Request;
using UnityEngine;

public class GameFacade : MonoBehaviour
{
    // 单例模式 整个客户端只设置这一个单例
    private static GameFacade _instance;
    public static GameFacade Instance => _instance;

    private UIManager uiMng;
    private AudioManager audioMng;
    private PlayerManager playerMng;
    private CameraManager cameraMng;
    private RequestManager requestMng;
    private ClientManager clientMng;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject); return;
        }

        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        InitManager();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateManager();
    }

    private void InitManager()
    {
        uiMng = new UIManager(this);
        audioMng = new AudioManager(this);
        playerMng = new PlayerManager(this);
        cameraMng = new CameraManager(this);
        requestMng = new RequestManager(this);
        clientMng = new ClientManager(this);
        
        uiMng.OnInit();
        audioMng.OnInit();
        playerMng.OnInit();
        cameraMng.OnInit();
        cameraMng.OnInit();
        requestMng.OnInit();
        clientMng.OnInit();
    }

    private void UpdateManager()
    {
        uiMng.Update();
        audioMng.Update();
        playerMng.Update();
        cameraMng.Update();
        cameraMng.Update();
        requestMng.Update();
        clientMng.Update();
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

    public void AddRequest(ActionCode actionCode, BaseRequest baseRequest)
    {
        requestMng.AddRequest(actionCode, baseRequest);
    }

    public void RemoveRequest(ActionCode actionCode)
    {
        requestMng.RemoveRequest(actionCode);
    }

    public void HandleResponse(ActionCode actionCode, string data)
    {
        requestMng.HandleResponse(actionCode, data);
    }

    public void ShowMessage(string msg)
    {
        uiMng.ShowMessage(msg);
    }

    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientMng.SendRequest(requestCode, actionCode, data);
    }

    public void PlayBgSound(string soundName)
    {
        audioMng.PlayBgSound(soundName);
    }

    public void PlayNormalSound(string soundName)
    {
        audioMng.PlayNormalSound(soundName);
    }

    public void SetUserData(UserData userData)
    {
        playerMng.UserData = userData;
    }

    public UserData GetUserData()
    {
        return playerMng.UserData;
    }
}
