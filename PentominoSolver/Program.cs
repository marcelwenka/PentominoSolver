using PentominoSolver.Pentominos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Serialization;

namespace PentominoSolver
{
    class Program
    {
        static void Main()
        {
            List<(string Algorithm, List<PentominoQuantity> PentominoQuantities)> problems;
            
            problems = InputHelper.ReadFile("input.txt");

            if (problems == null || !problems.Any())
                problems = InputHelper.ReadInput();

            var problemIndex = 0;
            foreach (var problem in problems)
            {
                if (problem.Algorithm == "e")
                {
                    Console.WriteLine("OPTIMAL ALGORITHM");
                    Console.Write("Pieces: ");
                    foreach (var pentominoQuantity in problem.PentominoQuantities)
                        Console.Write($"{pentominoQuantity.Pentomino.GetType().Name}: {pentominoQuantity.Quantity}, ");
                    Console.WriteLine();
                    Console.WriteLine("Solving...");

                    var (cutLength, solutionsCount, rectangle) = ExactAlgorithm.Solve(problem.PentominoQuantities);

                    Console.WriteLine($"Found {solutionsCount} exact solutions with the length of cuts needed to solve the problem equal to {cutLength}. First solution:");
                    PrintSolution(rectangle);

                }
                else if (problem.Algorithm == "h")
                {
                    Console.WriteLine("HEURISTIC ALGORITHM");
                    Console.Write("Pieces: ");
                    foreach (var pentominoQuantity in problem.PentominoQuantities)
                        Console.Write($"{pentominoQuantity.Pentomino.GetType().Name}: {pentominoQuantity.Quantity}, ");
                    Console.WriteLine();
                    Console.WriteLine("Solving...");

                    var (cutLength, rectangle) = HeuristicAlgorithm.Solve(problem.PentominoQuantities);

                    Console.WriteLine($"Aggregated length of cuts needed to solve the problem: {cutLength}. Solution:");
                    PrintSolution(rectangle);
                }
                Console.WriteLine();

                if (++problemIndex < problems.Count)
                {
                    Thread.Sleep(500);
                    Console.WriteLine("Press enter to move on to the next problem.");
                    while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
                    for (int i = 0; i < 3; i++)
                        Console.WriteLine();
                }
            }

            Console.WriteLine("All problems processed.");
            Console.WriteLine("Press esc to exit the program.");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
        }

        private static void PrintSolution(int[,] rectangle)
        {
            if (rectangle == null || rectangle.Length == 0)
                Console.WriteLine("No solution found.");

            var colors = Enum.GetValues(typeof(ConsoleColor)).OfType<ConsoleColor>().Except(new List<ConsoleColor>() { ConsoleColor.Black }).ToList();
            for (int i = 0; i < rectangle.GetLength(0); i++)
            {
                for (int j = 0; j < rectangle.GetLength(1); j++)
                {
                    Console.ForegroundColor = rectangle[i, j] == 0 ? ConsoleColor.Black : colors[rectangle[i, j] % colors.Count()];
                    Console.Write("\u25A0 ");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
