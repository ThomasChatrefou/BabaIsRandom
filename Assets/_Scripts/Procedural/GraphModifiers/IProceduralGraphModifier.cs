using System.Collections.Generic;

public interface IProceduralGraphModifier
{
    public bool Check(Graph graph, NodeSelector select);
    public void Execute(ref Graph graph, NodeSelector select);
}
