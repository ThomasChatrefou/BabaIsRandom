using UnityEngine;

[CreateAssetMenu(menuName = "Game/GridConfig", fileName = "AS_GridConfig")]
public class GridConfig : ScriptableObject
{
    public Vector2Int CellsCount { get { return _cellsCount; } }
    public Vector2 CellSize { get { return _cellSize; } }

    #region Private

    [SerializeField]
    private Vector2Int _cellsCount;
    [SerializeField]
    private Vector2 _cellSize;

    #endregion Private
}
