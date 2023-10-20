using AI.Math;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AI.NeuralNetworks
{
    [Serializable]
    public class MultilayerPerceptron
    {
        /// <summary>
        /// The network is a jagged array of floats. Each array represents a layer of the network.
        /// </summary>
        public float[][] network;
        public float[][][] weights;
        private int[] networkStructure;
        private float learningRate = 0.1f;
        private float mutationRate = 0.1f;
        private float[] inputAffinity;

        public MultilayerPerceptron(int[] networkStructure, float learningRate = 0.01f, float mutationRate = 0.01f, float[] inputAffinity = null)
        {
            List<float[]> network = new();

            foreach (int layer in networkStructure)
            {
                network.Add(new float[layer]);
            }

            this.network = network.ToArray();
            this.networkStructure = networkStructure;
            this.learningRate = learningRate;
            this.mutationRate = mutationRate;
            this.inputAffinity = inputAffinity;

            InitializeWeights();
        }

        public MultilayerPerceptron(MultilayerPerceptron parent0, MultilayerPerceptron parent1)
        {
            List<float[]> network = new();

            foreach (int layer in networkStructure)
            {
                network.Add(new float[layer]);
            }

            this.network = network.ToArray();
            this.networkStructure = parent0.networkStructure;
            this.learningRate = parent0.learningRate;
            this.mutationRate = parent0.mutationRate;
            this.inputAffinity = parent0.inputAffinity;

            Crossover(parent0, parent1);
        }

        private void InitializeWeights()
        {
            List<float[][]> weights = new();

            for (int i = 1; i < networkStructure.Length; i++)
            {
                int prevLayerSize = networkStructure[i - 1];
                int currentLayerSize = networkStructure[i];

                float[][] layerWeights = new float[prevLayerSize][];

                for (int j = 0; j < prevLayerSize; j++)
                {
                    layerWeights[j] = new float[currentLayerSize];
                    // Initialize weights with random values or any suitable initialization method
                    for (int k = 0; k < currentLayerSize; k++)
                    {
                        layerWeights[j][k] = XavierInitialization(prevLayerSize, currentLayerSize); // Implement a suitable random initialization method.
                    }
                }

                weights.Add(layerWeights);
            }

            this.weights = weights.ToArray();
        }

        private void Crossover(MultilayerPerceptron parent1, MultilayerPerceptron parent2)
        {
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[i].Length; j++)
                {
                    for (int k = 0; k < weights[i][j].Length; k++)
                    {
                        weights[i][j][k] = UnityEngine.Random.Range(0, 2) == 0 ? parent1.weights[i][j][k] + UnityEngine.Random.Range(-mutationRate, mutationRate) : parent2.weights[i][j][k] + UnityEngine.Random.Range(-mutationRate, mutationRate);
                    }
                }
            }
        }

        public float[] FeedForward(float[] inputs)
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                network[0][i] = inputs[i];
            }

            for (int i = 1; i < network.Length; i++)
            {
                if (i == 1 && inputAffinity != null)
                {
                    for (int j = 0; j < network[i].Length; j++)
                    {
                        if (j < inputAffinity.Length)
                        {
                            network[i][j] *= inputAffinity[j];
                        }
                    }
                    continue;
                }

                for (int j = 0; j < network[i].Length; j++)
                {
                    float sum = 0;

                    for (int k = 0; k < network[i - 1].Length; k++)
                    {
                        sum += network[i - 1][k] * weights[i - 1][k][j];
                    }

                    network[i][j] = AIMath.Sigmoid(sum);
                }
            }

            return network[^1];
        }

        public async Task Backpropagation(float error)
        {
            // Calculate the error at the output layer
            float[] outputErrors = new float[networkStructure[^1]];
            for (int i = 0; i < networkStructure[^1]; i++)
            {
                float output = network[^1][i];
                outputErrors[i] = error * AIMath.SigmoidDerivative(output);
            }

            // Create a list to store tasks for parallel execution
            List<Task> tasks = new List<Task>();

            // Propagate the error backward through the network and adjust weights in parallel
            await Task.Run(() =>
            {
                Parallel.For(1, network.Length, async i =>
                {
                    for (int j = 0; j < network[i].Length; j++)
                    {
                        float errorGradient = outputErrors[j];
                        for (int k = 0; k < network[i - 1].Length; k++)
                        {
                            weights[i - 1][k][j] += learningRate * errorGradient * network[i - 1][k];
                        }
                    }

                    if (i > 1)
                    {
                        float[] layerErrors = new float[networkStructure[i - 1]];
                        for (int j = 0; j < networkStructure[i - 1]; j++)
                        {
                            float errorSum = 0;
                            for (int k = 0; k < network[i].Length; k++)
                            {
                                errorSum += outputErrors[k] * weights[i - 1][j][k];
                            }
                            layerErrors[j] = errorSum * AIMath.SigmoidDerivative(network[i - 1][j]);
                        }
                        outputErrors = layerErrors;
                    }
                    await Task.Yield();
                });
            });
        }

        private float XavierInitialization(int fanIn, int fanOut)
        {
            float variance = 2.0f / (fanIn + fanOut);
            float standardDeviation = (float)Mathf.Sqrt(variance);

            // Generate a random weight using a Gaussian distribution with mean 0 and standard deviation = sqrt(2 / (fanIn + fanOut))
            float weight = (float)UnityEngine.Random.Range(0, standardDeviation);
            return weight;
        }
    }
}