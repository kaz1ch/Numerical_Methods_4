using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numerical_Methods_4
{
    public partial class Form1 : Form
    {
        const double l = Math.PI / 2;
        const double a2 = 1.0;

        public Form1()
        {
            InitializeComponent();
        }

        double Function_u0(double x)
        {
            if (x >= 0 && x <= (l / 3))
                return 3 * x / l;
            else
                return 1;
        }

        double Function_w0(double x)
        {
            return Function_u0(x) - x / l;
        }

        double Function_w0_withWave(double x, double t, int N, int n)
        {
            double SumRes = 0;
            double xi = l / N;

            for (int i = 1; i != N; i++)
            {
                SumRes += Function_w0(xi) * Math.Sin(Math.PI*n*xi/l);
                xi += l / N;
            }

            return (SumRes * 2 / N);
        }

        double Function_w(double x, double t, int N)
        {
            double SumRes = 0;

            for (int n = 1; n != N; n++)
            {
                SumRes += Function_w0_withWave(x, t, N, n) * Math.Exp(-1 * Math.PI * n / l * Math.PI * n / l * a2 * t) * Math.Sin(Math.PI * n* x / l);
            }

            return SumRes;
        }

        double Function_u(double x, double t, int N)
        {
            return x / l + Function_w(x, t, N);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int N = 10;
            double t0 = l * l / Math.PI / Math.PI / a2 / 100;
            double x = 0, y = 0, step = 0.01;

            while (x <= l)
            {
                y = Function_u0(x);
                this.chart1.Series[0].Points.AddXY(x, y);

                y = Function_u(x, 0, N);
                this.chart1.Series[1].Points.AddXY(x, y);

                for (int j = 1; j != 6; ++j)
                {
                    y = Function_u(x, t0 * j * j, N);
                    this.chart1.Series[j + 1].Points.AddXY(x, y);
                }

                y = Function_u(x, t0 * 20 * 20, N);
                this.chart1.Series[7].Points.AddXY(x, y);

                y = Function_u(x, t0 * 100 * 100, N);
                this.chart1.Series[8].Points.AddXY(x, y);

                x += step;
            }

        }
    }
}
