using Codice.CM.WorkspaceServer.Tree.GameUI.Checkin.Updater;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace UDT.AI
{
    public class SimpleCognitiveMap
    {
        private int size = 10;
        public Grid<Specimin> population;

        [SerializeField] private int[] networkStructure;
        [SerializeField] private float learningRate;
        [SerializeField] private float mutationRate;
        [SerializeField] private float scoringFalloff;
        [NonSerialized] private (int, int) currentSpecimen = (0, 0);
        [NonSerialized] private List<Specimin> contributors = new();
        [SerializeField] private float mutationSpread;
        private bool started = false;

        [SerializeField] private List<string> savedMultilayerPerceptrons = new();
        public enum NetworkState
        {
            inference,
            backpropagation,
            crossover
        }
        [NonSerialized] private NetworkState networkState = NetworkState.inference;

        public SimpleCognitiveMap(int size, int[] neuralNetworkStructure, float learningRate = 0.1f, float mutationRate = 0.1f, float scoringFalloff = 0.2f, float[] inputAffinity = null, float mutationSpread = 0.1f)
        {
            this.size = size;
            this.networkStructure = neuralNetworkStructure;
            this.learningRate = learningRate;
            this.mutationRate = mutationRate;
            this.scoringFalloff = scoringFalloff;
            this.mutationSpread = mutationSpread;

            population = new(this.size, this.size);

            foreach(var specimen in population)
            {
                specimen.value = new(networkStructure, learningRate, mutationRate);
            }
        }
        public async Task AdvanceToNextGeneration()
        {
            networkState = NetworkState.backpropagation;
            await Learn();
            networkState = NetworkState.crossover;
            await CropAndBlend();
            networkState = NetworkState.inference;
        }

        public float[] Infer(float[] inputs)
        {
            if (networkState != NetworkState.inference)
            {
                return null;
            }

            if(started)
            {
                var adjacentNodes = population.GetAdjacentNodes(population[currentSpecimen], true);
                foreach (var node in adjacentNodes) node.value.ScoreInput(inputs);
                adjacentNodes = adjacentNodes.OrderBy(x => x.value.tempScore).ToArray();
                
                var totalScore = adjacentNodes.Sum(x => x.value.tempScore);
                float randomValue = UnityEngine.Random.Range(0, totalScore);

                float cumulativeScore = 0;
                foreach(var node in adjacentNodes)
                {
                    cumulativeScore += node.value.tempScore;
                    if (randomValue < cumulativeScore)
                    {
                        currentSpecimen = population.GetPositionOf(node);
                        break;
                    }
                }
            }
            else
            {
                currentSpecimen = (UnityEngine.Random.Range(0, size), UnityEngine.Random.Range(0, size));
                started = true;
            }

            contributors.Add(population[currentSpecimen].value);
            return population[currentSpecimen].value.network.FeedForward(inputs);
        }

        public void Score(float score)
        {
            contributors.Reverse();

            for (int i = 0; i < contributors.Count; i++)
            {
                var contributor = contributors[i];
                contributor.fitness += score * (i - (i / contributors.Count * scoringFalloff));
            }
            contributors.Clear();
        }

        private async Task Learn()
        {
            population.Sort((x, y) => x.value.fitness.CompareTo(y.value.fitness));
            population.Reverse();
            var bestFitness = population[0].value.fitness;

            for (int i = 0; i < population.Count; i++)
            {
                var specimin = population[i];
                await specimin.value.network.Backpropagation(bestFitness - specimin.value.fitness);
            }
        }


        private async Task CropAndBlend()
        {
            List<Specimin> newPopulation = new();

            float totalFitness = 0;

            foreach(var specimin in population)
            {
                totalFitness += specimin.value.fitness;
            }

            float averageFitness = totalFitness / population.Count;
            List<GridNode<Specimin>> weakNodes = new();
            List<GridNode<Specimin>> strongNodes = new();

            foreach (var specimin in population)
            {
                if (specimin.value.fitness < averageFitness)
                {
                    weakNodes.Add(specimin);
                }
            }

            foreach(var specimin in population)
            {
                var positionTuple = population.GetPositionOf(specimin);
                var speciminPosition = new Vector2(positionTuple.x, positionTuple.y);

                if(specimin.value.fitness >= averageFitness)
                {
                    foreach(var weakSpecimin in population)
                    {
                        var weakPositionTuple = population.GetPositionOf(weakSpecimin);
                        var weakSpeciminPosition = new Vector2(weakPositionTuple.x, weakPositionTuple.y);

                        for (int i = 0; i < weakSpecimin.value.fittestInput.Length; i++)
                        {
                            weakSpecimin.value.fittestInput[i] = Mathf.Lerp(weakSpecimin.value.fittestInput[i], specimin.value.fittestInput[i], ((size + size) - (Vector2.Distance(speciminPosition, weakSpeciminPosition) / (size + size))) * mutationSpread);
                        }
                    }
                }
            }

            await Task.Yield();
        }
    }
}