using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Random = UnityEngine.Random;

public class Node
{
    public int Id { get; private set; }
    public List<int> Children { get; private set; } = new List<int>();
    public List<int> Keys { get; private set; } = new List<int>();
    public char AsciiChar { get; private set; }

    public Node(int id)
    {
        Id = id;
        AsciiChar = (char)(id + 65);
    }

    public void AddChildren(int childId)
    {
        Children.Add(childId);
    }
    
    public void AddKey(int keyId)
    {
        Keys.Add(keyId);
    }
}

public class ProceduralGenerator : WorldEntity
{
    public List<Node> Nodes { get; private set; } = new();

    public void Setup(ProceduralConfig config)
    {
        _config = config;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GenerateSeed()
    {
        return Random.Range(UInt16.MinValue, UInt16.MaxValue);
    }

    public void Generate(int seed)
    {
        Random.InitState(seed);
        int nodeCount = Random.Range(_config.NodeCountRange.x, _config.NodeCountRange.y + 1);
        Nodes = new List<Node> (nodeCount)
        {
            new Node(0)
        };

        for (int i = 1; i < nodeCount; i++)
        {
            Nodes.Add(new Node(i));
            Node parent = Nodes[Random.Range(0, i)];

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

                if (Nodes[peek].Keys.Count < _config.MaxKeyCountPerNode)
                {
                    keyIndex = peek;
                    break;
                }
            }
            Node keyLocation = Nodes[keyIndex];

            parent.AddChildren(i);
            keyLocation.AddKey(i);
        }
    }

    private ProceduralConfig _config;
}
