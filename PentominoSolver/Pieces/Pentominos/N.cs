using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pieces.Pentominos
{
    public class N : IPiece
    {
        public int Size => 5;

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
                        { 1, 1 },
                        { 1, 0 }
                    },
                    new int[2,4]
                    {
                        { 1, 1, 0, 0 },
                        { 0, 1, 1, 1 }
                    },
                    new int[4,2]
                    {
                        { 0, 1 },
                        { 1, 1 },
                        { 1, 0 },
                        { 1, 0 }
                    },
                    new int[2,4]
                    {
                        { 1, 1, 1, 0 },
                        { 0, 0, 1, 1 }
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
                            new Monominos.Monomino(),
                            new Tetrominos.Z()
                        }
                    ),
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Dominos.Domino(),
                            new Trominos.L()
                        }
                    ),
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Dominos.Domino(),
                            new Trominos.I()
                        }
                    ),
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Tetrominos.J(),
                            new Monominos.Monomino()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Trominos.L(),
                            new Monominos.Monomino(),
                            new Monominos.Monomino()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Trominos.I(),
                            new Monominos.Monomino(),
                            new Monominos.Monomino()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Dominos.Domino(),
                            new Dominos.Domino(),
                            new Monominos.Monomino()
                        }
                    ),
                    (
                        3,
                        new List<IPiece>()
                        {
                            new Dominos.Domino(),
                            new Monominos.Monomino(),
                            new Monominos.Monomino(),
                            new Monominos.Monomino()
                        }
                    ),
                    (
                        4,
                        new List<IPiece>()
                        {
                            new Monominos.Monomino(),
                            new Monominos.Monomino(),
                            new Monominos.Monomino(),
                            new Monominos.Monomino(),
                            new Monominos.Monomino()
                        }
                    )
                };
            }
        }
    }
}
