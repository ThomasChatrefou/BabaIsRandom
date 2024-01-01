using System;
using System.Collections.Generic;
using UnityEngine;

public class ProceduralSolver
{
    public struct State
    {
        public HashSet<int> ReachableNodes;
        public List<int> OwnedKeys;
        public List<int> NodesToVisit;
        public string Path;

        public State(State other)
        {
            ReachableNodes = new(other.ReachableNodes);
            OwnedKeys = new(other.OwnedKeys);
            NodesToVisit = new(other.NodesToVisit);
            Path = new(other.Path);
        }
    }

    public static event Action<int> Solved;

    public static void Solve(ref List<string> outputPaths, Graph graph)
    {

        Debug.Log("[ProceduralSolver] Solving graph...");
        State initState = new()
        {
            ReachableNodes = new(),
            OwnedKeys = new(),
            NodesToVisit = new() { 0 },
            Path = ""
        };
        Solve_Internal(ref outputPaths, initState, graph);
        Solved?.Invoke(outputPaths.Count);
        Debug.Log($"[ProceduralSolver] {outputPaths.Count} paths found !");
    }

    #region Private

    private static void Solve_Internal(ref List<string> outputs, State state, Graph graph)
    {
        if (state.NodesToVisit.Count > 0)
        {
            foreach (int nodeId in state.NodesToVisit)
            {
                State newState = ComputeNextState(new State(state), graph.GetNodeFromId(nodeId));
                Solve_Internal(ref outputs, newState, graph);
            }
        }
        else
        {
            outputs.Add(state.Path);
        }
    }

    private static State ComputeNextState(State state, Node currentNode)
    {
        state.Path += currentNode.AsciiName;
        state.NodesToVisit.Remove(currentNode.Id);
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
        foreach (int key in keysBuffer)
        {
            if (state.ReachableNodes.TryGetValue(key, out int _))
            {
                state.NodesToVisit.Add(key);
                state.ReachableNodes.Remove(key);
                state.OwnedKeys.Remove(key);
            }
        }
        return state;
    }

    #endregion Private
}
