using System;
using DG.Tweening;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    private Text text;
    private float showTime = 1;
    private string message = null;

    private void Update()
    {
        if (message != null)
        {
            ShowMessage(message);
            message = null;
        }
    }

    public override void OnEnter()
    {
        base.OnEnter();
        text = gameObject.GetComponentInChildren<Text>();
        text.enabled = false;
        uiMng.InjectMsgPanel(this);
    }

    public void ShowMessageSync(string msg)
    {
        message = msg;
    }

    public void ShowMessage(string msg)
    {
        text.CrossFadeAlpha(1, 0.2f, false); // text 只能在 unity 的主线程中进行访问
        text.text = msg;
        text.enabled = true;    
        Invoke("Hide", showTime);
    }

    private void Hide()
    {
        //text.DOFade(0 , showTime);
        text.CrossFadeAlpha(0, showTime, false);
    }
}
