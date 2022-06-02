using System;
using Common;

namespace Request
{
    public class StartPlayRequest : BaseRequest
    {
        private bool isStartPlaying = false;
        
        public override void Awake()
        {
            _actionCode = ActionCode.StartPlay;
            base.Awake();
        }

        private void Update()
        {
            if (isStartPlaying)
            {
                facade.StartPlaying();
                isStartPlaying = false;
            }
        }

        public override void OnResponse(string data)
        {
            isStartPlaying = true;
        }
    }
}