using System;
using DG.Tweening;
using Model;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class RoomListPanel : BasePanel
{
    private RectTransform personalInform;
    private RectTransform roomList;
    private VerticalLayoutGroup roomLayout;
    private GameObject roomItemPrefab;

    private void Start()
    {
        personalInform = transform.Find("PersonalInform").GetComponent<RectTransform>();
        roomList = transform.Find("RoomList").GetComponent<RectTransform>();
        transform.Find("RoomList/CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
        roomLayout = transform.Find("RoomList/ScrollRect/Layout").GetComponent<VerticalLayoutGroup>();
        roomItemPrefab = Resources.Load("UIPanel/RoomItem") as GameObject;
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
    }

    public override void OnExit()
    {
        base.OnExit();
        HideAnim();
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        uiMng.PopPanel();
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
        transform.Find("PersonalInform/TotalCount").GetComponent<Text>().text = "总场数:" + userData.TotalCount;
        transform.Find("PersonalInform/MaxScore").GetComponent<Text>().text = "最高得分:" + userData.MaxScore;
    }

    private void LoadRoomItem(int count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject roomItem = GameObject.Instantiate(roomItemPrefab, roomLayout.transform);
        }

        int roomCount = GetComponentsInChildren<RoomItem>().Length;
        Vector2 size = roomLayout.GetComponent<RectTransform>().sizeDelta;
        roomLayout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, roomCount * (roomItemPrefab
        .GetComponent<RectTransform>().sizeDelta.y + roomLayout.spacing));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadRoomItem(1);
        }
    }
}
