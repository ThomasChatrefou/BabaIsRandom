using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGenerator
{
    public struct Input
    {
        public int NodesCountMin;
        public int NodesCountMax;
        public int MaxKeyCountPerNode;
        public int Seed;
    }

    public static event Action<Graph> Generated;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GenerateSeed()
    {
        return Random.Range(UInt16.MinValue, UInt16.MaxValue);
    }

    public static void FindNearestValidSeed(ref int seed)
    {
        Mathf.Clamp(seed, 0, Int32.MaxValue - 1);
    }

    public static bool Generate(out Graph outputGraph, Input input)
    {
        Debug.Log("[ProceduralGenerator] Generating graph...");
        Random.InitState(input.Seed);
        int nodeCount = Random.Range(input.NodesCountMin, input.NodesCountMax + 1);
        bool canGenerate = nodeCount > 0;
        if (canGenerate)
        {
            List<Node> outputNodes = new();
            Generate_Internal(ref outputNodes, input, nodeCount);
            outputGraph = new Graph(outputNodes);
            Generated?.Invoke(outputGraph);
            Debug.Log($"[ProceduralGenerator] {nodeCount} nodes generated !");
        }
        else
        {
            outputGraph = new Graph(new List<Node>());
            Debug.LogWarning("[ProceduralGenerator] could not generate puzzle");
        }
        return canGenerate;
    }

    #region Private

    private static void Generate_Internal(ref List<Node> outputNodes, Input input, int count)
    {
        outputNodes = new List<Node>(count) { new Node(0) };
        for (int i = 1; i < count; i++)
        {
            outputNodes.Add(new Node(i));
            Node parent = outputNodes[Random.Range(0, i)];

            List<int> candidates = new(i);
            for (int j = 0; j < i; j++)
            {
                candidates.Add(j);
            }
            int keyIndex = 0;
            for (int j = 0; j < i; j++)
            {
                int peekIdx = Random.Range(0, candidates.Count);
                int peek = candidates[peekIdx];
                candidates.RemoveAt(peekIdx);

                if (outputNodes[peek].Keys.Count < input.MaxKeyCountPerNode)
                {
                    keyIndex = peek;
                    break;
                }
            }
            parent.Children.Add(i);
            outputNodes[keyIndex].Keys.Add(i);
        }
    }

    #endregion Private
}
