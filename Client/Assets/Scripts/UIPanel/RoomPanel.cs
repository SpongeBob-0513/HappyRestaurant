using System;
using Common;
using DG.Tweening;
using Model;
using Request;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class RoomPanel : BasePanel
{
    private Text P1Username;
    private Text P1TotalCount;
    private Text P1MaxScore;
    
    private Text P2Username;
    private Text P2TotalCount;
    private Text P2MaxScore;

    private Transform p1Panel;
    private Transform p2Panel;
    private Transform startButton;
    private Transform exitButton;

    private UserData ud = null;
    private UserData ud1 = null;
    private UserData ud2 = null;

    private QuitRoomRequest quitRoomRequest;
    private StartGameRequest startGameRequest;

    private void Start()
    {
        P1Username = transform.Find("P1Panel/Username").GetComponent<Text>();
        P1TotalCount = transform.Find("P1Panel/TotalCount").GetComponent<Text>();
        P1MaxScore = transform.Find("P1Panel/MaxScore").GetComponent<Text>();
        
        P2Username = transform.Find("P2Panel/Username").GetComponent<Text>();
        P2TotalCount = transform.Find("P2Panel/TotalCount").GetComponent<Text>();
        P2MaxScore = transform.Find("P2Panel/MaxScore").GetComponent<Text>();

        p1Panel = transform.Find("P1Panel");
        p2Panel = transform.Find("P2Panel");
        startButton = transform.Find("StartButton");
        exitButton = transform.Find("ExitButton");

        transform.Find("StartButton").GetComponent<Button>().onClick.AddListener(OnStartClick);
        transform.Find("ExitButton").GetComponent<Button>().onClick.AddListener(OnExitClick);

        quitRoomRequest = GetComponent<QuitRoomRequest>();
        startGameRequest = GetComponent<StartGameRequest>();

        EnterAnim();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        if (p1Panel != null)
        {
            EnterAnim();
        }
    }

    public override void OnPause()
    {
        base.OnPause();
        ExitAnim();
    }

    public override void OnResume()
    {
        base.OnResume();
        EnterAnim();
    }

    private void Update()
    {
        if (ud != null)
        {
            SetP1Res(ud.Username, ud.TotalCount.ToString(), ud.MaxScore.ToString());
            ClearP2Res();
            ud = null;
        }

        if (ud1 != null)
        {
            SetP1Res(ud1.Username, ud1.TotalCount.ToString(), ud1.MaxScore.ToString());
            if (ud2 != null)
            {
                SetP2Res(ud2.Username, ud2.TotalCount.ToString(), ud2.MaxScore.ToString());
            }
            else
            {
                ClearP2Res();
            }
            ud1 = null;
            ud2 = null;
        }
    }

    public override void OnExit()
    {
        base.OnExit();
        ExitAnim();
    }

    public void SetP1Res(string username, string totalCount, string maxScore)
    {
        P1Username.text = username;
        P1TotalCount.text = "总场数：" +  totalCount;
        P1MaxScore.text = "最高得分：" + maxScore;
    }

    public void SetP1ResSync()
    {
        ud = facade.GetUserData();
    }

    private void SetP2Res(string username, string totalCount, string maxScore)
    {
        P2Username.text = username;
        P2TotalCount.text = "总场数：" +  totalCount;
        P2MaxScore.text = "最高得分：" + maxScore;
    }
    
    public void SetAllPlayerResSync(UserData ud1, UserData ud2)
    {
        this.ud1 = ud1;
        this.ud2 = ud2;
    }
    
    public void ClearP2Res()
    {
        P2Username.text = "";
        P2TotalCount.text = "等待玩家加入...";
        P2MaxScore.text = "";
    }

    private void OnStartClick()
    {
        startGameRequest.SendRequest();
    }

    public void OnStartResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Fail)
        {
            uiMng.ShowMessageSync("您不是房主，无法开始游戏！");
        }
        else
        {
            uiMng.PushPanelSync(UIPanelType.Game);
            facade.EnterPlayingSync();
        }
    }

    private void OnExitClick()
    {
        quitRoomRequest.SendRequest();
    }

    public void OnExitResponse()
    {
        uiMng.PopPanelSync();
    }

    private void EnterAnim()
    {
        gameObject.SetActive(true);
        p1Panel.localPosition = new Vector3(-1000, 0, 0);
        p1Panel.DOLocalMoveX(-110, 0.4f);
        p2Panel.localPosition = new Vector3(1000, 0, 0);
        p2Panel.DOLocalMoveX(110, 0.4f);
        startButton.localScale = Vector3.zero;
        startButton.DOScale(1, 0.4f);
        exitButton.localScale = Vector3.zero;
        exitButton.DOScale(1, 0.4f);
    }

    private void ExitAnim()
    {
        p1Panel.DOLocalMoveX(-1000, 0.4f);
        p2Panel.DOLocalMoveX(1000, 0.4f);
        startButton.DOScale(0, 0.4f);
        exitButton.DOScale(0, 0.4f).OnComplete((() => gameObject.SetActive(false)));
    }
}
