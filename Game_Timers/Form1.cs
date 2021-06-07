using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game_Timers
{
    public partial class Form1 : Form
    {
        private static Button currentB = null;
        private static int counter = 0;
        private static int N = 16;
        private static int xBound=168;
        private static int yBound = 243;
        private static int xAdd=56;
        private static int yAdd = 63;
        // Button[] buttons= new Button[15];
        public Form1()
        {
            InitializeComponent();
        }
        private void swapEmpty(Button current)
        {
            int x = 0;
            int y = 54;
            bool found = false;
            for (int i = 1; i < N; i++)
            {
                found = false;
                foreach (Control control in Controls)
                {
                    if (control.Location.X == x && control.Location.Y == y)
                    {
                        if (x < xBound)
                        {
                            x += xAdd;
                            found = true;
                            break;
                        }
                        else
                        {
                            x = 0;
                            y += yAdd;
                            found = true;
                            break;
                        }

                    }
                }
                if (found == false)
                {
                    current.Location = new Point(x, y);
                }
            }
        }
        private void shuffle()
        {
            int[] arr = new int[N - 1];
            int i = 0;
            for (i = 0; i < N - 1; i++)
                arr[i] = i + 1;
            Random myRand = new Random();
            for (i = N - 2; i >= 0; i--)
            {
                int R = myRand.Next(i);

                int temp = arr[i];
                arr[i] = arr[R];
                arr[R] = temp;
            }
            int ctr = 0;
            foreach (Control control in Controls)
            {


                if (control.Location.X == xBound && control.Location.Y == yBound)
                {
                    swapEmpty((Button)control);
                }
                if (ctr == 15)
                {
                    break;
                }



                control.Text = arr[ctr].ToString();
                control.BackColor = Color.FromArgb(myRand.Next(256), myRand.Next(256), myRand.Next(256));
                // buttons[i] = (Button)control;

                ctr++;

            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            shuffle();
        }

        private bool leftEmpty(Button current)
        {
            foreach (Control control1 in Controls)
            {
                if (((current.Location.X - xAdd) == control1.Location.X) && current.Location.Y == control1.Location.Y)
                {
                    return false;
                }
                else continue;

            }

            return true;
        }
        private bool rightEmpty(Button current)
        {
            foreach (Control control1 in Controls)
            {
                if (current == control1)
                    continue;

                if (((current.Location.X + xAdd) == control1.Location.X) && current.Location.Y == control1.Location.Y)
                    return false;

            }

            return true;
        }
        private bool downEmpty(Button current)
        {
            foreach (Control control1 in Controls)
            {
                if ((current.Location.Y + yAdd) == control1.Location.Y && current.Location.X == control1.Location.X)
                {
                    return false;
                }
                else continue;

            }

            return true;
        }
        private bool upEmpty(Button current)
        {
            foreach (Control control1 in Controls)
            {
                if ((current.Location.Y - 63) == control1.Location.Y && current.Location.X == control1.Location.X)
                {
                    return false;
                }
                else continue;

            }

            return true;
        }
        private void checkboard()
        {
            int x = 0;
            int y = 54;
            bool found = false;
            for (int i = 1; i < 3; i++)
            {
                found = false;
                foreach (Control control in Controls)
                {

                    if (control.Location.X == x && control.Location.Y == y)
                    {

                        if (control.Text == i.ToString())
                        {
                            if (x < xBound)
                            {
                                x += xAdd;
                                found = true;
                                break;
                            }
                            else
                            {
                                x = 0;
                                y += yAdd;
                                found = true;
                                break;
                            }
                        }
                        else
                            return;
                    }


                }
                if (found == false)
                    return;
            }
            DialogResult dialogResult = MessageBox.Show("You Won!\n             New Game?", "You Won!", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                shuffle();
            }
            else
            {
                Close();
            }
        }
        private void any_buttonClick(object sender, EventArgs e)
        {

            if (timer1.Enabled == true || timer2.Enabled == true || timer3.Enabled == true || timer4.Enabled == true)
                return;
            counter = 0;

            Button current = (Button)sender;


            if ((current.Location.X < xBound) && rightEmpty(current)) // && button15.Location.Y < 579)
            {
                currentB = current;
                timer1.Start();
                counter = 0;
                //  button15.Location = p;
            }
            else if ((current.Location.Y < yBound) && (downEmpty(current)))
            {
                currentB = current;
                timer2.Start();
                counter = 0;
            }
            else if ((current.Location.X > 0) && leftEmpty(current))
            {
                currentB = current;
                timer3.Start();
                counter = 0;
            }
            else if ((current.Location.Y > 0) && upEmpty(current))
            {
                currentB = current;

                timer4.Start();
                counter = 0;
            }
            else
            {
                return;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            currentB.Location = new Point(currentB.Location.X + 4, currentB.Location.Y);

            counter++;
            if (counter == 14)
            {

                timer1.Stop();
                checkboard();
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true || timer2.Enabled == true || timer3.Enabled == true || timer4.Enabled == true)
                return;
            shuffle();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            currentB.Location = new Point(currentB.Location.X, currentB.Location.Y + 3);

            counter++;
            if (counter >= 21)
            {
                checkboard();
                timer2.Stop();
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            currentB.Location = new Point(currentB.Location.X - 4, currentB.Location.Y);

            counter++;
            if (counter == 14)
            {

                timer3.Stop();
                checkboard();
            }
        }

        private void timer4_Tick(object sender, EventArgs e)
        {
            currentB.Location = new Point(currentB.Location.X, currentB.Location.Y - 3);

            counter++;
            if (counter >= 21)
            {

                timer4.Stop();
                checkboard();

            }
        }
    }
}
