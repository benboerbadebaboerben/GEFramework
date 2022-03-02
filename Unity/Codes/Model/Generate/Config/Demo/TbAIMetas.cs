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
   
public partial class TbAIMetas
{
    private readonly Dictionary<int, Demo.AIMetas> _dataMap;
    private readonly List<Demo.AIMetas> _dataList;
    
    public TbAIMetas(ByteBuf _buf)
    {
        _dataMap = new Dictionary<int, Demo.AIMetas>();
        _dataList = new List<Demo.AIMetas>();
        
        for(int n = _buf.ReadSize() ; n > 0 ; --n)
        {
            Demo.AIMetas _v;
            _v = Demo.AIMetas.DeserializeAIMetas(_buf);
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
        PostInit();
    }

    public Dictionary<int, Demo.AIMetas> DataMap => _dataMap;
    public List<Demo.AIMetas> DataList => _dataList;

    public Demo.AIMetas GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Demo.AIMetas Get(int key) => _dataMap[key];
    public Demo.AIMetas this[int key] => _dataMap[key];

    public void Resolve(Dictionary<string, object> _tables)
    {
        foreach(var v in _dataList)
        {
            v.Resolve(_tables);
        }
        PostResolve();
    }

    public void TranslateText(System.Func<string, string, string> translator)
    {
        foreach(var v in _dataList)
        {
            v.TranslateText(translator);
        }
    }
    
    partial void PostInit();
    partial void PostResolve();
}

}