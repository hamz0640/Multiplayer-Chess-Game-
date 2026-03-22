using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programmeringseksamens_projekt.Enums;

namespace Programmeringseksamens_projekt
{
    public abstract class PieceBase : Ipiece
    {
        public PieceType Type { get; protected set; }
        public PieceColor Color { get; protected set; }
        public Position Position { get; set; }
        public bool HasMoved { get; set; }

        protected PieceBase(PieceType type, PieceColor color, Position position)
        {
            Type = type;
            Color = color;
            Position = position;
            HasMoved = false;
        }

        public abstract IEnumerable<Position> GetPseudoLegalMoves(Board board);
        public abstract Ipiece Clone();

    }
}
