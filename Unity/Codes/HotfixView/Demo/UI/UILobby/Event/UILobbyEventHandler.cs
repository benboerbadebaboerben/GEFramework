using System;
using UnityEngine;

namespace ET
{
	[AUIEvent(UIType.UILobby)]
	public  class UILobbyEventHandler : IAUIEventHandler
	{
        //初始化界面。
        public void OnInit(UIForm uiForm, int serialId, string uiFormAssetName, bool pauseCoveredUIForm, object userData)
        {
            uiForm.OnInit(serialId, uiFormAssetName, pauseCoveredUIForm, userData);
            var childForm = uiForm.AddComponent<UILobbyComponent>();
            childForm.OnInit();
        }

        //界面打开。
        public void OnOpen(UIForm uiForm, GameObject obj)
        {
            InitUIForm(uiForm, obj);
            uiForm.OnOpen();
            var childForm = uiForm.GetComponent<UILobbyComponent>();
            childForm.OnOpen();
        }

        //界面关闭。
        public void OnClose(UIForm uiForm, bool isShutdown, object userData)
        {
            uiForm.OnClose(isShutdown,userData);
            var childForm = uiForm.GetComponent<UILobbyComponent>();
            childForm.OnClose();
        }

        //界面回收。
        public void OnRecycle(UIForm uiForm)
        {
            uiForm.OnRecycle();
            var childForm = uiForm.GetComponent<UILobbyComponent>();
            childForm.OnRecycle();
        }

        //界面暂停。
        public void OnPause(UIForm uiForm)
        {
            uiForm.OnPause();
            var childForm = uiForm.GetComponent<UILobbyComponent>();
            childForm.OnPause();
        }

        //界面暂停恢复。
        public void OnResume(UIForm uiForm)
        {
            uiForm.OnResume();
            var childForm = uiForm.GetComponent<UILobbyComponent>();
            childForm.OnResume();
        }

        //界面遮挡。
        public void OnCover(UIForm uiForm)
        {
            uiForm.OnCover();
            var childForm = uiForm.GetComponent<UILobbyComponent>();
            childForm.OnCover();
        }

        //界面遮挡恢复。
        public void OnReveal(UIForm uiForm)
        {
            uiForm.OnReveal();
            var childForm = uiForm.GetComponent<UILobbyComponent>();
            childForm.OnReveal();
        }

        //界面激活。
        public void OnRefocus(UIForm uiForm, object userData)
        {
            uiForm.OnRefocus(userData);
            var childForm = uiForm.GetComponent<UILobbyComponent>();
            childForm.OnRefocus();
        }

        //界面深度改变。
        public void OnDepthChanged(UIForm uiForm, int uiGroupDepth, int depthInUIGroup)
        {
            uiForm.OnDepthChanged(depthInUIGroup, depthInUIGroup);
            var childForm = uiForm.GetComponent<UILobbyComponent>();
            childForm.OnDepthChanged();
        }

        //初始化UIForm。
        private void InitUIForm(UIForm uiForm, GameObject obj)
        {
            var uiComponent = uiForm.ZoneScene().GetComponent<UIComponent>();
            uiForm.obj = obj;
            try
            {
                if (obj == null)
                {
                    if (uiComponent.m_UIFormsToReleaseOnLoad.Contains(uiForm.m_SerialId))
                    {
                        uiComponent.m_UIFormsToReleaseOnLoad.Remove(uiForm.m_SerialId);
                    }
                    throw new GameFrameworkException("Open UI form info is invalid.");

				}

                if (uiForm == null)
                {
                    Log.Error("Can not create UI form in UI form helper.");
                }

				uiComponent.m_UIFormsBeingLoaded.Remove(uiForm.m_SerialId);
				Game.EventSystem.Publish(new UICommonEventType.OpenUIFormSuccessEventArgs());
			}

			catch (Exception)
			{
				Game.EventSystem.Publish(new UICommonEventType.OpenUIFormFailureEventArgs());
				throw;
			}
			uiForm.GetParent<UIGroup>().Refresh();
			uiForm.rf = uiForm.obj.GetComponent<ReferenceCollector>();
        }
    }
}

