using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    [ObjectSystem]
    public class UIFormAwakeSystem : AwakeSystem<UIForm>
    {
        public override void Awake(UIForm self)
        {
            
        }
    }

    [ObjectSystem]
    public class UIFormUpdateSystem : UpdateSystem<UIForm>
    {
        public override void Update(UIForm self)
        {

        }
    }

    public static class UIFormSystem
    {
        public static void OnRecycle(this UIForm self)
        {
            self.m_SerialId = 0;
            self.m_DepthInUIGroup = 0;
            self.m_PauseCoveredUIForm = true;
        }
    }
}
