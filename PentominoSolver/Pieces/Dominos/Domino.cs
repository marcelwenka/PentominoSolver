using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pieces.Dominos
{
    public class Domino : IPiece
    {
        public int Size => 2;

        public int[][,] Orientations
        {
            get
            {
                return new int[2][,]
                {
                    new int[2,1]
                    {
                        { 1 },
                        { 1 }
                    },
                    new int[1,2]
                    {
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
                            new Monominos.Monomino()
                        }
                    )
                };
            }
        }
    }
}
