using DG.Tweening;
using Request;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class StartPanel : BasePanel
{
    private Button loginButton;
    private Animator btnAnimator;
    public override void OnEnter()
    {
        base.OnEnter();
        loginButton = transform.Find("LoginButton").GetComponent<Button>();
        btnAnimator = loginButton.GetComponent<Animator>();
        loginButton.onClick.AddListener(OnLoginClick);
    }

    private void OnLoginClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Login);
    }

    public override void OnPause() // 当这个面板上显示其他面板时会调用 OnPause
    {
        base.OnPause();
        loginButton.gameObject.SetActive(false);
    }

    public override void OnResume() // 当这个面板重新位于最顶端是会调用这个函数
    {
        base.OnResume();
        loginButton.gameObject.SetActive(true);
    }
}
