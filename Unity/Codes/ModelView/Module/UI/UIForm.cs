using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    public class UIForm : Entity,IAwake,IUpdate
    {
        public GameObject obj;
        public int m_SerialId;
        public string m_UIFormAssetName;
        public UIGroup m_UIGroup;
        public int m_DepthInUIGroup;
        public bool m_PauseCoveredUIForm;
    }
}
