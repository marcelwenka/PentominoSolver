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
        private readonly static List<IPentomino> pieceTypes = new List<IPentomino>()
            { new F(), new Fp(), new I(), new L(), new Lp(), new N(), new Np(), new P(), new Pp(), new T(), new U(), new V(), new W(), new X(), new Y(), new Yp(), new Z(), new Zp() };

        static void Main()
        {
            List<(string Algorithm, List<PentominoQuantity> PentominoQuantities)> problems;
            
            problems = ReadFile("input.txt");

            if (problems == null || !problems.Any())
                problems = ReadInput();

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

        private static List<(string, List<PentominoQuantity>)> ReadInput()
        {
            List<PentominoQuantity> pieces;

            string input;
            while (true)
            {
                Console.WriteLine("Specify the number of pieces to generate or a list of numbers for each piece (delimited with spaces):");

                input = Console.ReadLine();

                if (TryParseNumbers(input, ' ', out var quantities) && quantities.Count > 1 && quantities.Count <= pieceTypes.Count && quantities.Any(x => x > 0))
                {
                    pieces = GeneratePieces(quantities);
                    break;
                }
                else if (int.TryParse(input, out var count) && count >= 1)
                {
                    pieces = GeneratePieces(count);
                    break;
                }
            }
            while (input != "e" && input != "h")
            {
                Console.WriteLine("Type e to use the exact algorithm or h to use the heuristic algorithm.");
                input = Console.ReadLine();
            }

            return new List<(string, List<PentominoQuantity>)>() { (input, pieces) };
        }

        private static List<(string, List<PentominoQuantity>)> ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine(Directory.GetCurrentDirectory());
                Console.WriteLine("Could not find file.");
                return null;
            }

            var list = new List<(string, List<PentominoQuantity>)>();
            var lines = File.ReadAllLines(path);

            for (int line = 0; line < lines.Length; line += 3)
            {
                var (algorithm, pentominoQuantities) = ReadProblem(lines, line);
                if (pentominoQuantities != null)
                    list.Add((algorithm, pentominoQuantities));
                else
                    break;
            }

            return list;
        }

        private static (string Algorithm, List<PentominoQuantity> PentominoQuantities) ReadProblem(string[] lines, int line)
        {
            string algorithm;

            if (lines.Length < line + 3)
            {
                Console.WriteLine("Could not parse file.");
                Console.WriteLine($"Not enough line arguments specified after line {line} (expected 3 and there only are {lines.Length - line}).");
                return (null, null);
            }

            if (int.TryParse(lines[line], out int problemSize))
                if (problemSize != 5)
                {
                    Console.WriteLine("Could not parse file.");
                    Console.WriteLine($"Invalid input at line {line}: {lines[line]}. Expected \"5\".");
                    return (null, null);
                }

            line++;

            if (lines[line].Trim() == "op")
                algorithm = "e";
            else if (lines[line].Trim() == "hp")
                algorithm = "h";
            else
            {
                Console.WriteLine("Could not parse file.");
                Console.WriteLine($"Invalid input at line {line}: {lines[line]}. Expected \"op\" or \"hp\".");
                return (null, null);
            }

            line++;

            if (TryParseNumbers(lines[line], ' ', out var quantities) && quantities.Count > 1 && quantities.Count <= pieceTypes.Count && quantities.Any(x => x > 0))
                return (algorithm, GeneratePieces(quantities));
            else if (int.TryParse(lines[line], out var count) && count >= 1)
                return (algorithm, GeneratePieces(count));

            Console.WriteLine("Could not parse file.");
            Console.WriteLine($"Invalid input at line {line}: expected a single number or a list of numbers delimited with spaces.");
            return (null, null);
        }

        private static bool TryParseNumbers(string input, char delimiter, out List<uint> list)
        {
            list = new List<uint>();
            var numberStrings = input.Split(delimiter);
            foreach (var numberString in numberStrings)
            {
                if (uint.TryParse(numberString, out var number))
                    list.Add(number);
                else
                    return false;
            }

            return true;
        }

        private static List<PentominoQuantity> GeneratePieces(List<uint> quantities)
        {
            if (quantities.Count < 2 || quantities.Count > pieceTypes.Count)
                throw new ArgumentException($"Unexpected error. Quantities length must be at least 2 and at most {pieceTypes.Count} (the number of different pieces).");

            var pieces = pieceTypes
                .Select(x => new PentominoQuantity(x, 0))
                .ToList();

            for (int i = 0; i < quantities.Count; i++)
                pieces[i].Quantity = (int)quantities[i];

            return pieces
                .Where(x => x.Quantity > 0)
                .ToList();
        }

        private static List<PentominoQuantity> GeneratePieces(int count)
        {
            var pieces = pieceTypes
                .ToDictionary(x => x, y => 0);
            var rand = new Random();
            for (int i = 0; i < count; i++)
                pieces[pieceTypes[rand.Next(pieceTypes.Count())]]++;
            return pieces
                .Select(x => new PentominoQuantity(x.Key, x.Value))
                .Where(x => x.Quantity > 0)
                .ToList();
        }
    }
}
