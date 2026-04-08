using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static Programmeringseksamens_projekt.Enums;

namespace Programmeringseksamens_projekt
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            // Test chess logic
            Board board = new Board();
            PrintBoard(board);

            // Test a pawn double push
            Move testMove = board.GetAllLegalMoves(PieceColor.White)
                .FirstOrDefault(m => m.From == (1, 3) && m.To == (3, 3));

            if (testMove != null)
            {
                board.ApplyMove(testMove);
                Debug.WriteLine("Move applied!");
                PrintBoard(board);

                // Test knight . pawn move
                Piece knight = board.Grid[0, 1];
                List<Move> knightMoves = knight.GetLegalMoves(board);
                Debug.WriteLine($"Knight at (0,1) has {knightMoves.Count} moves (expected 3):");
                foreach (Move m in knightMoves)
                    Debug.WriteLine($"  ({m.From.row},{m.From.col}) -> ({m.To.row},{m.To.col})");
            }



            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        static void PrintBoard(Board board)
        {
            Debug.WriteLine("  0 1 2 3 4 5 6 7");
            for (int row = 7; row >= 0; row--)  
            {
                string line = row + " ";
                for (int col = 0; col < 8; col++)
                {
                    Piece p = board.Grid[row, col];
                    if (p == null) line += ". ";
                    else line += GetSymbol(p) + " ";
                }
                Debug.WriteLine(line);
            }
            Debug.WriteLine("");
        }

        static string GetSymbol(Piece p)
        {
            string symbol;
            switch (p.Type)
            {
                case PieceType.Pawn:
                    symbol = "P";
                    break;
                case PieceType.Rook:
                    symbol = "R";
                    break;
                case PieceType.Knight:
                    symbol = "N";
                    break;
                case PieceType.Bishop:
                    symbol = "B";
                    break;
                case PieceType.Queen:
                    symbol = "Q";
                    break;
                case PieceType.King:
                    symbol = "K";
                    break;
                default:
                    symbol = "?";
                    break;
            }
            return p.Color == PieceColor.Black ? symbol.ToLower() : symbol;
        }
    }
}
