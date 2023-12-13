using System.Collections.Generic;
using UnityEngine;

public class ProceduralStringTranslator : WorldBehaviour, IProceduralTranslator
{
    public void Translate(List<Node> nodes)
    {
        Debug.Log($"[ProceduralStringTranslator] coucou");

        RecursionState initState = new()
        {
            ReachableNodes = new(),
            OwnedKeys = new(),
            CandidatesForNext = new() { 0 },
            Path = ""
        };

        List<string> outputs = new();
        Solve_Recursive(ref outputs, initState, nodes);
        foreach(string output in outputs)
        {
            Debug.Log(output);
        }
    }

    private struct RecursionState
    {
        public HashSet<int> ReachableNodes;
        public List<int> OwnedKeys;
        public List<int> CandidatesForNext;
        public string Path;

        public RecursionState(RecursionState other)
        {
            ReachableNodes = new(other.ReachableNodes);
            OwnedKeys = new(other.OwnedKeys);
            CandidatesForNext = new(other.CandidatesForNext);
            Path = new(other.Path);
        }
    }

    private void Solve_Recursive(ref List<string> outputs, RecursionState state, List<Node> nodes)
    {
        if (state.CandidatesForNext.Count > 0)
        {
            foreach (int nodeIdx in state.CandidatesForNext)
            {
                RecursionState newState = ComputeNextState(new RecursionState(state), nodes[nodeIdx]);
                Solve_Recursive(ref outputs, newState, nodes);
            }
        }
        else
        {
            outputs.Add(state.Path);
        }
    }

    private RecursionState ComputeNextState(RecursionState state, Node currentNode)
    {
        state.Path += currentNode.AsciiChar;
        state.CandidatesForNext.Remove(currentNode.Id);
        foreach (int childId in currentNode.Children)
        {
            state.ReachableNodes.Add(childId);
        }
        foreach (int keyId in currentNode.Keys)
        {
            state.OwnedKeys.Add(keyId);
        }

        int[] keysBuffer = new int[state.OwnedKeys.Count];
        state.OwnedKeys.CopyTo(keysBuffer);
        foreach(int key in keysBuffer)
        {
            if (state.ReachableNodes.TryGetValue(key, out int _))
            {
                state.CandidatesForNext.Add(key);
                state.ReachableNodes.Remove(key);
                state.OwnedKeys.Remove(key);
            }
        }
        return state;
    }
}
