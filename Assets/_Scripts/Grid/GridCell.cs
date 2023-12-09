using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector2Int Coord { get { return _coord; } }
    public Vector3 Position { get { return _position; } }

    public GridCell(Vector2Int coord, Vector3 position)
    {
        _coord = coord;
        _position = position;
    }

    #region Private

    private Vector2Int _coord;
    private Vector3 _position;
    private List<GridItem> _items = new();

    #endregion Private
}