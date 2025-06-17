using TMPro;
using UnityEngine;

//Used to update ergo highlights when exosuits are applied
public class HighlightUpdater : MonoBehaviour
{
    [SerializeField] Color supportedColor;
    [SerializeField] Color unSupportedColor;
    [SerializeField] GameObject ergoHighlight;
    [SerializeField] TextMeshProUGUI labelText;

    [SerializeField] private bool debugOverride = false;
    private Renderer rend;
    private Material runtimeMat;
    private bool hasSuit;
    private void Start()
    {
       if (supportedColor == null)
       {
            Debug.Log("Please assign an exosuit supported color");
       }

        if (unSupportedColor == null)
        {
            Debug.Log("Please assign an unsupported color");
        }

        rend = ergoHighlight.GetComponent<Renderer>();
        runtimeMat = new Material(rend.material);
        rend.material = runtimeMat;
        hasSuit = false;
    }
    void Update()
    {
        if (RobotSelectionManager.Instance != null)
        {
            hasSuit = RobotSelectionManager.Instance.HasEquippedSuit();
        }
        else
        {
            Debug.LogWarning("RobotSelectionManager not initialized");
        }

        if (debugOverride || hasSuit)
        {
            ergoHighlight.GetComponent<Renderer>().material.SetColor("_Color", supportedColor);
            ergoHighlight.GetComponent<Renderer>().material.SetColor("_EmissionColor", supportedColor);
            labelText.color = supportedColor;
            labelText.text = "Low Risk";
        }
        else
        {
            ergoHighlight.GetComponent<Renderer>().material.SetColor("_Color", unSupportedColor);
            ergoHighlight.GetComponent<Renderer>().material.SetColor("_EmissionColor", unSupportedColor);
            labelText.color = unSupportedColor;
            labelText.text = "High Risk";
        }
    }
}
