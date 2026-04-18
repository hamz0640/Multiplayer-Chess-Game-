using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeringseksamens_projekt
{
	public class Enums
	{
        public enum MessageType
		{
			Move,
			Resign,
			OfferDraw,
			AcceptDraw,
			DeclineDraw,
		}
		public enum PieceColor
		{
			White,
			Black
		}
		public enum PieceType
		{
			Pawn,
			Rook,
			Knight,
			Bishop,
			Queen,
			King
		}
		public enum MoveType
		{
			Normal,
			EnPassant,
			Promotion,
			CastlingKingside,
			CastlingQueenside
		}

		public enum WinResult
		{
			None,
			WhiteWin,
			BlackWin,
			Stalemate,
		}
	}
}
