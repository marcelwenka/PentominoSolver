using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Tetrominos
{
    public class I : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[2][,]
                {
                    new int[4,1]
                    {
                        { 1 },
                        { 1 },
                        { 1 },
                        { 1 }
                    },
                    new int[1,4]
                    {
                        { 1, 1, 1, 1 }
                    }
                };
            }
        }

        public string Type => "Tetromino.I";
    }
}
