using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Bitmap bitmap;
        Graphics g;
        SolidBrush brush = new SolidBrush(Color.Black);
        void Setpixel(int x, int y, SolidBrush color)//Рисование пикселя
        {
            g.FillRectangle(color, x, y, 1, 1);
        }
        void Pixel4(int x, int y, int dx, int dy, SolidBrush color)//Рисование пикселя для первого квадранта, и симметрично для остальных
        {
            Setpixel(x + dx, y + dy, color);
            Setpixel(x + dx, y - dy, color);
            Setpixel(x - dx, y - dy, color);
            Setpixel(x - dx, y + dy, color);
        }
        void Draw_Ellipse(int x, int y, int a, int b, SolidBrush color)
        {
            bitmap = new Bitmap(pictureBox.Width,pictureBox.Height);
            g = Graphics.FromImage(bitmap);
            int dx = 0;//Компонента x
            int dy = b;//Компонента y
            int a_sqr = a * a;//a^2 большая полуось
            int b_sqr = b * b;//b^2 малая полуось
            int delta = 4 * b_sqr * ((dx + 1) * (dx + 1)) + a_sqr * ((2 * dy - 1) * (2 * dy - 1)) - 4 * a_sqr * b_sqr;//Функция координат точки (x+1, y-1/2)
            while(a_sqr * (2 * dy - 1) > 2 * b_sqr * (dx + 1))//Первая часть дуги
            {
                Pixel4(x, y, dx, dy, color);
                if (delta < 0)// Переход по горизонтали
                {
                    dx++;
                    delta += 4 * b_sqr * (2 * dx + 3);
                }
                else//Переход по диагонали
                {
                    dx++;
                    delta = delta - 8 * a_sqr * (dy - 1) + 4 * b_sqr * (2 * dx + 3);
                    dy--;
                }
            }
            delta = b_sqr * ((2 * dx + 1) * (2 * dx + 1)) + 4 * a_sqr * ((dy + 1) * (dy + 1)) - 4 * a_sqr * b_sqr;//Функция координат точки (x+1/2, y-1)
            while(dy + 1 != 0)//Вторая часть дуги, если не выполняется условие первого цикла, значит выполняется a^2(2y-1) <= 2b^2(x+1)
            {
                Pixel4(x,y,dx,dy,color);
                if (delta < 0)//Переход по вертикали
                {
                    dy--;
                    delta += 4 * a_sqr * (2 * dy + 3);
                }
                else //Переход по диагонали
                {
                    dy--;
                    delta = delta - 8 * b_sqr * (dx + 1) + 4 * a_sqr * (2 * dy + 3);
                    dx++;
                }
            }
            pictureBox.Image = bitmap;
        }

        private void Form_Paint(object sender, PaintEventArgs e)
        {
            Draw_Ellipse(240,240,50,70,brush);
        }
    }
}
