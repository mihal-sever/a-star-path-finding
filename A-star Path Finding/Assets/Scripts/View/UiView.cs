using System;
using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour, IUiView
{
    [SerializeField]
    private Button editObstaclesButton;
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

    public event Action OnEditObstaclesPressed;
    public event Action OnEditStartPointPressed;
    public event Action OnEditGoalPointPressed;
    public event Action OnFindPathPressed;
    public event Action OnClearPathPressed;
    public event Action OnClearMapPressed;
    public event Action OnSaveMapPressed;

    private void Awake()
    {
        editObstaclesButton.onClick.AddListener(()=>OnEditObstaclesPressed?.Invoke());
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
        editObstaclesButton.interactable = false;
        editStartPointButton.interactable = false;
        editGoalPointButton.interactable = false;
        findPathButton.interactable = false;
        clearPathButton.interactable = true;
    }
    
    public void ResetUiView()
    {
        editObstaclesButton.interactable = true;
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
