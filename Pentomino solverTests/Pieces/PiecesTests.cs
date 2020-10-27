using Microsoft.VisualStudio.TestTools.UnitTesting;
using PentominoSolver.Pentominos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pentominos.Tests
{
    [TestClass()]
    public class PiecesTests
    {
        [TestMethod()]
        public void OrientationsTest()
        {
            var allPieces = new List<IPiece>
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

            var allOrientations = allPieces
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
        public void AreasTest()
        {
            var allPieces = new List<IPiece>
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

            var allOrientations = allPieces
                .SelectMany(x => x.Orientations)
                .ToList();

            foreach (var orientation in allOrientations)
                Assert.AreEqual(5, orientation.Cast<int>().Sum());
        }
    }
}