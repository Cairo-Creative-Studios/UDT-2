using UnityEngine;
using UDT.Scriptables.Utilities;
using XNode;

namespace UDT.Scriptables.Variables
{
    [CreateNodeMenu("Variables/Value Types/String")]
    public sealed class Timer : VariableNode<UDT.Scriptables.Timer>
    {
        [Output] public Float currentTime;
    }
}
