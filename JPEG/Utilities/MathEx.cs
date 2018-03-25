using System;
using System.Linq;

namespace JPEG.Utilities
{
    public static class MathEx
    {
        public static double Sum(int from, int to, Func<int, double> function)
        {
            return Enumerable.Range(from, to).Sum(function);
        }

        //public static double SumByTwoVariables(int from1, int to1, int from2, int to2, Func<int, int, double> function)
        //{
        //    return Sum(from1, to1, x => Sum(from2, to2, y => function(x, y)));
        //}

        public static double SumByTwoVariables(int from1, int to1, int from2, int to2, Func<int, int, double> function)
        {
            double result = 0;
            for (var x = from1; x < to1; x++)
                for (var y = from2; y < to2; y++)
                {
                    result += function(x, y);
                }

            return result;
        }
    }
}