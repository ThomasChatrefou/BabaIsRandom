using UnityEngine;

[CreateAssetMenu(menuName = "Procedural/Generator", fileName = "AS_ProceduralGenerator")]
public class ProceduralConfig : WorldConfig
{
    public Vector2Int NodeCountRange { get { return _nodeCountRange; } }
    public int MaxKeyCountPerNode { get { return _maxKeyCountPerNode; } }

    [SerializeField]
    private Vector2Int _nodeCountRange;
    [SerializeField]
    private int _maxKeyCountPerNode;
}
