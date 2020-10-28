using DlxLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PentominoSolver
{
    public static class ExactAlgorithm
    {
        public static (int, int[,]) Solve(List<IPentomino> pentominos)
        {
            var rectangle = GenerateRectangle(pentominos);
            int cutLength = 0;

            while (true)
            {
                var currentPiecesCombinations = GeneratePiecesWithCuts(pentominos, cutLength);

                foreach (var currentPiecesCombination in currentPiecesCombinations)
                {
                    var tempMatrix = new List<int[]>();
                    for (int i = 0; i < currentPiecesCombination.Count; i++)
                        tempMatrix.AddRange(CreateRows(currentPiecesCombination, rectangle, i));

                    var matrix = ConvertToArray(tempMatrix);
                    if (matrix == null)
                        continue;

                    var dlx = new Dlx();
                    var solution = dlx.Solve(matrix).FirstOrDefault();

                    if (solution != null)
                    {
                        var rand = new Random();
                        var pieceNumber = 0;
                        foreach (var index in solution.RowIndexes)
                        {
                            for (int i = currentPiecesCombination.Count; i < matrix.GetLength(1); i++)
                            {
                                if (matrix[index, i] == 1)
                                    rectangle[(i - currentPiecesCombination.Count) / rectangle.GetLength(1), (i - currentPiecesCombination.Count) % rectangle.GetLength(1)] = pieceNumber;
                            }
                            pieceNumber++;
                        }

                        return (cutLength, rectangle);
                    }
                }

                cutLength++;
            }
        }

        private static List<List<IPiece>> GeneratePiecesWithCuts(List<IPentomino> pentominos, int targetCutLength)
        {
            List<List<IPiece>> solutions = new List<List<IPiece>>();
            GeneratePiecesWithCuts(pentominos, solutions, new List<IPiece>(), targetCutLength, 0, 0);
            return solutions;
        }

        private static void GeneratePiecesWithCuts(List<IPentomino> pentominos, List<List<IPiece>> solutions, List<IPiece> pieces, int targetCutLength, int pentominoIndex, int currentCutLength)
        {
            if (pentominoIndex == pentominos.Count())
            {
                if (currentCutLength == targetCutLength)
                    solutions.Add(new List<IPiece>(pieces));

                return;
            }

            pieces.Add(pentominos[pentominoIndex]);
            GeneratePiecesWithCuts(pentominos, solutions, pieces, targetCutLength, pentominoIndex + 1, currentCutLength);
            pieces.RemoveAt(pieces.Count() - 1);

            foreach (var cut in pentominos[pentominoIndex].Cuts)
            {
                if (currentCutLength + cut.Item1 <= targetCutLength)
                {
                    pieces.AddRange(cut.Item2);
                    GeneratePiecesWithCuts(pentominos, solutions, pieces, targetCutLength, pentominoIndex + 1, currentCutLength + cut.Item1);
                    pieces = pieces.GetRange(0, pieces.Count() - cut.Item2.Count());
                }
            }
        }

        private static int[,] GenerateRectangle(List<IPentomino> pieces)
        {
            var area = pieces.Count() * 5;
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

        private static List<int[]> CreateRows(List<IPiece> pieces, int[,] rectangle, int pieceNumber)
        {
            var piece = pieces[pieceNumber];
            var piecesCount = pieces.Count;
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

        private static int[,] ConvertToArray(List<int[]> list)
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
    }
}
