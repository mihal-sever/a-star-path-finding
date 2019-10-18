using UnityEngine;

public struct MapColorSet
{
    public Color walkableColor;
    public Color obstacleColor;
    public Color startPointColor;
    public Color goalPointColor;
    public Color pathColor;

    public MapColorSet(Color walkableColor, Color obstacleColor, Color startPointColor, Color goalPointColor, Color pathColor)
    {
        this.walkableColor = walkableColor;
        this.obstacleColor = obstacleColor;
        this.startPointColor = startPointColor;
        this.goalPointColor = goalPointColor;
        this.pathColor = pathColor;
    }
}
