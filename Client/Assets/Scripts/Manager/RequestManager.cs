using System.Collections.Generic;
using Common;
using Extension;
using Request;
using UnityEngine;

namespace Manager
{
    public class RequestManager:BaseManager
    {
        public RequestManager(GameFacade gameFacade) : base(gameFacade)
        {
        }
        
        private Dictionary<RequestCode, BaseRequest> requestDict = new Dictionary<RequestCode, BaseRequest>();

        public void AddRequest(RequestCode requestCode, BaseRequest baseRequest)
        {
            requestDict.Add(requestCode, baseRequest);
        }

        public void RemoveRequest(RequestCode requestCode)
        {
            requestDict.Remove(requestCode);
        }
        
        public void HandleResponse(RequestCode requestCode, string data)
        {
            BaseRequest request =  requestDict.TryGet(requestCode);
            if (request == null)
            {
                Debug.LogWarning("无法得到 requestCode[" + requestCode + "] 对应的 Request 类");
                return;
            }
            request.OnResponse(data);
        }
    }
}