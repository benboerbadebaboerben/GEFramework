using System;
using System.Collections.Generic;

namespace ET
{
	/// <summary>
	/// 管理Scene上的UI
	/// </summary>
	public class UIComponent: Entity,IAwake,IUpdate
	{
        public Dictionary<string, UIGroup> m_UIGroups = new Dictionary<string, UIGroup>(StringComparer.Ordinal);
        public Dictionary<int, string> m_UIFormsBeingLoaded = new Dictionary<int, string>();
        public HashSet<int> m_UIFormsToReleaseOnLoad = new HashSet<int>();
        public Queue<UIForm> m_RecycleQueue = new Queue<UIForm>();
        public int m_Serial = 0;
        public bool m_IsShutdown = false;
    }
}