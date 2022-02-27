using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class UIFormAwakeSystem : AwakeSystem<UIForm>
    {
        public override void Awake(UIForm self)
        {
            
        }
    }

    public static class UIFormSystem
    {
        /// <summary>
        /// 获取界面实例。
        /// </summary>
        public static GameObject Handle(this UIForm self)
        {
            return self.obj;
        }

        /// <summary>
        /// 初始化界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroup">界面所处的界面组。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="isNewInstance">是否是新实例。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void OnInit(this UIForm self,int serialId, string uiFormAssetName, bool pauseCoveredUIForm, bool isNewInstance, object userData)
        {
            self.m_SerialId = serialId;
            self.m_UIFormAssetName = uiFormAssetName;
            self.m_DepthInUIGroup = 0;
            self.m_PauseCoveredUIForm = pauseCoveredUIForm;
            self.m_OriginalLayer = self.obj.layer;

            if (self.m_CachedTransform == null)
            {
                self.m_CachedTransform = self.obj.transform;
            }
            if (!isNewInstance)
            {
                return;
            }
        }

        /// <summary>
        /// 卸载指定的UI窗口实例
        /// </summary>
        /// <param name="isDispose">是否卸载脚本本身。</param>
        public static void OnRecycle(this UIForm self, bool isDispose = true)
        {
            try
            {
                if (null == self)
                {
                    Log.Error($"UIBaseWindow WindowId {self.m_UIFormAssetName} is null!!!");
                    return;
                }
                if (self.obj != null)
                {
                    Game.Scene.GetComponent<ResourcesComponent>()?.UnloadBundle(self.m_UIFormAssetName.StringToAB());
                    UnityEngine.Object.Destroy(self.obj);
                    self.obj = null;
                }
                self.m_SerialId = 0;
                self.m_DepthInUIGroup = 0;
                self.m_PauseCoveredUIForm = true;
                if (isDispose)
                {
                    self?.Dispose();
                }
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnRecycle with exception '{2}'.", self.m_SerialId, self.m_UIFormAssetName, exception);
            }

            
        }

        /// <summary>
        /// 界面打开。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public static void OnOpen(this UIForm self,object userData)
        {
            try
            {
                self.m_Available = true;
                self.m_Visible = true;
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnOpen with exception '{2}'.", self.m_SerialId, self.m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面关闭。
        /// </summary>
        /// <param name="isShutdown">是否是关闭界面管理器时触发。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void OnClose(this UIForm self, bool isShutdown, object userData)
        {
            try
            {
                self.obj.SetLayerRecursively(self.m_OriginalLayer);
                self.m_Visible = false;
                self.m_Available = false;
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnClose with exception '{2}'.", self.m_SerialId, self.m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面暂停。
        /// </summary>
        public static void OnPause(this UIForm self)
        {
            try
            {
                self.m_Visible = false;
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnPause with exception '{2}'.", self.m_SerialId, self.m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面暂停恢复。
        /// </summary>
        public static void OnResume(this UIForm self)
        {
            try
            {
                self.m_Visible = true;
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnResume with exception '{2}'.", self.m_SerialId, self.m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面遮挡。
        /// </summary>
        public static void OnCover(this UIForm self)
        {
            try
            {
                
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnCover with exception '{2}'.", self.m_SerialId, self.m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面遮挡恢复。
        /// </summary>
        public static void OnReveal(this UIForm self)
        {
            try
            {
                
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnReveal with exception '{2}'.", self.m_SerialId, self.m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面激活。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public static void OnRefocus(this UIForm self,object userData)
        {
            try
            {
                
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnRefocus with exception '{2}'.", self.m_SerialId, self.m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 界面深度改变。
        /// </summary>
        /// <param name="uiGroupDepth">界面组深度。</param>
        /// <param name="depthInUIGroup">界面在界面组中的深度。</param>
        public static void OnDepthChanged(this UIForm self,int uiGroupDepth, int depthInUIGroup)
        {
            self.m_DepthInUIGroup = depthInUIGroup;
            try
            {
                
            }
            catch (Exception exception)
            {
                Log.Error("UI form '[{0}]{1}' OnDepthChanged with exception '{2}'.", self.m_SerialId, self.m_UIFormAssetName, exception);
            }
        }

        /// <summary>
        /// 设置界面的可见性。
        /// </summary>
        /// <param name="visible">界面的可见性。</param>
        public static void InternalSetVisible(this UIForm self,bool visible)
        {
            self.obj.SetActive(visible);
        }
    }
}
