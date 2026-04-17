using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Programmeringseksamens_projekt;

namespace Programmeringseksamens_projekt
{
	public partial class Form1 : Form
	{
		Dictionary<(int row, int col), Panel> BoardPanels = new Dictionary<(int, int), Panel>();
		Dictionary<(int row, int col), PictureBox> Pieces = new Dictionary<(int, int), PictureBox>();

        (int row, int col)? selectedPiece = null; 
		
		Network network = new Network();
		Board board = new Board();

		const int SQUARE_SIZE = 43;

		public Form1()
		{
			InitializeComponent();
            int borderThickness = 15;

            Panel borderPanel = new Panel();
            borderPanel.Location = new Point(11 - borderThickness, 11 - borderThickness);
            borderPanel.Size = new Size(
                8 * SQUARE_SIZE + borderThickness * 2,
                8 * SQUARE_SIZE + borderThickness * 2
            );
            borderPanel.BackColor = Color.Black;
            Controls.Add(borderPanel);
            InitializePieces();
            borderPanel.SendToBack();
			board.SetupStartingPosition();
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

		private void HandleClick(object sender, MouseEventArgs e)
		{
			if (!network.IsStarted)
				return;

			int clickCol = e.X + ((Control)sender).Location.X;
			int clickRow = e.Y + ((Control)sender).Location.Y;

			clickCol = (clickCol - 11) / SQUARE_SIZE;
			clickRow = 7 - (clickRow - 11) / SQUARE_SIZE;

			if (clickCol < 0 || clickCol >= 8 || clickRow < 0 || clickRow >= 8)
				return;

			if (selectedPiece != null)
			{       
				for (int i = 0; i < 64; i++)
				{
					ToggleHighlight(i % 8, (i / 8) % 8, false);
				}

				Piece pieceAt = board.Grid[selectedPiece.Value.row, selectedPiece.Value.col];

				if (pieceAt == null)
					return;

                if (pieceAt.Color != board.CurrentTurn)
                {
                    selectedPiece = null;
                    return;
                }

                foreach (Move move in board.GetAllLegalMoves(pieceAt.Color))
				{
					if (move.From.col == move.To.col && move.From.row == move.To.row)
						continue;

                    if (move.From.col != selectedPiece.Value.col || move.From.row != selectedPiece.Value.row)
						continue;

					if (move.To.col != clickCol || move.To.row != clickRow)
                        continue;

					// TODO: Promotion and color, få Hamza til at gøre det :)
					board.ApplyMove(move);
                    MovePieceVisual(move);

					List<byte> bytes = new List<byte>();
					
					bytes.AddRange(BitConverter.GetBytes((byte)Enums.MessageType.Move));
					bytes.AddRange(BitConverter.GetBytes(move.From.row));
                    bytes.AddRange(BitConverter.GetBytes(move.From.col));
                    bytes.AddRange(BitConverter.GetBytes(move.To.row));
                    bytes.AddRange(BitConverter.GetBytes(move.To.col));
					bytes.AddRange(BitConverter.GetBytes((byte)move.Type));
					bytes.AddRange(BitConverter.GetBytes(move.PromotionPiece != null));
					if (move.PromotionPiece != null)
						bytes.AddRange(BitConverter.GetBytes((byte)move.PromotionPiece.Value));
					else
						bytes.Add(0);
					

                    network.Send(bytes.ToArray());

                    break;
				}

                selectedPiece = null;
            }
			else
			{
				if (!Pieces.ContainsKey((clickRow, clickCol)))
					return;

				ToggleHighlight(clickRow, clickCol, true);
				selectedPiece = (clickRow, clickCol);

				Piece pieceAt = board.Grid[clickRow, clickCol];

                if (pieceAt == null || pieceAt.Color != board.CurrentTurn)
                    return;

                foreach (Move move in board.GetAllLegalMoves(pieceAt.Color))
				{
					if (move.From.col != clickCol || move.From.row != clickRow)
						continue;

                    ToggleHighlight(move.To.row, move.To.col, true);
				}
			}
		}

		private void InitializePieces()
		{
			for (int row = 7; row >= 0; row--)
			{
				for (int col = 0; col < 8; col++)
				{
					Panel panel = new Panel();
					panel.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
					panel.Location = new Point(11 + col * SQUARE_SIZE, 11 + (7 - row) * SQUARE_SIZE);
					panel.BackColor = GetSquareColor((row, col));

					BoardPanels[(row, col)] = panel;
					Controls.Add(panel);
				}
			}

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

		private void MovePieceVisual(Move move)
		{
			PictureBox piece = Pieces[move.From];
			Pieces.Remove(move.From);

			PlaceVisually(piece, move.To);
            if (Pieces.ContainsKey(move.To))
			{
				Controls.Remove(Pieces[move.To]);
			}

			Pieces[move.To] = piece;

			
			piece.BackColor = GetSquareColor(move.To);

			if (move.Type == Enums.MoveType.CastlingKingside)
			{
				(int, int) rookFromPosition = board.CurrentTurn == Enums.PieceColor.White ? (7, 7) : (0, 7);
                (int, int) rookToPosition = board.CurrentTurn == Enums.PieceColor.White ? (7, 5) : (0, 5);

				PictureBox rook = Pieces[rookFromPosition];
				PlaceVisually(rook, rookToPosition);
				Pieces[rookToPosition] = rook;
				Pieces.Remove(rookFromPosition);
            }

            if (move.Type == Enums.MoveType.CastlingQueenside)
            {
                (int, int) rookFromPosition = board.CurrentTurn == Enums.PieceColor.White ? (7, 0) : (0, 0);
                (int, int) rookToPosition = board.CurrentTurn == Enums.PieceColor.White ? (7, 3) : (0, 3);

                PictureBox rook = Pieces[rookFromPosition];
                PlaceVisually(rook, rookToPosition);
                Pieces[rookToPosition] = rook;
                Pieces.Remove(rookFromPosition);
            }
        }

		private void PlaceVisually(PictureBox piece, (int row, int col) position)
		{
            piece.Location = new Point(
                11 + position.col * SQUARE_SIZE,
                11 + (7 - position.row) * SQUARE_SIZE
            );

			piece.BackColor = GetSquareColor(position);
        }

		private PictureBox AddPiece(int row, int col, Image pieceImage)
		{
			PictureBox piece = new PictureBox();
			piece.Image = pieceImage;
			piece.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
			piece.Location = new Point(
				11 + col * SQUARE_SIZE,
				11 + (7 - row) * SQUARE_SIZE
			);
			piece.SizeMode = PictureBoxSizeMode.CenterImage;
			piece.BackColor = GetSquareColor((row, col));

			Controls.Add(piece);
			piece.BringToFront();
			Pieces[(row, col)] = piece;

			return piece;
		}

		private void joinButton_Click(object sender, EventArgs e)
		{
			if (network.IsStarted)
			{
				return;
			}

			string ipString = ipEnterField.Text;
			IPAddress ip = IPAddress.None;

			if (!IPAddress.TryParse(ipString, out ip))
			{
				ipEnterField.BackColor = Color.IndianRed;
				return;
			} 
			else
			{
				ipEnterField.BackColor = Color.White;
			}

			Task join = network.Connect(ipString);
		}

		private void hostButton_Click(object sender, EventArgs e)
		{
			if (network.IsStarted)
			{
				return;
			}

			Task start = network.StartServer();
		}

        private async void checkNetworkTimerTick(object sender, EventArgs e)
        {
			if (!network.IsStarted)
				return;

			if (!network.BytesAvailable())
				return;

			var bytes = await network.Read();
			var messageType = (Enums.MessageType)BitConverter.ToChar(bytes, 0);

			switch (messageType)
			{
				case Enums.MessageType.Move:
					(int, int) from = (
						BitConverter.ToInt32(bytes, 1),
						BitConverter.ToInt32(bytes, 5)
					);

					(int, int) to = (
						BitConverter.ToInt32(bytes, 9),
						BitConverter.ToInt32(bytes, 13)
					);

					var moveType = (Enums.MoveType)BitConverter.ToChar(bytes, 17);

					bool isPromoting = BitConverter.ToBoolean(bytes, 18);
					Enums.PieceType pieceType = (Enums.PieceType)BitConverter.ToChar(bytes, 19);
                    
					Move move = new Move(from, to, moveType, pieceType);
                    board.ApplyMove(move);
                    MovePieceVisual(move);

                    break;
			}
        }
    }
}
