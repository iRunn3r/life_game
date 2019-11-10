using System;
using UnityEngine;

namespace Game
{
    [Serializable]
    internal class Board
    {
        internal bool[,] Cells { get; set; }
        internal int Width { get; set; }
        internal int Height { get; set; }

        internal Board(int width, int height)
        {
            Width = width;
            Height = height;
            Cells = new bool[width, height];
        }

        internal void Update()
        {
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var neighborCount = CountNeighbors(x, y);
                    if (GetCell(x, y) == false)
                    {
                        if (neighborCount == 3)
                            SetCell(x, y, true);
                    }
                    else
                    {
                        if (neighborCount < 2 || neighborCount > 3)
                            SetCell(x, y, false);
                    }
                }
            }
        }

        internal void SetCell(int x, int y, bool value)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException($"The cell position {x}:{y} is out of the bounds of the board!");

            Cells[x, y] = value;
        }

        internal bool GetCell(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException($"The cell position {x}:{y} is out of the bounds of the board!");

            return Cells[x, y];
        }

        private int CountNeighbors(int x, int y)
        {
            var neighborCount = 0;
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
                    if (((i == 3 && j == 4) || (i == 4 && j == 4) || (i == 5 && j == 4)) && x == 4 && y == 3)
                        Debug.Log($"GetCell({i}, {j}): {GetCell(i, j)}");
                    if (i >= 0 && i < Width && j >= 0 && j < Height && (i != x || j != y) && GetCell(i, j) == true)
                        neighborCount++;
                }
            }

            return neighborCount;
        }

        internal void Print()
        {
            if (Width > 100 || Height > 100)
            {
                Debug.LogError($"Board dimensions too large for printing ({Width}x{Height})");
                return;
            }

            var dead = "X";
            var alive = "O";

            var message = string.Empty;
            for (int y = Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    message += Cells[x, y] == true ? alive : dead;
                }
                message += "\n";
            }

            Debug.Log(message);
        }
    }
}
