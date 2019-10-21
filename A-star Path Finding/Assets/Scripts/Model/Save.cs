using System;

[Serializable]
public class Save
{
    public int[,] Grid { get; private set; }
    public Point StartPoint { get; private set; }
    public Point GoalPoint { get; private set; }

    public Save(int[,] grid, Point startPoint, Point goalPoint)
    {
        Grid = grid;
        StartPoint = startPoint;
        GoalPoint = goalPoint;
    }
}
