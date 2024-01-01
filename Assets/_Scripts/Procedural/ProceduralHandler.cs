using NaughtyAttributes;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(IProceduralTranslator))]
[RequireComponent(typeof(NodeSelector))]
public class ProceduralHandler : WorldBehaviour, IConfigurableComponent
{
    public bool UseCustomSeed { get; set; }
    public int Seed { get { return _seed; } }
    public ProceduralGenerator.Input ProGenInput { get { return _generatorInput; } }
    public IProceduralTranslator Translator
    {
        get
        {
            _translator ??= GetComponent<IProceduralTranslator>();
            return _translator;
        }
    }

    public void SetNodesCount_Clamped(ref int count)
    {
        int clampedByConfig = Mathf.Clamp(count, _proceduralConfig.NodesCountMin, _proceduralConfig.NodesCountMax);
        _generatorInput.NodesCountMin = clampedByConfig;
        _generatorInput.NodesCountMax = clampedByConfig;
        count = clampedByConfig;
    }

    public void SetSeed_Clamped(ref int seed)
    {
        ProceduralGenerator.FindNearestValidSeed(ref seed);
        _seed = seed;
    }

    public void ResetGeneratorInput()
    {
        _generatorInput = new ProceduralGenerator.Input()
        {
            NodesCountMin = _proceduralConfig.NodesCountMin,
            NodesCountMax = _proceduralConfig.NodesCountMax,
            MaxKeyCountPerNode = _proceduralConfig.MaxKeyCountPerNode,
        };
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

        if (!UseCustomSeed) _seed = ProceduralGenerator.GenerateSeed();
        _generatorInput.Seed = _seed;
    
        ProceduralGenerator.Generate(out _graph, _generatorInput);

        Translator.TranslateGraph(_graph);
    }

    private void Solve_Internal()
    {
        if (!_graph.IsValid) return;
        if (_graph.Nodes.Count > _proceduralConfig.NodesCountMax) return;
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
        if (TryGetComponent(out NodeSelector selector))
        {
            _selector = selector;
            SelectNodes();
        }
        ResetGeneratorInput();

        Debug.Log(this.gameObject);
    }

    [SerializeField]
    private ProceduralConfig _proceduralConfig;
    [SerializeField]
    [OnValueChanged("SelectNodes")]
    private List<char> _nodeSelection = new();

    private int _seed;
    private ProceduralGenerator.Input _generatorInput;
    private IProceduralTranslator _translator;
    private Graph _graph = new(new List<Node>());
    private NodeSelector _selector;
    private AddInBetweenModifier _addInBetweenModifier = new();
    private AddNewBranchModifier _addNewBranchModifier = new();
    private DeleteNodeModifier _deleteNodeModifier = new();

    #endregion Private
}
