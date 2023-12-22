using System.Collections.Generic;

public interface IProceduralTranslator
{
    public void TranslateGraph(Graph graph);
    public void TranslateSolutions(List<string> paths);
}
