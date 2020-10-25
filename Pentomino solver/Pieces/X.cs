using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentomino_solver.Pieces
{
    public class X : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[1][,]
                {
                    new int[3,3]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 1 },
                        { 0, 1, 0 }
                    }
                };
            }
        }

        public IPiece Clone()
        {
            return new X();
        }
    }
}
