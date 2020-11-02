using DlxLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PentominoSolver
{
    public static class HeuristicAlgorithm
    {
        private static readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private static List<int> bestSolutionIndexes = new List<int>();
        private static bool perfectSolutionFound = false;

        private static ulong currentIteration = 0;
        private static ulong maxIteration = 0;

        public static (int, int[,]) Solve(List<PentominoQuantity> pieces)
        {
            var piecesCount = pieces.Sum(x => x.Quantity);
            var rectangle = SolvingHelper.GenerateRectangle(pieces);
            maxIteration = (ulong)Math.Round(Math.Pow(pieces.Sum(x => x.Pentomino.Orientations.Length * x.Quantity), 3));

            var tempMatrix = new List<int[]>();
            for (int i = 0, j = 0; i < pieces.Count; i++)
                for (int k = 0; k < pieces[i].Quantity; j++, k++)
                    tempMatrix.AddRange(SolvingHelper.CreateRows(pieces[i].Pentomino, rectangle, piecesCount, j));

            var matrix = SolvingHelper.ConvertToArray(tempMatrix);
            if (matrix == null)
                return (0, null);

            var dlx = new Dlx(cancellationTokenSource.Token);
            dlx.SolutionFound += DlxSolutionFound;
            dlx.SearchStep += DlxSearchStep;
            dlx.Solve(matrix).ToList();

            var pieceNumber = 1;
            foreach (var index in bestSolutionIndexes)
            {
                for (int i = piecesCount; i < matrix.GetLength(1); i++)
                {
                    if (matrix[index, i] == 1)
                        rectangle[(i - piecesCount) / rectangle.GetLength(1), (i - piecesCount) % rectangle.GetLength(1)] = pieceNumber;
                }
                pieceNumber++;
            }

            // todo uzupełnienie dziur klockami

            return (0, rectangle);
        }

        private static void DlxSearchStep(object sender, SearchStepEventArgs e)
        {
            if (e.RowIndexes.Count() > bestSolutionIndexes.Count)
                bestSolutionIndexes = new List<int>(e.RowIndexes);

            if (++currentIteration > maxIteration)
                cancellationTokenSource.Cancel();

            // todo ograniczenie czasowe/iteracyjne
        }

        private static void DlxSolutionFound(object sender, SolutionFoundEventArgs e)
        {
            perfectSolutionFound = true;
            cancellationTokenSource.Cancel();
        }
    }
}
