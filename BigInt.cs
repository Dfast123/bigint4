﻿using System;
using System.Text;

namespace BigInt4
{
    class BigInt
    {
        private bool isNegative;
        public bool IsNegative
        {
            get { return isNegative; }
            set { isNegative = value; }
        }

        private string number;
        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public BigInt(string number, bool isNegative)
        {
            Number = number;
            IsNegative = isNegative;
        }

        public static BigInt Sum(BigInt number1, BigInt number2)
        {
            bool ResIsNegative = number1.IsNegative;

            if (number1.isNegative && !number2.IsNegative)//-+
            {
                return Substract(number2, new BigInt(number1.Number, !number1.IsNegative));
            }

            if (!number1.isNegative && number2.IsNegative)//+-
            {
                return Substract(number1, new BigInt(number2.number, !number2.IsNegative));
            }

            int length = number1.number.Length;
            if (number1.number.Length < number2.number.Length)
            {
                length = number2.number.Length;
            }

            StringBuilder sb = new StringBuilder();

            string ResNumber;
            int curr = 0;
            int NaUm = 0;
            int curr1 = 0;
            int curr2 = 0;

            StringBuilder sbNum1 = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                try
                {
                    sbNum1.Append(number1.number[number1.number.Length - i - 1]);
                }
                catch
                {
                    sbNum1.Append("0");
                }
            }

            StringBuilder sbNum2 = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                try
                {
                    sbNum2.Append(number2.number[number2.number.Length - i - 1]);
                }
                catch
                {
                    sbNum2.Append("0");
                }
            }

            for (int i = 0; i < length; i++)
            {
                try
                {
                    curr1 = int.Parse(sbNum1[i].ToString());
                }
                catch
                {
                    curr1 = 0;
                }
                try
                {
                    curr2 = int.Parse(sbNum2[i].ToString());
                }
                catch
                {
                    curr2 = 0;
                }

                curr = (NaUm + curr1 + curr2);
                NaUm = curr / 10;
                curr %= 10;
                sb.Append(curr.ToString());
            }
            if (NaUm != 0)
                sb.Append(NaUm);

            StringBuilder sbReverse = new StringBuilder();
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                sbReverse.Append(sb[i]);
            }
            ResNumber = sbReverse.ToString();

            return new BigInt(ResNumber, ResIsNegative);
        }

        public static BigInt Substract(BigInt number1, BigInt number2)
        {
            bool ResIsNegative = number1.IsNegative;
            if (number1.isNegative && !number2.IsNegative)//-+
            {
                return Sum(number1, new BigInt(number2.number, !number2.IsNegative));
            }

            if (!number1.isNegative && number2.IsNegative)//+-
            {
                return Sum(number1, new BigInt(number2.number, !number2.IsNegative));
            }

            int length = number1.number.Length;
            if (number1.number.Length < number2.number.Length)
            {
                length = number2.number.Length;
            }

            StringBuilder sbNum1 = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                try
                {
                    sbNum1.Append(number1.number[number1.number.Length - i - 1]);
                }
                catch
                {
                    sbNum1.Append("0");
                }
            }

            StringBuilder sbNum2 = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                try
                {
                    sbNum2.Append(number2.number[number2.number.Length - i - 1]);
                }
                catch
                {
                    sbNum2.Append("0");
                }
            }

            if (ReturnBigger(number1, number2) == number2)
            {
                StringBuilder sbSwap = sbNum1;
                sbNum1 = sbNum2;
                sbNum2 = sbSwap;
                //ako 2roto e po golqmo, se obrushta znaka!!!
                ResIsNegative = !ResIsNegative;
            }

            string ResNumber;
            int curr1;
            int curr2;
            int curr;
            int NaUm = 0;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                try
                {
                    curr1 = int.Parse(sbNum1[i].ToString());
                }
                catch
                {
                    curr1 = 0;
                }
                try
                {
                    curr2 = int.Parse(sbNum2[i].ToString());
                }
                catch
                {
                    curr2 = 0;
                }

                curr = (curr1 - curr2 - NaUm);
                NaUm = 0;
                if (curr < 0)
                {
                    curr += 10;
                    NaUm = 1;
                }
                sb.Append(curr.ToString());
            }
            StringBuilder sbReverse = new StringBuilder();
            for (int i = sb.Length - 1; i >= 0; i--)
            {
                sbReverse.Append(sb[i]);
            }
            ResNumber = sbReverse.ToString();

            return new BigInt(ResNumber, ResIsNegative);
        }      

        public static BigInt Multiply(BigInt number1, BigInt number2)
        {
            BigInt result = new BigInt("0", false);

            int length = number1.number.Length;
            if (number1.number.Length < number2.number.Length)
            {
                length = number2.number.Length;
            }

            StringBuilder sbNum1 = new StringBuilder();
            for (int i = 0; i < length - number1.Number.Length; i++)
            {
                sbNum1.Append("0");
            }
            sbNum1.Append(number1.number);

            StringBuilder sbNum2 = new StringBuilder();
            for (int i = 0; i < length - number2.Number.Length; i++)
            {
                sbNum2.Append("0");
            }
            sbNum2.Append(number2.number);

            for (int i = 0; i < length; i++)
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                int a = int.Parse(sbNum1[length - 1 - i].ToString());
                sb.Append(sbNum2.ToString());

                for (int j = 0; j < i; j++)
                    sb.Append("0");

                for (int k = 0; k < a; k++)
                {
                    result = Sum(result, new BigInt(sb.ToString(), false));
                }
            }

            if (number1.IsNegative != number2.IsNegative)
            {
                result.IsNegative = true;
            }
            result = BigInt.TrimZeros(result);
            return result;
        }

        public static BigInt Divide(BigInt number1, BigInt number2)
        {
            BigInt result = new BigInt("0", false);
            if (number2.Number.Length > number1.Number.Length)
                return result;
            if (String.IsNullOrEmpty(number2.Number))
                return result;

            bool resultIsNegative = false;

            if (number1.IsNegative != number2.IsNegative)
            {
                resultIsNegative = true;
            }
            number1.IsNegative = false;
            number2.IsNegative = false;

            BigInt number2pos = new BigInt(number2.Number, false);
            int length = 1 + number1.number.Length - number2.Number.Length;           

            BigInt num2Powered;
            for (int i = 0; i < length; i++)
            {
                int n = 0;
                while (ReturnBigger(number1, Multiply(number2, new BigInt(Math.Pow(10, n+1).ToString(), false))).Equals(number1))
                {
                    n++;
                }

                num2Powered = Multiply(number2, new BigInt(Math.Pow(10, n).ToString(), false));
                while (ReturnBigger(number1, num2Powered).Equals(number1))
                {
                    result = Sum(result, new BigInt(Math.Pow(10, n).ToString(), false));
                    number1 = Substract(number1, num2Powered);
                }                              
            }
            result.IsNegative = resultIsNegative;
            return result;
        }

        public static BigInt ModularDivision(BigInt number1, BigInt number2)
        {
            BigInt temp1 = Divide(number1, number2);
            BigInt temp2 = Multiply(number2, temp1);
            BigInt temp3 = Substract(number1, temp2);
            return temp3;
        }

        public static BigInt ReturnBigger(BigInt number1, BigInt number2)
        {
            number1 = TrimZeros(number1);
            number2 = TrimZeros(number2);

            if (number1.number.Length < number2.number.Length)
            {
                return number2;
            }
            else if (number1.number.Length > number2.number.Length)
            {
                return number1;
            }
            else
            {
                for (int i = 0; i < number1.number.Length; i++)
                {
                    if (number1.number[i] > number2.number[i])
                    {
                        return number1;
                    }
                    else if (number1.number[i] < number2.number[i])
                    {
                        return number2;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return number1;
        }

        public static BigInt TrimZeros(BigInt number)
        {
            number.Number = number.Number.TrimStart('0');
            return number;
        }
    }
}
