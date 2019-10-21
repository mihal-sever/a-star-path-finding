using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GameControllerBase
{
    protected IMapView mapView;
    protected IUiView uiView;
    protected IMapModel mapModel;
    protected IMapSaver mapSaver;

    protected GameControllerBase(IMapView mapView, IUiView uiView, IMapModel mapModel, IMapSaver mapSaver)
    {
        this.mapView = mapView;
        this.uiView = uiView;
        this.mapModel = mapModel;
        this.mapSaver = mapSaver;

        mapModel.OnPathChanged += OnPathChanged;

        uiView.OnFindPathPressed += OnFindPathPressed;
        uiView.OnEditObstaclesPressed += OnEditObstaclesPressed;
        uiView.OnEditStartPointPressed += OnEditStartPointPressed;
        uiView.OnEditGoalPointPressed += OnEditGoalPointPressed;
        uiView.OnClearPathPressed += OnClearPathPressed;
        uiView.OnClearMapPressed += OnClearMapPressed;
        uiView.OnSaveMapPressed += OnSaveMapPressed;

        mapView.onStart += LoadMap;
    }

    protected virtual void OnPathChanged(List<Point> path)
    {
        if (path == null)
        {
            uiView.ShowMessage("Path has not been found.");
            uiView.ResetUiView();
            return;
        }

        List<Vector2Int> unityPath = path.Select(p => ConvertToVector2Int(p)).ToList();
        mapView.DrawPath(unityPath);
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

    protected virtual void OnClearPathPressed()
    {
        mapView.ClearPath();
    }

    protected virtual void OnClearMapPressed()
    {
        mapView.ClearMap();
    }

    protected virtual void OnSaveMapPressed()
    {
        if (!mapView.IsMapCompleted())
        {
            uiView.ShowMessage("Start and Goal points should be set.");
            return;
        }

        Point startPoint = ConvertToPoint(mapView.GetStartPoint());
        Point goalPoint = ConvertToPoint(mapView.GetGoalPoint());

        mapSaver.SaveMap(mapView.GetGrid(), startPoint, goalPoint);
    }

    protected virtual void LoadMap()
    {
        Save save = mapSaver.LoadMap();

        if (save != null)
        {
            Vector2Int startPoint = ConvertToVector2Int(save.StartPoint);
            Vector2Int goalPoint = ConvertToVector2Int(save.GoalPoint);

            mapView.DrawLoadedMap(save.Grid, startPoint, goalPoint);
        }
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
