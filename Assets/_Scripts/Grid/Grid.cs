using UnityEngine;

public class GameGrid
{
    public bool Generated { get { return _generated; } }

    public GameGrid(GridConfig config, Vector3 anchorPosition)
    {
        _gridConfig = config;
        _anchorPosition = anchorPosition != null ? anchorPosition : Vector3.zero;
    }

    public void Generate()
    {
        GridGenerator.Input input = new GridGenerator.Input()
        {
            cellsCount = _gridConfig.CellsCount,
            cellSize = _gridConfig.CellSize,
            anchorPosition = _anchorPosition,
        };
        _generated = GridGenerator.Generate(ref _gridData, input);
    }

    public int GetCoordUniqueId(Vector2Int coord)
    {
        return coord.x * _gridConfig.CellsCount.x + coord.y;
    }

    public GridCell GetCellFromCoord(Vector2Int coord)
    {
        if (_generated)
        {
            return _gridData.Cells[GetCoordUniqueId(coord)];
        }
        Debug.LogWarning("[GameGrid] grid is not generated");
        return null;
    }

    #region Private

    private bool _generated = false;
    private GridData _gridData = null;

    private readonly Vector3 _anchorPosition;
    private readonly GridConfig _gridConfig;

    #endregion Private
}



// TO DELETE
//public class CellOccupation
//{
//    public enum EOccupierType
//    {
//        NONE,
//        PLANT,
//        OBSTACLE
//    }

//    public EOccupierType occupierType;
//    public float lastModificationTime;

//    public CellOccupation(float _lastModificationTime)
//    {
//        occupierType = EOccupierType.NONE;
//        lastModificationTime = _lastModificationTime;
//    }
//}

//public class Cell
//{
//    public Vector2Int gridCoord;
//    public Vector3 worldPosition;
//    public CellOccupation occupation;
//    public GameObject obstacleReference;
//    public Cell(Vector2Int _gridCoord, Vector3 _worldPosition, CellOccupation _occupation, GameObject _obstacleReference)
//    {
//        this.gridCoord = _gridCoord;
//        this.worldPosition = _worldPosition;
//        this.occupation = _occupation;
//        this.obstacleReference = _obstacleReference;
//    }

//    public void SetObstacleReference(GameObject _obstacleReference)
//    {
//        obstacleReference = _obstacleReference;
//    }
//}

//public struct Lane
//{
//    public int id;
//    public Vector3 start;
//    public Vector3 end;
//}

//[ExecuteInEditMode]
//public class Grid : MonoBehaviour
//{
//    [SerializeField] private Transform anchor;
//    [SerializeField] private int nbCellsX;
//    [SerializeField] private int nbCellsY;
//    [SerializeField] private int NumberOfColumnBackGarden;
//    [SerializeField] private int NumberOfColumnFrontGarden;

//    [OnValueChanged("Generate")]
//    [SerializeField] private float cellSizeX;

//    [OnValueChanged("Generate")]
//    [SerializeField] private float cellSizeY;

//    private Dictionary<Vector2Int, Cell> cellsHashset;
//    private Dictionary<int, Lane> lanesHashset;

//    [SerializeField] float laneOverflowDistance;

//    private bool bGenerated = false;

//    //[SerializeField] GameObject cellVisualizerPrefab;

//    public event Action Generated;

//    private void Start()
//    {
//        Generate();
//    }

//    [Button]
//    public void Generate()
//    {
//        cellsHashset = new Dictionary<Vector2Int, Cell>();

//        for (int i = 0; i < nbCellsX; i++)
//        {
//            for (int j = 0; j < nbCellsY; j++)
//            {
//                Cell currentCell = new Cell(new Vector2Int(i, j), anchor.position + new Vector3(cellSizeX * i, cellSizeY * j, 0f), new CellOccupation(Time.time), null);

//                cellsHashset.Add(currentCell.gridCoord, currentCell); // grid coord should have unique id in UInt16 so data would not be duplicated
//            }
//        }

//        lanesHashset = new Dictionary<int, Lane>();
//        for (int j = 0; j < nbCellsY; j++)
//        {
//            Lane currentLane;
//            currentLane.id = j;

//            Vector3 overflow = laneOverflowDistance * Vector3.right;
//            currentLane.start = cellsHashset[new Vector2Int(nbCellsX - 1, j)].worldPosition + overflow;
//            currentLane.end = cellsHashset[new Vector2Int(0, j)].worldPosition - overflow;

//            lanesHashset.Add(j, currentLane);
//        }
//        bGenerated = true;
//        Generated?.Invoke();
//    }

//    public List<Cell> GetNeighbourgCells(Cell _cell)
//    {
//        var cells = new List<Cell>();
//        int x = _cell.gridCoord.x;
//        int y = _cell.gridCoord.y;
//        if (x - 1 >= 0 && y - 1 >= 0)
//        {
//            cells.Add(GetCell(x - 1, y - 1));
//        }
//        if (x - 1 >= 0)
//        {
//            cells.Add(GetCell(x - 1, y));
//        }
//        if (x - 1 >= 0 && y + 1 <= 4 )
//        {
//            cells.Add(GetCell(x - 1, y + 1));
//        }
//        if (y - 1 >= 0)
//        {
//            cells.Add(GetCell(x, y - 1));
//        }
//        if (y + 1 <= 4)
//        {
//            cells.Add(GetCell(x, y + 1));
//        }
//        if (x + 1 <= 7 && y - 1 >= 0)
//        {
//            cells.Add(GetCell(x + 1, y - 1));
//        }
//        if (x + 1 <= 7)
//        {
//            cells.Add(GetCell(x + 1, y));
//        }
//        if (x + 1 <= 7 && y + 1 <= 4)
//        {
//            cells.Add(GetCell(x + 1, y + 1));
//        }

//       return cells;
//    }
//    public List<Cell> GetCellsOfOccupiedType(CellOccupation.EOccupierType _type)
//    {
//        List<Cell> returnCellsTypeNONE = new List<Cell>();
//        List<Cell> returnCellsTypePLANT = new List<Cell>();
//        List<Cell> returnCellsTypeOBSTACLE = new List<Cell>();

//        foreach (Cell cell in cellsHashset.Values)
//        {
//            if (cell.occupation.occupierType == CellOccupation.EOccupierType.NONE)
//            {
//                returnCellsTypeNONE.Add(cell);
//            }
//            if (cell.occupation.occupierType == CellOccupation.EOccupierType.PLANT)
//            {
//                returnCellsTypePLANT.Add(cell);
//            }
//            if (cell.occupation.occupierType == CellOccupation.EOccupierType.OBSTACLE)
//            {
//                returnCellsTypeOBSTACLE.Add(cell);
//            }
//        }

//        switch (_type) 
//        {
//            case CellOccupation.EOccupierType.NONE:
//                return returnCellsTypeNONE;
//            case CellOccupation.EOccupierType.PLANT:
//                return returnCellsTypePLANT;
//            case CellOccupation.EOccupierType.OBSTACLE:
//                return returnCellsTypeOBSTACLE;
//        }
//        return new List<Cell>();
//    }
//    public Cell GetCell(int x, int y)
//    {
//        return cellsHashset[new Vector2Int(x, y)];
//    }

//    public void DestroyObstacleOnCell(Cell cell)
//    {
//        Destroy(cell.obstacleReference);
//    }

//    public Cell GetNearestCell(Vector3 worldPosition)
//    {
//        Vector3 fromAnchor = worldPosition - anchor.position;

//        Vector2Int gridCoord = new(Mathf.RoundToInt(fromAnchor.x / cellSizeX), Mathf.RoundToInt(fromAnchor.y / cellSizeY));
//        gridCoord.x = Mathf.Clamp(gridCoord.x, 0, nbCellsX);
//        gridCoord.y = Mathf.Clamp(gridCoord.y, 0, nbCellsY);

//        return cellsHashset[gridCoord];
//    }

//    public bool GetCell(out Cell result, Vector3 worldPosition)
//    {
//        Vector3 fromAnchor = worldPosition - anchor.position;

//        Vector2Int gridCoord = new(Mathf.RoundToInt(fromAnchor.x / cellSizeX), Mathf.RoundToInt(fromAnchor.y / cellSizeY));

//        bool coordIsValid = gridCoord.x >= 0 && gridCoord.x < nbCellsX && gridCoord.y >= 0 && gridCoord.y < nbCellsY;
//        if (coordIsValid)
//        {
//            result = cellsHashset[gridCoord];
//            return true;
//        }
//        else
//        {
//            result = new Cell(Vector2Int.zero, Vector3.zero, null, null);
//            return false;
//        }
//    }

//    public bool GetLane(out Lane result, int laneId)
//    {
//        bool laneIdIsValid = laneId >= 0 && laneId < nbCellsY;
//        if (laneIdIsValid)
//        {
//            result = lanesHashset[laneId];
//            return true;
//        }
//        else
//        {
//            result = new Lane();
//            return false;
//        }
//    }

//    public void UpdateCellOccupation(Cell currentCell, CellOccupation.EOccupierType newOccupierType)
//    {
//        currentCell.occupation.occupierType = newOccupierType;
//        currentCell.occupation.lastModificationTime = Time.time;
//    }

//    public int GetNbCellsX()
//    { 
//        return nbCellsX; 
//    }

//    public int GetNbCellsY()
//    {
//        return nbCellsY;
//    }
//    public float GetCellSizeX()
//    {
//        return cellSizeX;
//    }

//    public float GetCellSizeY()
//    {
//        return cellSizeY;
//    }
//    public int GetColumnBackGarden()
//    {
//        return NumberOfColumnBackGarden;
//    }

//    public int GetColumnFrontGarden()
//    {
//        return NumberOfColumnFrontGarden;
//    }

//    private void OnDrawGizmos()
//    {
//        if (!bGenerated) return;

//        if (cellsHashset != null)
//        {
//            {
//                // Draw vertical lines
//                Gizmos.color = Color.red;
//                Cell startCell = GetCell(0, 0);
//                Cell endCell = GetCell(0, nbCellsY - 1);

//                Vector3 start = startCell.worldPosition + 0.5f * new Vector3(-cellSizeX, -cellSizeY, 0f);
//                Vector3 end = endCell.worldPosition + 0.5f * new Vector3(-cellSizeX, cellSizeY, 0f);
//                Gizmos.DrawLine(start, end);

//                for (int i = 0; i < nbCellsX; i++)
//                {
//                    startCell = GetCell(i, 0);
//                    endCell = GetCell(i, nbCellsY - 1);

//                    start = startCell.worldPosition + 0.5f * new Vector3(cellSizeX, -cellSizeY, 0f);
//                    end = endCell.worldPosition + 0.5f * new Vector3(cellSizeX, cellSizeY, 0f);

//                    Gizmos.DrawLine(start, end);
//                }
//            }
//            {
//                // Draw horizontal lines
//                Gizmos.color = Color.red;
//                Cell startCell = GetCell(0, 0);
//                Cell endCell = GetCell(nbCellsX - 1, 0);

//                Vector3 start = startCell.worldPosition + 0.5f * new Vector3(-cellSizeX, -cellSizeY, 0f);
//                Vector3 end = endCell.worldPosition + 0.5f * new Vector3(cellSizeX, -cellSizeY, 0f);
//                Gizmos.DrawLine(start, end);

//                for (int i = 0; i < nbCellsY; i++)
//                {
//                    startCell = GetCell(0, i);
//                    endCell = GetCell(nbCellsX - 1, i);

//                    start = startCell.worldPosition + 0.5f * new Vector3(-cellSizeX, cellSizeY, 0f);
//                    end = endCell.worldPosition + 0.5f * new Vector3(cellSizeX, cellSizeY, 0f);

//                    Gizmos.DrawLine(start, end);
//                }
//            }
//        }
//    }
//}

