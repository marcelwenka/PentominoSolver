using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Trominos
{
    public class I : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[2][,]
                {
                    new int[3,1]
                    {
                        { 1 },
                        { 1 },
                        { 1 }
                    },
                    new int[1,3]
                    {
                        { 1, 1, 1 }
                    }
                };
            }
        }
    }
}
