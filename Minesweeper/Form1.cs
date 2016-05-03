using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Minesweeper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Text = "Minesweeper - Flags : " + (Minesweeper.Bombs - Minesweeper.Flags) + " in "
                        + Minesweeper.Time + " secounds - by Matin Lotfali";
            Minesweeper.Initialize();
            while (ClientSize.Width < Minesweeper.FormBitmap.Size.Width) Width++;
            while (ClientSize.Height < Minesweeper.FormBitmap.Size.Height) Height++;
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Minesweeper.GameOver)
            {
                if (e.Button == MouseButtons.Left)
                    Minesweeper.MouseClick();
                else if (e.Button == MouseButtons.Right)
                {
                    Minesweeper.MouseRightClick();
                    Text = "Minesweeper - Flags : " + (Minesweeper.Bombs - Minesweeper.Flags) + " in "
                        + Minesweeper.Time + " secounds - by Matin Lotfali";
                }
                Graphics.FromHwnd(this.Handle).DrawImage(Minesweeper.FormBitmap, 0, 0);

                if (Minesweeper.GameOver)
                {
                    System.Threading.Thread.Sleep(1000);
                    if (Minesweeper.Lost)
                    {
                        Minesweeper.Score.Info.Text = "OH NO!!! You lost!";
                        Minesweeper.Score.Load();
                        Minesweeper.Score.ShowDialog();
                    }
                    else
                    {
                        Minesweeper.Score.Info.Text = "Congratulations. You won!";
                        Minesweeper.Score.AddToScores(Minesweeper.Time);
                    }

                }
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (Minesweeper.MouseMove(e.X, e.Y) && !Minesweeper.GameOver)
                Graphics.FromHwnd(this.Handle).DrawImage(Minesweeper.FormBitmap, 0, 0);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics.FromHwnd(this.Handle).DrawImage(Minesweeper.FormBitmap, 0, 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Minesweeper.Tick();
            Text = "Minesweeper - Flags : " + (Minesweeper.Bombs - Minesweeper.Flags) + " in "
                       + Minesweeper.Time + " secounds - by Matin Lotfali";
        }
    }
}
