using UnityEngine;

public class PushActionState : IActionState
{
    public void OnPlayerMove(IEntityStateMachine context, Vector2Int direction)
    {
    }

    // copy pasted from YouActionState, some refactoring needed
    public bool OnTryEnterCell(IEntityStateMachine context, GridCell newCell)
    {
        bool canEnter = true;
        foreach (GameEntity entity in newCell.Entities)
        {
            if (entity.State is PushActionState push)
            {
                Vector2Int nextCoord = 2 * entity.Cell.Coord - context.Entity.Cell.Coord;
                GridCell nextCell = context.Entity.World.Grid.GetCellFromCoord(nextCoord);
                if (nextCell == entity.Cell)
                {
                    canEnter = false;
                    break;
                }
                else
                {
                    canEnter &= push.OnTryEnterCell(entity.Context, nextCell);
                }
            }
            else if (entity.State is StopActionState)
            {
                canEnter = false;
                break;
            }
        }
        // Should do somthing special when there is an entity in Defeat state
        return canEnter;
    }

    public void OnEntityEnteredCell(IEntityStateMachine context, GameEntity enteringEntity)
    {
        Vector2Int nextCoord = 2 * context.Entity.Cell.Coord - enteringEntity.Cell.Coord;
        GridCell nextCell = context.Entity.World.Grid.GetCellFromCoord(nextCoord);

        // No need for check because we alreay now it can move
        // maybe this shows that something is wrong with the current implem
        context.Entity.TryMoveTo(nextCell);
    }
}