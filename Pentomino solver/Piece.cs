using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pentomino_solver
{
    public interface IPiece
    {
        int[][,] Orientations { get; }

        IPiece Clone();
    }
}
