using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NeuralNetwork1;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private int[]   m_Vector;
        private bool    m_IsMouseDown;

        public Form1()
        {
            InitializeComponent();

            this.m_Vector       = new int[64];
            this.m_IsMouseDown  = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if(m_IsMouseDown == true)
            {
                int c = e.X / 32;
                int l = e.Y / 32;

                int index = 8 * l + c;

                m_Vector[index] = 1;

                panel1.Invalidate(true); // declenche l'appel de la fonction paint
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            m_IsMouseDown = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            m_IsMouseDown = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.LightGray);

            for (int i=0; i<m_Vector.Length; i++)
            {
                if(m_Vector[i] == 1)
                {
                    // Dessiner un petit rectangle noir correspondant à l'index i

                    int l = i / 8;
                    int c = i % 8;

                    int x = c * 32;
                    int y = l * 32;

                    e.Graphics.FillRectangle(Brushes.Black, x, y, 32, 32);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filename = "D:\\base.txt";

            string s;
            s = textBox1.Text;

            if(s == "")
            {
                MessageBox.Show("Veuillez saisir la classe");
                return;
            }

            for (int i = 0; i < m_Vector.Length; i++)
                s = s + " " + m_Vector[i].ToString();

            File.AppendAllText(filename, s + "\n");

            Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            for (int i = 0; i < m_Vector.Length; i++)
                m_Vector[i] = 0;

            panel1.Invalidate(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] layers = new[] { 64, 32, 3 };

            var nn = new NeuralNetwork(layers)
            {
                Iterations = 1000,              //training iterations
                Alpha = 3.5,                    //learning rate, lower is slower, too high may not converge.
                L2_Regularization = true,       //set L2 regularization to prevent overfitting
                Lambda = 0.0003,                //strength of L2
                Rnd = new Random(12345)         //provide a seed for repeatable outputs
            };

            // nn.Train()
        }
    }
}
