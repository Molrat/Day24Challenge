using Day24challenge;
using Day24challenge.algorithms;

string input = "#E######\n#>>.<^<#\n#.<..<<#\n#>v.><>#\n#<^v^^>#\n######.#";
// The dimension of time can be seen as a third spacial dimension to convert to problem in a ordinary shortest path problem with stationary obstacles.
// Key to this is that the blizzard pattern repeats itself, making this third dimension limited in size.
ShortestPathProblemWithStationaryObstacles problem = InputReader.ComputeStationaryObsticleFieldProblemFromBlizzarards(input);
problem.PrintProblem();
AstarAlgorithm aStart = new(problem);
int result = aStart.ExecuteAstarAlgorithm();
Console.WriteLine(result);