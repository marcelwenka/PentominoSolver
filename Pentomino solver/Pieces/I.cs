using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentomino_solver.Pieces
{
    public class I : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[2][,]
                {
                    new int[1,5]
                    {
                        { 1, 1, 1, 1, 1 }
                    },
                    new int[5,1]
                    {
                        { 1 },
                        { 1 },
                        { 1 },
                        { 1 },
                        { 1 }
                    }
                };
            }
        }

        public IPiece Clone()
        {
            return new I();
        }
    }
}
