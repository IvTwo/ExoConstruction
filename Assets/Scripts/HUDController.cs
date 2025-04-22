using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public Transform leftHandAnchor;
    public GameObject ActivityMenu;
    public GameObject ActivityHUDCanvas;
    public GameObject MenuRayInteraction;
    public GameObject ModalRayInteraction;

    [SerializeField] private float offsetX = 0f;
    [SerializeField] private float offsetY = 0.5f;
    [SerializeField] private float offsetZ = 0f;

    private bool isActivityMenuActive = false;

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
            ActivityHUDCanvas.SetActive(true);
            ActivityMenu.SetActive(false);
            SetRayInteraction(MenuRayInteraction);

            if (leftHandAnchor != null)
            {
                Vector3 offset = new Vector3(offsetX, offsetY, offsetZ);
                ActivityMenu.transform.SetParent(leftHandAnchor);
                ActivityMenu.transform.localPosition = offset;
            }
        }
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            ToggleActivityMenuAndHUD();
        }
    }

    private void ToggleActivityMenuAndHUD()
    {
        isActivityMenuActive = !isActivityMenuActive;
        ActivityMenu.SetActive(isActivityMenuActive);
        ActivityHUDCanvas.SetActive(!isActivityMenuActive);
    }

    public void OnReturnToMainMenuFromModal()
    {
        Debug.Log("Modal button pressed ? Returning to Main Menu");
        SceneManager.LoadSceneAsync("Start");
    }

    public void OnModalOpened()
    {
        Debug.Log("Switching to Modal Ray Interaction");

        SetRayInteraction(ModalRayInteraction);
    }

    public void OnModalClosed()
    {
        Debug.Log("Switching to Modal Ray Interaction");

        SetRayInteraction(MenuRayInteraction);
    }

    private void SetRayInteraction(GameObject target)
    {
        MenuRayInteraction.SetActive(false);
        ModalRayInteraction.SetActive(false);

        if (target != null)
        {
            target.SetActive(true);
        }
    }
}