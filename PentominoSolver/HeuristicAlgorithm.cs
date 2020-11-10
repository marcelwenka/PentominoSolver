using DlxLib;
using PentominoSolver.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Metadata;
using System.Threading;

namespace PentominoSolver
{
    public static class HeuristicAlgorithm
    {
        private static CancellationTokenSource cancellationTokenSource;

        private static List<int> bestSolutionIndexes;

        private static ulong currentIteration;
        private static ulong maxIteration;

        public static (int, int[,]) Solve(List<PieceQuantity> pieceQuantities)
        {
            cancellationTokenSource = new CancellationTokenSource();
            bestSolutionIndexes = new List<int>();
            currentIteration = 0;
            maxIteration = 0;

            var piecesCount = pieceQuantities.Sum(x => x.Quantity);
            var rectangle = SolvingHelper.GenerateRectangle(pieceQuantities);
            maxIteration = (ulong)Math.Round(Math.Pow(pieceQuantities.Sum(x => x.Piece.Orientations.Length * x.Quantity), 3));

            var tempMatrix = new List<int[]>();
            for (int i = 0, j = 0; i < pieceQuantities.Count; i++)
                for (int k = 0; k < pieceQuantities[i].Quantity; j++, k++)
                    tempMatrix.AddRange(SolvingHelper.CreateRows(pieceQuantities[i].Piece, rectangle, piecesCount, j));

            var matrix = SolvingHelper.ConvertToArray(tempMatrix);
            if (matrix == null)
                return (0, null);

            var dlx = new Dlx(cancellationTokenSource.Token);
            dlx.SolutionFound += DlxSolutionFound;
            dlx.SearchStep += DlxSearchStep;
            dlx.Solve(matrix).ToList();

            List<IPiece> pieces = pieceQuantities
                .SelectMany(x =>
                {
                    var list = new List<IPiece>();
                    for (int i = 0; i < x.Quantity; i++)
                        list.Add(x.Piece);
                    return list;
                })
                .Cast<IPiece>()
                .ToList();
            var pieceNumber = 1;
            foreach (var index in bestSolutionIndexes)
            {
                for (int i = 0; i < piecesCount; i++)
                {
                    if (matrix[index, i] == 1)
                    {
                        if (!pieces.Remove(GetPiece(pieceQuantities, i)))
                            throw new InvalidOperationException("Internal error. Could not find piece to remove.");
                        break;
                    }
                }
                for (int i = piecesCount; i < matrix.GetLength(1); i++)
                {
                    if (matrix[index, i] == 1)
                        rectangle[(i - piecesCount) / rectangle.GetLength(1), (i - piecesCount) % rectangle.GetLength(1)] = pieceNumber;
                }
                pieceNumber++;
            }

            var cutLength = 0;
            for (int row = 0; row < rectangle.GetLength(0); row++)
            {
                for (int column = 0; column < rectangle.GetLength(1); column++)
                {
                    if (rectangle[row,column] == 0)
                    {
                        cutLength += PutBestPiece(pieces, rectangle, row, column, pieceNumber);
                        pieceNumber++;
                    }
                }
            }

            return (cutLength, rectangle);
        }

        private static int PutBestPiece(List<IPiece> pieces, int[,] rectangle, int row, int column, int pieceNumber)
        {
            IPiece bestPiece = null;
            int[,] bestOrientation = null;
            int bestOrientationOffset = 0;
            foreach (var piece in pieces)
                foreach (var orientation in piece.Orientations)
                    if (FitPiece(orientation, rectangle, row, column, out int offset))
                        if (bestPiece == null || piece.Size > bestPiece.Size)
                        {
                            bestPiece = piece;
                            bestOrientation = orientation;
                            bestOrientationOffset = offset;
                        }

            if (bestPiece != null)
            {
                PutPiece(bestOrientation, rectangle, row, column, bestOrientationOffset, pieceNumber);
                pieces.Remove(bestPiece);
                return 0;
            }

            (int CutLength, List<IPiece> Pieces) bestCut = (int.MaxValue, null);
            IPiece bestCutPiece = null;

            foreach (var piece in pieces)
            {
                foreach (var cut in piece.Cuts)
                {
                    foreach (var cutPiece in cut.Pieces)
                    {
                        foreach (var orientation in cutPiece.Orientations)
                        {
                            if (bestCutPiece == null || (cut.CutLength < bestCut.CutLength || (cut.CutLength == bestCut.CutLength && cutPiece.Size > bestCutPiece.Size)))
                            {
                                if (FitPiece(orientation, rectangle, row, column, out int offset))
                                {
                                    bestPiece = piece;
                                    bestCut = cut;
                                    bestCutPiece = cutPiece;
                                    bestOrientation = orientation;
                                    bestOrientationOffset = offset;
                                }
                            }
                        }
                    }
                }
            }

            PutPiece(bestOrientation, rectangle, row, column, bestOrientationOffset, pieceNumber);
            var newPieces = bestCut.Pieces.ToList();
            newPieces.Remove(bestCutPiece);
            pieces.Remove(bestPiece);
            pieces.AddRange(newPieces);
            return bestCut.CutLength;
        }

        private static void PutPiece(int[,] pieceOrientation, int[,] rectangle, int row, int column, int offset, int pieceNumber)
        {
            for (int i = 0; i < pieceOrientation.GetLength(0); i++)
                for (int j = 0; j < pieceOrientation.GetLength(1); j++)
                    if (pieceOrientation[i, j] == 1)
                        rectangle[row + i, column - offset + j] = pieceNumber;
        }

        private static bool FitPiece(int[,] pieceOrientation, int[,] rectangle, int row, int column, out int offset)
        {
            offset = 0;
            for (int i = 0; i < pieceOrientation.GetLength(1); i++)
            {
                if (pieceOrientation[0, i] == 1)
                {
                    offset = i;
                    break;
                }
            }

            if (column - offset >= 0 && column - offset + pieceOrientation.GetLength(1) <= rectangle.GetLength(1))
            {
                if (row + pieceOrientation.GetLength(0) <= rectangle.GetLength(0))
                {
                    for (int i = 0; i < pieceOrientation.GetLength(0); i++)
                        for (int j = 0; j < pieceOrientation.GetLength(1); j++)
                            if (pieceOrientation[i, j] == 1 && rectangle[row + i, column - offset + j] >= 1)
                                return false;

                    return true;
                }
            }

            return false;
        }

        private static IPiece GetPiece(List<PieceQuantity> pieceQuantities, int i)
        {
            var index = 0;
            foreach (var pieceQuantity in pieceQuantities)
            {
                if (i < index + pieceQuantity.Quantity)
                    return pieceQuantity.Piece;
                else
                    index += pieceQuantity.Quantity;
            }

            throw new InvalidOperationException("Internal error. Index of piece out of range.");
        }

        private static void DlxSearchStep(object sender, SearchStepEventArgs e)
        {
            if (e.RowIndexes.Count() > bestSolutionIndexes.Count)
                bestSolutionIndexes = new List<int>(e.RowIndexes);

            if (++currentIteration > maxIteration)
                cancellationTokenSource.Cancel();
        }

        private static void DlxSolutionFound(object sender, SolutionFoundEventArgs e)
        {
            cancellationTokenSource.Cancel();
        }
    }
}
