using Common;
using DG.Tweening;
using Request;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class RegisterPanel : BasePanel
{
    private InputField usernameIF;
    private InputField passwordIF;
    private InputField rePasswordIF;
    private RegisterRequest registerRequest;

    private void Start()
    {
        registerRequest = GetComponent<RegisterRequest>();
        
        usernameIF = transform.Find("UsernameLable/UsernameInput").GetComponent<InputField>();
        passwordIF = transform.Find("PasswordLable/PasswordInput").GetComponent<InputField>();
        rePasswordIF = transform.Find("RePasswordLable/RePasswordInput").GetComponent<InputField>();
        
        transform.Find("RegisterButton").GetComponent<Button>().onClick.AddListener(OnRegisterClick);
        transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(OnCloseClick);
    }

    public override void OnEnter()
    {
        base.OnEnter();
        
        gameObject.SetActive(true);
        
        transform.localScale = Vector3.zero;
        transform.DOScale(0.5f, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.one, 0.2f);
    }

    public void OnRegisterResponse(ReturnCode returnCode)
    {
        if (returnCode == ReturnCode.Success)
        {

            uiMng.ShowMessageSync("注册成功");
        }
        else
        {
            uiMng.ShowMessageSync("用户名重复");
        }
    }

    private void OnRegisterClick()
    {
        PlayClickSound();
        string msg = "";
        if (string.IsNullOrEmpty(usernameIF.text))
        {
            msg += "用户名不能为空";
        }

        if (string.IsNullOrEmpty(passwordIF.text))
        {
            msg += " 密码不能为空";
        }

        if (passwordIF.text != rePasswordIF.text)
        {
            msg += " 密码不一致";
        }

        if (msg != "")
        {
            uiMng.ShowMessage(msg);
            return;
        }
        
        // 进行注册，发送到服务端
        registerRequest.SendRequest(usernameIF.text, passwordIF.text);
    }

    private void OnCloseClick()
    {
        PlayClickSound();
        transform.DOScale(0, 0.4f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f);
        tweener.OnComplete(() => { uiMng.PopPanel(); });
    }

    public override void OnExit()
    {
        base.OnExit();
        gameObject.SetActive(false);
    }
}
