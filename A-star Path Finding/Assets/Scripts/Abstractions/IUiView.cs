using System;

public interface IUiView
{
    event Action OnEditMapPressed;
    event Action OnEditStartPointPressed;
    event Action OnEditGoalPointPressed;
    event Action OnFindPathPressed;
    event Action OnClearPathPressed;
    void ShowMessage(string message);
    void EnableCleanPath();
}
