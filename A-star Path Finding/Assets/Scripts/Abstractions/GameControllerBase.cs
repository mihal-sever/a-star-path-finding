using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GameControllerBase
{
    protected IMapView mapView;
    protected IUiView uiView;
    protected IMapModel mapModel;

    protected GameControllerBase(IMapView view, IUiView uiView)
    {
        this.mapView = view;
        this.uiView = uiView;

        uiView.OnFindPathPressed += OnFindPathPressed;
        uiView.OnEditObstaclesPressed += OnEditObstaclesPressed;
        uiView.OnEditStartPointPressed += OnEditStartPointPressed;
        uiView.OnEditGoalPointPressed += OnEditGoalPointPressed;
        uiView.OnClearPathPressed += OnClearPathPressed;
        uiView.OnClearMapPressed += OnClearMapPressed;

        mapModel.OnPathChanged += OnPathChanged;
    }

    protected virtual void OnFindPathPressed()
    {
        if (!mapView.IsMapCompleted())
        {
            uiView.ShowMessage("Start and Goal points should be set.");
            return;
        }
        uiView.EnableCleanPath();
        
        SetupMapModel();
        mapModel.FindPath();
    }

    protected virtual void OnClearPathPressed()
    {
        mapView.ClearPath();
    }

    protected virtual void OnClearMapPressed()
    {
        mapView.ClearMap();
    }
    protected virtual void OnPathChanged(List<Point> path)
    {
        if (path == null)
        {
            uiView.ShowMessage("Path has not been found.");
            return;
        }

        List<Vector2Int> unityPath = path.Select(p => ConvertToVector2Int(p)).ToList();
        mapView.DrawPath(unityPath);
    }

    protected virtual void OnEditObstaclesPressed()
    {
        mapView.ChangeState(MapState.EditObstacles);
    }

    protected virtual void OnEditStartPointPressed()
    {
        mapView.ChangeState(MapState.EditStartPoint);
    }

    protected virtual void OnEditGoalPointPressed()
    {
        mapView.ChangeState(MapState.EditGoalPoint);
    }

    protected abstract void SetupMapModel();

    protected Point ConvertToPoint(Vector2Int vector)
    {
        return new Point(vector.x, vector.y);
    }

    protected Vector2Int ConvertToVector2Int(Point point)
    {
        return new Vector2Int(point.x, point.y);
    }
}
