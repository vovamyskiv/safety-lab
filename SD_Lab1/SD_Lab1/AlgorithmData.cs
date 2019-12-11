using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD_Lab1
{
    class Algorithm
    {
        public static readonly int Modul = (int)Math.Pow(2, 17) - 1;

        public static readonly int A = (int)Math.Pow(4, 3);

        public static readonly int C = 21;

        public static readonly int StartValue = 256;

        //----------------

        private static int _prevValue;

        private List<int> _result;

        static Algorithm()
        {
            _prevValue = StartValue;
        }

        public Algorithm(int amount)
        {
            _result = new List<int>(amount);
        }

        public List<int> Run()
        {
            for(int i = 0; i < _result.Capacity; ++i)
            {
                _prevValue = (A * _prevValue + C) % Modul;

                _result.Add(_prevValue);
            }
            return _result;
        }

        public List<int> Run(int min, int max)
        {
            if (max <= min) throw new Exception("max should be bigger than min");

            double realMediana = Modul / 2;
            
            int radius = (max - min) / 2;

            int changedMediana = min + radius;
            
            for (int i = 0; i < _result.Capacity; ++i)
            {
                _prevValue = (A * _prevValue + C) % Modul;

                double proportion = (_prevValue - realMediana) / realMediana;

                int correctValue = (int)(radius * proportion) + changedMediana;

                _result.Add(correctValue);
            }
            return _result;
        }

        public int FindPeriod()
        {
            var set = new HashSet<int>() { StartValue };

            int count = set.Count;

            int value = StartValue;

            for (int i = 0; i < Modul; ++i)
            {
                value = (A * value + C) % Modul;

                set.Add(value);

                if(set.Count == count)
                {
                    ++count;
                    break;
                }

                ++count;
            }

            return count;
        }
    }
}
