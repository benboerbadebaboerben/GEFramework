using UnityEngine;

namespace ET
{
    [ObjectSystem]
    public class GlobalComponentAwakeSystem: AwakeSystem<GlobalComponent>
    {
        public override void Awake(GlobalComponent self)
        {
            GlobalComponent.Instance = self;
            
            self.Global = GameObject.Find("/Global").transform;
            self.Unit = GameObject.Find("/Global/Unit").transform;
            self.UI = GameObject.Find("/Global/UI").transform;
            self.NormalGroup = GameObject.Find("Global/UI/NormalRoot").transform;
            self.PopupGroup = GameObject.Find("Global/UI/PopUpRoot").transform;
            self.HintGroup = GameObject.Find("Global/UI/FixedRoot").transform;
            self.ToppestGroup = GameObject.Find("Global/UI/OtherRoot").transform;
        }
    }
}