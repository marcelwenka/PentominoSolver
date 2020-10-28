﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pentominos
{
    public class V : IPentomino
    {
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
                            new Monomino(),
                            new Tetrominos.L()
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
                    )

                };
            }
        }
        public IPiece Clone()
        {
            return new V();
        }
    }
}
