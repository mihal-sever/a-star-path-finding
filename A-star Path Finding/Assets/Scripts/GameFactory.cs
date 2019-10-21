using UnityEngine;

public class GameFactory : MonoBehaviour
{
    [SerializeField]
    private MapView mapView;
    [SerializeField]
    private UiView uiView;

    private GameControllerBase controller;

    private void Awake()
    {
        if (mapView == null || uiView == null)
            Application.Quit();

        controller = new GameController(mapView, uiView, new MapModel(), new AStarPathFinder(), new MapSaver());
    }
}
