public class GameController : GameControllerBase
{
    IPathFinder pathFinder;

    public GameController(IMapView view, IUiView uiView, IMapModel mapModel, IPathFinder pathFinder) : base(view, uiView)
    {
        base.mapModel = mapModel;
        this.pathFinder = pathFinder;

        mapModel.OnPathChanged += OnPathChanged;
    }

    protected override void SetupMapModel()
    {
        Point startPoint = ConvertToPoint(mapView.GetStartPoint());
        Point goalPoint = ConvertToPoint(mapView.GetGoalPoint());
        int[,] grid = mapView.GetGrid();

        mapModel.SetupMapModel(grid, startPoint, goalPoint, pathFinder);
    }
}
