using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Minesweeper
{
    public partial class Score : Form
    {
        public Score()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            l1.Text = t1.Text;
            l2.Text = t2.Text;
            l3.Text = t3.Text;
            l4.Text = t4.Text;
            l5.Text = t5.Text;

            t1.Visible = false;
            t2.Visible = false;
            t3.Visible = false;
            t4.Visible = false;
            t5.Visible = false;

            StreamWriter myFile = new StreamWriter("scores.txt", false);
            myFile.WriteLine(l1.Text);
            myFile.WriteLine(time1.Text);
            myFile.WriteLine(l2.Text);
            myFile.WriteLine(time2.Text);
            myFile.WriteLine(l3.Text);
            myFile.WriteLine(time3.Text);
            myFile.WriteLine(l4.Text);
            myFile.WriteLine(time4.Text);
            myFile.WriteLine(l5.Text);
            myFile.WriteLine(time5.Text);
            myFile.Close();

            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void AddToScores(int Time)
        {
            Load();
            button1.Visible = true;
            if (Time <= Convert.ToInt32(time1.Text))
            {
                l5.Text = l4.Text;
                l4.Text = l3.Text;
                l3.Text = l2.Text;
                l2.Text = l1.Text;

                time5.Text = time4.Text;
                time4.Text = time3.Text;
                time3.Text = time2.Text;
                time2.Text = time1.Text;
                time1.Text = Time.ToString();

                t1.Visible = true;
            }
            else if (Time <= Convert.ToInt32(time2.Text))
            {
                l5.Text = l4.Text;
                l4.Text = l3.Text;
                l3.Text = l2.Text;

                time5.Text = time4.Text;
                time4.Text = time3.Text;
                time3.Text = time2.Text;
                time2.Text = Time.ToString();

                t2.Visible = true;
            }
            else if (Time <= Convert.ToInt32(time3.Text))
            {
                l5.Text = l4.Text;
                l4.Text = l3.Text;

                time5.Text = time4.Text;
                time4.Text = time3.Text;
                time3.Text = time2.Text;
                time2.Text = Time.ToString();

                t3.Visible = true;
            }
            else if (Time <= Convert.ToInt32(time4.Text))
            {
                l5.Text = l4.Text;

                time5.Text = time4.Text;
                time4.Text = Time.ToString();

                t4.Visible = true;
            }
            else if (Time <= Convert.ToInt32(time5.Text))
            {
                time5.Text = Time.ToString();

                t5.Visible = true;
            }
            else
                button1.Visible = false;

            ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Minesweeper.Initialize();
            Close();
        }

        private void Score_Shown(object sender, EventArgs e)
        {

        }

        public void Load()
        {
            StreamReader myFile = new StreamReader("scores.txt");
            l1.Text = myFile.ReadLine();
            t1.Text = l1.Text;
            time1.Text = myFile.ReadLine();
            l2.Text = myFile.ReadLine();
            t2.Text = l2.Text;
            time2.Text = myFile.ReadLine();
            l3.Text = myFile.ReadLine();
            t3.Text = l3.Text;
            time3.Text = myFile.ReadLine();
            l4.Text = myFile.ReadLine();
            t4.Text = l4.Text;
            time4.Text = myFile.ReadLine();
            l5.Text = myFile.ReadLine();
            t5.Text = l5.Text;
            time5.Text = myFile.ReadLine();
            myFile.Close();
        }
    }
}
