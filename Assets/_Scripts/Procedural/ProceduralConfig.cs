using UnityEngine;

[CreateAssetMenu(menuName = "Procedural/Generator", fileName = "AS_ProceduralGenerator")]
public class ProceduralConfig : WorldConfig
{
    public int NodesCountMin { get { return _nodesCountRange.x; } }
    public int NodesCountMax { get { return _nodesCountRange.y; } }
    public int MaxKeyCountPerNode { get { return _maxKeyCountPerNode; } }

    #region Private

    [SerializeField]
    private Vector2Int _nodesCountRange;
    [SerializeField]
    private int _maxKeyCountPerNode;

    #endregion Private
}
