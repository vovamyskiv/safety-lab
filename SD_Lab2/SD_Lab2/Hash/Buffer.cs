using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SD_Lab2.Hash
{
    class Buffer:ICloneable
    {
        #region Basic

        private byte[] _A;

        private byte[] _B;

        private byte[] _C;

        private byte[] _D;

        private static int _Length = 4;

        public Buffer()
        {
            A = BitConverter.GetBytes(uint.Parse("67452301", System.Globalization.NumberStyles.HexNumber));
            B = BitConverter.GetBytes(uint.Parse("EFCDAB89", System.Globalization.NumberStyles.HexNumber));
            C = BitConverter.GetBytes(uint.Parse("98BADCFE", System.Globalization.NumberStyles.HexNumber));
            D = BitConverter.GetBytes(uint.Parse("10325476", System.Globalization.NumberStyles.HexNumber));
        }
        
        public byte[] A
        {
            get { return _A; }
            set
            {
                if (value.Length != _Length)
                    throw new Not32BitException();
                _A = value;
            }
        }

        public byte[] B
        {
            get { return _B; }
            set
            {
                if (value.Length != _Length)
                    throw new Not32BitException();
                _B = value;
            }
        }

        public byte[] C
        {
            get { return _C; }
            set
            {
                if (value.Length != _Length)
                    throw new Not32BitException();
                _C = value;
            }
        }

        public byte[] D
        {
            get { return _D; }
            set
            {
                if (value.Length != _Length)
                    throw new Not32BitException();
                _D = value;
            }
        }

        #endregion

        public void Round()
        {
            var swap = new byte[_Length];

            A.CopyTo(swap, 0);

            A = D;
            D = C;
            C = B;
            B = swap;
        }

        public byte[] GetBuffer()
        {
            var result = new List<byte>();

            result.AddRange(A);
            result.AddRange(B);
            result.AddRange(C);
            result.AddRange(D);

            return result.ToArray();
        }

        public object Clone()
        {
            var clone = new Buffer();

            A.CopyTo(clone.A, 0);
            B.CopyTo(clone.B, 0);
            C.CopyTo(clone.C, 0);
            D.CopyTo(clone.D, 0);

            return clone;
        }

        public static Buffer operator+ (Buffer left, Buffer right)
        {
            var result = new Buffer();

            result.A = BitConverter.GetBytes(BitConverter.ToUInt32(left.A, 0) + BitConverter.ToUInt32(right.A, 0));

            result.B = BitConverter.GetBytes(BitConverter.ToUInt32(left.B, 0) + BitConverter.ToUInt32(right.B, 0));

            result.C = BitConverter.GetBytes(BitConverter.ToUInt32(left.C, 0) + BitConverter.ToUInt32(right.C, 0));

            result.D = BitConverter.GetBytes(BitConverter.ToUInt32(left.D, 0) + BitConverter.ToUInt32(right.D, 0));

            return result;
        }

        public class Not32BitException : Exception
        {
            public Not32BitException() : base()
            {

            }
        }
    }
}
