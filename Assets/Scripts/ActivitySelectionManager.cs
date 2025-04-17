using UnityEngine;
using UnityEngine.SceneManagement;

public class ActivitySelectionManager : MonoBehaviour
{
    public static ActivitySelectionManager Instance { get; private set; }

    private string selectedSceneName;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartWalkthrough()
    {
        SceneManager.LoadScene("Exterior");
    }

    // Called when an activity button is clicked
    public void SetSelectedActivity(string sceneName)
    {
        selectedSceneName = sceneName;
        Debug.Log("Activity selected: " + sceneName);
    }

    // Called by the confirm button in the modal
    public void ConfirmActivity()
    {
        if (!string.IsNullOrEmpty(selectedSceneName))
        {
            SceneManager.LoadScene(selectedSceneName);
        }
        else
        {
            Debug.LogWarning("No activity selected before confirmation.");
        }
    }
}