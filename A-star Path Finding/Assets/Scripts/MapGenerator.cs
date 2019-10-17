using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject cellPrefab;

    private int rows = 50;
    private int cols = 50;
    private float outlinePersent = .05f;

    private Renderer[,] mapRenderers;

    private Color walkableColor = Color.white;
    private Color obstaclesColor = Color.red;

    private Camera cam;
    private Transform prevHit = null;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            EditMap();
        }
    }
    
    private void GenerateMap()
    {
        mapRenderers = new Renderer[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                Vector3 cellPosition = new Vector3(col, 0, row);
                Transform cell = Instantiate(cellPrefab, cellPosition, Quaternion.Euler(Vector3.right * 90), transform).transform;
                cell.localScale = Vector3.one * (1 - outlinePersent);
                cell.name = row + " " + col;

                mapRenderers[row, col] = cell.GetComponent<Renderer>();
            }
        }
    }

    private void EditMap()
    {
        var pos = Input.mousePosition;
        var ray = cam.ScreenPointToRay(new Vector3(pos.x, pos.y, 0));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (prevHit == hit.transform)
                return;

            prevHit = hit.transform;
            ChangeCellColor(GetCellCoordinates(hit.point));
        }
    }
    
    private void ChangeCellColor(Vector2Int cellCoordinates)
    {
        Renderer rend = mapRenderers[cellCoordinates.y, cellCoordinates.x];
        rend.material.color = rend.material.color == obstaclesColor ? walkableColor : obstaclesColor;
    }

    private Vector2Int GetCellCoordinates(Vector3 cellWorldPosition)
    {
        return new Vector2Int(
            Mathf.RoundToInt(cellWorldPosition.x),
            Mathf.RoundToInt(cellWorldPosition.z));
    }
}
