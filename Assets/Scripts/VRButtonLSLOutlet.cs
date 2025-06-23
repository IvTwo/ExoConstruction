using LSL;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VRButtonLSLOutlet : MonoBehaviour
{
    public static VRButtonLSLOutlet Instance;

    string StreamName = "VRStream";
    string StreamType = "Markers";
    private StreamOutlet outlet;
    private StreamOutlet outlet2;
    private string[] sample = { "" };
    private float[] positionSample = new float[9];

    private Transform headTransform;
    private Transform leftHandTransform;
    private Transform rightHandTransform;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        var hash = new Hash128();
        hash.Append(StreamName);
        hash.Append(StreamType);
        hash.Append(gameObject.GetInstanceID());

        StreamInfo streamInfo = new StreamInfo(StreamName, StreamType, 1, LSL.LSL.IRREGULAR_RATE, channel_format_t.cf_string, hash.ToString());
        outlet = new StreamOutlet(streamInfo);
        //Debug.Log("LSL stream created: " + StreamName);

        var hash2 = new Hash128();
        hash2.Append("VRPositionStream");
        hash2.Append("PositionStream");
        hash2.Append(gameObject.GetInstanceID());

        StreamInfo streamInfo2 = new StreamInfo("VRPositionStream", "PositionStream", 9, LSL.LSL.IRREGULAR_RATE, channel_format_t.cf_float32, hash2.ToString());
        outlet2 = new StreamOutlet(streamInfo2);
        //Debug.Log("LSL position stream created: " + "VRPositionStream");
    }

    void Update()
    {
        if (headTransform && leftHandTransform && rightHandTransform)
        {
            Vector3 h = headTransform.position;
            Vector3 l = leftHandTransform.position;
            Vector3 r = rightHandTransform.position;

            positionSample[0] = h.x;
            positionSample[1] = h.y;
            positionSample[2] = h.z;

            positionSample[3] = l.x;
            positionSample[4] = l.y;
            positionSample[5] = l.z;

            positionSample[6] = r.x;
            positionSample[7] = r.y;
            positionSample[8] = r.z;

            outlet2?.push_sample(positionSample);
        }
    }

    public void SendMarker(string label)
    {
        if (outlet != null)
        {
            sample[0] = label;
            outlet.push_sample(sample);
            //Debug.Log("LSL marker sent: " + label);
        }
        else
        {
            //Debug.Log("LSL outlet not initialized");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("Scene loaded: " + scene.name);
        SendMarker("Scene Entered: " + scene.name);

        headTransform = GameObject.Find("CenterEyeAnchor")?.transform;
        leftHandTransform = GameObject.Find("LeftHandAnchor")?.transform;
        rightHandTransform = GameObject.Find("RightHandAnchor")?.transform;
    }

    private void OnDestroy()
    {
        if (outlet != null)
        {
            outlet.Close();
            outlet = null;
        }

        if (outlet2 != null)
        {
            outlet2.Close();
            outlet2 = null;
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
