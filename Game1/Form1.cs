using System;
using System.Drawing;
using System.Windows.Forms;
using System.Media;

namespace Game1
{
    public partial class Form1 : Form
    {
        public struct Point
        {
            public Point(int x, int y, bool c, Color color)
            {
                X = x;
                Y = y;
                C = c;
                Color = color;
            }

            public int X;

            public int Y;

            public bool C;

            public Color Color;


            public void changeY(int y)
            {
                Y = y;
            }
            public void changeX(int x)
            {
                X = x;
            }
            public void changeC(bool c)
            {
                C = c;
            }

        }



        Point[] _points;

        Color[] colors = new Color[5] { Color.Brown, Color.DarkGray, Color.Red, Color.Green, Color.Yellow };

        Random r;

        private void Generate(int i)
        {
            Color c = colors[r.Next(0, 4)];
            if (i == 0)
            {   //O
                _points[0] = new Point(4, 19, false, c);
                _points[1] = new Point(4, 18, false, c);
                _points[2] = new Point(5, 19, false, c);
                _points[3] = new Point(5, 18, false, c);

            }
            else if (i == 1)
            {   //L
                _points[0] = new Point(4, 19, false, c);
                _points[1] = new Point(4, 18, false, c);
                _points[2] = new Point(4, 17, false, c);
                _points[3] = new Point(4, 16, false, c);
            }
            else if (i == 2)
            {    
                _points[0] = new Point(4, 19, false, c);
                _points[2] = new Point(5, 19, false, c);
                _points[1] = new Point(4, 18, false, c);
                _points[3] = new Point(3, 18, false, c);
            }
            else if (i == 3)
            {
                _points[0] = new Point(4, 19, false, c);
                _points[1] = new Point(4, 18, false, c);
                _points[2] = new Point(3, 18, false, c);
                _points[3] = new Point(5, 18, false, c);
            }
            else if (i == 4)
            {
                _points[0] = new Point(4, 19, false, c);
                _points[1] = new Point(4, 18, false, c);
                _points[2] = new Point(4, 17, false, c);
                _points[3] = new Point(5, 17, false, c);
            }
            else if (i == 5)
            {
                _points[0] = new Point(4, 19, false, c);
                _points[1] = new Point(4, 18, false, c);
                _points[2] = new Point(4, 17, false, c);
                _points[3] = new Point(3, 17, false, c);
            }
            else if (i == 6)
            {
                _points[0] = new Point(4, 19, false, c);
                _points[1] = new Point(4, 18, false, c);
                _points[2] = new Point(5, 19, false, c);
                _points[3] = new Point(3, 19, false, c);
            }

            
        }

        private void Turn()
        {
            timer1.Stop();
            Point mainPoint = _points[1];
            int dX;
            int dY;
            Point[] save = new Point[4];
            bool check = true;

            for (int i = 0; i < 4; i++)
            {
                save[i] = new Point(_points[i].X, _points[i].Y, _points[i].C, _points[i].Color);               
            }

            for (int i = 0; i < 4; i++)
            {
                if (i != 1)
                {
                    dX = _points[i].X - mainPoint.X;
                    dY = _points[i].Y - mainPoint.Y;
                    int tmp = dX;

                    dX = -dY;
                    dY = tmp;

                    dX += mainPoint.X;
                    dY += mainPoint.Y;

                    int newX = dX;
                    int newY = dY;
                    _points[i] = new Point(newX, newY, false, _points[i].Color);

                    if (_points[i].X < 0 || _points[i].X > 9 || _points[i].Y < 0 || _points[i].Y > 19 || (IsFull(_points[i].X, _points[i].Y) == true))
                    {                      
                        check = false;
                        
                    }
                }
            }
            if (check == false)
            {
                
                for (int i = 0; i < 4; i++)
                {
                    _points[i] = new Point(save[i].X, save[i].Y, save[i].C, save[i].Color);
                    
                }
            }
            timer1.Start();
        }

        private bool IsFull(int x, int y)
        {
            foreach (Point p in points)
            {
                if (p.X == x && p.Y == y)
                {
                    return p.C;
                }
            }
            return true;
        }

        private void Down()
        {
            int i = 0;
            foreach (Point p in _points)
            {
                int n = p.Y;
                Point np = new Point(p.X, p.Y - 1, false, p.Color);
                _points[i] = np;
                i++;

            }
        }

        private void Control()
        {
            foreach (Point p in _points)
            {
                if (points[p.Y - 1, p.X].C)
                {
                    foreach (Point pw in _points)
                    {
                        points[pw.Y, pw.X] = new Point(pw.X, pw.Y, true, pw.Color);
                    }
                    Generate(r.Next(0, 6));
                    break;
                }
                else if (p.Y == 1)
                {
                    foreach (Point pw in _points)
                    {
                        points[pw.Y - 1, pw.X] = new Point(pw.X, pw.Y - 1, true, pw.Color);
                    }
                    Generate(r.Next(0, 6));
                    break;
                }

            }
        }

        private void Visualize()
        {
            foreach (Point p in _points)
            {
                pictures[p.Y, p.X].BackColor = p.Color;

            }
        }

        public Form1()
        {
            InitializeComponent();
        }
        Point[,] points;
        Label[,] labels;
        PictureBox[,] pictures;
        private void Form1_Load(object sender, EventArgs e)
        {         
            points = new Point[20, 10];
            labels = new Label[20, 10];
            _points = new Point[4];
            pictures = new PictureBox[20, 10];

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    PictureBox p = new PictureBox();
                    p.Height = p.Width = 20;
                    p.BackColor = Color.AliceBlue;
                    p.SizeMode = PictureBoxSizeMode.StretchImage;
                    flowLayoutPanel1.Controls.Add(p);

                    Label label = new Label();
                    label.Height = label.Width = 26;
                    label.BackColor = Color.White;
                    label.Text = (j).ToString() + " " + (i).ToString();
                    label.Visible = true;

                    points[j, i] = new Point(i, j, false, Color.White);

                    labels[j, i] = label;
                    pictures[j, i] = p;


                }
            }
            r = new Random();
            Generate(r.Next(0, 6));
        }

        private void B()
        {
            foreach (Point p in points)
            {
                if (p.C)
                {
                    pictures[p.Y, p.X].BackColor = p.Color;

                }
                else
                {
                    pictures[p.Y, p.X].BackColor = p.Color;

                }
            }
        }

        private void DownRow(int i)
        {
            for (int j = 0; j < 10; j++)
            {
                if (points[i, j].C == true)
                {
                    Color c = points[i, j].Color;
                    points[i, j] = new Point(j, i, false, Color.White);
                    points[i - 1, j] = new Point(j, i - 1, true, c);
                }
            }
        }
        int _score = 0;
        private void IsComplete()
        {
            int count = 0;
            for (int i = 19; i >= 0; i--)
            {
                int control = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (points[i, j].C == true)
                    {
                        control++;
                    }
                }
                if (control == 10)
                {
                    _score += 10;
                    scoreLabel.Text = _score.ToString();
                    for (int k = 0; k < 9; k++)
                    {
                        points[i, k] = new Point(k, i, false, Color.White);
                    }
                    count++;
                    for (int m = i + 1; m < 20; m++)
                    {
                        DownRow(m);
                    }
                }
            }
            if (count != 0)
            {
                SoundPlayer soundPlayer = new SoundPlayer(@"..\..\NewFolder1\point.wav");
                soundPlayer.Play();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            Control();
            B();
            Visualize();
            Down();
            IsComplete();
            foreach (Point p in _points)
            {
                if (points[p.Y, p.X].C)
                {
                    timer1.Stop();


                    SoundPlayer soundPlayer = new SoundPlayer(@"..\..\NewFolder1\gameover.wav");
                    soundPlayer.Play();
                    MessageBox.Show("Oyun bitti");
                    break;
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
                    timer1.Start();
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool control = true;

            foreach (Point p in _points)
            {
                if (p.X == 0)
                {
                    control = false;
                    break;
                }
            }
            if (control)
            {
                foreach (Point p in _points)
                {
                    if (IsFull(p.X - 1, p.Y))
                    {
                        control = false;
                        break;
                    }
                }
            }
            if (control)
            {

                int i = 0;
                foreach (Point p in _points)
                {
                    _points[i] = new Point(p.X - 1, p.Y, false, p.Color);
                    i++;

                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bool control = true;

            foreach (Point p in _points)
            {
                if (p.X == 9)
                {
                    control = false;
                    break;
                }
            }
            if (control)
            {
                foreach (Point p in _points)
                {
                    if (IsFull(p.X + 1, p.Y))
                    {
                        control = false;
                        break;
                    }
                }
            }
            if (control)
            {

                int i = 0;
                foreach (Point p in _points)
                {
                    _points[i] = new Point(p.X + 1, p.Y, false, p.Color);
                    i++;

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Turn();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            B();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Control();
            Down();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                timer1.Stop();
                Turn();
                Control();
                B();
                Visualize();
                IsComplete();
                timer1.Start();
            }
            else if(e.KeyCode == Keys.S)
            {
                timer1.Stop();
                Control();
                Down();
                Control();
                B();
                Visualize();
                IsComplete();
                timer1.Start();
            }
            else if(e.KeyCode == Keys.A)
            {
                timer1.Stop();
                bool control = true;

                foreach (Point p in _points)
                {
                    if (p.X == 0)
                    {
                        control = false;
                        break;
                    }
                }
                if (control)
                {
                    foreach (Point p in _points)
                    {
                        if (IsFull(p.X - 1, p.Y))
                        {
                            control = false;
                            break;
                        }
                    }
                }
                if (control)
                {

                    int i = 0;
                    foreach (Point p in _points)
                    {
                        _points[i] = new Point(p.X - 1, p.Y, false, p.Color);
                        i++;

                    }
                }
                Control();
                B();
                Visualize();
                IsComplete();
                timer1.Start();
            }
            else if(e.KeyCode == Keys.D)
            {
                timer1.Stop();

                bool control = true;

                foreach (Point p in _points)
                {
                    if (p.X == 9)
                    {
                        control = false;
                        break;
                    }
                }
                if (control)
                {
                    foreach (Point p in _points)
                    {
                        if (IsFull(p.X + 1, p.Y))
                        {
                            control = false;
                            break;
                        }
                    }
                }
                if (control)
                {

                    int i = 0;
                    foreach (Point p in _points)
                    {
                        _points[i] = new Point(p.X + 1, p.Y, false, p.Color);
                        i++;

                    }
                }
                
                Control();
                B();
                Visualize();
                Down();
                IsComplete();
                timer1.Start();
            }
        }
    }
}
