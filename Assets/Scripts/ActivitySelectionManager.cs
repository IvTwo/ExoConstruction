using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class ActivitySelectionManager : MonoBehaviour
{
    public static ActivitySelectionManager Instance { get; private set; }

    private string selectedSceneName;

    // Activity completion state
    private Dictionary<string, bool> activityCompletion = new Dictionary<string, bool>()
    {
        { "Carpentry", false },
        { "Construction_Labor", false },
        { "Drywall", false },
        { "Electrician", false },
        { "Exterior", false },
        { "Masonry", false }
    };

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

    public void CompleteActivity()
    {
        string activityName = SceneManager.GetActiveScene().name;

        if (activityCompletion.ContainsKey(activityName))
        {
            activityCompletion[activityName] = true;
            Debug.Log(activityName + " marked as completed.");

            // Go back to start scene once done
            SceneManager.LoadScene("Start");
        }
        else
        {
            Debug.LogWarning("Unknown activity: " + activityName);
        }
    }

    public bool IsActivityCompleted(string activityName)
    {
        return activityCompletion.ContainsKey(activityName) && activityCompletion[activityName];
    }

    public int GetCompletedCount()
    {
        int count = 0;
        foreach (var entry in activityCompletion)
        {
            if (entry.Key != "Exterior" && entry.Value)
            {
                count++;
            }
        }
        return count;
    }

    public int GetTotalCount()
    {
        return activityCompletion.Count - 1;
    }
}