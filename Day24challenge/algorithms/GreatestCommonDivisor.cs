namespace Day24challenge.algorithms
{
    internal class GreatestCommonDivisor
    {
        internal static int EuclidianAlgorithm(int a, int b)
        {
            while (b != 0)
            {
                int t = b;
                b = a % b;
                a = t;
            }
            return a;
        }
    }
}
