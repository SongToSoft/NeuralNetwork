using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace NeuralNetwork.Neurons
{
    class CNeuronWithSynapse : CNeuron
    {
        private List<double> m_Synapse;
        private List<double> m_wLast = new List<double>();

        public CNeuronWithSynapse(double value, List<double> synapse) : base(value)
        {
            SetSynapse(synapse);
            for (int i = 0; i < synapse.Count; ++i)
            {
                m_wLast.Add(0);
            }
        }

        public List<double> GetSynapses()
        {
            return m_Synapse;
        }
   
        public double GetCertainSynapse(int index)
        {
            return m_Synapse[index];
        }

        public void SetSynapse(List<double> synapse)
        {
            m_Synapse = synapse;
        }

        public void SetСertainOutSynape(int index, double synapse)
        {
            m_Synapse[index] = synapse;
        }

        public double GetLastDelta(int index)
        {
            return m_wLast[index];
        }

        public void SetCertainLastDelta(int index, double delta)
        {
            m_wLast[index] = delta;
        }

        public new void Print()
        {
            Console.Write("Value: " + m_Value + " ");
            for (int i = 0; i < m_Synapse.Count; ++i)
            {
                Console.Write("Synapse: " + i + " -- " + m_Synapse[i] + " ");
            }
            Console.WriteLine();
        }
    }
}