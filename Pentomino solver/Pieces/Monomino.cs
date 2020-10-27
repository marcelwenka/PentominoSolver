using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver
{
    public class Monomino : IPiece
    {
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

        public IPiece Clone()
        {
            return new Monomino();
        }
    }
}
