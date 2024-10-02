using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Interfaces;

namespace Logic.Domain
{
    public class Calculator : ICalculator
    {
        private double _memory;
        private bool _memoryIsClear;

        public Calculator()
        {
            _memoryIsClear = true;
        }

        public double Percent(double a, double b)
        {
            return a / 100 * b;        
        }

        public double Multiplication(double a, double b)
        {
            return a * b;
        }

        public double Division(double a, double b)
        {
            return a / b;
        }

        public double Addition(double a, double b)
        {
            return a + b;
        }

        public double Subtraction(double a, double b)
        {
            return a - b;
        }

        public double Sqrt(double b)
        {
            if (b < 0)
			{
                throw new Exception("Invalid input");
			}

            return Math.Sqrt(b);
        }

        public double Degree2(double b)
        {
            return Math.Pow(b, 2);
        }

        public double OneDivX(double b)
        {
            return 1.0 / b;
        }

        public void ClearMemory()
        {
            _memory = 0;
            _memoryIsClear = true;
        }

        public double GetMemory()
        {
            if (_memoryIsClear)
            {
                throw new Exception("Memory is clear.");
            }

            return _memory;
        }

        public void MAddition(double a)
        {
            _memory += a;
            _memoryIsClear = false;
        }

        public void MSubtraction(double a)
        {
            _memoryIsClear = false;
            _memory -= a;
        }


    }
}
