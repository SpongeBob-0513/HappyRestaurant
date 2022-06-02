using System;
using Common;
using UIPanel;

namespace Request
{
    public class QuitPlayingRequest : BaseRequest
    {
        private bool isQuitPlaying = false;
        private GamePanel gamePanel;
        
        public override void Awake()
        {
            _requestCode = RequestCode.Game;
            _actionCode = ActionCode.QuitPlaying;
            gamePanel = GetComponent<GamePanel>();
            base.Awake();
        }

        private void Update()
        {
            if (isQuitPlaying)
            {
                gamePanel.OnExitPlayingResponse();
                isQuitPlaying = false;
            }
        }

        public override void SendRequest()
        {
            base.SendRequest("r");
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            isQuitPlaying = true;
        }
    }
}