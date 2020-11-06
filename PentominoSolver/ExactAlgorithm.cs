using DlxLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PentominoSolver
{
    public static class ExactAlgorithm
    {
        public static (int, List<int[,]>) Solve(List<PentominoQuantity> pentominos)
        {
            var rectangle = SolvingHelper.GenerateRectangle(pentominos);
            int cutLength = 0;

            while (true)
            {
                var currentPiecesCombinations = GeneratePiecesWithCuts(pentominos, cutLength);
                var results = new List<int[,]>();

                foreach (var currentPiecesCombination in currentPiecesCombinations)
                {
                    var tempMatrix = new List<int[]>();
                    for (int i = 0; i < currentPiecesCombination.Count; i++)
                        tempMatrix.AddRange(SolvingHelper.CreateRows(currentPiecesCombination[i], rectangle, currentPiecesCombination.Count, i));

                    var matrix = SolvingHelper.ConvertToArray(tempMatrix);
                    if (matrix == null)
                        continue;

                    var dlx = new Dlx();
                    var solutions = dlx.Solve(matrix).ToList();

                    foreach (var solution in solutions)
                    {
                        var pieceNumber = 1;
                        foreach (var index in solution.RowIndexes)
                        {
                            for (int i = currentPiecesCombination.Count; i < matrix.GetLength(1); i++)
                            {
                                if (matrix[index, i] == 1)
                                    rectangle[(i - currentPiecesCombination.Count) / rectangle.GetLength(1), (i - currentPiecesCombination.Count) % rectangle.GetLength(1)] = pieceNumber;
                            }
                            pieceNumber++;
                        }

                        results.Add((int[,])rectangle.Clone());
                    }
                }

                if (results.Any())
                    return (cutLength, results);

                cutLength++;
            }
        }

        private static List<List<IPiece>> GeneratePiecesWithCuts(List<PentominoQuantity> pentominos, int targetCutLength)
        {
            List<List<IPiece>> solutions = new List<List<IPiece>>();
            var pentominosQuantities = new LinkedList<PentominoQuantity>(pentominos);
            GeneratePiecesWithCuts(pentominosQuantities.First, solutions, new List<IPiece>(), 0, -1, targetCutLength, 0);
            return solutions;
        }

        private static void GeneratePiecesWithCuts(LinkedListNode<PentominoQuantity> pentominoQuantity, List<List<IPiece>> solutions, List<IPiece> pieces, int pentominoIndex, int cutIndex, int targetCutLength, int currentCutLength)
        {
            if (pentominoIndex == pentominoQuantity.Value.Quantity)
            {
                pentominoQuantity = pentominoQuantity.Next;
                pentominoIndex = 0;
                cutIndex = -1;
            }
            if (pentominoQuantity == null)
            {
                if (currentCutLength == targetCutLength)
                    solutions.Add(new List<IPiece>(pieces));

                return;
            }

            var piece = pentominoQuantity.Value.Pentomino;

            if (cutIndex == -1)
            {
                pieces.Add(piece);
                GeneratePiecesWithCuts(pentominoQuantity, solutions, pieces, pentominoIndex + 1, cutIndex, targetCutLength, currentCutLength);
                pieces.RemoveAt(pieces.Count - 1);
                cutIndex++;
            }

            for (; cutIndex < piece.Cuts.Count; cutIndex++)
            {
                if (currentCutLength + piece.Cuts[cutIndex].CutLength <= targetCutLength)
                {
                    pieces.AddRange(piece.Cuts[cutIndex].Pieces);
                    GeneratePiecesWithCuts(pentominoQuantity, solutions, pieces, pentominoIndex + 1, cutIndex, targetCutLength, currentCutLength + piece.Cuts[cutIndex].CutLength);
                    pieces.RemoveRange(pieces.Count - piece.Cuts[cutIndex].Pieces.Count, piece.Cuts[cutIndex].Pieces.Count);
                }
            }
        }
    }
}
