using System.Collections.Generic;

public class GridData
{
    public List<GridCell> Cells { get; private set; }

    public GridData(List<GridCell> cells)
    {
        Cells = cells;
    }
}
