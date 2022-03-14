using DG.Tweening;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : BasePanel
{
    private Text text;
    private float showTime = 1;
    
    public override void OnEnter()
    {
        base.OnEnter();
        text = gameObject.GetComponentInChildren<Text>();
        text.enabled = false;
        uiMng.InjectMsgPanel(this);
    }

    public void ShowMessage(string msg)
    {
        text.DOFade(1, 0);
        //text.color = Color.white; // 将透明度重新置为 1.
        text.text = msg;
        text.enabled = true;    
        Invoke("Hide", showTime);
    }

    private void Hide()
    {
        text.DOFade(0 , showTime);
        // text.CrossFadeAlpha(0, showTime, false);
    }
}
