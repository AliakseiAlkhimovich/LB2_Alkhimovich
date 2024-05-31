using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LB2_Alkhimovich
{
    public class Class_Lab2_1
    {
        private double _x;
        private double _xEnd;
        private double _step;
        private int _n;

        public double X
        {
            get => _x;
            set
            {
                _x = value;
            }
        }

        public double XEnd
        {
            get => _xEnd;
            set
            {
                if (value < X)
                    throw new ArgumentException("Значение XEnd не может быть меньше X");
                else
                    _xEnd = value;
            }
        }

        public double Step
        {
            get => _step;
            set
            {
                _step = value;
            }
        }

        public int N
        {
            get => _n;
            set
            {
                if (value < 3) 
                throw new ArgumentException("Значение должно быть больше либо равно 3"); 
                else 
                _n=value; 
            }
        }


        public Class_Lab2_1(double x, double xEnd, double step, int n)
        {
            X = x;
            XEnd = xEnd;
            Step = step;
            N = n;
        }

    }

}

