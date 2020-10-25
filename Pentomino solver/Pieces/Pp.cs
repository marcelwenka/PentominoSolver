using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentomino_solver.Pieces
{
    public class Pp : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[4][,]
                {
                    new int[3,2]
                    {
                        { 1, 1 },
                        { 1, 1 },
                        { 0, 1 }
                    },
                    new int[2,3]
                    {
                        { 0, 1, 1 },
                        { 1, 1, 1 }
                    },
                    new int[3,2]
                    {
                        { 1, 0 },
                        { 1, 1 },
                        { 1, 1 }
                    },
                    new int[2,3]
                    {
                        { 1, 1, 1 },
                        { 1, 1, 0 }
                    }
                };
            }
        }

        public IPiece Clone()
        {
            return new Pp();
        }
    }
}
