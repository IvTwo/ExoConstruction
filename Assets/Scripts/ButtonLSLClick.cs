using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLSLClick : MonoBehaviour
{
    public VRButtonLSLOutlet lslOutlet;
    public string streamOutputText;

    public void OnButtonClick()
    {
        if (lslOutlet != null)
        {
            lslOutlet.SendMarker("UIButtonClicked: " + streamOutputText);
        }
    }
}