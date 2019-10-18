public class GameController : GameControllerBase
{
    IPathFinder pathFinder;

    public GameController(IMapView view, IUiView uiView, IMapModel mapModel, IPathFinder pathFinder) : base(view, uiView)
    {
        base.mapModel = mapModel;
        this.pathFinder = pathFinder;
    }

    protected override void SetupMapModel()
    {
        Point mapSize = ConvertToPoint(mapView.GetMapSize());
        Point startPoint = ConvertToPoint(mapView.GetStartPoint());
        Point goalPoint = ConvertToPoint(mapView.GetGoalPoint());
        int[,] grid = mapView.GetGrid();

        Node[,] mapGrid = new Node[grid.GetLength(0), grid.GetLength(1)];
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                mapGrid[x, y] = new Node(new Point(x, y), grid[x, y]);
            }
        }

        mapModel.SetupMapModel(mapGrid, startPoint, goalPoint, pathFinder);
    }
}
