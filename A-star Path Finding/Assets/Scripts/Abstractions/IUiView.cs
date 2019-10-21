using System;

public interface IUiView
{
    event Action OnAddObstaclesPressed;
    event Action OnRemoveObstaclesPressed;
    event Action OnEditStartPointPressed;
    event Action OnEditGoalPointPressed;
    event Action OnFindPathPressed;
    event Action OnClearPathPressed;
    event Action OnClearMapPressed;
    event Action OnSaveMapPressed;

    void ShowMessage(string message);
    void EnableCleanPath();
    void ResetUiView();
}
