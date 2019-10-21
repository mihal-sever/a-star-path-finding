using UnityEngine;

public interface IMapSaver
{
    void SaveMap(int[,] grid, Point startPoint, Point goalPoint);
    Save LoadMap();
}
