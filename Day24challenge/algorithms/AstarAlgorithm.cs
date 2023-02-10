namespace Day24challenge.algorithms
{
    internal class AstarAlgorithm
    {
        // Special case of the astar algorithm where the cost of one step is always 1, and the neighbors of a node are always at z = (z + 1) % maxZ.
        private readonly ShortestPathProblemWithStationaryObstacles problem;
        private readonly PriorityQueue<Node, int> front = new();

        internal AstarAlgorithm(ShortestPathProblemWithStationaryObstacles problem)
        {
            this.problem = problem;
        }

        internal int ExecuteAstarAlgorithm()
        {
            Node startNode = new(problem.Start.X, problem.Start.Y, problem.Start.Z, 0);
            AddNodeToFront(startNode);
            while (front.Count > 0)
            {
                Node currentNode = front.Dequeue();
                if (currentNode.Position.X == problem.Goal.X && currentNode.Position.Y == problem.Goal.Y)
                {
                    return currentNode.CostSoFar;
                }
                ComputeNextNodes(currentNode);
            }
            return -1;
        }

        private void AddNodeToFront(Node node)
        {
            int estimatedRemainingCost = CostHeuristic(node);
            front.Enqueue(node, node.CostSoFar + estimatedRemainingCost);
            // since all steps have cost 1, it is impossible that a second time a 3D position is reached the cost is lower.
            // therefore, we make it impossible to go to the same position twice by turning it into an obstacle:
            problem.ObstacleField[node.Position.X, node.Position.Y, node.Position.Z] = true; 
        }

        private int CostHeuristic(in Node node)
        {
            return node.Position.ManhattanDistanceToOtherPosition(problem.Goal);
        }

        private void ComputeNextNodes(Node currentNode)
        {
            // Loop over a diamond shape around the current node's position, bounded by the walls of the obsticle field.
            // if z reached limit, go back to 0. This represent the blizzards repeating pattern.
            int z = (currentNode.Position.Z + 1) % problem.ObstacleField.GetLength(2);
            int currentX = currentNode.Position.X;
            int currentY = currentNode.Position.Y;
            int nextCostSoFar = currentNode.CostSoFar + 1;

            // try same place:
            if (!problem.ObstacleField[currentX, currentY, z])
            {
                Node newNode = new(currentX, currentY, z, nextCostSoFar);
                AddNodeToFront(newNode);
            }
            // try up:
            if (currentY > 1 && !problem.ObstacleField[currentX, currentY - 1, z])
            {
                Node newNode = new(currentX, currentY - 1, z,nextCostSoFar);
                AddNodeToFront(newNode);
            }
            // try right (not possible from start position):
            if(currentY != problem.Start.Y && currentX < problem.ObstacleField.GetLength(0) - 2 && !problem.ObstacleField[currentX + 1, currentY, z])
            {
                Node newNode = new(currentX + 1, currentY, z, nextCostSoFar);
                AddNodeToFront(newNode);
            }
            // try down (also possible if above goal):
            if ((currentX == problem.Goal.X && currentY + 1 == problem.Goal.Y) || (currentY < problem.ObstacleField.GetLength(1) - 2) && !problem.ObstacleField[currentX, currentY + 1, z])
            {
                Node newNode = new(currentX, currentY + 1, z, nextCostSoFar);
                AddNodeToFront(newNode);
            }
            // try left (not possible from start position):
            if (currentY != problem.Start.Y && currentX > 1 && !problem.ObstacleField[currentX + 1, currentY, z])
            {
                Node newNode = new(currentX - 1, currentY, z, nextCostSoFar);
                AddNodeToFront(newNode);
            }
        }
    }
}
