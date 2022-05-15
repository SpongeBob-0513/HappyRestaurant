using Common;
using UIPanel;

namespace Request
{
    public class ShowTimerRequest : BaseRequest
    {
        private GamePanel gamePanel;
        
        public override void Awake()
        {
            _actionCode = ActionCode.ShowTimer;
            gamePanel = GetComponent<GamePanel>();
            base.Awake();
        }

        public override void OnResponse(string data)
        {
            base.OnResponse(data);
            int time = int.Parse(data);
            gamePanel.ShowTimeSync(time);
        }
    }
}
