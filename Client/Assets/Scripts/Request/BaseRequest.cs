using Common;
using UnityEngine;

namespace Request
{
    public class BaseRequest : MonoBehaviour
    {
        protected RequestCode _requestCode = RequestCode.None;
        protected ActionCode _actionCode = ActionCode.None;
        protected GameFacade _facade;

        public virtual void Awake()
        {
            GameFacade.Instance.AddRequest(_actionCode, this);
            _facade = GameFacade.Instance;
        }

        protected void SendRequest(string data)
        {
            _facade.SendRequest(_requestCode, _actionCode, data);
        }
        public virtual void SendRequest(){}

        public virtual void OnResponse(string data){}

        public virtual void OnDestroy()
        {
            GameFacade.Instance.RemoveRequest(_actionCode);
        }
    }
}