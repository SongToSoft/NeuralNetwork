using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace NeuralNetwork
{
    class CImageInterpreter
    {
        private List<Bitmap>[] m_imageLists;
        private char[] cyrilicLetter = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ж', 'З', 'И', 'Й',
                                         'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У',
                                         'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Э', 'Ю', 'Я',};
        public CImageInterpreter()
        {
            int index = 0;
            m_imageLists = new List<Bitmap>[29];
            DirectoryInfo letterDir = new DirectoryInfo(@"C:\Users\dbogdano\source\repos\NeuralNetwork\NeuralNetwork\Cyrillic");
            foreach (var letter in letterDir.GetDirectories())
            {
                m_imageLists[index] = new List<Bitmap>();
                foreach (var image in letter.GetFiles())
                {
                    if (image.FullName.EndsWith(".png"))
                    {
                        Bitmap tmpImage = new Bitmap(image.FullName);
                        tmpImage = new Bitmap(tmpImage, new Size(10, 10));
                        m_imageLists[index].Add(tmpImage);
                    }
                }
                ++index;
            }
            Console.WriteLine("End of ImageInterpreter initialize");
        }

        public List<double> GetImageListValues(int iIndex, int jIndex)
        {
            List<double> value = new List<double>();
            Bitmap bitmap = m_imageLists[iIndex][jIndex];
            for (int i = 0; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width; ++j)
                {
                    double pixel = bitmap.GetPixel(i, j).R;
                    value.Add(pixel);
                }
            }
            return value;
        }
    
        public double[,] GetImageArrayValues(int iIndex, int jIndex)
        {
            Bitmap bitmap = m_imageLists[iIndex][jIndex];
            double[,] value = new double[bitmap.Height, bitmap.Width];
            for (int i = 0; i < bitmap.Height; ++i)
            {
                for (int j = 0; j < bitmap.Width; ++j)
                {
                    value[i, j] = bitmap.GetPixel(i, j).R;
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
            if (index < GetLetterCount())
            {
                return m_imageLists[index].Count();
            }
            else
            {
                return 0;
            }
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

        public char GetCyrilic(List<double> values)
        {
            double maxValue = double.MinValue;
            int maxIndex = 0;
            for (int i = 0; i < values.Count; ++i)
            {
                if (maxValue < values[i])
                {
                    maxValue = values[i];
                    maxIndex = i;
                }
            }
            return cyrilicLetter[maxIndex];
        }
    }
}
