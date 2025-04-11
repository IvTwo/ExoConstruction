using Michsky.UI.Heat;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    [SerializeField] private HorizontalSelector selector;
    [SerializeField] private float rotationSpeed = 10f;

    private Transform[] models;
    private int currentIndex = -1;

    void Start()
    {
        // Cache all direct children (models)
        int childCount = transform.childCount;
        models = new Transform[childCount];
        for (int i = 0; i < childCount; i++)
        {
            models[i] = transform.GetChild(i);
            models[i].gameObject.SetActive(false); // Start with all deactivated
        }

        // Register event and show default model
        selector.onValueChanged.AddListener(UpdateActiveModel);
        UpdateActiveModel(selector.index);
    }

    void Update()
    {
        if (currentIndex >= 0 && currentIndex < models.Length)
        {
            models[currentIndex].Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
        }
    }

    void UpdateActiveModel(int index)
    {
        // Disable all models
        foreach (Transform model in models)
            model.gameObject.SetActive(false);

        // Activate selected model
        if (index >= 0 && index < models.Length)
        {
            models[index].gameObject.SetActive(true);
            models[index].rotation = Quaternion.Euler(0f, 180f, 0f); // Reset rotation
            currentIndex = index;
        }
        else
        {
            currentIndex = -1;
        }
    }
}
