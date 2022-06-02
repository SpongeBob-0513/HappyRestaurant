using System;
using Common;
using UIPanel;

namespace Request
{
    public class GameOverRequest : BaseRequest
    {
        private GamePanel gamePanel;
        private bool isGameOver = false;
        private int totalScore = 0;
        
        public override void Awake()
        {
            _requestCode = RequestCode.Game;
            _actionCode = ActionCode.GameOver;
            gamePanel = GetComponent<GamePanel>();
            base.Awake();
        }

        private void Update()
        {
            if (isGameOver)
            {
                gamePanel.OnGameOverResponse(totalScore);
                isGameOver = false;
            }
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            totalScore = int.Parse(data);
            isGameOver = true;
        }
    }
}