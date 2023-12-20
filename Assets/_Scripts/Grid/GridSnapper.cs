using NaughtyAttributes;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnapper : WorldBehaviour
{
    #region Private

    private void SnapToNearestCell()
    {
        Cell nearestCell = World.Grid.GetNearestCell(transform.position);
        if (nearestCell is not null)
        {
            transform.position = nearestCell.Position;
            _currentCoordinate = nearestCell.Coord;
        }
        transform.hasChanged = false;
    }

    private void OnEnable()
    {
        World.Grid.OnGenerated += SnapToNearestCell;
    }

    private void Update()
    {
        if (transform.hasChanged)
        {
            SnapToNearestCell();
        }
    }

    private void OnDisable()
    {
        World.Grid.OnGenerated -= SnapToNearestCell;
    }

    [ShowNonSerializedField]
    private Vector2Int _currentCoordinate;

    #endregion Private
}
