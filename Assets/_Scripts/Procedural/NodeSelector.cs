using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

public class NodeSelector : WorldBehaviour
{
    public bool IsValid { get { return _selection.Count > 0; } }
    public bool HasMultiSelection { get { return _selection.Count > 1; } }
    public int LastSelectedNodeId { get { return IsValid ? _selection[^1] : Node.NULL_NODE_ID; } }
    public int LastPreSelectedNodeId { get { return HasMultiSelection ? _selection[^2] : Node.NULL_NODE_ID; } }

    public void Select(int nodeId)
    {
        _selection.Clear();
        _selection.Add(nodeId);
    }

    public void MultiSelect(List<int> nodeIds)
    {
        _selection.AddRange(nodeIds);
    }

    public void MultiSelect_AddOrRemove(int nodeId)
    {
        if (_selection.Contains(nodeId))
        {
            _selection.Remove(nodeId);
        }
        else
        {
            _selection.Add(nodeId);
        }
    }

    public void MultiSelect_Clear()
    {
        _selection.Clear();
    }

    #region Private

    private void OnEnable()
    {
        if (_nodeSelectionChannel == null || _nodeMultiSelectionChannel == null) return;
        _nodeSelectionChannel.OnEventTrigger += Select;
        _nodeMultiSelectionChannel.OnEventTrigger += MultiSelect_AddOrRemove;
    }

    private void OnDisable()
    {
        if (_nodeSelectionChannel == null || _nodeMultiSelectionChannel == null) return;
        _nodeSelectionChannel.OnEventTrigger -= Select;
        _nodeMultiSelectionChannel.OnEventTrigger -= MultiSelect_AddOrRemove;
    }

    [SerializeField]
    [BoxGroup("Listening to")]
    private IntSenderEventChannelSO _nodeSelectionChannel;
    [SerializeField]
    [BoxGroup("Listening to")]
    private IntSenderEventChannelSO _nodeMultiSelectionChannel;

    private List<int> _selection = new();

    #endregion Private
}
