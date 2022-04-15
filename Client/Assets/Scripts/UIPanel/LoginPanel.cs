using Common;
using DG.Tweening;
using Request;
using UIFramework.Manager;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private Button closeButton;
    private InputField usernameIF;
    private InputField passwordIF;
    private LoginRequest _loginRequest;

    public override void OnEnter()
    {
        base.OnEnter();
        
        gameObject.SetActive(true);

        // 进入动画
        transform.localScale = Vector3.zero;
        transform.DOScale(0.5f, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.one, 0.2f);

        _loginRequest = GetComponent<LoginRequest>();
        usernameIF = transform.Find("UsernameLable/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLable/PasswordInput").GetComponent<InputField>();
        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
        transform.Find("LoginButton").GetComponent<Button>().onClick.AddListener(OnLoginClick);
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
    }

    private void OnCloseClick()
    {
        transform.DOScale(0, 0.4f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f);
        tweener.OnComplete(() => { uiMng.PopPanel(); });
    }

    private void OnLoginClick()
    {
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
            
        }
        else
        {
            uiMng.ShowMessage("用户名或密码错误，请重新输入");
        }
    }

    private void OnRegisterClick()
    {
        
    }

    public override void OnExit() // PopPanel() 中调用 OnExit
    {
        base.OnExit();
        gameObject.SetActive(false);
    }
}