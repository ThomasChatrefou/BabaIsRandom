using UnityEngine;

public class WinActionState : IActionState
{
    public void OnPlayerMove(IEntityStateMachine context, Vector2Int direction)
    {
    }

    public bool OnTryEnterCell(IEntityStateMachine context, GridCell newCell)
    {
        return true;
    }

    public void OnEntityEnteredCell(IEntityStateMachine context, GameEntity enteringEntity)
    {
    }
}