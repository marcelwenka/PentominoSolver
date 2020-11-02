using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Tetrominos
{
    public class Z : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[2][,]
                {
                    new int[2,3]
                    {
                        { 1, 1, 0 },
                        { 0, 1, 1 },
                    },
                    new int[3,2]
                    {
                        { 0, 1 },
                        { 1, 1 },
                        { 1, 0 }
                    }
                };
            }
        }
    }
}
