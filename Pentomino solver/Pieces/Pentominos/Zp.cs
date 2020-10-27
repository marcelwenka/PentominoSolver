using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pentominos
{
    public class Zp : IPentomino
    {
        public int[][,] Orientations
        {
            get
            {
                return new int[2][,]
                {
                    new int[3,3]
                    {
                        { 0, 1, 1 },
                        { 0, 1, 0 },
                        { 1, 1, 0 }
                    },
                    new int[3,3]
                    {
                        { 1, 0, 0 },
                        { 1, 1, 1 },
                        { 0, 0, 1 }
                    },
                };
            }
        }

        public List<(int, List<IPiece>)> Cuts => new List<(int, List<IPiece>)>();

        public IPiece Clone()
        {
            return new Zp();
        }
    }
}
