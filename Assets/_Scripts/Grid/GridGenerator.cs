using System.Collections.Generic;
using UnityEngine;

public class GridGenerator
{
    public struct Input
    {
        public Vector2Int cellsCount;
        public Vector2 cellSize;
        public Vector3 anchorPosition;
    }

    public static bool Generate(ref GridData data, Input input)
    {
        bool isEmpty = input.cellsCount.x == 0 || input.cellsCount.y == 0;
        if (!isEmpty)
        {
            List<GridCell> cells = new()
            {
                Capacity = input.cellsCount.x * input.cellsCount.y
            };

            for (int i = 0; i < input.cellsCount.x; i++)
            {
                for (int j = 0; j < input.cellsCount.y; j++)
                {
                    cells.Add(new GridCell(
                        coord : new Vector2Int(i,j),
                        position: new Vector3(
                            x: input.anchorPosition.x + i * input.cellSize.x,
                            y: input.anchorPosition.y + j * input.cellSize.y
                            )
                        ));
                }
            }
            data = new GridData(cells);
        }
        return !isEmpty;
    }
}
