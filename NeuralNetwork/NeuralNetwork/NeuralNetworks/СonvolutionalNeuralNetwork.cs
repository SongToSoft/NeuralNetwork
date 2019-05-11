using LibraryOfEverything.LinearAlgebra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class CСonvolutionalNeuralNetwork : CNeuralNetwork
    {
        double[,] m_coreValues1 = new double[,] { { 1, 0, -1 },
                                                 { 2, 0, -2 },
                                                 { 1, 0, -1 } };
        double[,] m_coreValues2 = new double[,] { { 0, 1, 2 },
                                                 { 2, 2, 0 },
                                                 { 0, 1, 2 } };
        double[,] m_coreValues3 = new double[,] { { 0, 1, 0 },
                                                 { 1, -4, 1 },
                                                 { 0, 1, 0 } };
        private Matrix<double> m_core;

        public Matrix<double> Convolution(Matrix<double> imageMatrix)
        {
            Console.WriteLine("Before: " + imageMatrix.Height() + " " + imageMatrix.Width());
            m_core = new Matrix<double>(m_coreValues2);

            //Convolution
            Matrix<double> result = new Matrix<double>(imageMatrix.Height() - m_core.Height(), imageMatrix.Width() - m_core.Width());
            for (int i = 0; i < imageMatrix.Height() - m_core.Height(); ++i)
            {
                for (int j = 0; j < imageMatrix.Width() - m_core.Width(); ++j)
                {
                    Matrix<double> convolutionMatrix = new Matrix<double>(imageMatrix.GetPart(i, j, m_core.Height(), m_core.Width()));
                    convolutionMatrix *= m_core;
                    result.SetValue(i, j, convolutionMatrix.SumAllValues());
                }
            }

            //Resize
            //Matrix<double> result = new Matrix<double>(imageMatrix.Height() / 2, imageMatrix.Width() / 2);
            //for (int i = 0; i < imageMatrix.Height() / 2; i += 2)
            //{
            //    for (int j = 0; j < imageMatrix.Width() / 2; j += 2)
            //    {
            //        Matrix<double> convolutionMatrix = new Matrix<double>(imageMatrix.GetPart(i, j, 2, 2));
            //        result.SetValue(i, j, convolutionMatrix.GetMaxValue());
            //    }
            //}
            //Console.WriteLine("After: " + result.Height() + " " + result.Width() );
            return result;
        }
    }
}
