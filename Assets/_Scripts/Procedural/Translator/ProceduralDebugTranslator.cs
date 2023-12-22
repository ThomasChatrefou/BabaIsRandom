using System;
using System.Collections.Generic;
using UnityEngine;

// Must be a WorldB (MonoB) to be a component
public class ProceduralDebugTranslator : WorldBehaviour, IProceduralTranslator
{
    public event Action<List<string>> OnGraphTranslated;
    public event Action<List<string>> OnSolutionTranslated;

    public void TranslateGraph(Graph graph)
    {
        Debug.Log($"[ProceduralDebugTranslator] translating graph...");
        List<string> outputLogs = new();
        foreach (KeyValuePair<int, Node> pair in graph.Nodes)
        {
            Node node = pair.Value;
            char parentName = graph.IsRootNode(node.Id) ? '-' : graph.FindParentNode(node).AsciiName;
            string childrenStr = ConvertIndexesToNames(node.Children, graph);
            string keysStr = ConvertIndexesToNames(node.Keys, graph);
            outputLogs.Add($"[Node {node.AsciiName}] Parent : {parentName} ; Children nodes : [" + childrenStr + "] ; Keys : [" + keysStr + "]. ");
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

    private string ConvertIndexesToNames(List<int> input, Graph graph)
    {
        List<string> strings = new();
        foreach (int element in input)
        {
            strings.Add(graph.GetNodeFromId(element).AsciiName.ToString());
        }
        return string.Join(", ", strings);
    }

    #endregion Private
}
