using ET.ObjectPool;
using System.Collections.Generic;

namespace ET
{
	/// <summary>
	/// Unity相关组件的对象池
	/// </summary>
	public class ObjectPoolComponent : Entity
	{
		public ObjectPoolManager m_ObjectPoolManager = null;

        /// <summary>
        /// 获取对象池数量。
        /// </summary>
        public int Count
        {
            get
            {
                return m_ObjectPoolManager.Count;
            }
        }
    }
}