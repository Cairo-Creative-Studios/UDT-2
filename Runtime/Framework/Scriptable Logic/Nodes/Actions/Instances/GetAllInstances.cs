using UDT.Scriptables.Utilities;
using NaughtyAttributes;
using UDT.Scriptables.Variables;
using UDT.PrefabTables;

namespace UDT.Scriptables.Actions
{
    [CreateNodeMenu("Instances/Actions/Get All Instances of Prefab")]
    public class GetAllInstances : ActionNode
	{
		[Dropdown("GetPrefabs")]
		public UnityEngine.GameObject prefab;
		[Output] public InstanceList instances;

		public override void Process()
		{
			var pool = PrefabPools.GetPool(prefab, false);

            instances.Value.Clear();
			if (prefab != null && instances != null) 
			{
				foreach(var instance in PrefabPools.GetPool(prefab, false))
				{
					var instanceVariable = CreateInstance<Instance>();
					instanceVariable.Value = instance;
					instances.Value.Add(instanceVariable);
				}
			}
			base.Process();
		}

		public DropdownList<UnityEngine.GameObject> GetPrefabs()
		{
			return Instances.Instance.PrefabDropdownList;
		}
	}
}