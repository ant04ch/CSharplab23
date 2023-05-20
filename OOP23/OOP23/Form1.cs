using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OOP23
{
    public partial class Form1 : Form
    {
        private const int AxisMargin = 40;
        private const float ScaleFactor = 20f;

        private float a;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            a = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!float.TryParse(textBox1.Text, out a))
            {
                MessageBox.Show("Некоректне значення для 'a'.");
                return;
            }

            // Очистити панель 
            panel1.Refresh();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Шрифт
            Font axisLabelFont = new Font("Arial", 8);

            // Кольори для графіку
            Pen axisPen = new Pen(Color.Black, 2);
            Pen graphPen = new Pen(Color.Blue, 2);

            // Розміри панелі
            int panelWidth = panel1.Width;
            int panelHeight = panel1.Height;

            // Центр панелі
            float centerX = panelWidth / 2f;
            float centerY = panelHeight / 2f;

            // Побудова осі координат
            g.DrawLine(axisPen, AxisMargin, centerY, panelWidth - AxisMargin, centerY);
            g.DrawLine(axisPen, centerX, AxisMargin, centerX, panelHeight - AxisMargin);

            // Підписи осей
            g.DrawString("x", axisLabelFont, Brushes.Black, panelWidth - AxisMargin, centerY);
            g.DrawString("y", axisLabelFont, Brushes.Black, centerX, AxisMargin);

            // Побудова значень на осі x
            for (int i = -10; i <= 10; i++)
            {
                float x = centerX + i * ScaleFactor;
                g.DrawString(i.ToString(), axisLabelFont, Brushes.Black, x, centerY);
            }

            // Побудова значень на осі y
            for (int i = -10; i <= 10; i++)
            {
                float y = centerY - i * ScaleFactor;
                g.DrawString(i.ToString(), axisLabelFont, Brushes.Black, centerX, y);
            }

            // Побудувати графік функції
            float tStep = 0.1f;
            float tMax = (float)(2 * Math.PI);

            PointF prevPoint = PointF.Empty;

            for (float t = 0; t <= tMax; t += tStep)
            {
                float x = centerX + a * (t - (float)Math.Sin(t)) * ScaleFactor;
                float y = centerY - a * (t - (float)Math.Cos(t)) * ScaleFactor;
                PointF currentPoint = new PointF(x, y);

                if (!prevPoint.IsEmpty)
                {
                    g.DrawLine(graphPen, prevPoint, currentPoint);
                }

                prevPoint = currentPoint;
            }
        }
    }
}
