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

        private int score = 0;
        private PictureBox[] tiles = new PictureBox[100];
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
            PictureBox[] tile = new PictureBox[100];
            int x = 0;
            int y = 0;
            int cnt = 0;
            Random rand = new Random();


            for(int i = 0; i < 100; i++)
            {
                int num = rand.Next(0, 2);

                tiles[i] = new PictureBox();

                tiles[i].Height = 40;
                tiles[i].Width = 40;
                tiles[i].Left = x;
                tiles[i].Top = y;
                tiles[i].BorderStyle = BorderStyle.FixedSingle;
                tiles[i].Image = imageList1.Images[2];
               

                if (num == 0)
                {
                    tiles[i].Tag = "bomb";
                }
                else
                {
                    tiles[i].Tag = "ok";
                }

                tiles[i].SizeMode = PictureBoxSizeMode.StretchImage;
               

                tiles[i].Click += new EventHandler(ClickTile);

                panel1.Controls.Add(tiles[i]);

                x += 40;
                cnt++;

                if(cnt == 10)
                {
                    x = 0;
                    y += 40;
                    cnt = 0;
                }
            }
        }

        private void ClickTile(object sender, EventArgs e)
        {
            DialogResult ans;
            PictureBox tile = (PictureBox)sender;
           
            if(tile.Tag.ToString() == "bomb")
            {
                tile.Image = imageList1.Images[0];
                ans = MessageBox.Show("Game Over.\nScore: " + score+"\nPlay Again?", "Game Over", 
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (ans == DialogResult.Yes)
                {
                    score = 0;
                    lblScore.Text = "0";
                    foreach(PictureBox it in tiles)
                    {
                        panel1.Controls.Remove(it);
                    }
                    load_tiles();
                }
                else
                {
                    Application.Exit();
                }
            }
            else if (tile.Tag.ToString() == "ok")
            {
                tile.Image= imageList1.Images[1];
                score += 5;
                lblScore.Text = score.ToString();
            }
        }
    }
}
