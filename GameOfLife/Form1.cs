using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace GameOfLife
{
    public partial class Form1 
        : Form
    {
        bool[,] cells = new bool[Constants.numOfCells, Constants.numOfCells];
        bool[,] cells2 = new bool[Constants.numOfCells, Constants.numOfCells];
        List<int> xind = new List<int>();
        List<int> yind = new List<int>();

        bool flag=true;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            trackBar1.Scroll += trackBar1_Scroll;

        }
        void calcNeighbours(ref bool[,] cells, int x, int y, ref int dead, ref int alive)
        {
            if (x > 0)
            {
                if (y > 0)
                {
                    if (cells[x - 1, y - 1])
                        alive++;
                    else
                        dead++;
                }
                if (y < Constants.numOfCells - 1)
                    if (cells[x - 1, y + 1])
                        alive++;
                    else
                        dead++;
                if (cells[x - 1, y])
                    alive++;
                else
                    dead++;

            }
            if (x < Constants.numOfCells - 1)
            {
                if (y > 0)
                    if (cells[x + 1, y - 1])
                        alive++;
                    else
                        dead++;
                if (y < Constants.numOfCells - 1)
                {
                    if (cells[x + 1, y + 1])
                        alive++;
                    else
                        dead++;
                }
                if (cells[x + 1, y])
                    alive++;
                else
                    dead++;
            }
            if (y > 0)
                if (cells[x, y - 1])
                    alive++;
                else
                    dead++;

            if (y < Constants.numOfCells - 1)
                if (cells[x, y + 1])
                    alive++;
                else
                    dead++;
        }
    void print(bool [,]cells)
        {
            richTextBox1.Text += "start printing"+'\n';
            for (int i = 0; i < Constants.numOfCells; ++i)
            {
                for (int j = 0; j < Constants.numOfCells; ++j)
                    richTextBox1.Text += cells[i, j] + " ";
                richTextBox1.Text += '\n';
            }
            richTextBox1.Text += "end printing"+'\n';
        }
        void clear()
        {
            Graphics g = panel1.CreateGraphics();
            for (int i = 0; i < Constants.numOfCells; ++i)
                for (int j = 0; j < Constants.numOfCells; ++j)
                {
                    cells[i, j] = false;
                    cells2[i, j] = false;
                    g.FillRectangle(new SolidBrush(Color.WhiteSmoke), new Rectangle(i * Constants.cellSize, j * Constants.cellSize, Constants.cellSize, Constants.cellSize));
                    g.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(i * Constants.cellSize, j * Constants.cellSize, Constants.cellSize, Constants.cellSize));
                }
        }
        void seed()
        {
            clear();
            Graphics g = panel1.CreateGraphics();
            if(comboBox1.SelectedIndex == 0)
            {
                go();
       
            }
           else if (comboBox1.SelectedIndex == 1)
            {
                xind = new List<int> { 1, 1, 1 };
                yind = new List<int> { 0, 1, 2 };
            }

            else if (comboBox1.SelectedIndex == 2)
            {
                xind = new List<int> { 1, 1, 1, 2, 2, 2 };
                yind = new List<int> { 1, 2, 3, 0, 1, 2 };
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                xind = new List<int> { 0, 0, 1, 1, 2, 2, 3, 3 };
               yind = new List<int> { 0, 1, 0, 1, 2, 3, 2, 3 };
            }
            else if (comboBox1.SelectedIndex == 4)
            {
                yind = new List<int> { 2, 2, 2, 2, 2, 2,    4, 4, 4, 4,  5,5,5,5,    6,6,6,6,    7,7,7,7,7,7,     9,9,9,  9,9,9,     10,10,10,10,   11,11,11,11,   12,12,12,12,   14,14,14,14,14,14};
                xind = new List<int> { 6, 7, 8, 12,13,14,   4, 9, 11,16, 4,9,11,16,  4,9,11,16,  6,7,8,12,13,14,  6,7,8,12,13,14,    4,9,11,16,      4,9,11,16,     4,9,11,16,    6,7,8,12,13,14};

            }
            else if(comboBox1.SelectedIndex==5)
            {
                xind = new List<int> { 0, 1, 1, 2, 2 };
                yind = new List<int> { 2, 2, 0, 2, 1 };
            }
            else
            {
                xind = new List<int>();
                yind = new List<int>();
                foreach (string a in richTextBox1.Lines[0].Split(' '))
                    xind.Add(int.Parse(a));
                foreach (string a in richTextBox1.Lines[1].Split(' '))
                    yind.Add(int.Parse(a));
            
            }


            for (int i = 0; i < xind.Count; ++i)
            {
                cells2[xind[i], yind[i]] = cells[xind[i], yind[i]] = true;
                g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(xind[i] * Constants.cellSize, yind[i] * Constants.cellSize, Constants.cellSize, Constants.cellSize));
                g.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(xind[i] * Constants.cellSize, yind[i] * Constants.cellSize, Constants.cellSize, Constants.cellSize));
            }
           
           // print(cells);
        }
        void step(bool[,] cells,ref bool [,]cells2)
        {
            Graphics g = panel1.CreateGraphics();
            for (int i = 0; i < Constants.numOfCells; ++i)
                for (int j = 0; j < Constants.numOfCells; ++j)
                {
                    int dead = 0; int alive = 0;
                    calcNeighbours(ref cells, i, j, ref dead, ref alive);
                    if (((cells[i, j]) && (alive < 2)) || ((cells[i, j]) && (alive > 3)))
                    {
                        cells2[i, j] = false;//dies
                        g.FillRectangle(new SolidBrush(Color.WhiteSmoke), new Rectangle(i*Constants.cellSize, j* Constants.cellSize, Constants.cellSize, Constants.cellSize));
                        g.DrawRectangle(new Pen(Color.Black,1), new Rectangle(i * Constants.cellSize, j * Constants.cellSize, Constants.cellSize, Constants.cellSize));
                    }
                    else
                        if ((!cells[i, j]) && (alive == 3))
                    {
                        cells2[i, j] = true;
                        g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(i* Constants.cellSize, j*Constants.cellSize, Constants.cellSize, Constants.cellSize));
                        g.DrawRectangle(new Pen(Color.Black,1), new Rectangle(i * Constants.cellSize, j * Constants.cellSize, Constants.cellSize, Constants.cellSize));

                    }

                }
           // print(cells2);
        }
        void go()
        {
            if (flag)
            {
                for (int i = 0; i < Constants.numOfCells; ++i)
                    for (int j = 0; j < Constants.numOfCells; ++j)
                        cells2[i, j] = cells[i, j];
                step(cells, ref cells2);
            }
            else
            {
                for (int i = 0; i < Constants.numOfCells; ++i)
                    for (int j = 0; j < Constants.numOfCells; ++j)
                        cells[i, j] = cells2[i, j];
                step(cells2, ref cells);
            }
            flag = !flag;
        }
        private void button1_Click(object sender, EventArgs e)
        {

            go();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            panel1.BackColor = Color.WhiteSmoke;
            Graphics g = panel1.CreateGraphics();
            Pen p = new Pen(Color.Black, 1);
            for (int i = 0; i < Constants.numOfCells; i++)
            {
                // Vertical
                g.DrawLine(p, i * Constants.cellSize, 0, i * Constants.cellSize, Constants.numOfCells * Constants.cellSize);
                // Horizontal
                g.DrawLine(p, 0, i * Constants.cellSize, Constants.numOfCells * Constants.cellSize, i * Constants.cellSize);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            seed();
        }

        private void button3_Click(object sender, EventArgs e)
        {         
            seed(); 
            timer1.Start();      
        }

      

        private void timer1_Tick(object sender, EventArgs e)
        {
            go();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        { if (!(comboBox1.SelectedIndex == 6))
            richTextBox1.Text += comboBox1.SelectedItem.ToString() + "mode was chosen. ";
            else
                richTextBox1.Text = String.Empty;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label1.Text = String.Format("Speed: {0}", trackBar1.Value);
            timer1.Interval = trackBar1.Value;
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            Graphics g = panel1.CreateGraphics();
            int x = e.X / Constants.cellSize;
            int y = e.Y / Constants.cellSize;
            if ((xind.Contains(x)) && (yind.Contains(y)))
            {//deleting element
                xind.Remove(x);
                yind.Remove(y);
                cells2[x, y] = cells[x, y] = false;
                g.FillRectangle(new SolidBrush(Color.WhiteSmoke), new Rectangle(x * Constants.cellSize, y * Constants.cellSize, Constants.cellSize, Constants.cellSize));
                g.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(x * Constants.cellSize, y * Constants.cellSize, Constants.cellSize, Constants.cellSize));

            }
            else
            {
                xind.Add(x);
                yind.Add(y);
                cells2[x, y] = cells[x, y] = true;
                g.FillRectangle(new SolidBrush(Color.CornflowerBlue), new Rectangle(x * Constants.cellSize, y * Constants.cellSize, Constants.cellSize, Constants.cellSize));
                g.DrawRectangle(new Pen(Color.Black, 1), new Rectangle(x * Constants.cellSize, y * Constants.cellSize, Constants.cellSize, Constants.cellSize));
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
        }
    }
   
}
