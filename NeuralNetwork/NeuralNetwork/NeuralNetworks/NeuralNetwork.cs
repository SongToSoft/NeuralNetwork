using NeuralNetwork.Neurons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class CNeuralNetwork
    {
        private string m_name;
        private List<CNeuronWithSynapse> m_InputNeurons = new List<CNeuronWithSynapse>();
        private List<CNeuronWithSynapse> m_HiddenNeurons = new List<CNeuronWithSynapse>();
        private List<CNeuron> m_OutputNeurons = new List<CNeuron>();

        private JsonCommander jsonCommander;
        private double epsilon = 0.7;
        private double alpha = 0.3;

        private Random random = new Random();
        
        public CNeuralNetwork(string name, int inputCount, int hiddenCount, int outputCount)
        {
            jsonCommander = new JsonCommander();
            m_name = name;
            for (int i = 0; i < inputCount; ++i)
            {
                CNeuronWithSynapse inputNeuron = new CNeuronWithSynapse(0, CreateRandomList(hiddenCount));
                m_InputNeurons.Add(inputNeuron);
            }
            for (int i = 0; i < hiddenCount; ++i)
            {
                CNeuronWithSynapse hiddenNeuron = new CNeuronWithSynapse(0, CreateRandomList(outputCount));
                m_HiddenNeurons.Add(hiddenNeuron);
            }
            for (int i = 0; i < outputCount; ++i)
            {
                CNeuron outputNeuron = new CNeuron(0);
                m_OutputNeurons.Add(outputNeuron);
            }
            if (File.Exists(m_name + ".json"))
            {
                JsonData[] synapses = jsonCommander.DeSerialize(m_name);
                for (int i = 0; i < m_InputNeurons.Count; ++i)
                {
                    for (int j = 0; j < m_HiddenNeurons.Count; ++j)
                    {
                        m_InputNeurons[i].SetСertainOutSynape(j, synapses[0].m_values[i][j]);
                    }
                }
                for (int i = 0; i < m_HiddenNeurons.Count; ++i)
                {
                    for (int j = 0; j < m_OutputNeurons.Count; ++j)
                    {
                        m_HiddenNeurons[i].SetСertainOutSynape(j, synapses[1].m_values[i][j]);
                    }
                }
            }
            Print("After Create");
        }

        private List<double>[] GetInputSynapses()
        {
            List<double>[] synapses = new List<double>[m_InputNeurons.Count];
            for (int i = 0; i < m_InputNeurons.Count; ++i)
            {
                synapses[i] = new List<double>();
                for (int j = 0; j < m_HiddenNeurons.Count; ++j)
                {
                    synapses[i].Add(m_InputNeurons[i].GetCertainSynapse(j));
                }
            }
            return synapses;
        }

        private List<double>[] GetHiddenSynapses()
        {
            List<double>[] synapses = new List<double>[m_HiddenNeurons.Count];
            for (int i = 0; i < m_HiddenNeurons.Count; ++i)
            {
                synapses[i] = new List<double>();
                for (int j = 0; j < m_OutputNeurons.Count; ++j)
                {
                    synapses[i].Add(m_HiddenNeurons[i].GetCertainSynapse(j));
                }
            }
            return synapses;
        }

        private double Sigmoid(double value)
        {
            return (1 / (1 + Math.Exp(-value)));
        }

        private double DerivativeSigmoid(double value)
        {
            return ((1 - value) * value);
        }
        
        private double DeltaOut(double outIdeal, double outActual)
        {
            return ((outIdeal - outActual) * DerivativeSigmoid(outActual));
        }
        
        private double DeltaHidden(CNeuronWithSynapse neuron, double delta)
        {
            double deltaHidden = DerivativeSigmoid(neuron.GetValue());
            double sum = 0;
            for (int i = 0; i < neuron.GetSynapses().Count; ++i)
            {
                sum += (neuron.GetCertainSynapse(i) * delta);
            }
            deltaHidden *= sum;
            return deltaHidden;
        }

        private List<double> CreateRandomList(int size)
        {
            List<double> randomList = new List<double>();
            for (int i = 0; i < size; ++i)
            {
                randomList.Add(random.NextDouble());
            }
            return randomList;
        }

        public void Print(string description = "")
        {
            Console.WriteLine("------------------");
            Console.WriteLine(description);
            Console.WriteLine("Input Neurons");
            for (int i = 0; i < m_InputNeurons.Count; ++i)
            {
                m_InputNeurons[i].Print();
            }
            Console.WriteLine("Hidden Neurons");
            for (int i = 0; i < m_HiddenNeurons.Count; ++i)
            {
                m_HiddenNeurons[i].Print();
            }
            Console.WriteLine("Output Neurons");
            for (int i = 0; i < m_OutputNeurons.Count; ++i)
            {
                m_OutputNeurons[i].Print();
            }
            Console.WriteLine("------------------");
        }

        public double Error(List<double> result, List<double> answer)
        {
            double error = 0;
            for (int i = 0; i < result.Count; ++i)
            {
                error = (result[i] - answer[i]) * (result[i] - answer[i]);
            }
            error /= result.Count;
            //Console.WriteLine("Error: " + error);
            return error;
        }

        public List<double> Run(List<double> inputValue, List<double> answer)
        {
            List<double> result = new List<double>();
            for (int i = 0; i < inputValue.Count; ++i)
            {
                m_InputNeurons[i].SetValue(Sigmoid(inputValue[i]));
                //m_InputNeurons[i].SetValue((inputValue[i]));
            }

            for (int i = 0; i < m_HiddenNeurons.Count; ++i)
            {
                double hiddenValue = 0;
                for (int j = 0; j < m_InputNeurons.Count; ++j)
                {
                    hiddenValue += (m_InputNeurons[j].GetValue() * m_InputNeurons[j].GetCertainSynapse(i));
                }
                m_HiddenNeurons[i].SetValue(Sigmoid(hiddenValue));
                //m_HiddenNeurons[i].SetValue(hiddenValue);
            }

            for (int i = 0; i < m_OutputNeurons.Count; ++i)
            {
                double outputValue = 0;
                for (int j = 0; j < m_HiddenNeurons.Count; ++j)
                {
                    outputValue += (m_HiddenNeurons[j].GetValue() * m_HiddenNeurons[j].GetCertainSynapse(i));
                }
                m_OutputNeurons[i].SetValue(Sigmoid(outputValue));
                //m_OutputNeurons[i].SetValue((outputValue));

            }

            for (int i = 0; i < m_OutputNeurons.Count; ++i)
            {
                result.Add(m_OutputNeurons[i].GetValue());
            }
            double error = Error(result, answer);
            //Print("Before Train");
            Train(answer, error);
            //Print("After Train");
            return result;
        }

        public void Train(List<double> answer, double error)
        {
            for (int i = 0; i < m_OutputNeurons.Count; ++i)
            {
                double deltaOut = DeltaOut(answer[i], m_OutputNeurons[i].GetValue());

                for (int j = 0; j < m_HiddenNeurons.Count; ++j)
                {
                    double deltaHidden = DeltaHidden(m_HiddenNeurons[j], deltaOut);

                    double gradWHidden = m_HiddenNeurons[j].GetValue() * deltaOut;
                    double differenceHidden = epsilon * gradWHidden + alpha * m_HiddenNeurons[j].GetLastDelta(i);

                    m_HiddenNeurons[j].SetСertainOutSynape(i, m_HiddenNeurons[j].GetCertainSynapse(i) + differenceHidden);
                    m_HiddenNeurons[j].SetCertainLastDelta(i, differenceHidden);
                    for (int k = 0; k < m_InputNeurons.Count; ++k)
                    {
                        double gradWInput = m_InputNeurons[k].GetValue() * deltaHidden;
                        double differenceInput = epsilon * gradWInput + alpha * m_InputNeurons[k].GetLastDelta(j);

                        m_InputNeurons[k].SetСertainOutSynape(j, m_InputNeurons[k].GetCertainSynapse(j) + differenceInput);
                        m_InputNeurons[k].SetCertainLastDelta(j, differenceInput);
                    }
                }
            }
        }

        public void Save()
        {
            var inputSynapses = GetInputSynapses();
            var hiddenSynapses = GetHiddenSynapses();
            jsonCommander.Serialize(m_name, inputSynapses, hiddenSynapses);
        }

    }
}
