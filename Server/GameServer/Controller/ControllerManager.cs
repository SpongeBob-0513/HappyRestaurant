﻿using System;
 using System.Collections.Generic;
 using System.Reflection;
 using Common;
 using GameServer.Servers;

 namespace GameServer.Controller
{
    /// <summary>
    /// 处理请求，根据传送的消息指定到对应的 controller
    /// </summary>
    public class ControllerManager
    {
        private Dictionary<RequestCode, BaseController> controllerDict = new Dictionary<RequestCode, BaseController>();
        private Server server; // server 因为是独一无二的，所以这里的单独创建一个方便访问
        
        // 构造方法
        public ControllerManager(Server server)
        {
            this.server = server;
            InitController();
        }

        void InitController()
        {
            DefaultController defaultController = new DefaultController();
            controllerDict.Add(defaultController.RequestCode, defaultController);
            controllerDict.Add(RequestCode.User, new UserController());
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            BaseController controller;
            bool isGet = controllerDict.TryGetValue(requestCode, out controller);
            
            // 如果是正式的服务器的话需要生成日志保存在本地文件，方便以后查看，这里只是自己做的小游戏，先只是输出到控制台
            if (isGet == false)
            {
                Console.WriteLine("无法得到" + requestCode + "所对应的 Controller，无法处理请求"); return;
            }

            string methodName = Enum.GetName(typeof(ActionCode), actionCode); // 将枚举类型转换为字符串
            MethodInfo methodInfo = controller.GetType().GetMethod(methodName); // 映射查找对应 controller 里面的对应方法
            if (methodInfo == null)
            {
                Console.WriteLine("[警告]在 Controller[" + controller.GetType() + "] 中没有对应的处理方法：[" + methodName + "]"); return;
            }
            
            object[] parameters = new object[]{data, client, server}; 
            object o = methodInfo.Invoke(controller, parameters);
            if (o == null || string.IsNullOrEmpty(o as string)) // o 没有或者是空字符串
            {
                return;
            }
            
            server.SendRespoonse(client, actionCode, o as string);
        }
    }
}