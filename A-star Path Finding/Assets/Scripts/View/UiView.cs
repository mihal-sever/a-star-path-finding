using System;
using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour, IUiView
{
    [SerializeField]
    private Button addObstaclesButton;
    [SerializeField]
    private Button removeObstaclesButton;
    [SerializeField]
    private Button editStartPointButton;
    [SerializeField]
    private Button editGoalPointButton;
    [SerializeField]
    private Button findPathButton;
    [SerializeField]
    private Button clearPathButton;
    [SerializeField]
    private Button clearMapButton;
    [SerializeField]
    private Button saveMapButton;
    [SerializeField]
    private MessagePanel messagePanel;

    public event Action OnAddObstaclesPressed;
    public event Action OnRemoveObstaclesPressed;
    public event Action OnEditStartPointPressed;
    public event Action OnEditGoalPointPressed;
    public event Action OnFindPathPressed;
    public event Action OnClearPathPressed;
    public event Action OnClearMapPressed;
    public event Action OnSaveMapPressed;

    private void Awake()
    {
        addObstaclesButton.onClick.AddListener(()=>OnAddObstaclesPressed?.Invoke());
        removeObstaclesButton.onClick.AddListener(() => OnRemoveObstaclesPressed?.Invoke());
        editStartPointButton.onClick.AddListener(() => OnEditStartPointPressed?.Invoke());
        editGoalPointButton.onClick.AddListener(() => OnEditGoalPointPressed?.Invoke());
        findPathButton.onClick.AddListener(() => OnFindPathPressed?.Invoke());
        clearPathButton.onClick.AddListener(OnClearPath);
        clearMapButton.onClick.AddListener(OnClearMap);
        saveMapButton.onClick.AddListener(() => OnSaveMapPressed?.Invoke());

        messagePanel.gameObject.SetActive(false);
    }

    private void Start()
    {
        ResetUiView();
    }

    public void ShowMessage(string message)
    {
        messagePanel.ShowMessage(message);
    }

    public void EnableCleanPath()
    {
        addObstaclesButton.interactable = false;
        removeObstaclesButton.interactable = false;
        editStartPointButton.interactable = false;
        editGoalPointButton.interactable = false;
        findPathButton.interactable = false;
        clearPathButton.interactable = true;
    }
    
    public void ResetUiView()
    {
        addObstaclesButton.interactable = true;
        removeObstaclesButton.interactable = true;
        editStartPointButton.interactable = true;
        editGoalPointButton.interactable = true;
        findPathButton.interactable = true;
        clearMapButton.interactable = true;
        clearPathButton.interactable = false;
    }

    private void OnClearMap()
    {
        ResetUiView();
        OnClearMapPressed?.Invoke();
    }

    private void OnClearPath()
    {
        ResetUiView();
        OnClearPathPressed?.Invoke();
    }
}
