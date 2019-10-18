using System;
using System.Collections.Generic;

public class MapModel : IMapModel
{
    public event Action<List<Point>> OnPathChanged;

    private Node[,] grid;
    private Point startPoint;
    private Point goalPoint;
    private IPathFinder pathFinder;

    private List<Point> path;
    private List<Point> Path
    {
        get { return path; }
        set
        {
            path = value;
            OnPathChanged?.Invoke(path);
        }
    }
    
    public MapModel(Node[,] grid, Point startPoint, Point goalPoint, IPathFinder pathFinder)
    {
        this.grid = grid;
        this.startPoint = startPoint;
        this.goalPoint = goalPoint;
        this.pathFinder = pathFinder;
    }

    public void FindPath()
    {
        Path = pathFinder.FindPath(grid, startPoint, goalPoint);
    }
}
