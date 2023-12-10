using NaughtyAttributes;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/GridConfig", fileName = "AS_GridConfig")]
public class GridConfig : WorldConfig
{
    public Vector2Int CellsCount { get { return _cellsCount; } }
    public Vector2 CellSize { get { return _cellSize; } }
    public Vector2 OffsetPosition { get { return _offsetPosition; } }

    #region Private

    private void DirtyGrid()
    {
        World.Grid.MakeDirty();
    }

    [SerializeField]
    [OnValueChanged("DirtyGrid")]
    private Vector2Int _cellsCount;
    [SerializeField]
    [OnValueChanged("DirtyGrid")]
    private Vector2 _cellSize;
    [SerializeField]
    [OnValueChanged("DirtyGrid")]
    private Vector2 _offsetPosition;

    #endregion Private
}
