using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pentominos
{
    public class Lp : IPentomino
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[4][,]
                {
                    new int[4,2]
                    {
                        { 0, 1 },
                        { 0, 1 },
                        { 0, 1 },
                        { 1, 1 }
                    },
                    new int[2,4]
                    {
                        { 1, 1, 1, 1 },
                        { 0, 0, 0, 1 }
                    },
                    new int[4,2]
                    {
                        { 1, 1 },
                        { 1, 0 },
                        { 1, 0 },
                        { 1, 0 }
                    },
                    new int[2,4]
                    {
                        { 1, 0, 0, 0 },
                        { 1, 1, 1, 1 }
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
                            new Tetrominos.I()
                        }
                    ),
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
                            new Domino(),
                            new Trominos.I()
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
                            new Domino(),
                            new Domino(),
                            new Monomino()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Trominos.L(),
                            new Monomino(),
                            new Monomino()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Trominos.I(),
                            new Monomino(),
                            new Monomino()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Domino(),
                            new Domino(),
                            new Monomino()
                        }
                    ),
                    (
                        3,
                        new List<IPiece>()
                        {
                            new Domino(),
                            new Monomino(),
                            new Monomino(),
                            new Monomino()
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
                    ),
                };
            }
        }
    }
}
