using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pieces.Trominos
{
    public class L : IPiece
    {
        public int Size => 3;

        public int[][,] Orientations
        {
            get
            {
                return new int[4][,]
                {
                    new int[2,2]
                    {
                        { 1, 0 },
                        { 1, 1 },
                    },
                    new int[2,2]
                    {
                        { 1, 1 },
                        { 1, 0 }
                    },
                    new int[2,2]
                    {
                        { 1, 1 },
                        { 0, 1 },
                    },
                    new int[2,2]
                    {
                        { 0, 1 },
                        { 1, 1 }
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
                            new Dominos.Domino()
                        }
                    ),
                    (
                        2,
                        new List<IPiece>()
                        {
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
