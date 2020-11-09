using PentominoSolver.Pieces.Pentominos;
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
            List<(Algorithm Algorithm, List<PieceQuantity> PieceQuantities)> problems;

            problems = InputHelper.ReadFile("input.txt");

            if (problems == null || !problems.Any())
                problems = InputHelper.ReadInput();

            foreach (var problem in problems)
            {
                switch (problem.Algorithm)
                {
                    case Algorithm.Exact:
                        HandleExactProblem(problem.PieceQuantities);
                        break;
                    case Algorithm.Heuristic:
                        HandleHeuristicProblem(problem.PieceQuantities);
                        break;
                } 
            }

            Console.WriteLine("All problems processed.");
            Console.WriteLine("Press esc to exit the program.");
            while (Console.ReadKey(true).Key != ConsoleKey.Escape) { }
        }

        private static void HandleExactProblem(List<PieceQuantity> pieceQuantities)
        {
            Console.WriteLine("OPTIMAL ALGORITHM");
            Console.Write("Pieces: ");
            foreach (var pentominoQuantity in pieceQuantities)
                Console.Write($"{pentominoQuantity.Piece.GetType().Name}: {pentominoQuantity.Quantity}, ");
            Console.WriteLine();
            Console.WriteLine("Solving...");

            var (cutLength, rectangles) = ExactAlgorithm.Solve(pieceQuantities);

            Console.WriteLine($"Found {rectangles.Count} exact solutions with the length of cuts needed to solve the problem equal to {cutLength}. First solution:");
            PrintSolution(rectangles.First());

            Console.WriteLine($"Type a number between 1 and {rectangles.Count} to see the corresponding solution or type 'n' to move on to the next problem.");
            var input = "";
            while (input != "n")
            {
                if (int.TryParse(input, out int number) && number >= 1 && number <= rectangles.Count)
                    PrintSolution(rectangles[number - 1]);
                input = Console.ReadLine();
            }
            Console.Clear();
        }

        private static void HandleHeuristicProblem(List<PieceQuantity> pieceQuantities)
        {
            Console.WriteLine("HEURISTIC ALGORITHM");
            Console.Write("Pieces: ");
            foreach (var pentominoQuantity in pieceQuantities)
                Console.Write($"{pentominoQuantity.Piece.GetType().Name}: {pentominoQuantity.Quantity}, ");
            Console.WriteLine();
            Console.WriteLine("Solving...");

            var (cutLength, rectangle) = HeuristicAlgorithm.Solve(pieceQuantities);

            Console.WriteLine($"Aggregated length of cuts needed to solve the problem: {cutLength}. Solution:");
            PrintSolution(rectangle);

            Console.WriteLine("Type 'n' to move on to the next problem.");
            while (Console.ReadLine() != "n") { }
            Console.Clear();
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
            Console.WriteLine();
        }
    }
}
