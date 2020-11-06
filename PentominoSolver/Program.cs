using PentominoSolver.Pentominos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            foreach (var problem in problems)
            {
                if (problem.Algorithm == "e")
                {
                    var (cutLength, solutionsCount, rectangle) = ExactAlgorithm.Solve(problem.PentominoQuantities);

                    Console.WriteLine("Optimal algorithm");
                    Console.WriteLine($"Found {solutionsCount} exact solutions with the length of cuts needed to solve the problem equal to {cutLength}. First solution:");
                    PrintSolution(rectangle);
                    Console.WriteLine();

                }
                else if (problem.Algorithm == "h")
                {
                    var (cutLength, rectangle) = HeuristicAlgorithm.Solve(problem.PentominoQuantities);

                    Console.WriteLine("Heuristic algorithm");
                    Console.WriteLine($"Aggregated length of cuts needed to solve the problem: {cutLength}. Solution:");
                    PrintSolution(rectangle);
                    Console.WriteLine();
                }
            }

            Console.ReadKey();
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
                    Console.Write("x ");
                }
                Console.WriteLine();
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
