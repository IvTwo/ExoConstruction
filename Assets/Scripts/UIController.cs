using UnityEngine;

public class UIController : MonoBehaviour
{
    public Transform leftHandAnchor;
    public GameObject UIWrapper;
    public GameObject ConstructionActivityLibrary;
    public GameObject WearableRobotLibrary;
    public GameObject MainMenu;

    [Header("Ray Interaction Objects")]
    public GameObject MainMenuRayInteraction;
    public GameObject CALRayInteraction;
    public GameObject CALModalRayInteraction;
    public GameObject WRLRayInteraction;
    public GameObject WRLModalRayInteraction;

    [SerializeField] private float UIOffsetX = 0f;
    [SerializeField] private float UIOffsetY = 0.1f;
    [SerializeField] private float UIOffsetZ = 0.3f;

    [SerializeField] private bool isHandAnchored = false;
    private Vector3 UIOffset;
    private bool isUIActive;

    private void Start()
    {
        isUIActive = true;
        UIWrapper.SetActive(true);

        // Ensure only Main Menu is active at start
        MainMenu.SetActive(true);
        ConstructionActivityLibrary.SetActive(false);
        WearableRobotLibrary.SetActive(false);
        SetRayInteraction(MainMenuRayInteraction);

        // If the UI type is hand anchored then use Y toggle logic
        if (isHandAnchored)
        {
            UIOffset = new Vector3(UIOffsetX, UIOffsetY, UIOffsetZ);
            UIWrapper.transform.SetParent(leftHandAnchor);
            UIWrapper.transform.localPosition += UIOffset;
        }

    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four) && isHandAnchored)
        {
            ToggleUI();
        }
    }

    private void ToggleUI()
    {
        isUIActive = !isUIActive;
        UIWrapper.SetActive(isUIActive);
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

    public void OnReturnToMainMenuFromModal()
    {
        Debug.Log("Modal button pressed ? Returning to Main Menu");

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
        } else
        {
            SetRayInteraction(WRLModalRayInteraction);
        }
        
    }

    private void SetRayInteraction(GameObject target)
    {
        MainMenuRayInteraction.SetActive(false);
        CALRayInteraction.SetActive(false);
        CALModalRayInteraction.SetActive(false);
        WRLRayInteraction.SetActive(false);
        WRLModalRayInteraction.SetActive(false);

        if (target != null)
        {
            target.SetActive(true);
        }
    }
}