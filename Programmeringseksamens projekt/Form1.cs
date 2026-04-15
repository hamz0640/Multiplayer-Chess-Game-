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
		Dictionary<(int, int), Panel> BoardPanels = new Dictionary<(int, int), Panel>();
		Dictionary<(int, int), PictureBox> Pieces = new Dictionary<(int, int), PictureBox>();

        (int, int)? selectedPiece = null; 
		
		Network network = new Network();
		Board board = new Board();

		const int SQUARE_SIZE = 43;

		public Form1()
		{
			InitializeComponent();
			InitializePieces();

			board.SetupStartingPosition();
			foreach (Control control in Controls)
			{
				control.MouseClick += HandleClick;
			}
		}

		private void ToggleHighlight(int x, int y, bool highlighted)
		{
			Color color;
			if (SquareIsWhite(x, y))
				color = highlighted ? Color.Orange : Color.SandyBrown;
			else
				color = highlighted ? Color.Chocolate : Color.Sienna;

			BoardPanels[(x, y)].BackColor = color;
			if (Pieces.ContainsKey((x, y)))
			{
				Pieces[(x, y)].BackColor = color;
			}
		}

		private void HandleClick(object sender, MouseEventArgs e)
		{
			int x = e.X + ((Control)sender).Location.X;
			int y = e.Y + ((Control)sender).Location.Y;

			x = (x - 11) / SQUARE_SIZE;
			y = 7 - (y - 11) / SQUARE_SIZE;

			if (x < 0 || x >= 8 || y < 0 || y >= 8)
				return;

			if (selectedPiece != null)
			{       
				for (int i = 0; i < 64; i++)
				{
					ToggleHighlight(i % 8, (i / 8) % 8, false);
				}

				Piece pieceAt = board.Grid[selectedPiece.Value.Item2, selectedPiece.Value.Item1];

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

                    if (move.From.col != selectedPiece.Value.Item1 || move.From.row != selectedPiece.Value.Item2)
						continue;

					Debug.Print(x + " " + y);
					Debug.Print(move.To.col + " " + move.To.row);
					if (move.To.col != x || move.To.row != y)
                        continue;

					// TODO: Promotion and color, få Hamza til at gøre det :)
					board.ApplyMove(move);
					Debug.Print("Moved");

                    MovePiece(selectedPiece.Value.Item1, selectedPiece.Value.Item2, x, y);

                    break;
				}

				Debug.Print("Unselected");
                selectedPiece = null;
            }
			else
			{
				if (!Pieces.ContainsKey((x, y)))
					return;

				ToggleHighlight(x, y, true);
				selectedPiece = (x, y);
				Debug.Print("x: " + x + ", y: " + y);

				Piece pieceAt = board.Grid[y, x];

                if (pieceAt == null || pieceAt.Color != board.CurrentTurn)
                    return;

                foreach (Move move in board.GetAllLegalMoves(pieceAt.Color))
				{
					if (move.From.col != x || move.From.row != y)
						continue;

                    ToggleHighlight(move.To.col, move.To.row, true);
				}
			}
		}

		private void InitializePieces()
		{
			for (int y = 7; y >= 0; y--)
			{
				for (int x = 0; x < 8; x++)
				{
					Panel panel = new Panel();
					panel.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
					panel.Location = new Point(11 + x * SQUARE_SIZE, 11 + (7 - y) * SQUARE_SIZE);
					panel.BackColor = GetSquareColor(x, y);

					BoardPanels[(x, y)] = panel;
					Controls.Add(panel);
				}
			}

			for (int x = 0; x < 8; x++)
			{
				AddPiece(x, 1, Properties.Resources.wP);
				AddPiece(x, 6, Properties.Resources.bP);
			}

			AddPiece(0, 0, Properties.Resources.wR);
			AddPiece(1, 0, Properties.Resources.wN);
			AddPiece(2, 0, Properties.Resources.wB);
			AddPiece(3, 0, Properties.Resources.wQ);
			AddPiece(4, 0, Properties.Resources.wK);
			AddPiece(5, 0, Properties.Resources.wB);
			AddPiece(6, 0, Properties.Resources.wN);
			AddPiece(7, 0, Properties.Resources.wR);

			AddPiece(0, 7, Properties.Resources.bR);
			AddPiece(1, 7, Properties.Resources.bN);
			AddPiece(2, 7, Properties.Resources.bB);
			AddPiece(3, 7, Properties.Resources.bQ);
			AddPiece(4, 7, Properties.Resources.bK);
			AddPiece(5, 7, Properties.Resources.bB);
			AddPiece(6, 7, Properties.Resources.bN);
			AddPiece(7, 7, Properties.Resources.bR);
		}

		private bool SquareIsWhite(int x, int y)
		{
			return ((x + (y % 2 == 0 ? 1 : 0)) % 2 == 0);
		}

		private Color GetSquareColor(int x, int y)
		{
			if (SquareIsWhite(x, y))
				return Color.SandyBrown;
			else
				return Color.Sienna;
		}

		private void MovePiece(int x1, int y1, int x2, int y2)
		{
			PictureBox piece = Pieces[(x1, y1)];
			ToggleHighlight(x1, y1, false);
			Pieces.Remove((x1, y1));

			if (Pieces.ContainsKey((x2, y2)))
			{
				Controls.Remove(Pieces[(x2, y2)]);
			}

			Pieces[(x2, y2)] = piece;

			piece.Location = new Point(
				11 + x2 * SQUARE_SIZE,
				11 + (7 - y2) * SQUARE_SIZE
			);
			piece.BackColor = GetSquareColor(x2, y2);
		}

		private PictureBox AddPiece(int x, int y, Image pieceImage)
		{
			PictureBox piece = new PictureBox();
			piece.Image = pieceImage;
			piece.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
			piece.Location = new Point(
				11 + x * SQUARE_SIZE,
				11 + (7 - y) * SQUARE_SIZE
			);
			piece.SizeMode = PictureBoxSizeMode.CenterImage;
			piece.BackColor = GetSquareColor(x, y);

			Controls.Add(piece);
			piece.BringToFront();
			Pieces[(x, y)] = piece;

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
			Task start = network.StartServer();
		}
	}
}
