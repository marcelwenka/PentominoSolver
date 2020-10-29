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
            var rectangle = SolvingHelper.GenerateRectangle(pentominos);
            int cutLength = 0;

            while (true)
            {
                var currentPiecesCombinations = GeneratePiecesWithCuts(pentominos, cutLength);

                foreach (var currentPiecesCombination in currentPiecesCombinations)
                {
                    var tempMatrix = new List<int[]>();
                    for (int i = 0; i < currentPiecesCombination.Count; i++)
                        tempMatrix.AddRange(SolvingHelper.CreateRows(currentPiecesCombination[i], rectangle, currentPiecesCombination.Count, i));

                    var matrix = SolvingHelper.ConvertToArray(tempMatrix);
                    if (matrix == null)
                        continue;

                    var dlx = new Dlx();
                    var solution = dlx.Solve(matrix).FirstOrDefault();

                    if (solution != null)
                    {
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
    }
}
