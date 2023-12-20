using System.Collections.Generic;

public interface IProceduralTranslator
{
    public void TranslateGraph(List<Node> nodes);
    public void TranslateSolutions(List<string> paths);
}
