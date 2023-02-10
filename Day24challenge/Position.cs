namespace Day24challenge
{
    internal class Position
    {
        internal int X { get; set; }
        internal int Y { get; set; }

        internal int Z { get; set; }
        internal Position(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        internal int ManhattanDistanceToOtherPosition(Position other)
        {
            return Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
        }
    }
}
