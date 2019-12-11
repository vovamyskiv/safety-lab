using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD_Lab2.Hash
{
    public class MD5
    {
        public MD5()
        {

        }

        public byte[] GetHash(string _message)
        {
            var Message = new Message(_message);

            var buffer = new Buffer();

            foreach(var block in Message)
            {
                buffer = _Hash512Bit(block, buffer);
            }

            return buffer.GetBuffer();
        }

        private Buffer _Hash512Bit(uint[] block, Buffer buffer)
        {
            var loopHelper = new LoopHelper();

            var basicBuffer = (Buffer)buffer.Clone();

            for (int loop = 0; loop < 4; ++loop)
            {
                for (int round = 0; round < 16; ++round)
                {
                    var funcResult = loopHelper.Func(BytesToUInt(buffer.B), BytesToUInt(buffer.C), BytesToUInt(buffer.D));

                    var a = BytesToUInt(buffer.A);

                    var b = BytesToUInt(buffer.B);

                    var resultA = b + Helper.BitLoopLeft(a + funcResult + block[loopHelper.GetWordIndex(round)] + Helper.GetT(), 
                        loopHelper.GetLoopStep());

                    buffer.A = UIntToBytes(resultA);

                    buffer.Round();
                }

                if(loop != 3)
                    loopHelper.NextLoop();
            }
            
            return basicBuffer + buffer;
        }

        public static uint BytesToUInt(byte[] array)
        {
            return BitConverter.ToUInt32(array, 0);
        }

        public static byte[] UIntToBytes(uint num)
        {
            return BitConverter.GetBytes(num);
        }
    }
}
