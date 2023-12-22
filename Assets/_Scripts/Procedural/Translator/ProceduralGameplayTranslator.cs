using System.Collections.Generic;
using UnityEngine;

public class ProceduralGameplayTranslator : WorldBehaviour, IProceduralTranslator
{
    public void TranslateGraph(Graph graph)
    {
        Debug.Log($"[ProceduralGameplayTranslator] translating graph...");
    }

    public void TranslateSolutions(List<string> paths)
    {
        Debug.Log($"[ProceduralGameplayTranslator] translating solutions...");
    }
}