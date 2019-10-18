using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class GameControllerBase
{
    protected IMapView view;
    protected IUiView uiView;
    protected IMapModel model;

    protected GameControllerBase(IMapView view, IUiView uiView)
    {
        this.view = view;
        this.uiView = uiView;

        uiView.OnFindPathPressed += OnFindPathPressed;
        uiView.OnEditObstaclesPressed += OnEditObstaclesPressed;
        uiView.OnEditStartPointPressed += OnEditStartPointPressed;
        uiView.OnEditGoalPointPressed += OnEditGoalPointPressed;
        uiView.OnClearPathPressed += OnClearPathPressed;
        uiView.OnClearMapPressed += OnClearMapPressed;
    }

    protected virtual void OnFindPathPressed()
    {
        if (!view.IsMapCompleted())
        {
            uiView.ShowMessage("Start and Goal points should be set.");
            return;
        }
        uiView.EnableCleanPath();

        if (model == null)
        {
            model = CreateMap();
            model.OnPathChanged += OnPathChanged;
        }
        model.FindPath();
    }

    protected virtual void OnClearPathPressed()
    {
        view.ClearPath();
    }

    protected virtual void OnClearMapPressed()
    {
        view.ClearMap();
    }
    protected virtual void OnPathChanged(List<Point> path)
    {
        if (path == null)
        {
            uiView.ShowMessage("Path has not been found.");
            return;
        }

        List<Vector2Int> unityPath = path.Select(p => ConvertToVector2Int(p)).ToList();
        view.DrawPath(unityPath);
    }

    protected virtual void OnEditObstaclesPressed()
    {
        view.ChangeState(MapState.EditObstacles);
    }

    protected virtual void OnEditStartPointPressed()
    {
        view.ChangeState(MapState.EditStartPoint);
    }

    protected virtual void OnEditGoalPointPressed()
    {
        view.ChangeState(MapState.EditGoalPoint);
    }

    protected abstract IMapModel CreateMap();

    protected abstract IPathFinder GetPathFinder();

    protected Point ConvertToPoint(Vector2Int vector)
    {
        return new Point(vector.x, vector.y);
    }

    protected Vector2Int ConvertToVector2Int(Point point)
    {
        return new Vector2Int(point.x, point.y);
    }
}
