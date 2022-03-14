using DG.Tweening;
using Request;
using UIFramework.Manager;
using UIPanel;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : BasePanel
{
    private Button closeButton;

    public override void OnEnter()
    {
        base.OnEnter();
        
        gameObject.SetActive(true);

        // 进入动画
        transform.localScale = Vector3.zero;
        transform.DOScale(0.5f, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.one, 0.2f);

        closeButton = transform.Find("CloseButton").GetComponent<Button>();
        closeButton.onClick.AddListener(OnCloseClick);
    }

    private void OnCloseClick()
    {
        transform.DOScale(0, 0.4f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f);
        tweener.OnComplete(() => { uiMng.PopPanel(); });
    }

    public override void OnExit() // PopPanel() 中调用 OnExit
    {
        base.OnExit();
        gameObject.SetActive(false);
    }
}