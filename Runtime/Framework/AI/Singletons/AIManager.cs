using System.Collections.Generic;
using System.Linq;
using UDT.Scriptables;
using UDT.System;
using UnityEngine;

namespace UDT.AI
{
    public class AIManager : Singleton<AIManager>
    {
        private static List<ScriptableEventObject> Agents = new();
        private static AIGraph[] graphs;
        public static AIGraph[] AIGraphs
        {
            get
            {
                if(graphs == null)
                {
                    graphs = Resources.LoadAll<AIGraph>("");
                }
                return graphs;
            }
        }

        public static AIGraph CreateAIAgent(string graphName)
        {
            var graph = AIGraphs.FirstOrDefault(x => x.name == graphName);
            Agents.Add(ScriptableManager.CreateEventSingleton(graph));
            return graph;
        }
    }
}