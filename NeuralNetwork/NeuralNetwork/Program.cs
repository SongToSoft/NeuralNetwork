using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryOfEverything.LinearAlgebra;

namespace NeuralNetwork
{
    class Program
    {
        private static Random random = new Random();
        static void Main(string[] args)
        {
            //CNeuralNetwork neuralNetwork = new CNeuralNetwork();
            //neuralNetwork.Create("XOR", 2, 5, 1);
            //for (int i = 0; i < 100000; ++i)
            //{
            //    for (int j = 0; j <= 1; ++j)
            //    {
            //        for (int k = 0; k <= 1; ++k)
            //        {
            //            List<double> inputValue = new List<double> { j, k };
            //            List<double> answer = new List<double> { j ^ k };
            //            Console.WriteLine(answer[0] + " -- " + neuralNetwork.Run(inputValue, answer)[0]);
            //        }
            //    }
            //}
            //neuralNetwork.Save();


            //I think it's dont work.
            CСonvolutionalNeuralNetwork convolutionalNeuralNetwork = new CСonvolutionalNeuralNetwork();
            convolutionalNeuralNetwork.Create("CyrilicRecognition", 100, 1000, 29);
            CImageInterpreter imageInterpreter = new CImageInterpreter();
            for (int k = 0; k < 10000; ++k)
            {
                int iIndex = random.Next(0, imageInterpreter.GetLetterCount() - 1);
                int jIndex = random.Next(0, imageInterpreter.GetImageCount(iIndex) - 1);

                Matrix<double> matrix = new Matrix<double>(imageInterpreter.GetImageArrayValues(iIndex, jIndex));
                var result = convolutionalNeuralNetwork.Run(matrix.ToList(), imageInterpreter.GetAnswer(iIndex));

                Console.WriteLine("Answer: " + imageInterpreter.GetCyrilic(imageInterpreter.GetAnswer(iIndex)) + "; Result: " + imageInterpreter.GetCyrilic(result));
                convolutionalNeuralNetwork.Save();
                Console.WriteLine("End of try " + k);
            }
            Console.WriteLine("End of Run");
            Console.ReadKey();
        }
    }
}
