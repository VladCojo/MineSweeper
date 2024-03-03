﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
        private void btnReset_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ROW; i++)
                for (int j = 0; j < COL; j++)
                {
                    tiles[i, j].Text = "";
                    tiles[i, j].BackColor = Color.White;//SystemColors.Control;
                    hasBomb[i, j] = false;
                    
                }
            
            
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

        private void checkForBombsAround(int row, int col, Button btn)
        {
            int bombs = 0;

            if (row + 1 < ROW && hasBomb[row + 1, col])
            {
                bombs++;
            }
            if (row + 1 < ROW && col - 1 >= 0 && hasBomb[row + 1, col - 1])
            {
                bombs++;
            }
            if (row + 1 < ROW && col + 1 < COL && hasBomb[row + 1, col + 1])
            {
                bombs++;
            }
            if (col + 1 < COL && hasBomb[row, col + 1])
            {
                bombs++;
            }
            if (col - 1 >= 0 && hasBomb[row, col - 1])
            {
                bombs++;
            }
            if (row - 1 >= 0 && hasBomb[row - 1, col])
            {
                bombs++;
            }
            if (row - 1 >= 0 && col - 1 >= 0 && hasBomb[row - 1, col - 1])
            {
                bombs++;
            }
            if (row - 1 >= 0 && col + 1 < COL && hasBomb[row - 1, col + 1])
            {
                bombs++;
            }


            writeToTile(bombs, btn);
        }

        private void writeToTile(int bombs, Button btn)
        {
            btn.Text = bombs.ToString();
            btn.Font = new Font(btn.Font, FontStyle.Bold);

            if(bombs == 0)
            {
                btn.Text = "";
            } 
            else if (bombs == 1)
            {
                btn.ForeColor = Color.Blue;
            } 
            else if (bombs == 2)
            {
                btn.ForeColor = Color.Green;
            }
            else if (bombs == 3)
            {
                btn.ForeColor = Color.Red;
            }
            else if (bombs == 4)
            {
                btn.ForeColor = Color.Yellow;
            }
            else if (bombs == 5)
            {
                btn.ForeColor = Color.Pink;
            }
            else if(bombs == 6)
            {
                btn.ForeColor = Color.Cyan;
            }
            else if (bombs == 7)
            {
                btn.ForeColor = Color.Magenta;
            }
            else if (bombs == 8)
            {
                btn.ForeColor = Color.Black;
            }
        }

        private void ClickTile(object sender, EventArgs e)
        {
            Color customColor = Color.FromArgb(208, 208, 208);
            Button clickedButton = (Button)sender;
            Tuple<int, int> indeces = (Tuple<int, int>)clickedButton.Tag;

            int clickedRow = indeces.Item1;
            int clickedCol = indeces.Item2;

            if (hasBomb[clickedRow, clickedCol])
            {
                
                clickedButton.Text = "X";
            }
            else
            {
                clickedButton.BackColor = customColor;
                checkForBombsAround(clickedRow, clickedCol, clickedButton);
                
            }
            

            
        }

        
    }
}
