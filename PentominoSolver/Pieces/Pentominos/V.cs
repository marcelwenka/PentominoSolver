using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pieces.Pentominos
{
    public class V : IPiece
    {
        public int Size => 5;

        public int[][,] Orientations
        {
            get
            {
                return new int[4][,]
                {
                    new int[3,3]
                    {
                        { 1, 0, 0 },
                        { 1, 0, 0 },
                        { 1, 1, 1 }
                    },
                    new int[3,3]
                    {
                        { 1, 1, 1 },
                        { 1, 0, 0 },
                        { 1, 0, 0 }
                    },
                    new int[3,3]
                    {
                        { 1, 1, 1 },
                        { 0, 0, 1 },
                        { 0, 0, 1 }
                    },
                    new int[3,3]
                    {
                        { 0, 0, 1 },
                        { 0, 0, 1 },
                        { 1, 1, 1 }
                    },
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
                            new Tetrominos.L()
                        }
                    ),
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Monominos.Monomino(),
                            new Tetrominos.J()
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
                        2,
                        new List<IPiece>()
                        {
                            new Dominos.Domino(),
                            new Dominos.Domino(),
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
