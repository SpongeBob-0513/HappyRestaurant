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
        
        private Dictionary<ActionCode, BaseRequest> requestDict = new Dictionary<ActionCode, BaseRequest>();

        public void AddRequest(ActionCode actionCode, BaseRequest baseRequest)
        {
            requestDict.Add(actionCode, baseRequest);
        }

        public void RemoveRequest(ActionCode actionCode)
        {
            requestDict.Remove(actionCode);
        }
        
        public void HandleResponse(ActionCode actionCode, string data)
        {
            BaseRequest request =  requestDict.TryGet(actionCode);
            if (request == null)
            {
                Debug.LogWarning("无法得到 actionCode[" + actionCode + "] 对应的 Request 类");
                return;
            }
            request.OnResponse(data);
        }
    }
}