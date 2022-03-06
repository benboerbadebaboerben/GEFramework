

namespace ET
{
	public class AppStartInitFinish_CreateLoginUI: AEvent<EventType.AppStartInitFinish>
	{
		protected override async ETTask Run(EventType.AppStartInitFinish args)
		{
			var config = ConfigComponent.Instance.Tables.UIConfigCategory.Get((int)UIType.Login);
			await args.ZoneScene.GetComponent<UIComponent>().OpenUIForm(config.AssetName, config.UIGroupName);
		}
	}
}
