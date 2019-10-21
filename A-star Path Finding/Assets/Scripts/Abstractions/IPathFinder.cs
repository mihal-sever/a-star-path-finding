using System.Collections.Generic;

public interface IPathFinder
{
    List<Point> FindPath(int[,] grid, Point start, Point goal);
}
