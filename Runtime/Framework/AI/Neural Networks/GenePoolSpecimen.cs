
using AI.NeuralNetworks;
using System;

namespace UDT.AI
{
    [Serializable]
    public class Specimin
    {
        public MultilayerPerceptron network;
        public float fitness;
        public float[] fittestInput;
        public float tempScore;

        public Specimin(int[] networkStructure, float learningRate, float mutationRate, float[] inputAffinity = null)
        {
            network = new MultilayerPerceptron(networkStructure, learningRate, mutationRate, inputAffinity);
        }

        public Specimin(Specimin parent0, Specimin parent1)
        {
            network = new MultilayerPerceptron(parent0.network, parent1.network);
        }

        /// <summary>
        /// The farther the Fittest Inputs are from the Inputs given, the lower the score. 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void ScoreInput(float[] input)
        {
            float score = 0;

            for(int i = 0; i < input.Length; i++)
            {
                score += fittestInput[i] - input[i];
            }

            tempScore = score;
        }
    }
}