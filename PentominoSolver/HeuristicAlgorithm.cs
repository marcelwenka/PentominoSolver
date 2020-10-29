using DlxLib;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace PentominoSolver
{
    public static class HeuristicAlgorithm
    {
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private static List<int> bestSolutionIndexes = new List<int>();

        public static (int, int[,]) Solve(List<IPentomino> pieces)
        {
            var rectangle = SolvingHelper.GenerateRectangle(pieces);

            var tempMatrix = new List<int[]>();
            for (int i = 0; i < pieces.Count; i++)
                tempMatrix.AddRange(SolvingHelper.CreateRows(pieces[i], rectangle, pieces.Count, i));

            var matrix = SolvingHelper.ConvertToArray(tempMatrix);
            if (matrix == null)
                return (0, null);

            var dlx = new Dlx(cancellationTokenSource.Token);
            dlx.SolutionFound += DlxSolutionFound;
            dlx.SearchStep += DlxSearchStep;
            dlx.Solve(matrix).ToList();

            var pieceNumber = 0;
            foreach (var index in bestSolutionIndexes)
            {
                for (int i = pieces.Count; i < matrix.GetLength(1); i++)
                {
                    if (matrix[index, i] == 1)
                        rectangle[(i - pieces.Count) / rectangle.GetLength(1), (i - pieces.Count) % rectangle.GetLength(1)] = pieceNumber;
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

            // todo ograniczenie czasowe/iteracyjne
        }

        private static void DlxSolutionFound(object sender, SolutionFoundEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }
    }
}
