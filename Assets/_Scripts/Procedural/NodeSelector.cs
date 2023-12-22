using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelector
{
    public bool IsValid { get { return Selection.Count > 0; } }
    public bool HasMultiSelection { get { return Selection.Count > 1; } }
    public int LastSelectedNodeId { get { return IsValid ? Selection[^1] : Node.NULL_NODE_ID; } }
    public int LastPreselectedNodeId { get { return HasMultiSelection ? Selection[^2] : Node.NULL_NODE_ID; } }
    public List<int> Selection { get; private set; } = new();

    public void Select(int nodeId)
    {
        Selection.Clear();
        Selection.Add(nodeId);
    }

    public void MultiSelect(List<int> nodeIds)
    {
        Selection.AddRange(nodeIds);
    }

    public void MultiSelect_Add(int nodeId)
    {
        Selection.Add(nodeId);
    }

    public void MultiSelect_Remove(int nodeId)
    {
        Selection.Remove(nodeId);
    }

    public void MultiSelect_Clear()
    {
        Selection.Clear();
    }
}
