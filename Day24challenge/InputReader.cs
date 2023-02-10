namespace Day24challenge
{
    internal class InputReader 
    {
        // The positions of the blizzards are in a loop,
        // therefore we can convert time to the third spacial dimension of the valley with the size of the loop time (the least common multiple of the width and height of the blizzard area)
        // and use regular A* algorithm to find the goal in a 3D space with stationary obstacles.
        internal static ShortestPathProblemWithStationaryObstacles ComputeStationaryObsticleFieldProblemFromBlizzarards(string fileLocation, string fileName)
        {
            string textInFile = GetTextFromFile(fileLocation, fileName);
            return ComputeStationaryObsticleFieldProblemFromBlizzarards(textInFile);
        }

        internal static ShortestPathProblemWithStationaryObstacles ComputeStationaryObsticleFieldProblemFromBlizzarards(string unparsedValley)
        {
            string[] rows = unparsedValley.Split("\n");

            // find start and goal positions of problem:
            int startX = FindStartOrGoalPositionInRow(rows.First());
            Position start = new(startX, 0, 0);
            
            int goalX = FindStartOrGoalPositionInRow(rows.Last());
            Position goal = new(goalX, rows.Length - 1, 0);

            // Compute dimensions of 3D obsticle field.
            int height = rows.Length;
            int width = rows[0].Length;
            int innerHeight = height - 2;
            int innerWidth = width - 2;
            // -> Compute shortest time in which all blizzards are in original positions (i.e., the size of the third dimension):
            int blizzardConfigurationLoopTime = algorithms.LeastCommonMultiple.ComputeLeastCommonMultiple(innerHeight, innerWidth);
            // -> Initialize the problem with empty 3D obsticlefield:
            ShortestPathProblemWithStationaryObstacles Valley3DwithStationaryBlizzards = new(start, goal, width, height, blizzardConfigurationLoopTime);

            // Collect blizzards from text:
            HashSet<Blizzard> verticalBlizzards = new();
            HashSet<Blizzard> horizontalBlizzards = new();
            for (int y = 1; y < height - 1; y++)
            {
                for(int x = 1; x<width - 1; x++)
                {
                    switch (rows[y][x])
                    {
                        case '<':
                            horizontalBlizzards.Add(new BlizzardMovingLeft(x, y, width));
                            break;
                        case '>':
                            horizontalBlizzards.Add(new BlizzardMovingRight(x, y, width));
                            break;
                        case '^':
                            verticalBlizzards.Add(new BlizzardMovingUp(y, x, height));
                            break;
                        case 'v':
                            verticalBlizzards.Add(new BlizzardMovingDown(y, x, height));
                            break;
                        default:
                            break;
                    }
                }
            }

            // Convert dynamic blizzards of 2D grid into 3D spatial stationary blizzards (todo: eliminate duplicate code for horizontal and vertical blizzards):
            foreach (Blizzard blizzard in horizontalBlizzards)
            {
                int nrOfHorizontalLoops = blizzardConfigurationLoopTime / innerWidth;
                for(int minute = 0; minute < innerWidth; minute++)
                {
                    Position blizzardPositionAtMinute = blizzard.ComputePositionAtMinute(minute);
                    for(int horizontalLoopIndex = 0; horizontalLoopIndex < nrOfHorizontalLoops; horizontalLoopIndex++)
                    {
                        Valley3DwithStationaryBlizzards.ObstacleField[blizzardPositionAtMinute.X, blizzardPositionAtMinute.Y, minute + horizontalLoopIndex * innerWidth] = true;
                    }
                }
            }
            foreach (Blizzard blizzard in verticalBlizzards)
            {
                int nrOfVerticalLoops = blizzardConfigurationLoopTime / innerHeight;
                for (int minute = 0; minute < innerHeight; minute++)
                {
                    Position blizzardPositionAtMinute = blizzard.ComputePositionAtMinute(minute);
                    for (int horizontalLoopIndex = 0; horizontalLoopIndex < nrOfVerticalLoops; horizontalLoopIndex++)
                    {
                        Valley3DwithStationaryBlizzards.ObstacleField[blizzardPositionAtMinute.X, blizzardPositionAtMinute.Y, minute + horizontalLoopIndex * innerHeight] = true;
                    }
                }
            }
            return Valley3DwithStationaryBlizzards;
        }

        private static string GetTextFromFile(string fileLocation, string fileName)
        {
            return File.ReadAllText(fileLocation + fileName);
        }

        private static int FindStartOrGoalPositionInRow(string row)
        {
            for (int characterIndex = 0; characterIndex < row.Length; characterIndex++)
            {
                if (row[characterIndex] != '#')
                {
                    return characterIndex;
                }
            }
            return -1;
        }
    }
}
