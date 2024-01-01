using System;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NodeSelectorButton : WorldBehaviour
{
    [NonSerialized]
    public int NodeId;

    #region Private

    private void OnButtonClick()
    {
        if (World.Player.IsMultiSelecting)
        {
            _nodeMultiSelectionChannel.RequestRaiseEvent(NodeId);
        }
        else
        {
            _nodeSelectionChannel.RequestRaiseEvent(NodeId);
        }
    }

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    [SerializeField]
    [BoxGroup("Broadcasting on")]
    private IntSenderEventChannelSO _nodeSelectionChannel;
    [SerializeField]
    [BoxGroup("Broadcasting on")]
    private IntSenderEventChannelSO _nodeMultiSelectionChannel;

    private Button _button;

    #endregion Private
}
