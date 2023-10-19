using NaughtyAttributes;
using UDT.Scriptables.Utilities;
using UnityEngine;

namespace UDT.Scriptables.Variables
{
    [CreateNodeMenu("Variables/Collision/Collider")]
    public sealed class CollisionLayer : VariableNode<int>
    {
        [SerializeProperty("Value", true)]
        [Dropdown("GetCollisionLayers")]
        public new int value;


        public DropdownList<int> GetCollisionLayers()
        {
            DropdownList<int> returnList = new();
            string layerName = " ";

            int index = 0;
            while(layerName.Length !> 0)
            {
                layerName = LayerMask.LayerToName(index);
                returnList.Add(layerName, index);
                index++;
            }

            return returnList;
        }
    }
}
