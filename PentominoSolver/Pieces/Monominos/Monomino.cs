using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pieces.Monominos
{
    public class Monomino : IPiece
    {
        public int Size => 1;

        public int[][,] Orientations
        {
            get
            {
                return new int[1][,]
                {
                    new int[1,1]
                    {
                        { 1 }
                    }
                };
            }
        }

        public List<(int, List<IPiece>)> Cuts
        {
            get
            {
                return new List<(int, List<IPiece>)>();
            }
        }
    }
}
