using System;
using System.Collections.Generic;
using UnityEngine;

// Must be a WorldB (MonoB) to be a component
public class ProceduralDebugTranslator : WorldBehaviour, IProceduralTranslator
{
    public event Action<List<string>> OnGraphTranslated;
    public event Action<List<string>> OnSolutionTranslated;

    public void TranslateGraph(List<Node> nodes)
    {
        Debug.Log($"[ProceduralDebugTranslator] translating graph...");
        List<string> outputLogs = new();
        foreach (Node node in nodes)
        {
            string childrenStr = ConvertIndexesToNames(node.Children, nodes);
            string keysStr = ConvertIndexesToNames(node.Keys, nodes);
            outputLogs.Add($"[Node {node.AsciiName}] : Children nodes : [" + childrenStr + "]; Keys : [" + keysStr + "]. ");
            Debug.Log(outputLogs[^1]);
        }
        OnGraphTranslated?.Invoke(outputLogs);
    }

    public void TranslateSolutions(List<string> paths)
    {
        Debug.Log($"[ProceduralDebugTranslator] translating solutions...");

        foreach (string path in paths)
        {
            Debug.Log(path);
        }
        OnSolutionTranslated?.Invoke(paths);
    }

    #region Private

    private string ConvertIndexesToNames(List<int> input, List<Node> nodes)
    {
        List<string> strings = new();
        foreach (int element in input)
        {
            strings.Add(nodes[element].AsciiName.ToString());
        }
        return string.Join(", ", strings);
    }

    #endregion Private
}
