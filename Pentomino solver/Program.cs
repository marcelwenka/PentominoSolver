using PentominoSolver.Pentominos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PentominoSolver
{
    class Program
    {
        private readonly static List<IPentomino> pieceTypes = new List<IPentomino>()
        {
            new F(),
            new Fp(),
            new I(),
            new L(),
            new Lp(),
            new N(),
            new Np(),
            new P(),
            new Pp(),
            new T(),
            new U(),
            new V(),
            new W(),
            new X(),
            new Y(),
            new Yp(),
            new Z(),
            new Zp()
        };

        static void Main()
        {
            var pieces = GetPieces();

            var (n, rectangle) = ExactAlgorithm.Solve(pieces);

            if (rectangle != null)
            {
                Console.WriteLine($"Aggregated length of cuts needed to solve the problem: {n}. First solution:");
                var colors = Enum.GetValues(typeof(ConsoleColor)).OfType<ConsoleColor>().Except(new List<ConsoleColor>() { ConsoleColor.Black }).ToList();
                for (int i = 0; i < rectangle.GetLength(0); i++)
                {
                    for (int j = 0; j < rectangle.GetLength(1); j++)
                    {
                        Console.ForegroundColor = colors[rectangle[i, j] % colors.Count()];
                        Console.Write("x ");
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("There are no solutions");
            }

            Console.ReadKey();
        }

        private static List<IPentomino> GetPieces()
        {
            while (true)
            {
                Console.WriteLine("Specify the number of pieces to generate or the number of individual pieces (delimited with commas):");
                var input = Console.ReadLine();
                var numbers = input.Split(',');
                if (numbers.Length == 18)
                {
                    var error = false;
                    var pieces = new List<IPentomino>();
                    for (int i = 0; i < 18; i++)
                    {
                        if (int.TryParse(numbers[i], out var number) && number >= 0)
                        {
                            for (int j = 0; j < number; j++)
                                pieces.Add(pieceTypes[i]);
                        }
                        else
                        {
                            error = true;
                            break;
                        }
                    }
                    if (!error)
                        return pieces;
                }
                else if (int.TryParse(input, out var count) && count >= 0)
                {
                    return GeneratePieces(count);
                }
            }
        }

        private static List<IPentomino> GeneratePieces(int count)
        {
            var pieces = new List<IPentomino>();
            var rand = new Random();
            for (int i = 0; i < count; i++)
                pieces.Add(pieceTypes[rand.Next(pieceTypes.Count())]);
            return pieces;
        }
    }
}
