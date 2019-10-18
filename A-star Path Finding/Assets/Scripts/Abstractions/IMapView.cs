using System.Collections.Generic;
using UnityEngine;

public interface IMapView
{
    Vector2Int GetMapSize();
    Vector2Int GetStartPoint();
    Vector2Int GetGoalPoint();
    int[,] GetGrid();
    bool IsMapCompleted();
    void ChangeState(MapState state);
    void ClearPath();
    void DrawPath(List<Vector2Int> path);
    void ClearMap();
}
