using UnityEngine;
using UnityEngine.XR;

public class UI : MonoBehaviour
{
    public Transform leftHandAnchor;
    public GameObject uiElement;
    // determines offset from controller anchor
    [SerializeField] private float UIOffsetX = 0f;
    [SerializeField] private float UIOffsetY = 0.1f;
    [SerializeField] private float UIOffsetZ = 0.3f;
    private Vector3 UIOffset;
    private bool isUIActive;

    private void Start()
    {
        isUIActive = false;
        uiElement.SetActive(isUIActive);
        UIOffset = new Vector3(UIOffsetX, UIOffsetY, UIOffsetZ);
        // Make the UI element a child of the left hand anchor to follow its movement
        uiElement.transform.SetParent(leftHandAnchor);
        // Offset the UI elements position
        uiElement.transform.localPosition += UIOffset;
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
        isUIActive = !isUIActive;
        uiElement.SetActive(isUIActive);
    }
}
