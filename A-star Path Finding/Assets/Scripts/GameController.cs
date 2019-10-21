public class GameController : GameControllerBase
{
    IPathFinder pathFinder;

    public GameController(IMapView view, IUiView uiView, IMapModel mapModel, IPathFinder pathFinder, IMapSaver mapSaver) 
        : base(view, uiView, mapModel, mapSaver)
    {
        this.pathFinder = pathFinder;
    }

    protected override void SetupMapModel()
    {
        Point startPoint = ConvertToPoint(mapView.GetStartPoint());
        Point goalPoint = ConvertToPoint(mapView.GetGoalPoint());
        int[,] grid = mapView.GetGrid();

        mapModel.SetupMapModel(grid, startPoint, goalPoint, pathFinder);
    }
}
