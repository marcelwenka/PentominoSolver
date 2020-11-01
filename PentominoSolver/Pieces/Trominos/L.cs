using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Trominos
{
    public class L : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[4][,]
                {
                    new int[2,2]
                    {
                        { 1, 0 },
                        { 1, 1 },
                    },
                    new int[2,2]
                    {
                        { 1, 1 },
                        { 1, 0 }
                    },
                    new int[2,2]
                    {
                        { 1, 1 },
                        { 0, 1 },
                    },
                    new int[2,2]
                    {
                        { 0, 1 },
                        { 1, 1 }
                    }
                };
            }
        }

        public string Type => "Tromino.L";
    }
}
