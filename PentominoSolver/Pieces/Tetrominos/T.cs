using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Tetrominos
{
    public class T : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[4][,]
                {
                    new int[2,3]
                    {
                        { 1, 1, 1 },
                        { 0, 1, 0 },
                    },
                    new int[3,2]
                    {
                        { 0, 1 },
                        { 1, 1 },
                        { 0, 1 }
                    },
                    new int[2,3]
                    {
                        { 0, 1, 0 },
                        { 1, 1, 1 },
                    },
                    new int[3,2]
                    {
                        { 1, 0 },
                        { 1, 1 },
                        { 1, 0 }
                    }
                };
            }
        }

        public string Type => "Tetromino.T";
    }
}
