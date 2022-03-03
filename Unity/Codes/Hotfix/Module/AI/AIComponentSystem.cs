using System;
using UnityEngine;

namespace ET
{
    [Timer(TimerType.AITimer)]
    public class AITimer: ATimer<AIComponent>
    {
        public override void Run(AIComponent self)
        {
            try
            {
                self.Check();
            }
            catch (Exception e)
            {
                Log.Error($"move timer error: {self.Id}\n{e}");
            }
        }
    }
    
    [ObjectSystem]
    public class AIComponentAwakeSystem: AwakeSystem<AIComponent, int>
    {
        public override void Awake(AIComponent self, int aiConfigId)
        {
            self.AIConfigId = aiConfigId;
            self.Timer = TimerComponent.Instance.NewRepeatedTimer(1000, TimerType.AITimer, self);
        }
    }

    [ObjectSystem]
    public class AIComponentDestroySystem: DestroySystem<AIComponent>
    {
        public override void Destroy(AIComponent self)
        {
            TimerComponent.Instance?.Remove(ref self.Timer);
            self.CancellationToken?.Cancel();
            self.CancellationToken = null;
            self.Current = 0;
        }
    }

    public static class AIComponentSystem
    {
        public static void Check(this AIComponent self)
        {
            if (self.Parent == null)
            {
                TimerComponent.Instance.Remove(ref self.Timer);
                return;
            }

            var oneAI = ConfigUtil.Tables.TbAIMetas.Get(self.AIConfigId);

            foreach (Cfg.Demo.AIMeta aiMeta in oneAI.Metas)
            {

                AIDispatcherComponent.Instance.AIHandlers.TryGetValue(aiMeta.Name, out AAIHandler aaiHandler);

                if (aaiHandler == null)
                {
                    Log.Error($"not found aihandler: {aiMeta.Name}");
                    continue;
                }

                int ret = aaiHandler.Check(self, aiMeta);
                if (ret != 0)
                {
                    continue;
                }

                if (self.Current == aiMeta.Id)
                {
                    break;
                }

                self.Cancel(); // 取消之前的行为
                ETCancellationToken cancellationToken = new ETCancellationToken();
                self.CancellationToken = cancellationToken;
                self.Current = aiMeta.Id;

                aaiHandler.Execute(self, aiMeta, cancellationToken).Coroutine();
                return;
            }
            
        }

        private static void Cancel(this AIComponent self)
        {
            self.CancellationToken?.Cancel();
            self.Current = 0;
            self.CancellationToken = null;
        }
    }
} 