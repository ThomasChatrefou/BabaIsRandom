using System;
using NaughtyAttributes;
using UnityEngine;

public class GameEntityHandler : WorldBehaviour
{
    [Button]
    public void SetIsYou() => _entity.SetIsYou();

    [Button]
    public void BreakIsYou() => _entity.SetIsEmpty();

    #region Private 

    private void UpdateTransform(Vector3 newPosition)
    {
        transform.position = newPosition;
        _gameEntityCoord = _entity.Cell.Coord;
    }

    private void OnEnable()
    {
        // maybe we should make a true spawner instead but I don't see why for now
        _entity = new GameEntity(transform.position);
        _entity.OnMove += UpdateTransform;
        SetupDefaultState();
    }

    private void OnDisable()
    {
        _entity.OnMove -= UpdateTransform;
        _entity = null;
    }

    private GameEntity _entity;

    // Temp for debug
    [Serializable]
    private enum State
    {
        Empty,
        You,
        Push,
        Stop,
        Win,
        Defeat
    }

    private void SetupDefaultState()
    {
        switch(_defaultState)
        {
            case State.Empty: _entity.SetIsEmpty(); break;
            case State.You: _entity.SetIsYou(); break;
            case State.Push: _entity.SetIsPush(); break;
            case State.Stop: _entity.SetIsStop(); break;
            case State.Win: _entity.SetIsWin(); break;
            case State.Defeat: _entity.SetIsDefeat(); break;
            default: break;
        }
    }

    [SerializeField] private State _defaultState = State.Empty;

    [ShowNonSerializedField]
    private Vector2Int _gameEntityCoord;

    #endregion Private
}
