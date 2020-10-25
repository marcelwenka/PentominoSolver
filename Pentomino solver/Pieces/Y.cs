using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentomino_solver.Pieces
{
    public class Y : IPiece
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[4][,]
                {
                    new int[4,2]
                    {
                        { 0, 1 },
                        { 1, 1 },
                        { 0, 1 },
                        { 0, 1 }
                    },
                    new int[2,4]
                    {
                        { 1, 1, 1, 1 },
                        { 0, 1, 0, 0 }
                    },
                    new int[4,2]
                    {
                        { 1, 0 },
                        { 1, 0 },
                        { 1, 1 },
                        { 1, 0 }
                    },
                    new int[2,4]
                    {
                        { 0, 0, 1, 0 },
                        { 1, 1, 1, 1 }
                    }
                };
            }
        }

        public IPiece Clone()
        {
            return new Y();
        }
    }
}
