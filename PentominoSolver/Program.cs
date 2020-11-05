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
                int n = 0;
                int[,] rectangle = null;

                if (problem.Algorithm == "e")
                    (n, rectangle) = ExactAlgorithm.Solve(problem.PentominoQuantities);
                else if (problem.Algorithm == "h")
                    (n, rectangle) = HeuristicAlgorithm.Solve(problem.PentominoQuantities);

                if (rectangle != null)
                {
                    Console.WriteLine($"Aggregated length of cuts needed to solve the problem: {n}. First solution:");
                    PrintSolution(rectangle);
                }
                else
                {
                    Console.WriteLine("There are no solutions.");
                }
            }

            Console.ReadKey();
        }

        private static void PrintSolution(int[,] rectangle)
        {
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
