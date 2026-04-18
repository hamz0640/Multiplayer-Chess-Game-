using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programmeringseksamens_projekt
{
	internal class SyncBoard
	{
		private const int SQUARE_SIZE = 43;
		private const int BoardOffset = 100;

		Dictionary<(int row, int col), Panel> BoardPanels = new Dictionary<(int, int), Panel>();
		Dictionary<(int row, int col), PictureBox> Pieces = new Dictionary<(int, int), PictureBox>();
		HashSet<Control> Coordinates = new HashSet<Control>();
		Dictionary<Enums.PieceType, int> CapturedPiecesWhite = new Dictionary<Enums.PieceType, int>();
		Dictionary<Enums.PieceType, int> CapturedPiecesBlack = new Dictionary<Enums.PieceType, int>();
		(int row, int col)? SelectedPiece = null;
		Enums.PieceColor PlayerColor = Enums.PieceColor.White;

		// These are references to the global state
		private Control.ControlCollection Controls = null;
		private Board Board = null;
		private Network Network = null;
		private Form1 Form = null;

		public SyncBoard(Board board, Network network, Form1 form, Control.ControlCollection controls)
		{
			Board = board;
			Network = network;
			Form = form;
			Controls = controls;

			InitializeBoard(Enums.PieceColor.White);
		}

		public void Reset()
		{
			foreach (KeyValuePair<(int, int), PictureBox> piece in Pieces.ToList())
			{
				Controls.Remove(piece.Value);
				Pieces.Remove(piece.Key);
			}

			foreach (KeyValuePair<(int, int), Panel> panel in BoardPanels.ToList())
			{
				Controls.Remove(panel.Value);
				BoardPanels.Remove(panel.Key);
			}

			foreach (Control coordinate in Coordinates.ToList())
			{
				Controls.Remove(coordinate);
				Coordinates.Remove(coordinate);
			}
		}

		public void Start(Enums.PieceColor color)
		{
			foreach (KeyValuePair<(int, int), Panel> panel in BoardPanels.ToList())
			{
				Controls.Remove(panel.Value);
				BoardPanels.Remove(panel.Key);
			}

			PlayerColor = color;
			DrawCoordinates();
			InitializeBoard(color);
			InitializePieces();

			foreach (Control control in Controls)
			{
				control.MouseClick += HandleClick;
			}
		}

		private void ToggleHighlight(int row, int col, bool highlighted)
		{
			Color color;
			if (SquareIsWhite((row, col)))
				color = highlighted ? Color.Orange : Color.SandyBrown;
			else
				color = highlighted ? Color.Chocolate : Color.Sienna;

			BoardPanels[(row, col)].BackColor = color;
			if (Pieces.ContainsKey((row, col)))
			{
				Pieces[(row, col)].BackColor = color;
			}
		}

		private bool SquareIsWhite((int row, int col) position)
		{
			return ((position.col + (position.row % 2 == 0 ? 1 : 0)) % 2 == 0);
		}

		private Color GetSquareColor((int row, int col) position)
		{
			if (SquareIsWhite(position))
				return Color.SandyBrown;

			else
				return Color.Sienna;
		}

		public void MovePieceVisual(Move move)
		{
			PictureBox piece = Pieces[move.From];
			Pieces.Remove(move.From);

			if (Pieces.ContainsKey(move.To))
			{
				Controls.Remove(Pieces[move.To]);
				Pieces.Remove(move.To);
			}

			PlaceVisually(piece, move.To);
			Pieces[move.To] = piece;


			piece.BackColor = GetSquareColor(move.To);

			if (move.Type == Enums.MoveType.CastlingKingside)
			{
				(int, int) rookFromPosition = move.To.row == 0 ? (7, 7) : (0, 7);
				(int, int) rookToPosition = move.To.row == 0 ? (7, 5) : (0, 5);

				PictureBox rook = Pieces[rookFromPosition];
				PlaceVisually(rook, rookToPosition);
				Pieces[rookToPosition] = rook;
				Pieces.Remove(rookFromPosition);
			}

			if (move.Type == Enums.MoveType.CastlingQueenside)
			{
				(int, int) rookFromPosition = move.To.row == 0 ? (7, 0) : (0, 0);
				(int, int) rookToPosition = move.To.row == 0 ? (7, 3) : (0, 3);

				PictureBox rook = Pieces[rookFromPosition];
				PlaceVisually(rook, rookToPosition);
				Pieces[rookToPosition] = rook;
				Pieces.Remove(rookFromPosition);
			}

			if (move.Type == Enums.MoveType.EnPassant)
			{
				Controls.Remove(Pieces[(move.From.row, move.To.col)]);
				Pieces.Remove((move.From.row, move.To.col));
				Debug.Print("Enpassant");
			}
		}

		private void PlaceVisually(PictureBox piece, (int row, int col) position)
		{
			if (PlayerColor == Enums.PieceColor.White)
			{
				piece.Location = new Point(
					BoardOffset + position.col * SQUARE_SIZE,
					BoardOffset + (7 - position.row) * SQUARE_SIZE
				);

				piece.BackColor = GetSquareColor(position);
			}
			else
			{
				piece.Location = new Point(
					BoardOffset + (7 - position.col) * SQUARE_SIZE,
					BoardOffset + position.row * SQUARE_SIZE
				);

				piece.BackColor = GetSquareColor(position);
			}
		}

		private void InitializeBoard(Enums.PieceColor color)
		{
			if (color == Enums.PieceColor.White)
			{
				for (int row = 7; row >= 0; row--)
				{
					for (int col = 0; col < 8; col++)
					{
						Panel panel = new Panel();
						panel.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
						panel.Location = new Point(BoardOffset + col * SQUARE_SIZE, BoardOffset + (7 - row) * SQUARE_SIZE);
						panel.BackColor = GetSquareColor((row, col));

						BoardPanels[(row, col)] = panel;
						Controls.Add(panel);
					}
				}
			}
			else
			{
				for (int row = 7; row >= 0; row--)
				{
					for (int col = 0; col < 8; col++)
					{
						Panel panel = new Panel();
						panel.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
						panel.Location = new Point(BoardOffset + col * SQUARE_SIZE, BoardOffset + (7 - row) * SQUARE_SIZE);
						panel.BackColor = GetSquareColor((row, col));

						BoardPanels[(7 - row, 7 - col)] = panel;
						Controls.Add(panel);
					}
				}
			}
		}

		private void InitializePieces()
		{
			for (int col = 0; col < 8; col++)
			{
				AddPiece(1, col, Properties.Resources.wP);
				AddPiece(6, col, Properties.Resources.bP);
			}

			AddPiece(0, 0, Properties.Resources.wR);
			AddPiece(0, 1, Properties.Resources.wN);
			AddPiece(0, 2, Properties.Resources.wB);
			AddPiece(0, 3, Properties.Resources.wQ);
			AddPiece(0, 4, Properties.Resources.wK);
			AddPiece(0, 5, Properties.Resources.wB);
			AddPiece(0, 6, Properties.Resources.wN);
			AddPiece(0, 7, Properties.Resources.wR);

			AddPiece(7, 0, Properties.Resources.bR);
			AddPiece(7, 1, Properties.Resources.bN);
			AddPiece(7, 2, Properties.Resources.bB);
			AddPiece(7, 3, Properties.Resources.bQ);
			AddPiece(7, 4, Properties.Resources.bK);
			AddPiece(7, 5, Properties.Resources.bB);
			AddPiece(7, 6, Properties.Resources.bN);
			AddPiece(7, 7, Properties.Resources.bR);
		}

		private PictureBox AddPiece(int row, int col, Image pieceImage)
		{
			PictureBox piece = new PictureBox();
			piece.Image = pieceImage;
			piece.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
			PlaceVisually(piece, (row, col));
			piece.SizeMode = PictureBoxSizeMode.CenterImage;
			piece.BackColor = GetSquareColor((row, col));

			Controls.Add(piece);
			piece.BringToFront();
			Pieces[(row, col)] = piece;

			return piece;
		}

		private void DrawCoordinates()
		{

			// Draw Letters
			for (int i = 0; i < 8; i++)
			{
				Label LetterLabel = new Label();
				LetterLabel.Text = PlayerColor == Enums.PieceColor.White ? "" + (char)(i + 65) : "" + (char)(72 - i);
				LetterLabel.Width = SQUARE_SIZE;
				LetterLabel.TextAlign = ContentAlignment.MiddleCenter;
				LetterLabel.Location = new Point(BoardOffset + i * SQUARE_SIZE, BoardOffset + (7 * SQUARE_SIZE) + SQUARE_SIZE);
				LetterLabel.BackColor = Color.Transparent;
				LetterLabel.ForeColor = Color.White;
				Controls.Add(LetterLabel);
				Coordinates.Add(LetterLabel);
			}

			// Draw Numbers 
			for (int i = 0; i < 8; i++)
			{
				Label LetterLabel = new Label();
				LetterLabel.Text = PlayerColor == Enums.PieceColor.White ? "" + (char)(56 - i) : "" + (char)(i + 49);
				LetterLabel.Width = SQUARE_SIZE;
				LetterLabel.Height = SQUARE_SIZE;
				LetterLabel.TextAlign = ContentAlignment.MiddleCenter;
				LetterLabel.Location = new Point(BoardOffset + (7 * SQUARE_SIZE) + SQUARE_SIZE, BoardOffset + (i * SQUARE_SIZE));
				LetterLabel.BackColor = Color.Transparent;
				LetterLabel.ForeColor = Color.White;
				Controls.Add(LetterLabel);
				Coordinates.Add(LetterLabel);
			}

		}

		private void HandleClick(object sender, MouseEventArgs e)
		{
			if (!Network.IsConnected)
				return;

			if (Board.CurrentTurn != PlayerColor)
				return;

			int clickCol = e.X + ((Control)sender).Location.X;
			int clickRow = e.Y + ((Control)sender).Location.Y;

			if (PlayerColor == Enums.PieceColor.White)
			{
				clickCol = (clickCol - BoardOffset) / SQUARE_SIZE;
				clickRow = 7 - (clickRow - BoardOffset) / SQUARE_SIZE;
			}
			else
			{
				clickCol = 7 - (clickCol - BoardOffset) / SQUARE_SIZE;
				clickRow = (clickRow - BoardOffset) / SQUARE_SIZE;
			}
				

			if (clickCol < 0 || clickCol >= 8 || clickRow < 0 || clickRow >= 8)
				return;

			if (SelectedPiece != null)
			{
				for (int i = 0; i < 64; i++)
				{
					ToggleHighlight(i % 8, (i / 8) % 8, false);
				}

				Piece pieceAt = Board.Grid[SelectedPiece.Value.row, SelectedPiece.Value.col];

				if (pieceAt == null)
					return;

				if (pieceAt.Color != Board.CurrentTurn)
				{
					SelectedPiece = null;
					return;
				}

				foreach (Move move in Board.GetAllLegalMoves(pieceAt.Color))
				{
					if (move.From.col == move.To.col && move.From.row == move.To.row)
						continue;

					if (move.From.col != SelectedPiece.Value.col || move.From.row != SelectedPiece.Value.row)
						continue;

					if (move.To.col != clickCol || move.To.row != clickRow)
						continue;


					Form.RegisterCapture(move);
					Board.ApplyMove(move);
					MovePieceVisual(move);
					Form.ShowCapturedPieces();

					byte[] bytes = Message.Encode(move);
					Network.Send(bytes.ToArray());

					Form.SetMove();
					Form.UpdateMoveList(move);

                    Enums.WinResult winResult = Board.GetWinResult();
                    if (winResult != Enums.WinResult.None)
                    {
                        if (winResult == Enums.WinResult.WhiteWin)
                            MessageBox.Show("White won!");

                        if (winResult == Enums.WinResult.BlackWin)
                            MessageBox.Show("Black won!");

                        if (winResult == Enums.WinResult.Stalemate)
                            MessageBox.Show("The match ended in a stalemate");

                        Form.ResetAll();
                    }

                    break;
				}

				SelectedPiece = null;
			}
			else
			{
				if (!Pieces.ContainsKey((clickRow, clickCol)))
					return;

				ToggleHighlight(clickRow, clickCol, true);
				SelectedPiece = (clickRow, clickCol);

				Piece pieceAt = Board.Grid[clickRow, clickCol];

				if (pieceAt == null || pieceAt.Color != Board.CurrentTurn)
					return;

				foreach (Move move in Board.GetAllLegalMoves(pieceAt.Color))
				{
					if (move.From.col != clickCol || move.From.row != clickRow)
						continue;

					ToggleHighlight(move.To.row, move.To.col, true);
				}
			}
		}
	}
}
