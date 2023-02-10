namespace Day24challenge
{
    internal class ShortestPathProblemWithStationaryObstacles
    {
        internal Position Start { get; } 
        internal Position Goal { get; }
        internal bool[,,] ObstacleField { get; set; }
        internal ShortestPathProblemWithStationaryObstacles(Position start, Position goal, int xSize, int ySize, int zSize) 
        {
            Start = start;
            Goal = goal;
            ObstacleField = new bool[xSize, ySize, zSize];
        }

        internal void PrintProblem()
        {
            for(int z = 0; z < ObstacleField.GetLength(2);z++)
            {
                Console.WriteLine("z = " + z);
                PrintOneTimeStep(z);
            }
        }

        private void PrintOneTimeStep(int time)
        {
            string result = "";
            int width = ObstacleField.GetLength(0);
            int height = ObstacleField.GetLength(1);
            for (int y = 0; y < height; y++)
            {
                for(int x = 0; x < width; x++)
                {
                    // borders
                    if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    {
                        if (y == 0 && x == Start.X)
                        {
                            result += "E";
                        }
                        else if(y == height - 1 && x == Goal.X)
                        {
                            result += ".";
                        }
                        else
                        {
                            result += "#";
                        }
                    }
                    else if (ObstacleField[x, y, time])
                    {
                        result += "X";
                    }
                    else
                    {
                        result += ".";
                    }
                }
                result += "\n";
            }
            Console.WriteLine(result);
        }
    }
}
