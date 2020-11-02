using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Tetrominos
{
    public class O : IPiece
    {
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
    }
}
