using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pentominos
{
    public class Yp : IPentomino
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[4][,]
                {
                    new int[4,2]
                    {
                        { 1, 0 },
                        { 1, 1 },
                        { 1, 0 },
                        { 1, 0 }
                    },
                    new int[2,4]
                    {
                        { 0, 1, 0, 0 },
                        { 1, 1, 1, 1 }
                    },
                    new int[4,2]
                    {
                        { 0, 1 },
                        { 0, 1 },
                        { 1, 1 },
                        { 0, 1 }
                    },
                    new int[2,4]
                    {
                        { 1, 1, 1, 1 },
                        { 0, 0, 1, 0 }
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
                            new Tetrominos.J()
                        }
                    ),
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Monomino(),
                            new Tetrominos.I()
                        }
                    ),
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Monomino(),
                            new Tetrominos.T()
                        }
                    ),
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Domino(),
                            new Trominos.L()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Monomino(),
                            new Monomino(),
                            new Trominos.I()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Monomino(),
                            new Monomino(),
                            new Trominos.L()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Monomino(),
                            new Domino(),
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
                            new Domino()
                        }
                    ),
                    (
                        4,
                        new List<IPiece>()
                        {
                            new Monomino(),
                            new Monomino(),
                            new Monomino(),
                            new Monomino(),
                            new Monomino()
                        }
                    )
                };
            }
        }

        public IPiece Clone()
        {
            return new Yp();
        }
    }
}
