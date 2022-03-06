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

public sealed partial class UIGroupConfig :  Bright.Config.BeanBase 
{
    public UIGroupConfig(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        Comment = _buf.ReadString();
        UIGroupName = _buf.ReadString();
        Depth = _buf.ReadInt();
        PostInit();
    }

    public static UIGroupConfig DeserializeUIGroupConfig(ByteBuf _buf)
    {
        return new UIGroupConfig(_buf);
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
    /// 界面组名称
    /// </summary>
    public string UIGroupName { get; private set; }
    /// <summary>
    /// 界面组深度
    /// </summary>
    public int Depth { get; private set; }

    public const int __ID__ = -666942611;
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
        + "UIGroupName:" + UIGroupName + ","
        + "Depth:" + Depth + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}