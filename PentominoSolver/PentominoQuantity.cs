using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver
{
    public class PentominoQuantity
    {
        public IPentomino Pentomino { get; set; }
        public int Quantity { get; set; }

        public PentominoQuantity(IPentomino pentomino, int quantity)
        {
            Pentomino = pentomino;
            Quantity = quantity;
        }
    }
}
