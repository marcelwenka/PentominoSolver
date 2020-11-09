using DlxLib;
using PentominoSolver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PentominoSolver
{
    public static class ExactAlgorithm
    {
        public static (int, List<int[,]>) Solve(List<PieceQuantity> pieceQuantities)
        {
            var rectangle = SolvingHelper.GenerateRectangle(pieceQuantities);
            var results = new List<int[,]>();
            int cutLength = 0;
            int resultsCount = 0;

            while (true)
            {
                var currentPiecesCombinations = GeneratePiecesWithCuts(pieceQuantities, cutLength);

                foreach (var currentPieces in currentPiecesCombinations)
                {
                    var currentPiecesCount = currentPieces.Sum(x => x.Quantity);
                    var tempMatrix = new List<int[]>();
                    var repeatedRowsDictionary = new Dictionary<int, int>();
                    int pieceIndex = 0, repeatedRowIndex = 0, disctinctRowIndex = 0;
                    foreach (var pieceQuantity in currentPieces)
                    {
                        int rowsCount = 0;
                        for (int i = 0; i < pieceQuantity.Quantity; i++)
                        {
                            var rows = SolvingHelper.CreateRows(pieceQuantity.Piece, rectangle, currentPiecesCount, pieceIndex++);
                            rowsCount = rows.Count;
                            foreach (var row in rows)
                            {
                                repeatedRowsDictionary[repeatedRowIndex++] = disctinctRowIndex++;
                                tempMatrix.Add(row);
                            }
                            disctinctRowIndex -= rowsCount;
                        }
                        disctinctRowIndex += rowsCount;
                    }

                    var matrix = SolvingHelper.ConvertToArray(tempMatrix);
                    if (matrix == null)
                        continue;

                    var dlx = new Dlx();
                    var solutions = dlx.Solve(matrix).ToList();
                    var distinctSolutionsHashSet = new HashSet<IEnumerable<int>>(new SequenceComparer<int>());
                    foreach (var solution in solutions)
                    {
                        if (!distinctSolutionsHashSet.Add(solution.RowIndexes.Select(x => repeatedRowsDictionary[x]).OrderBy(x => x)))
                            continue;

                        var pieceNumber = 1;
                        foreach (var rowIndex in solution.RowIndexes)
                        {
                            for (int i = currentPiecesCount; i < matrix.GetLength(1); i++)
                            {
                                if (matrix[rowIndex, i] == 1)
                                    rectangle[(i - currentPiecesCount) / rectangle.GetLength(1), (i - currentPiecesCount) % rectangle.GetLength(1)] = pieceNumber;
                            }
                            pieceNumber++;
                        }

                        results.Add((int[,])rectangle.Clone());
                    }

                    var solutionRepetitions = currentPieces
                        .Select(x => Factorial(x.Quantity))
                        .Aggregate((x, y) => x * y);

                    if (distinctSolutionsHashSet.Count != solutions.Count / solutionRepetitions)
                        throw new InvalidOperationException("Internal error. Number of distinct solutions is not proper.");

                    resultsCount += solutions.Count / solutionRepetitions;
                }

                if (resultsCount > 0)
                    return (cutLength, results);

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

        private static List<List<PieceQuantity>> GeneratePiecesWithCuts(List<PieceQuantity> pieceQuantities, int targetCutLength)
        {
            List<List<IPiece>> solutions = new List<List<IPiece>>();
            GeneratePiecesWithCuts(new LinkedList<PieceQuantity>(pieceQuantities).First, solutions, new List<IPiece>(), 0, -1, targetCutLength, 0);
            return solutions
                .Select(x =>
                    x.GroupBy(y => y.GetType())
                        .Select(y => new PieceQuantity(y.First(), y.Count()))
                        .ToList()
                )
                .ToList();
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

        class SequenceComparer<T> : IEqualityComparer<IEnumerable<T>>
        {
            public bool Equals(IEnumerable<T> seq1, IEnumerable<T> seq2)
            {
                return seq1.SequenceEqual(seq2);
            }

            public int GetHashCode(IEnumerable<T> seq)
            {
                int hash = 1234567;
                foreach (T elem in seq)
                    hash = hash * 37 + elem.GetHashCode();
                return hash;
            }
        }
    }
}
