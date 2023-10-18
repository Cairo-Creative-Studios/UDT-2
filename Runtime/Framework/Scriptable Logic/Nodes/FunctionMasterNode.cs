using XNode;

namespace UDT.Scriptables.Utilities
{
    public class FunctionMasterNode : MasterNode
    {
        [Output(dynamicPortList = true)] public SerializableDictionary<string, object> parameters;


    }
}