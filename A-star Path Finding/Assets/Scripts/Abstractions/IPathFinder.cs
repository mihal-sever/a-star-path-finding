using System.Collections.Generic;

public interface IPathFinder
{
    List<Point> FindPath(Node[,] grid, Point start, Point goal);
}
