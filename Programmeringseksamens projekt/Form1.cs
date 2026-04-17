using Programmeringseksamens_projekt;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Programmeringseksamens_projekt
{
    public partial class Form1 : Form
    {
        Dictionary<(int row, int col), Panel> BoardPanels = new Dictionary<(int, int), Panel>();
        Dictionary<(int row, int col), PictureBox> Pieces = new Dictionary<(int, int), PictureBox>();
        Dictionary<Enums.PieceType, int> CapturedPiecesWhite = new Dictionary<Enums.PieceType, int>();
        Dictionary<Enums.PieceType, int> CapturedPiecesBlack = new Dictionary<Enums.PieceType, int>();

        (int row, int col)? selectedPiece = null;

        Network network = new Network();
        Board board = new Board();

        const int SQUARE_SIZE = 43;

        const int BoardOffset = 100;

        Enums.PieceColor PlayerColer = Enums.PieceColor.White;

        public Form1()
        {
            InitializeComponent();
            InitializePieces();
            DrawCoordinates(PlayerColer);
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

            clickCol = (clickCol - BoardOffset) / SQUARE_SIZE;
            clickRow = 7 - (clickRow - BoardOffset) / SQUARE_SIZE;

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


                    RegisterCapture(move);   
                    board.ApplyMove(move);
                    MovePieceVisual(move);
                    ShowCapturedPieces();

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
                    panel.Location = new Point(BoardOffset + col * SQUARE_SIZE, BoardOffset + (7 - row) * SQUARE_SIZE);
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

        private void DrawCoordinates(Enums.PieceColor PlayerColor)
        {
            
            // Draw Letters
            for (int i = 0; i < 8; i++)
            {
                Label LetterLabel = new Label();
                LetterLabel.Text = PlayerColor == Enums.PieceColor.White ? "" + (char)(i+65) : "" + (char)(72 - i);
                LetterLabel.Width = SQUARE_SIZE;
                LetterLabel.TextAlign = ContentAlignment.MiddleCenter;
                LetterLabel.Location = new Point(BoardOffset + i * SQUARE_SIZE,BoardOffset + (7 * SQUARE_SIZE) + SQUARE_SIZE);
                LetterLabel.BackColor = Color.Transparent;
                LetterLabel.ForeColor = Color.White;
                Controls.Add(LetterLabel);
            }

            // Draw Numbers 
            for (int i = 0; i < 8; i++)
            {
                Label LetterLabel = new Label();
                LetterLabel.Text = PlayerColor == Enums.PieceColor.White ? "" + (char)(56 - i) : "" + (char)(i + 49) ;
                LetterLabel.Width = SQUARE_SIZE;
                LetterLabel.Height = SQUARE_SIZE;
                LetterLabel.TextAlign = ContentAlignment.MiddleCenter;
                LetterLabel.Location = new Point(BoardOffset + (7 * SQUARE_SIZE) + SQUARE_SIZE, BoardOffset + (i * SQUARE_SIZE));
                LetterLabel.BackColor = Color.Transparent;
                LetterLabel.ForeColor = Color.White;
                Controls.Add(LetterLabel);
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
                BoardOffset + position.col * SQUARE_SIZE,
                BoardOffset + (7 - position.row) * SQUARE_SIZE
            );

            piece.BackColor = GetSquareColor(position);
        }

        private PictureBox AddPiece(int row, int col, Image pieceImage)
        {
            PictureBox piece = new PictureBox();
            piece.Image = pieceImage;
            piece.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
            piece.Location = new Point(
                BoardOffset + col * SQUARE_SIZE,
                BoardOffset + (7 - row) * SQUARE_SIZE
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
                        BitConverter.ToInt32(bytes, 2),
                        BitConverter.ToInt32(bytes, 6)
                    );

                    (int, int) to = (
                        BitConverter.ToInt32(bytes, 10),
                        BitConverter.ToInt32(bytes, 14)
                    );

                    var moveType = (Enums.MoveType)BitConverter.ToChar(bytes, 18);

                    bool isPromoting = BitConverter.ToBoolean(bytes, 19);
                    Enums.PieceType pieceType = (Enums.PieceType)BitConverter.ToChar(bytes, 20);

                    Move move = new Move(from, to, moveType);
                    if (isPromoting)
                        move.PromotionPiece = pieceType;

                    RegisterCapture(move);
                    board.ApplyMove(move);
                    MovePieceVisual(move);
                    ShowCapturedPieces();
                    break;
            }
        }

       

        private void ShowCapturedPieces()
        {
            foreach (var entry in CapturedPiecesBlack)
            {
                if (entry.Key == Enums.PieceType.Pawn) this.BpC.Text = $"x{entry.Value}";
                if (entry.Key == Enums.PieceType.Knight) this.BnC.Text = $"x{entry.Value}";
                if (entry.Key == Enums.PieceType.Bishop) this.BbC.Text = $"x{entry.Value}";
                if (entry.Key == Enums.PieceType.Rook) this.BrC.Text = $"x{entry.Value}";
                if (entry.Key == Enums.PieceType.Queen) this.BqC.Text = $"x{entry.Value}";
            }
            foreach (var entry in CapturedPiecesWhite)
            {
                if (entry.Key == Enums.PieceType.Pawn) this.WpC.Text = $"x{entry.Value}";
                if (entry.Key == Enums.PieceType.Knight) this.WnC.Text = $"x{entry.Value}";
                if (entry.Key == Enums.PieceType.Bishop) this.WbC.Text = $"x{entry.Value}";
                if (entry.Key == Enums.PieceType.Rook) this.WrC.Text = $"x{entry.Value}";
                if (entry.Key == Enums.PieceType.Queen) this.WqC.Text = $"x{entry.Value}";
            }
        }

        private void RegisterCapture(Move move)
        {
            Piece capturedPiece = board.Grid[move.To.row, move.To.col];

            if (capturedPiece == null)
                return;

            var type = capturedPiece.Type;
            var color = capturedPiece.Color;

            var dict = color == Enums.PieceColor.White
                ? CapturedPiecesWhite
                : CapturedPiecesBlack;

            if (dict.ContainsKey(type))
                dict[type]++;
            else
                dict[type] = 1;
        }

        private void TitleLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
