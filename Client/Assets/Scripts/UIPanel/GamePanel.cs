using System;
using DG.Tweening;
using Manager;
using Request;
using UnityEngine;
using UnityEngine.UI;

namespace UIPanel
{
    public class GamePanel : BasePanel
    {
        private Text timer;
        private int time = -1;
        private Button exitButton;
        private Button exitPlayingButton;
        private Text totalScoreText;

        private QuitPlayingRequest quitPlayingRequest;

        private void Start()
        {
            timer = transform.Find("Timer").GetComponent<Text>();
            timer.gameObject.SetActive(false);
            exitButton = transform.Find("ExitButton").GetComponent<Button>();
            exitButton.onClick.AddListener(OnResultClick);
            exitButton.gameObject.SetActive(false);
            totalScoreText = transform.Find("ExitButton/TotalScore").GetComponent<Text>();
            exitPlayingButton = transform.Find("ExitPlayingButton").GetComponent<Button>();
            exitPlayingButton.onClick.AddListener(OnExitPlayingClick);
            exitPlayingButton.gameObject.SetActive(false);

            quitPlayingRequest = GetComponent<QuitPlayingRequest>();
        }

        private void Update()
        {
            if (time > -1)
            {
                ShowTime(time);
                time = -1;
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
            gameObject.SetActive(true);
        }

        public override void OnExit()
        {
            base.OnExit();
            exitButton.gameObject.SetActive(false);
            exitPlayingButton.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }

        private void OnExitPlayingClick()
        {
            quitPlayingRequest.SendRequest();
        }

        public void OnExitPlayingResponse()
        {
            OnResultClick();
        }

        private void OnResultClick()
        {
            uiMng.PopPanel();
            uiMng.PopPanel();
            facade.GameOver();
        }

        public void ShowTimeSync(int time)
        {
            this.time = time;
        }

        public void ShowTime(int time)
        {
            if (time == 3)
            {
                exitPlayingButton.gameObject.SetActive(true);
            }
            
            timer.gameObject.SetActive(true);
            
            timer.text = time.ToString();
            timer.transform.localScale = Vector3.one;
            Color tempColor = timer.color;
            tempColor.a = 1;
            timer.color = tempColor;
            
            timer.transform.DOScale(2, 0.3f).SetDelay(0.3f);
            timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => timer.gameObject.SetActive(false));
            facade.PlayNormalSound(AudioManager.SoundTimer);
        }

        public void OnGameOverResponse(int totalScore)
        {
            totalScoreText.text = "当前得分：" + totalScore;
            exitButton.gameObject.SetActive(true);
            exitButton.transform.localScale = Vector3.zero;
            exitButton.transform.DOScale(1, 0.5f);
        }
    }
}