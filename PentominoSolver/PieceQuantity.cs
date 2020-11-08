using PentominoSolver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver
{
    public class PieceQuantity
    {
        public IPiece Piece { get; set; }
        public int Quantity { get; set; }

        public PieceQuantity(IPiece piece, int quantity)
        {
            Piece = piece;
            Quantity = quantity;
        }
    }
}
