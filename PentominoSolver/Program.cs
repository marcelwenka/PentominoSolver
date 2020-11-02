using PentominoSolver.Pentominos;
using System;
using System.Collections.Generic;
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
            var pieces = GetPieces();

            string input = "";
            while (input != "e" && input != "h")
            {
                Console.WriteLine("Type e to use the exact algorithm or h to use the heuristic algorithm.");
                input = Console.ReadLine();
            }

            int n = 0;
            int[,] rectangle = null;

            if (input == "e")
                (n, rectangle) = ExactAlgorithm.Solve(pieces);
            else if (input == "h")
                (n, rectangle) = HeuristicAlgorithm.Solve(pieces);

            if (rectangle != null)
            {
                Console.WriteLine($"Aggregated length of cuts needed to solve the problem: {n}. First solution:");
                PrintSolution(rectangle);
            }
            else
            {
                Console.WriteLine("There are no solutions.");
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
        }

        private static List<PentominoQuantity> GetPieces()
        {
            while (true)
            {
                Console.WriteLine("Specify the number of pieces to generate or the number of individual pieces (delimited with commas):");

                var input = Console.ReadLine();

                if (TryParseNumbers(input, ',', out var quantities) && quantities.Count == pieceTypes.Count && quantities.Any(x => x > 0))
                    return GeneratePieces(quantities);
                else if (int.TryParse(input, out var count) && count >= 1)
                    return GeneratePieces(count);
            }
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
            if (quantities.Count != pieceTypes.Count)
                throw new ArgumentException("Unexpected error. Quantities length must be the same as the number of different pieces.");

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
