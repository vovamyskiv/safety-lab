using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SD_Lab2.Hash;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var hash = new MD5();

            var result = hash.GetHash("12345678901234567890123456789012345678901234567890123456789012345678901234567890");

            Console.WriteLine(BitConverter.ToString(result).Replace("-", ""));

            //var bytes = Encoding.ASCII.GetBytes("a");

            //Console.WriteLine(BitConverter.ToString(bytes).Replace("-", ""));

            Console.ReadKey();
        }
    }

    //var result = ulong.Parse("FFFFFFFFFFFFFFFF", System.Globalization.NumberStyles.HexNumber);

    //Console.WriteLine(result);

    //result *= 2;

    //var array = BitConverter.GetBytes(result);

    //Console.WriteLine(String.Join(" ", array));

    //result = BitConverter.ToUInt32(array, 0);

    //Console.WriteLine(result);
}
