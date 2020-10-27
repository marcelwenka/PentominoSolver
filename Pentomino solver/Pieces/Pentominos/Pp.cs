using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pentominos
{
    public class Pp : IPentomino
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

        public List<(int, List<IPiece>)> Cuts => new List<(int, List<IPiece>)>();

        public IPiece Clone()
        {
            return new Pp();
        }
    }
}
