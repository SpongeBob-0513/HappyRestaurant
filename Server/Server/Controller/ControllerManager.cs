using System.Collections.Generic;
using Common;

namespace Server.Controller
{
    /// <summary>
    /// 处理请求，根据传送的消息指定到对应的 controller
    /// </summary>
    public class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        
        // 构造方法
        public ControllerManager()
        {
            Init();
        }

        void Init()
        {
            // TODO
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode)
        {
            
        }
    }
}