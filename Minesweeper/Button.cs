using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Minesweeper
{
    class Button
    {
        private static Bitmap Default;
        private static Bitmap Up;
        private static Bitmap Down;
        private static Bitmap Flag;
        private static Bitmap Cross;
        private static Bitmap Bomb;
        private static Font myFont;
        public bool IsButtonDown = false;
        public bool IsMouseEnter = false;
        public bool IsFlagged = false;
        public bool IsCrossed = false;
        public char Data = '0';
        public int Left = 0;
        public int Top = 0;
        public int Height = 20;
        public int Width = 20;


        public Button(int Left, int Top, int Height, int Width)
        {
            this.Left = Left;
            this.Top = Top;
            this.Height = Height;
            this.Width = Width;
            if (Flag == null) DrawImages();
        }

        public void MouseClick()
        {
            IsButtonDown = true;
            DrawButton();
        }

        public void DrawButton()
        {
            if (IsButtonDown)
            {
                Minesweeper.Graph.DrawImage(Down, Left, Top);
                if (Data != '0' && Data != 'b')
                    Minesweeper.Graph.DrawString
                        (Data.ToString(), myFont, Brushes.Black
                        , Left + Width / 6, Top + Height / 8);
                else if (Data == 'b')
                    Minesweeper.Graph.DrawImage(Bomb, Left, Top);
            }
            else if (IsMouseEnter)
            {
                Minesweeper.Graph.DrawImage(Up, Left, Top);
                if (IsFlagged)
                    Minesweeper.Graph.DrawImage(Flag, Left, Top);
                if (IsCrossed)
                    Minesweeper.Graph.DrawImage(Cross, Left, Top);
            }
            else
            {
                Minesweeper.Graph.DrawImage(Default, Left, Top);
                if (IsFlagged)
                    Minesweeper.Graph.DrawImage(Flag, Left, Top);
                if (IsCrossed)
                    Minesweeper.Graph.DrawImage(Cross, Left, Top);
            }
        }

        public void MouseRightClick()
        {
            if (!IsFlagged)
            {
                if (Minesweeper.Flags < Minesweeper.Bombs)
                {
                    IsFlagged = true;
                    Minesweeper.Flags++;
                }
            }
            else
            {
                IsFlagged = false;
                Minesweeper.Flags--;
            }
            DrawButton();
        }

        private void DrawImages()
        {
            myFont = new Font("tahoma", Width / 2);
            Default = new Bitmap(Width, Height);
            Up = new Bitmap(Width, Height);
            Down = new Bitmap(Width, Height);
            Flag = new Bitmap(Width, Height);
            Cross = new Bitmap(Width, Height);
            Bomb = new Bitmap(Width, Height);

            Pen myPen = new Pen(Color.Black);
            Graphics.FromImage(Default).DrawRectangle(myPen, 0, 0, Width - 1, Height - 1);
            for (int i = 0; i < Height - 2; i++)
            {
                int r = Scale(0, 63, i, Height - 2);
                int g = Scale(162, 72, i, Height - 2);
                int b = Scale(232, 204, i, Height - 2);
                myPen.Color = Color.FromArgb(r, g, b);
                Graphics.FromImage(Default).DrawLine(myPen, 1, 1 + i, Width - 2, 1 + i);
            }

            myPen.Color = Color.Black;
            Graphics.FromImage(Up).DrawRectangle(myPen, 0, 0, Width - 1, Height - 1);
            for (int i = 0; i < Height - 2; i++)
            {
                int r = Scale(255, 255, i, Height - 2);
                int g = Scale(242, 201, i, Height - 2);
                int b = Scale(0, 14, i, Height - 2);
                myPen.Color = Color.FromArgb(r, g, b);
                Graphics.FromImage(Up).DrawLine(myPen, 1, 1 + i, Width - 2, 1 + i);
            }

            myPen.Color = Color.Black;
            Graphics.FromImage(Down).Clear(Color.White);
            Graphics.FromImage(Down).DrawRectangle(myPen, 0, 0, Width - 1, Height - 1);

            myPen.Color = Color.Black;
            myPen.Width = Height / 10;
            Graphics.FromImage(Flag).Clear(Color.FromArgb(0, Color.White));
            Graphics.FromImage(Flag).DrawLine(myPen, Width / 6, Height * 5 / 6, Width * 5 / 6, Height * 5 / 6);
            Graphics.FromImage(Flag).DrawLine(myPen, Width / 2, Height / 6, Width / 2, Height * 5 / 6);
            Graphics.FromImage(Flag).FillPie(Brushes.Red, Width / 6, Height / 6, Width / 2, Height / 2, 90, 180);

            Graphics.FromImage(Bomb).Clear(Color.FromArgb(0, Color.White));
            Graphics.FromImage(Bomb).FillEllipse(Brushes.Black, Width / 4, Height / 4
                        , Width / 2, Height / 2);
            Graphics.FromImage(Bomb).DrawLine(myPen, Width / 2, Height / 6, Width / 2, Height * 5 / 6);
            Graphics.FromImage(Bomb).DrawLine(myPen, Width / 6, Height / 2, Width * 5 / 6, Height / 2);

            myPen.Color = Color.Red;
            myPen.Width = 3;
            Graphics.FromImage(Cross).Clear(Color.FromArgb(0, Color.White));
            Graphics.FromImage(Cross).DrawLine(myPen, 0, 0, Width, Height);
            Graphics.FromImage(Cross).DrawLine(myPen, 0, Height, Width, 0);

        }

        public void WrongFlagged()
        {
            IsCrossed = true;
            DrawButton();
        }

        private int Scale(double from, double to, double i, double until)
        { return (int)((to - from) * i / until + from); }


    }
}
