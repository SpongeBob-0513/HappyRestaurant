using System;
using Common;
using Net;
using UnityEngine;

namespace Request
{
    public class BaseRequest : MonoBehaviour
    {
        private RequestCode _requestCode = RequestCode.None;

        public virtual void Awake()
        {
            GameFacade.Instance.AddRequest(_requestCode, this);
        }
        public virtual void SendRequest(){}

        public virtual void OnResponse(string data){}

        public virtual void OnDestroy()
        {
            GameFacade.Instance.RemoveRequest(_requestCode);
        }
    }
}