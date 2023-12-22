using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(IProceduralTranslator))]
public class ProceduralHandler : WorldBehaviour, IConfigurableComponent
{
    public IProceduralTranslator Translator
    {
        get
        {
            _translator ??= GetComponent<IProceduralTranslator>();
            return _translator;
        }
    }

    #region Editor

    [Button] public void Generate() => Generate_Internal();
    [Button] public void Solve() => Solve_Internal();
    [Button] public void AddNewBranch() => TryExecute(_addNewBranchModifier);
    [Button] public void AddInBetween() => TryExecute(_addInBetweenModifier);
    [Button] public void DeleteNode() => TryExecute(_deleteNodeModifier);

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
        ProceduralGenerator.Generate(out _graph, input);

        Translator.TranslateGraph(_graph);
    }

    private void Solve_Internal()
    {
        List<string> outputPaths = new();
        ProceduralSolver.Solve(ref outputPaths, _graph);
        Translator.TranslateSolutions(outputPaths);
    }

    private void TryExecute(IProceduralGraphModifier modifier)
    {
        if (modifier.Check(_graph, _selector))
        {
            modifier.Execute(ref _graph, _selector);
            Translator.TranslateGraph(_graph);
        }
    }

    private void SelectNodes()
    {
        _selector.MultiSelect_Clear();
        List<int> nodeIds = new();
        foreach (char name in _nodeSelection)
        {
            nodeIds.Add(_graph.GetNodeIdFromAscii(name));
        }
        _selector.MultiSelect(nodeIds);
    }

    private void OnEnable()
    {
        SelectNodes();
    }

    [SerializeField]
    private ProceduralConfig _proceduralConfig;
    [SerializeField]
    private bool _useCustomSeed;
    [SerializeField]
    [EnableIf("_useCustomSeed")]
    private int _seed;
    [SerializeField]
    [OnValueChanged("SelectNodes")]
    private List<char> _nodeSelection = new();

    private IProceduralTranslator _translator;
    private Graph _graph = new(new List<Node>());
    private NodeSelector _selector = new();
    private AddInBetweenModifier _addInBetweenModifier = new();
    private AddNewBranchModifier _addNewBranchModifier = new();
    private DeleteNodeModifier _deleteNodeModifier = new();

    #endregion Private
}
