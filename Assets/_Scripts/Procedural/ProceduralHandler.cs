using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(IProceduralTranslator))]
public class ProceduralHandler : WorldBehaviour, IConfigurableComponent
{
    #region Editor

    [Button] public void Generate() => Generate_Internal();
    [Button] public void Solve() => Solve_Internal();

    public string GetConfigPropertyName()
    {
        return "_proceduralConfig";
    }

    #endregion Editor

    #region Private

    private void Generate_Internal()
    {
        if (_proceduralConfig == null)
        {
            Debug.LogWarning($"[{name}] 'Generate' method needs a procedural config");
            return;
        }

        if (!_useCustomSeed) _seed = ProceduralGenerator.GenerateSeed();

        ProceduralGenerator.Input input = new()
        {
            NodeCountRange = _proceduralConfig.NodeCountRange,
            MaxKeyCountPerNode = _proceduralConfig.MaxKeyCountPerNode,
            Seed = _seed,
        };
        ProceduralGenerator.Generate(ref _nodes, input);

        _translator ??= GetComponent<IProceduralTranslator>();
        _translator.TranslateGraph(_nodes);
    }

    private void Solve_Internal()
    {
        List<string> outputPaths = new();
        ProceduralSolver.Solve(ref outputPaths, _nodes);
        _translator ??= GetComponent<IProceduralTranslator>();
        _translator.TranslateSolutions(outputPaths);
    }
    
    [SerializeField] 
    private ProceduralConfig _proceduralConfig;
    [SerializeField]
    private bool _useCustomSeed;
    [EnableIf("_useCustomSeed")]
    [SerializeField]
    private int _seed;

    private IProceduralTranslator _translator;
    private List<Node> _nodes = new();

    #endregion Private
}
