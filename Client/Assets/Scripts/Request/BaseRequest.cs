using Common;
using UnityEngine;

namespace Request
{
    public class BaseRequest : MonoBehaviour
    {
        protected RequestCode _requestCode = RequestCode.None;
        protected ActionCode _actionCode = ActionCode.None;

        private GameFacade _facade;
        protected GameFacade facade
        {
            get
            {
                if(_facade == null)
                    _facade = GameFacade.Instance;
                return _facade;
            }
        }

        public virtual void Awake()
        {
            facade.AddRequest(_actionCode, this);
        }

        protected void SendRequest(string data)
        {
            facade.SendRequest(_requestCode, _actionCode, data);
        }

        public virtual void SendRequest()
        {
        }

        public virtual void OnResponse(string data)
        {
        }

        public virtual void OnDestroy()
        {
            if (facade != null)
                facade.RemoveRequest(_actionCode);
        }
    }
}