using System.Collections.Generic;

public class GridData
{
    public List<GridCell> Cells { get {  return _cells; } }

    public GridData(List<GridCell> cells)
    {
        _cells = cells;
    }

    #region Private

    private readonly List<GridCell> _cells;

    #endregion Private
}
