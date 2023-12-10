using System.Collections.Generic;
using UnityEngine;

public class GridGenerator
{
    public struct Input
    {
        public Vector2Int CellsCount;
        public Vector2 CellSize;
        public Vector2 Offset;
    }

    public static bool Generate(ref GridData data, Input input)
    {
        bool isEmpty = input.CellsCount.x == 0 || input.CellsCount.y == 0;
        if (!isEmpty)
        {
            List<GridCell> cells = new()
            {
                Capacity = input.CellsCount.x * input.CellsCount.y
            };
            Vector2 start = ComputeStartingPosition(input);
            for (int i = 0; i < input.CellsCount.x; i++)
            {
                for (int j = 0; j < input.CellsCount.y; j++)
                {
                    cells.Add(new GridCell(
                        coord : new Vector2Int(i,j),
                        position: new Vector3(
                            x: start.x + i * input.CellSize.x,
                            y: start.y + j * input.CellSize.y
                            )
                        ));
                }
            }
            data = new GridData(cells);
        }
        return !isEmpty;
    }

    #region Private

    private static Vector2 ComputeStartingPosition(Input input)
    {
        return input.Offset - new Vector2(
            0.5f * (input.CellsCount.x - 1) * input.CellSize.x,
            0.5f * (input.CellsCount.y - 1) * input.CellSize.y
            );
    }

    #endregion Private
}
