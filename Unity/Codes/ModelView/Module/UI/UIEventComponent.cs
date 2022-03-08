using System.Collections.Generic;

namespace ET
{
    public class UIEventComponent : Entity,IAwake,IDestroy
    {
        public static UIEventComponent Instance { get; set; }
        public readonly Dictionary<UIType, IAUIEventHandler> UIEventHandlers = new Dictionary<UIType, IAUIEventHandler>();
    }
}