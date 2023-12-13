using System;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(IProceduralTranslator))]
public class ProceduralHandler : WorldBehaviour
{
    [Button]
    private void Generate()
    {
        if (_proceduralConfig == null)
        {
            Debug.LogWarning($"[{name}] 'Generate' method needs a procedural config");
            return;
        }

        _generator.Setup(_proceduralConfig);
        if (!_useCustomSeed) _seed = _generator.GenerateSeed();
        _generator.Generate(_seed);
        PrintNodes();
    }

    [Button]
    private void Translate()
    {
        _translator = GetComponent<IProceduralTranslator>();
        _translator.Translate(_generator.Nodes);
    }

    [Button]
    private void PrintNodes()
    {
        Debug.Log($"Nb nodes generated : {_generator.Nodes.Count}");
        foreach(Node node in _generator.Nodes)
        {
            string childrenStr = ListToString(node.Children, _generator.Nodes);
            string keysStr = ListToString(node.Keys, _generator.Nodes);

            Debug.Log($"[Node {node.AsciiChar}] : Children nodes : [" + childrenStr + "]; Keys : [" + keysStr + "]. ");
        }
    }

    private string ListToString(List<int> input, List<Node> nodes)
    {
        List<string> strings = new();
        foreach (int element in input)
        {
            strings.Add(nodes[element].AsciiChar.ToString());
        }
        return string.Join(", ", strings);
    }
    
    [SerializeField] 
    private ProceduralConfig _proceduralConfig;
    [SerializeField]
    private IProceduralTranslator _translator;

    [SerializeField]
    private bool _useCustomSeed;
    [EnableIf("_useCustomSeed")]
    [SerializeField]
    private int _seed;

    [NonSerialized]
    private ProceduralGenerator _generator = new();
}
