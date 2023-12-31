using System;
using System.Collections.Generic;
using UnityEngine;

public class GridData
{
    public List<Cell> Cells { get; private set; }

    public GridData(List<Cell> cells)
    {
        Cells = cells;
    }
}

public class Cell
{
    public Vector2Int Coord { get; private set; }
    public Vector3 Position { get; private set; }
    public List<GameEntity> Entities { get; private set; } = new();

    public event Action<GameEntity> OnEntityEnteredCell;

    public Cell(Vector2Int coord, Vector3 position)
    {
        Coord = coord;
        Position = position;
    }

    public void Add(GameEntity newEntity)
    {
        Entities.Add(newEntity);
        OnEntityEnteredCell?.Invoke(newEntity);
    }

    public void Remove(GameEntity Entity)
    {
        Entities.Remove(Entity);
    }
}