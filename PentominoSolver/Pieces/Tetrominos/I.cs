﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pieces.Tetrominos
{
    public class I : IPiece
    {
        public int Size => 4;

        public int[][,] Orientations
        {
            get
            {
                return new int[2][,]
                {
                    new int[4,1]
                    {
                        { 1 },
                        { 1 },
                        { 1 },
                        { 1 }
                    },
                    new int[1,4]
                    {
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
                            new Monominos.Monomino(),
                            new Trominos.I()
                        }
                    ),
                    (
                        1,
                        new List<IPiece>()
                        {
                            new Dominos.Domino(),
                            new Dominos.Domino(),
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
                            new Monominos.Monomino(),
                            new Monominos.Monomino(),
                            new Dominos.Domino()
                        }
                    ),
                    (
                        3,
                        new List<IPiece>()
                        {
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
