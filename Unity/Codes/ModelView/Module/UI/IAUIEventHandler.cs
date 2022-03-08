using UnityEngine;

namespace ET
{
    public interface IAUIEventHandler
    {
        //
        // 摘要:
        //     初始化界面。
        //
        // 参数:
        //   serialId:
        //     界面序列编号。
        //
        //   uiFormAssetName:
        //     界面资源名称。
        //
        //   uiGroup:
        //     界面所属的界面组。
        //
        //   pauseCoveredUIForm:
        //     是否暂停被覆盖的界面。
        //
        //   isNewInstance:
        //     是否是新实例。
        //
        //   userData:
        //     用户自定义数据。
        void OnInit(UIForm uiForm, int serialId, string uiFormAssetName, bool pauseCoveredUIForm, object userData);

        //
        // 摘要:
        //     界面回收。
        void OnRecycle(UIForm uiForm);

        //
        // 摘要:
        //     界面打开。
        //
        // 参数:
        //   userData:
        //     用户自定义数据。
        void OnOpen(UIForm uiForm, GameObject obj);

        //
        // 摘要:
        //     界面关闭。
        //
        // 参数:
        //   isShutdown:
        //     是否是关闭界面管理器时触发。
        //
        //   userData:
        //     用户自定义数据。
        void OnClose(UIForm uiForm, bool isShutdown, object userData);

        //
        // 摘要:
        //     界面暂停。
        void OnPause(UIForm uiForm);

        //
        // 摘要:
        //     界面暂停恢复。
        void OnResume(UIForm uiForm);

        //
        // 摘要:
        //     界面遮挡。
        void OnCover(UIForm uiForm);

        //
        // 摘要:
        //     界面遮挡恢复。
        void OnReveal(UIForm uiForm);

        //
        // 摘要:
        //     界面激活。
        //
        // 参数:
        //   userData:
        //     用户自定义数据。
        void OnRefocus(UIForm uiForm, object userData);

        //
        // 摘要:
        //     界面深度改变。
        //
        // 参数:
        //   uiGroupDepth:
        //     界面组深度。
        //
        //   depthInUIGroup:
        //     界面在界面组中的深度。
        void OnDepthChanged(UIForm uiForm, int uiGroupDepth, int depthInUIGroup);
    }
}