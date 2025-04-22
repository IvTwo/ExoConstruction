using UnityEngine;
using Michsky.UI.Heat;

public class BindActivityButton : MonoBehaviour
{
    public BoxButtonManager activityButton;
    public ButtonManager confirmButton;
    public ButtonManager completeButton;
    public string activitySceneName;
    public bool isWalkthrough = false;

    void Start()
    {
        if (activityButton != null)
        {
            activityButton.onClick.RemoveAllListeners();
            activityButton.onClick.AddListener(() =>
            {
                if (isWalkthrough)
                {
                    ActivitySelectionManager.Instance.StartWalkthrough();
                }
                else
                {
                    ActivitySelectionManager.Instance.SetSelectedActivity(activitySceneName);
                }
            });
        }

        if (confirmButton != null)
        {
            confirmButton.onClick.RemoveAllListeners();
            confirmButton.onClick.AddListener(() =>
                ActivitySelectionManager.Instance.ConfirmActivity());
        }

        if (completeButton != null)
        {
            completeButton.onClick.RemoveAllListeners();
            completeButton.onClick.AddListener(() =>
                ActivitySelectionManager.Instance.CompleteActivity());
        }
    }
}