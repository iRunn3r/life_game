using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [Serializable]
    internal class Board : MonoBehaviour
    {
        internal Cell[,] Cells { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private static GameObject deadPrefab;
        private static GameObject alivePrefab;

        internal struct Cell
        {
            internal bool State { get; set; }
            internal int X { get; set; }
            internal int Y { get; set; }
            internal GameObject Tile;

            internal Cell(int x, int y, bool state)
            {
                State = state;
                X = x;
                Y = y;
                Tile = Instantiate(deadPrefab, new Vector3(x, y), Quaternion.identity);
            }
        }
        
        internal Board(int width, int height)
        {
            Initialize(width, height);
        }
        
        internal void Initialize(int width, int height)
        {
            deadPrefab = Resources.Load("DeadCell") as GameObject;
            alivePrefab = Resources.Load("AliveCell") as GameObject;

            Width = width;
            Height = height;
            Cells = new Cell[width, height];

            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    Cells[x, y] = new Cell(x, y, false);
                }
            }
        }
        
        internal void UpdateCells()
        {
            var changedCells = new List<(int X, int Y, bool Value)>();
            for (var x = 0; x < Width; x++)
            {
                for (var y = 0; y < Height; y++)
                {
                    var neighborCount = CountNeighbors(x, y);
                    if (GetCell(x, y) == false)
                    {
                        if (neighborCount == 3)
                        {
                            changedCells.Add((x, y, true));
                        }  
                    }
                    else
                    {
                        if (neighborCount < 2 || neighborCount > 3)
                            changedCells.Add((x, y, false));
                    }
                }
            }

            for (var i = 0; i < changedCells.Count; i++)
            {
                SetCell(changedCells[i].X, changedCells[i].Y, changedCells[i].Value);
            }
        }

        internal void SetCell(int x, int y, bool value)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException($"The cell position {x}:{y} is out of the bounds of the board!");

            SetPrefab(x, y, value);
            Cells[x, y].State = value;
        }

        private void SetPrefab(int x, int y, bool alive)
        {
            if (alive == Cells[x, y].State)
                return;
            
            DestroyImmediate(Cells[x, y].Tile);
            Cells[x, y].Tile = Instantiate(alive ? alivePrefab : deadPrefab, new Vector3(x, y), Quaternion.identity);
        }

        internal bool GetCell(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                throw new ArgumentOutOfRangeException($"The cell position {x}:{y} is out of the bounds of the board!");

            return Cells[x, y].State;
        }

        private int CountNeighbors(int x, int y)
        {
            var neighborCount = 0;
            for (var i = x - 1; i <= x + 1; i++)
            {
                for (var j = y - 1; j <= y + 1; j++)
                {
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

            const string dead = "X";
            const string alive = "O";
            
            var message = string.Empty;
            for (var y = Height - 1; y >= 0; y--)
            {
                for (var x = 0; x < Width; x++)
                {
                    message += Cells[x, y].State ? alive : dead;
                }
                message += "\n";
            }

            Debug.Log(message);
        }
    }
}
