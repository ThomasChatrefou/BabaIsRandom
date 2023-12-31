using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelector : WorldBehaviour
{
    public bool IsValid { get { return _slection.Count > 0; } }
    public bool HasMultiSelection { get { return _slection.Count > 1; } }
    public int LastSelectedNodeId { get { return IsValid ? _slection[^1] : Node.NULL_NODE_ID; } }
    public int LastPreselectedNodeId { get { return HasMultiSelection ? _slection[^2] : Node.NULL_NODE_ID; } }

    public void Select(int nodeId)
    {
        _slection.Clear();
        _slection.Add(nodeId);
    }

    public void MultiSelect(List<int> nodeIds)
    {
        _slection.AddRange(nodeIds);
    }

    public void MultiSelect_Add(int nodeId)
    {
        _slection.Add(nodeId);
    }

    public void MultiSelect_Remove(int nodeId)
    {
        _slection.Remove(nodeId);
    }

    public void MultiSelect_Clear()
    {
        _slection.Clear();
    }

    #region Private

    private void OnEnable()
    {
        _nodeSelectionChannel.OnEventTrigger += Select;
    }

    private void OnDisable()
    {
        _nodeSelectionChannel.OnEventTrigger -= Select;
    }

    [SerializeField]
    [BoxGroup("Listening to")]
    private IntSenderEventChannelSO _nodeSelectionChannel;

    private List<int> _slection = new();

    #endregion Private
}
