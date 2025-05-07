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
        rend.material = new Material(rend.material);
    }
    void Update()
    {
        if (debugOverride || RobotSelectionManager.Instance.HasEquippedSuit())
        {
            rend.material.color = supportedColor;
            rend.material.SetColor("_EmissionColor", supportedColor);
            labelText.color = supportedColor;
            labelText.text = "Low Risk";
        }
        else
        {
            rend.material.color = unSupportedColor;
            rend.material.SetColor("_EmissionColor", unSupportedColor);
            labelText.color = unSupportedColor;
            labelText.text = "High Risk";
        }
    }
}
