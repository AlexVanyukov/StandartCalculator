using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    interface ICalculator
    {
        double Percent(double a, double b);

        double Multiplication(double a, double b);

        double Division(double a, double b);

        double Addition(double a, double b);

        double Subtraction(double a, double b);

        double Degree2(double a);

        double Sqrt(double a);

        void ClearMemory();

        double GetMemory();
    }
}
