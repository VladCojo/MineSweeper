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

                tile[i] = new PictureBox();

                tile[i].Height = 40;
                tile[i].Width = 40;
                tile[i].Left = x;
                tile[i].Top = y;
                tile[i].BorderStyle = BorderStyle.FixedSingle;
                tile[i].Image = imageList1.Images[2];
               

                if (num == 0)
                {
                    tile[i].Tag = "bomb";
                }
                else
                {
                    tile[i].Tag = "ok";
                }

                tile[i].SizeMode = PictureBoxSizeMode.StretchImage;
               

                tile[i].Click += new EventHandler(ClickTile);

                panel1.Controls.Add(tile[i]);

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
            PictureBox tile = (PictureBox)sender;
            MessageBox.Show(tile.Tag.ToString());
        }
    }
}
