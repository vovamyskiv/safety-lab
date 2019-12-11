using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD_Lab2.Hash
{
    class Message:IEnumerable<uint[]>
    {
        private uint[] _Words;

        private static readonly int _Mod = 448;

        public Message(string message)
        {
            var byteMessage = Encoding.ASCII.GetBytes(message);

            ulong bitLength = (ulong)byteMessage.Length * 8;

            int mod = (int)(bitLength % 512);

            int additiveLength = 0;

            if(mod < _Mod)
            {
                additiveLength = _Mod - mod; 
            }
            else
            {
                additiveLength = 512 - (mod - _Mod);
            }

            var additiveBytes = new byte[additiveLength / 8];

            additiveBytes[0] = BitConverter.GetBytes((uint)128).FirstOrDefault();

            _CreateWords(bitLength, byteMessage, additiveBytes);
        }

        private void _CreateWords(ulong bitLength, byte[] byteMessage, byte[] additiveBytes)
        {
            var lengthBytes = BitConverter.GetBytes(bitLength);
            
            byte[] buffer = new byte[4];

            _Words = new uint[(byteMessage.Length + additiveBytes.Length + lengthBytes.Length) / 4];

            var wholeMessageList = new List<byte>();

            wholeMessageList.AddRange(byteMessage);
            wholeMessageList.AddRange(additiveBytes);
            wholeMessageList.AddRange(lengthBytes);

            var wholeMessage = wholeMessageList.ToArray();

            for (int i = 0; i < wholeMessage.Length / 4; ++i)
            {
                Array.Copy(wholeMessage, i * 4, buffer, 0, buffer.Length);

                _Words[i] = BitConverter.ToUInt32(buffer, 0);
            }
        }

        public IEnumerator<uint[]> GetEnumerator()
        {
            for(int i = 0; i < _Words.Length / 16; ++i)
            {
                var result = new uint[16];
                Array.Copy(_Words, (i * 16), result, 0, result.Length);
                yield return result;
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
