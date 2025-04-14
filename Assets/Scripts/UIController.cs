using UnityEngine;

public class UI : MonoBehaviour
{
    public Transform leftHandAnchor;
    public GameObject UIWrapper;
    public GameObject ConstructionActivityLibrary;
    public GameObject MainMenu;
    // determines offset from controller anchor
    [SerializeField] private float UIOffsetX = 0f;
    [SerializeField] private float UIOffsetY = 0.1f;
    [SerializeField] private float UIOffsetZ = 0.3f;
    private Vector3 UIOffset;
    private bool isUIActive;

    private void Start()
    {
        isUIActive = false;
        ConstructionActivityLibrary.SetActive(false);
        // Set to main menu screen
        MainMenu.SetActive(true);
        UIWrapper.SetActive(isUIActive);
        UIOffset = new Vector3(UIOffsetX, UIOffsetY, UIOffsetZ);
        // Make the UI element a child of the left hand anchor to follow its movement
        UIWrapper.transform.SetParent(leftHandAnchor);
        // Offset the UI elements position
        UIWrapper.transform.localPosition += UIOffset;
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            ToggleUI();
        }
    }

    private void ToggleUI()
    {
        Debug.Log("Toggling UI");
        isUIActive = !isUIActive;
        UIWrapper.SetActive(isUIActive);
    }

    // CAL: Construction activity library
    public void OnCALPressed()
    {
        Debug.Log("Construction Activity Library button pressed!");
        // Swap to CAL screen and turn off main menu
        ConstructionActivityLibrary.SetActive(true);
        MainMenu.SetActive(false);
        return;
    }

    // WRL: Wearable robot library
    public void OnWRLPressed() 
    {
        Debug.Log("Wearable Robot Library button pressed!");
        return;
    }
}
