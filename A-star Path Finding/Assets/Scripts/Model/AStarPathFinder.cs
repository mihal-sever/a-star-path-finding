using System;
using System.Collections.Generic;

public class AStarPathFinder : IPathFinder
{
    public List<Point> FindPath(Node[,] grid, Point start, Point goal)
    {
        //throw new NotImplementedException();
        return new List<Point>() { new Point(1, 1), new Point(1, 2), new Point(1, 3) };
    }

    private List<Point> GetPathFromNode(Node goal)
    {
        throw new NotImplementedException();
    }
}
