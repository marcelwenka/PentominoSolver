using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver
{
    public interface IPentomino : IPiece
    {
        List<(int CutLength, List<IPiece> Pieces)> Cuts { get; }
    }
}