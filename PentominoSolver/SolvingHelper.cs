using PentominoSolver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PentominoSolver
{
    public static class SolvingHelper
    {
        public static List<int[]> CreateRows(IPiece piece, int[,] rectangle, int piecesCount, int pieceNumber)
        {
            var rectangleRows = rectangle.GetLength(0);
            var rectangleColumns = rectangle.GetLength(1);
            var rows = new List<int[]>();

            foreach (var orientation in piece.Orientations)
            {
                for (int i = 0; i <= rectangleRows - orientation.GetLength(0); i++)
                {
                    for (int j = 0; j <= rectangleColumns - orientation.GetLength(1); j++)
                    {
                        var row = new int[piecesCount + rectangle.Length];
                        row[pieceNumber] = 1;

                        for (int k = 0; k < orientation.GetLength(0); k++)
                        {
                            for (int l = 0; l < orientation.GetLength(1); l++)
                            {
                                if (orientation[k, l] == 1)
                                    row[piecesCount + (i + k) * rectangleColumns + j + l] = 1;
                            }
                        }

                        rows.Add(row);
                    }
                }
            }

            return rows;
        }

        public static int[,] ConvertToArray(this List<int[]> list)
        {
            if (list == null || !list.Any())
                return null;
            var length = list.First().Length;
            if (!list.All(x => x.Length == length))
                throw new InvalidOperationException("All arrays have to be of the same size.");

            var array = new int[list.Count, list.First().Length];
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < length; j++)
                    array[i, j] = list[i][j];

            return array;
        }

        public static int[,] RemoveEmptyColumns(int[,] matrix)
        {
            var columnsToRemove = new List<int>();

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                var shouldRemove = true;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[j, i] == 1)
                    {
                        shouldRemove = false;
                        break;
                    }
                }
                if (shouldRemove)
                    columnsToRemove.Add(i);
            }

            if (!columnsToRemove.Any())
                return matrix;

            int[,] result = new int[matrix.GetLength(0), matrix.GetLength(1) - columnsToRemove.Count];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0, k = 0; j < matrix.GetLength(1); j++)
                {
                    if (j == columnsToRemove[k])
                    {
                        k++;
                        continue;
                    }

                    result[i, j - k] = matrix[i, j];
                }
            }

            return result;
        }

        public static void PrintDlxMatrix(int[,] matrix)
        {
            var list = new List<int[]>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                var tab = new int[matrix.GetLength(1)];
                for (int j = 0; j < matrix.GetLength(1); j++)
                    tab[j] = matrix[i, j];
                list.Add(tab);
            }
            var userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            System.IO.File.WriteAllLines(userPath + "Desktop/file.txt", list.Select(x => string.Join(",", x)));
        }

        public static int[,] GenerateRectangle(List<PieceQuantity> pieces)
        {
            var area = pieces.Sum(x => x.Quantity) * 5;
            var dim1 = 1;
            var dim2 = area;

            for (int i = 2; i * i <= area; i++)
            {
                if (area % i == 0)
                {
                    dim1 = i;
                    dim2 = area / i;
                }
            }

            return new int[dim1, dim2];
        }
    }
}
