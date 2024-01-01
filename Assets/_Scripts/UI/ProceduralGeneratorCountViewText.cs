using System;

public class ProceduralGeneratorNodeCounter : ICounter
{
    public event Action<int> OnCountUpdated;

    public ProceduralGeneratorNodeCounter()
    {
        ProceduralGenerator.Generated += OnGenerated;
    }

    ~ProceduralGeneratorNodeCounter()
    {
        ProceduralGenerator.Generated -= OnGenerated;
    }

    private void OnGenerated(Graph graph)
    {
        OnCountUpdated?.Invoke(graph.Nodes.Count);
    }
}

public class ProceduralGeneratorCountViewText : CountViewText<ProceduralGeneratorNodeCounter>
{
    private new void OnEnable()
    {
        _counter = new ProceduralGeneratorNodeCounter();
        base.OnEnable();
    }
}
