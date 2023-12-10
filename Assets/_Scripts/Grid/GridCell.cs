using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector2Int Coord { get; private set; }
    public Vector3 Position { get; private set; }

    public GridCell(Vector2Int coord, Vector3 position)
    {
        Coord = coord;
        Position = position;
    }

    #region Private

    private List<GridItem> _items = new();

    #endregion Private
}