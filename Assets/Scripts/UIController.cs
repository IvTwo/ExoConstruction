using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject UIWrapper;
    public GameObject ConstructionActivityLibrary;
    public GameObject WearableRobotLibrary;
    public GameObject MainMenu;

    [Header("Ray Interaction Objects")]
    public GameObject MainMenuRayInteraction;
    public GameObject CALRayInteraction;
    public GameObject CALModalRayInteraction;
    public GameObject WRLRayInteraction;

    private void Start()
    {
        UIWrapper.SetActive(true);

        // Ensure only Main Menu is active at start
        MainMenu.SetActive(true);
        ConstructionActivityLibrary.SetActive(false);
        WearableRobotLibrary.SetActive(false);
        SetRayInteraction(MainMenuRayInteraction);
    }

    public void OnCALPressed()
    {
        Debug.Log("Switching to CAL");
        MainMenu.SetActive(false);
        ConstructionActivityLibrary.SetActive(true);
        SetRayInteraction(CALRayInteraction);
    }

    public void OnWRLPressed()
    {
        Debug.Log("Switching to WRL");
        MainMenu.SetActive(false);
        WearableRobotLibrary.SetActive(true);
        SetRayInteraction(WRLRayInteraction);
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Returning to Main Menu");

        ConstructionActivityLibrary.SetActive(false);
        WearableRobotLibrary.SetActive(false);
        MainMenu.SetActive(true);
        SetRayInteraction(MainMenuRayInteraction);
    }

    public void OnModalOpened(string modal)
    {
        Debug.Log("Switching to Modal Ray Interaction");

        if (modal.Equals("CAL"))
        {
            SetRayInteraction(CALModalRayInteraction);
        }
        
    }

    private void SetRayInteraction(GameObject target)
    {
        MainMenuRayInteraction.SetActive(false);
        CALRayInteraction.SetActive(false);
        CALModalRayInteraction.SetActive(false);
        WRLRayInteraction.SetActive(false);

        if (target != null)
        {
            target.SetActive(true);
        }
    }
}