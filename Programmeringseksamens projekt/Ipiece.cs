using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Programmeringseksamens_projekt.Enums;

namespace Programmeringseksamens_projekt
{
    public interface Ipiece
    {
        /// Interface that all chess pieces must implement.

        PieceType Type { get; }
        PieceColor Color { get; }
        Position Position { get; set; }
        bool HasMoved { get; set; }

        /// Returns all pseudo-legal target squares this piece can move to,


        IEnumerable<Position> GetPseudoLegalMoves(Board board);

        /// Creates a deep copy of this piece (used for board cloning during validation).

        Ipiece Clone();
    }
}
