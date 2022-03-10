using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public static class UILoginComponentSystem
    {
        //初始化界面。
        public static void OnInit(this UILoginComponent self)
        {
            
        }

        //界面打开。
        public static void OnOpen(this UILoginComponent self)
        {
            var rf = self.GetParent<UIForm>().rf;
            self.account = rf.Get<GameObject>("Account");
            self.password = rf.Get<GameObject>("Password");
            self.loginBtn = rf.Get<GameObject>("LoginBtn").GetComponent<Button>();
            self.loginBtn.onClick.AddListener(self.OnLoginBtnClick);
        }

        //界面关闭。
        public static void OnClose(this UILoginComponent self)
        {

        }

        //界面回收。
        public static void OnRecycle(this UILoginComponent self)
        {

        }

        //界面暂停。
        public static void OnPause(this UILoginComponent self)
        {

        }

        //界面暂停恢复。
        public static void OnResume(this UILoginComponent self)
        {

        }

        //界面遮挡。
        public static void OnCover(this UILoginComponent self)
        {

        }

        //界面遮挡恢复。
        public static void OnReveal(this UILoginComponent self)
        {

        }

        //界面激活。
        public static void OnRefocus(this UILoginComponent self)
        {

        }

        //界面深度改变。
        public static void OnDepthChanged(this UILoginComponent self)
        {

        }

        //按钮点击。
        public static void OnLoginBtnClick(this UILoginComponent self)
        {
            Log.Error(Path.Combine(UnityEngine.Application.dataPath.Substring(0,
            UnityEngine.Application.dataPath.LastIndexOf('/')),
             ""));
            //LoginHelper.Login(
            //    self.DomainScene(),
            //    ConstValue.LoginAddress,
            //    self.account.GetComponent<InputField>().text,
            //    self.password.GetComponent<InputField>().text).Coroutine();
        }
    }
}
