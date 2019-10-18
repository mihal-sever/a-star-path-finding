public class GameController : GameControllerBase
{
    public GameController(IMapView view, IUiView uiView) : base(view, uiView)
    { }

    protected override IMapModel CreateMap()
    {
        Point mapSize = ConvertToPoint(view.GetMapSize());
        Point startPoint = ConvertToPoint(view.GetStartPoint());
        Point goalPoint = ConvertToPoint(view.GetGoalPoint());
        int[,] grid = view.GetGrid();

        Node[,] mapGrid = new Node[grid.GetLength(0), grid.GetLength(1)];
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                mapGrid[x, y] = new Node(new Point(x, y), grid[x, y]);
            }
        }

        return new MapModel(mapGrid, startPoint, goalPoint, GetPathFinder());
    }

    protected override IPathFinder GetPathFinder()
    {
        return new AStarPathFinder();
    }
}
