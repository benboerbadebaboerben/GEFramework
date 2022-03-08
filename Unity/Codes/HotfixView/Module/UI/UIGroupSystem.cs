using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    [ObjectSystem]
    public class UIGroupAwakeSystem : AwakeSystem<UIGroup,string,int,GameObject>
    {
        public override void Awake(UIGroup self, string name, int depth,GameObject obj)
        {
            if (string.IsNullOrEmpty(name))
            {
                Log.Error("UI group name is invalid.");
            }
            self.obj = obj;
            self.m_CachedCanvas = self.obj.GetOrAddComponent<Canvas>();
            self.obj.GetOrAddComponent<GraphicRaycaster>();
            self.m_Name = name;
            self.m_Pause = false;
            self.m_Depth = depth;
            self.m_CachedNode = null;

            self.m_UIFormInfos.Clear();
        }

    }
    public static class UIGroupSystem
    {
        //不用UpdateSystem的原因：
        //ET的做法是把所有关于Update塞到一个Action里面
        //这样可能导致UI界面的Update调用顺序与其他模块混合
        /// <summary>
        /// 界面管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public static void Update(this UIGroup self,float elapseSeconds, float realElapseSeconds)
        {
            LinkedListNode<UIForm> current = self.m_UIFormInfos.First;
            while (current != null)
            {
                if (current.Value.m_Paused)
                {
                    break;
                }

                self.m_CachedNode = current.Next;
                current = self.m_CachedNode;
                self.m_CachedNode = null;
            }
        }

        /// <summary>
        /// 获取界面组名称。
        /// </summary>
        public static string GetGroupName(UIGroup self)
        {
            return self.m_Name;
        }

        /// <summary>
        /// 获取界面组深度。
        /// </summary>
        public static int GetGroupDepth(UIGroup self)
        {
            return self.m_Depth;
        }
        
        /// <summary>
        /// 设置界面组深度。
        /// </summary>
        public static void SetGroupDepth(UIGroup self,int depth)
        {
            if (self.m_Depth == depth)
            {
                return;
            }

            self.m_Depth = depth;
            self.m_CachedCanvas.overrideSorting = true;
            self.m_CachedCanvas.sortingOrder = self.DepthFactor * depth;
            self.Refresh();
        }

        /// <summary>
        /// 获取界面组是否暂停。
        /// </summary>
        public static bool GetGroupPause(this UIGroup self)
        {
            return self.m_Pause;
        }

        /// <summary>
        /// 设置界面组是否暂停。
        /// </summary>
        public static void GetGroupPause(this UIGroup self,bool pause)
        {
            if (self.m_Pause == pause)
            {
                return;
            }

            self.m_Pause = pause;
            self.Refresh();
        }

        /// <summary>
        /// 获取界面组中界面数量。
        /// </summary>
        public static int GetUIFormCount(UIGroup self)
        {
             return self.m_UIFormInfos.Count;
        }

        /// <summary>
        /// 获取当前界面。
        /// </summary>
        public static UIForm CurrentUIForm(UIGroup self)
        {
            return self.m_UIFormInfos.First != null ? self.m_UIFormInfos.First.Value : null;
        }

        /// <summary>
        /// 界面组中是否存在界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>界面组中是否存在界面。</returns>
        public static bool HasUIForm(this UIGroup self,int serialId)
        {
            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                if (uiFormInfo.m_SerialId == serialId)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 界面组中是否存在界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>界面组中是否存在界面。</returns>
        public static bool HasUIForm(this UIGroup self,string uiFormAssetName)
        {
            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                throw new GameFrameworkException("UI form asset name is invalid.");
            }

            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                if (uiFormInfo.m_UIFormAssetName == uiFormAssetName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 从界面组中获取界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>要获取的界面。</returns>
        public static UIForm GetUIForm(this UIGroup self,int serialId)
        {
            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                if (uiFormInfo.m_SerialId == serialId)
                {
                    return uiFormInfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 从界面组中获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public static UIForm GetUIForm(this UIGroup self,string uiFormAssetName)
        {
            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                throw new GameFrameworkException("UI form asset name is invalid.");
            }

            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                if (uiFormInfo.m_UIFormAssetName == uiFormAssetName)
                {
                    return uiFormInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// 从界面组中获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public static UIForm[] GetUIForms(this UIGroup self,string uiFormAssetName)
        {
            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                throw new GameFrameworkException("UI form asset name is invalid.");
            }

            List<UIForm> results = new List<UIForm>();
            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                if (uiFormInfo.m_UIFormAssetName == uiFormAssetName)
                {
                    results.Add(uiFormInfo);
                }
            }

            return results.ToArray();
        }

        /// <summary>
        /// 从界面组中获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="results">要获取的界面。</param>
        public static void GetUIForms(this UIGroup self, string uiFormAssetName, List<UIForm> results)
        {
            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                throw new GameFrameworkException("UI form asset name is invalid.");
            }

            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                if (uiFormInfo.m_UIFormAssetName == uiFormAssetName)
                {
                    results.Add(uiFormInfo);
                }
            }
        }

        /// <summary>
        /// 从界面组中获取所有界面。
        /// </summary>
        /// <returns>界面组中的所有界面。</returns>
        public static UIForm[] GetAllUIForms(this UIGroup self)
        {
            List<UIForm> results = new List<UIForm>();
            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                results.Add(uiFormInfo);
            }

            return results.ToArray();
        }

        /// <summary>
        /// 从界面组中获取所有界面。
        /// </summary>
        /// <param name="results">界面组中的所有界面。</param>
        public static void GetAllUIForms(this UIGroup self, List<UIForm> results)
        {
            if (results == null)
            {
                throw new GameFrameworkException("Results is invalid.");
            }

            results.Clear();
            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                results.Add(uiFormInfo);
            }
        }

        /// <summary>
        /// 往界面组增加界面。
        /// </summary>
        /// <param name="uiForm">要增加的界面。</param>
        public static void AddUIForm(this UIGroup self, UIForm uiForm)
        {
            uiForm.m_Paused = true;
            uiForm.m_Covered = true;
            self.m_UIFormInfos.AddFirst(uiForm);
        }

        /// <summary>
        /// 从界面组移除界面。
        /// </summary>
        /// <param name="uiForm">要移除的界面。</param>
        public static void RemoveUIForm(this UIGroup self, UIForm uiForm)
        {
            UIForm uiFormInfo = self.GetUIFormInfo(uiForm);
            if (uiFormInfo == null)
            {
                throw new GameFrameworkException(Utility.Text.Format("Can not find UI form info for serial id '{0}', UI form asset name is '{1}'.", uiForm.m_SerialId, uiForm.m_UIFormAssetName));
            }

            if (!uiFormInfo.m_Covered)
            {
                uiFormInfo.m_Covered = true;
                UIEventComponent.Instance.GetUIEventHandler(uiForm.uiType).OnCover(uiForm);
            }

            if (!uiFormInfo.m_Paused)
            {
                uiFormInfo.m_Paused = true;
                UIEventComponent.Instance.GetUIEventHandler(uiForm.uiType).OnPause(uiForm);
            }

            if (self.m_CachedNode != null && self.m_CachedNode.Value == uiForm)
            {
                self.m_CachedNode = self.m_CachedNode.Next;
            }

            if (!self.m_UIFormInfos.Remove(uiFormInfo))
            {
                throw new GameFrameworkException(Utility.Text.Format("UI group '{0}' not exists specified UI form '[{1}]{2}'.", self.m_Name, uiForm.m_SerialId, uiForm.m_UIFormAssetName));
            }
            uiFormInfo.Dispose();
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void RefocusUIForm(this UIGroup self, UIForm uiForm, object userData)
        {
            UIForm uiFormInfo = self.GetUIFormInfo(uiForm);
            if (uiFormInfo == null)
            {
                throw new GameFrameworkException("Can not find UI form info.");
            }

            self.m_UIFormInfos.Remove(uiFormInfo);
            self.m_UIFormInfos.AddFirst(uiFormInfo);
        }

        /// <summary>
        /// 刷新界面组。
        /// </summary>
        public static void Refresh(this UIGroup self)
        {
            LinkedListNode<UIForm> current = self.m_UIFormInfos.First;
            bool pause = self.m_Pause;
            bool cover = false;
            int depth = self.m_Depth;
            while (current != null && current.Value != null)
            {
                LinkedListNode<UIForm> next = current.Next;
                UIEventComponent.Instance.GetUIEventHandler(current.Value.uiType).OnDepthChanged(current.Value, self.m_Depth, depth--);
                if (current.Value == null)
                {
                    return;
                }

                if (pause)
                {
                    if (!current.Value.m_Covered)
                    {
                        current.Value.m_Covered = true;
                        UIEventComponent.Instance.GetUIEventHandler(current.Value.uiType).OnCover(current.Value);
                        if (current.Value == null)
                        {
                            return;
                        }
                    }

                    if (!current.Value.m_Paused)
                    {
                        current.Value.m_Paused = true;
                        UIEventComponent.Instance.GetUIEventHandler(current.Value.uiType).OnPause(current.Value);
                        if (current.Value == null)
                        {
                            return;
                        }
                    }
                }
                else
                {
                    if (current.Value.m_Paused)
                    {
                        current.Value.m_Paused = false;
                        UIEventComponent.Instance.GetUIEventHandler(current.Value.uiType).OnResume(current.Value);
                        if (current.Value == null)
                        {
                            return;
                        }
                    }

                    if (current.Value.m_PauseCoveredUIForm)
                    {
                        pause = true;
                    }

                    if (cover)
                    {
                        if (!current.Value.m_Covered)
                        {
                            current.Value.m_Covered = true;
                            UIEventComponent.Instance.GetUIEventHandler(current.Value.uiType).OnCover(current.Value);
                            if (current.Value == null)
                            {
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (current.Value.m_Covered)
                        {
                            current.Value.m_Covered = false;
                            UIEventComponent.Instance.GetUIEventHandler(current.Value.uiType).OnReveal(current.Value);
                            if (current.Value == null)
                            {
                                return;
                            }
                        }

                        cover = true;
                    }
                }

                current = next;
            }
        }

        public static void InternalGetUIForms(this UIGroup self,string uiFormAssetName, List<UIForm> results)
        {
            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                if (uiFormInfo.m_UIFormAssetName == uiFormAssetName)
                {
                    results.Add(uiFormInfo);
                }
            }
        }

        public static void InternalGetAllUIForms(this UIGroup self, List<UIForm> results)
        {
            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                results.Add(uiFormInfo);
            }
        }

        public static UIForm GetUIFormInfo(this UIGroup self, UIForm uiForm)
        {
            if (uiForm == null)
            {
                throw new GameFrameworkException("UI form is invalid.");
            }

            foreach (UIForm uiFormInfo in self.m_UIFormInfos)
            {
                if (uiFormInfo == uiForm)
                {
                    return uiFormInfo;
                }
            }

            return null;
        }

    }
}
