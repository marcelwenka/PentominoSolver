using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver
{
    public class Domino : IPiece
    {
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

        public IPiece Clone()
        {
            return new Domino();
        }
    }
}
