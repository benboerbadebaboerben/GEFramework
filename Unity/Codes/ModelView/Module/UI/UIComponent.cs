using ET.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
	/// <summary>
	/// 管理Scene上的UI
	/// </summary>
	public class UIComponent: Entity
	{
        public const int DefaultPriority = 0;

        public UIManager m_UIManager = new UIManager();

        public readonly List<IUIForm> m_InternalUIFormResults = new List<IUIForm>();

        [SerializeField]
        public bool m_EnableOpenUIFormSuccessEvent = true;

        [SerializeField]
        public bool m_EnableOpenUIFormFailureEvent = true;

        [SerializeField]
        public bool m_EnableOpenUIFormUpdateEvent = false;

        [SerializeField]
        public bool m_EnableOpenUIFormDependencyAssetEvent = false;

        [SerializeField]
        public bool m_EnableCloseUIFormCompleteEvent = true;

        [SerializeField]
        public float m_InstanceAutoReleaseInterval = 60f;

        [SerializeField]
        public int m_InstanceCapacity = 16;

        [SerializeField]
        public float m_InstanceExpireTime = 60f;

        [SerializeField]
        public int m_InstancePriority = 0;

        [SerializeField]
        private Transform m_InstanceRoot = null;

        [SerializeField]
        private string m_UIFormHelperTypeName = "UnityGameFramework.Runtime.DefaultUIFormHelper";

        [SerializeField]
        private UIFormHelperBase m_CustomUIFormHelper = null;

        [SerializeField]
        private string m_UIGroupHelperTypeName = "UnityGameFramework.Runtime.DefaultUIGroupHelper";

        [SerializeField]
        private UIGroupHelperBase m_CustomUIGroupHelper = null;

        [SerializeField]
        private UIGroup[] m_UIGroups = null;

        /// <summary>
        /// 获取界面组数量。
        /// </summary>
        public int UIGroupCount
        {
            get
            {
                return m_UIManager.UIGroupCount;
            }
        }
    }
}