using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programmeringseksamens_projekt
{
    internal static class Message
    {
        public static Enums.MessageType GetType(byte[] bytes)
        {
            return (Enums.MessageType)BitConverter.ToUInt32(bytes, 0);
        }

        public static Move DecodeMove(byte[] bytes) {
            if (GetType(bytes) != Enums.MessageType.Move)
                throw new Exception("Message was decoded, but wrong type was found in bytes");

            (int row, int col) from = (0, 0);
            from.row = BitConverter.ToInt32(bytes, 4);
            from.col = BitConverter.ToInt32(bytes, 8);

            (int row, int col) to = (0, 0);
            to.row = BitConverter.ToInt32(bytes, 12);
            to.col = BitConverter.ToInt32(bytes, 16);

            Enums.MoveType moveType = (Enums.MoveType)BitConverter.ToUInt32(bytes, 20);

            bool isPromoting = BitConverter.ToBoolean(bytes, 24);
            Enums.PieceType promotionPiece = (Enums.PieceType)BitConverter.ToUInt32(bytes, 25);

            if (isPromoting)
                return new Move(from, to, moveType, promotionPiece);
            else
                return new Move(from, to, moveType, null);
        }

        public static byte[] Encode(Move move) {
            List<byte> bytes = new List<byte>();

            bytes.AddRange(BitConverter.GetBytes((uint)Enums.MessageType.Move));
            bytes.AddRange(BitConverter.GetBytes(move.From.row));
            bytes.AddRange(BitConverter.GetBytes(move.From.col));
            bytes.AddRange(BitConverter.GetBytes(move.To.row));
            bytes.AddRange(BitConverter.GetBytes(move.To.col));
            bytes.AddRange(BitConverter.GetBytes((uint)move.Type));
            bytes.AddRange(BitConverter.GetBytes(move.PromotionPiece != null));
            if (move.PromotionPiece != null)
                bytes.AddRange(BitConverter.GetBytes((uint)move.PromotionPiece.Value));
            else
                bytes.AddRange(BitConverter.GetBytes((uint)0));

            return bytes.ToArray();
        }

        public static byte[] Encode(Enums.MessageType messageType)
        {
            if (messageType == Enums.MessageType.Move)
                throw new Exception("Attempted to encode move as a single message type. Use other overload of Encode method.");

            return BitConverter.GetBytes((uint)messageType);
        }
    }
}
