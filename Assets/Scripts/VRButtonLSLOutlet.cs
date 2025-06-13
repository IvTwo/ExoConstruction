using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;


public class VRButtonLSLOutlet : MonoBehaviour
{
    string StreamName = "VRButtonPressStream";
    string StreamType = "Markers";
    private StreamOutlet outlet;
    private string[] sample = { "" };

    void Start()
    {
        var hash = new Hash128();
        hash.Append(StreamName);
        hash.Append(StreamType);
        hash.Append(gameObject.GetInstanceID());
        StreamInfo streamInfo = new StreamInfo(StreamName, StreamType, 1, LSL.LSL.IRREGULAR_RATE, channel_format_t.cf_string, hash.ToString());
        outlet = new StreamOutlet(streamInfo);
    }

    // called once per frame
    public void SendMarker(string label)
    {
        if (outlet != null)
        {
            sample[0] = label;
            outlet.push_sample(sample);
        }
    }
}