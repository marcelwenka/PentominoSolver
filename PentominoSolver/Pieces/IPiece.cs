using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver.Pieces
{
    public interface IPiece
    {
        int[][,] Orientations { get; }

        List<(int CutLength, List<IPiece> Pieces)> Cuts { get; }

        int Size { get; }
    }
}
