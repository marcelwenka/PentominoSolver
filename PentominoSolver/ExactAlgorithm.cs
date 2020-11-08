using DlxLib;
using PentominoSolver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PentominoSolver
{
    public static class ExactAlgorithm
    {
        public static (int, int, int[,]) Solve(List<PieceQuantity> pieceQuantities)
        {
            var rectangle = SolvingHelper.GenerateRectangle(pieceQuantities);
            int cutLength = 0;
            int resultsCount = 0;

            while (true)
            {
                var currentPiecesCombinations = GeneratePiecesWithCuts(pieceQuantities, cutLength);

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
                    if (solutions.Any())
                    {
                        var pieceNumber = 1;
                        foreach (var index in solutions.First().RowIndexes)
                        {
                            for (int i = currentPiecesCombination.Count; i < matrix.GetLength(1); i++)
                            {
                                if (matrix[index, i] == 1)
                                    rectangle[(i - currentPiecesCombination.Count) / rectangle.GetLength(1), (i - currentPiecesCombination.Count) % rectangle.GetLength(1)] = pieceNumber;
                            }
                            pieceNumber++;
                        }

                        var solutionRepetitions = currentPiecesCombination
                            .GroupBy(x => x.GetType().FullName)
                            .Select(x => Factorial(x.ToList().Count))
                            .Aggregate((x, y) => x * y);

                        resultsCount += solutions.Count / solutionRepetitions;
                    }
                }

                if (resultsCount > 0)
                    return (cutLength, resultsCount, rectangle);

                cutLength++;
            }
        }

        private static int Factorial(int n)
        {
            int result = 1;
            while (n != 1)
            {
                result = result * n;
                n = n - 1;
            }
            return result;
        }

        private static List<List<IPiece>> GeneratePiecesWithCuts(List<PieceQuantity> pieceQuantities, int targetCutLength)
        {
            List<List<IPiece>> solutions = new List<List<IPiece>>();
            GeneratePiecesWithCuts(new LinkedList<PieceQuantity>(pieceQuantities).First, solutions, new List<IPiece>(), 0, -1, targetCutLength, 0);
            return solutions;
        }

        private static void GeneratePiecesWithCuts(LinkedListNode<PieceQuantity> pieceQuantity, List<List<IPiece>> solutions, List<IPiece> pieces, int pieceIndex, int cutIndex, int targetCutLength, int currentCutLength)
        {
            if (pieceIndex == pieceQuantity.Value.Quantity)
            {
                pieceQuantity = pieceQuantity.Next;
                pieceIndex = 0;
                cutIndex = -1;
            }
            if (pieceQuantity == null)
            {
                if (currentCutLength == targetCutLength)
                    solutions.Add(new List<IPiece>(pieces));

                return;
            }

            var piece = pieceQuantity.Value.Piece;

            if (cutIndex == -1)
            {
                pieces.Add(piece);
                GeneratePiecesWithCuts(pieceQuantity, solutions, pieces, pieceIndex + 1, cutIndex, targetCutLength, currentCutLength);
                pieces.RemoveAt(pieces.Count - 1);
                cutIndex++;
            }

            for (; cutIndex < piece.Cuts.Count; cutIndex++)
            {
                if (currentCutLength + piece.Cuts[cutIndex].CutLength <= targetCutLength)
                {
                    pieces.AddRange(piece.Cuts[cutIndex].Pieces);
                    GeneratePiecesWithCuts(pieceQuantity, solutions, pieces, pieceIndex + 1, cutIndex, targetCutLength, currentCutLength + piece.Cuts[cutIndex].CutLength);
                    pieces.RemoveRange(pieces.Count - piece.Cuts[cutIndex].Pieces.Count, piece.Cuts[cutIndex].Pieces.Count);
                }
            }
        }
    }
}
