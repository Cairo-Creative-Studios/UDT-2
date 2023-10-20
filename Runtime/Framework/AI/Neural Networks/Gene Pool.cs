
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace UDT.AI
{
    [Serializable]
    public class GenePool
    {
        [SerializeField] private int populationSize;
        [SerializeField] private int[] networkStructure;
        [SerializeField] private float learningRate;
        [SerializeField] private float mutationRate;
        [SerializeField] private float scoringFalloff;
        [SerializeField] private List<Specimin> population = new();
        [NonSerialized] private int currentSpecimen = 0;
        [NonSerialized] private List<Specimin> contributors = new();
        [SerializeField] private float[] inputAffinity;
        [SerializeField] private float mutationSpread;

        [SerializeField] private List<string> savedMultilayerPerceptrons = new();

        public enum NetworkState
        {
            inference,
            backpropagation,
            crossover
        }
        [NonSerialized] private NetworkState networkState = NetworkState.inference;

        public GenePool(int populationSize, int[] networkStructure, float learningRate = 0.1f, float mutationRate = 0.1f, float scoringFalloff = 0.2f, float[] inputAffinity = null, float mutationSpread = 0.1f)
        {
            this.populationSize = populationSize;
            this.networkStructure = networkStructure;
            this.learningRate = learningRate;
            this.mutationRate = mutationRate;
            this.scoringFalloff = scoringFalloff;
            this.inputAffinity = inputAffinity;
            this.mutationSpread = mutationSpread;

            InitializePopulation();
        }


        /// <summary>
        /// Returns the output of the current specimin, and advances to the next specimin.
        /// The pool will cycle through the population for Inference, and return to the first specimin when the last is reached.
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        public float[] Infer(float[] inputs)
        {
            if (networkState != NetworkState.inference)
            {
                return null;
            }

            currentSpecimen = (currentSpecimen + 1) % populationSize;
            contributors.Add(population[currentSpecimen]);
            return population[currentSpecimen].network.FeedForward(inputs);
        }

        /// <summary>
        /// Applies a fitness score to the specimins that contributed to the events that occured leading up to the score, 
        /// ands clears the contributing specimins to track the next score.
        /// </summary>
        /// <param name="score"></param>
        public void Score(float score)
        {
            contributors.Reverse();

            for (int i = 0; i < contributors.Count; i++)
            {
                var contributor = contributors[i];

                contributor.fitness += score;
            }
            contributors.Clear();
        }

        /// <summary>
        /// Serializes the GenePool to a JSON string.
        /// </summary>
        /// <param name="genePool"></param>
        /// <returns></returns>
        public static string Serialize(GenePool genePool)
        {
            return JsonConvert.SerializeObject(genePool);
        }

        /// <summary>
        /// Deserializes a GenePool from a JSON string.
        /// </summary>
        /// <param name="JSON"></param>
        /// <returns></returns>
        public static GenePool Deserialize(string JSON)
        {
            return JsonConvert.DeserializeObject<GenePool>(JSON);
        }

        /// <summary>
        /// Specimins who's fitness score is less than the fittest specimin will run Backpropagation with an error value of fittestFitness - speciminFitness.
        /// Then, the population will be sorted by fitness, and the fittest specimins will be selected to breed the next generation.
        /// </summary>
        public async Task AdvanceToNextGeneration()
        {
            networkState = NetworkState.backpropagation;
            await Learn();
            networkState = NetworkState.crossover;
            await Crossover();
            networkState = NetworkState.inference;
        }

        void InitializePopulation()
        {
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new Specimin(networkStructure, learningRate, mutationRate));
            }
        }

        Specimin SelectParent(Specimin other = null)
        {
            var searchedPopulation = population;

            if (other != null)
                searchedPopulation.Remove(other);

            Specimin randomParent = searchedPopulation[UnityEngine.Random.Range(0, populationSize)];

            foreach (var fitParent in searchedPopulation)
            {
                if (fitParent.fitness > randomParent.fitness && UnityEngine.Random.Range(0, 10) > 5)
                {
                    randomParent = fitParent;
                }
            }

            return randomParent;
        }

        private async Task Learn()
        {
            population.Sort((x, y) => x.fitness.CompareTo(y.fitness));
            population.Reverse();
            var bestFitness = population[0].fitness;

            for (int i = 0; i < population.Count; i++)
            {
                var specimin = population[i];
                await specimin.network.Backpropagation(bestFitness - specimin.fitness);
            }
        }


        private async Task Crossover()
        {
            List<Specimin> newPopulation = new();

            for (int i = 0; i < populationSize; i++)
            {
                Specimin parent0 = SelectParent();
                Specimin parent1 = SelectParent(parent0);

                newPopulation.Add(new Specimin(parent0, parent1));
                await Task.Yield();
            }

            population = newPopulation;
        }
    }
}