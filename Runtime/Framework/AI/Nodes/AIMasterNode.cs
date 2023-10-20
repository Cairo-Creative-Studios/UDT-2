using System.Collections.Generic;
using System;
using UDT.Scriptables.Utilities;
using UnityEngine;

namespace UDT.AI
{
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