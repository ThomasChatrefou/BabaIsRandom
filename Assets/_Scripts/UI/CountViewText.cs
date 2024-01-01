using System;
using TMPro;
using UnityEngine;

public interface ICounter
{
    public event Action<int> OnCountUpdated;
}

[RequireComponent(typeof(TMP_Text))]
public class CountViewText<T> : MonoBehaviour where T : ICounter
{
    #region Private

    private void OnCountUpdated(int newValue)
    {
        _text.text = _initialText + newValue.ToString();
    }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        _initialText = _text.text;
    }

    protected void OnEnable()
    {
        _counter.OnCountUpdated += OnCountUpdated;
    }

    private void OnDisable()
    {
        _counter.OnCountUpdated -= OnCountUpdated;
    }

    protected T _counter;

    private string _initialText;
    private TMP_Text _text;

    #endregion Private
}