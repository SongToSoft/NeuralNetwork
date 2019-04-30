using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class CImageInterpreter
    {
        private List<Bitmap>[] m_imageLists;

        public CImageInterpreter()
        {
            int index = 0;
            m_imageLists = new List<Bitmap>[32];
            DirectoryInfo letterDir = new DirectoryInfo(@"C:\Users\dbogdano\source\repos\NeuralNetwork\NeuralNetwork\Cyrillic");
            foreach (var letter in letterDir.GetDirectories())
            {
                //Console.WriteLine("!!!!!!!!!!!!");
                //Console.WriteLine(letter.FullName);
                m_imageLists[index] = new List<Bitmap>();
                foreach (var image in letter.GetFiles())
                {
                    if (image.FullName.EndsWith(".png"))
                    {
                        Bitmap tmpImage = new Bitmap(image.FullName);
                        m_imageLists[index].Add(tmpImage);
                    }
                    //Console.WriteLine(image.FullName);
                }
            }
            Console.WriteLine("End of ImageInterpreter initialize");
        }

        public List<double> GetImageValue(int iIndex, int jIndex)
        {
            List<double> value = new List<double>();
            Bitmap bitmap = m_imageLists[iIndex][jIndex];
            for (int i = 0; i < bitmap.Width; ++i)
            {
                for (int j = 0; j < bitmap.Height; ++j)
                {
                    double pixel = bitmap.GetPixel(i, j).R;
                    value.Add(pixel);
                }
            }
            return value;
        }

        public int GetLetterCount()
        {
            return m_imageLists.Count();
        }

        public int GetImageCount(int index)
        {
            return m_imageLists[index].Count();
        }

        public List<double> GetAnswer(int letterNumber)
        {
            List<double> answer = new List<double>();
            for (int i = 0; i < m_imageLists.Count(); ++i)
            {
                if (i != letterNumber)
                {
                    answer.Add(0);
                }
                else
                {
                    answer.Add(1);
                }
            }
            return answer;
        }
    }
}
