using UnityEngine;

namespace ET
{
    public class GlobalComponent: Entity, IAwake
    {
        public static GlobalComponent Instance;
        
        public Transform Global;
        public Transform Unit;
        public Transform UI;
        public Transform NormalGroup;
        public Transform PopupGroup;
        public Transform HintGroup;
        public Transform ToppestGroup;
    }
}