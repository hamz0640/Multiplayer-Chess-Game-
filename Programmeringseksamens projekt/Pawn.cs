using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programmeringseksamens_projekt.Enums;
using System.Diagnostics;

namespace Programmeringseksamens_projekt
{
    public class Pawn : Piece
    {
        public Pawn(PieceColor color, (int, int) position) : base(color, PieceType.Pawn, position) { }

        public override List<Move> GetLegalMoves(Board board, bool includeCastling = true)
        {
            int direction = (this.Color == PieceColor.White) ? 1 : -1;
            int lastRow = (this.Color == PieceColor.White) ? 7 : 0;
            List<Move> legalMoves = new List<Move>();
            int newRow;

            // Double push
            if (!this.HasMoved)
            {
                newRow = Position.row + (direction * 2);
                if (board.IsInBounds(newRow, Position.col))
                    if (board.Grid[newRow, Position.col] == null && board.Grid[newRow - direction, Position.col] == null)
                        legalMoves.Add(new Move((Position.row, Position.col), (newRow, Position.col), MoveType.Normal));
            }

            // Single push
            newRow = Position.row + direction;
            if (board.IsInBounds(newRow, Position.col) && board.Grid[newRow, Position.col] == null)
            {
                if (newRow == lastRow)
                    legalMoves.Add(new Move((Position.row, Position.col), (newRow, Position.col), MoveType.Promotion, PieceType.Queen));
                else
                    legalMoves.Add(new Move((Position.row, Position.col), (newRow, Position.col), MoveType.Normal));
            }

            // Diagonal captures
            int captureRow = Position.row + direction;
            if (board.IsInBounds(captureRow, Position.col + 1))
            {
                Piece targetRight = board.Grid[captureRow, Position.col + 1];
                if (targetRight != null && targetRight.Color != this.Color)
                {
                    if (captureRow == lastRow)
                    {
                        legalMoves.Add(new Move((Position.row, Position.col), (captureRow, Position.col + 1), MoveType.Promotion, PieceType.Queen));   
                    }

                    else
                        legalMoves.Add(new Move((Position.row, Position.col), (captureRow, Position.col + 1), MoveType.Normal));
                }
            }
            if (board.IsInBounds(captureRow, Position.col - 1))
            {
                Piece targetLeft = board.Grid[captureRow, Position.col - 1];
                if (targetLeft != null && targetLeft.Color != this.Color)
                {
                    if (captureRow == lastRow)
                        legalMoves.Add(new Move((Position.row, Position.col), (captureRow, Position.col - 1), MoveType.Promotion, PieceType.Queen));
                    else
                        legalMoves.Add(new Move((Position.row, Position.col), (captureRow, Position.col - 1), MoveType.Normal));
                }
            }

            // En passant
            if (board.MoveHistory.Count != 0)
            {
                Move lastMove = board.MoveHistory.Last().Move;

                if (board.IsInBounds(Position.row, Position.col + 1))
                {
                    Piece rightPawn = board.Grid[Position.row, Position.col + 1];
                    if (rightPawn != null && rightPawn.Type == PieceType.Pawn && rightPawn.Color != this.Color
                        && lastMove.To == (Position.row, Position.col + 1)
                        && Math.Abs(lastMove.From.row - lastMove.To.row) == 2)
                        legalMoves.Add(new Move((Position.row, Position.col), (captureRow, Position.col + 1), MoveType.EnPassant));
                }

                if (board.IsInBounds(Position.row, Position.col - 1))
                {
                    Piece leftPawn = board.Grid[Position.row, Position.col - 1];
                    if (leftPawn != null && leftPawn.Type == PieceType.Pawn && leftPawn.Color != this.Color
                        && lastMove.To == (Position.row, Position.col - 1)
                        && Math.Abs(lastMove.From.row - lastMove.To.row) == 2)
                        legalMoves.Add(new Move((Position.row, Position.col), (captureRow, Position.col - 1), MoveType.EnPassant));
                }
            }

            return legalMoves;
        }
    }
}