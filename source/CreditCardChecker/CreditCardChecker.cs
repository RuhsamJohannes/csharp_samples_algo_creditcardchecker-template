﻿using System;
using System.Linq;

namespace CreditCardChecker
{
    public class CreditCardChecker
    {
        /// <summary>
        /// Diese Methode überprüft eine Kreditkartenummer, ob diese gültig ist.
        /// Regeln entsprechend der Angabe.
        /// </summary>
        public static bool IsCreditCardValid(string creditCardNumber)
        {
            if (creditCardNumber.Length != 16 || string.IsNullOrEmpty(creditCardNumber))
            {
                return false;
            }

            var evenSum = creditCardNumber.Where((even, i) => i % 2 == 0)
                                        .Select(ch => CalculateDigitSum(ConvertToInt(ch)*2))
                                        .Sum();

            var oddSum = creditCardNumber.Where((odd, i) => i % 2 != 0 && i != 15)
                                        .Select(ch => ConvertToInt(ch))
                                        .Sum();
            /*
            int evenSum = 0;
            int oddSum = 0;

            for (int i = 0; i < creditCardNumber.Length; i += 2)
            {
                evenSum += CalculateDigitSum(ConvertToInt(creditCardNumber[i])*2);
            }

            for (int i = 1; i < creditCardNumber.Length -1; i += 2)
            {
                oddSum += ConvertToInt(creditCardNumber[i]);
            }
            */

            return CalculateCheckDigit(oddSum, evenSum) == ConvertToInt(creditCardNumber[15]);
        }

        /// <summary>
        /// Berechnet aus der Summe der geraden Stellen (bereits verdoppelt) und
        /// der Summe der ungeraden Stellen die Checkziffer.
        /// </summary>
        private static int CalculateCheckDigit(int oddSum, int evenSum)
        {
            int result = oddSum + evenSum;

            result %= 10;

            if (result > 0)
                result = 10 - result;

            return result;
        }

        /// <summary>
        /// Berechnet die Ziffernsumme einer Zahl.
        /// </summary>
        private static int CalculateDigitSum(int number)
        {
            return (number % 10) + (number / 10);
        }

        private static int ConvertToInt(char ch)
        {
            return ch - '0';
        }
    }
}
