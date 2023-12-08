using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace YAGOL2023
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Field.SetSize(200);
            Field.RandomFill();
            
        }

        public Field Field { get; set; } = new Field();

        private Color color1 = Color.Plum;
        private Color color2 = Color.Pink;

        private void button1_Click(object sender, EventArgs e)
        {
            var g = panel1.CreateGraphics();
            /// g.Clear(Color.AliceBlue);
            g.Clear(color1);
            
            var pen = new Pen(color2);

            Field.Paint(g,pen);
            g.Dispose();
            Field.Update();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var sz = (int)numericUpDown1.Value;
            if (sz<20) sz = 20;
            if (sz>600) sz = 600;
            Field.SetSize(sz);
            button1_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var t = (int)trackBar1.Value;
            Field.RandomFill(t);
            button1_Click(sender, e);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var t = (int)trackBar1.Value;
            Field.SimmetryFill(t);
            button1_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var N = (int)numericUpDown2.Value;
            progressBar1.Value = 0;
            progressBar1.Maximum = N;

            for (int i = 0; i < N; i++)
            {
                Field.Update();
                button1_Click(sender, e);
                progressBar1.Increment(1);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            // this.colorDialog1 = new ColorDialog();
            colorDialog1.ShowDialog();
            color1 = this.colorDialog1.Color;
        }
        
        private void button7_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            color2 = this.colorDialog1.Color;
        }
    }
}
