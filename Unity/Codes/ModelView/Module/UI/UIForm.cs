using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class UIForm : Entity,IAwake, IUpdate
    {
        public GameObject obj;
        /// <summary>
        /// 获取界面序列编号。
        /// </summary>
        public int m_SerialId;
        /// <summary>
        /// 获取界面资源名称。
        /// </summary>
        public string m_UIFormAssetName;
        /// <summary>
        /// 获取界面深度。
        /// </summary>
        public int m_DepthInUIGroup;
        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        public bool m_PauseCoveredUIForm;
        public bool m_Pause;
        public bool m_Covered;
        public bool m_Available = false;
        public bool m_Visible = false;
        public Transform m_CachedTransform = null;
        public int m_OriginalLayer = 0;
    }
}
