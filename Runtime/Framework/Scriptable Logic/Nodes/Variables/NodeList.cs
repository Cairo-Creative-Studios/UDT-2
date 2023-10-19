using System;
using UnityEngine;
using UDT.Scriptables.Utilities;
using System.Collections.Generic;

// TODO: List construction might rely heavily on programmtically handling port connections. 
// If it's not working, that's probably why
namespace UDT.Scriptables.Variables
{
    public class NodeList : VariableNode<List<VariableNode>>
    {}

    public class NodeList<T> : NodeList
    {
        [Input(dynamicPortList = true)] public List<VariableNode> Items;
        public Type type = typeof(T);

        new void OnValidate()
        {
            foreach(var item in Items.ToArray())
            {
                if(item == null || item.Value.GetType() != type)
                    Items.Remove(item);
                if(!Value.Contains(item))
                    Value.Add(item);
            }
        }
    }
}
