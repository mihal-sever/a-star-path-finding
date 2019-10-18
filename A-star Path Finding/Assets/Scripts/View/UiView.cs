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
    private MessagePanel messagePanel;

    public event Action OnEditMapPressed;
    public event Action OnEditStartPointPressed;
    public event Action OnEditGoalPointPressed;
    public event Action OnFindPathPressed;
    public event Action OnClearPathPressed;

    private void Awake()
    {
        editMapButton.onClick.AddListener(()=>OnEditMapPressed?.Invoke());
        editStartPointButton.onClick.AddListener(() => OnEditStartPointPressed?.Invoke());
        editGoalPointButton.onClick.AddListener(() => OnEditGoalPointPressed?.Invoke());
        findPathButton.onClick.AddListener(() => OnFindPathPressed?.Invoke());
        clearPathButton.onClick.AddListener(OnClearPath);

        messagePanel.gameObject.SetActive(false);
    }

    private void Start()
    {
        clearPathButton.interactable = false;
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

    private void OnClearPath()
    {
        editMapButton.interactable = true;
        editStartPointButton.interactable = true;
        editGoalPointButton.interactable = true;
        findPathButton.interactable = true;
        clearPathButton.interactable = false;

        OnClearPathPressed?.Invoke();
    }
}
