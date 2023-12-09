using Unity.VisualScripting;
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    [SerializeField] private GameObject visualCellPrefab;
    [SerializeField] private Grid grid;

    private Vector2Int lastInstanciatedCellCoord = new(-1, -1);
    public void ShowNearestCellToPosition(Vector3 worldPosition)
    {
        Cell nearestCell = grid.GetNearestCell(worldPosition);

        if (nearestCell.gridCoord != lastInstanciatedCellCoord)
        {
            GameObject visualCell = Instantiate(visualCellPrefab, nearestCell.worldPosition, Quaternion.identity, transform);
            visualCell.transform.localPosition += 1e-5f * Vector3.back; // small offset to avoid Z clipping
            visualCell.transform.localScale = new Vector3(grid.GetCellSizeX(), grid.GetCellSizeY(), 1f);
        }
    }
}
