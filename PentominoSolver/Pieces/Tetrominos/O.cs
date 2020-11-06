using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Tetrominos
{
    public class O : IPiece
    {
        public int Size => 4;

        public int[][,] Orientations
        {
            get
            {
                return new int[1][,]
                {
                    new int[2,2]
                    {
                        { 1, 1 },
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
                        2,
                        new List<IPiece>()
                        {
                            new Domino(),
                            new Domino()
                        }
                    ),
                    (
                        3,
                        new List<IPiece>()
                        {
                            new Domino(),
                            new Monomino(),
                            new Monomino()
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
