using System;

public class ProceduralSolverNodeCounter : ICounter
{
    public event Action<int> OnCountUpdated;

    public ProceduralSolverNodeCounter()
    {
        ProceduralSolver.Solved += OnSolved;
    }

    ~ProceduralSolverNodeCounter()
    {
        ProceduralSolver.Solved -= OnSolved;
    }

    private void OnSolved(int pathsCount)
    {
        OnCountUpdated?.Invoke(pathsCount);
    }
}

public class ProceduralSolverCountViewText : CountViewText<ProceduralSolverNodeCounter>
{
    private new void OnEnable()
    {
        _counter = new ProceduralSolverNodeCounter();
        base.OnEnable();
    }
}