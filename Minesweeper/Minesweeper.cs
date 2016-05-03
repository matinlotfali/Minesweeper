using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Minesweeper
{
    class Minesweeper
    {
        private static int MouseI = -1, MouseJ = -1;
        private static int SizeX = 9, SizeY = 9;
        private static int ButtonsSize = 50;
        private static Button[,] Buttons = new Button[SizeX, SizeY];
        private static bool StartGame = false;
        private static int Pressed = 0;
        public static int Time = 0;
        public static bool Won = false;
        public static bool Lost = false;
        public static bool GameOver = false;
        public static int Bombs = 8;
        public static int Flags = 0;
        public static Bitmap FormBitmap = new Bitmap(SizeX * ButtonsSize, SizeY * ButtonsSize);
        public static Graphics Graph = Graphics.FromImage(FormBitmap);
        public static Score Score;

        public static void Initialize()
        {
            MouseI = -1;
            MouseJ = -1;
            StartGame = false;
            Pressed = 0;
            Won = false;
            Lost = false;
            GameOver = false;
            Flags = 0;
            Time = 0;
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    Buttons[i, j] = new Button(ButtonsSize * i, ButtonsSize * j, ButtonsSize, ButtonsSize);
            InitializeButtons();
            Score = new Score();
            Refresh();
        }

        private static void Refresh()
        {
            for (int i = 0; i < SizeX; i++)
                for (int j = 0; j < SizeY; j++)
                    Buttons[i, j].DrawButton();
        }

        public static bool MouseMove(int MouseX, int MouseY)
        {
            if (MouseI != MouseX / ButtonsSize || MouseJ != MouseY / ButtonsSize && !GameOver)
            {
                if (MouseI != -1)
                    Buttons[MouseI, MouseJ].IsMouseEnter = false;
                MouseI = MouseX / ButtonsSize;
                MouseJ = MouseY / ButtonsSize;
                if (MouseI >= SizeX) MouseI = SizeX - 1;
                if (MouseJ >= SizeY) MouseJ = SizeY - 1;
                Buttons[MouseI, MouseJ].IsMouseEnter = true;
                Refresh();
                return true;
            }
            return false;
        }

        public static void MouseClick()
        {
            if (!StartGame)
            {
                StartGame = true;
                while (Buttons[MouseI, MouseJ].Data == 'b')
                    Initialize();
            }
            Clicker(MouseI, MouseJ);
        }

        public static void MouseRightClick()
        {
            if (!GameOver && !Buttons[MouseI, MouseJ].IsButtonDown)
                Buttons[MouseI, MouseJ].MouseRightClick();
        }

        static void InitializeButtons()
        {
            Random myRandom = new Random();
            int a, i, j;
            for (int z = 0; z < Bombs; z++)
            {
                a = myRandom.Next(SizeX * SizeY);
                i = a / SizeX;
                j = a % SizeY;
                if (Buttons[i, j].Data != 'b')
                {
                    Buttons[i, j].Data = 'b';
                    if (i > 0)
                    {
                        if (Buttons[i - 1, j].Data != 'b')
                            Buttons[i - 1, j].Data++;
                        if (j > 0)
                            if (Buttons[i - 1, j - 1].Data != 'b')
                                Buttons[i - 1, j - 1].Data++;
                        if (j + 1 < SizeY)
                            if (Buttons[i - 1, j + 1].Data != 'b')
                                Buttons[i - 1, j + 1].Data++;
                    }
                    if (j > 0)
                    {
                        if (Buttons[i, j - 1].Data != 'b')
                            Buttons[i, j - 1].Data++;
                        if (i + 1 < SizeX)
                            if (Buttons[i + 1, j - 1].Data != 'b')
                                Buttons[i + 1, j - 1].Data++;
                    }
                    if (i + 1 < SizeX)
                    {
                        if (Buttons[i + 1, j].Data != 'b')
                            Buttons[i + 1, j].Data++;
                        if (j + 1 < SizeY)
                            if (Buttons[i + 1, j + 1].Data != 'b')
                                Buttons[i + 1, j + 1].Data++;
                    }
                    if (j + 1 < SizeY)
                        if (Buttons[i, j + 1].Data != 'b')
                            Buttons[i, j + 1].Data++;
                }
                else
                    z--;
            }
        }

        static void Clicker(int i, int j)
        {
            if (!Buttons[i, j].IsButtonDown && !Buttons[i, j].IsFlagged)
            {
                Pressed++;
                Buttons[i, j].MouseClick();
                if (Buttons[i, j].Data == '0')
                {
                    if (i > 0) Clicker(i - 1, j);
                    if (j > 0) Clicker(i, j - 1);
                    if (i < SizeX - 1) Clicker(i + 1, j);
                    if (j < SizeY - 1) Clicker(i, j + 1);
                    if (i > 0 && j > 0) Clicker(i - 1, j - 1);
                    if (i < SizeX - 1 && j > 0) Clicker(i + 1, j - 1);
                    if (i < SizeX - 1 && j < SizeY - 1) Clicker(i + 1, j + 1);
                    if (i > 0 && j < SizeY - 1) Clicker(i - 1, j + 1);
                }
                else if (Buttons[i, j].Data == 'b')
                {
                    for (i = 0; i < SizeX; i++)
                        for (j = 0; j < SizeY; j++)
                        {
                            if (Buttons[i, j].Data == 'b' && !Buttons[i, j].IsFlagged)
                                Buttons[i, j].MouseClick();
                            if (Buttons[i, j].Data != 'b' && Buttons[i, j].IsFlagged)
                                Buttons[i, j].WrongFlagged();

                        }
                    GameOver = true;
                    Lost = true;
                    Refresh();
                }
                if (Pressed + Bombs == SizeX * SizeY)
                {
                    for (i = 0; i < SizeX; i++)
                        for (j = 0; j < SizeY; j++)
                            if (!Buttons[i, j].IsButtonDown)
                            {
                                Buttons[i, j].IsFlagged = true;
                                Buttons[i, j].DrawButton();
                            }
                    GameOver = true;
                    Won = true;

                }
            }
        }

        public static void Tick()
        {
            if (StartGame && !GameOver)
                Time++;
        }
    }
}
