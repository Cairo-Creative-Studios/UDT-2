using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.Instances;
using System.Collections.Generic;
using XNode;

namespace UDT.Scriptables.Variables
{
    [CreateNodeMenu("Variables/System/Lists/Generic Object List")]
    public sealed class GenericObjectList : NodeList<Variables.GameObject>    
    {
    }
}
