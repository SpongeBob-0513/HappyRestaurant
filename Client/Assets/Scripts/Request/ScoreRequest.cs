using Common;
using Manager;

namespace Request
{
    public class ScoreRequest : BaseRequest
    {
        public override void Awake()
        {
            _requestCode = RequestCode.Game;
            _actionCode = ActionCode.Score;
            base.Awake();
        }

        public void SendRequest(int score)
        {
            base.SendRequest(score.ToString());
        }
    }
}