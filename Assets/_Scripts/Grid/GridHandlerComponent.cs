using System;
using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class GridHandlerComponent : MonoBehaviour
{
    #region Private

    [Button]
    private void Generate()
    {
        if (_gridConfig == null)
        {
            Debug.LogWarning($"[{name}] 'Generate' method needs a grid config");
            return;
        }

        _gameGrid = new(_gridConfig, _anchor.position);
        _gameGrid.Generate();
    }

    private void OnDrawGizmos()
    {
        if (_gameGrid == null) return;
        if (!_gameGrid.Generated) return;

        Gizmos.color = Color.red;

        Vector2Int count = _gridConfig.CellsCount;
        Vector2 size = _gridConfig.CellSize;

        sbyte[,] leftLineSigns = { { -1, -1 }, { -1, 1 } };
        sbyte[,] botLineSigns = { { -1, -1 }, { 1, -1 } };
        sbyte[,] rightLineSigns = { { 1, -1 }, { 1, 1 } };
        sbyte[,] topLineSigns = { { -1, 1 }, { 1, 1 } };

        DrawLine(new Vector2Int(0, 0), new Vector2Int(0, count.y - 1), count, size, leftLineSigns);
        DrawLine(new Vector2Int(0, 0), new Vector2Int(count.x - 1, 0), count, size, botLineSigns);
        for (int i = 0; i < count.x; i++)
        {
            DrawLine(new Vector2Int(i, 0), new Vector2Int(i, count.y - 1), count, size, rightLineSigns);
        }
        for (int i = 0; i < count.x; i++)
        {
            DrawLine(new Vector2Int(0, i), new Vector2Int(count.x - 1, i), count, size, topLineSigns);
        }
    }

    private void DrawLine(Vector2Int startCoord, Vector2Int endCoord, Vector2Int count, Vector2 size, sbyte[,] signs)
    {
        GridCell startCell = _gameGrid.GetCellFromCoord(startCoord);
        GridCell endCell = _gameGrid.GetCellFromCoord(endCoord);

        Vector3 start = startCell.Position + 0.5f * new Vector3(signs[0, 0] * size.x, signs[0, 1] * size.y, 0f);
        Vector3 end = endCell.Position + 0.5f * new Vector3(signs[1, 0] * size.x, signs[1, 1] * size.y, 0f);
        Gizmos.DrawLine(start, end);
    }


    [SerializeField]
    private GridConfig _gridConfig;
    [SerializeField]
    private Transform _anchor;

    [NonSerialized]
    private GameGrid _gameGrid = null;

    #endregion Private
}
