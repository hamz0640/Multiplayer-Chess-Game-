using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeringseksamens_projekt
{
    public class MoveRecord
    {
        public Move Move { get; set; }
        public Piece CapturedPiece { get; set; }
        public bool PieceHadMoved { get; set; }
        public (int, int)? PreviousEnPassantTarget { get; set; }
        public bool PreviousWhiteCanCastleKingside { get; set; }
        public bool PreviousWhiteCanCastleQueenside { get; set; }
        public bool PreviousBlackCanCastleKingside { get; set; }
        public bool PreviousBlackCanCastleQueenside { get; set; }

        public MoveRecord(Move move, Piece capturedPiece, bool pieceHadMoved,
            (int, int)? previousEnPassantTarget,
            bool previousWhiteCanCastleKingside,
            bool previousWhiteCanCastleQueenside,
            bool previousBlackCanCastleKingside,
            bool previousBlackCanCastleQueenside)
        {
            Move = move;
            CapturedPiece = capturedPiece;
            PieceHadMoved = pieceHadMoved;
            PreviousEnPassantTarget = previousEnPassantTarget;
            PreviousWhiteCanCastleKingside = previousWhiteCanCastleKingside;
            PreviousWhiteCanCastleQueenside = previousWhiteCanCastleQueenside;
            PreviousBlackCanCastleKingside = previousBlackCanCastleKingside;
            PreviousBlackCanCastleQueenside = previousBlackCanCastleQueenside;
        }
    }
}
