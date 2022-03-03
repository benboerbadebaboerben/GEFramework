using Bright.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ET
{
	[ObjectSystem]
	public class ConfigAwakeSystem : AwakeSystem<ConfigComponent>
	{
		public override void Awake(ConfigComponent self)
		{
			ConfigComponent.Instance = self;
		}
	}

	[ObjectSystem]
	public class ConfigDestroySystem : DestroySystem<ConfigComponent>
	{
		public override void Destroy(ConfigComponent self)
		{
			ConfigComponent.Instance = null;
		}
	}

	public static class ConfigComponentSystem
	{
		public static void LoadOneConfig(this ConfigComponent self, Type configType)
		{
		}

		public static async ETTask LoadAsync(this ConfigComponent self, Func<string, ByteBuf> loadFunc)
		{
			self.Tables = new Cfg.Tables(loadFunc);
			await ETTask.CompletedTask;
		}
	}
}