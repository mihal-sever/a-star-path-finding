using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    EditMap,
    EditStartPoint,
    EditGoalPoint
}


// TODO: implement isMapCompletedProperly
// TODO: get rid of switch in Update
public class MapView : MonoBehaviour, IMapView
{
    [SerializeField]
    private GameObject cellPrefab;

    private Vector2Int mapSize;
    private Vector2Int startPoint;
    private Vector2Int goalPoint;
    private List<Vector2Int> path;

    private const float outlineWidthInPersent = .05f;

    private MapState state;

    private Renderer[,] mapRenderers;
    private MapColorSet colorSet;

    private Camera cam;
    private Transform prevHit;


    private void Awake()
    {
        cam = Camera.main;
        state = MapState.EditMap;
        mapSize = new Vector2Int(50, 50);
        CreateMapColorSet();
    }

    private void Start()
    {
        GenerateMap();
    }

    private void Update()
    {
        switch (state)
        {
            case MapState.EditMap:
                EditMap();
                break;
            case MapState.EditStartPoint:
                EditPoint(ref startPoint, colorSet.startPointColor);
                break;
            case MapState.EditGoalPoint:
                EditPoint(ref goalPoint, colorSet.goalPointColor);
                break;
        }
    }
    
    public Vector2Int GetMapSize() => mapSize;

    public Vector2Int GetStartPoint() => startPoint;

    public Vector2Int GetGoalPoint() => goalPoint;

    public int[,] GetGrid()
    {
        int[,] grid = new int[mapSize.x, mapSize.y];
        for (int x = 0; x < grid.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                if (mapRenderers[x, y].material.color == colorSet.obstacleColor)
                    grid[x, y] = int.MaxValue;
                else
                    grid[x, y] = 1;
            }
        }
        return grid;
    }

    public bool IsMapCompleted()
    {
        return startPoint != Vector2Int.zero && goalPoint != Vector2Int.zero;
    }
    
    public void ChangeState(MapState state)
    {
        this.state = state;
    }

    public void ClearPath()
    {
        foreach (Vector2Int v in path)
        {
            ChangeCellColor(v, colorSet.walkableColor);
        }
        path = null;
    }

    public void DrawPath(List<Vector2Int> path)
    {
        this.path = path;
        foreach (Vector2Int v in path)
        {
            ChangeCellColor(v, colorSet.pathColor);
        }
    }

    public void ClearMap()
    {
        startPoint = Vector2Int.zero;
        goalPoint = Vector2Int.zero;
        path = null;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                ChangeCellColor(new Vector2Int(x, y), colorSet.walkableColor);
            }
        }
    }

    private void EditMap()
    {
        if (!Input.GetMouseButton(0))
            return;

        var pos = Input.mousePosition;
        var ray = cam.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (prevHit == hit.transform)
                return;

            prevHit = hit.transform;
            Vector2Int cell = GetCellCoordinates(hit.point);

            if (mapRenderers[cell.y, cell.x].material.color == colorSet.obstacleColor)
                ChangeCellColor(cell, colorSet.walkableColor);
            else
                ChangeCellColor(cell, colorSet.obstacleColor);
        }
    }
    
    private void EditPoint(ref Vector2Int point, Color color)
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        var pos = Input.mousePosition;
        var ray = cam.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (point != Vector2Int.zero)
                ChangeCellColor(point, colorSet.walkableColor);

            point = GetCellCoordinates(hit.point);
            ChangeCellColor(point, color);
        }
    }
    
    private void GenerateMap()
    {
        mapRenderers = new Renderer[mapSize.x, mapSize.y];

        for (int x = 0; x < mapRenderers.GetLength(0); x++)
        {
            for (int y = 0; y < mapRenderers.GetLength(1); y++)
            {
                Vector3 cellPosition = new Vector3(y, 0, x);
                Transform cell = Instantiate(cellPrefab, cellPosition, Quaternion.Euler(Vector3.right * 90), transform).transform;
                cell.localScale = Vector3.one * (1 - outlineWidthInPersent);
                cell.name = x + " " + y;

                mapRenderers[x, y] = cell.GetComponent<Renderer>();
            }
        }
    }
    
    private void ChangeCellColor(Vector2Int cellCoordinates, Color color)
    {
        Renderer rend = mapRenderers[cellCoordinates.y, cellCoordinates.x];
        rend.material.color = color;
    }

    private Vector2Int GetCellCoordinates(Vector3 cellWorldPosition)
    {
        return new Vector2Int(
            Mathf.RoundToInt(cellWorldPosition.x),
            Mathf.RoundToInt(cellWorldPosition.z));
    }

    private void CreateMapColorSet()
    {
        colorSet = new MapColorSet(Color.white, Color.red, Color.blue, Color.green, Color.gray);
    }
}