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

		public override List<Move> GetLegalMoves(Board board)
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
			// Kingside castling
			if (Color == PieceColor.White ? board.WhiteCanCastleKingside : board.BlackCanCastleKingside)
			{
				if (board.IsEmpty(Position.row, 5) && board.IsEmpty(Position.row, 6))
					legalMoves.Add(new Move((Position.row, Position.col), (Position.row, 6), MoveType.CastlingKingside));
			}

			// Queenside castling
			if (Color == PieceColor.White ? board.WhiteCanCastleQueenside : board.BlackCanCastleQueenside)
			{
				if (board.IsEmpty(Position.row, 1) && board.IsEmpty(Position.row, 2) && board.IsEmpty(Position.row, 3))
					legalMoves.Add(new Move((Position.row, Position.col), (Position.row, 2), MoveType.CastlingQueenside));
			}

			return legalMoves;
		}
	}
}
