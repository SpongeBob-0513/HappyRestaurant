using System;
using System.Runtime.Serialization;
using UnityEngine;

namespace UIFramework.UIPanel
{
    [Serializable]
    public class UIPanelInfo:ISerializationCallbackReceiver
    {
        [NonSerialized] public UIPanelType panelType;

        public string panelTypeString;
        public string path;
        
        // 反序列化  从文本信息到对象
        public void OnAfterDeserialize()
        {
            UIPanelType type = (UIPanelType) System.Enum.Parse(typeof(UIPanelType), panelTypeString);
            panelType = type;
        }

        public void OnBeforeSerialize()
        {
            
        }
    }
}