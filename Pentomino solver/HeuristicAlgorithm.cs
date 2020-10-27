using DlxLib;
using System.Collections.Generic;
using System.Threading;

namespace PentominoSolver
{
    public static class HeuristicAlgorithm
    {
        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        public static (int, int[,]) Solve(List<IPentomino> pentominos)
        {
            var dlx = new Dlx(cancellationTokenSource.Token);
            dlx.SolutionFound += DlxSolutionFound;
            dlx.SearchStep += DlxSearchStep;

            return (0, null);
        }

        private static void DlxSearchStep(object sender, SearchStepEventArgs e)
        {
            // todo zapamiętywanie najlepszego rozwiązania
        }

        private static void DlxSolutionFound(object sender, SolutionFoundEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }
    }
}
