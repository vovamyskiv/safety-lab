using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD_Lab2.Hash
{
    class LoopHelper
    {
        private int _loopId = 0;
        
        public LoopHelper()
        {
            NextLoop();
        }



        public void NextLoop()
        {
            _loopId++;
            if (_loopId > 4)
                throw new Exception("Too much loops");
        }

        public uint Func(uint B, uint C, uint D)
        {
            uint result = 0;
            switch (_loopId)
            {
                case 1: result = _FuncF(B, C, D);
                    break;

                case 2: result = _FuncG(B, C, D);
                    break;

                case 3: result = _FuncH(B, C, D);
                    break;

                case 4: result = _FuncL(B, C, D);
                    break;

                default: throw new Exception("Too much loops");
            }
            return result;
        }

        public int GetWordIndex(int index)
        {
            int result = 0;
            switch (_loopId)
            {
                case 1:
                    result = _WordIndexF(index);
                    break;

                case 2:
                    result = _WordIndexG(index);
                    break;

                case 3:
                    result = _WordIndexH(index);
                    break;

                case 4:
                    result = _WordIndexL(index);
                    break;

                default: throw new Exception("Too much loops");
            }
            return result;
        }

        private int _loopStepId = 0;

        private static Dictionary<int, int[]> _LoopStepDictionary = new Dictionary<int, int[]>()
        {
            { 1, new int[]{7, 12, 17, 22} },
            { 2, new int[]{5, 9, 14, 20} },
            { 3, new int[]{4, 11, 16, 23} },
            { 4, new int[]{6, 10, 15, 21} },
        };

        public int GetLoopStep()
        {
            var result = _LoopStepDictionary[_loopId][_loopStepId];

            ++_loopStepId;

            if(_loopStepId == 4)
            {
                _loopStepId = 0;
            }

            return result;
        }

        //------------------------------------------

        #region Funcs_Words

        private uint _FuncF(uint B, uint C, uint D)
        {
            return (B & C) | ((~B) & D);
        }

        private uint _FuncG(uint B, uint C, uint D)
        {
            return (B & D) | (C & (~D));
        }

        private uint _FuncH(uint B, uint C, uint D)
        {
            return B ^ C ^ D;
        }

        private uint _FuncL(uint B, uint C, uint D)
        {
            return C ^ (B | (~D));
        }

        private int _WordIndexF(int index)
        {
            return index;
        }

        private int _WordIndexG(int index)
        {
            return (1 + (5 * index)) % 16;
        }

        private int _WordIndexH(int index)
        {
            return (5 + (3 * index)) % 16;
        }

        private int _WordIndexL(int index)
        {
            return (7 * index) % 16;
        }

        #endregion
    }
}
