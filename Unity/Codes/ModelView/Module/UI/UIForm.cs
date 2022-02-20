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
        public int m_SerialId;
        public string m_UIFormAssetName;
        public int m_DepthInUIGroup;
        /// <summary>
        /// 获取是否暂停被覆盖的界面。
        /// </summary>
        public bool m_PauseCoveredUIForm;
        public bool m_Pause;
        public bool m_Covered;
    }
}
