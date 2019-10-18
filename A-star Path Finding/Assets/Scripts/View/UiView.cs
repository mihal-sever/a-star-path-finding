using System;
using UnityEngine;
using UnityEngine.UI;

public class UiView : MonoBehaviour, IUiView
{
    [SerializeField]
    private Button editMapButton;
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
    private MessagePanel messagePanel;

    public event Action OnEditMapPressed;
    public event Action OnEditStartPointPressed;
    public event Action OnEditGoalPointPressed;
    public event Action OnFindPathPressed;
    public event Action OnClearPathPressed;
    public event Action OnClearMapPressed;

    private void Awake()
    {
        editMapButton.onClick.AddListener(()=>OnEditMapPressed?.Invoke());
        editStartPointButton.onClick.AddListener(() => OnEditStartPointPressed?.Invoke());
        editGoalPointButton.onClick.AddListener(() => OnEditGoalPointPressed?.Invoke());
        findPathButton.onClick.AddListener(() => OnFindPathPressed?.Invoke());
        clearPathButton.onClick.AddListener(OnClearPath);
        clearMapButton.onClick.AddListener(OnClearMap);

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
        editMapButton.interactable = false;
        editStartPointButton.interactable = false;
        editGoalPointButton.interactable = false;
        findPathButton.interactable = false;
        clearPathButton.interactable = true;
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

    private void ResetUiView()
    {
        editMapButton.interactable = true;
        editStartPointButton.interactable = true;
        editGoalPointButton.interactable = true;
        findPathButton.interactable = true;
        clearMapButton.interactable = true;
        clearPathButton.interactable = false;
    }
}
