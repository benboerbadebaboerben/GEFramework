using ET.ObjectPool;
using ET.Resource;
using System.Collections.Generic;

namespace ET
{
	/// <summary>
	/// 管理Scene上的UI
	/// </summary>
	public static class UIComponentSystem
	{
		/// <summary>
		/// 获取界面组数量。
		/// </summary>
		public static int GetUIGroupCount(this UIComponent self)
		{
			return self.m_UIManager.UIGroupCount;
		}

		/// <summary>
		/// 获取界面实例对象池自动释放可释放对象的间隔秒数。
		/// </summary>
		public static float GetInstanceAutoReleaseInterval(this UIComponent self)
		{
			return self.m_UIManager.InstanceAutoReleaseInterval;
		}

		/// <summary>
		/// 设置界面实例对象池自动释放可释放对象的间隔秒数。
		/// </summary>
		/// <param name="value">间隔秒数。</param>
		public static void SetInstanceAutoReleaseInterval(this UIComponent self, float value)
		{
			self.m_InstanceAutoReleaseInterval = value;
			self.m_UIManager.InstanceAutoReleaseInterval = value;
		}

		/// <summary>
		/// 获取界面实例对象池对象过期秒数。
		/// </summary>
		public static float GetInstanceExpireTime(this UIComponent self)
		{
			return self.m_UIManager.InstanceExpireTime;
		}

		/// <summary>
		/// 设置界面实例对象池对象过期秒数。
		/// </summary>
		/// <param name="value">过期秒数。</param>
		public static void SetInstanceExpireTime(this UIComponent self, float value)
		{
			self.m_InstanceExpireTime = value;
			self.m_UIManager.InstanceExpireTime = value;
		}

		/// <summary>
		/// 获取界面实例对象池的优先级。
		/// </summary>
		public static int GetInstancePriority(this UIComponent self)
		{
			return self.m_UIManager.InstancePriority;
		}

		/// <summary>
		/// 设置界面实例对象池的优先级。
		/// </summary>
		/// <param name="value">优先级。</param>
		public static void SetInstancePriority(this UIComponent self,int value)
		{
			self.m_InstancePriority = value;
			self.m_UIManager.InstancePriority = value;
		}



		public static void OnOpenUIFormSuccess(this UIComponent self, object sender, UI.OpenUIFormSuccessEventArgs e)
		{
			//m_EventComponent.Fire(this, OpenUIFormSuccessEventArgs.Create(e));
		}

		public static void OnOpenUIFormFailure(this UIComponent self, object sender, UI.OpenUIFormFailureEventArgs e)
		{
			Log.Warning("Open UI form failure, asset name '{0}', UI group name '{1}', pause covered UI form '{2}', error message '{3}'.", e.UIFormAssetName, e.UIGroupName, e.PauseCoveredUIForm, e.ErrorMessage);
			//if (m_EnableOpenUIFormFailureEvent)
			//{
			//	m_EventComponent.Fire(this, OpenUIFormFailureEventArgs.Create(e));
			//}
		}

		public static void OnOpenUIFormUpdate(this UIComponent self, object sender, UI.OpenUIFormUpdateEventArgs e)
		{
			//m_EventComponent.Fire(this, OpenUIFormUpdateEventArgs.Create(e));
		}

		public static void OnOpenUIFormDependencyAsset(this UIComponent self, object sender, UI.OpenUIFormDependencyAssetEventArgs e)
		{
			//m_EventComponent.Fire(this, OpenUIFormDependencyAssetEventArgs.Create(e));
		}

		public static void OnCloseUIFormComplete(this UIComponent self, object sender, UI.CloseUIFormCompleteEventArgs e)
		{
			//m_EventComponent.Fire(this, CloseUIFormCompleteEventArgs.Create(e));
		}
	}

	[ObjectSystem]
	public class UIComponentAwakeSystem : AwakeSystem<UIComponent>
	{
		public override void Awake(UIComponent self)
		{
			self.m_UIManager.OpenUIFormSuccess += self.OnOpenUIFormSuccess;
			self.m_UIManager.OpenUIFormFailure += self.OnOpenUIFormFailure;
			self.m_UIManager.OpenUIFormUpdate += self.OnOpenUIFormUpdate;
			self.m_UIManager.OpenUIFormDependencyAsset += self.OnOpenUIFormDependencyAsset;
			self.m_UIManager.CloseUIFormComplete += self.OnCloseUIFormComplete;
			if(CodeLoader.Instance.CodeMode == CodeMode.Mono)
				self.m_UIManager.SetResourceManager(baseComponent.EditorResourceHelper);
			else
				self.m_UIManager.SetResourceManager(GameFrameworkEntry.GetModule<ResourceManager>());

			self.m_UIManager.SetObjectPoolManager(GameFrameworkEntry.GetModule<ObjectPoolManager>());
			self.m_UIManager.InstanceAutoReleaseInterval = self.m_InstanceAutoReleaseInterval;
			self.m_UIManager.InstanceCapacity = self.m_InstanceCapacity;
			self.m_UIManager.InstanceExpireTime = self.m_InstanceExpireTime;
			self.m_UIManager.InstancePriority = self.m_InstancePriority;
		}
	}
}