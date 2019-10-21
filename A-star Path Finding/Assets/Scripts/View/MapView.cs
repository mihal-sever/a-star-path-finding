using System;
using System.Collections.Generic;
using UnityEngine;

public enum MapState
{
    EditObstacles,
    EditStartPoint,
    EditGoalPoint
}


public class MapView : MonoBehaviour, IMapView
{
    public event Action onStart;

    [SerializeField]
    private GameObject cellPrefab;

    private Vector2Int mapSize;
    private Vector2Int? startPoint;
    private Vector2Int? goalPoint;
    private List<Vector2Int> path;

    private const float outlineWidthInPersent = .1f;

    private MapState state;

    private Renderer[,] mapRenderers;
    private MapColorSet colorSet;

    private Camera cam;
    private Transform prevHit;


    private void Awake()
    {
        cam = Camera.main;
        state = MapState.EditObstacles;
        mapSize = new Vector2Int(50, 50);
        CreateMapColorSet();
    }

    private void Start()
    {
        GenerateMap();
        onStart();
    }

    private void Update()
    {
        switch (state)
        {
            case MapState.EditObstacles:
                EditObstacles();
                break;
            case MapState.EditStartPoint:
                EditPoint(ref startPoint, colorSet.startPointColor);
                break;
            case MapState.EditGoalPoint:
                EditPoint(ref goalPoint, colorSet.goalPointColor);
                break;
        }
    }

    public void ChangeState(MapState state)
    {
        this.state = state;
    }

    public Vector2Int GetMapSize() => mapSize;

    public Vector2Int GetStartPoint() => startPoint.Value;

    public Vector2Int GetGoalPoint() => goalPoint.Value;

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
        return startPoint.HasValue && goalPoint.HasValue &&
                (GetCellColor(startPoint.Value) == colorSet.startPointColor) &&
                (GetCellColor(goalPoint.Value) == colorSet.goalPointColor);
    }
    
    public void DrawPath(List<Vector2Int> path)
    {
        this.path = path;

        for (int i = 1; i < path.Count - 1; i++)
        {
            ChangeCellColor(path[i], colorSet.pathColor);
        }
    }

    public void ClearPath()
    {
        for (int i = 1; i < path.Count - 1; i++)
        {
            ChangeCellColor(path[i], colorSet.walkableColor);
        }
        path = null;
    }

    public void ClearMap()
    {
        startPoint = null;
        goalPoint = null;
        path = null;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                ChangeCellColor(new Vector2Int(x, y), colorSet.walkableColor);
            }
        }
    }

    public void DrawLoadedMap(int[,] grid, Vector2Int startPoint, Vector2Int goalPoint)
    {
        for (int x = 0; x < grid.GetLength(0) && x < mapRenderers.GetLength(0); x++)
        {
            for (int y = 0; y < grid.GetLength(1) && y < mapRenderers.GetLength(1); y++)
            {
                if (grid[x, y] == int.MaxValue)
                    ChangeCellColor(new Vector2Int(x, y), colorSet.obstacleColor);
                else
                    ChangeCellColor(new Vector2Int(x, y), colorSet.walkableColor);
            }
        }
        this.startPoint = startPoint;
        this.goalPoint = goalPoint;

        ChangeCellColor(startPoint, colorSet.startPointColor);
        ChangeCellColor(goalPoint, colorSet.goalPointColor);
    }

    private void EditObstacles()
    {
        if (!Input.GetMouseButton(0))
        {
            prevHit = null;
            return;
        }
        
        RaycastHit? hit = GetHit();
        if (hit.HasValue)
        {
            if (prevHit == hit.Value.transform)
                return;

            prevHit = hit.Value.transform;
            Vector2Int cell = GetCellCoordinates(hit.Value.point);

            if (GetCellColor(cell) == colorSet.obstacleColor)
                ChangeCellColor(cell, colorSet.walkableColor);
            else
                ChangeCellColor(cell, colorSet.obstacleColor);
        }
    }
    
    private void EditPoint(ref Vector2Int? point, Color color)
    {
        if (!Input.GetMouseButtonDown(0))
            return;
        
        RaycastHit? hit = GetHit();
        if (hit.HasValue)
        {
            if (point.HasValue && GetCellColor(point.Value) == color)
            {
                ChangeCellColor(point.Value, colorSet.walkableColor);
            }

            point = GetCellCoordinates(hit.Value.point);
            ChangeCellColor(point.Value, color);
        }
    }

    private RaycastHit? GetHit()
    {
        var pos = Input.mousePosition;
        var ray = cam.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            return hit;
        return null;
    }

    private void GenerateMap()
    {
        mapRenderers = new Renderer[mapSize.x, mapSize.y];
        for (int x = 0; x < mapRenderers.GetLength(0); x++)
        {
            for (int y = 0; y < mapRenderers.GetLength(1); y++)
            {
                Vector3 cellPosition = new Vector3(x, 0, y);
                Transform cell = Instantiate(cellPrefab, cellPosition, Quaternion.Euler(Vector3.right * 90), transform).transform;
                cell.localScale = Vector3.one * (1 - outlineWidthInPersent);
                cell.name = x + " " + y;

                mapRenderers[x, y] = cell.GetComponent<Renderer>();
            }
        }
    }
    
    private void ChangeCellColor(Vector2Int cellCoordinates, Color color)
    {
        mapRenderers[cellCoordinates.x, cellCoordinates.y].material.color = color;
    }

    private Color GetCellColor(Vector2Int cellCoordinates)
    {
        return mapRenderers[cellCoordinates.x, cellCoordinates.y].material.color;
    }

    private Vector2Int GetCellCoordinates(Vector3 cellWorldPosition)
    {
        return new Vector2Int(
            Mathf.RoundToInt(cellWorldPosition.x),
            Mathf.RoundToInt(cellWorldPosition.z));
    }

    private void CreateMapColorSet()
    {
        colorSet = new MapColorSet(Color.white, Color.red, Color.blue, Color.green, Color.yellow);
    }
}