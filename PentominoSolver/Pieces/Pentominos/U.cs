﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pieces.Pentominos
{
    public class U : IPiece
    {
        public int Size => 5;

        public int[][,] Orientations
        {
            get
            {
                return new int[4][,]
                {
                    new int[2,3]
                    {
                        { 1, 0, 1 },
                        { 1, 1, 1 }
                    },
                    new int[3,2]
                    {
                        { 1, 1 },
                        { 1, 0 },
                        { 1, 1 }
                    },
                    new int[2,3]
                    {
                        { 1, 1, 1 },
                        { 1, 0, 1 }
                    },
                    new int[3,2]
                    {
                        { 1, 1 },
                        { 0, 1 },
                        { 1, 1 }
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
                            new Dominos.Domino(),
                            new Trominos.L()
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
                        2,
                        new List<IPiece>()
                        {
                            new Monominos.Monomino(),
                            new Monominos.Monomino(),
                            new Trominos.L()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Monominos.Monomino(),
                            new Dominos.Domino(),
                            new Dominos.Domino()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Monominos.Monomino(),
                            new Monominos.Monomino(),
                            new Trominos.I()
                        }
                    ),
                    (
                        3,
                        new List<IPiece>()
                        {
                            new Monominos.Monomino(),
                            new Monominos.Monomino(),
                            new Monominos.Monomino(),
                            new Dominos.Domino()
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
