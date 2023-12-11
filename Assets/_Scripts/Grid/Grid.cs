using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameGrid
{
    public bool Generated { get; private set; }
    public List<GridCell> Cells => _gridData.Cells;

    public event Action OnGenerated;

    public void Setup(GridConfig config)
    {
        _gridConfig = config;
    }

    public void Generate()
    {
        GridGenerator.Input input = new()
        {
            CellsCount = _gridConfig.CellsCount,
            CellSize = _gridConfig.CellSize,
            Offset = _gridConfig.OffsetPosition,
        };
        Generated = GridGenerator.Generate(ref _gridData, input);
        OnGenerated?.Invoke();
    }

    public void MakeDirty()
    {
        Generated = false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int GetCoordUniqueId(Vector2Int coord)
    {
        return coord.x * _gridConfig.CellsCount.y + coord.y;
    }

    public GridCell GetCellFromCoord(Vector2Int coord)
    {
        if (ThrowWarningIfNotGenerated()) return null;
        coord.Clamp(Vector2Int.zero, _gridConfig.CellsCount - Vector2Int.one);
        return GetCellFromCoord_NoCheck(coord);
    }

    public GridCell GetNearestCell(Vector3 worldPosition)
    {
        if (ThrowWarningIfNotGenerated()) return null;

        Vector3 fromAnchor = worldPosition - Cells[0].Position;

        Vector2Int gridCoord = new(
            Mathf.RoundToInt(fromAnchor.x / _gridConfig.CellSize.x),
            Mathf.RoundToInt(fromAnchor.y / _gridConfig.CellSize.y
            ));
        gridCoord.Clamp(Vector2Int.zero, _gridConfig.CellsCount - Vector2Int.one);

        return GetCellFromCoord_NoCheck(gridCoord);
    }

    #region Private

    private bool ThrowWarningIfNotGenerated()
    {
        if (!Generated) Debug.LogWarning("[GameGrid] grid is not generated");
        return !Generated;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private GridCell GetCellFromCoord_NoCheck(Vector2Int coord)
    {
        return _gridData.Cells[GetCoordUniqueId(coord)];
    }

    private GridData _gridData = null;
    private GridConfig _gridConfig;

    #endregion Private
}