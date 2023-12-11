using UnityEngine;

public interface IEntityStateMachine
{
    public GameEntity Entity { get; }
    public void SetState(IActionState state);
}

public interface IActionState
{
    public void OnPlayerMove(IEntityStateMachine context, Vector2Int direction);
    public bool OnTryEnterCell(IEntityStateMachine context, GridCell newCell);
    public void OnEntityEnteredCell(IEntityStateMachine context, GameEntity enteringEntity);
}

public class ActionStateMachine : WorldEntity, IEntityStateMachine
{
    public GameEntity Entity { get; private set; }
    public IActionState CurrentState { get; private set; }

    public void SetState(IActionState newState)
    {
        CurrentState = newState;
    }

    public ActionStateMachine(GameEntity owner)
    {
        Entity = owner;
        Entity.Cell.OnEntityEnteredCell += OnEntityEnteredCell;
        if (World.CheckPlayer()) World.Player.OnMove += OnPlayerMove;
    }

    ~ActionStateMachine()
    {
        Entity.Cell.OnEntityEnteredCell -= OnEntityEnteredCell;
        if (World.CheckPlayer()) World.Player.OnMove -= OnPlayerMove;
    }

    public void UpdateCellCallback(GridCell newCell)
    {
        Entity.Cell.OnEntityEnteredCell -= OnEntityEnteredCell;
        newCell.OnEntityEnteredCell += OnEntityEnteredCell;
    }

    public void OnPlayerMove(Vector2Int direction) => CurrentState.OnPlayerMove(this, direction);
    public bool OnTryEnterCell(GridCell newCell) => CurrentState.OnTryEnterCell(this, newCell);
    public void OnEntityEnteredCell(GameEntity enteringEntity) => CurrentState.OnEntityEnteredCell(this, enteringEntity);
}
