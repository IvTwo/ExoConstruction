using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LSL;
using UnityEngine.SceneManagement;


public class VRButtonLSLOutlet : MonoBehaviour
{
    public static VRButtonLSLOutlet Instance;

    string StreamName = "VRStream";
    string StreamType = "Markers";
    private StreamOutlet outlet;
    private string[] sample = { "" };

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
        Debug.Log("LSL stream created: " + StreamName);
    }

    // called once per frame
    public void SendMarker(string label)
    {
        if (outlet != null)
        {
            sample[0] = label;
            outlet.push_sample(sample);
            Debug.Log("LSL marker sent: " + label);
        }
        else
        {
            Debug.Log("LSL outlet not initialized");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
        SendMarker("Scene Entered: " + scene.name);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}