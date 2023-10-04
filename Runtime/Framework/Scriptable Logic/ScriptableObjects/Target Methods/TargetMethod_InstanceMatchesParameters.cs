using UDT.Instances;
using UDT.Scriptables.Utilities;
using UnityEngine;

namespace UDT.Scriptables.TargetMethods
{
    public class TargetMethod_InstanceMatchesParameters : TargetMethod<TargetMethod_InstanceMatchesParameters, Instance>
    {
        public override Instance[] search(params ScriptableCondition[] parameters)
        {
            var instances = Instance.Instances;

            foreach (Instance instance in instances.ToArray())
            {
                foreach (ScriptableCondition parameter in parameters)
                {
                    if (parameter.value.GetType() == typeof(GameObject) && !parameter.Check(parameter.op == ScriptableCondition.Operator.And, instance.gameObject))
                    {
                        instances.Remove(instance);
                    }
                    else if(!parameter.Check(parameter.op == ScriptableCondition.Operator.And, instance))
                    {
                        instances.Remove(instance);
                    }
                }
            }

            return instances.ToArray();
        }
    }
}