using System;

namespace ET
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AUIEventAttribute: BaseAttribute
    {
        public UIType UIType
        {
            get;
        }

        public AUIEventAttribute(UIType uiType)
        {
            this.UIType = uiType;
        }
    }
}