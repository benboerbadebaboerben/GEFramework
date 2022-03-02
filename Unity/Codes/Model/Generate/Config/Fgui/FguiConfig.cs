//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;



namespace Cfg.Fgui
{

public sealed partial class FguiConfig :  Bright.Config.BeanBase 
{
    public FguiConfig(ByteBuf _buf) 
    {
        Id = (FGUIType)_buf.ReadInt();
        Name = _buf.ReadString();
        Path = _buf.ReadString();
        PackageName = _buf.ReadString();
        ComponentName = _buf.ReadString();
        Layer = (Enums.FGUILayer)_buf.ReadInt();
        PostInit();
    }

    public static FguiConfig DeserializeFguiConfig(ByteBuf _buf)
    {
        return new Fgui.FguiConfig(_buf);
    }

    /// <summary>
    /// Id,它和FGUIType的ID直接相关，记得加上对应枚举
    /// </summary>
    public FGUIType Id { get; private set; }
    /// <summary>
    /// 名字
    /// </summary>
    public string Name { get; private set; }
    /// <summary>
    /// Desc文件的Addressable路径
    /// </summary>
    public string Path { get; private set; }
    /// <summary>
    /// 包名
    /// </summary>
    public string PackageName { get; private set; }
    /// <summary>
    /// 组件名
    /// </summary>
    public string ComponentName { get; private set; }
    /// <summary>
    /// 层级，这个枚举在Defines\enums_define.xml中
    /// </summary>
    public Enums.FGUILayer Layer { get; private set; }

    public const int __ID__ = 1974202160;
    public override int GetTypeId() => __ID__;

    public  void Resolve(Dictionary<string, object> _tables)
    {
        PostResolve();
    }

    public  void TranslateText(System.Func<string, string, string> translator)
    {
    }

    public override string ToString()
    {
        return "{ "
        + "Id:" + Id + ","
        + "Name:" + Name + ","
        + "Path:" + Path + ","
        + "PackageName:" + PackageName + ","
        + "ComponentName:" + ComponentName + ","
        + "Layer:" + Layer + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
