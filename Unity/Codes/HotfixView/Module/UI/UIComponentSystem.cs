using System;
using System.Collections.Generic;
using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class UIComponentAwakeSystem : AwakeSystem<UIComponent>
    {
        public override void Awake(UIComponent self)
        {

        }
    }

    [ObjectSystem]
    public class UIComponentUpdateSystem : UpdateSystem<UIComponent>
    {
        public override void Update(UIComponent self)
        {
            while (self.m_RecycleQueue.Count > 0)
            {
                UIForm uiForm = self.m_RecycleQueue.Dequeue();
                uiForm.OnRecycle();
            }
            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                uiGroup.Value.Update(Time.deltaTime, Time.unscaledDeltaTime);
            }
        }
    }

    /// <summary>
    /// 管理Scene上的UI
    /// </summary>
    public static class UIComponentSystem
	{
        /// <summary>
        /// 同步加载
        /// </summary>
        private static void LoadBaseWindows(this UIComponent self, string UIName)
        {
            ResourcesComponent.Instance.LoadBundle(UIName.StringToAB());
            GameObject go = ResourcesComponent.Instance.GetAsset(UIName.StringToAB(), UIName) as GameObject;
            //baseWindow.UIPrefabGameObject = UnityEngine.Object.Instantiate(go);
            //baseWindow.UIPrefabGameObject.name = go.name;

            //UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitWindowCoreData(baseWindow);

            //baseWindow?.SetRoot(EUIRootHelper.GetTargetRoot(baseWindow.WindowData.windowType));
            //baseWindow.uiTransform.SetAsLastSibling();

            //UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitComponent(baseWindow);
            //UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);

            //self.AllWindowsDic[(int)baseWindow.WindowID] = baseWindow;
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        //private static async ETTask LoadBaseWindowsAsync(this UIComponent self, UIBaseWindow baseWindow)
        //{

        //    if (!UIPathComponent.Instance.WindowPrefabPath.TryGetValue((int)baseWindow.WindowID, out string value))
        //    {
        //        Log.Error($"{baseWindow.WindowID} is not Exist!");
        //        return;
        //    }
        //    self.LoadingWindows.Add(baseWindow.WindowID);
        //    await ResourcesComponent.Instance.LoadBundleAsync(value.StringToAB());
        //    GameObject go = ResourcesComponent.Instance.GetAsset(value.StringToAB(), value) as GameObject;
        //    baseWindow.UIPrefabGameObject = UnityEngine.Object.Instantiate(go);
        //    baseWindow.UIPrefabGameObject.name = go.name;

        //    UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitWindowCoreData(baseWindow);

        //    baseWindow?.SetRoot(EUIRootHelper.GetTargetRoot(baseWindow.WindowData.windowType));
        //    baseWindow.uiTransform.SetAsLastSibling();

        //    UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitComponent(baseWindow);
        //    UIEventComponent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);

        //    self.AllWindowsDic[(int)baseWindow.WindowID] = baseWindow;
        //    self.LoadingWindows.Remove(baseWindow.WindowID);
        //}

        /// <summary>
        /// 获取界面组数量。
        /// </summary>
        public static int UIGroupCount(this UIComponent self)
        {
            return self.m_UIGroups.Count;
        }

        /// <summary>
        /// 获取或设置界面实例对象池自动释放可释放对象的间隔秒数。
        /// </summary>

        /// <summary>
        /// 获取或设置界面实例对象池的容量。
        /// </summary>

        /// <summary>
        /// 获取或设置界面实例对象池对象过期秒数。
        /// </summary>

        /// <summary>
        /// 获取或设置界面实例对象池的优先级。
        /// </summary>

        /// <summary>
        /// 关闭并清理界面管理器。
        /// </summary>
        public static void Shutdown(this UIComponent self)
        {
            self.m_IsShutdown = true;
            self.CloseAllLoadedUIForms();
            self.m_UIGroups.Clear();
            self.m_UIFormsBeingLoaded.Clear();
            self.m_UIFormsToReleaseOnLoad.Clear();
            self.m_RecycleQueue.Clear();
        }

        /// <summary>
        /// 是否存在界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>是否存在界面组。</returns>
        public static bool HasUIGroup(this UIComponent self,string uiGroupName)
        {
            if (string.IsNullOrEmpty(uiGroupName))
            {
                Log.Error("UI group name is invalid.");
            }

            return self.m_UIGroups.ContainsKey(uiGroupName);
        }

        /// <summary>
        /// 获取界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>要获取的界面组。</returns>
        public static UIGroup GetUIGroup(this UIComponent self,string uiGroupName)
        {
            if (string.IsNullOrEmpty(uiGroupName))
            {
                Log.Error("UI group name is invalid.");
            }

            UIGroup uiGroup = null;
            if (self.m_UIGroups.TryGetValue(uiGroupName, out uiGroup))
            {
                return uiGroup;
            }

            return null;
        }

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <returns>所有界面组。</returns>
        public static UIGroup[] GetAllUIGroups(this UIComponent self)
        {
            int index = 0;
            UIGroup[] results = new UIGroup[self.m_UIGroups.Count];
            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                results[index++] = uiGroup.Value;
            }

            return results;
        }

        /// <summary>
        /// 获取所有界面组。
        /// </summary>
        /// <param name="results">所有界面组。</param>
        public static void GetAllUIGroups(this UIComponent self,List<UIGroup> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                results.Add(uiGroup.Value);
            }
        }

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="uiGroupHelper">界面组辅助器。</param>
        /// <returns>是否增加界面组成功。</returns>
        public static bool AddUIGroup(this UIComponent self, string uiGroupName)
        {
            return self.AddUIGroup(uiGroupName, 0);
        }

        /// <summary>
        /// 增加界面组。
        /// </summary>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="uiGroupDepth">界面组深度。</param>
        /// <param name="uiGroupHelper">界面组辅助器。</param>
        /// <returns>是否增加界面组成功。</returns>

        public static bool AddUIGroup(this UIComponent self, string uiGroupName, int uiGroupDepth)
        {
            if (string.IsNullOrEmpty(uiGroupName))
            {
                Log.Error("UI group name is invalid.");
            }

            if (self.HasUIGroup(uiGroupName))
            {
                return false;
            }
            UIGroup uiGroup = self.AddChild<UIGroup,string,int>(uiGroupName, uiGroupDepth);
            self.m_UIGroups.Add(uiGroupName, uiGroup);

            return true;
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>是否存在界面。</returns>
        public static bool HasUIForm(this UIComponent self, int serialId)
        {
            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                if (uiGroup.Value.HasUIForm(serialId))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 是否存在界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>是否存在界面。</returns>
        public static bool HasUIForm(this UIComponent self, string uiFormAssetName)
        {
            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                Log.Error("UI form asset name is invalid.");
            }

            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                if (uiGroup.Value.HasUIForm(uiFormAssetName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>要获取的界面。</returns>
        public static UIForm GetUIForm(this UIComponent self, int serialId)
        {
            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                UIForm uiForm = uiGroup.Value.GetUIForm(serialId);
                if (uiForm != null)
                {
                    return uiForm;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public static UIForm GetUIForm(this UIComponent self, string uiFormAssetName)
        {
            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                Log.Error("UI form asset name is invalid.");
            }

            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                UIForm uiForm = uiGroup.Value.GetUIForm(uiFormAssetName);
                if (uiForm != null)
                {
                    return uiForm;
                }
            }

            return null;
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>要获取的界面。</returns>
        public static UIForm[] GetUIForms(this UIComponent self, string uiFormAssetName)
        {
            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                Log.Error("UI form asset name is invalid.");
            }

            List<UIForm> results = new List<UIForm>();
            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                results.AddRange(uiGroup.Value.GetUIForms(uiFormAssetName));
            }

            return results.ToArray();
        }

        /// <summary>
        /// 获取界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="results">要获取的界面。</param>
        public static void GetUIForms(this UIComponent self, string uiFormAssetName, List<UIForm> results)
        {
            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                Log.Error("UI form asset name is invalid.");
            }

            if (results == null)
            {
                Log.Error("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                uiGroup.Value.InternalGetUIForms(uiFormAssetName, results);
            }
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <returns>所有已加载的界面。</returns>
        public static UIForm[] GetAllLoadedUIForms(this UIComponent self)
        {
            List<UIForm> results = new List<UIForm>();
            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                results.AddRange(uiGroup.Value.GetAllUIForms());
            }

            return results.ToArray();
        }

        /// <summary>
        /// 获取所有已加载的界面。
        /// </summary>
        /// <param name="results">所有已加载的界面。</param>
        public static void GetAllLoadedUIForms(this UIComponent self, List<UIForm> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<string, UIGroup> uiGroup in self.m_UIGroups)
            {
                uiGroup.Value.InternalGetAllUIForms(results);
            }
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <returns>所有正在加载界面的序列编号。</returns>
        public static int[] GetAllLoadingUIFormSerialIds(this UIComponent self)
        {
            int index = 0;
            int[] results = new int[self.m_UIFormsBeingLoaded.Count];
            foreach (KeyValuePair<int, string> uiFormBeingLoaded in self.m_UIFormsBeingLoaded)
            {
                results[index++] = uiFormBeingLoaded.Key;
            }

            return results;
        }

        /// <summary>
        /// 获取所有正在加载界面的序列编号。
        /// </summary>
        /// <param name="results">所有正在加载界面的序列编号。</param>
        public static void GetAllLoadingUIFormSerialIds(this UIComponent self, List<int> results)
        {
            if (results == null)
            {
                Log.Error("Results is invalid.");
            }

            results.Clear();
            foreach (KeyValuePair<int, string> uiFormBeingLoaded in self.m_UIFormsBeingLoaded)
            {
                results.Add(uiFormBeingLoaded.Key);
            }
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <param name="serialId">界面序列编号。</param>
        /// <returns>是否正在加载界面。</returns>
        public static bool IsLoadingUIForm(this UIComponent self, int serialId)
        {
            return self.m_UIFormsBeingLoaded.ContainsKey(serialId);
        }

        /// <summary>
        /// 是否正在加载界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <returns>是否正在加载界面。</returns>
        public static bool IsLoadingUIForm(this UIComponent self, string uiFormAssetName)
        {
            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                Log.Error("UI form asset name is invalid.");
            }

            return self.m_UIFormsBeingLoaded.ContainsValue(uiFormAssetName);
        }

        /// <summary>
        /// 是否是合法的界面。
        /// </summary>
        /// <param name="uiForm">界面。</param>
        /// <returns>界面是否合法。</returns>
        public static bool IsValidUIForm(this UIComponent self, UIForm uiForm)
        {
            if (uiForm == null)
            {
                return false;
            }

            return self.HasUIForm(uiForm.m_SerialId);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <returns>界面的序列编号。</returns>
        public static ETTask OpenUIForm(this UIComponent self, string uiFormAssetName, string uiGroupName)
        {
            //默认优先级为0
            return self.OpenUIForm(uiFormAssetName, uiGroupName, 0, false, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <returns>界面的序列编号。</returns>
        public static ETTask OpenUIForm(this UIComponent self, string uiFormAssetName, string uiGroupName, int priority)
        {
            return self.OpenUIForm(uiFormAssetName, uiGroupName, priority, false, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public static ETTask OpenUIForm(this UIComponent self, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm)
        {
            //默认优先级为0
            return self.OpenUIForm(uiFormAssetName, uiGroupName, 0, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public static ETTask OpenUIForm(this UIComponent self, string uiFormAssetName, string uiGroupName, object userData)
        {
            //默认优先级为0
            return self.OpenUIForm(uiFormAssetName, uiGroupName, 0, false, userData);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <returns>界面的序列编号。</returns>
        public static ETTask OpenUIForm(this UIComponent self, string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm)
        {
            return self.OpenUIForm(uiFormAssetName, uiGroupName, priority, pauseCoveredUIForm, null);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public static ETTask OpenUIForm(this UIComponent self, string uiFormAssetName, string uiGroupName, int priority, object userData)
        {
            return self.OpenUIForm(uiFormAssetName, uiGroupName, priority, false, userData);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public static ETTask OpenUIForm(this UIComponent self, string uiFormAssetName, string uiGroupName, bool pauseCoveredUIForm, object userData)
        {
            //默认优先级为0
            return self.OpenUIForm(uiFormAssetName, uiGroupName, 0, pauseCoveredUIForm, userData);
        }

        /// <summary>
        /// 打开界面。
        /// </summary>
        /// <param name="uiFormAssetName">界面资源名称。</param>
        /// <param name="uiGroupName">界面组名称。</param>
        /// <param name="priority">加载界面资源的优先级。</param>
        /// <param name="pauseCoveredUIForm">是否暂停被覆盖的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        /// <returns>界面的序列编号。</returns>
        public static async ETTask OpenUIForm(this UIComponent self, string uiFormAssetName, string uiGroupName, int priority, bool pauseCoveredUIForm, object userData)
        {

            if (string.IsNullOrEmpty(uiFormAssetName))
            {
                Log.Error("UI form asset name is invalid.");
            }

            if (string.IsNullOrEmpty(uiGroupName))
            {
                Log.Error("UI group name is invalid.");
            }

            UIGroup uiGroup = (UIGroup)self.GetUIGroup(uiGroupName);
            if (uiGroup == null)
            {
                Log.Error(Utility.Text.Format("UI group '{0}' is not exist.Please First Create UIGroup", uiGroupName));
            }

            int serialId = ++self.m_Serial;
            await ETTask.CompletedTask;
            await self.Domain.GetComponent<ResourcesLoaderComponent>().LoadAsync(UIType.UILobby.StringToAB());
            GameObject bundleGameObject = (GameObject)ResourcesComponent.Instance.GetAsset(UIType.UILobby.StringToAB(), UIType.UILobby);
            //GameObject gameObject = UnityEngine.Object.Instantiate(bundleGameObject, UIEventComponent.Instance.UILayers[(int)uiLayer]);
            //if (gameObject == null)
            //{
            //    self.m_UIFormsBeingLoaded.Add(serialId, uiFormAssetName);
                
            //}
            //else
            //{
            //    UIForm uiFormInstanceObject = uiGroup.AddChild<UIForm>();
            //    //uiFormInstanceObject.obj = gameObject;
            //    uiFormInstanceObject.m_UIFormAssetName = uiFormAssetName;
            //    //uiFormInstanceObject.AddComponent<UICoponent>();
            //    //self.InternalOpenUIForm(serialId, uiFormAssetName, uiGroup, uiFormInstanceObject.Target, pauseCoveredUIForm, false, 0f, userData);
            //}
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        public static void CloseUIForm(this UIComponent self, int serialId)
        {
            self.CloseUIForm(serialId, null);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="serialId">要关闭界面的序列编号。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void CloseUIForm(this UIComponent self, int serialId, object userData)
        {
            if (self.IsLoadingUIForm(serialId))
            {
                self.m_UIFormsToReleaseOnLoad.Add(serialId);
                self.m_UIFormsBeingLoaded.Remove(serialId);
                return;
            }

            UIForm uiForm = self.GetUIForm(serialId);
            if (uiForm == null)
            {
                Log.Error(Utility.Text.Format("Can not find UI form '{0}'.", serialId));
            }

            self.CloseUIForm(uiForm, userData);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        public static void CloseUIForm(this UIComponent self, UIForm uiForm)
        {
            self.CloseUIForm(uiForm, null);
        }

        /// <summary>
        /// 关闭界面。
        /// </summary>
        /// <param name="uiForm">要关闭的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void CloseUIForm(this UIComponent self, UIForm uiForm, object userData)
        {
            if (uiForm == null)
            {
                Log.Error("UI form is invalid.");
            }

            UIGroup uiGroup = uiForm.GetParent<UIGroup>();
            if (uiGroup == null)
            {
                Log.Error("UI group is invalid.");
            }

            uiGroup.RemoveUIForm(uiForm);
            uiForm.OnClose(self.m_IsShutdown, userData);
            uiGroup.Refresh();

            Game.EventSystem.Publish(new UICommonEventType.CloseUIFormCompleteEventArgs());
            self.m_RecycleQueue.Enqueue(uiForm);
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        public static void CloseAllLoadedUIForms(this UIComponent self)
        {
            self.CloseAllLoadedUIForms(null);
        }

        /// <summary>
        /// 关闭所有已加载的界面。
        /// </summary>
        /// <param name="userData">用户自定义数据。</param>
        public static void CloseAllLoadedUIForms(this UIComponent self, object userData)
        {
            UIForm[] uiForms = self.GetAllLoadedUIForms();
            foreach (UIForm uiForm in uiForms)
            {
                if (!self.HasUIForm(uiForm.m_SerialId))
                {
                    continue;
                }

                self.CloseUIForm(uiForm, userData);
            }
        }

        /// <summary>
        /// 关闭所有正在加载的界面。
        /// </summary>
        public static void CloseAllLoadingUIForms(this UIComponent self)
        {
            foreach (KeyValuePair<int, string> uiFormBeingLoaded in self.m_UIFormsBeingLoaded)
            {
                self.m_UIFormsToReleaseOnLoad.Add(uiFormBeingLoaded.Key);
            }

            self.m_UIFormsBeingLoaded.Clear();
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        public static void RefocusUIForm(this UIComponent self, UIForm uiForm)
        {
            self.RefocusUIForm(uiForm, null);
        }

        /// <summary>
        /// 激活界面。
        /// </summary>
        /// <param name="uiForm">要激活的界面。</param>
        /// <param name="userData">用户自定义数据。</param>
        public static void RefocusUIForm(this UIComponent self, UIForm uiForm, object userData)
        {
            if (uiForm == null)
            {
                Log.Error("UI form is invalid.");
            }

            UIGroup uiGroup = uiForm.GetParent<UIGroup>();
            if (uiGroup == null)
            {
                Log.Error("UI group is invalid.");
            }

            uiGroup.RefocusUIForm(uiForm, userData);
            uiGroup.Refresh();
            uiForm.OnRefocus(userData);
        }

        /// <summary>
        /// 设置界面实例是否被加锁。
        /// </summary>
        /// <param name="uiFormInstance">要设置是否被加锁的界面实例。</param>
        /// <param name="locked">界面实例是否被加锁。</param>

        /// <summary>
        /// 设置界面实例的优先级。
        /// </summary>
        /// <param name="uiFormInstance">要设置优先级的界面实例。</param>
        /// <param name="priority">界面实例优先级。</param>

        //private void InternalOpenUIForm(this UIComponent self, int serialId, string uiFormAssetName, UIGroup uiGroup, object uiFormInstance, bool pauseCoveredUIForm, bool isNewInstance, float duration, object userData)
        //{
        //    try
        //    {
        //        UIForm uiForm = UIFormHelper.CreateUIForm(uiFormInstance, uiGroup, userData);
        //        if (uiForm == null)
        //        {
        //            Log.Error("Can not create UI form in UI form helper.");
        //        }

        //        uiForm.OnInit(serialId, uiFormAssetName, pauseCoveredUIForm, isNewInstance, userData);
        //        uiGroup.AddUIForm(uiForm);
        //        uiForm.OnOpen(userData);
        //        uiGroup.Refresh();

        //        Game.EventSystem.Publish(new UICommonEventType.OpenUIFormSuccessEventArgs());
        //    }
        //    catch (Exception)
        //    {
        //        Game.EventSystem.Publish(new UICommonEventType.OpenUIFormFailureEventArgs());
        //        throw;
        //    }
        //}

        //private void LoadAssetSuccessCallback(string uiFormAssetName, object uiFormAsset, float duration, object userData)
        //{
        //    OpenUIFormInfo openUIFormInfo = (OpenUIFormInfo)userData;
        //    if (openUIFormInfo == null)
        //    {
        //        throw new GameFrameworkException("Open UI form info is invalid.");
        //    }

        //    if (m_UIFormsToReleaseOnLoad.Contains(openUIFormInfo.SerialId))
        //    {
        //        m_UIFormsToReleaseOnLoad.Remove(openUIFormInfo.SerialId);
        //        ReferencePool.Release(openUIFormInfo);
        //        m_UIFormHelper.ReleaseUIForm(uiFormAsset, null);
        //        return;
        //    }

        //    m_UIFormsBeingLoaded.Remove(openUIFormInfo.SerialId);
        //    UIFormInstanceObject uiFormInstanceObject = UIFormInstanceObject.Create(uiFormAssetName, uiFormAsset, m_UIFormHelper.InstantiateUIForm(uiFormAsset), m_UIFormHelper);
        //    m_InstancePool.Register(uiFormInstanceObject, true);

        //    InternalOpenUIForm(openUIFormInfo.SerialId, uiFormAssetName, openUIFormInfo.UIGroup, uiFormInstanceObject.Target, openUIFormInfo.PauseCoveredUIForm, true, duration, openUIFormInfo.UserData);
        //    ReferencePool.Release(openUIFormInfo);
        //}

        //private void LoadAssetFailureCallback(string uiFormAssetName, LoadResourceStatus status, string errorMessage, object userData)
        //{
        //    OpenUIFormInfo openUIFormInfo = (OpenUIFormInfo)userData;
        //    if (openUIFormInfo == null)
        //    {
        //        throw new GameFrameworkException("Open UI form info is invalid.");
        //    }

        //    if (m_UIFormsToReleaseOnLoad.Contains(openUIFormInfo.SerialId))
        //    {
        //        m_UIFormsToReleaseOnLoad.Remove(openUIFormInfo.SerialId);
        //        return;
        //    }

        //    m_UIFormsBeingLoaded.Remove(openUIFormInfo.SerialId);
        //    string appendErrorMessage = Utility.Text.Format("Load UI form failure, asset name '{0}', status '{1}', error message '{2}'.", uiFormAssetName, status, errorMessage);
        //    if (m_OpenUIFormFailureEventHandler != null)
        //    {
        //        OpenUIFormFailureEventArgs openUIFormFailureEventArgs = OpenUIFormFailureEventArgs.Create(openUIFormInfo.SerialId, uiFormAssetName, openUIFormInfo.UIGroup.Name, openUIFormInfo.PauseCoveredUIForm, appendErrorMessage, openUIFormInfo.UserData);
        //        m_OpenUIFormFailureEventHandler(this, openUIFormFailureEventArgs);
        //        ReferencePool.Release(openUIFormFailureEventArgs);
        //        return;
        //    }

        //    throw new GameFrameworkException(appendErrorMessage);
        //}

        //private void LoadAssetUpdateCallback(string uiFormAssetName, float progress, object userData)
        //{
        //    OpenUIFormInfo openUIFormInfo = (OpenUIFormInfo)userData;
        //    if (openUIFormInfo == null)
        //    {
        //        throw new GameFrameworkException("Open UI form info is invalid.");
        //    }

        //    if (m_OpenUIFormUpdateEventHandler != null)
        //    {
        //        OpenUIFormUpdateEventArgs openUIFormUpdateEventArgs = OpenUIFormUpdateEventArgs.Create(openUIFormInfo.SerialId, uiFormAssetName, openUIFormInfo.UIGroup.Name, openUIFormInfo.PauseCoveredUIForm, progress, openUIFormInfo.UserData);
        //        m_OpenUIFormUpdateEventHandler(this, openUIFormUpdateEventArgs);
        //        ReferencePool.Release(openUIFormUpdateEventArgs);
        //    }
        //}

        //private void LoadAssetDependencyAssetCallback(string uiFormAssetName, string dependencyAssetName, int loadedCount, int totalCount, object userData)
        //{
        //    OpenUIFormInfo openUIFormInfo = (OpenUIFormInfo)userData;
        //    if (openUIFormInfo == null)
        //    {
        //        throw new GameFrameworkException("Open UI form info is invalid.");
        //    }

        //    if (m_OpenUIFormDependencyAssetEventHandler != null)
        //    {
        //        OpenUIFormDependencyAssetEventArgs openUIFormDependencyAssetEventArgs = OpenUIFormDependencyAssetEventArgs.Create(openUIFormInfo.SerialId, uiFormAssetName, openUIFormInfo.UIGroup.Name, openUIFormInfo.PauseCoveredUIForm, dependencyAssetName, loadedCount, totalCount, openUIFormInfo.UserData);
        //        m_OpenUIFormDependencyAssetEventHandler(this, openUIFormDependencyAssetEventArgs);
        //        ReferencePool.Release(openUIFormDependencyAssetEventArgs);
        //    }
        //}

    }
}