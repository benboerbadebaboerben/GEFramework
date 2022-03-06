using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 初始化界面组的新实例。
    /// </summary>
    /// <param name="name">界面组名称。</param>
    /// <param name="depth">界面组深度。</param>
    public class UIGroup : Entity,IAwake<string,int,GameObject>
    {
        public GameObject obj;
        public Canvas m_CachedCanvas;
        public readonly int DepthFactor = 10000;
        public string m_Name;
        public int m_Depth;
        public bool m_Pause;
        public GameFrameworkLinkedList<UIForm> m_UIFormInfos = new GameFrameworkLinkedList<UIForm>();
        public LinkedListNode<UIForm> m_CachedNode;
    }
}
