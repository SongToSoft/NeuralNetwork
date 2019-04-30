using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            //CNeuralNetwork neuralNetwork = new CNeuralNetwork("XOR", 2, 5, 1);
            //for (int i = 0; i < 10000; ++i)
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

            //Eat many memory, i dont try run this
            CNeuralNetwork neuralNetwork = new CNeuralNetwork("CyrilicRecognition", 21025, 30000, 32);
            CImageInterpreter imageInterpreter = new CImageInterpreter();
            for (int i = 0; i < imageInterpreter.GetLetterCount(); ++i)
            {
                for (int j = 0; j < imageInterpreter.GetImageCount(i); ++j)
                {
                    neuralNetwork.Run(imageInterpreter.GetImageValue(i, j), imageInterpreter.GetAnswer(i));
                }
            }
            neuralNetwork.Save();
            Console.WriteLine("End of Run");
            Console.ReadKey();
        }
    }
}
