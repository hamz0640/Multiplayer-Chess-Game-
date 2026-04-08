using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programmeringseksamens_projekt.Enums;

namespace Programmeringseksamens_projekt
{
    public class Move
    {
        public (int row, int col) From { get; set; }
        public (int row, int col) To { get; set; }
        public MoveType Type { get; set; }
        public PieceType? PromotionPiece { get; set; }

        public Move((int, int) from, (int, int) to, MoveType type = MoveType.Normal, PieceType? promotionPiece = null)
        {
            From = from;
            To = to;
            Type = type;
            PromotionPiece = promotionPiece;
        }

    }
}
