using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class NodeSelectorButton : MonoBehaviour
{
    [SerializeField]
    private IntSenderEventChannelSO _broadcastChannel;

    [NonSerialized]
    public int NodeId;

    public event Action<int> OnSelected;

    #region Private

    private void OnButtonClick()
    {
        _broadcastChannel.RequestRaiseEvent(NodeId);
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

    private Button _button;

    #endregion Private
}
