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
        //不用UpdateSystem的原因：
        //ET的做法是把所有关于Update塞到一个Action里面
        //这样可能导致UI界面的Update调用顺序与其他模块混合
        /// <summary>
        /// 界面管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public static void OnUpdate(this UIForm self, float elapseSeconds, float realElapseSeconds)
        {

        }
        public static void OnRecycle(this UIForm self)
        {
            self.m_SerialId = 0;
            self.m_DepthInUIGroup = 0;
            self.m_PauseCoveredUIForm = true;
        }
    }
}
