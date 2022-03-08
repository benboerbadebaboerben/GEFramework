using System;

namespace ET
{
    [ObjectSystem]
    public class UIEventComponentAwakeSystem : AwakeSystem<UIEventComponent>
    {
        public override void Awake(UIEventComponent self)
        {
            UIEventComponent.Instance = self;
            self.Awake();
        }
    }
    
    [ObjectSystem]
    public class UIEventComponentDestroySystem : DestroySystem<UIEventComponent>
    {
        public override void Destroy(UIEventComponent self)
        {
            self.UIEventHandlers.Clear();
            UIEventComponent.Instance = null;
        }
    }
    
    
    public static class UIEventComponentSystem
    {
        public static void Awake(this UIEventComponent self)
        {
            self.UIEventHandlers.Clear();
            foreach (Type v in Game.EventSystem.GetTypes(typeof (AUIEventAttribute)))
            {
                AUIEventAttribute attr = v.GetCustomAttributes(typeof (AUIEventAttribute), false)[0] as AUIEventAttribute;
                self.UIEventHandlers.Add(attr.UIType, Activator.CreateInstance(v) as IAUIEventHandler);
            }
        }
        
        public static IAUIEventHandler GetUIEventHandler(this UIEventComponent self, UIType uiType)
        {
            if (self.UIEventHandlers.TryGetValue(uiType, out IAUIEventHandler handler))
            {
                return handler;
            }
            Log.Error($"uiType : {uiType} is not have any uiEvent");
            return null;
        }
    }
}