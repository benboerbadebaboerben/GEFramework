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
            
        }

        private static void Cancel(this AIComponent self)
        {
            self.CancellationToken?.Cancel();
            self.Current = 0;
            self.CancellationToken = null;
        }
    }
} 