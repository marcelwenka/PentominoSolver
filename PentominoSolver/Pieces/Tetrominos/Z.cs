using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Tetrominos
{
    public class Z : IPiece
    {
        public int Size => 4;

        public int[][,] Orientations
        {
            get
            {
                return new int[2][,]
                {
                    new int[2,3]
                    {
                        { 1, 1, 0 },
                        { 0, 1, 1 },
                    },
                    new int[3,2]
                    {
                        { 0, 1 },
                        { 1, 1 },
                        { 1, 0 }
                    }
                };
            }
        }

        public List<(int, List<IPiece>)> Cuts
        {
            get
            {
                return new List<(int, List<IPiece>)>()
                {
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Monomino(),
                            new Trominos.L()
                        }
                    ),
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Domino(),
                            new Domino()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Monomino(),
                            new Monomino(),
                            new Domino()
                        }
                    ),
                    (
                        3,
                        new List<IPiece>()
                        {
                            new Monomino(),
                            new Monomino(),
                            new Monomino(),
                            new Monomino()
                        }
                    )
                };
            }
        }
    }
}
