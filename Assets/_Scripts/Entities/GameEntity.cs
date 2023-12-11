using System;
using UnityEngine;

public class GameEntity : WorldEntity
{
    public GridCell Cell { get; private set; }
    public ActionStateMachine Context { get; private set; }
    public IActionState State => Context.CurrentState;

    public event Action<Vector3> OnMove;

    public GameEntity(Vector3 position)
    {
        Cell = World.Grid.GetNearestCell(position);
        Cell.Add(this);
        Context = new ActionStateMachine(this);
    }

    // Temp
    public void SetIsEmpty() => Context.SetState(new EmptyActionState());
    public void SetIsYou() => Context.SetState(new YouActionState());
    public void SetIsPush() => Context.SetState(new PushActionState());
    public void SetIsStop() => Context.SetState(new StopActionState());
    public void SetIsWin() => Context.SetState(new WinActionState());
    public void SetIsDefeat() => Context.SetState(new DefeatActionState());

    public bool TryMoveTo(GridCell newCell)
    {
        if (Context.OnTryEnterCell(newCell))
        {
            Cell.Remove(this);
            newCell.Add(this);
            Context.UpdateCellCallback(newCell);

            Cell = newCell;
            OnMove?.Invoke(Cell.Position);

            return true;
        }
        return false;
    }
}
