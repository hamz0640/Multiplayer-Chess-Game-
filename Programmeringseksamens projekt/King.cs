using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programmeringseksamens_projekt.Enums;

namespace Programmeringseksamens_projekt
{
	public class King : Piece
	{
		public King(PieceColor color, (int, int) position) : base(color, PieceType.King, position) { }

		public override List<Move> GetLegalMoves(Board board, bool includeCastling = true)
		{
			List<Move> legalMoves = new List<Move>();
			int[] dRow = { -1, -1, -1, 0, 0, 1, 1, 1 };
			int[] dCol = { -1, 0, 1, -1, 1, -1, 0, 1 };
			for (int dir = 0; dir < 8; dir++)
			{
				int newRow = Position.row + dRow[dir];
				int newCol = Position.col + dCol[dir];
				if (board.IsInBounds(newRow, newCol))
				{
					Piece targetPiece = board.Grid[newRow, newCol];
					if (targetPiece == null || targetPiece.Color != this.Color)
					{
						legalMoves.Add(new Move((Position.row, Position.col), (newRow, newCol), MoveType.Normal));
					}
				}
			}


            if (includeCastling)
            {
                // Kingside
                if (Color == PieceColor.White ? board.WhiteCanCastleKingside : board.BlackCanCastleKingside)
                {
                    if (board.IsEmpty(Position.row, 5) && board.IsEmpty(Position.row, 6))
                    {
                        PieceColor enemyColor = Color == PieceColor.White ? PieceColor.Black : PieceColor.White;
                        var enemyMoves = board.GetRawMoves(enemyColor); 
                        bool safe = !enemyMoves.Any(m => m.To.row == Position.row &&
                                    (m.To.col == 4 || m.To.col == 5 || m.To.col == 6));
                        if (safe)
                            legalMoves.Add(new Move((Position.row, Position.col), (Position.row, 6), MoveType.CastlingKingside));
                    }
                }

                // Queenside
                if (Color == PieceColor.White ? board.WhiteCanCastleQueenside : board.BlackCanCastleQueenside)
                {
                    if (board.IsEmpty(Position.row, 1) && board.IsEmpty(Position.row, 2) && board.IsEmpty(Position.row, 3))
                    {
                        PieceColor enemyColor = Color == PieceColor.White ? PieceColor.Black : PieceColor.White;
                        var enemyMoves = board.GetRawMoves(enemyColor);
                        bool safe = !enemyMoves.Any(m => m.To.row == Position.row &&
                                    (m.To.col == 2 || m.To.col == 3 || m.To.col == 4));
                        if (safe)
                            legalMoves.Add(new Move((Position.row, Position.col), (Position.row, 2), MoveType.CastlingQueenside));
                    }
                }
            }
            return legalMoves;
		}
	}
}
