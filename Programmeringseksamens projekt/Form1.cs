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

        Network Network;
        Board Board;
        SyncBoard SyncBoard;

        const int SQUARE_SIZE = 43;

        const int BoardOffset = 100;

        public Form1()
        {
            InitializeComponent();

            moveList.Columns.Add("White");
            moveList.Columns.Add("Black");            
            
            Board = new Board();
            Board.SetupStartingPosition();

            Network = new Network();

            SyncBoard = new SyncBoard(Board, Network, this, Controls);
        }

        async private void joinButton_Click(object sender, EventArgs e)
        {
            if (Network.IsStarted)
                return;

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

            

            bool joinResult = await Network.Connect(ipString);

            if (joinResult == true)
            {
                Text = "Ultimate Chess Pro 2026 ♔ (Client)";
                SyncBoard.Start(Enums.PieceColor.Black);
            }
            else
            {
                MessageBox.Show("Connection to host failed. Is there a host to connect to?");
            }
        }

        private void hostButton_Click(object sender, EventArgs e)
        {
            if (Network.IsStarted)
                return;

            Text = "Ultimate Chess Pro 2026 ♔ (Host)";

            SyncBoard.Start(Enums.PieceColor.White);

            Task start = Network.StartServer();
        }

        private void resignButton_Click(object sender, EventArgs e)
        {
            if (Network.IsConnected == false)
                return;

            byte[] bytes = Message.Encode(Enums.MessageType.Resign);
            Network.Send(bytes);

            ResetAll();
        }

        private void drawButton_Click(object sender, EventArgs e)
        {
            if (!Network.IsConnected)
                return;

            byte[] bytes = Message.Encode(Enums.MessageType.OfferDraw);
            Network.Send(bytes);
        }

        private async void checkNetworkTimerTick(object sender, EventArgs e)
        {
            if (!Network.IsStarted)
                return;

            if (!Network.BytesAvailable())
                return;

            var bytes = await Network.Read();
            var messageType = Message.GetType(bytes);

            if (messageType == Enums.MessageType.Move)
            {
                Move move = Message.DecodeMove(bytes);
            

                RegisterCapture(move);
                Board.ApplyMove(move);
                CurrentTurn.Text = Board.CurrentTurn.ToString() + " To Move";
                SyncBoard.MovePieceVisual(move);

                ShowCapturedPieces();

                UpdateMoveList(move);

                Enums.WinResult winResult = Board.GetWinResult();
                if (winResult != Enums.WinResult.None)
                {
                    if (winResult == Enums.WinResult.WhiteWin)
                        MessageBox.Show("White won!");

                    if (winResult == Enums.WinResult.BlackWin)
                        MessageBox.Show("Black won!");

                    if (winResult == Enums.WinResult.Stalemate)
                        MessageBox.Show("The match ended in a stalemate");

                    ResetAll();
                }
            }

            if (messageType == Enums.MessageType.Resign)
            {
                ResetAll();
            }

            if (messageType == Enums.MessageType.OfferDraw)
            {
                string message = "The other player has offered a draw. Do you accept?";
                string caption = "Draw offer";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                DialogResult result = MessageBox.Show(message, caption, buttons);

                if (result == DialogResult.Yes)
                {
                    Network.Send(Message.Encode(Enums.MessageType.AcceptDraw));
                    ResetAll();
                }
                else
                {
                    Network.Send(Message.Encode(Enums.MessageType.DeclineDraw));
                }
            }

            if (messageType == Enums.MessageType.AcceptDraw)
            {
                ResetAll();
            }

            if (messageType == Enums.MessageType.DeclineDraw)
            {
                string message = "The other player declined the draw";
                string caption = "Draw offer";
                MessageBox.Show(message, caption);
            }
        }

        public void ResetAll()
        {
            Text = "Ultimate Chess Pro 2026 ♔";

            Network.Close();
            Network = new Network();
            Board = new Board();
            Board.SetupStartingPosition();

            SyncBoard.Reset();
            SyncBoard = new SyncBoard(Board, Network, this, Controls);

            CapturedPiecesWhite = new Dictionary<Enums.PieceType, int>();
            CapturedPiecesBlack = new Dictionary<Enums.PieceType, int>();

            BpC.Text = "x0";
            BnC.Text = "x0";
            BbC.Text = "x0";
            BrC.Text = "x0";
            BqC.Text = "x0";

            WpC.Text = "x0";
            WnC.Text = "x0";
            WbC.Text = "x0";
            WrC.Text = "x0";
            WqC.Text = "x0";
        } 

        public void ShowCapturedPieces()
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

        public void RegisterCapture(Move move)
        {
            Piece capturedPiece = Board.Grid[move.To.row, move.To.col];

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

        public void SetMove() { CurrentTurn.Text = Board.CurrentTurn.ToString() + " To Move"; }

        public void UpdateMoveList(Move move)
        {
            if (moveList.Items.Count == 0)
            {
                var newItem = new ListViewItem(move.From.ToString() + " -> " + move.To.ToString());
                moveList.Items.Add(newItem);
            }
            else if (moveList.Items[moveList.Items.Count - 1].SubItems[0] == null)
            {
                moveList.Items[-1].SubItems.Add(move.From.ToString() + " -> " + move.To.ToString());
            }
            else
            {
                var newItem = new ListViewItem(move.From.ToString() + " -> " + move.To.ToString());
                moveList.Items.Add(newItem);
            }
        }
    }
}
