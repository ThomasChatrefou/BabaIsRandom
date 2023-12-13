using System.Collections.Generic;
using UnityEngine;

public class ProceduralGameplayTranslator : WorldBehaviour, IProceduralTranslator
{
    public void Translate(List<Node> nodes)
    {
        Debug.Log($"[ProceduralGameplayTranslator] coucou");
    }
}