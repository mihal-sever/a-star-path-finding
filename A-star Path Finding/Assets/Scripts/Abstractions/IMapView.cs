using System.Collections.Generic;
using UnityEngine;

public interface IMapView
{
    void ChangeState(MapState state);

    Vector2Int GetMapSize();
    Vector2Int GetStartPoint();
    Vector2Int GetGoalPoint();
    int[,] GetGrid();
    bool IsMapCompleted();

    void DrawPath(List<Vector2Int> path);
    void ClearPath();
    void ClearMap();
}
