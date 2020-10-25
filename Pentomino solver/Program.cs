using DlxLib;
using Pentomino_solver.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pentomino_solver
{
    class Program
    {
        private readonly static List<IPiece> pieceTypes = new List<IPiece>()
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

        static void Main(string[] args)
        {
            List<IPiece> pieces;
            try
            {
                pieces = DecodeArgs(args);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Specify the number of pieces to generate or the number of individual pieces (delimited with commas):");
                var input = Console.ReadLine();
                var numbers = input.Split(',');
                if (numbers.Length == 18)
                {
                    pieces = new List<IPiece>();
                    for (int i = 0; i < 18; i++)
                    {
                        if (int.TryParse(numbers[i], out var number))
                        {
                            for (int j = 0; j < number; j++)
                                pieces.Add(pieceTypes[i].Clone());
                        }
                        else
                        {
                            Console.WriteLine("Cannot parse given input.");
                            return;
                        }
                    }
                }
                else if (!int.TryParse(input, out var count) || count < 1)
                {
                    Console.WriteLine("Cannot parse given input.");
                    return;
                }
                else
                {
                    pieces = GeneratePieces(count);
                }
            }

            var rectangle = GenerateRectangle(pieces);
            var tempMatrix = new List<int[]>();

            for (int i = 0; i < pieces.Count; i++)
                tempMatrix.AddRange(CreateRows(pieces, rectangle, i));

            var matrix = ConvertToArray(tempMatrix);

            var dlx = new Dlx();
            var solutions = dlx.Solve(matrix);

            if (solutions != null && solutions.Any())
            {
                var solution = solutions.First();
                var colors = Enum.GetValues(typeof(ConsoleColor)).OfType<ConsoleColor>().ToList();
                var rand = new Random();
                foreach (var index in solution.RowIndexes)
                {
                    var colorNumber = rand.Next(colors.Count);
                    for (int i = pieces.Count; i < matrix.GetLength(1); i++)
                    {
                        if (matrix[index, i] == 1)
                            rectangle[(i - pieces.Count) / rectangle.GetLength(1), (i - pieces.Count) % rectangle.GetLength(1)] = colorNumber;
                    }
                }

                for (int i = 0; i < rectangle.GetLength(0); i++)
                {
                    for (int j = 0; j < rectangle.GetLength(1); j++)
                    {
                        Console.ForegroundColor = colors[rectangle[i, j]];
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

        private static int[,] ConvertToArray(List<int[]> list)
        {
            if (list == null || !list.Any())
                throw new InvalidOperationException("List cannot be null or empty.");
            var length = list.First().Length;
            if (!list.All(x => x.Length == length))
                throw new InvalidOperationException("All arrays have to be of the same size.");

            var array = new int[list.Count, list.First().Length];
            for (int i = 0; i < list.Count; i++)
                for (int j = 0; j < length; j++)
                    array[i,j] = list[i][j];

            return array;
        }

        private static List<IPiece> DecodeArgs(string[] args)
        {
            if (args.Length == 1)
            {
                if (int.TryParse(args[0], out int count))
                    return GeneratePieces(count);
                else
                    throw new ArgumentException("Cannot parse the number of pieces to generate.");
            }
            else if (args.Length == 18)
            {
                // todo 18 argumentów
                return null;
            }
            else
            {
                throw new ArgumentException("Wrong input data.");
            }
        }

        private static List<IPiece> GeneratePieces(int count)
        {
            var pieces = new List<IPiece>();
            var rand = new Random();
            for (int i = 0; i < count; i++)
                pieces.Add(pieceTypes[rand.Next(pieceTypes.Count())].Clone());
            return pieces;
        }

        private static int[,] GenerateRectangle(List<IPiece> pieces)
        {
            var area = pieces.Count() * 5;
            var dim1 = 1;
            var dim2 = area;

            for (int i = 2; i * i <= area; i++)
            {
                if (area % i == 0)
                {
                    dim1 = i;
                    dim2 = area / i;
                }
            }

            return new int[dim1, dim2];
        }

        private static List<int[]> CreateRows(List<IPiece> pieces, int[,] rectangle, int pieceNumber)
        {
            var piece = pieces[pieceNumber];
            var piecesCount = pieces.Count;
            var rectangleRows = rectangle.GetLength(0);
            var rectangleColumns = rectangle.GetLength(1);
            var rows = new List<int[]>();

            foreach (var orientation in piece.Orientations)
            {
                for (int i = 0; i <= rectangleRows - orientation.GetLength(0); i++)
                {
                    for (int j = 0; j <= rectangleColumns - orientation.GetLength(1); j++)
                    {
                        var row = new int[piecesCount + rectangle.Length];
                        row[pieceNumber] = 1;

                        for (int k = 0; k < orientation.GetLength(0); k++)
                        {
                            for (int l = 0; l < orientation.GetLength(1); l++)
                            {
                                if (orientation[k, l] == 1)
                                    row[piecesCount + (i + k) * rectangleColumns + j + l] = 1;
                            }
                        }

                        rows.Add(row);
                    }
                }
            }

            return rows;
        }
    }
}
