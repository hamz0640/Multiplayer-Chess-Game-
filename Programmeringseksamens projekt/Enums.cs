using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeringseksamens_projekt
{
    public class Enums
    {
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
        public enum GameState
        {
            Ongoing,
            Check,
            Checkmate,
            Stalemate
        }
        public enum  MoveType
        {
            Normal,
            Castling,
            EnPassant,
            Promotion,
            CastlingKingside,
            CastlingQueenside
        }
    }
}
