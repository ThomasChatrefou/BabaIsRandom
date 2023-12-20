using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProceduralGenerator
{
    public struct Input
    {
        public Vector2Int NodeCountRange;
        public int MaxKeyCountPerNode;
        public int Seed;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GenerateSeed()
    {
        return Random.Range(UInt16.MinValue, UInt16.MaxValue);
    }

    public static bool Generate(ref List<Node> outputNodes, Input input)
    {
        Debug.Log("[ProceduralGenerator] Generating graph...");
        Random.InitState(input.Seed);
        int nodeCount = Random.Range(input.NodeCountRange.x, input.NodeCountRange.y + 1);
        bool canGenerate = nodeCount > 0;
        if (canGenerate)
        {
            Generate_Internal(ref outputNodes, input, nodeCount);
            Debug.Log($"[ProceduralGenerator] {nodeCount} nodes generated !");
        }
        else
        {
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
            parent.AddChildren(i);
            outputNodes[keyIndex].AddKey(i);
        }
    }

    #endregion Private
}
