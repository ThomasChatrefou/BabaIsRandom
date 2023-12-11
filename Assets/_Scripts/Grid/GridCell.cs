using System;
using System.Collections.Generic;
using UnityEngine;

public class GridCell
{
    public Vector2Int Coord { get; private set; }
    public Vector3 Position { get; private set; }
    public List<GameEntity> Entities { get; private set; } = new();

    public event Action<GameEntity> OnEntityEnteredCell;

    public GridCell(Vector2Int coord, Vector3 position)
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