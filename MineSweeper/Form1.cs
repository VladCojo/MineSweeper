using System;
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
        // MAI TREBUIE SA ADAUGI NIVELE DE DIFICULTATE + RECURSIVITATE PENTRU PATRATELE GOALE
        private static int ROW = 10;
        private static int COL = 10;
        private Button[,] tiles = new Button[ROW, COL];
        private bool [,] hasBomb = new bool[ROW, COL];
        private bool flagMode = false;
        private int points = 0;


        private static int NUM_BOMBS = 20;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {  
            load_tiles();
            placeBombs();

            //Delet this after verifying that the win condition works
            for (int i = 0; i < ROW; i++)
                for (int j = 0; j < COL; j++)
                    if (hasBomb[i, j])
                        tiles[i, j].Text = "💣";
        }


        private void ClickTile(object sender, EventArgs e)
        {
            Color customColor = Color.FromArgb(208, 208, 208);
            Button clickedButton = (Button)sender;
            Tuple<int, int> indeces = (Tuple<int, int>)clickedButton.Tag;

            int clickedRow = indeces.Item1;
            int clickedCol = indeces.Item2;

            
            if (!flagMode)
            {
                if (clickedButton.Text == "F")
                    return;

                if (hasBomb[clickedRow, clickedCol])
                {

                    clickedButton.Text = "💣";

                    // Reveal all the bombs
                    for (int i = 0; i < ROW; i++)
                        for (int j = 0; j < COL; j++)
                            if (hasBomb[i, j])
                                tiles[i, j].Text = "💣";
                    DialogResult result = MessageBox.Show("Oops, you clicked on a bomb!\nPlay Again?",
                                                           "", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        Reset();
                    }
                    else
                    {
                        Application.Exit();
                    }

                }
                else if (clickedButton.BackColor != customColor) // check that the button was not pressed prev
                {

                    clickedButton.BackColor = customColor;
                    //checkForBombsAround(clickedRow, clickedCol, clickedButton);


                    

                    int adjacentBombs = CountAdjacentBombs(clickedRow, clickedCol);
                    writeToTile(adjacentBombs, clickedButton);

                    if(adjacentBombs == 0)
                    {
                        RevealEmptyNeighbors(clickedRow, clickedCol);
                    }

                    points++;
                    lblScore.Text = points.ToString();
                    if (points == ROW * COL - NUM_BOMBS)
                    {
                        DialogResult result = MessageBox.Show("YOU WON!\nPlay Again?",
                                               "", MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            Reset();
                        }
                        else
                        {
                            Application.Exit();
                        }

                    }



                }
                
            } 
            else if (flagMode)
            {
                ToggleFlag(clickedButton);
            }
            
            
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            for (int i = 0; i < ROW; i++)
                for (int j = 0; j < COL; j++)
                {
                    tiles[i, j].Text = "";
                    tiles[i, j].ForeColor = Color.Black;
                    tiles[i, j].BackColor = Color.White;
                    hasBomb[i, j] = false;

                }

            points = 0;
            lblScore.Text = "0";
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
                    tiles[i, j].Text = "";
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

        private void ToggleFlag(Button btn)
        {
            if (btn.Text == "")
            {
                btn.Text = "F"; 
                btn.ForeColor = Color.Red; 
            }
            else if (btn.Text == "F")
            {
                btn.Text = ""; 
                btn.ForeColor = Color.Black; 
            }
        }

        private void btnFlagMode_Click(object sender, EventArgs e)
        {
            flagMode = !flagMode;
            btnFlagMode.Text = flagMode ? "Flag Mode ON" : "Flag Mode OFF";
        }

        // TRY TO COMBINE CountAdjacentBombs with checkForBombsAround to make the code shorter
        private int CountAdjacentBombs(int row, int col)
        {
            int bombs = 0;

            // Loop through the adjacent tiles
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    // Check if the current tile is within the grid boundaries
                    if (i >= 0 && i < ROW && j >= 0 && j < COL)
                    {
                        // Skip the current tile itself
                        if (!(i == row && j == col))
                        {
                            // If the current adjacent tile has a bomb, increment the bomb count
                            if (hasBomb[i, j])
                            {
                                bombs++;
                            }
                        }
                    }
                }
            }

            return bombs;
        }

        private void RevealEmptyNeighbors(int row, int col)
        {
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < ROW && j >= 0 && j < COL && !(i == row && j == col))
                    {
                        Button neighborButton = tiles[i, j];
                        if (neighborButton.BackColor != Color.FromArgb(208, 208, 208))
                        {
                            neighborButton.BackColor = Color.FromArgb(208, 208, 208);
                            int adjacentBombs = CountAdjacentBombs(i, j);
                            writeToTile(adjacentBombs, neighborButton);
                            points++;
                            if (adjacentBombs == 0)
                            {
                                
                                RevealEmptyNeighbors(i, j); // Recursively reveal neighboring tiles
                            }
                        }
                    }
                }
            }
        }
    }
}
