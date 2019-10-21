using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    public Text textMessage;
    public Button acceptButton;
    public Behaviour[] behavioursToBeBlocked;
    
    private void Awake()
    {
        acceptButton.onClick.AddListener(AcceptButtonPressed);
    }

    public void ShowMessage(string message)
    {
        SetComponentsEnabled(false);
        textMessage.text = message;
        gameObject.SetActive(true);
    }

    private void AcceptButtonPressed()
    {
        SetComponentsEnabled(true);
        gameObject.SetActive(false);
    }

    private void SetComponentsEnabled(bool isEnabled)
    {
        foreach (Behaviour b in behavioursToBeBlocked)
        {
            b.enabled = isEnabled;
        }
    }
}
