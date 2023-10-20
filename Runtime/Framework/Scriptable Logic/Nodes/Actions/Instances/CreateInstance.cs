using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using UDT.Scriptables.Variables;
using UnityEngine;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Instances/Actions/Create Instance")]
    public class CreateInstance : ActionNode
	{
		[Tooltip("If left blank, the Instances name will be the same as the prefab + it's Instance ID")]
		public string instanceName = "";
		[Dropdown("GetPrefabs")]
		[Input(backingValue = ShowBackingValue.Unconnected)]
		public Prefab prefab;
		[Output] public Instance instance;

		public override void Process()
		{
			var instance = new Instances.Instance(prefab.Value.GameObject);
			base.Process();
		}

		public DropdownList<UnityEngine.GameObject> GetPrefabs()
		{
			return Instances.Instance.PrefabDropdownList;
		}
	}
}