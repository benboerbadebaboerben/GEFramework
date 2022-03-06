//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;



namespace ET
{

public sealed partial class UIConfig :  Bright.Config.BeanBase 
{
    public UIConfig(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Comment = _buf.ReadString();
        AssetName = _buf.ReadString();
        UIGroupName = _buf.ReadString();
        AllowMultiInstance = _buf.ReadBool();
        PauseCoveredUIForm = _buf.ReadBool();
        PostInit();
    }

    public static UIConfig DeserializeUIConfig(ByteBuf _buf)
    {
        return new UIConfig(_buf);
    }

    /// <summary>
    /// Id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 描述
    /// </summary>
    public string Comment { get; private set; }
    /// <summary>
    /// 资源名称
    /// </summary>
    public string AssetName { get; private set; }
    /// <summary>
    /// 界面组名称
    /// </summary>
    public string UIGroupName { get; private set; }
    /// <summary>
    /// 是否允许多个界面实例
    /// </summary>
    public bool AllowMultiInstance { get; private set; }
    /// <summary>
    /// 是否暂停被覆盖的界面
    /// </summary>
    public bool PauseCoveredUIForm { get; private set; }

    public const int __ID__ = 202324726;
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
        + "Comment:" + Comment + ","
        + "AssetName:" + AssetName + ","
        + "UIGroupName:" + UIGroupName + ","
        + "AllowMultiInstance:" + AllowMultiInstance + ","
        + "PauseCoveredUIForm:" + PauseCoveredUIForm + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}