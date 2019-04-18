using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class CNeuron
    {
        protected double m_Value = 0;

        public CNeuron(double value)
        {
            m_Value = value;
        }

        public double GetValue()
        {
            return m_Value;
        }

        public void SetValue(double value)
        {
            m_Value = value;
        }

        public void Print()
        {
            Console.WriteLine( "Value: " + m_Value);
        }

    }
}
