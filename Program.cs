using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigInt4
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("vuvedi chislo 1");
            string input1 = Console.ReadLine();
            BigInt number1 = Input(input1);            

            Console.WriteLine("vuvedi chislo 2");
            string input2 = Console.ReadLine();
            BigInt number2 = Input(input2);

            Console.WriteLine();
            Console.WriteLine();
            Describe(number1);
            Describe(number2);
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("result Plus :");
            Stopwatch sum = new Stopwatch();
            sum.Start();
            BigInt result = BigInt.Sum(number1, number2);
            sum.Stop();
            Describe(result);
            Console.WriteLine("Plus Done for "+sum.ElapsedMilliseconds+"ms.");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("result Minus :");
            Stopwatch substr = new Stopwatch();
            substr.Start();
            BigInt result2 = BigInt.Substract(number1, number2);
            substr.Stop();
            Describe(result2);
            Console.WriteLine("Minus Done for " + substr.ElapsedMilliseconds + "ms.");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("result Multiplication :");
            Stopwatch mult = new Stopwatch();
            mult.Start();
            BigInt result3 = BigInt.Multiply(number1, number2);
            mult.Stop();
            Describe(result3);
            Console.WriteLine("Multiplication Done for " + mult.ElapsedMilliseconds + "ms.");
            Console.WriteLine();
            Console.WriteLine();

          /*  Console.WriteLine("result Division :");
            BigInt result5 = BigInt.Divide(number1, number2);
            Describe(result5);
            Console.WriteLine();
            Console.WriteLine();
            */ 

            Console.WriteLine("sum of result + and result -");
            BigInt result4 = BigInt.Sum(result, result2);
            Describe(result4);
        }

        static BigInt Input(string input)
        {
            input = input.TrimStart('0');
            BigInt number = new BigInt(input, false);
            try
            {
                if (input[0] == '-')
                {
                    input = input.TrimStart('-');
                    input = input.TrimStart('0');
                    number = new BigInt(input, true);
                }
                return number;
            }
            catch
            {
                return number;
            }
        }

        static void Describe(BigInt number)
        {
            string descr = number.Number.TrimStart('0');
            if (number.IsNegative)
            {
                Console.Write("-");
            }
            Console.WriteLine(descr);
        }


    }
}
