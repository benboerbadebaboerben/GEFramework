//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using Bright.Serialization;
using System.Collections.Generic;



namespace Cfg.Demo
{

public sealed partial class StartProcessConfig :  Bright.Config.BeanBase 
{
    public StartProcessConfig(ByteBuf _buf) 
    {
        Id = _buf.ReadInt();
        MachineId = _buf.ReadInt();
        InnerPort = _buf.ReadInt();
        AppName = _buf.ReadString();
        PostInit();
    }

    public static StartProcessConfig DeserializeStartProcessConfig(ByteBuf _buf)
    {
        return new Demo.StartProcessConfig(_buf);
    }

    /// <summary>
    /// id
    /// </summary>
    public int Id { get; private set; }
    /// <summary>
    /// 所属机器
    /// </summary>
    public int MachineId { get; private set; }
    /// <summary>
    /// 内网端口
    /// </summary>
    public int InnerPort { get; private set; }
    /// <summary>
    /// 程序名
    /// </summary>
    public string AppName { get; private set; }

    public const int __ID__ = 141424442;
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
        + "MachineId:" + MachineId + ","
        + "InnerPort:" + InnerPort + ","
        + "AppName:" + AppName + ","
        + "}";
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}
