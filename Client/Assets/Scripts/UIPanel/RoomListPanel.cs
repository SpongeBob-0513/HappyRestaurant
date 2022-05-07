using System;
using System.Collections.Generic;
using Common;
using DG.Tweening;
using Model;
using Request;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    private RectTransform personalInform;
    private RectTransform roomList;
    private VerticalLayoutGroup roomLayout;
    private GameObject roomItemPrefab;
    private ListRoomRequest listRoomRequest;
    private CreateRoomRequest createRoomRequest;
    private JoinRoomRequest joinRoomRequest;
    private List<UserData> udList = null;

    private void Start()
    {
        personalInform = transform.Find("PersonalInform").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        roomLayout = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        transform.Find("RoomList/CreateRoomButton").GetComponent<Button>().onClick.AddListener(OnCreatRoomClick);
        transform.Find("RoomList/RefreshButton").GetComponent<Button>().onClick.AddListener(OnRefreshClick);
        listRoomRequest = GetComponent<ListRoomRequest>();
        createRoomRequest = GetComponent<CreateRoomRequest>();
        joinRoomRequest = GetComponent<JoinRoomRequest>();
        EnterAnim();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (personalInform != null)
        {
            EnterAnim();
        }
        SetPersonalInfom();
        if (listRoomRequest == null)
        {
            listRoomRequest = GetComponent<ListRoomRequest>();
        }
        listRoomRequest.SendRequest();
    }

    public override void OnExit()
    {
        base.OnExit();
        HideAnim();
    }

    public override void OnPause()
    {
        base.OnPause();
        HideAnim();
    }

    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
        listRoomRequest.SendRequest();
    }

    private void Update()
    {
        if (udList != null)
        {
            LoadRoomItem(udList);
            udList = null;
        }
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        uiMng.PopPanel();
    }

    private void OnCreatRoomClick()
    {
        BasePanel panel = uiMng.PushPanel(UIPanelType.Room);
        createRoomRequest.SetPanel(panel);
        createRoomRequest.SendRequest();
    }

    private void OnRefreshClick()
    {
        listRoomRequest.SendRequest();  // 向服务器端重新申请房间信息
    }

    private void EnterAnim()
    {
        gameObject.SetActive(true);
        personalInform.localPosition = new Vector3(-1000, 0);
        personalInform.DOLocalMoveX(-219, 0.5f);
        
        roomList.localPosition = new Vector3(1000, 0);
        roomList.DOLocalMoveX(62, 0.5f);
    }

    private void HideAnim()
    {
        personalInform.DOLocalMoveX(-1000, 0.5f);
        roomList.DOLocalMoveX(1000, 0.5f).OnComplete(() => { gameObject.SetActive(false); });
    }

    private void SetPersonalInfom()
    {
        UserData userData = facade.GetUserData();
        transform.Find("PersonalInform/Username").GetComponent<Text>().text = userData.Username;
        transform.Find("PersonalInform/TotalCount").GetComponent<Text>().text = "总场数：" + userData.TotalCount;
        transform.Find("PersonalInform/MaxScore").GetComponent<Text>().text = "最高得分：" + userData.MaxScore;
    }

    public void LoadRoomItemSync(List<UserData> udList)
    {
        this.udList = udList;
    }

    private void LoadRoomItem(List<UserData> udList)
    {
        RoomItem[] riArray = roomLayout.GetComponentsInChildren<RoomItem>();
        foreach (RoomItem ri in riArray)
        {
            ri.DestroySelf();
        }
        
        int count = udList.Count;
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab, roomLayout.transform);
            UserData ud = udList[i];
            roomItem.GetComponent<RoomItem>().SetRoomInform(ud.Id,ud.Username, ud.TotalCount, ud.MaxScore, this);
        }

        int roomCount = GetComponentsInChildren<RoomItem>().Length;
        Vector2 size = roomLayout.GetComponent<RectTransform>().sizeDelta;
        roomLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, roomCount * (roomItemPrefab
        .GetComponent<RectTransform>().sizeDelta.y + roomLayout.spacing));
    }

    public void OnJoinClick(int id)
    {
        joinRoomRequest.SendRequest(id);
    }

    public void OnJoinResponse(ReturnCode returnCode, UserData ud1, UserData ud2)
    {
        switch (returnCode)
        {
            case ReturnCode.NotFound:
                uiMng.ShowMessageSync("房间被销毁，无法加入");
                break;
            case ReturnCode.Fail:
                uiMng.ShowMessageSync("房间已满，无法加入");
                break;
            case ReturnCode.Success:
                BasePanel panel = uiMng.PushPanel(UIPanelType.Room);
                (panel as RoomPanel).SetAllPlayerResSync(ud1, ud2);
                break;
        }
    }
}
