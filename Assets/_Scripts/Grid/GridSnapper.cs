using UnityEngine;

[ExecuteInEditMode]
public class GridSnapper : WorldBehaviour
{
    #region Private

    private void SnapToNearestCell()
    {
        GridCell nearestCell = World.Grid.GetNearestCell(transform.position);
        if (nearestCell is not null)
        {
            transform.position = nearestCell.Position;
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
        World.Grid.OnGenerated += SnapToNearestCell;
    }

    #endregion Private
}
