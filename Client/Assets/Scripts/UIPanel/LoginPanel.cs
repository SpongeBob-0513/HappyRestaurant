using System;
using Common;
using DG.Tweening;
using Request;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private Button closeButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest _loginRequest;

    private void Start()
    {
        _loginRequest = GetComponent<LoginRequest>();
        usernameIF = transform.Find("UsernameLable/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLable/PasswordInput").GetComponent<InputField>();
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        EnterAnim();
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
    }
    
    public override void OnExit() // PopPanel() 中调用 OnExit
    {
        HideAnim();
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        uiMng.PopPanel();
    }

    private void OnLoginClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空 ";
        }
        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += "密码不能为空";
        }

        if (msg != "")
        {
            uiMng.ShowMessage(msg);
            return;
        }
        _loginRequest.SendRequest(usernameIF.text, passwordIF.text);
    }

    public void OnLoginResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {
            uiMng.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiMng.ShowMessageSync("用户名或密码错误，请重新输入"); // 因为这里接收服务端响应另外开的一个线程，所以需要异步访问 text 
                                                                     // text 只能在 unity 的主线程中进行访问
        }
    }

    private void OnRegisterClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Register);
    }

    private void EnterAnim()
    {
        // 进入动画
        gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(0.5f, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.one, 0.2f);
    }

    private void HideAnim()
    {
        transform.DOScale(0, 0.3f);
        transform.DOLocalMoveX(1000, 0.3f).OnComplete(()=>{gameObject.SetActive(false);});
    }
}