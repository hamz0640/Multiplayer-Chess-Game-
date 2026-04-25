using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Programmeringseksamens_projekt.Enums;
using System.Diagnostics;

namespace Programmeringseksamens_projekt
{
	public class Board
	{
		public Piece[,] Grid { get; set; } // row, col; row=0-> white back rank
		public PieceColor CurrentTurn { get; set; }
		public List<MoveRecord> MoveHistory { get; set; }
		public bool WhiteCanCastleKingside = false;
		public bool WhiteCanCastleQueenside = false;
		public bool BlackCanCastleKingside = false;
		public bool BlackCanCastleQueenside = false;
		(int, int)? EnPassantTarget { get; set; }
		private bool _isSimulating = false; // flag to tell the “this move is fake (simulation), not a real move

		public Board()
		{
			MoveHistory = new List<MoveRecord>();
			CurrentTurn = PieceColor.White;
			SetupStartingPosition();
		}

		public void ApplyMove(Move move)
		{
			MoveHistory.Add(new MoveRecord(
				move,
				Grid[move.To.row, move.To.col],
				Grid[move.From.row, move.From.col].HasMoved,
				EnPassantTarget,
				WhiteCanCastleKingside,
				WhiteCanCastleQueenside,
				BlackCanCastleKingside,
				BlackCanCastleQueenside
			));

			switch (move.Type)
			{
				case MoveType.Normal:
					Piece piece = Grid[move.From.row, move.From.col];
					Grid[move.To.row, move.To.col] = piece;
					Grid[move.From.row, move.From.col] = null;
					piece.HasMoved = true;
					piece.Position = (move.To.row, move.To.col);

					if (piece.Type == PieceType.King)
					{
						if (piece.Color == PieceColor.White) { WhiteCanCastleKingside = false; WhiteCanCastleQueenside = false; }
						else { BlackCanCastleKingside = false; BlackCanCastleQueenside = false; }
					}
					
					break;
				case MoveType.CastlingKingside:
					Piece kingSideking = Grid[move.From.row, 4];
					Piece kingSiderook = Grid[move.From.row, 7];

					// Move king to g-file (col 6)
					Grid[move.From.row, 6] = kingSideking;
					Grid[move.From.row, 4] = null;
					kingSideking.HasMoved = true;
					kingSideking.Position = (move.From.row, 6);

					// Move rook to f-file (col 5)
					Grid[move.From.row, 5] = kingSiderook;
					Grid[move.From.row, 7] = null;
					kingSiderook.HasMoved = true;
					kingSiderook.Position = (move.From.row, 5);

					// Disable castling for that color
					if (kingSideking.Color == PieceColor.White) { WhiteCanCastleKingside = false; WhiteCanCastleQueenside = false; }
					else { BlackCanCastleKingside = false; BlackCanCastleQueenside = false; }
					break;
				case MoveType.CastlingQueenside:
					Piece queenSideKing = Grid[move.From.row, 4];
					Piece queenSideRook = Grid[move.From.row, 0];

					// Move king to c-file (col 2)
					Grid[move.From.row, 2] = queenSideKing;
					Grid[move.From.row, 4] = null;
					queenSideKing.HasMoved = true;
					queenSideKing.Position = (move.From.row, 2);

					// Move rook to d-file (col 3)
					Grid[move.From.row, 3] = queenSideRook;
					Grid[move.From.row, 0] = null;
					queenSideRook.HasMoved = true;
					queenSideRook.Position = (move.From.row, 3);

					// Disable castling for that color
					if (queenSideKing.Color == PieceColor.White) { WhiteCanCastleKingside = false; WhiteCanCastleQueenside = false; }
					else { BlackCanCastleKingside = false; BlackCanCastleQueenside = false; }
					break;
				case MoveType.EnPassant:
					Piece pawn = Grid[move.From.row, move.From.col];

					// Move the capturing pawn 
					Grid[move.To.row, move.To.col] = pawn;
					Grid[move.From.row, move.From.col] = null;
					pawn.HasMoved = true;
					pawn.Position = (move.To.row, move.To.col);

					// Remove the captured pawn - same row as From, same col as To
					Grid[move.From.row, move.To.col] = null;
					break;
				case MoveType.Promotion:
					Debug.Print("What ze fuk");
					Piece promotedPiece = Grid[move.From.row, move.From.col];

					if (move.PromotionPiece == PieceType.Queen) promotedPiece = new Queen(Grid[move.From.row, move.From.col].Color, move.To);
					if (move.PromotionPiece == PieceType.Rook) promotedPiece = new Rook(Grid[move.From.row, move.From.col].Color, move.To);
					if (move.PromotionPiece == PieceType.Knight) promotedPiece = new Knight(Grid[move.From.row, move.From.col].Color, move.To);
					if (move.PromotionPiece == PieceType.Bishop) promotedPiece = new Bishop(Grid[move.From.row, move.From.col].Color, move.To);

					Grid[move.To.row, move.To.col] = promotedPiece;
					Grid[move.From.row, move.From.col] = null;
					promotedPiece.HasMoved = true;
					break;
			}
			// Update en passant target
			EnPassantTarget = null;
			Piece movedPiece = Grid[move.To.row, move.To.col];
			if (movedPiece is Pawn && Math.Abs(move.To.row - move.From.row) == 2)
				EnPassantTarget = ((move.From.row + move.To.row) / 2, move.To.col);

			// Flip turn
			CurrentTurn = CurrentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
		}

		public WinResult GetWinResult()
		{
			if (IsInCheck(CurrentTurn) && GetAllLegalMoves(CurrentTurn).Count == 0)
			{
				return CurrentTurn == PieceColor.White ? WinResult.BlackWin : WinResult.WhiteWin;
			}
			else
			{
				if (GetAllLegalMoves(CurrentTurn).Count == 0)
					return WinResult.Stalemate;
			}

			uint pieceCount = 0;
			uint whiteKnightCount = 0;
			uint blackKnightCount = 0;
			uint bishopCount = 0;
			foreach (Piece piece in Grid)
			{
				pieceCount += 1;
				if (piece.Type == PieceType.Knight)
				{
					if (piece.Color == PieceColor.White) whiteKnightCount += 1;
					else blackKnightCount += 1;
				}
				if (piece.Type == PieceType.Bishop) bishopCount += 1;
			}

			if (pieceCount == 2) return WinResult.Draw;

			if (pieceCount == 3)
			{
				if (whiteKnightCount == 1 || blackKnightCount == 1) return WinResult.Draw;
				if (bishopCount == 1) return WinResult.Draw;
			}

			if (pieceCount == 4 && (whiteKnightCount == 2 || blackKnightCount == 2)) return WinResult.Draw;

			return WinResult.None;
		}

		public void UndoMove()
		{
			if (MoveHistory.Count == 0) return;

			MoveRecord record = MoveHistory.Last();
			MoveHistory.RemoveAt(MoveHistory.Count - 1);
			Move move = record.Move;

			switch (move.Type)
			{
				case MoveType.Normal:
					Piece piece = Grid[move.To.row, move.To.col];
					Grid[move.From.row, move.From.col] = piece;
					Grid[move.To.row, move.To.col] = record.CapturedPiece;
					piece.Position = (move.From.row, move.From.col);
					piece.HasMoved = record.PieceHadMoved;
					break;
				case MoveType.Promotion:
					Grid[move.From.row, move.From.col] = new Pawn(MoveHistory.Count % 2 == 0 ? PieceColor.White : PieceColor.Black, (move.From.row, move.From.col));
					Grid[move.From.row, move.From.col].HasMoved = record.PieceHadMoved;
					Grid[move.To.row, move.To.col] = record.CapturedPiece;
					break;
				case MoveType.EnPassant:
					Piece epPawn = Grid[move.To.row, move.To.col];
					Grid[move.From.row, move.From.col] = epPawn;
					Grid[move.To.row, move.To.col] = null;
					epPawn.Position = (move.From.row, move.From.col);
					// Restore captured pawn on the square it was on
					PieceColor capturedColor = epPawn.Color == PieceColor.White ? PieceColor.Black : PieceColor.White;
					Grid[move.From.row, move.To.col] = new Pawn(capturedColor, (move.From.row, move.To.col));
					break;

				case MoveType.CastlingKingside:
					Piece ksKing = Grid[move.From.row, 6];
					Piece ksRook = Grid[move.From.row, 5];
					Grid[move.From.row, 4] = ksKing;
					Grid[move.From.row, 6] = null;
					Grid[move.From.row, 7] = ksRook;
					Grid[move.From.row, 5] = null;
					ksKing.Position = (move.From.row, 4);
					ksRook.Position = (move.From.row, 7);
					ksKing.HasMoved = false;
					ksRook.HasMoved = false;
					break;

				case MoveType.CastlingQueenside:
					Piece qsKing = Grid[move.From.row, 2];
					Piece qsRook = Grid[move.From.row, 3];
					Grid[move.From.row, 4] = qsKing;
					Grid[move.From.row, 2] = null;
					Grid[move.From.row, 0] = qsRook;
					Grid[move.From.row, 3] = null;
					qsKing.Position = (move.From.row, 4);
					qsRook.Position = (move.From.row, 0);
					qsKing.HasMoved = false;
					qsRook.HasMoved = false;
					break;
			}

			// Restore all previous board state
			EnPassantTarget = record.PreviousEnPassantTarget;
			WhiteCanCastleKingside = record.PreviousWhiteCanCastleKingside;
			WhiteCanCastleQueenside = record.PreviousWhiteCanCastleQueenside;
			BlackCanCastleKingside = record.PreviousBlackCanCastleKingside;
			BlackCanCastleQueenside = record.PreviousBlackCanCastleQueenside;

			// Flip turn back
			CurrentTurn = CurrentTurn == PieceColor.White ? PieceColor.Black : PieceColor.White;
		}

		public bool IsInCheck(PieceColor pieceColor)
		{
			(int row, int col) kingPos = (-1, -1);
			foreach (Piece p in Grid)
				if (p != null && p is King && p.Color == pieceColor)
					kingPos = p.Position;

			PieceColor enemyColor = pieceColor == PieceColor.White ? PieceColor.Black : PieceColor.White;

			return GetRawMoves(enemyColor).Any(m => m.To == kingPos);
		}

		public void SetupStartingPosition()
		{
			Grid = new Piece[8, 8];

			for (int col = 0; col < 8; col++)
			{
				Grid[1, col] = new Pawn(PieceColor.White, (1, col));
				Grid[6, col] = new Pawn(PieceColor.Black, (6, col));
			}

			// White Pieces
			Grid[0, 0] = new Rook(PieceColor.White, (0, 0));
			Grid[0, 7] = new Rook(PieceColor.White, (0, 7));

			Grid[0, 1] = new Knight(PieceColor.White, (0, 1));
			Grid[0, 6] = new Knight(PieceColor.White, (0, 6));

			Grid[0, 2] = new Bishop(PieceColor.White, (0, 2));
			Grid[0, 5] = new Bishop(PieceColor.White, (0, 5));

			Grid[0, 3] = new Queen(PieceColor.White, (0, 3));
			Grid[0, 4] = new King(PieceColor.White, (0, 4));

			// Black Pieces 
			Grid[7, 7] = new Rook(PieceColor.Black, (7, 7));
			Grid[7, 0] = new Rook(PieceColor.Black, (7, 0));

			Grid[7, 1] = new Knight(PieceColor.Black, (7, 1));
			Grid[7, 6] = new Knight(PieceColor.Black, (7, 6));

			Grid[7, 2] = new Bishop(PieceColor.Black, (7, 2));
			Grid[7, 5] = new Bishop(PieceColor.Black, (7, 5));

			Grid[7, 3] = new Queen(PieceColor.Black, (7, 3));
			Grid[7, 4] = new King(PieceColor.Black, (7, 4));

			// Castling starts as available
			WhiteCanCastleKingside = true;
			WhiteCanCastleQueenside = true;
			BlackCanCastleKingside = true;
			BlackCanCastleQueenside = true;
		}

		public bool IsInBounds(int row, int col) => row >= 0 && row < 8 && col >= 0 && col < 8;

		public bool IsEmpty(int row, int col) => Grid[row, col] == null;

		public List<Move> GetAllLegalMoves(PieceColor pieceColor)
		{
			List<Move> legal = new List<Move>();
			_isSimulating = true;
			foreach (Move m in GetRawMoves(pieceColor)) 
			{
				ApplyMove(m);
				if (!IsInCheck(pieceColor))
					legal.Add(m);
				UndoMove();
			}

			(int row, int col) kingPos = pieceColor == PieceColor.White ? (0, 4) : (7, 4);
			Piece kingPiece = Grid[kingPos.row, kingPos.col];
			if (kingPiece is King k)
			{
				foreach (Move m in k.GetLegalMoves(this, true)
									.Where(m => m.Type == MoveType.CastlingKingside
											 || m.Type == MoveType.CastlingQueenside))
				{
					ApplyMove(m);
					if (!IsInCheck(pieceColor))
						legal.Add(m);
					UndoMove();
				}
			}

			_isSimulating = false;
			return legal;
		}

		public List<Move> GetRawMoves(PieceColor color)
		{
			List<Move> rawMoves = new List<Move>();
			for (int row = 0; row < 8; row++)
			{
				for (int col = 0; col < 8; col++)
				{
					Piece piece = Grid[row, col];
					if (piece != null && piece.Color == color)
					{
						rawMoves.AddRange(piece is King king? king.GetLegalMoves(this,false) : piece.GetLegalMoves(this));

					}
				}
			}
			return rawMoves;
		}
	}
}
