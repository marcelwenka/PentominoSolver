using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Tetrominos
{
    public class S : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[2][,]
                {
                    new int[2,3]
                    {
                        { 0, 1, 1 },
                        { 1, 1, 0 },
                    },
                    new int[3,2]
                    {
                        { 1, 0 },
                        { 1, 1 },
                        { 0, 1 }
                    }
                };
            }
        }

        public IPiece Clone()
        {
            return new S();
        }
    }
}
