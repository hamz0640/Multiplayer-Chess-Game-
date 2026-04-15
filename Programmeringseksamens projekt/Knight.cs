using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programmeringseksamens_projekt.Enums;

namespace Programmeringseksamens_projekt
{
	public class Knight : Piece
	{
		public Knight(PieceColor color, (int, int) position) : base(color, PieceType.Knight, position) { }

		public override List<Move> GetLegalMoves(Board board)
		{
			List<Move> legalMoves = new List<Move>();
			int[] dRow = { -2, -1, 1, 2, 2, 1, -1, -2 };
			int[] dCol = { 1, 2, 2, 1, -1, -2, -2, -1 };
			for (int i = 0; i < 8; i++)
			{
				int newRow = Position.row + dRow[i];
				int newCol = Position.col + dCol[i];
				if (board.IsInBounds(newRow, newCol))
				{
					Piece targetPiece = board.Grid[newRow, newCol];
					if (targetPiece == null || targetPiece.Color != this.Color)
					{
						legalMoves.Add(new Move((Position.row, Position.col), (newRow, newCol), MoveType.Normal));
					}
				}
			}
			return legalMoves;
		}
	}
}
