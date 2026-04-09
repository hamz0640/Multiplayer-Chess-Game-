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

namespace Programmeringseksamens_projekt
{
    public partial class Form1 : Form
    {
        Dictionary<(int, int), Panel> boardPanels = new Dictionary<(int, int), Panel>();
        Dictionary<(int, int), Panel> hightlightPanels = new Dictionary<(int, int), Panel>();
        Network network = new Network();

        const int SQUARE_SIZE = 43;

        public Form1()
        {
            InitializeComponent();

            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    Panel panel = new Panel();
                    panel.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
                    panel.Location = new Point(11 + x * SQUARE_SIZE, 11 + y * SQUARE_SIZE);
                    if ((x + (y % 2 == 0 ? 0 : 1)) % 2 == 0)
                        panel.BackColor = Color.SandyBrown;
                    else
                        panel.BackColor = Color.Sienna;

                    boardPanels[(x, y)] = panel;
                    Controls.Add(panel);


                    Panel hightlightPanel = new Panel();
                    hightlightPanel.Size = new Size(SQUARE_SIZE, SQUARE_SIZE);
                    hightlightPanel.Location = new Point(11 + x * SQUARE_SIZE, 11 + y * SQUARE_SIZE);
                    if ((x + (y % 2 == 0 ? 0 : 1)) % 2 == 0)
                        hightlightPanel.BackColor = Color.Peru;
                    else
                        hightlightPanel.BackColor = Color.SaddleBrown;

                    hightlightPanels[(x, y)] = hightlightPanel;
                    hightlightPanel.Hide();

                    Controls.Add(hightlightPanel);
                }
            }


        }

        private void joinButton_Click(object sender, EventArgs e)
        {
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
