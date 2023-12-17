using UnityEngine;

public class YouActionState : IActionState
{
    public void OnPlayerMove(IEntityStateMachine context, Vector2Int direction)
    {
        Cell newCell = context.Entity.World.Grid.GetCellFromCoord(context.Entity.Cell.Coord + direction);
        if (newCell == context.Entity.Cell) return;
        context.Entity.TryMoveTo(newCell);
    }

    public bool OnTryEnterCell(IEntityStateMachine context, Cell newCell)
    {
        bool canEnter = true;
        foreach (GameEntity entity in newCell.Entities)
        {
            // Feels weird than this code is here, should be in PushActionState somehow
            if (entity.State is PushActionState push)
            {
                Vector2Int nextCoord = 2 * entity.Cell.Coord - context.Entity.Cell.Coord;
                Cell nextCell = context.Entity.World.Grid.GetCellFromCoord(nextCoord);
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
    }
}