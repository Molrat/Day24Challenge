namespace Day24challenge.algorithms
{
    internal class LeastCommonMultiple
    {
        internal static int ComputeLeastCommonMultiple(int a, int b)
        {
            int greatestCommonDivisor = GreatestCommonDivisor.EuclidianAlgorithm(a, b);
            return (a / greatestCommonDivisor) * b;
        }
    }
}
