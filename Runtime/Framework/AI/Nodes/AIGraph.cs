using System.Linq;
using UDT.Scriptables;

namespace UDT.AI
{
    public class AIGraph : EventGraph
    {
        AIMasterNode masterNode;
        AIInputNode[] inputNodes;
        AIOutputNode[] outputNodes;

        SimpleCognitiveMap cognitiveMap;

        public override void InitializeScript()
        {
            var masterNodes = nodes.OfType<AIMasterNode>();
            if(masterNodes != null )
            {
                masterNode = masterNodes.First();
            }
            var inputNodes = nodes.OfType<AIInputNode>();
            if (inputNodes != null)
            {
                this.inputNodes = inputNodes.ToArray();
            }
            var outputNodes = nodes.OfType<AIOutputNode>();
            if (outputNodes != null)
            {
                this.outputNodes = outputNodes.ToArray();
            }
        }

        public override void UpdateScript()
        {
            float[] inputs = new float[inputNodes[0].NetworkInputs.Count];

            foreach (var inputNode in inputNodes) 
            { 
                for(int i = 0; i < inputNode.NetworkInputs.Count; i++)
                {
                    inputs[i] = inputNode.NetworkInputs[i].Value;
                }
            }

            float[] outputs = cognitiveMap.Infer(inputs);


            foreach (var outputNode in outputNodes)
            {
                for (int i = 0; i < outputNode.NetworkOutputs.Count; i++)
                {
                    var output = outputNode.NetworkOutputs[i];
                }

                foreach (var sequence in outputNodes[0].OnUpdated)
                {
                    sequence.Process();
                }
            }
        }
    }
}