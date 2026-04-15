using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programmeringseksamens_projekt.Enums;

namespace Programmeringseksamens_projekt
{
	public class Queen : Piece
	{
		public Queen(PieceColor color, (int, int) position) : base(color, PieceType.Queen, position) { }

		public override List<Move> GetLegalMoves(Board board)
		{
			List<Move> legalMoves = new List<Move>();
			// combine Rook and Bishp moves
			int[] dRow = { -1, -1, -1, 0, 0, 1, 1, 1 };
			int[] dCol = { -1, 0, 1, -1, 1, -1, 0, 1 };
			for (int dir = 0; dir < 8; dir++)
			{
				for (int step = 1; step < 8; step++)
				{
					int newRow = Position.row + dRow[dir] * step;
					int newCol = Position.col + dCol[dir] * step;
					if (!board.IsInBounds(newRow, newCol))
						break;
					Piece targetPiece = board.Grid[newRow, newCol];
					if (targetPiece == null)
					{
						legalMoves.Add(new Move((Position.row, Position.col), (newRow, newCol), MoveType.Normal));
					}
					else
					{
						if (targetPiece.Color != this.Color)
						{
							legalMoves.Add(new Move((Position.row, Position.col), (newRow, newCol), MoveType.Normal));
						}
						break; 
					}
				}
			}
			return legalMoves;
		}
	}
}
