using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET
{
    public static class UIGroupSystem
    {
        //不用UpdateSystem的原因：
        //ET的做法是把所有关于Update塞到一个Action里面
        //这样可能导致UI界面的Update调用顺序与其他模块混合
        /// <summary>
        /// 界面管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        public static void Update(this UIGroup self,float elapseSeconds, float realElapseSeconds)
        {

        }
    }
}
