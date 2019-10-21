using System;

public interface IUiView
{
    event Action OnEditObstaclesPressed;
    event Action OnEditStartPointPressed;
    event Action OnEditGoalPointPressed;
    event Action OnFindPathPressed;
    event Action OnClearPathPressed;
    event Action OnClearMapPressed;

    void ShowMessage(string message);
    void EnableCleanPath();
    void ResetUiView();
}
