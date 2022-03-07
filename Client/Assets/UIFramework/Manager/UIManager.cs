using System;
using System.Collections.Generic;
using System.Linq;
using Extension;
using Manager;
using Net;
using UIFramework.UIPanel;
using UIPanel;
using UnityEngine;

namespace UIFramework.Manager
{
    public class UIManager:BaseManager
    {
        public UIManager(GameFacade gameFacade) : base(gameFacade)
        {
            ParseUIPanelTypeJson();
        }

        public override void OnInit()
        {
            base.OnInit();
            PushPanel(UIPanelType.Message);
            PushPanel(UIPanelType.Start);
        }

        private Transform canvasTransform;

        public Transform CanvasTransform
        {
            get
            {
                if (canvasTransform == null)
                {
                    canvasTransform = GameObject.Find("Canvas").transform;
                }

                return canvasTransform;
            }
        }

        private Dictionary<UIPanelType, string> panelPathDict;  // 存储所有面板 prefab 的路径
        private Dictionary<UIPanelType, BasePanel> panelDict;  // 保存所有实例化面板的游戏物体上的 BasePanel 组件
        private Stack<BasePanel> panelStack;
        private MessagePanel msgPanel;
        
        
        /// <summary>
        /// 把某个页面入栈，把某个页面显示到界面上
        /// </summary>
        /// <param name="panelType"></param>
        public void PushPanel(UIPanelType panelType)
        {
            if (panelStack.Count==null)
            {
                panelStack = new Stack<BasePanel>();
            }
            
            // 判断栈里面是否有页面
            if (panelStack.Count > 0)
            {
                BasePanel topPanel = panelStack.Peek();
                topPanel.OnPause();
            }

            BasePanel panel = GetPanel(panelType);
            panel.OnEnter();
            panelStack.Push(panel);
        }
        
        /// <summary>
        /// 出栈，把页面从界面上移除
        /// </summary>
        public void PopPanel()
        {
            if (panelStack == null)
            {
                panelStack = new Stack<BasePanel>();
            }

            if (panelStack.Count <= 0) return;
            
            // 关闭栈顶页面的显示
            BasePanel topPanel = panelStack.Pop();
            topPanel.OnExit();
            
            if(panelStack.Count <=0) return;
            BasePanel topPanel2 = panelStack.Peek();
            topPanel2.OnResume();
        }
        /// <summary>
        /// 根据面板类型 得到实例化的面板
        /// </summary>
        /// <param name="panelType"></param>
        /// <returns></returns>
        private BasePanel GetPanel(UIPanelType panelType)
        {
            if (panelDict == null)
            {
                panelDict = new Dictionary<UIPanelType, BasePanel>();
            }

            BasePanel panel = panelDict.TryGet(panelType);

            if (panel == null)
            {
                //如果找不到，那么就找这个面板的prefab的路径，然后去根据prefab去实例化面板
                string path = panelPathDict.TryGet(panelType);
                GameObject instPanel = GameObject.Instantiate(Resources.Load(path)) as GameObject;
                instPanel.transform.SetParent(CanvasTransform, false);
                instPanel.GetComponent<BasePanel>().UIMng = this;
                panelDict.Add(panelType, instPanel.GetComponent<BasePanel>());
                return instPanel.GetComponent<BasePanel>();
            }
            else
            {
                return panel;
            }
        }
        
        [Serializable]
        class UIPanelTypeJson
        {
            public List<UIPanelInfo> infoList;
        }

        private void ParseUIPanelTypeJson()
        {
            panelDict = new Dictionary<UIPanelType, BasePanel>();

            TextAsset ta = Resources.Load<TextAsset>("UIPanelType");
            
            UIPanelTypeJson jsonObject = JsonUtility.FromJson<UIPanelTypeJson>(ta.text);
            foreach (UIPanelInfo info in jsonObject.infoList)
            {
                panelPathDict.Add(info.panelType, info.path);
            }
        }

        public void InjectMsgPanel(MessagePanel msgPanel)
        {
            this.msgPanel = msgPanel;
        }
        
        public void ShowMessage(string msg)
        {
            if (msgPanel == null)
            {
                Debug.Log("无法显示提示信息，MsgPanel 为空"); return;
            }
            msgPanel.ShowMessage(msg);
        }
    }
}