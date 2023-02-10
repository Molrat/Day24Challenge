namespace Day24challenge.algorithms
{
    internal class Node
    {
        internal Position Position { get; }
        internal int CostSoFar { get; }
        internal Node(int x, int y, int z, int costSoFar)
        {
            Position = new(x, y, z);
            CostSoFar = costSoFar;
        }
    }
}
