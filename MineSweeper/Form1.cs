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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {  
            load_tiles();

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

                    panel1.Controls.Add(tiles[i, j]);

                    x += 40;
                    
                }
                y += 40;
                x = 0;
            }
        }

        private void ClickTile(object sender, EventArgs e)
        {
            MessageBox.Show("btn pressed");
        }
    }
}
