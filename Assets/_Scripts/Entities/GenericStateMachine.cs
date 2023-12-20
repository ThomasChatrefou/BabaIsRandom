using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericStateMachine<StateClass>
{
    public GameEntity Owner { get; private set; }
    public HashSet<StateClass> CurrentStates { get; private set; } = new();

    public void PushState(StateClass newState)
    {
        CurrentStates.Add(newState);
    }


    public void PopState(StateClass newState)
    {
        CurrentStates.Remove(newState);
    }

}
