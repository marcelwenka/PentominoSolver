using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace PentominoSolver.Pentominos.Tests
{
    [TestClass()]
    public class PiecesTests
    {
        [TestMethod()]
        [DynamicData(nameof(GetPieces))]
        public void OrientationsTest(List<IPiece> pieces, int _)
        {
            var allOrientations = pieces
                .SelectMany(x => x.Orientations)
                .ToList();

            for (int i = 0; i < allOrientations.Count() - 1; i++)
            {
                for (int j = i + 1; j < allOrientations.Count(); j++)
                {
                    if (allOrientations[i].GetLength(0) == allOrientations[j].GetLength(0)
                        && allOrientations[i].GetLength(1) == allOrientations[j].GetLength(1)
                        && allOrientations[i].Cast<int>().SequenceEqual(allOrientations[j].Cast<int>()))
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        [TestMethod()]
        [DynamicData(nameof(GetPieces))]
        public void AreaTest(List<IPiece> pieces, int size)
        {
            foreach (var piece in pieces)
                foreach (var orientation in piece.Orientations)
                    Assert.AreEqual(size, orientation.Cast<int>().Sum());
        }

        [TestMethod()]
        [DynamicData(nameof(GetPieces))]
        public void SizeTest(List<IPiece> pieces, int _)
        {
            var allOrientations = pieces
                .SelectMany(x => x.Orientations)
                .ToList();

            foreach (var piece in pieces)
                foreach (var orientation in piece.Orientations)
                    Assert.AreEqual(piece.Size, orientation.Cast<int>().Sum());
        }

        [TestMethod()]
        [DynamicData(nameof(GetPieces))]
        public void CutSizesTest(List<IPiece> pieces, int _)
        {
            var allOrientations = pieces
                .SelectMany(x => x.Orientations)
                .ToList();

            foreach (var piece in pieces)
                foreach (var cut in piece.Cuts)
                    Assert.AreEqual(piece.Size, cut.Pieces.Sum(x => x.Size));
        }

        public static IEnumerable<object[]> GetPieces
        {
            get
            {
                return new[]
                {
                    new object[] {
                        new List<IPiece>
                        {
                            new Pentominos.F(),
                            new Pentominos.Fp(),
                            new Pentominos.I(),
                            new Pentominos.L(),
                            new Pentominos.Lp(),
                            new Pentominos.N(),
                            new Pentominos.Np(),
                            new Pentominos.P(),
                            new Pentominos.Pp(),
                            new Pentominos.T(),
                            new Pentominos.U(),
                            new Pentominos.V(),
                            new Pentominos.W(),
                            new Pentominos.X(),
                            new Pentominos.Y(),
                            new Pentominos.Yp(),
                            new Pentominos.Z(),
                            new Pentominos.Zp()
                        },
                        5
                    },
                    new object[] {
                        new List<IPiece>
                        {
                            new Tetrominos.I(),
                            new Tetrominos.J(),
                            new Tetrominos.L(),
                            new Tetrominos.O(),
                            new Tetrominos.S(),
                            new Tetrominos.T(),
                            new Tetrominos.Z()
                        },
                        4
                    },
                    new object[] {
                        new List<IPiece>
                        {
                            new Trominos.I(),
                            new Trominos.L()
                        },
                        3
                    },
                    new object[] {
                        new List<IPiece>
                        {
                            new Domino(),
                        },
                        2
                    },
                    new object[] {
                        new List<IPiece>
                        {
                            new Monomino(),
                        },
                        1
                    }
                };
            }
        }
    }
}