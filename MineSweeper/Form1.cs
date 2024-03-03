using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class Form1 : Form
    {

        private static int ROW = 10;
        private static int COL = 10;
        private Button[,] tiles = new Button[ROW, COL];
        private bool [,] hasBomb = new bool[ROW, COL];

        private static int NUM_BOMBS = 10;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {  
            load_tiles();
            placeBombs();
        }

        private void load_tiles()
        {
            
            int x = 0;
            int y = 0;
           
            for(int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    tiles[i, j] = new Button();

                    tiles[i, j].Height = 40;
                    tiles[i, j].Width = 40;
                    tiles[i, j].Left = x;
                    tiles[i, j].Top = y;
                    tiles[i, j].Click += new EventHandler(ClickTile);
                    // to optimize the search for bombs
                    tiles[i, j].Tag = new Tuple<int, int>(i, j);

                    panel1.Controls.Add(tiles[i, j]);

                    x += 40;
                    
                }
                y += 40;
                x = 0;
            }
        }

        private void placeBombs()
        {
            Random random = new Random();

            int bombCount = 0;

            while(bombCount < NUM_BOMBS)
            {
                int row = random.Next(ROW);
                int col = random.Next(COL);

                if (!hasBomb[row, col])
                {
                    hasBomb[row, col] = true;
                    bombCount++;
                }
            }
        }

        private void ClickTile(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            Tuple<int, int> indeces = (Tuple<int, int>)clickedButton.Tag;

            int clickedRow = indeces.Item1;
            int clickedCol = indeces.Item2;

            if (hasBomb[clickedRow, clickedCol])
            {
                MessageBox.Show("OOPS YOU DIED");
            }
            else
            {
                MessageBox.Show("nice");
            }

            
        }
    }
}
