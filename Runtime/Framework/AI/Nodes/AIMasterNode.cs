using UDT.Scriptables.Utilities;

namespace UDT.AI
{
    [CreateNodeMenu("AI/AI Master Node")]
    public class AIMasterNode : MasterNode
    {
        public enum AINetwork
        {
            GeneticCognitiveMap
        }
        public AINetwork networkType;

        public int populationSize;

        public int[] specimenNeuralNetworkStructure;
        public float learningRate = 0.1f;
        public float mutationRate = 0.1f;
        public float scoringFalloff = 0.2f;
        public float mutationSpread = 0.1f;
    }
}