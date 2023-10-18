using UnityEngine;
using UDT.Scriptables.Utilities;
using UDT.Instances;
using System.Collections.Generic;
using XNode;

namespace UDT.Scriptables.Variables
{
    [CreateNodeMenu("Variables/Objects/Lists/GameObject List")]
    public sealed class GameObjectList : NodeList<Variables.GameObject>    
    {
    }
}
