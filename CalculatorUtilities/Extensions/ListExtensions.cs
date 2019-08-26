using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorUtilities
{
    public static class ListExtensions
    {
        public static Double Multiply(this List<Double> inputList)
        {
            Double result = inputList.First();

            for(int index = 1; index < inputList.Count(); index++)
            {
                result = result * inputList[index];
            }

            return result;
        }

        public static Double Subtract(this List<Double> inputList)
        {
            Double result = inputList.First();

            for (int index = 1; index < inputList.Count(); index++)
            {
                result = result - inputList[index];
            }

            return result;
        }

        public static Double Divide(this List<Double> inputList)
        {
            Double result = inputList.First();

            for (int index = 1; index < inputList.Count(); index++)
            {
                var indexVal = inputList[index];

                if(indexVal == 0)
                {
                    Console.WriteLine("!!! Error: Cannot divide by zero. Returning Zero.");

                    return 0;
                }

                result = result / inputList[index];
            }

            return result;
        }
    }
}
