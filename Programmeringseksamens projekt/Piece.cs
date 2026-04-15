using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programmeringseksamens_projekt.Enums;

namespace Programmeringseksamens_projekt
{
	public abstract class Piece
	{
		public PieceColor Color { get; set; }
		public PieceType Type { get; set; }
		public (int row, int col) Position { get; set; }
		public bool HasMoved { get; set; } = false;

		public Piece(PieceColor color, PieceType type, (int, int) position)
		{
			Color = color;
			Type = type;
			Position = position;
		}

		public abstract List<Move> GetLegalMoves(Board board);
	}
}
