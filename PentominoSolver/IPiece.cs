﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentominoSolver
{
    public interface IPiece
    {
        int[][,] Orientations { get; }
    }
}
