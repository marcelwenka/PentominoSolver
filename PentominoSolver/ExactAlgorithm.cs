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
            var pentominosQuantities = new LinkedList<(IPentomino, int)>(pentominos
                .GroupBy(x => x.Type)
                .Select(x => (x.First(), x.Count())));
            GeneratePiecesWithCuts(pentominosQuantities.First, solutions, new List<IPiece>(), 0, -1, targetCutLength, 0);
            return solutions;
        }

        private static void GeneratePiecesWithCuts(LinkedListNode<(IPentomino Piece, int Quantity)> pentomino, List<List<IPiece>> solutions, List<IPiece> pieces, int pentominoIndex, int cutIndex, int targetCutLength, int currentCutLength)
        {
            if (pentominoIndex == pentomino.Value.Quantity)
            {
                pentomino = pentomino.Next;
                pentominoIndex = 0;
                cutIndex = -1;
            }
            if (pentomino == null)
            {
                if (currentCutLength == targetCutLength)
                    solutions.Add(new List<IPiece>(pieces));

                return;
            }

            var piece = pentomino.Value.Piece;

            if (cutIndex == -1)
            {
                pieces.Add(piece);
                GeneratePiecesWithCuts(pentomino, solutions, pieces, pentominoIndex + 1, cutIndex, targetCutLength, currentCutLength);
                pieces.RemoveAt(pieces.Count - 1);
                cutIndex++;
            }

            for (; cutIndex < piece.Cuts.Count; cutIndex++)
            {
                if (currentCutLength + piece.Cuts[cutIndex].CutLength <= targetCutLength)
                {
                    pieces.AddRange(piece.Cuts[cutIndex].Pieces);
                    GeneratePiecesWithCuts(pentomino, solutions, pieces, pentominoIndex + 1, cutIndex, targetCutLength, currentCutLength + piece.Cuts[cutIndex].CutLength);
                    pieces.RemoveRange(pieces.Count - piece.Cuts[cutIndex].Pieces.Count, piece.Cuts[cutIndex].Pieces.Count);
                }
            }
        }
    }
}
