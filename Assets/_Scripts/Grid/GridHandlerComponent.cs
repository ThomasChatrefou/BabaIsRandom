using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class GridHandlerComponent : WorldBehaviour, IConfigurableComponent
{
    #region Editor

    [Button] public void Generate() => Generate_Internal();

    public string GetConfigPropertyName()
    {
        return "_gridConfig";
    }

    #endregion Editor

    #region Private

    private void Generate_Internal()
    {
        if (_gridConfig == null)
        {
            Debug.LogWarning($"[{name}] 'Generate' method needs a grid config");
            return;
        }

        World.Grid.Setup(_gridConfig);
        World.Grid.Generate();

        transform.localScale = new Vector3(
            _gridConfig.CellsCount.x * _gridConfig.CellSize.x,
            _gridConfig.CellsCount.y * _gridConfig.CellSize.y,
            0f
            );
    }

    private void Awake()
    {
        Generate_Internal();
    }

    private void Reset()
    {
        Generate_Internal();
    }

    #region Gizmos

    private void OnDrawGizmos()
    {
        if (!World.Grid.Generated) return;

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
        for (int i = 0; i < count.y; i++)
        {
            DrawLine(new Vector2Int(0, i), new Vector2Int(count.x - 1, i), count, size, topLineSigns);
        }
        foreach (Cell cell in World.Grid.Cells)
        {
            Gizmos.DrawWireCube(cell.Position, 0.1f * new Vector3(size.x, size.y, 0f));
        }
    }

    private void DrawLine(Vector2Int startCoord, Vector2Int endCoord, Vector2Int count, Vector2 size, sbyte[,] signs)
    {
        Cell startCell = World.Grid.GetCellFromCoord(startCoord);
        Cell endCell = World.Grid.GetCellFromCoord(endCoord);

        Vector3 start = startCell.Position + 0.5f * new Vector3(signs[0, 0] * size.x, signs[0, 1] * size.y, 0f);
        Vector3 end = endCell.Position + 0.5f * new Vector3(signs[1, 0] * size.x, signs[1, 1] * size.y, 0f);
        Gizmos.DrawLine(start, end);
    }

    #endregion Gizmos

    [SerializeField]
    private GridConfig _gridConfig;

    #endregion Private
}
