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


            for(int i = 0; i < 100; i++)
            {
                tile[i] = new PictureBox();

                tile[i].Height = 40;
                tile[i].Width = 40;
                tile[i].Left = x;
                tile[i].Top = y;

                tile[i].BorderStyle = BorderStyle.FixedSingle;
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
    }
}
